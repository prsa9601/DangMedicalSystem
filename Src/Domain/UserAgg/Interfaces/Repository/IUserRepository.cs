using Common.Domain.Repository;
using System.Linq.Expressions;

namespace Domain.UserAgg.Interfaces.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<UserAgg.User?> GetUserByFilter(Expression<Func<User, bool>> expression);
        Task<User?> GetUserByFilterAsync(Expression<Func<User, bool>> expression);
        Task<int> SaveChangeAsync(Domain.UserAgg.User entity,
            params Expression<Func<Domain.UserAgg.User, object>>[] updatedProperties);

    }
}
