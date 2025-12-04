using MediatR;
using Query.Role.DTOs;
using Query.Role.GetRoles;

namespace Facade.Role
{
    public interface IRoleFacade
    {
        Task<List<RoleDto>?> GetRoles();
    }
    public class RoleFacade : IRoleFacade
    {
        private readonly IMediator _mediator;

        public RoleFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<List<RoleDto>?> GetRoles()
        {
            return await _mediator.Send(new GetRolesQuery());
        }
    }
}
