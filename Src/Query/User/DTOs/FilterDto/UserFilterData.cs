using Common.Query;
using Domain.UserAgg;
using Domain.UserAgg.Enum;

namespace Query.User.DTOs.FilterDto
{
    public class UserFilterData : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageName { get; set; }
        public bool IsActive { get; set; } //کاربر اکتیوه و میتونه کار کنه

        public UserRoleDto? UserRole { get; set; }
        public UserDocumentDto? UserDocument { get; set; }

        public UserBankAccountDto? BankAccount { get; set; }

        public List<UserOtpDto>? UserOtps { get; set; }
        public List<UserBlockDto>? UserBlocks { get; set; }
        public List<UserAttemptDto>? UserAttempts { get; set; }
        public List<UserSessionDto>? UserSessions { get; set; }

    }

    public class UserFilterForDocumentsData : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalityCode { get; set; }
        public string ImageName { get; set; }
        public string NationalCardPhoto { get; set; }
        public string BirthCertificatePhoto { get; set; }
        public UserDocumentStatus Status { get; set; }
        public bool IsActive { get; set; } //کاربر اکتیوه و میتونه کار کنه

        public UserRoleDto? UserRole { get; set; }

        public UserBankAccountDto? BankAccount { get; set; }
        public UserDocumentDto? UserDocument { get; set; }

        public List<UserOtpDto>? UserOtps { get; set; }
        public List<UserBlockDto>? UserBlocks { get; set; }
        public List<UserAttemptDto>? UserAttempts { get; set; }
        public List<UserSessionDto>? UserSessions { get; set; }

    }
}
