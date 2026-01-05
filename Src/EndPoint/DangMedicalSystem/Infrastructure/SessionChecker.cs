using Application.Auth.Commands.GetSession;
using Facade.Role;
using Facade.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace DangMedicalSystem.Api.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class SessionChecker : Attribute, IAsyncAuthorizationFilter
    {
        private IUserFacade _userFacade;

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            _userFacade = context.HttpContext.RequestServices.GetRequiredService<IUserFacade>();
            var res = await UserHasSession(context);
            if (context.HttpContext.User.Identity.IsAuthenticated != null
                && context.HttpContext.User.Identity.IsAuthenticated)
            {
                if (res == false)
                {
                    //context.HttpContext.Request.Headers["RefreshToken"] = StringValues.Empty;
                    context.HttpContext.Response.Headers.Append("RefreshToken", "Logout");
                }
            }
            else
            {
                if (res == false)
                {
                    //context.HttpContext.Request.Headers["RefreshToken"] = StringValues.Empty;
                    context.HttpContext.Response.Headers["RefreshToken"] = "Logout";
                }
                //context.Result = new UnauthorizedObjectResult("Unauthorized");
            }
        }
        private async Task<bool> UserHasSession(AuthorizationFilterContext context)
        {
            var refreshToken = context.HttpContext.Request.Headers["RefreshToken"].ToString();
            var authToken = context.HttpContext.Request.Headers["AuthToken"].ToString();
            var res = await _userFacade.GetSession(new GetSessionCommand
            {
                RefreshToken = refreshToken,
            });
            var res2 = await _userFacade.GetSession(new GetSessionCommand
            {
                RefreshToken = authToken,
            });
            return res.Data == false ? res2.Data : res.Data;
        }
    }
}
