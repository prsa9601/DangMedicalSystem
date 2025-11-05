using Application.Shared.Abstractions.Jwt.Enum;
using Application.Shared.Abstractions.Jwt.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace Infrastructure.Shared.Middleware
{
    public class JwtAuthenticationMiddleware
    {


        private readonly RequestDelegate _next;

        public JwtAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, Context dbContext, IJwtSettingsFactory jwtFactory)
        {
            var endpoint = context.GetEndpoint();
            var authorizeAttribute = endpoint?.Metadata.GetMetadata<AuthorizeAttribute>();

            if (authorizeAttribute == null)
            {
                await _next(context);
                return;
            }

            try
            {
                string accessToken = ExtractAccessToken(context.Request);
                string refreshToken = context.Request.Cookies["RefreshToken"];

                bool isAccessTokenValid = false;

                // بررسی توکن دسترسی
                if (!string.IsNullOrEmpty(accessToken))
                {
                    isAccessTokenValid = await ValidateAccessToken(context, dbContext, jwtFactory, accessToken);
                }

                // اگر توکن دسترسی معتبر باشد
                if (isAccessTokenValid)
                {
                    await _next(context);
                    return;
                }

                // اگر توکن دسترسی نامعتبر است اما توکن رفرش معتبر است
                if (!string.IsNullOrEmpty(refreshToken))
                {
                    var refreshResult = await TryRefreshTokens(context, dbContext, jwtFactory, refreshToken);
                    if (refreshResult.success)
                    {
                        // تنظیم توکن جدید در هدر
                        context.Request.Headers["Authorization"] = $"Bearer {refreshResult.newAccessToken}";
                        await _next(context);
                        return;
                    }
                }

                // اگر هیچکدام معتبر نباشند
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized - Please login again");
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Internal Server Error");
            }
        }

        private string ExtractAccessToken(HttpRequest request)
        {
            if (request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                var token = authHeader.ToString().Replace("Bearer ", "");
                return string.IsNullOrEmpty(token) ? null : token;
            }
            return null;
        }

        private async Task<bool> ValidateAccessToken(HttpContext context, Context dbContext, IJwtSettingsFactory jwtFactory, string token)
        {
            try
            {
                var jwtService = jwtFactory.CreateSetting(TokenType.AuthToken);
                var principal = jwtService.ValidateToken(token);

                if (principal == null) return false;

                // بررسی بلاک لیست کاربر
                if (await IsUserBlocked(dbContext, principal))
                {
                    return false;
                }

                // تنظیم کاربر در context برای استفاده در کنترلرها
                context.User = principal;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private async Task<(bool success, string newAccessToken)> TryRefreshTokens(HttpContext context, Context dbContext, IJwtSettingsFactory jwtFactory, string refreshToken)
        {
            var refreshTokenService = jwtFactory.CreateSetting(TokenType.AuthRefreshToken);

            try
            {
                var refreshPrincipal = refreshTokenService.ValidateToken(refreshToken);
                if (refreshPrincipal == null) return (false, null);

                // بررسی وجود کاربر و وضعیت بلاک
                if (!await ValidateUserAndBlockStatus(context, dbContext, refreshPrincipal))
                    return (false, null);

                // تولید توکن دسترسی جدید
                var newAccessToken = GenerateNewAccessToken(jwtFactory, refreshPrincipal);
                var newRefreshToken = GenerateNewRefreshToken(jwtFactory, refreshPrincipal);

                // آپدیت کوکی رفرش توکن
                SetRefreshTokenCookie(context, newRefreshToken);

                // تنظیم principal جدید در context از روی توکن دسترسی جدید
                var authTokenService = jwtFactory.CreateSetting(TokenType.AuthToken);
                var newPrincipal = authTokenService.ValidateToken(newAccessToken);
                context.User = newPrincipal;

                return (true, newAccessToken);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }

        private async Task<bool> ValidateUserAndBlockStatus(HttpContext context, Context dbContext, ClaimsPrincipal principal)
        {
            if (!Guid.TryParse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            {
                return false;
            }

            var user = await dbContext.Users
                .Include(u => u.UserBlocks)
                .Include(u => u.UserSessions)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            // بررسی اینکه رفرش توکن در سشن‌های کاربر وجود دارد
            var currentRefreshToken = context.Request.Cookies["RefreshToken"];
            var hasValidSession = user.UserSessions.Any(s =>
                s.JwtRefreshToken.Equals(currentRefreshToken) && s.IsActive);

            if (!hasValidSession)
            {
                return false;
            }

            if (user.UserBlocks.Any(block => block.IsActive && block.BlockToDate > DateTime.Now))
            {
                return false;
            }

            return true;
        }

        private async Task<bool> IsUserBlocked(Context dbContext, ClaimsPrincipal principal)
        {
            if (!Guid.TryParse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
                return true;

            var user = await dbContext.Users
                .Include(u => u.UserBlocks)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.UserBlocks.Any(block => block.IsActive && block.BlockToDate > DateTime.Now) == true;
        }

        private string GenerateNewAccessToken(IJwtSettingsFactory jwtFactory, ClaimsPrincipal principal)
        {
            var authTokenService = jwtFactory.CreateSetting(TokenType.AuthToken);
            Guid userId = Guid.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            string phoneNumber = principal.FindFirst(JwtRegisteredClaimNames.PhoneNumberVerified)?.Value;
            var roles = principal.FindAll(ClaimTypes.Role)?.Select(r => r.Value).ToList();

            return authTokenService.GenerateToken(userId, phoneNumber, roles);
        }

        private string GenerateNewRefreshToken(IJwtSettingsFactory jwtFactory, ClaimsPrincipal principal)
        {
            var refreshTokenService = jwtFactory.CreateSetting(TokenType.AuthRefreshToken);
            Guid userId = Guid.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            string phoneNumber = principal.FindFirst(JwtRegisteredClaimNames.PhoneNumberVerified)?.Value;
            var roles = principal.FindAll(ClaimTypes.Role)?.Select(r => r.Value).ToList();

            return refreshTokenService.GenerateToken(userId, phoneNumber, roles);
        }

        private void SetRefreshTokenCookie(HttpContext context, string refreshToken)
        {
            context.Response.Cookies.Delete("RefreshToken");
            context.Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.Now.AddDays(7)
            });
        }
    }
}

