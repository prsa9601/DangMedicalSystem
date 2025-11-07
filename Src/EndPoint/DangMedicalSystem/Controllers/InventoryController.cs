using Application.Product.Commands.AddInventory;
using Application.Product.Commands.EditInventory;
using Application.Product.Commands.SetProfitableTime;
using Common.AspNetCore;
using Facade.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DangMedicalSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ApiController
    {
        private readonly IInventoryFacade _facade;

        public InventoryController(IInventoryFacade facade)
        {
            _facade = facade;
        }

        [HttpPost("Create")]
        public async Task<ApiResult> Create(AddInventoryCommand command)
        {
            return CommandResult(await _facade.Create(command));
        }
        
        [HttpPatch("Edit")]
        public async Task<ApiResult> Edit(EditInventoryCommand command)
        {
            return CommandResult(await _facade.Edit(command));
        }
        
        [HttpPatch("SetProfitable")]
        public async Task<ApiResult> SetProfitable(SetInventoryProfitableCommand command)
        {
            return CommandResult(await _facade.SetProfitableTime(command));
        }
    }
}
