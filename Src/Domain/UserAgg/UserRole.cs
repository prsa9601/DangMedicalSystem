using Common.Domain;

namespace Domain.UserAgg
{
    public class UserRole : BaseEntity
    {
        public Guid RoleId { get; private set; }
        public Guid UserId { get; internal set; }

        public UserRole(Guid roleId)
        {
            RoleId = roleId;
        }

    }
}
