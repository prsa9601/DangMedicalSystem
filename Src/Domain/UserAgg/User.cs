using Common.Domain;

namespace Domain.UserAgg
{
    public class User : AggregateRoot
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Password { get; set; }
        public string NationalityCode { get; set; }
        public string ImageName { get; set; }
        public string NationalCardPhoto { get; set; }
        public string BirthCertificatePhoto { get; set; }


        public List<UserOtp> UserOtps { get; set; }
        public List<UserBlock> UserBlocks { get; set; }
        public List<UserSession> UserSessions { get; set; }
        public User()
        {
            UserBlocks = new();
            UserOtps = new();
            UserSessions = new();
        }
        public void SetUserOtp(DateTime expireDate, string otpCode)
        {
            var userOtp = new UserOtp(otpCode, expireDate);
            userOtp.UserId = Id;
            UserOtps.Add(userOtp);
        }

        public void SetUserBlock(DateTime blockToDate, string description)
        {
            var userBlock = new UserBlock(blockToDate, description);
            userBlock.UserId = Id;
            UserBlocks.Add(userBlock);
        }
        public void SetUserSession(string jwtRefreshToken, string ipAddress, DateTime expireDate)
        {
            var userSession = new UserSession(jwtRefreshToken, ipAddress, expireDate);
            userSession.UserId = Id;
            UserSessions.Add(userSession);
        }
    }
}
