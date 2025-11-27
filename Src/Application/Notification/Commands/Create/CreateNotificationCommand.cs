using Common.Application;
using Domain.NotificationAgg.Interfaces.Repository;

namespace Application.Notification.Commands.Create
{
    public class CreateNotificationCommand : IBaseCommand
    {
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
    internal sealed class CreateNotificationCommandHandler : IBaseCommandHandler<CreateNotificationCommand>
    {
        private readonly INotificationRepository _repository;

        public CreateNotificationCommandHandler(INotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = new Domain.NotificationAgg.Notification(request.Title, 
                request.UserId, request.Description, request.Link);

            await _repository.AddAsync(notification);
            
            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
