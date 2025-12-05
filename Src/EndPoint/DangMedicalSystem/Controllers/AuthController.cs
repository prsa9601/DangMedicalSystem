using Application.Auth.Commands.GenerateAndSendOtpCode;
using Application.Auth.Commands.Login;
using Application.Auth.Commands.Logout;
using Application.Auth.Commands.Register;
using Application.Auth.Commands.VerificationOtpCode;
using Common.Application;
using Common.AspNetCore;
using DangMedicalSystem.Api.Infrastructure;
using Facade.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DangMedicalSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiController
    {
        private readonly IAuthFacade _facade;

        public AuthController(IAuthFacade facade)
        {
            _facade = facade;
        }

        
        [HttpPost("GenerateOtpCode")]
        public async Task<ApiResult<Dictionary<string, string>>> GenerateAndSendOtpCode(GenerateAndSendOtpCodeCommand command)
        {
            return CommandResult(await _facade.GenerateAndSendOtpCode(command))!;
        }

        [HttpPost("RegisterUser")]
        public async Task<ApiResult> RegisterUser(RegisterUserCommand command)
        {
            return CommandResult(await _facade.RegisterUser(command))!;
        }

        [HttpPost("LoginUser")]
        public async Task<ApiResult<LoginCommandResult>> LoginUser(UserLoginCommand command)
        {
            var result = await _facade.LoginUser(command);
          
            return CommandResult(result)!;
        }

        [HttpPost("VerificationOtpCode")]
        public async Task<ApiResult<bool>> VerificationOtpCode(VerificationOtpCodeCommand command)
        {
            return CommandResult(await _facade.VerificationOtpCode(command))!;
        }

        [Authorize]
        [HttpDelete("Logout")]
        public async Task<ApiResult> Logout(string refreshToken)
        {
            return CommandResult(await _facade.Logout(new LogoutUserCommand
            {
                RefreshToken = refreshToken,
                UserId = User.GetUserId(),
            }))!;
        }
    }
}
