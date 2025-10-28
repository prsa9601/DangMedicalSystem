using Common.Query;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Product.DTOs.FilterDto;
using Query.User.DTOs.FilterDto;
using Query.User.Map;

namespace Query.Product.GetFilter
{
    public class GetProductFilterQuery : QueryFilter<ProductFilterResult, ProductFilterParam>
    {
        public GetProductFilterQuery(ProductFilterParam filterParams) : base(filterParams)
        {
        }
    }
    internal sealed class GetProductFilterQueryHandler : IQueryHandler<GetProductFilterQuery, ProductFilterResult>
    {
        private readonly Context _context;

        public GetProductFilterQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<ProductFilterResult> Handle(GetProductFilterQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;
            var result = _context.Products.OrderByDescending(d => d.Id).AsQueryable();

            if (result == null) return null;
            //if (!string.IsNullOrWhiteSpace(@params.UserIds))
            //    result = result.Where(r => r.Email.Contains(@params.Email));

            if (!string.IsNullOrWhiteSpace(@params.Title))
                result = result.Where(r => r.Title.Contains(@params.Title));

            if (@params.Status != null)
                result = result.Where(r => r.Status == @params.Status);

            var skip = (@params.PageId - 1) * @params.Take;
            var model = new ProductFilterResult()
            {
                Data = await result.Skip(skip).Take(@params.Take)
                    .Select(product => product.MapProductFilterData()).ToListAsync(cancellationToken),
                FilterParams = @params
            };

            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }
    }
}
