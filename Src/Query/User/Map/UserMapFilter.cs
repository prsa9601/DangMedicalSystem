using Domain.UserAgg;
using Infrastructure;
using Query.User.DTOs;
using Query.User.DTOs.FilterDto;

namespace Query.User.Map
{
    public static class UserMapFilter
    {
        public static UserFilterData UserMapFilterData(this Domain.UserAgg.User user, Context context)
        {
            return new UserFilterData
            {
                Id = user.Id,
                CreationDate = user.CreationDate,
                FirstName = user.FirstName,
                BankAccount = user.BankAccount,
                BirthCertificatePhoto = user.BirthCertificatePhoto,
                ImageName = user.ImageName,
                IsActive = user.IsActive,
                LastName = user.LastName,
                NationalCardPhoto = user.NationalCardPhoto,
                NationalityCode = user.NationalityCode,
                PhoneNumber = user.PhoneNumber,
                Status = user.Status,
                UserRole = user.UserRole!.UserRoleMapToUserRoleDto(context) ?? null,
                //UserAttempts = user.UserAttempts.MapUserAttempts(context),
                //UserBlocks = user.UserBlocks.MapUserBlocks(context),
                //UserOtps = user.UserOtps.MapUserOtps(context),
                //UserSessions = user.UserSessions.MapUserSessions(context),
            };
        }

        //public static List<UserAttemptDto>? MapUserAttempts(this List<UserAttempt>? userAttempts
        //    , Context context)
        //{

        //} 
        //public static List<UserOtpDto>? MapUserOtps(this List<UserOtp>? userOtps
        //    , Context context)
        //{

        //} 
        //public static List<UserBlockDto>? MapUserBlocks(this List<UserBlock>? userBlocks
        //    , Context context)
        //{

        //} 
        //public static List<UserSessionDto>? MapUserSessions(this List<UserSession>? userSessions
        //    , Context context)
        //{

        //} 
    }
}
