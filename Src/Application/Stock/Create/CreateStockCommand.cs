using Common.Application;
using Domain.ProductAgg;
using Domain.ProductAgg.Interfaces.Repository;
using Domain.PurchaseReportAgg.Interfaces.Repository;
using Domain.StockAgg;
using Domain.StockAgg.Interfaces.Repository;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.Stock.Create
{
    public class CreateStockCommand : IBaseCommand
    {
        public Guid UserId { get; set; }
        public Guid PurchaseId { get; set; }
    }
    internal sealed class CreateStockCommandHandler : IBaseCommandHandler<CreateStockCommand>
    {
        private readonly IStockRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPurchaseReportRepository _purchaseReportRepository;

        public CreateStockCommandHandler(IStockRepository repository, IUserRepository userRepository,
            IProductRepository productRepository, IPurchaseReportRepository purchaseReportRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _purchaseReportRepository = purchaseReportRepository;
        }

        public async Task<OperationResult> Handle(CreateStockCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetTracking(request.UserId);
            var purchaseReport = await _purchaseReportRepository.GetTracking(request.PurchaseId);
           
            if (purchaseReport == null)
                return OperationResult.NotFound("ریپورتی یافت نشد");

            var product = await _productRepository.GetTracking(purchaseReport.ProductId);

            if (user == null)
                return OperationResult.NotFound("کاربری یافت نشد");

            if (product == null)
                return OperationResult.NotFound("محصولی یافت نشد");


            var stocks = await _repository.GetListByFilterAsync(r => r.ProductId.Equals(product.Id) &&
            r.UserId.Equals(user.Id) && r.PurchaseId.Equals(purchaseReport.Id));


            var nextPayment = product.Inventory.ProfitableTime switch
            {
                Domain.ProductAgg.Enum.PaymentTime.ماهانه => DateTime.Now.AddMonths(1),
                Domain.ProductAgg.Enum.PaymentTime.سه_ماهه => DateTime.Now.AddMonths(3),
                Domain.ProductAgg.Enum.PaymentTime.شش_ماهه => DateTime.Now.AddMonths(6),
                Domain.ProductAgg.Enum.PaymentTime.سالانه => DateTime.Now.AddYears(1),
                _ => DateTime.Now.AddMonths(1),
            };

            var stock = new Domain.StockAgg.Stock(product.Id, user.Id, purchaseReport.Id,
                (decimal.Parse(product.Inventory.Profit) * decimal.Parse(purchaseReport.TotalDang)).ToString(), nextPayment,
                stocks.Count() + 1);


            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
