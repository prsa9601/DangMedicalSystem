using Application.Auth.Commands.GenerateAndSendOtpCode;
using Application.Auth.Commands.Login;
using Application.Auth.Commands.Register;
using Application.Auth.Commands.VerificationOtpCode;
using Common.Application;
using MediatR;

namespace Facade.Auth
{
    public interface IAuthFacade
    {
        Task<OperationResult<Dictionary<string, string>>> GenerateAndSendOtpCode(GenerateAndSendOtpCodeCommand command); 
        Task<OperationResult> RegisterUser(RegisterUserCommand command); 
        Task<OperationResult<LoginCommandResult>> LoginUser(UserLoginCommand command); 
        Task<OperationResult<bool>> VerificationOtpCode(VerificationOtpCodeCommand command); 
    }
    internal class AuthFacade : IAuthFacade
    {
        private readonly IMediator _mediator;

        public AuthFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult<Dictionary<string, string>>> GenerateAndSendOtpCode(GenerateAndSendOtpCodeCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult<LoginCommandResult>> LoginUser(UserLoginCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> RegisterUser(RegisterUserCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult<bool>> VerificationOtpCode(VerificationOtpCodeCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
