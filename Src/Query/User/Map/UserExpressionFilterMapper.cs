using Infrastructure;
using Query.User.DTOs.FilterDto;
using Query.User.DTOs.FilterDto.FilterExpression;

namespace Query.User.Map
{
    public static class UserExpressionFilterMapper
    {
        public static UserExpressionFilterData UserExpressionMapFilterData(this Domain.UserAgg.User user, Context context)
        {
            return new UserExpressionFilterData
            {
                Id = user.Id,
                CreationDate = user.CreationDate,
                FirstName = user.FirstName,
                BankAccount = user.BankAccount,
                ImageName = user.ImageName,
                IsActive = user.IsActive,
                LastName = user.LastName,
                //BirthCertificatePhoto = user.BirthCertificatePhoto,
                //NationalCardPhoto = user.NationalCardPhoto,
                //NationalityCode = user.NationalityCode,
                PhoneNumber = user.PhoneNumber,
                //Status = user.Status,
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
