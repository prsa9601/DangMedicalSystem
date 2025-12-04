using Common.Query;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Role.DTOs;

namespace Query.Role.GetRoles
{
    public class GetRolesQuery : IQuery<List<RoleDto>?>
    {
    }
    internal sealed class GetRolesQueryHandler : IQueryHandler<GetRolesQuery, List<RoleDto>?>
    {
        private readonly Context _context;

        public GetRolesQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<List<RoleDto>?> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = _context.Roles;
            if (roles == null) return null;

            return await roles.Select(x => new RoleDto()
            {
                CreationDate = x.CreationDate,
                Id = x.Id,
                RolePermissions = x.RolePermissions.Select(i=>new RolePermissionDto
                {
                    CreationDate = i.CreationDate,
                    Id = i.Id,  
                    Permission = i.Permission,
                    RoleId = i.RoleId,
                }).ToList(),
                Title = x.Title,
            }).ToListAsync();
        }
    }
}
