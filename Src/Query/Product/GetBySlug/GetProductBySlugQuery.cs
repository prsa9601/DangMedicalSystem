using Common.Query;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Product.DTOs;

namespace Query.Product.GetBySlug
{
    public record class GetProductBySlugQuery(string slug) : IQuery<ProductDto?>;

    internal class GetProductBySlugQueryHandler : IQueryHandler<GetProductBySlugQuery, ProductDto?>
    {
        private readonly Context _context;

        public GetProductBySlugQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<ProductDto?> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(
                product => product.Slug.Equals(request.slug), cancellationToken);

            if (product == null) return null;

            return product.Map();
        }
    }
}
