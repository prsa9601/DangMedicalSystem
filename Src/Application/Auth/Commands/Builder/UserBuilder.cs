using Domain.UserAgg;
using Domain.UserAgg.Enum;
using Domain.UserAgg.Interfaces.Builder;

namespace Application.Auth.Commands.Builder
{
    public class UserBuilder : IUserBuilder
    {
        private bool? _isActive = false;
        private string? _lastName;
        private string? _hashPassword;
        private string? _phoneNumber;
        private string? _imageName;
        private string? _firstName;

        public Domain.UserAgg.User Build()
        {
            var user = new Domain.UserAgg.User();

            if (_isActive == true) user.SetAsActive();

            if (_phoneNumber is not null) user.SetPhoneNumber(_phoneNumber);

            if (_firstName is not null) user.SetFirstName(_firstName);

            if (_lastName is not null) user.SetLastName(_lastName);

            if (_hashPassword is not null) user.SetPassword(_hashPassword);

            if (_imageName is not null) user.SetImageName(_imageName);


            return user;
        }

        public IUserBuilder WithFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        public IUserBuilder WithImageName(string imageName)
        {
            _imageName = imageName;
            return this;
        }

        public IUserBuilder WithIsActive(bool isActive)
        {
            _isActive = isActive;
            return this;
        }

        public IUserBuilder WithLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }

        public IUserBuilder WithPassword(string hashPassword)
        {
            _hashPassword = hashPassword;
            return this;
        }

        public IUserBuilder WithPhoneNumber(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
            return this;
        }

    }
}
