using Common.Query;
using Domain.UserAgg;
using Domain.UserAgg.Enum;

namespace Query.User.DTOs
{
    public class UserDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalityCode { get; set; }
        public string ImageName { get; set; }
        public string NationalCardPhoto { get; set; }
        public string BirthCertificatePhoto { get; set; }
        public UserStatus Status { get; set; }
        public bool IsActive { get; set; }
        public UserRoleDto? UserRole { get; set; }

        public UserBankAccount? BankAccount { get; set; }
    }
}
