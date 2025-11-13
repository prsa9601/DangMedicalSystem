using Common.Query;
using Common.Query.Filter;
using Domain.UserAgg.Enum;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Query.User.DTOs.FilterDto;
using Query.User.Map;

namespace Query.User.GetFilterForDocuments
{
    public class GetUsersByFilterForDocumentsQuery :
        QueryFilter<UserFilterForDocumentsResult, UserFilterForDocumentsParam>
    {
        public GetUsersByFilterForDocumentsQuery(UserFilterForDocumentsParam filterParams) : base(filterParams)
        {
        }
    }
    public sealed class GetUsersByFilterForDocumentsQueryHandler :
        IQueryHandler<GetUsersByFilterForDocumentsQuery, UserFilterForDocumentsResult>
    {
        private readonly Context _context;

        public GetUsersByFilterForDocumentsQueryHandler(Context context)
        {
            _context = context;
        }

        async Task<UserFilterForDocumentsResult> IRequestHandler<GetUsersByFilterForDocumentsQuery, UserFilterForDocumentsResult>.Handle(GetUsersByFilterForDocumentsQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;
            var result = _context.Users.Where(user => user.UserDocument != null).OrderByDescending(d => d.Id).AsQueryable();

            //if (!string.IsNullOrWhiteSpace(@params.UserIds))
            //    result = result.Where(r => r.Email.Contains(@params.Email));

            if (!string.IsNullOrWhiteSpace(@params.PhoneNumber))
                result = result.Where(r => r.PhoneNumber.Contains(@params.PhoneNumber));

            if (!string.IsNullOrWhiteSpace(@params.UserName))
                result = result.Where(r => $"{r.FirstName} {r.LastName}".Contains(@params.UserName));

            if (@params.UserStatus != null)
                result = result.Where(r => r.UserDocument.Status.Equals(@params.UserName));

            if (@params.IsActive != null)
                result = result.Where(r => r.IsActive == @params.IsActive);

            var skip = (@params.PageId - 1) * @params.Take;
            var model = new UserFilterForDocumentsResult()
            {
                Data = await result.Skip(skip).Take(@params.Take)
                    .Select(user => user.UserMapFilterForDocumentsData(_context)).ToListAsync(cancellationToken),
                FilterParams = @params
            };

            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }
    }
}
