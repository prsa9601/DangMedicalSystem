using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Product;
using Query.PurchaseReport.DTOs;
using System.Threading.Tasks;

namespace Query.PurchaseReport
{
    public static class PurchaseReportMapper
    {
        public static PurchaseReportDto? Map(
            this Domain.PurchaseReportAgg.PurchaseReport? purchaseReport)
        {
            if (purchaseReport == null)
                return null;
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
        public static async Task<UserPurchaseReportDto> MapUserReport(this Domain.PurchaseReportAgg.PurchaseReport model
            , Context context)
        {
            var userAgg = await context.Users.FirstOrDefaultAsync(user => user.Id.Equals
                                     (model.UserId));

            if (userAgg == null)
                return null;

            var userReportMapResult = userAgg.UserReportDtoMapper();
            userReportMapResult.PurchaseReport.Add(model.Map());
            userReportMapResult.ProfitPurchases = await model.MapProfitReport(context);
            userReportMapResult.ProductPurchase.Add(await model.ProductReportDtoMapper(context));

            return userReportMapResult;
        }

        public static async Task<List<ProfitPurchaseReportDto>> MapProfitReport(this Domain.PurchaseReportAgg.PurchaseReport model
            , Context context)
        {
            var profit = context.Profits.Where(profit => profit.UserId.Equals(model.UserId) &&
            profit.ProductId.Equals(model.ProductId));

            var product = await context.Products.FirstOrDefaultAsync(i => i.Id.Equals(model.ProductId));

            if (profit == null)
                return null;

            if (product == null)
                return null;



            List<ProfitPurchaseReportDto> result = new();
            foreach (var item in profit)
            {
                result.Add(new ProfitPurchaseReportDto
                {
                    AmountPaid = item.AmountPaid,
                    CreationDate = item.CreationDate,
                    Id = item.Id,
                    ImageName = item.ImageName,
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    Status = item.Status,
                    UserId = item.UserId,
                    ProductName = product.Title,
                    ForWhatPeriod = item.ForWhatPeriod,
                    ForWhatTime = item.ForWhatTime,
                    ProfitableTime = product.Inventory!.ProfitableTime,

                });
            }
            return result;
        }

        public static UserPurchaseReportDto UserReportDtoMapper(
            this Domain.UserAgg.User model)
        {
            return new UserPurchaseReportDto
            {
                UserId = model.Id,
                FirstName = model.FirstName,
                Id = model.Id,
                ImageName = model.ImageName,
                CreationDate = model.CreationDate,
                Lastame = model.LastName,
                PhoneNumber = model.PhoneNumber,
            };

        }
        public static async Task<ProductPurchaseReportDto?> ProductReportDtoMapper(
            this Domain.PurchaseReportAgg.PurchaseReport? model, Context context)
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
                InventoryDto = product.Inventory.MapInventory(),
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