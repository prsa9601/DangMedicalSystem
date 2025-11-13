using Domain.UserAgg.Enum;

namespace Domain.UserAgg.Interfaces.Builder
{
    public interface IUserBuilder
    {
        public IUserBuilder WithFirstName(string firstName);
        public IUserBuilder WithPassword(string hashPassword);
        public IUserBuilder WithLastName(string lastName);
        public IUserBuilder WithPhoneNumber(string phoneNumber);
        public IUserBuilder WithImageName(string imageName);
        public IUserBuilder WithIsActive(bool isActive);

        public User Build(); 
    }
}
