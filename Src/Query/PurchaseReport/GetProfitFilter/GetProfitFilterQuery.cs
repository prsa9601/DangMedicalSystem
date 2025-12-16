using Common.Query;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Contract.DTOs;
using Query.PurchaseReport.DTOs;

namespace Query.PurchaseReport.GetProfitFilter
{
    public class GetProfitFilterQuery : QueryFilter<UserProfitPurchaseReportDtoFilterResult, UserProfitPurchaseReportDtoFilterParam>
    {
        public GetProfitFilterQuery(UserProfitPurchaseReportDtoFilterParam filterParams) : base(filterParams)
        {
        }
    }
    public class GetProfitFilterQueryHandler : IQueryHandler<GetProfitFilterQuery, UserProfitPurchaseReportDtoFilterResult>
    {
        private readonly Context _context;

        public GetProfitFilterQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<UserProfitPurchaseReportDtoFilterResult> Handle(GetProfitFilterQuery request, CancellationToken cancellationToken)
        {
            var @param = request.FilterParams;

            var orders = _context.Orders.Where(i => i.status == Domain.OrderAgg.Enum.OrderStatus.paid);
            var userIds = orders.Select(i => i.UserId).Distinct();

            var user = _context.Users.Where(i => userIds.Contains(i.Id)).ToList();

            var res = new List<UserProfitPurchaseReportDto>();

            foreach (var item in user)
            {
                var result = SetUser(item);

                var order = _context.Orders.AsTracking().Where(i => i.UserId.
                Equals(item.Id) && i.status == Domain.OrderAgg.Enum.OrderStatus.paid).Include(i => i.OrderItems);


                result.InvestmentCount = orders.Count();
                foreach (var item2 in order)
                {
                    if (item2.OrderItems == null)
                        continue;
                    result = SetOrder(result, item2, _context);
                }
                res.Add(result);
            }

            var skip = (@param.PageId - 1) * @param.Take;
            var model = new UserProfitPurchaseReportDtoFilterResult()
            {
                Data = res.Skip(skip).Take(@param.Take).ToList(),
                FilterParams = @param
            };

            model.GeneratePaging(res.AsQueryable(), @param.Take, @param.PageId);
            return model;

        }

