using Common.Domain;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Domain.UserAgg
{
    public class UserSession : BaseEntity
    {

        public UserSession(string jwtRefreshToken, string ipAddress, DateTime expireDate)
        {
            JwtRefreshToken = jwtRefreshToken;
            IpAddress = ipAddress;
            ExpireDate = expireDate;
            IsActive = ExpireDate > DateTime.Now ? true : false;
        }
        public Guid UserId { get; internal set; }
        public string JwtRefreshToken { get; private set; }

        //public string JwtAuthToken { get; set; }

        public DateTime ExpireDate { get; private set; }
        public bool IsActive { get; private set; } // اگه کاربر لاگ ائت کنه فالس میشه
        public string IpAddress { get; private set; }

        public void ChangeActivity(bool isActive)
        {
            IsActive = isActive;
        }
    }
}
