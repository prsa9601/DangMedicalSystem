using Application.Auth.Shared.Utilities;
using Common.Application;
using Common.Application.SecurityUtil;
using Domain.UserAgg;
using Domain.UserAgg.Events;
using Domain.UserAgg.Interfaces.Builder;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.Auth.Commands.Register
{
    public sealed class RegisterUserCommand : IBaseCommand
    {
        public RegisterUserCommand(string phoneNumber, string password, string firstName, string lastName, string ipAddress)
        {
            this.phoneNumber = phoneNumber.EnsureLeadingZero();
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.ipAddress = ipAddress;
        }

        public string phoneNumber { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string ipAddress { get; set; }
    }
    public sealed class RegisterUserCommandHandler : IBaseCommandHandler<RegisterUserCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IUserBuilder _builder;

        public RegisterUserCommandHandler(IUserRepository repository, IUserBuilder builder)
        {
            _repository = repository;
            _builder = builder;
        }

        public async Task<OperationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var password = Sha256Hasher.Hash(request.password);
            var user = await _repository.GetByFilterAsync
                (i=>i.PhoneNumber.Equals(request.phoneNumber));

            user.SetFirstName(request.firstName);
            user.SetLastName(request.lastName);
            user.SetPassword(password);
            user.SetPhoneNumber(request.phoneNumber);

            if (CheckSessionForOtpCode(user, request.ipAddress) == false)
            {
                return OperationResult.Error("قبل از ثبت نام باید شماره تماستون رو فعال کنید.");
            }

            if (user is null)
                OperationResult.Error("خطایی سمت سرور رخ داده است لطفا بعدا تلاش کنید!");

            user.SetUserSession(Sha256Hasher.Hash(password), request.ipAddress, DateTime.Now.AddMinutes(3));
            
            user.ChangeActivity(true);
            //await _repository.AddAsync(user!);

            user.AddDomainEvent(new CreateOrderEvent() { UserId = user.Id});

            _repository.SaveChange();

            return OperationResult.Success("ثبت نام با موفقیت انجام شد.");
        }

        private bool CheckSessionForOtpCode(Domain.UserAgg.User user, string ipAddress)
        {
            if (user == null)
                return false;

            var session = user.UserSessions.FirstOrDefault(i => i.ExpireDate > DateTime.Now && i.UserId.Equals(user.Id)
            && i.IpAddress == ipAddress && i.IsActive == true && ((i.ExpireDate.Minute - i.CreationDate.Minute) == 3));

            if (session is null)
                return false;

            session.ChangeActivity(false);
            return true;
        }
    }
}