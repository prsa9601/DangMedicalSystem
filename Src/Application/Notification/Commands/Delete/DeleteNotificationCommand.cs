using Common.Application;
using Domain.NotificationAgg.Interfaces.Repository;

namespace Application.Notification.Commands.Delete
{
    public class DeleteNotificationCommand : IBaseCommand
    {
        public Guid NotificationId { get; set; }
    }
    internal sealed class DeleteNotificationCommandHandler : IBaseCommandHandler<DeleteNotificationCommand>
    {
        private readonly INotificationRepository _repository;

        public DeleteNotificationCommandHandler(INotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await _repository.GetTracking(request.NotificationId);
            if (notification == null) return OperationResult.NotFound();

            await _repository.DeleteAsync(notification);
            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
