using Common.Query;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Product.DTOs;

namespace Query.Product.GetById
{
    public record class GetProductByIdQuery(Guid productId) : IQuery<ProductDto?>;
    
    internal sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDto?>
    {
        private readonly Context _context;

        public GetProductByIdQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(
                product => product.Id.Equals(request.productId), cancellationToken);

            if (product == null) return null;

            return product.Map(_context);
        }
    }
}
