using Common.Query;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.User.DTOs;
using Query.User.Map;
using UserMapper = Query.User.Map;

namespace Query.User.CheckOtpCodeForPhoneNumber
{
    public enum ForAuthAction
    {
        Login,
        Register,
        ResetPassword
    }
    public record class CheckOtpCodeForPhoneNumberQuery(string phoneNumber, string ipAddress) : IQuery<UserDto?>;
    public class CheckOtpCodeForPhoneNumberQueryHandler : IQueryHandler<CheckOtpCodeForPhoneNumberQuery, UserDto?>
    {
        private readonly Context _context;

        public CheckOtpCodeForPhoneNumberQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<UserDto?> Handle(CheckOtpCodeForPhoneNumberQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.PhoneNumber.Equals(request.phoneNumber));

            if (user == null)
                return null;

            var session = user.UserSessions.FirstOrDefault(i => i.ExpireDate > DateTime.Now && i.UserId.Equals(user.Id)
            && i.IpAddress == request.ipAddress && i.IsActive == true && ((i.ExpireDate.Minute - i.CreationDate.Minute) == 3));

            //if(request.Action == ForAuthAction.Login)
            //{

            //}

      
            if (session is null)
                return null;

            return await UserMapper.UserMapper.UserMapToUserDto(user, _context);

        }
    }
}
