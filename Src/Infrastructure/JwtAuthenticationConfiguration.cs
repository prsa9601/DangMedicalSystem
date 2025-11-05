using Application.Shared.Abstractions.Jwt;
using Application.Shared.Abstractions.Jwt.Interfaces;
using Infrastructure.Auth.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{
    public static class JwtAuthenticationConfiguration
    {
        public static IServiceCollection ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
                options.Secure = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
                options.CheckConsentNeeded = context => true;
            });

            services.AddAntiforgery(options =>
            {
                options.Cookie.Name = "XSRF-TOKEN";
                options.Cookie.HttpOnly = false;
                options.HeaderName = "X-XSRF-TOKEN";
            });
                services.AddAuthorization();

            // Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtConfig:SecretKey"])),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JwtConfig:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JwtConfig:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                //options.SaveToken = true;
                //options.Events = new JwtBearerEvents
                //{
                //    OnChallenge = async context =>
                //    {
                //        // جلوگیری از پاسخ پیش‌فرض
                //        context.HandleResponse();

                //        if (context.Request.Path.StartsWithSegments("/api"))
                //        {
                //            context.Response.StatusCode = 401;
                //            await context.Response.WriteAsync("Unauthorized");
                //        }
                //        else
                //        {
                //            context.Response.Redirect("/Auth/Login");
                //        }
                //    }
                //};
            });
            //.AddCookie(options =>
            //{
            //    options.Cookie.Name = "auth-Token";
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
            //    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
            //    options.Cookie.MaxAge = TimeSpan.FromMinutes(60);

                //options.Events = new CookieAuthenticationEvents
                //{
                //    OnValidatePrincipal = async context =>
                //    {
                //        // بررسی انقضا و رفرش توکن
                //        await TokenRefreshMiddleware(context);
                //    }


                // ۱. هنگام ساخت کوکی
                //OnSigningIn = async context =>
                //{
                //    // قبل از ایجاد کوکی احراز هویت
                //    await Task.CompletedTask;
                //},
                //
                //// ۲. پس از ورود موفق
                //OnSignedIn = async context =>
                //{
                //    // بعد از ایجاد کوکی
                //    await Task.CompletedTask;
                //},
                //
                //// ۳. هنگام خروج
                //OnSigningOut = async context =>
                //{
                //    // قبل از پاک کردن کوکی
                //    await Task.CompletedTask;
                //},
                //
                //// ۴. اعتبارسنجی اصلی (که شما دیدید)
                //OnValidatePrincipal = async context =>
                //{
                //    // بررسی انقضا و رفرش توکن
                //    await TokenRefreshMiddleware(context);
                //},
                //
                //// ۵. هنگامی که کاربر دسترسی ندارد
                //OnRedirectToAccessDenied = async context =>
                //{
                //    // مدیریت خطای دسترسی ممنوع
                //    context.Response.StatusCode = 403;
                //    await context.Response.WriteAsync("Access Denied");
                //    context.Response.Redirect("/AccessDenied");
                //},
                //
                //// ۶. هنگامی که کاربر لاگین نکرده
                //OnRedirectToLogin = async context =>
                //{
                //    // مدیریت redirect به صفحه لاگین
                //    if (context.Request.Path.StartsWithSegments("/api"))
                //    {
                //        context.Response.StatusCode = 401;
                //        await context.Response.WriteAsync("Unauthorized");
                //    }
                //    else
                //    {
                //        context.Response.Redirect("/Login");
                //    }
                //},
                //
                //// ۷. هنگامی که کاربر از لاگ اوت بازدید می‌کند
                //OnRedirectToLogout = async context =>
                //{
                //    // مدیریت redirect به صفحه خروج
                //    context.Response.Redirect("/Logout");
                //},
                //
                //// ۸. هنگامی که کوکی منقضی شده
                //OnRedirectToReturnUrl = async context =>
                //{
                //    // مدیریت بازگشت به URL اصلی
                //    context.Response.Redirect(context.RedirectUri);
                //}

                //};



                //});


            //Jwt
            services.AddSingleton<JwtSettings>(new JwtSettings());
            services.AddScoped<IJwtSettingsFactory, JwtSettingsFactory>();
            services.AddScoped<IJwtServcie, JwtService>();

            // در InfrastructureBootstrapper
            services.AddScoped<IJwtServcie>(provider =>
            {
                return new JwtService(new JwtSettings());
            });
         


            return services;
        }
    }
}
