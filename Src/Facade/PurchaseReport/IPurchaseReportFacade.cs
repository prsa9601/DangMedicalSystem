using MediatR;
using Query.PurchaseReport.DTOs;
using Query.PurchaseReport.ReportForAdmin;

namespace Facade.PurchaseReport
{
    public interface IPurchaseReportFacade 
    {
        Task<PurchaseReportFilterResult> GetFilterForAdmin(PurchaseReportFilterParam param, CancellationToken cancellationToken);
    }
    internal class PurchaseReportFacade : IPurchaseReportFacade
    {
        private readonly IMediator _mediator;

        public PurchaseReportFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<PurchaseReportFilterResult> GetFilterForAdmin(PurchaseReportFilterParam param, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetPurchaseReportForAdmin(param), cancellationToken);
        }
    }
}
