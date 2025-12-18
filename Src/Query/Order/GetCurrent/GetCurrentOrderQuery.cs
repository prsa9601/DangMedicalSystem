using Common.Query;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Order.DTOs;

namespace Query.Order.GetCurrent
{
    public record class GetCurrentOrderQuery(Guid userId) : IQuery<OrderDto?>;

    internal sealed class GetCurrentOrderQueryHandler : IQueryHandler<GetCurrentOrderQuery, OrderDto?>
    {
        private readonly Context _context;

        public GetCurrentOrderQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<OrderDto?> Handle(GetCurrentOrderQuery request, CancellationToken cancellationToken)
        {
            var userOrder = await _context.Orders.
                FirstOrDefaultAsync(i => i.UserId.Equals(request.userId) && i.status == Domain.OrderAgg.Enum.OrderStatus.AwaitingPayment);

            return userOrder.Map();
        }
    }
}
