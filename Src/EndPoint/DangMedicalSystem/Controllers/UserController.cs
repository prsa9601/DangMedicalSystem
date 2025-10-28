using Application.User.Commands.CompletionOfInformation;
using Application.User.Commands.ConfirmedAccount;
using Application.User.Commands.Create;
using Application.User.Commands.Edit;
using Application.User.Commands.SetImage;
using Common.AspNetCore;
using Facade.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Query.User.DTOs;
using Query.User.DTOs.FilterDto;
using Query.User.DTOs.FilterDto.FilterExpression;

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
        
        [HttpPost("CreateUserForAdmin")]
        public async Task<ApiResult<Guid>> CreateUserForAdmin(CreateUserForAdminCommand command)
        {
            return CommandResult(await _facade.CreateUserForAdmin(command));
        }
        
        [HttpPost("CompletionOfInformation")]
        public async Task<ApiResult> CompletionOfInformationUser(CompletionOfInformationCommand command)
        {
            return CommandResult(await _facade.CompletionOfInformation(command));
        }
        
        [HttpPost("ConfirmedAccount")]
        public async Task<ApiResult> ConfirmedAccount(ConfirmedAccountUserCommand command)
        {
            return CommandResult(await _facade.ConfirmedAccount(command));
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
     
        [HttpGet("GetUserById")]
        public async Task<ApiResult<UserDto?>> GetUserById(Guid userId)
        {
            return QueryResult(await _facade.GetById(userId));
        }
    
        [HttpGet("GetUserByFilter")]
        public async Task<ApiResult<UserFilterResult>> GetUserByFilter([FromQuery] UserFilterParam param)
        {
            return QueryResult(await _facade.GetByFilter(param));
        }
    
        //[HttpGet("GetUserByExpressionFilter")]
        //public async Task<ApiResult<UserExpressionFilterResult>> GetUserByExpressionFilter(
        //    [FromQuery] UserExpressionFilterParam param)
        //{
        //    return QueryResult(await _facade.GetByExpressionFilter(param));
        //}

    }
}
