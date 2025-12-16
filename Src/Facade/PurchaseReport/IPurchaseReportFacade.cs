using MediatR;
using Query.PurchaseReport.DTOs;
using Query.PurchaseReport.GetById;
using Query.PurchaseReport.GetProfit;
using Query.PurchaseReport.GetProfitFilter;
using Query.PurchaseReport.ReportForAdmin;
using Query.PurchaseReport.ReportUserInvestment;

namespace Facade.PurchaseReport
{
    public interface IPurchaseReportFacade
    {
        Task<PurchaseReportFilterResult> GetFilterForAdmin(PurchaseReportFilterParam param, CancellationToken cancellationToken);
        Task<PurchaseReportUserInvestmentFilterResult> GetFilterPurchaseReportForAdmin(UserPurchaseReportFilterParam param, CancellationToken cancellationToken);
        Task<PurchaseReportUserInvestmentFilterResult> GetFilterPurchaseReportForCurrentUser(UserPurchaseReportFilterParam param);
        Task<UserPurchaseReportDto?> GetById(Guid UserId);
        Task<UserProfitPurchaseReportDto> GetProfit(Guid UserId);
        Task<UserProfitPurchaseReportDtoFilterResult> GetProfitFilter(UserProfitPurchaseReportDtoFilterParam param);
    }
    internal class PurchaseReportFacade : IPurchaseReportFacade
    {
        private readonly IMediator _mediator;

        public PurchaseReportFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<UserPurchaseReportDto?> GetById(Guid UserId)
        {
            return await _mediator.Send(new GetPurchaseReportByIdQuery
            {
                UserId = UserId
            });
        }

        public async Task<PurchaseReportFilterResult> GetFilterForAdmin(PurchaseReportFilterParam param, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetPurchaseReportForAdmin(param), cancellationToken);
        }

        public async Task<PurchaseReportUserInvestmentFilterResult> GetFilterPurchaseReportForAdmin(UserPurchaseReportFilterParam param, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new ReportUserInvestmentQueryFilter(param), cancellationToken);
        }

        public async Task<PurchaseReportUserInvestmentFilterResult> GetFilterPurchaseReportForCurrentUser(UserPurchaseReportFilterParam param)
        {
            return await _mediator.Send(new ReportUserInvestmentQueryFilter(param));
        }

        public async Task<UserProfitPurchaseReportDto> GetProfit(Guid UserId)
        {
            return await _mediator.Send(new GetProfitForCurrentUserQuery()
            {
                userId = UserId
            });
        }

        public async Task<UserProfitPurchaseReportDtoFilterResult> GetProfitFilter(UserProfitPurchaseReportDtoFilterParam param)
        {
            return await _mediator.Send(new GetProfitFilterQuery(param));
        }
    }
}
