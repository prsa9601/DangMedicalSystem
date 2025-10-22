using Domain.RoleAgg.Enum;
using Domain.UserAgg;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.User.DTOs;
using System.Threading.Tasks;

namespace Query.User.Map
{
    public static class UserMapper
    {
        public static async Task<UserDto?> UserMapToUserDto(this Domain.UserAgg.User user, Context context)
        {
            return new UserDto
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
                UserRole =  user.UserRole!.UserRoleMapToUserRoleDto(context) ?? null,
            };
        }
        public static UserRoleDto UserRoleMapToUserRoleDto(this UserRole? userRole, Context context)
        {
            if (userRole == null) return null;
            var role =  context.Roles.FirstOrDefault(role => role.Id.Equals(userRole.RoleId));
            var permission = role!.RolePermissions;
            var userRoleDto = new UserRoleDto
            {
                Id = userRole.Id,
                CreationDate = userRole.CreationDate,
                RoleId = userRole.RoleId,
                RoleName = role.Title,
                UserId = userRole.UserId,
            };

            userRoleDto.Permissions.AddRange(permission.Select(i => i.Permission));
            return userRoleDto;
        }
    }
}
