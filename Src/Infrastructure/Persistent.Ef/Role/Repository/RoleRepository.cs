using Domain.RoleAgg.Interfaces.Repository;
using Infrastructure._Utilities;

namespace Infrastructure.Persistent.Ef.Role.Repository
{
    public class RoleRepository : BaseRepository<Domain.RoleAgg.Role>, IRoleRepository
    {
        public RoleRepository(Context context) : base(context)
        {
        }
    }
}
