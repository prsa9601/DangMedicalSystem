using Application.Notification.Commands.Create;
using Application.Notification.Commands.CreateList;
using Application.Notification.Commands.Delete;
using Application.Notification.Commands.Edit;
using Common.AspNetCore;
using Domain.UserAgg;
using Facade.Notification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Query.Notification.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DangMedicalSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ApiController
    {
        private readonly INotificationFacade _facade;

        public NotificationController(INotificationFacade facade)
        {
            _facade = facade;
        }

        [HttpPost("CreateNotification")]
        public async Task<ApiResult> CreateNotification(CreateNotificationCommand command)
        {
            return CommandResult(await _facade.Create(command));
        }

        [HttpPost("CreateListNotification")]
        public async Task<ApiResult> CreateListNotification(CreateListCommand command)
        {
            return CommandResult(await _facade.CreateList(command));
        }

        [HttpPost("EditNotification")]
        public async Task<ApiResult> EditNotification(EditNotificationCommand command)
        {
            return CommandResult(await _facade.Edit(command));
        }

        [HttpPost("DeleteNotification")]
        public async Task<ApiResult> DeleteNotification(Guid id)
        {
            return CommandResult(await _facade.Delete(id));
        }

        [HttpGet("GetFilterForAdmin")]
        public async Task<ApiResult<NotificationFilterResult>> GetFilterForAdmin(NotificationFilterParam param)
        {
            return QueryResult(await _facade.GetFilterForAdmin(param));
        }

        [HttpGet("GetFilterForCurrentUser")]
        public async Task<ApiResult<NotificationFilterResultForUser>> GetFilterForCurrentUser(NotificationFilterParam param)
        {
            return QueryResult(await _facade.GetFilterForCurrentUser(new NotificationFilterParamForUser
            {
                UserId = User.GetUserId(),
                Description = param.Description,
                PageId = param.PageId,
                Take = param.Take,
                Title = param.Title,
            }));
        }

        [HttpGet("GetById")]
        public async Task<ApiResult<NotificationDtoForUser?>> GetById(Guid id, Guid userId)
        {
            return QueryResult(await _facade.GetById(id, userId));
        }

        [HttpGet("GetByIdForAdmin")]
        public async Task<ApiResult<NotificationDto?>> GetByIdForAdmin(Guid id)
        {
            return QueryResult(await _facade.GetByIdForAdmin(id));
        }
    }
}
