using Common.Application;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.User.Commands.ChangeActivityAccount
{
    public class ChangeActivityUserAccountCommand : IBaseCommand
    {
        public Guid userId { get; set; }
        public bool Activity { get; set; }
    }
    internal sealed class ChangeActivityUserAccountCommandHandler : IBaseCommandHandler<ChangeActivityUserAccountCommand>
    {
        private readonly IUserRepository _repository;

        public ChangeActivityUserAccountCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(ChangeActivityUserAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTracking(request.userId);
            if (user == null)
                return OperationResult.NotFound();

            user.ChangeActivity(request.Activity);
            return OperationResult.Success();
        }
    }
}
