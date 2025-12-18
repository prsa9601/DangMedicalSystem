using Common.Query;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Product.DTOs.FilterDto;

namespace Query.Product.GetFilterForIndexPage
{
    public class GetFilterForIndexPageQuery : QueryFilter<ProductFilterForIndexPageResult, ProductFilterParam>
    {
        public GetFilterForIndexPageQuery(ProductFilterParam filterParams) : base(filterParams)
        {
        }
    }
    public sealed class GetFilterForIndexPageQueryHandler : IQueryHandler<GetFilterForIndexPageQuery, ProductFilterForIndexPageResult>
    {
        private readonly Context _context;

        public GetFilterForIndexPageQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<ProductFilterForIndexPageResult> Handle(GetFilterForIndexPageQuery request, CancellationToken cancellationToken)
        {
            var @param = request.FilterParams;
            var result = _context.Products.Where(i => i.SeoData.IndexPage == true && i.Inventory != null).OrderByDescending(i => i.CreationDate).AsQueryable();

            if (result == null) return null;
            if (!string.IsNullOrWhiteSpace(@param.Title))
                result = result.Where(r => r.Title.Contains(@param.Title));

            if (@param.Status != null)
                result = result.Where(r => r.Status == @param.Status);



            var skip = (@param.PageId - 1) * @param.Take;
            var model = new ProductFilterForIndexPageResult()
            {
                Data = await result.Skip(skip).Take(@param.Take)
                    .Select(product => product.MapProductFilterForIndexPageData(_context)).ToListAsync(cancellationToken),
                FilterParams = @param
            };

            model.GeneratePaging(result, @param.Take, @param.PageId);
            return model;
        }
    }
}