        private UserProfitPurchaseReportDto SetUser(Domain.UserAgg.User user)
        {
            return new UserProfitPurchaseReportDto
            {
                Id = user.Id,
                CreationDate = user.CreationDate,
                FirstName = user.FirstName,
                ImageName = user.ImageName,
                Lastame = user.LastName,
                PhoneNumber = user.PhoneNumber,
                //OrderDtos =,
                //InvestmentCount =,

            };
        }
        private UserProfitPurchaseReportDto SetOrder(UserProfitPurchaseReportDto model, Domain.OrderAgg.Order order, Context context)
        {
            var product = context.Products.FirstOrDefault(i => i.Id.
            Equals(order.OrderItems.ProductId) && i.Inventory.Id.Equals(order.OrderItems.InventoryId));

            var orderDto = new OrderProfitDto
            {
                Id = order.Id,
                status = order.status,
                DateOfPurchase = order.DateOfPurchase,
                CreationDate = order.CreationDate,
                OrderItems = new OrderProfitItemDto
                {
                    CreationDate = order.OrderItems.CreationDate,
                    DongAmount = order.OrderItems.DongAmount,
                    Id = order.OrderItems.Id,
                    PricePerDong = order.OrderItems.PricePerDong,
                    InventoryId = order.OrderItems.InventoryId,
                    Product = new ProductProfitPurchaseReportDto
                    {
                        CreationDate = product.CreationDate,
                        Id = product.Id,
                        profitPurchaseReportDtos = SetProfit(model, order, product, context),
                        ImageName = product.ImageName,
                        purchaseReportDto = SetPurchse(model, order, product, context),
                        Title = product.Title,
                        InventoryDto = new Product.DTOs.InventoryDto
                        {
                            ProductId = product.Id,
                            Id = product.Inventory.Id,
                            Dong = product.Inventory.Dong,
                            CreationDate = product.Inventory.CreationDate,
                            Profit = product.Inventory.Profit,
                            ProfitableTime = product.Inventory.ProfitableTime,
                            TotalPrice = product.Inventory.TotalPrice,
                        },
                        //Paid =,
                        //Unpaid = ,
                    },
                    OrderId = order.OrderItems.OrderId,

                },
                UserId = order.UserId,
            };
            model.OrderDtos.Add(orderDto);
            CalculateProfit(model, orderDto, product, context);

            return model;
        }
        private List<PurchaseProfitReportDto> SetPurchse(UserProfitPurchaseReportDto model, Domain.OrderAgg.Order order, Domain.ProductAgg.Product product, Context context)
        {
            var purchases = context.PurchaseReports.Where(i => i.UserId.
            Equals(model.Id) && i.OrderId.Equals(order.Id) && i.ProductId.Equals(product.Id));
            var result = new List<PurchaseProfitReportDto>();

            foreach (var purchase in purchases)
            {
                result.Add(new PurchaseProfitReportDto
                {
                    Id = purchase.Id,
                    CreationDate = purchase.CreationDate,
                    ProductId = purchase.ProductId,
                    Profit = purchase.Profit,
                    ProfitPerDang = purchase.ProfitPerDang,
                    PurchaseDang = purchase.PurchaseDang,
                    PurchaseDangPerDang = purchase.PurchaseDangPerDang,
                    PurchasePrice = purchase.PurchasePrice,
                    PurchasePricePerDang = purchase.PurchasePricePerDang,
                    TotalDang = purchase.TotalDang,
                    TotalPrice = purchase.TotalPrice,
                    TotalProfit = purchase.TotalProfit,
                    UserId = purchase.UserId,
                });
            }
            return result;
        }
        private List<ProfitPurchaseReportProfitDto> SetProfit(UserProfitPurchaseReportDto model, Domain.OrderAgg.Order order, Domain.ProductAgg.Product product, Context context)
        {
            var profits = context.Profits.Where(i => i.UserId.
            Equals(model.Id) && i.OrderId.Equals(order.Id) && i.ProductId.Equals(product.Id));
            var result = new List<ProfitPurchaseReportProfitDto>();


            foreach (var profit in profits)
            {
                result.Add(new ProfitPurchaseReportProfitDto
                {
                    Id = profit.Id,
                    CreationDate = profit.CreationDate,
                    ProductId = profit.ProductId,
                    ProductName = product.Title,
                    AmountPaid = profit.AmountPaid,
                    ForWhatPeriod = profit.ForWhatPeriod,
                    ForWhatTime = profit.ForWhatTime,
                    ImageName = product.ImageName,
                    OrderId = profit.OrderId,
                    ProfitableTime = product.Inventory.ProfitableTime,
                    Status = profit.Status,
                    UserId = profit.UserId,
                });
            }
            return result;
        }
        private void CalculateProfit(UserProfitPurchaseReportDto model, OrderProfitDto order, Domain.ProductAgg.Product product, Context context)
        {
            int period = 0;
            var orderProfit = model.OrderDtos.FirstOrDefault(i => i.Id.Equals(order.Id));
            var date = orderProfit.DateOfPurchase;

            var perioDate = orderProfit.OrderItems.Product.InventoryDto.ProfitableTime switch
            {
                Domain.ProductAgg.Enum.PaymentTime.شش_ماهه => TimeSpan.FromDays(180),
                Domain.ProductAgg.Enum.PaymentTime.سالانه => TimeSpan.FromDays(365),
                Domain.ProductAgg.Enum.PaymentTime.ماهانه => TimeSpan.FromDays(30),
                Domain.ProductAgg.Enum.PaymentTime.سه_ماهه => TimeSpan.FromDays(90),
                _ => throw new ArgumentException()
            };

            while ((date += perioDate) <= DateTime.Now)
            {
                period++;

                if (CheckStockPayment(date, order.Id, order.OrderItems.Product.Id, order.UserId, context))
                {
                    order.OrderItems.Product.Paid.Add(new PaidProfit
                    {
                        Date = date,
                        PeriodNumber = period,
                        OrderId = order.Id,
                        ProductId = product.Id,
                        ExpectedAmount = (decimal.Parse(order.OrderItems.Product.InventoryDto.Profit) * order.OrderItems.DongAmount) / 6,
                        ProductName = order.OrderItems.Product.Title,
                    });
                }
                else
                {
                    order.OrderItems.Product.Unpaid.Add(new UnpaidProfit
                    {
                        Date = date,
                        PeriodNumber = period,
                        OrderId = order.Id,
                        ProductId = product.Id,
                        ExpectedAmount = (decimal.Parse(order.OrderItems.Product.InventoryDto.Profit) * order.OrderItems.DongAmount) / 6,
                        ProductName = order.OrderItems.Product.Title,
                    });
                }

            }
        }

        private bool CheckStockPayment(DateTime date, Guid orderId, Guid productId, Guid userId, Context context)
        {
            var stock = context.Profits.Where(i => i.ForWhatTime == date &&
            i.Status == Domain.ProfitAgg.ProfitStatus.Success && i.OrderId.Equals(orderId) &&
            i.ProductId.Equals(productId) && i.UserId.Equals(userId));

            if (stock.Any())
            {
                return true;
            }
            return false;
        }

    }
}