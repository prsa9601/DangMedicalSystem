using Common.Domain.Repository;
using System.Linq.Expressions;

namespace Domain.UserAgg.Interfaces.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<UserAgg.User?> GetUserByFilter(Expression<Func<User, bool>> expression);

    }
}
