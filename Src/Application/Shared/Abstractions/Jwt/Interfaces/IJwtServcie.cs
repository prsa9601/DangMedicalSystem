using System.Security.Claims;

namespace Application.Shared.Abstractions.Jwt.Interfaces
{
    public interface IJwtServcie
    {
        string GenerateToken(Guid? userId, string phonNumber, List<string>? roles);
        ClaimsPrincipal ValidateToken(string token);
        DateTime GetExpireDate(string token);
    }
}
