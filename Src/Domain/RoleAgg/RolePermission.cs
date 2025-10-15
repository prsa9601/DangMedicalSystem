using Common.Domain;
using Domain.RoleAgg.Enum;

namespace Domain.RoleAgg
{
    public class RolePermission : BaseEntity
    {
        public Guid RoleId { get; set; }
        public Permission Permission { get; set; }
    }
}
