using Application.User.Create;
using Application.User.Edit;
using Application.User.SetImage;
using Common.Application;
using MediatR;

namespace Facade.User
{
    public interface IUserFacade
    {
        Task<OperationResult> CreateUser(CreateUserCommand command);
        Task<OperationResult> EditUser(EditUserCommand command);
        Task<OperationResult> SetImageUser(SetImageUserCommand command);
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

        public async Task<OperationResult> SetImageUser(SetImageUserCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
