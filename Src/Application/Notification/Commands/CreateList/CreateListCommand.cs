using Common.Application;
using Domain.NotificationAgg.Interfaces.Repository;

namespace Application.Notification.Commands.CreateList
{
    public class CreateListCommand : IBaseCommand
    {
        public string Title { get; set; }
        public List<Guid> UserIds { get; set; } = new List<Guid>();
        public string Description { get; set; }
        public string Link { get; set; }
    }
    internal sealed class CreateListCommandHandler : IBaseCommandHandler<CreateListCommand>
    {
        private readonly INotificationRepository _repository;

        public CreateListCommandHandler(INotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(CreateListCommand request, CancellationToken cancellationToken)
        {
            var notification = new Domain.NotificationAgg.Notification(request.Title,
                request.UserIds, request.Description, request.Link);

            await _repository.AddAsync(notification);

            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
