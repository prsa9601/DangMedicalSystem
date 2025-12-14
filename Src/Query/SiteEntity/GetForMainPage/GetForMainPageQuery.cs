using Common.Query;
using Domain.ProductAgg;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Product;
using Query.SiteEntity.DTOs;

namespace Query.SiteEntity.GetForMainPage
{
    public class GetForMainPageQuery : IQuery<MainPageDto?>
    {
    }
    public class GetForMainPageQueryHandler : IQueryHandler<GetForMainPageQuery, MainPageDto?>
    {
        private readonly Context _context;

        public GetForMainPageQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<MainPageDto?> Handle(GetForMainPageQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products.Where(i => i.SeoData.IndexPage == true
            && i.Status != Domain.ProductAgg.Enum.ProductStatus.NotActive).ToListAsync();
            if (products == null) return null;

            var investmentCount = _context.Orders
                 .Where(i => i.status == Domain.OrderAgg.Enum.OrderStatus.paid)
                 .Select(i => i.UserId)   // فقط شناسه کاربرها
                 .Distinct()              // حذف کاربران تکراری
                 .Count();                // شمارش تعداد یکتا

            var orders = _context.Orders.Where(i => products
            .Select(i => i.Id)
            .Contains(i.OrderItems.ProductId));

            var result = new List<ProductMainPageQuery>();
            for (int i = 0; i < products.Count(); i++)
            {
                var product = products.Skip(i).FirstOrDefault();
                var order = orders.Where(i => i.OrderItems.ProductId
                                .Equals(product.Id)).ToList();
                decimal totalPurchaseDang = order
                    .Sum(i => i.OrderItems.DongAmount);

                if (totalPurchaseDang < 6)
                {
                    result.Add(new ProductMainPageQuery
                    {
                        DangRemains = 6 - totalPurchaseDang,
                        Description = product.Description,
                        seoData = product.SeoData,
                        CreationDate = product.CreationDate,
                        Status = product.Status,
                        Id = product.Id,
                        Inventory = product.Inventory.MapInventory(),
                        Title = product.Title,
                    });
                }
            }

            return new MainPageDto
            {
                InvestmentNmber = investmentCount,
                product = result.OrderByDescending(i =>
                  i.Inventory.Profit).Take(3).ToList()
            };
        }
    }
}
