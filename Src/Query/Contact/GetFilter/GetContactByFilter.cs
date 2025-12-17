using Common.Query;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Contact.DTOs;
using Query.Order.DTOs;

namespace Query.Contact.GetFilter
{
    public class GetContactByFilter : QueryFilter<ContactFilterResult, ContactFilterParam>
    {
        public GetContactByFilter(ContactFilterParam filterParams) : base(filterParams)
        {
        }
    }
    internal class GetContactByFilterHandler : IQueryHandler<GetContactByFilter, ContactFilterResult>
    {
        private readonly Context _context;

        public GetContactByFilterHandler(Context context)
        {
            _context = context;
        }

        public async Task<ContactFilterResult> Handle(GetContactByFilter request, CancellationToken cancellationToken)
        {
            var @param = request.FilterParams;
            var result = _context.Contacts.OrderByDescending(i => i.CreationDate).AsQueryable();


            if (!string.IsNullOrWhiteSpace(param.FullName))
            {
                result = result.Where(i => i.FullName.ToLower().Contains(param.FullName.ToLower()));
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
            var model = new ContactFilterResult()
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
