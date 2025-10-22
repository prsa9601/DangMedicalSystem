using Common.Query;
using Infrastructure;
using Infrastructure.Mapping.Service;
using Microsoft.EntityFrameworkCore;
using Query.User.DTOs;
using Query.User.Map;

namespace Query.User.GetById
{
    public record class GetUserByIdQuery(Guid userId) : IQuery<UserDto?>;

    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto?>
    {
        private readonly Context _context;
        private readonly IMapperService _mapperService;

        public GetUserByIdQueryHandler(Context context, IMapperService mapperService)
        {
            _context = context;
            _mapperService = mapperService;
        }

        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id.Equals(request.userId));
            if (user == null) return null;

            var userDto = await user.UserMapToUserDto(_context);

            return userDto;
        }
    }
}
