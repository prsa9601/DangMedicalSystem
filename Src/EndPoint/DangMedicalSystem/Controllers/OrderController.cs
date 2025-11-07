using Application.Order.CreateOrder;
using Application.Order.IsFinally;
using Common.AspNetCore;
using Facade.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Query.Order.DTOs;
using System.Threading.Tasks;

namespace DangMedicalSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ApiController
    {
        private readonly IOrderFacade _facade;

        public OrderController(IOrderFacade facade)
        {
            _facade = facade;
        }

        [HttpPost("Create")]
        public async Task<ApiResult> Create(CancellationToken cancellationToken)
        {
            return CommandResult(await _facade.Create(new CreateOrderCommand { userId = User.GetUserId() }, cancellationToken));
        }
        
        [HttpPost("IsFinally")]
        public async Task<ApiResult> IsFinally(OrderIsFinallyCommand command, CancellationToken cancellationToken)
        {
            return CommandResult(await _facade.IsFinally(command, cancellationToken));
        }
        
        [HttpGet("GetById")]
        public async Task<ApiResult<OrderDto?>> GetById(Guid orderId, CancellationToken cancellationToken)
        {
            return QueryResult(await _facade.GetById(orderId, cancellationToken));
        }
        
        [HttpGet("GetFilter")]
        public async Task<ApiResult<OrderFilterResult>> GetFilter([FromQuery] OrderFilterParam param, CancellationToken cancellationToken)
        {
            return QueryResult(await _facade.GetFilter(param, cancellationToken));
        }
      
        [HttpGet("GetForReport")]
        public async Task<ApiResult<OrderFilterResult>> GetForReport([FromQuery] OrderFilterParam param, CancellationToken cancellationToken)
        {
            return QueryResult(await _facade.GetForReport(param, cancellationToken));
        }
    }
}
