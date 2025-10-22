using Application.User.Create;
using Application.User.Edit;
using Application.User.SetImage;
using Common.AspNetCore;
using Facade.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DangMedicalSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiController
    {
        private readonly IUserFacade _facade;

        public UserController(IUserFacade facade)
        {
            _facade = facade;
        }

        [HttpPost("CreateUser")]
        public async Task<ApiResult> CreateUser(CreateUserCommand command)
        {
            return CommandResult(await _facade.CreateUser(command));
        }
        
        [HttpPatch("EditUser")]
        public async Task<ApiResult> EditUser(EditUserCommand command)
        {
            return CommandResult(await _facade.EditUser(command));
        }
    
        [HttpPatch("SetImageUser")]
        public async Task<ApiResult> SetImageUser([FromForm] SetImageUserCommand command)
        {
            return CommandResult(await _facade.SetImageUser(command));
        }

    }
}
