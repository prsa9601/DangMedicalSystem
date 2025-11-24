using Common.Application;
using Domain.UserAgg.Enum;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.User.Commands.ChangeAccountStatus
{
    public class ChangeUserDocumentStatusCommand : IBaseCommand
    {
        public Guid UserId { get; set; }
        public UserDocumentStatus Status { get; set; }
    }
    internal sealed class ChangeUserDocumentStatusCommandHandler : IBaseCommandHandler<ChangeUserDocumentStatusCommand>
    {
        private readonly IUserRepository _repository;

        public ChangeUserDocumentStatusCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(ChangeUserDocumentStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTracking(request.UserId);
            if (user == null) return OperationResult.NotFound();

            user.SetDocumentStatus(request.Status);
            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
