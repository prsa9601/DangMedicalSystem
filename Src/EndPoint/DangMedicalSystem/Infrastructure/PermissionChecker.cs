using Common.AspNetCore;
using Domain.RoleAgg.Enum;
using Domain.RoleAgg.Interfaces.Repository;
using Domain.UserAgg.Interfaces.Repository;
using Facade.Role;
using Facade.User;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DangMedicalSystem.Api.Infrastructure
{
    public class PermissionChecker(Permission permission) : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private IRoleFacade _roleFacade;
        private IUserFacade _userFacade;

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            _roleFacade = context.HttpContext.RequestServices.GetRequiredService<IRoleFacade>();
            _userFacade = context.HttpContext.RequestServices.GetRequiredService<IUserFacade>();

            if (context.HttpContext.User.Identity.IsAuthenticated != null
                && context.HttpContext.User.Identity.IsAuthenticated)
            {
                if (await UserHasPermission(context) == false)
                {
                    context.Result = new ForbidResult();
                }
            }
            else
            {
                context.Result = new UnauthorizedObjectResult("Unauthorized");
            }
        }
        private async Task<bool> UserHasPermission(AuthorizationFilterContext context)
        {
            var user = await _userFacade.GetById(context.HttpContext.User.GetUserId());
            if (user == null)
                return false;

            //var roleNames = user.UserRole.Select(s => s.RoleName).ToList();
            var roleNames = user.UserRole.RoleName;
            var roles = await _roleFacade.GetRoles();
            if (roles == null)
            {
                return false;
            }
            var userRoles = roles.Where(i => roleNames.Contains(i.Title)).ToList();

            return userRoles.Any(i => i.RolePermissions.Any(x => x.Permission.Equals(permission)));
        }
    }
}
