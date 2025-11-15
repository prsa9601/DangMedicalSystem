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
                BankAccount = user.BankAccount.BankAccountMapToBankAccountDto(),
                //BirthCertificatePhoto = user.BirthCertificatePhoto,
                ImageName = user.ImageName,
                IsActive = user.IsActive,
                UserDocument = user.UserDocument.UserDocumentMapToUserDocumentDto() ?? null,
                LastName = user.LastName,
                //NationalCardPhoto = user.NationalCardPhoto,
                //NationalityCode = user.NationalityCode,
                PhoneNumber = user.PhoneNumber,
                //Status = user.Status,
                UserRole = user.UserRole!.UserRoleMapToUserRoleDto(context) ?? null,
            };
        }
        public static UserDocumentDto UserDocumentMapToUserDocumentDto(this UserDocument? userDocument)
        {
            if (userDocument is null)
                return null;
            return new UserDocumentDto
            {
                CreationDate = userDocument.CreationDate,
                BirthCertificatePhoto = userDocument.BirthCertificatePhoto,
                Id = userDocument.Id,
                NationalCardPhoto = userDocument.NationalCardPhoto,
                NationalityCode = userDocument.NationalCardPhoto,
                Status = userDocument.Status,
                UserId = userDocument.UserId,
            };
        }
        public static UserRoleDto UserRoleMapToUserRoleDto(this UserRole? userRole, Context context)
        {
            if (userRole == null) return null;
            var role = context.Roles.FirstOrDefault(role => role.Id.Equals(userRole.RoleId));
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
        public static UserBankAccountDto BankAccountMapToBankAccountDto(this UserBankAccount? bankAccount)
        {
            if (bankAccount == null) return null;
            return new UserBankAccountDto
            {
                UserId = bankAccount.UserId,
                FirstName = bankAccount.FirstName,
                CardNumber = bankAccount.CardNumber
                ,
                ExpirationDateMonth = bankAccount.ExpirationDateMonth,
                ExpirationDateYear = bankAccount.ExpirationDateYear,
                IsConfirmed = bankAccount.IsConfirmed,
                LastName = bankAccount.LastName,
                Shaba = bankAccount.Shaba,
            };
        }
    }
}
