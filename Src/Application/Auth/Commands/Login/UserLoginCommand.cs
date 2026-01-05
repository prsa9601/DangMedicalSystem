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
    public class UserLoginCommand : IBaseCommand<LoginCommandResult>
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
    public class UserLoginCommandHandler : IBaseCommandHandler<UserLoginCommand, LoginCommandResult>
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

        public async Task<OperationResult<LoginCommandResult>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByFilter(i => i.PhoneNumber == request.phoneNumber);
            if (user == null) return OperationResult<LoginCommandResult>.NotFound("کاربری با این مشخصات یافت نشد.", null);

            var sessions = user.UserSessions.Where(i => i.ExpireDate > DateTime.Now && i.JwtRefreshToken.Length > 15);
            if (sessions.Count() >= 3)
            {
                return OperationResult<LoginCommandResult>.Error("امکان ورود با 3 دستگاه همزمان وجود ندارد.");
            }

            if (CheckSessionForOtpCode(user, request.ipAddress) == false)
            {
                return OperationResult<LoginCommandResult>.Error("قبل از ورود باید شماره تماستون رو فعال کنید.");
            }

            string hashPassword = Sha256Hasher.Hash(request.password);

            if (!Sha256Hasher.IsCompare(user.Password, request.password))
                return OperationResult<LoginCommandResult>.Error("کاربری با این مشخصات یافت نشد.");

            if (user.UserRole == null)
            {
                var jwtRefreshService = _jwtServiceFactory.CreateSetting(TokenType.AuthRefreshToken);
                var jwtAuthService = _jwtServiceFactory.CreateSetting(TokenType.AuthToken);
                string refreshToken = null;
                string authToken = jwtAuthService.GenerateToken(
                        user.Id, user.PhoneNumber, new List<string> { "Guest" } ?? null);

                if (request.rememberMe == true)
                {

                    refreshToken = jwtRefreshService.GenerateToken(
                                           user.Id, user.PhoneNumber, new List<string> { "Guest" } ?? null);

                    user.SetUserSession(Sha256Hasher.Hash(refreshToken), request.ipAddress, DateTime.Now.AddDays(14));
                }
                else
                {
                    user.SetUserSession(Sha256Hasher.Hash(authToken), request.ipAddress, DateTime.Now.AddMinutes(30));
                }

                await _repository.SaveChangeAsync();

                return OperationResult<LoginCommandResult>.Success(new LoginCommandResult
                {
                    AuthToken = authToken,
                    RefreshToken = refreshToken ?? string.Empty,
                });
            }
            else
            {
                var role = await _roleRepository.GetByFilterAsync(i => i.Id.Equals(user.UserRole.RoleId)) ?? null;

                var jwtRefreshService = _jwtServiceFactory.CreateSetting(TokenType.AuthRefreshToken);
                var jwtAuthService = _jwtServiceFactory.CreateSetting(TokenType.AuthToken);
                string refreshToken = null;
                
                string authToken = jwtAuthService.GenerateToken(
                        user.Id, user.PhoneNumber, new List<string> { role.Title } ?? null);

                if (request.rememberMe == true)
                {

                    refreshToken = jwtRefreshService.GenerateToken(
                        user.Id, user.PhoneNumber, new List<string> { role.Title } ?? null);

                    user.SetUserSession(Sha256Hasher.Hash(refreshToken), request.ipAddress, DateTime.Now.AddDays(14));
                }
                else
                {
                    user.SetUserSession(Sha256Hasher.Hash(authToken), request.ipAddress, DateTime.Now.AddMinutes(30));
                }


                await _repository.SaveChangeAsync();


                return OperationResult<LoginCommandResult>.Success(new LoginCommandResult
                {
                    RefreshToken = refreshToken ?? string.Empty,
                    AuthToken = authToken,
                });
            }
        }
        private bool CheckSessionForOtpCode(Domain.UserAgg.User user, string ipAddress)
        {
            if (user == null)
                return false;

            var session = user.UserSessions.FirstOrDefault(i => i.ExpireDate > DateTime.Now && i.UserId.Equals(user.Id)
            && i.IpAddress == ipAddress && i.IsActive == true && ((i.ExpireDate.Minute - i.CreationDate.Minute) == 3));

            if (session is null)
                return false;

            session.ChangeActivity(false);
            return true;
        }
    }
}
