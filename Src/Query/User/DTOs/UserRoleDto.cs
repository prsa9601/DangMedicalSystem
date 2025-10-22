using Common.Query;
using Domain.RoleAgg.Enum;

namespace Query.User.DTOs
{
    public class UserRoleDto : BaseDto
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
        public string RoleName { get; set; }
        public List<Permission> Permissions { get; set; } = new();
    }
}
