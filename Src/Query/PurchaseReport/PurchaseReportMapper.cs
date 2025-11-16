using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.PurchaseReport.DTOs;
using System.Threading.Tasks;

namespace Query.PurchaseReport
{
    public static class PurchaseReportMapper
    {
        public static PurchaseReportDto Map(
            this Domain.PurchaseReportAgg.PurchaseReport purchaseReport)
        {
            return new PurchaseReportDto
            {
                CreationDate = purchaseReport.CreationDate,
                Id = purchaseReport.Id,
                ProductId = purchaseReport.ProductId,
                Profit = purchaseReport.Profit,
                ProfitPerDang = purchaseReport.ProfitPerDang,
                PurchaseDang = purchaseReport.PurchaseDang,
                PurchaseDangPerDang = purchaseReport.PurchaseDangPerDang,
                PurchasePricePerDang = purchaseReport.PurchasePricePerDang,
                TotalDang = purchaseReport.TotalDang,
                PurchasePrice = purchaseReport.PurchasePrice,
                TotalPrice = purchaseReport.TotalPrice,
                TotalProfit = purchaseReport.TotalProfit,
                UserId = purchaseReport.UserId,
            };
        }
        public static async Task<List<UserPurchaseReportDto>> MapUserReport(this List<Domain.PurchaseReportAgg.PurchaseReport> model, Context context)
        {
            Dictionary<Guid, UserPurchaseReportDto> UserReport = new();

            foreach (var item in model)
            {
                if (UserReport.TryGetValue(item.UserId, out var user))
                {
                    user.InvestmentCount += 1;
                    user.PurchaseReport.Add(item.Map());
                    user.ProductPurchase.Add(await item.ProductReportDtoMapper(context));
                }
                else
                {
                    var userAgg = await context.Users.FirstOrDefaultAsync(user => user.Id.Equals
                                             (item.UserId));

                    if (userAgg == null)
                        return null;

                    var userReportMapResult = userAgg.UserReportDtoMapper();
                    userReportMapResult.PurchaseReport.Add(item.Map());
                    userReportMapResult.ProductPurchase.Add(await item.ProductReportDtoMapper(context));
                    UserReport.Add(userAgg.Id, userReportMapResult);
                }

            }

            return UserReport.Values.ToList();
        }
        public static UserPurchaseReportDto UserReportDtoMapper(
            this Domain.UserAgg.User model)
        {
            return new UserPurchaseReportDto
            {
                UserId = model.Id,
                FirstName = model.FirstName,
                Id = model.Id,
                CreationDate = model.CreationDate,
                Lastame = model.LastName,
                PhoneNumber = model.PhoneNumber,
            };

        }
        public static async Task<ProductPurchaseReportDto> ProductReportDtoMapper(
            this Domain.PurchaseReportAgg.PurchaseReport model, Context context)
        {

            var product = await context.Products.FirstOrDefaultAsync
                (product => product.Id.Equals(model.ProductId));

            if (product == null)
                return null;
            return new ProductPurchaseReportDto
            {
                ImageName = product.ImageName,
                Title = product.Title,
                Id = product.Id,
                CreationDate = product.CreationDate,
                PurchaseId = model.Id,
            };

        }
    }
}
//return new PurchaseReportDto
//            {
//                CreationDate = purchaseReport.CreationDate,
//                Id = purchaseReport.Id,
//                ProductId = purchaseReport.ProductId,
//                Profit = purchaseReport.Profit,
//                ProfitPerDang = purchaseReport.ProfitPerDang,
//                PurchaseDang = purchaseReport.PurchaseDang,
//                PurchaseDangPerDang = purchaseReport.PurchaseDangPerDang,
//                PurchasePricePerDang = purchaseReport.PurchasePricePerDang,
//                TotalDang = purchaseReport.TotalDang,
//                PurchasePrice = purchaseReport.PurchasePrice,
//                TotalPrice = purchaseReport.TotalPrice,
//                TotalProfit = purchaseReport.TotalProfit,
//                UserId = purchaseReport.UserId,
//            };