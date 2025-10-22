using AngleSharp.Dom;
using Domain.UserAgg;
using Domain.UserAgg.Interfaces.Repository;
using Infrastructure._Utilities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistent.Ef.User.Repository
{
    public class UserRepository : BaseRepository<Domain.UserAgg.User>, IUserRepository
    {
        public UserRepository(Context context) : base(context)
        {
        }

        public async Task<Domain.UserAgg.User?> GetUserByFilter(
            Expression<Func<Domain.UserAgg.User, bool>> expression)
        {
            var user = await Context.Users.FirstOrDefaultAsync(expression);
            return user;
        }
        public async Task<Domain.UserAgg.User?> GetUserByFilterAsync(Expression<Func<Domain.UserAgg.User, bool>> expression)
        {
            return await Context.Users.AsTracking().Include(u => u.BankAccount)
                   .Include(u => u.UserRole)
                   .Include(u => u.UserOtps)
                   .Include(u => u.UserBlocks)
                   .Include(u => u.UserAttempts)
                   .Include(u => u.UserSessions)
                   //.Include(u => u.FirstName)
                   //.Include(u => u.LastName)
                   //.Include(u => u.PhoneNumber)
                   //.Include(u => u.Password)
                   //.Include(u => u.ImageName)
                   //.Include(u => u.Status)
                   //.Include(u => u.IsActive)
                   //.Include(u => u.BirthCertificatePhoto)
                   //.Include(u => u.NationalCardPhoto)
                   //.Include(u => u.NationalityCode)
                   //.Include(u => u.ConcurrencyStamp)
                .FirstOrDefaultAsync(expression);
        }
        public async Task<int> SaveChangeAsync(Domain.UserAgg.User entity,
              params Expression<Func<Domain.UserAgg.User, object>>[] updatedProperties)
        {
            //Context.Attach(entity);

            //foreach (var property in updatedProperties)
            //{
            //    Context.Entry(entity).Property(property).IsModified = true;
            //}

            Context.ChangeTracker.DetectChanges();
            await Context.Entry(entity).ReloadAsync();
            return await Context.SaveChangesAsync();
        }
    }
}
