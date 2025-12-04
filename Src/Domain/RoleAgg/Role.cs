using Common.Domain;
using System.Security;

namespace Domain.RoleAgg
{
    public class Role : AggregateRoot
    {
        public string Title { get; set; }
        public List<RolePermission> RolePermissions { get; set; } = new();
    }
}
