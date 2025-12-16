using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Order.DTOs;
using Query.Product;
using Query.PurchaseReport.DTOs;
using System.Collections.Generic;
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
        public static async Task<List<UserPurchaseReportDto>> MapUsersReport(this List<Domain.PurchaseReportAgg.PurchaseReport> model, Context context)
        {
            Dictionary<Guid, UserPurchaseReportDto> UserReport = new();

            foreach (var item in model)
            {
                if (UserReport.TryGetValue(item.UserId, out var user))
                {
                    user.InvestmentCount += 1;
                    user.PurchaseReport.Add(item.Map());
                    user.ProductPurchase.AddRange(await item.ProductReportDtoMapper(context));
                    user.OrderDtos.AddRange(await item.OrderReportDtoMapper(context));
                }
                else
                {
                    var userAgg = await context.Users.FirstOrDefaultAsync(user => user.Id.Equals
                                             (item.UserId));

                    if (userAgg == null)
                        return null;

                    var userReportMapResult = userAgg.UserReportDtoMapper();
                    userReportMapResult.PurchaseReport.Add(item.Map());
                    userReportMapResult.ProfitPurchases = await item.MapProfitReport(context);
                    userReportMapResult.ProductPurchase.AddRange(await item.ProductReportDtoMapper(context));
                    userReportMapResult.OrderDtos.AddRange(await item.OrderReportDtoMapper(context));
                    UserReport.Add(userAgg.Id, userReportMapResult);
                }

            }

            return UserReport.Values.ToList();
        }
        public static async Task<UserPurchaseReportDto> MapUserReport(this List<Domain.PurchaseReportAgg.PurchaseReport> model
            , Context context)
        {

            if (model == null || model.Count == 0)
                return null;

            var userAgg = await context.Users.FirstOrDefaultAsync(user => user.Id.Equals
                                     (model.FirstOrDefault().UserId));

            if (userAgg == null)
                return null;

            var userReportMapResult = userAgg.UserReportDtoMapper();
            foreach (var item in model)
            {
                userReportMapResult.PurchaseReport.Add(item.Map());
                userReportMapResult.ProfitPurchases = await item.MapProfitReport(context);
                userReportMapResult.ProductPurchase.AddRange(await item.ProductReportDtoMapper(context));
                userReportMapResult.OrderDtos.AddRange(await item.OrderReportDtoMapper(context));
            }


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
        public static async Task<List<ProductPurchaseReportDto>?> ProductReportDtoMapper(
            this Domain.PurchaseReportAgg.PurchaseReport? model, Context context)
        {

            var product = context.Products.AsTracking().Where
                (product => product.Id.Equals(model.ProductId)).AsEnumerable();

            if (product == null)
                return null;
            var result = product.Select(i => new ProductPurchaseReportDto
            {
                ImageName = i.ImageName,
                Title = i.Title,
                Id = i.Id,
                CreationDate = i.CreationDate,
                PurchaseId = model.Id,
                InventoryDto = i.Inventory.MapInventory(),
            });

            return result.ToList();
        }
        public static async Task<List<OrderDto>?> OrderReportDtoMapper(
            this Domain.PurchaseReportAgg.PurchaseReport? model, Context context)
        {

            var order = context.Orders.AsTracking().Where
                (order => order.Id.Equals(model.OrderId)).AsEnumerable();

            if (order == null)
                return null;
            var result = order.Select(i => new OrderDto
            {
                Id = i.Id,
                CreationDate = i.CreationDate,
                status = i.status,
                UserId = i.UserId,
                DateOfPurchase = i.DateOfPurchase,
                OrderItems = new OrderItemDto
                {
                    CreationDate = i.OrderItems.CreationDate,
                    DongAmount = i.OrderItems.DongAmount,
                    Id = i.OrderItems.Id,
                    InventoryId = i.OrderItems.InventoryId,
                    OrderId = i.OrderItems.OrderId,
                    PricePerDong = i.OrderItems.PricePerDong,
                    ProductId = i.OrderItems.ProductId,
                },
                //OrderItems = i.OrderItems.Select(i => new OrderItemDto
                //{
                //    CreationDate = i.CreationDate,
                //    DongAmount = i.DongAmount,
                //    Id = i.Id,
                //    InventoryId = i.InventoryId,
                //    OrderId = i.OrderId,
                //    PricePerDong = i.PricePerDong,
                //    ProductId = i.ProductId,
                //}).ToList(),
            });

            return result.ToList();
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