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
        public static async Task<PurchaseReportUserInvestmentFilterResult> MapUserReport(this Domain.PurchaseReportAgg.PurchaseReport model, Context context)
        {
            Dictionary<Guid, PurchaseReportUserInvestmentFilterResult> UserReport = new();
            if (UserReport.TryGetValue(model.UserId, out var user))
            {

            }
            else
            {
                var userAgg = await context.Users.FirstOrDefaultAsync(user => user.Id.Equals
                (model.UserId));

                if (userAgg == null)
                    return null;

                var userReportMapResult = userAgg.UserReportDtoMapper();
                userReportMapResult.PurchaseReport.Add(model.Map());
                userReportMapResult.ProductPurchase.Add(await model.ProductReportDtoMapper(context));
                UserReport.Add(userAgg.Id, userAgg.UserReportDtoMapper());

            }
        }
        public static UserPurchaseReportDto UserReportDtoMapper(
            this Domain.UserAgg.User model)
        {
            return new UserPurchaseReportDto
            {
                UserId = model.Id,
                FirstName = model.FirstName,
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