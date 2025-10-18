using Common.Application;
using Common.Application.SecurityUtil;
using Domain.UserAgg;
using Domain.UserAgg.Interfaces.Builder;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.Auth.Commands.Register
{
    public sealed class RegisterUserCommand : IBaseCommand
    {
        public string phoneNumber { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
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
            var user = _builder.WithPassword(password).WithPhoneNumber(request.phoneNumber).
                WithFirstName(request.firstName).WithLastName(request.lastName).Build();

            if (user is null)
                OperationResult.Error("خطایی سمت سرور رخ داده است لطفا بعدا تلاش کنید!");

            await _repository.AddAsync(user!);
            _repository.SaveChange();

            return OperationResult.Success();
        }
    }
}
