using Common.Domain;
using Common.Query;
using Domain.OrderAgg;
using Domain.OrderAgg.Enum;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Order.DTOs;
using System.Reflection.Metadata.Ecma335;

namespace Query.Order.GetById
{
    public record class GetOrderById(Guid orderId) : IQuery<OrderDto?>;

    internal sealed class GetOrderByIdHandler : IQueryHandler<GetOrderById, OrderDto?>
    {
        private readonly Context _context;

        public GetOrderByIdHandler(Context context)
        {
            _context = context;
        }

        public async Task<OrderDto?> Handle(GetOrderById request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(order => order.Id.Equals(request.orderId), cancellationToken);
            if (order == null) 
                return null;

            return order.Map();
        }
    }
}