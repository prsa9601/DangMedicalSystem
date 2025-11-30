using Application.Notification.Commands.Create;
using Application.Notification.Commands.CreateList;
using Application.Notification.Commands.Delete;
using Application.Notification.Commands.Edit;
using Common.Application;
using MediatR;
using Query.Notification.DTOs;
using Query.Notification.GetByIdForAdmin;
using Query.Notification.GetByIdForUser;
using Query.Notification.GetFilterForAdmin;
using Query.Notification.GetFilterForCurrentUser;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Facade.Notification
{
    public interface INotificationFacade
    {
        Task<OperationResult> Create(CreateNotificationCommand command);
        Task<OperationResult> CreateList(CreateListCommand command);
        Task<OperationResult> Edit(EditNotificationCommand command);
        Task<OperationResult> Delete(Guid id);
        Task<NotificationFilterResult> GetFilterForAdmin(NotificationFilterParam param);
        Task<NotificationFilterResultForUser> GetFilterForCurrentUser(NotificationFilterParamForUser param);
        Task<NotificationDtoForUser?> GetById(Guid id, Guid userId);
        Task<NotificationDto?> GetByIdForAdmin(Guid id);
    }
    internal class NotificationFacade : INotificationFacade
    {
        private readonly IMediator _mediator;

        public NotificationFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> Create(CreateNotificationCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> CreateList(CreateListCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Delete(Guid id)
        {
            return await _mediator.Send(new DeleteNotificationCommand
            {
                NotificationId = id
            });
        }

        public async Task<OperationResult> Edit(EditNotificationCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<NotificationDtoForUser?> GetById(Guid id, Guid userId)
        {
            return await _mediator.Send(new GetNotificationByIdQuery(id, userId));
        }

        public async Task<NotificationDto?> GetByIdForAdmin(Guid id)
        {
            return await _mediator.Send(new GetNotificationForAdminQuery(id));
        }

        public async Task<NotificationFilterResult> GetFilterForAdmin(NotificationFilterParam param)
        {
            return await _mediator.Send(new GetFilterNotificationForAdminQuery(new NotificationFilterParam
            {
                PageId = param.PageId,
                Take = param.Take,
                Description = param.Description,
                Title = param.Title,
            }));
        }

        public async Task<NotificationFilterResultForUser> GetFilterForCurrentUser(NotificationFilterParamForUser param)
        {
            return await _mediator.Send(new GetFilterNotificationCurrentUserQuery(param));
        }
    }
}
