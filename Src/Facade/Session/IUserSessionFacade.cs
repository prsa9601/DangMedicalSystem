using Application.Sessions.Commands.AddSession;
using Common.Application;
using MediatR;

namespace Facade.Session
{
    public interface IUserSessionFacade
    {
        Task<OperationResult> AddSession(AddSessionsCommand command);
    }
    public class UserSessionFacade : IUserSessionFacade
    {
        private readonly IMediator _mediator;

        public UserSessionFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<OperationResult> AddSession(AddSessionsCommand command)
        {
            return _mediator.Send(command);
        }
    }
}
