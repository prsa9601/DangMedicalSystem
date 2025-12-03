using Application.Order.CreateOrder;
using Application.Order.EditOrder;
using Application.Order.IsFinally;
using Application.Order.SetOrderItem;
using Common.Application;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Query.Order.DTOs;
using Query.Order.GetById;
using Query.Order.GetFilter;
using Query.Order.GetForReport;
using Query.SiteEntity.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Facade.Order
{
    public interface IOrderFacade
    {
        Task<OperationResult> Create(CreateOrderCommand command, CancellationToken cancellationToken);
        Task<OperationResult> Edit(EditOrderCommand command, CancellationToken cancellationToken);
        Task<OperationResult> SetOrderItem(SetOrderItemCommand command, CancellationToken cancellationToken);
        Task<OperationResult> IsFinally(OrderIsFinallyCommand command, CancellationToken cancellationToken);


        Task<OrderDto?> GetById(Guid orderId, CancellationToken cancellationToken);
        Task<OrderFilterResult> GetFilter(OrderFilterParam param, CancellationToken cancellationToken);
        Task<OrderFilterResult> GetForReport(OrderFilterParam param, CancellationToken cancellationToken);
    }
    internal class OrderFacade : IOrderFacade
    {
        private readonly IMemoryCache _cache;
        private readonly IMediator _mediator;

        public OrderFacade(IMediator mediator, IMemoryCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }

        public async Task<OperationResult> Create(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        public async Task<OperationResult> Edit(EditOrderCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        public async Task<OrderDto?> GetById(Guid orderId, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetOrderById(orderId), cancellationToken);
        }

        public async Task<OrderFilterResult> GetFilter(OrderFilterParam param, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetOrderFilterQuery(param), cancellationToken);
        }

        public async Task<OrderFilterResult> GetForReport(OrderFilterParam param, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetOrderForReportQuery(param), cancellationToken);
        }

        public async Task<OperationResult> IsFinally(OrderIsFinallyCommand command, CancellationToken cancellationToken)
        {
            if (_cache.TryGetValue("MainPageContent", out MainPageDto? cachedResult))
            {
                _cache.Remove("MainPageContent");
            }
            return await _mediator.Send(command, cancellationToken);
        }

        public async Task<OperationResult> SetOrderItem(SetOrderItemCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }
    }
}
