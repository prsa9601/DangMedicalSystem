using Common.Query;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.User.DTOs.FilterDto;
using Query.User.Map;

namespace Query.User.GetFilter
{
    public class GetUsersByFilterQuery : QueryFilter<UserFilterResult, UserFilterParam>
    {
        public GetUsersByFilterQuery(UserFilterParam filterParams) : base(filterParams)
        {
        }
    }
    internal class GetUsersByFilterQueryFilterHandler : IQueryHandler<GetUsersByFilterQuery, UserFilterResult>
    {
        private readonly Context _context;

        public GetUsersByFilterQueryFilterHandler(Context context)
        {
            _context = context;
        }

        public async Task<UserFilterResult> Handle(GetUsersByFilterQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;
            var result = _context.Users.OrderByDescending(d => d.Id).AsQueryable();

            //if (!string.IsNullOrWhiteSpace(@params.UserIds))
            //    result = result.Where(r => r.Email.Contains(@params.Email));

            if (!string.IsNullOrWhiteSpace(@params.Search))
                result = result.Where(r => r.PhoneNumber.Contains(@params.Search) || r.FirstName.Contains(@params.Search)
                || r.LastName.Contains(@params.Search));
         
           
            if (@params.IsActive != null)
                result = result.Where(r => r.IsActive == @params.IsActive);

            var skip = (@params.PageId - 1) * @params.Take;
            var model = new UserFilterResult()
            {
                Data = await result.Skip(skip).Take(@params.Take)
                    .Select(user => user.UserMapFilterData(_context)).ToListAsync(cancellationToken),
                FilterParams = @params
            };

            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }
    }
}
