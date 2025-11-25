using Application.Profit.Create;
using Common.AspNetCore;
using DangMedicalSystem.Api.Models.Profit;
using Facade.Profit;
using Microsoft.AspNetCore.Mvc;

namespace DangMedicalSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfitController : ApiController
    {
        private readonly IProfitFacade _facade;

        public ProfitController(IProfitFacade facade)
        {
            _facade = facade;
        }

        [HttpPost("Create")]
        public async Task<ApiResult> Create(CreateProfitViewModel command)
        {
            return CommandResult(await _facade.Create(new CreateProfitCommand
            {
                UserId = User.GetUserId(),
                Image = command.Image,
                OrderId = command.OrderId,
                ProductId = command.ProductId,
                Status = command.Status,
            }));
        }
    }
}
