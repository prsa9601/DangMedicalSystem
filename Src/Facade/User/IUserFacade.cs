using Application.User.Create;
using Application.User.Edit;
using Application.User.SetImage;
using Common.Application;
using MediatR;
using Query.User.DTOs;
using Query.User.DTOs.FilterDto;
using Query.User.GetById;
using Query.User.GetFilter;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Facade.User
{
    public interface IUserFacade
    {
        Task<OperationResult> CreateUser(CreateUserCommand command);
        Task<OperationResult> EditUser(EditUserCommand command);
        Task<OperationResult> SetImageUser(SetImageUserCommand command);


        Task<UserDto?> GetById(Guid userId);
        Task<UserFilterResult> GetByFilter(UserFilterParam param);
    }
    internal class UserFacade : IUserFacade
    {
        private readonly IMediator _mediator;

        public UserFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> CreateUser(CreateUserCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> EditUser(EditUserCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<UserFilterResult> GetByFilter(UserFilterParam param)
        {
            return await _mediator.Send(new GetUsersByFilterQuery(param));
        }

        public async Task<UserDto?> GetById(Guid userId)
        {
            return await _mediator.Send(new GetUserByIdQuery(userId));
        }

        public async Task<OperationResult> SetImageUser(SetImageUserCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
