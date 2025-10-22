using Common.Query;

namespace Query.User.DTOs
{
    public class UserBlockDto : BaseDto
    {
        public Guid UserId { get; set; }
        public DateTime BlockToDate { get; set; }
        public string Description { get; set; } //چرا بلاک شده
        public bool IsActive { get; set; }
    }
}
