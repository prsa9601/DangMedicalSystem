using Application.Auth.Commands.Builder;
using Common.Application;
using Common.Application.SecurityUtil;
using Domain.UserAgg.Interfaces.Builder;
using Domain.UserAgg.Interfaces.Repository;
using System.Reflection.Metadata.Ecma335;

namespace Application.User.Commands.Edit
{
    public class EditUserCommand : IBaseCommand
    {
        public Guid userId { get; set; }
        public required string phoneNumber { get; set; }
        public required string firstName { get; set; }
        public required string lastName { get; set; }
        public string? password { get; set; }
    }
    internal sealed class EditUserCommandHandler : IBaseCommandHandler<EditUserCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IUserBuilder _builder;

        public EditUserCommandHandler(IUserRepository repository, IUserBuilder builder)
        {
            _repository = repository;
            _builder = builder;
        }

        public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTracking(request.userId);
            if (user is null)
                return OperationResult.NotFound();

            if (request.password != null)
            {
                var password = Sha256Hasher.Hash(request.password);

                user.SetFirstName(request.firstName);
                user.SetPassword(password);
                user.SetPhoneNumber(request.phoneNumber);
                user.SetLastName(request.lastName);

            }
            else
            {
                user.SetFirstName(request.firstName);
                user.SetPhoneNumber(request.phoneNumber);
                user.SetLastName(request.lastName);
            }
            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
