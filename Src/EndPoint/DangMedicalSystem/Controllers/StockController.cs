using Application.Stock.Create;
using Common.AspNetCore;
using DangMedicalSystem.Api.Models.Stock;
using Facade.Stock;
using Microsoft.AspNetCore.Mvc;

namespace DangMedicalSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ApiController
    {
        private readonly IStockFacade _facade;

        public StockController(IStockFacade facade)
        {
            _facade = facade;
        }

        [HttpPost("Create")]
        public async Task<ApiResult> Create(CreateStockViewModel command)
        {
            return CommandResult(await _facade.Create(new CreateStockCommand
            {
                UserId = User.GetUserId(),
                PurchaseId = command.PurchaseId,
            }));
        }
    }
}
