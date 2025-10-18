using Application.Shared.Abstractions.Jwt.Enum;
using Application.Shared.Abstractions.Jwt.Interfaces;
using Identity.Application.Shared.Abstractions.Jwt;
using Identity.Infrastructure.Auth.Jwt;

namespace Infrastructure.Auth.Jwt
{
    public class JwtSettingsFactory : IJwtSettingsFactory
    {

        public JwtSettingsFactory()
        {

        }

        public IJwtServcie CreateSetting(TokenType tokenType)
        {
            var setting = tokenType switch
            {
                TokenType.PhoneNumberConfirmedToken => new JwtSettings("Vestra", "Vestra-Identity-Api",
                "fjwgrwe8f94u8iojjkjkljklreoifw534hfow463{563}t2wte", TimeSpan.FromMinutes(30)),

                TokenType.AuthRefreshToken => new JwtSettings("Vestra", "Vestra-Identity-Api",
                "fjwgrwe8f94u8iojjkjkljklreowei{32rd563}t2wte", TimeSpan.FromDays(14)),

                TokenType.AuthToken => new JwtSettings("Vestra", "Vestra-Identity-Api",
                "wef23234jdhfjkhhwefuhuhkjefjwiojio{8ugferh76kl}few", TimeSpan.FromMinutes(30)),
                
                _ => new JwtSettings("Vestra", "Vestra-Identity-Api", "fjweoiffjnuweh87nw534hfow463{563}t2wte", TimeSpan.FromDays(3))
            };
            return new JwtService(setting);
        }
    }
}