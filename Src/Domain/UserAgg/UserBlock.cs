using Common.Domain;

namespace Domain.UserAgg
{
    public class UserBlock : BaseEntity
    {
        public UserBlock(DateTime blockToDate, string description)
        {
            BlockToDate = blockToDate;
            Description = description;
            IsActive = true;
        }

        public Guid UserId { get; internal set; }
        public DateTime BlockToDate { get; private set; }
        public string Description { get; private set; } //چرا بلاک شده
        public bool IsActive { get; private set; }

        public void ChangeActivity(bool isActive)
        {
            IsActive = isActive;
        }
    }
}
