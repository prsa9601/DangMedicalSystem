using Domain.UserAgg.Interfaces.Repository;
using Domain.UserAgg.Interfaces.Services;

namespace Application.User.Services
{
    internal class UserDomainService : IUserDomainService
    {
        private readonly IUserRepository _repository;

        public UserDomainService(IUserRepository repository)
        {
            _repository = repository;
        }

        public bool PhoneNumberIsExist(string phoneNumber)
        {
            return _repository.Exists(i => i.PhoneNumber.Equals(phoneNumber));
        }
    }
}
