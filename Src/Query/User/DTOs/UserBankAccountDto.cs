using Common.Query;

namespace Query.User.DTOs
{
    public class UserBankAccountDto : BaseDto
    {
        public string Shaba { get; set; }
        public string CardNumber { get; set; }
        public string FullName { get; set; }
        public bool IsConfirmed { get; set; }
        public Guid UserId { get; set; }
    }
}
