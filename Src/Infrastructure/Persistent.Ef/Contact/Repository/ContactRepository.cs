using Domain.Contact.Repository;
using Infrastructure._Utilities;

namespace Infrastructure.Persistent.Ef.Contact.Repository
{
    public class ContactRepository : BaseRepository<Domain.Contact.ContactAgg>, IContactRepository
    {
        public ContactRepository(Context context) : base(context)
        {
        }
    }
}
