using Application.Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Domain.ProfitAgg;
using Domain.ProfitAgg.Repository;
using Domain.ProfitAgg.Service;
using Microsoft.AspNetCore.Http;

namespace Application.Profit.Create
{
    public class CreateProfitCommand : IBaseCommand
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public ProfitStatus Status { get; set; }
        public decimal Price { get; set; }
        public DateTime ForWhatTime { get; set; }
        public int ForWhatPeriod { get; set; }
        public IFormFile Image { get; set; }
    }
    internal class CreateProfitCommandHandler : IBaseCommandHandler<CreateProfitCommand>
    {
        private readonly IProfitService _service;
        private readonly IProfitRepository _repository;
        private readonly IFileService _fileService;

        public CreateProfitCommandHandler(IProfitService service, IProfitRepository repository, 
            IFileService fileService)
        {
            _service = service;
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(CreateProfitCommand request, CancellationToken cancellationToken)
        {
            var profit = new Domain.ProfitAgg.Profit(request.UserId, request.ProductId, 
                request.Status, request.Price, request.OrderId, _service, request.ForWhatTime, request.ForWhatPeriod);

            if(request.Image != null)
            {
                var imageName = await _fileService.SaveFileAndGenerateName(request.Image, Directories.ProfitImages);
                profit.SetImage(imageName);
            }

            await _repository.AddAsync(profit);
            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
