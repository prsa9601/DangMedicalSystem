using Application.Contract.Answered;
using Application.Contract.Create;
using Common.AspNetCore;
using Facade.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.DTOs;

namespace DangMedicalSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ApiController
    {
        private readonly IContractFacade _facade;

        public ContractController(IContractFacade facade)
        {
            _facade = facade;
        }

        [HttpPost("AnsweredContract")]
        public async Task<ApiResult> AnsweredContract(ContractAnsweredCommand command) 
        {
            return CommandResult(await _facade.AnsweredContract(command));
        }

        [HttpPost("CreateContract")]
        public async Task<ApiResult> CreateContract(CreateContractCommand command) 
        {
            return CommandResult(await _facade.CreateContract(command));
        }

        [HttpGet("GetContractByFilter")]
        public async Task<ApiResult<ContractFilterResult>> GetContractByFilter([FromQuery] ContractFilterParam param) 
        {
            return QueryResult(await _facade.GetFilter(param));
        }
    }
}
