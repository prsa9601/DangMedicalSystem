using Application.Order.CreateOrder;
using Application.Order.IsFinally;
using Application.Order.SetOrderItem;
using Common.AspNetCore;
using DangMedicalSystem.Api.Models.Order;
using Facade.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Order.DTOs;

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
        public async Task<ApiResult> IsFinally(OrderIsFinallyViewModel command, CancellationToken cancellationToken)
        {
            return CommandResult(await _facade.IsFinally(new OrderIsFinallyCommand
            {
                orderId = command.orderId,
                userId = User.GetUserId(),
            }, cancellationToken));
        }

        [Authorize]
        [HttpPatch("SetOrderItem")]
        public async Task<ApiResult> SetOrderItem(SetOrderItemCommandViewModel command, CancellationToken cancellationToken)
        {
            return CommandResult(await _facade.SetOrderItem(new SetOrderItemCommand
            {
                userId = User.GetUserId(),
                dongAmount = command.dongAmount,
                //orderId = command.orderId,
                productId = command.productId,
            }, cancellationToken));
        }
        
        [HttpGet("GetOrderById")]
        public async Task<ApiResult<OrderDto?>> GetById(Guid orderId, CancellationToken cancellationToken)
        {
            return QueryResult(await _facade.GetById(orderId, cancellationToken));
        }
        
        
        //[HttpGet("GetCurrentUser")]
        //public async Task<ApiResult<OrderDto?>> GetCurrentUser()
        //{
        //    return QueryResult(await _facade.get(orderId, cancellationToken));
        //}
        
        [HttpGet("GetOrdersByFilter")]
        public async Task<ApiResult<OrderFilterResult>> GetFilter([FromQuery] OrderFilterParam param, CancellationToken cancellationToken)
        {
            return QueryResult(await _facade.GetFilter(param, cancellationToken));
        }
      
        [HttpGet("GetOrdersForReport")]
        public async Task<ApiResult<OrderFilterResult>> GetForReport([FromQuery] OrderFilterParam param, CancellationToken cancellationToken)
        {
            return QueryResult(await _facade.GetForReport(param, cancellationToken));
        }
    }
}
