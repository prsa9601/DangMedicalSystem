using Common.Query;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Contract.DTOs;
using Query.Order.DTOs;

namespace Query.Contract.GetFilter
{
    public class GetContractByFilter : QueryFilter<ContractFilterResult, ContractFilterParam>
    {
        public GetContractByFilter(ContractFilterParam filterParams) : base(filterParams)
        {
        }
    }
    internal class GetContractByFilterHandler : IQueryHandler<GetContractByFilter, ContractFilterResult>
    {
        private readonly Context _context;

        public GetContractByFilterHandler(Context context)
        {
            _context = context;
        }

        public async Task<ContractFilterResult> Handle(GetContractByFilter request, CancellationToken cancellationToken)
        {
            var @param = request.FilterParams;
            var result = _context.Contracts.OrderByDescending(i => i.CreationDate).AsQueryable();


            if (!string.IsNullOrWhiteSpace(param.FullName))
            {
                result = result.Where(i => i.FullName.Contains(param.FullName, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrWhiteSpace(param.PhoneNumber))
            {
                result = result.Where(i => i.PhoneNumber.Contains(param.PhoneNumber));
            }
            if (!string.IsNullOrWhiteSpace(param.Email))
            {
                result = result.Where(i => i.Email.Contains(param.Email));
            }
            if (param.Status != null)
            {
                result = result.Where(i => i.Status == param.Status);
            }


            var skip = (@param.PageId - 1) * @param.Take;
            var model = new ContractFilterResult()
            {
                Data = await result.Skip(skip).Take(@param.Take)
                    .Select(order => order.Map()).ToListAsync(cancellationToken),
                FilterParams = @param
            };

            model.GeneratePaging(result, @param.Take, @param.PageId);
            return model;
        }
    }
}
