using Common.Query;

namespace Query.User.DTOs
{
    public class UserBankAccountDto : BaseDto
    {
        public string Shaba { get; set; }
        public string CardNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsConfirmed { get; set; }
        public int ExpirationDateMonth { get; set; }
        public int ExpirationDateYear { get; set; }
        public Guid UserId { get; set; }
    }
}
