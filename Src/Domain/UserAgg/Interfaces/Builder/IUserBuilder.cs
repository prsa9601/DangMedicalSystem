namespace Domain.UserAgg.Interfaces.Builder
{
    public interface IUserBuilder
    {
        public IUserBuilder WithUserName(string userName);
        public IUserBuilder WithFirstName(string firstName);
        public IUserBuilder WithPassword(string password);
        public IUserBuilder WithLastName(string lastName);
        public IUserBuilder WithPhoneNumber(string phoneNumber);
        public IUserBuilder WithAge(string age);
        public IUserBuilder WithNationalityCode(string nationalityCode);
        public IUserBuilder WithImageName(string imageName);
        public IUserBuilder WithBirthCertificatePhoto(string birthCertificatePhoto);
        public IUserBuilder WithNationalCardPhoto(string nationalCardPhoto);

        public User Build(); 
    }
}
