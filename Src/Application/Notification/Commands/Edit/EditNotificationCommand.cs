using Common.Application;
using Domain.NotificationAgg.Interfaces.Repository;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.Notification.Commands.Edit
{
    public class EditNotificationCommand : IBaseCommand
    {
        public Guid NotificationId { get; set; }
        public string Title { get; set; }
        public List<Guid> UserId { get; set; } = new List<Guid>();
        public string Description { get; set; }
        public string? Link { get; set; }
        public bool SendToAll { get; set; }
    }
    internal sealed class EditNotificationCommandHandle : IBaseCommandHandler<EditNotificationCommand>
    {
        private readonly INotificationRepository _repository;
        private readonly IUserRepository _userRepository;


        public EditNotificationCommandHandle(INotificationRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<OperationResult> Handle(EditNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await _repository.GetTracking(request.NotificationId);
            if (notification == null) return OperationResult.NotFound();

            if (request.SendToAll == true)
            {
                var users = await _userRepository.GetListTrackingAsync();
                notification.Edit(request.Title, users.Where(i => i.IsActive == true)
                    .Select(i => i.Id)
                    .ToList(), request.Description, request.Link);

                await _repository.SaveChangeAsync();
                return OperationResult.Success();
            }
            else
            {
                notification.Edit(request.Title, request.UserId, request.Description, request.Link);
                await _repository.SaveChangeAsync();
                return OperationResult.Success();
            }


        }
    }
}
