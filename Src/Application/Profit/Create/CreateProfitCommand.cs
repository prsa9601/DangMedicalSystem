using Application.Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Domain.OrderAgg.Interfaces.Repository;
using Domain.ProductAgg.Interfaces.Repository;
using Domain.ProfitAgg;
using Domain.ProfitAgg.Repository;
using Domain.ProfitAgg.Service;
using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;

namespace Application.Profit.Create
{
    public class CreateProfitCommand : IBaseCommand
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public ProfitStatus Status { get; set; }
        public IFormFile Image { get; set; }
    }
    internal class CreateProfitCommandHandler : IBaseCommandHandler<CreateProfitCommand>
    {
        private readonly IProfitService _service;
        private readonly IProfitRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IFileService _fileService;

        public CreateProfitCommandHandler(IProfitService service, IProfitRepository repository,
            IFileService fileService, IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _service = service;
            _repository = repository;
            _fileService = fileService;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<OperationResult> Handle(CreateProfitCommand request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetByFilterAsync(i => i.Id.Equals(request.OrderId) &&
            i.UserId.Equals(request.UserId) && i.OrderItems.Any(r => r.ProductId.Equals(request.ProductId)));

            //var orders = await _orderRepository.GetListByFilterAsync(i => i.Id.Equals(request.OrderId) &&
            //i.UserId.Equals(request.UserId) && i.OrderItems.Any(r => r.ProductId.Equals(request.ProductId)));

            if (orders is null)
                return OperationResult.NotFound("اوردری یافت نشد.");

            var product = await _productRepository.GetTracking(request.ProductId);

            var dongAmounts = orders.OrderItems.Where(o => o.ProductId.Equals(request.ProductId))
                .Select(i => i.DongAmount);

            //var dongAmounts = orders.Where(i => i.OrderItems.Any(o => o.ProductId.Equals(request.ProductId)))
            //    .Select(i => i.OrderItems.Select(i => i.DongAmount));

            decimal dongAmount = dongAmounts.Sum();

            var profits = await _repository.GetListByFilterAsync(i => i.ProductId.Equals(request.ProductId)
            && i.UserId.Equals(request.UserId));

            var profit = profits.Where(i => i.Status == ProfitStatus.Success).OrderByDescending(i => i.ForWhatPeriod).FirstOrDefault();
            DateTime nextPayment;
            if (profit == null)
            {
                nextPayment = product.Inventory.ProfitableTime switch
                {
                    Domain.ProductAgg.Enum.PaymentTime.ماهانه => orders.DateOfPurchase.AddMonths(1),
                    Domain.ProductAgg.Enum.PaymentTime.سه_ماهه => orders.DateOfPurchase.AddMonths(3),
                    Domain.ProductAgg.Enum.PaymentTime.شش_ماهه => orders.DateOfPurchase.AddMonths(6),
                    Domain.ProductAgg.Enum.PaymentTime.سالانه => orders.DateOfPurchase.AddYears(1),
                    _ => DateTime.Now.AddMonths(1),
                };
            }
            else
            {
                nextPayment = product.Inventory.ProfitableTime switch
                {
                    Domain.ProductAgg.Enum.PaymentTime.ماهانه => profit.CreationDate.AddMonths(1),
                    Domain.ProductAgg.Enum.PaymentTime.سه_ماهه => profit.CreationDate.AddMonths(3),
                    Domain.ProductAgg.Enum.PaymentTime.شش_ماهه => profit.CreationDate.AddMonths(6),
                    Domain.ProductAgg.Enum.PaymentTime.سالانه => profit.CreationDate.AddYears(1),
                    _ => DateTime.Now.AddMonths(1),
                };
            }

            var newProfit = new Domain.ProfitAgg.Profit(request.UserId, request.ProductId,
                request.Status, dongAmount * decimal.Parse(product.Inventory.Profit),
                request.OrderId, _service, nextPayment, profits.Where(i => i.Status == ProfitStatus.Success).Count() + 1);

            if (request.Image != null)
            {
                var imageName = await _fileService.SaveFileAndGenerateName(request.Image, Directories.ProfitImages);
                newProfit.SetImage(imageName);
            }

            await _repository.AddAsync(newProfit);
            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
