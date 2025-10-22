using Common.Query;

namespace Query.User.DTOs
{
    public class UserSessionDto : BaseDto
    {
        public Guid UserId { get; set; }
        public string JwtRefreshToken { get; set; }

        //public string JwtAuthToken { get; set; }

        public DateTime ExpireDate { get; set; }
        public bool IsActive { get; set; } // اگه کاربر لاگ اوت کنه فالس میشه
        public string IpAddress { get; set; }

    }
}
