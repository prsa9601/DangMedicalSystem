using Common.Domain;
using System.Security;

namespace Domain.RoleAgg
{
    public class Role : BaseEntity
    {
        public string Title { get; set; }
        public List<RolePermission> RolePermissions { get; set; }
    }
}
