using Domain.UserAgg;
using Domain.UserAgg.Enum;
using Domain.UserAgg.Interfaces.Builder;

namespace Application.Auth.Commands.Builder
{
    public class UserBuilder : IUserBuilder
    {
        private string? _birthCertificatePhoto;
        private bool? _isActive = false;
        private string? _lastName;
        private string? _nationalCardPhoto;
        private string? _nationalityCode;
        private string? _hashPassword;
        private string? _phoneNumber;
        private string? _imageName;
        private string? _firstName;
        private UserStatus _userStatus = UserStatus.NotConfirmed;

        public User Build()
        {
            var user = new User();

            if (_birthCertificatePhoto is not null) user.SetBirthCertificatePhoto(_birthCertificatePhoto);

            if (_isActive == true) user.SetAsActive();

            if (_phoneNumber is not null) user.SetPhoneNumber(_phoneNumber);

            if (_firstName is not null) user.SetFirstName(_firstName);

            if (_lastName is not null) user.SetLastName(_lastName);

            if (_hashPassword is not null) user.SetPassword(_hashPassword);

            if (_imageName is not null) user.SetImageName(_imageName);

            if (_nationalCardPhoto is not null) user.SetNationalCardPhoto(_nationalCardPhoto);

            if (_nationalityCode is not null) user.SetNationalityCode(_nationalityCode);

            user.SetUserStatus(_userStatus);

            return user;
        }

        public IUserBuilder WithBirthCertificatePhoto(string birthCertificatePhoto)
        {
            _birthCertificatePhoto = birthCertificatePhoto;
            return this;
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

        public IUserBuilder WithNationalCardPhoto(string nationalCardPhoto)
        {
            _nationalCardPhoto = nationalCardPhoto;
            return this;
        }

        public IUserBuilder WithNationalityCode(string nationalityCode)
        {
            _nationalityCode = nationalityCode;
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

        public IUserBuilder WithUserStatus(UserStatus userStatus)
        {
            _userStatus = userStatus;
            return this;
        }
    }
}
