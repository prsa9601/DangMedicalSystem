using Application.Contact.Answered;
using Application.Contact.Create;
using Common.AspNetCore;
using Facade.Contact;
using Microsoft.AspNetCore.Mvc;
using Query.Contact.DTOs;

namespace DangMedicalSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ApiController
    {
        private readonly IContactFacade _facade;

        public ContactController(IContactFacade facade)
        {
            _facade = facade;
        }

        [HttpPost("AnsweredContact")]
        public async Task<ApiResult> AnsweredContact(ContactAnsweredCommand command) 
        {
            return CommandResult(await _facade.AnsweredContact(command));
        }

        [HttpPost("CreateContact")]
        public async Task<ApiResult> CreateContact(CreateContactCommand command) 
        {
            return CommandResult(await _facade.CreateContact(command));
        }

        [HttpGet("GetContactByFilter")]
        public async Task<ApiResult<ContactFilterResult>> GetContactByFilter([FromQuery] ContactFilterParam param) 
        {
            return QueryResult(await _facade.GetFilter(param));
        }
    }
}
