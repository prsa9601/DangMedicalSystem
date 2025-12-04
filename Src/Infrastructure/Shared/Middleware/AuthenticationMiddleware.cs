using Application.Shared.Abstractions.Jwt.Enum;
using Application.Shared.Abstractions.Jwt.Interfaces;
using Domain.UserAgg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace Infrastructure.Shared.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, Context dbContext, IJwtSettingsFactory _jwtFactory)
        {
            try
            {
                // بررسی اینکه آیا endpoint جاری دارای اتریبیوت [Authorize] است
                var endpoint = context.GetEndpoint();
                var authorizeAttribute = endpoint?.Metadata.GetMetadata<AuthorizeAttribute>();

                // اگر endpoint دارای [Authorize] نبود، به ادامه پردازش برو
                if (authorizeAttribute == null)
                {

                    if (!context.Request.Headers.TryGetValue("AuthToken", out var _))
                    {
                        var refreshTokenHeader = context.Request.Headers["RefreshToken"].ToString();
                        var refreshTokenCookie = context.Request.Cookies["RefreshToken"]?.ToString();

                        var refreshToken = (!string.IsNullOrEmpty(refreshTokenHeader) ? refreshTokenHeader : refreshTokenCookie)?.Replace("Bearer ", "");
                        var jwtServiceAuthRefreshToken = _jwtFactory.CreateSetting(TokenType.AuthRefreshToken);
                        var jwtServiceAuthToken = _jwtFactory.CreateSetting(TokenType.AuthToken);


                        try
                        {
                            var refreshTokenValidate = jwtServiceAuthRefreshToken.ValidateToken(refreshToken);
                            Guid.TryParse(refreshTokenValidate.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id);

                            if (refreshTokenValidate.FindFirst
                                (JwtRegisteredClaimNames.PhoneNumberVerified)?.Value == null)
                            {
                                context.Response.StatusCode = 401;
                                await context.Response.WriteAsync("PhoneNumber is Not Exist In Token");
                                return;
                            }
                            var authToken = jwtServiceAuthToken.GenerateToken(
                                   id,
                                   refreshTokenValidate.FindFirst(JwtRegisteredClaimNames.PhoneNumberVerified)!.Value!,
                                   refreshTokenValidate.FindAll(ClaimTypes.Role)?.Select(r => r.Value).ToList());

                            context.Response.Cookies.Append("auth-Token",
                                   authToken,
                                   new CookieOptions
                                   {
                                       HttpOnly = true,
                                       Secure = true,
                                       SameSite = SameSiteMode.Strict,
                                       Expires = DateTime.UtcNow.AddMinutes(30),
                                       Path = "/",
                                   });

                            context.Response.Headers["X-Auth-Token"] = authToken;
                            //context.Request.Headers["Authorization"] = $"Bearer {authToken}";
                            context.Response.Headers["Authorization"] = $"Bearer {authToken}";

                        }
                        catch
                        {
                            await _next(context);
                            return;
                        }
                    }
                    await _next(context);
                    return;
                }

                if (context.Request.Headers.TryGetValue("AuthToken", out var authHeader))
                {
                    string token = authHeader.ToString().Replace("Bearer ", "");
                    //var refreshToken = context.Request.Cookies["RefreshToken"].Replace("Bearer ", "");
                    var refreshToken = context.Request.Headers["RefreshToken"].ToString()
                        .Replace("Bearer ", "") ?? null;

                    var jwtServiceAuthRefreshToken = _jwtFactory.CreateSetting(TokenType.AuthRefreshToken);

                    var userEntity = await dbContext.Users.FirstOrDefaultAsync(user => user.UserSessions.
                      Any(session => session.JwtRefreshToken.Equals(refreshToken)));

                    //if (userEntity is null)
                    //{
                    //    context.Response.StatusCode = 401;
                    //    await context.Response.WriteAsync("User Is Not Found");
                    //    return;
                    //}

                    //var blackListToken = userEntity.UserBlocks.Any(i => i.BlockToDate > DateTime.Now && i.IsActive == true);

                    //if (blackListToken == true)
                    //{
                    //    context.Response.StatusCode = 401;
                    //    await context.Response.WriteAsync("Token is in BlackList");
                    //    return;
                    //}

                    var jwtServiceAuthToken = _jwtFactory.CreateSetting(TokenType.AuthToken);
                    var user = jwtServiceAuthToken.ValidateToken(token);

                    // اگر توکن منقضی شده، رفرش توکن را چک کن

                    if (user != null)
                    {

                        if (string.IsNullOrEmpty(refreshToken))
                        {
                            if (user.FindFirst(JwtRegisteredClaimNames.PhoneNumberVerified)?.Value == null)
                            {
                                context.Response.StatusCode = 401;
                                await context.Response.WriteAsync("PhoneNumber is Not Exist In Token");
                                return;
                            }


                            Guid.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id);

                            var refreshTokenAuth = jwtServiceAuthRefreshToken.GenerateToken(
                                id,
                                user.FindFirst(JwtRegisteredClaimNames.PhoneNumberVerified)!.Value!,
                                user.FindAll(ClaimTypes.Role)?.Select(r => r.Value).ToList());


                            // توکن جدید را به هدر اضافه کن
                            //context.Request.Headers["Authorization"] = $"Bearer {newTokens.AccessToken}";

                            // کوکی رفرش توکن را آپدیت کن
                            context.Response.Cookies.Append("RefreshToken",
                                refreshTokenAuth,
                                new CookieOptions
                                {
                                    HttpOnly = true,
                                    Secure = true,
                                    SameSite = SameSiteMode.Strict
                                });
                            context.Request.Headers["Authorization"] = $"Bearer {token}";

                            await _next(context);
                            return;
                        }
                        else
                        {
                            try
                            {
                                var refreshTokenValidate = jwtServiceAuthRefreshToken.ValidateToken(refreshToken);
                            }
                            catch
                            {

                                Guid.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id);

                                var refreshTokenAuth = jwtServiceAuthRefreshToken.GenerateToken(
                                    id,
                                    user.FindFirst(JwtRegisteredClaimNames.PhoneNumberVerified)!.Value!,
                                    user.FindAll(ClaimTypes.Role)?.Select(r => r.Value).ToList());


                                // توکن جدید را به هدر اضافه کن
                                //context.Request.Headers["Authorization"] = $"Bearer {newTokens.AccessToken}";

                                // کوکی رفرش توکن را آپدیت کن
                                context.Response.Cookies.Delete("RefreshToken");
                                context.Response.Cookies.Append("RefreshToken",
                                    refreshTokenAuth,
                                    new CookieOptions
                                    {
                                        HttpOnly = true,
                                        Secure = true,
                                        SameSite = SameSiteMode.Strict
                                    });

                            }

                            context.Request.Headers["Authorization"] = $"Bearer {token}";
                            await _next(context);
                            return;
                        }
                    }
                    if (user == null)
                    {
                        if (!string.IsNullOrEmpty(refreshToken))
                        {
                            var refreshTokenClaimsPrincipal = jwtServiceAuthRefreshToken.ValidateToken(refreshToken);

                            Guid.TryParse(refreshTokenClaimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id);

                            if (refreshTokenClaimsPrincipal.FindFirst
                                (JwtRegisteredClaimNames.PhoneNumberVerified)?.Value == null)
                            {
                                context.Response.StatusCode = 401;
                                await context.Response.WriteAsync("PhoneNumber is Not Exist In Token");
                                return;
                            }

                            var authToken = jwtServiceAuthToken.GenerateToken(
                                id,
                                refreshTokenClaimsPrincipal.FindFirst(JwtRegisteredClaimNames.PhoneNumberVerified)!.Value!,
                                refreshTokenClaimsPrincipal.FindAll(ClaimTypes.Role)?.Select(r => r.Value).ToList());

                            context.Request.Headers["Authorization"] = $"Bearer {authToken}";
                            await _next(context);
                            return;
                        }
                        else
                        {
                            context.Response.StatusCode = 401;
                            await context.Response.WriteAsync("You Need To Login");
                            return;
                        }
                    }
                }
                else
                {

                    //var refreshToken2 = context.Request.Cookies["RefreshToken"];
                    var refreshToken = context.Request.Headers["RefreshToken"].ToString()
                        .Replace("Bearer ", "") ?? null;
                    if (refreshToken is null)
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("You Need To Login");
                        return;
                    }
                    //context.Request.Cookies["RefreshToken"]
                    var jwtServiceAuthRefreshToken = _jwtFactory.CreateSetting(TokenType.AuthRefreshToken);

                    var refreshTokenClaimsPrincipal = jwtServiceAuthRefreshToken.ValidateToken(refreshToken);

                    if (refreshTokenClaimsPrincipal == null)
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("You Need To Login");
                        return;
                    }

                    var jwtServiceAuthToken = _jwtFactory.CreateSetting(TokenType.AuthToken);
                    Guid.TryParse(refreshTokenClaimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id);

                    var authToken = jwtServiceAuthToken.GenerateToken(
                        id,
                        refreshTokenClaimsPrincipal.FindFirst(JwtRegisteredClaimNames.PhoneNumberVerified)!.Value!,
                        refreshTokenClaimsPrincipal.FindAll(ClaimTypes.Role)?.Select(r => r.Value).ToList());

                    context.Response.Cookies.Append("auth-Token",
                                   authToken,
                                   new CookieOptions
                                   {
                                       HttpOnly = true,
                                       Secure = true,
                                       SameSite = SameSiteMode.Strict,
                                       Expires = DateTime.UtcNow.AddMinutes(30),
                                       Path = "/",
                                   });

                    context.Request.Headers["Authorization"] = $"Bearer {authToken}";
                    //context.Response.Headers["Authorization"] = $"Bearer {authToken}";

                    // ✅ ۲. ارسال توکن در هدر مخصوص برای فرانت‌اند
                    context.Response.Headers["X-Auth-Token"] = authToken;

                    await _next(context);
                    return;

                }
                await _next(context);
                return;
            }
            catch
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("You Need To Login");
                return;
            }
        }
    }
}
