using Domain.UserAgg;
using Domain.UserAgg.Interfaces.Repository;
using Infrastructure._Utilities;

namespace Infrastructure.Persistent.Ef.User.Repository
{
    public class UserRepository :  BaseRepository<Domain.UserAgg.User>, IUserRepository
    {
        public UserRepository(Context context) : base(context)
        {
        }
    }
}
