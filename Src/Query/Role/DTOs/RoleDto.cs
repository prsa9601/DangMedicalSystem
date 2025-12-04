using Common.Domain;
using Common.Query;
using Domain.RoleAgg.Enum;

namespace Query.Role.DTOs
{
    public class RoleDto : BaseDto
    {
        public string Title { get; set; }
        public List<RolePermissionDto> RolePermissions { get; set; }
    }

    public class RolePermissionDto : BaseDto
    {
        public Guid RoleId { get; set; }
        public Permission Permission { get; set; }
    }
}
