using Common.Query;
using Domain.UserAgg;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.User.DTOs;
using Query.User.Map;
using System.Linq.Expressions;

namespace Query.User.GetExpression
{
    public record class GetUserByExpressionQuery(Expression<Func<Domain.UserAgg.User, bool>> expression) : IQuery<UserDto?>;
    
    internal class GetUserByExpressionQueryHandler : IQueryHandler<GetUserByExpressionQuery, UserDto?>
    {
        private readonly Context _context;

        public GetUserByExpressionQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<UserDto?> Handle(GetUserByExpressionQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(request.expression);
            if (user == null) return null;

            var userDto = await user.UserMapToUserDto(_context);

            return userDto;
        }
    }
}
