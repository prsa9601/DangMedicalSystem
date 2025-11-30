using Common.Application;
using Domain.NotificationAgg.Interfaces.Repository;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.Notification.Commands.CreateList
{
    public class CreateListCommand : IBaseCommand
    {
        public bool SendToAll { get; set; }
        public string Title { get; set; }
        public List<Guid> UserIds { get; set; } = new List<Guid>();
        public string Description { get; set; }
        public string Link { get; set; }
    }
    internal sealed class CreateListCommandHandler : IBaseCommandHandler<CreateListCommand>
    {
        private readonly INotificationRepository _repository;
        private readonly IUserRepository _userRepository;

        public CreateListCommandHandler(INotificationRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<OperationResult> Handle(CreateListCommand request, CancellationToken cancellationToken)
        {
            if (request.SendToAll == true)
            {
                var users = await _userRepository.GetListTrackingAsync();
                var notification = new Domain.NotificationAgg.Notification(
                    request.Title,
                    users.Where(i => i.IsActive == true)
                    .Select(i => i.Id)
                    .ToList(),
                    request.Description, 
                    request.Link);

                await _repository.AddAsync(notification);

                await _repository.SaveChangeAsync();
                return OperationResult.Success();
            }
            else
            {
                var notification = new Domain.NotificationAgg.Notification(request.Title,
                    request.UserIds, request.Description, request.Link);

                await _repository.AddAsync(notification);

                await _repository.SaveChangeAsync();
                return OperationResult.Success();
            }
        }
    }
}
