using Common.Application;
using Domain.NotificationAgg.Interfaces.Repository;

namespace Application.Notification.Commands.Edit
{
    public class EditNotificationCommand : IBaseCommand
    {
        public Guid NotificationId { get; set; }
        public string Title { get; set; }
        public List<Guid> UserId { get; set; } = new List<Guid>();
        public string Description { get; set; }
        public string Link { get; set; }
    }
    internal sealed class EditNotificationCommandHandle : IBaseCommandHandler<EditNotificationCommand>
    {
        private readonly INotificationRepository _repository;

        public EditNotificationCommandHandle(INotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(EditNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await _repository.GetTracking(request.NotificationId);
            if (notification == null) return OperationResult.NotFound();

            notification.Edit(request.Title, request.UserId, request.Description, request.Link);
            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
