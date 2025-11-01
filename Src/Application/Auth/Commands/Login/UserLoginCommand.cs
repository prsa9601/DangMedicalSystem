using Application.Auth.Shared.Utilities;
using Application.Shared.Abstractions.Jwt.Enum;
using Application.Shared.Abstractions.Jwt.Interfaces;
using Common.Application;
using Common.Application.SecurityUtil;
using Domain.RoleAgg.Interfaces.Repository;
using Domain.UserAgg.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System.Security;

namespace Application.Auth.Commands.Login
{
    public class UserLoginCommand : IBaseCommand<string>
    {
        public UserLoginCommand(string phoneNumber, bool rememberMe, string password, string ipAddress)
        {
            this.phoneNumber = phoneNumber.EnsureLeadingZero();
            this.rememberMe = rememberMe;
            this.password = password;
            this.ipAddress = ipAddress;
        }

        public required string phoneNumber { get; set; }
        public bool rememberMe { get; set; }
        public required string password { get; set; }
        public required string ipAddress { get; set; }
    }
    public class UserLoginCommandHandler : IBaseCommandHandler<UserLoginCommand, string>
    {
        private readonly IUserRepository _repository;
        private readonly IRoleRepository _roleRepository;
        private readonly IJwtSettingsFactory _jwtServiceFactory;

        public UserLoginCommandHandler(IUserRepository repository, IJwtSettingsFactory jwtServiceFactory, IRoleRepository roleRepository)
        {
            _repository = repository;
            _jwtServiceFactory = jwtServiceFactory;
            _roleRepository = roleRepository;
        }

        public async Task<OperationResult<string>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByFilter(i => i.PhoneNumber == request.phoneNumber);
            if (user == null) return OperationResult<string>.NotFound("کاربری با این مشخصات یافت نشد.");

            if (CheckSessionForOtpCode(user, request.ipAddress) == false)
            {
                return OperationResult<string>.Error("قبل از ورود باید شماره تماستون رو فعال کنید.");
            }

            string hashPassword = Sha256Hasher.Hash(request.password);

            if (!Sha256Hasher.IsCompare(user.Password, request.password))
                return OperationResult<string>.Error("کاربری با این مشخصات یافت نشد.");

            if (user.UserRole == null)
            {
                var jwtRefreshService = _jwtServiceFactory.CreateSetting(TokenType.AuthRefreshToken);
                var jwtAuthService = _jwtServiceFactory.CreateSetting(TokenType.AuthToken);

                if (request.rememberMe == true)
                {

                    string refreshToken = jwtRefreshService.GenerateToken(
                        user.Id, user.PhoneNumber, new List<string> { "Guest" } ?? null);

                    user.SetUserSession(refreshToken, request.ipAddress, DateTime.Now.AddDays(14));
                }

                string authToken = jwtAuthService.GenerateToken(
                    user.Id, user.PhoneNumber, new List<string> { "Guest" } ?? null);



                await _repository.SaveChangeAsync();

                return OperationResult<string>.Success(authToken);
            }
            else
            {
                var role = await _roleRepository.GetByFilterAsync(i => i.Id.Equals(user.UserRole.RoleId)) ?? null;

                var jwtRefreshService = _jwtServiceFactory.CreateSetting(TokenType.AuthRefreshToken);
                var jwtAuthService = _jwtServiceFactory.CreateSetting(TokenType.AuthToken);

                if (request.rememberMe == true)
                {

                    string refreshToken = jwtRefreshService.GenerateToken(
                        user.Id, user.PhoneNumber, new List<string> { role.Title } ?? null);

                    user.SetUserSession(refreshToken, request.ipAddress, DateTime.Now.AddDays(14));
                }

                string authToken = jwtAuthService.GenerateToken(
                    user.Id, user.PhoneNumber, new List<string> { role.Title } ?? null);




                await _repository.SaveChangeAsync();

                return OperationResult<string>.Success(authToken);
            }
        }
        private bool CheckSessionForOtpCode(Domain.UserAgg.User user, string ipAddress)
        {
            if (user == null)
                return false;

            var session = user.UserSessions.FirstOrDefault(i => i.ExpireDate > DateTime.Now && i.UserId.Equals(user.Id)
            && i.IpAddress == ipAddress && i.IsActive == true && ((i.ExpireDate.Minute - i.CreationDate.Minute) == 7));

            if (session is null)
                return false;

            session.ChangeActivity(false);
            return true;
        }
    }
}
