using Common.AspNetCore;
using Facade.PurchaseReport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Query.PurchaseReport.DTOs;

namespace DangMedicalSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseReportController : ApiController
    {
        private readonly IPurchaseReportFacade _facade;

        public PurchaseReportController(IPurchaseReportFacade facade)
        {
            _facade = facade;
        }

        [HttpGet("GetPurchaseReportFilterForAdmin")]
        public async Task<ApiResult<PurchaseReportFilterResult>> GetFilterForAdmin([FromForm] PurchaseReportFilterParam param
            , CancellationToken cancellationToken)
        {
            return QueryResult(await _facade.GetFilterForAdmin(param, cancellationToken));
        }

        [HttpGet("GetPurchaseUserReportFilterForAdmin")]
        public async Task<ApiResult<PurchaseReportUserInvestmentFilterResult>> GetFilterUserReportForAdmin([FromQuery] UserPurchaseReportFilterParam param
            , CancellationToken cancellationToken)
        {
            return QueryResult(await _facade.GetFilterPurchaseReportForAdmin(param, cancellationToken));
        }

        [Authorize]
        [HttpGet("GetPurchaseUserReportFilterForCurrentUser")]
        public async Task<ApiResult<PurchaseReportUserInvestmentFilterResult>> GetFilterUserReportForCurrentUser([FromQuery] 
        UserPurchaseReportForCurrentUserFilterParam param)
        {
            return QueryResult(await _facade.GetFilterPurchaseReportForCurrentUser(new UserPurchaseReportFilterParam
            {
                StartDate = param.StartDate,
                EndDate = param.EndDate,
                PageId = param.PageId,
                PhoneNumber = param.PhoneNumber,
                ProductId = param.ProductId,
                PurchaseReportFilter = param.PurchaseReportFilter,
                Take = param.Take,
                UserId = User.GetUserId(),
            }));
        }
      
        [HttpGet("GetById")]
        public async Task<ApiResult<UserPurchaseReportDto?>> GetById(Guid UserId)
        {
            return QueryResult(await _facade.GetById(UserId));
        }

        [Authorize]
        [HttpGet("GetForCurrentUser")]
        public async Task<ApiResult<UserPurchaseReportDto?>> GetForCurrentUser()
        {
            return QueryResult(await _facade.GetById(User.GetUserId()));
        }
    }
}
