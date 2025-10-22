using Application.Auth.Commands.GenerateAndSendOtpCode;
using Application.Auth.Commands.Login;
using Application.Auth.Commands.Register;
using Application.Auth.Commands.VerificationOtpCode;
using Common.AspNetCore;
using Facade.Auth;
using Microsoft.AspNetCore.Http;
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
        public async Task<ApiResult<string>> GenerateAndSendOtpCode(GenerateAndSendOtpCodeCommand command)
        {
            return CommandResult(await _facade.GenerateAndSendOtpCode(command))!;
        }

        [HttpPost("RegisterUser")]
        public async Task<ApiResult> RegisterUser(RegisterUserCommand command)
        {
            return CommandResult(await _facade.RegisterUser(command))!;
        }
 
        [HttpPost("LoginUser")]
        public async Task<ApiResult<string>> LoginUser(UserLoginCommand command)
        {
            return CommandResult(await _facade.LoginUser(command))!;
        }

        [HttpPost("VerificationOtpCode")]
        public async Task<ApiResult<bool>> VerificationOtpCode(VerificationOtpCodeCommand command)
        {
            return CommandResult(await _facade.VerificationOtpCode(command))!;
        }
    }
}
