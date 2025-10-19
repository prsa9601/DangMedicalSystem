using Application.Shared.Abstractions.Jwt.Enum;
using Application.Shared.Abstractions.Jwt.Interfaces;
using Common.Application;
using Common.Application.SecurityUtil;
using Domain.RoleAgg.Interfaces.Repository;
using Domain.UserAgg.Interfaces.Repository;
using Microsoft.AspNetCore.Http;

namespace Application.Auth.Commands.Login
{
    public class UserLoginCommand : IBaseCommand<string>
    {
        public required string phoneNumber { get; set; }
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
            var user = await _repository.GetUserByFilter(i => i.PhoneNumber.Equals(request.phoneNumber));
            if (user == null) return OperationResult<string>.NotFound("کاربری با این مشخصات یافت نشد.");

            string hashPassword = Sha256Hasher.Hash(request.password);

            if (!Sha256Hasher.IsCompare(user.Password, request.password))
                return OperationResult<string>.Error("کاربری با این مشخصات یافت نشد.");

            var role = await _roleRepository.GetByFilterAsync(i => i.Id.Equals(user.UserRole.RoleId));

            var jwtRefreshService = _jwtServiceFactory.CreateSetting(TokenType.AuthRefreshToken);
            var jwtAuthService = _jwtServiceFactory.CreateSetting(TokenType.AuthToken);

            string refreshToken = jwtRefreshService.GenerateToken(
                user.Id, user.PhoneNumber, new List<string> { role.Title } ?? null);

            string authToken = jwtAuthService.GenerateToken(
                user.Id, user.PhoneNumber, new List<string> { role.Title } ?? null);


            user.SetUserSession(refreshToken, request.ipAddress,DateTime.Now.AddDays(14));

            await _repository.SaveChangeAsync();

            return OperationResult<string>.Success(authToken);
        }
    }
}
