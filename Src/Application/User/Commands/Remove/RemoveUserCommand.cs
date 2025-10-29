using Common.Application;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.User.Commands.Remove
{
    public class RemoveUserCommand : IBaseCommand<string>
    {
        public Guid UserId { get; set; }
    }
    internal class RemoveUserCommandHandler : IBaseCommandHandler<RemoveUserCommand, string>
    {
        private readonly IUserRepository _repository;

        public RemoveUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<string>> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTracking(request.UserId);
            if (user is null)
                return OperationResult<string>.NotFound();

            var result = await _repository.DeleteAsync(user);
            await _repository.SaveChangeAsync();
            return OperationResult<string>.Success($"{user.FirstName} {user.LastName}");
        }
    }
}
