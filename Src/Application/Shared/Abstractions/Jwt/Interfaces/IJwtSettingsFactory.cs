using Application.Shared.Abstractions.Jwt.Enum;

namespace Application.Shared.Abstractions.Jwt.Interfaces
{
    public interface IJwtSettingsFactory
    {
        IJwtServcie CreateSetting(TokenType tokenType);
    }
}
