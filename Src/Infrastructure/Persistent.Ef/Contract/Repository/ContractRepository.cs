using Domain.Contract.Repository;
using Infrastructure._Utilities;

namespace Infrastructure.Persistent.Ef.Contract.Repository
{
    public class ContractRepository : BaseRepository<Domain.Contract.ContractAgg>, IContractRepository
    {
        public ContractRepository(Context context) : base(context)
        {
        }
    }
}
