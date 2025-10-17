using Domain.UserAgg.Enum;

namespace Domain.UserAgg.Interfaces.Builder
{
    public interface IUserBuilder
    {
        public IUserBuilder WithFirstName(string firstName);
        public IUserBuilder WithPassword(string hashPassword);
        public IUserBuilder WithLastName(string lastName);
        public IUserBuilder WithPhoneNumber(string phoneNumber);
        public IUserBuilder WithNationalityCode(string nationalityCode);
        public IUserBuilder WithImageName(string imageName);
        public IUserBuilder WithBirthCertificatePhoto(string birthCertificatePhoto);
        public IUserBuilder WithNationalCardPhoto(string nationalCardPhoto);
        public IUserBuilder WithIsActive(bool IsActive);
        public IUserBuilder WithUserStatus(UserStatus userStatus);

        public User Build(); 
    }
}
