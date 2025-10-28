using Common.Query;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.User.DTOs.FilterDto;
using Query.User.DTOs.FilterDto.FilterExpression;
using Query.User.GetExpression;
using Query.User.Map;
using System.Linq.Expressions;

namespace Query.User.GetListByExpression
{
    public class GetListByExpressionQuery : QueryFilter<UserExpressionFilterResult, UserExpressionFilterParam>
    {
        public GetListByExpressionQuery(UserExpressionFilterParam filterParams) : base(filterParams)
        {
        }
    }
    public class GetListByExpressionQueryHandler : IQueryHandler<GetListByExpressionQuery, UserExpressionFilterResult>
    {
        private readonly Context _context;

        public GetListByExpressionQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<UserExpressionFilterResult> Handle(GetListByExpressionQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;
            var result = _context.Users.Select(i => i.UserExpressionMapFilterData(_context))
                .Where(@params.Expression);

            var skip = (@params.PageId - 1) * @params.Take;
            var model = new UserExpressionFilterResult()
            {
                Data = await result.ToListAsync(cancellationToken),
                FilterParams = @params
            };

            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }
    }
}
