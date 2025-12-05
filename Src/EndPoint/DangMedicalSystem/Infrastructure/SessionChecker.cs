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

            if (context.HttpContext.User.Identity.IsAuthenticated != null
                && context.HttpContext.User.Identity.IsAuthenticated)
            {
                if (await UserHasSession(context) == false)
                {
                    //context.HttpContext.Request.Headers["RefreshToken"] = StringValues.Empty;
                    context.HttpContext.Response.Headers.Append("RefreshToken", "Logout");
                }
            }
            else
            {
                if (await UserHasSession(context) == false)
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
            var res = await _userFacade.GetSession(new GetSessionCommand
            {
                RefreshToken = refreshToken,
            });
            return res.Data;
        }
    }
}
