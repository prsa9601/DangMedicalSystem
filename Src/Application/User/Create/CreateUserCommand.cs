using Common.Application;
using Common.Application.SecurityUtil;
using Domain.UserAgg.Interfaces.Builder;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.User.Create
{
    public class CreateUserCommand :IBaseCommand
    {
        public required string phoneNumber { get; set; }
        public required string firstName { get; set; }
        public required string lastName{ get; set; }
        public required string password { get; set; }
    }
    internal sealed class CreateUserCommandHandler : IBaseCommandHandler<CreateUserCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IUserBuilder _builder;

        public CreateUserCommandHandler(IUserRepository repository, IUserBuilder builder)
        {
            _repository = repository;
            _builder = builder;
        }

        public async Task<OperationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            string password = Sha256Hasher.Hash(request.password);
            var user = _builder.WithPhoneNumber(request.phoneNumber).WithFirstName(request.firstName).WithLastName(request.lastName)
                .WithPassword(password).Build();

            await _repository.AddAsync(user);
            await _repository.SaveChangeAsync();

            return OperationResult.Success();
        }
    }
}
