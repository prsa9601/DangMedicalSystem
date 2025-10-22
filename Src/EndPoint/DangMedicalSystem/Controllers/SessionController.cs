using Application.Auth.Commands.GenerateAndSendOtpCode;
using Application.Sessions.Commands.AddSession;
using Common.AspNetCore;
using Facade.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DangMedicalSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ApiController
    {
        private readonly IUserSessionFacade _facade;

        public SessionController(IUserSessionFacade facade)
        {
            _facade = facade;
        }

        [HttpPost("AddSession")]
        public async Task<ApiResult> AddSession(AddSessionsCommand command)
        {
            return CommandResult(await _facade.AddSession(command))!;
        }
    }
}
