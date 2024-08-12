using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using IdentityModel;
using Microsoft.Extensions.Options;
using AppCore.Infrastructure.Common;
using AppCore.Infrastructure.Common.Constants;
using AppCore.Identity.API.Models.Requests;
using AppCore.Identity.API.Models.Responses;
using AppCore.Infrastructure.Extensions;


namespace AppCore.Identity.API.Services
{
    public class TokenService
    {
        private readonly IOptions<AppSetting> _config;
        public TokenService(IOptions<AppSetting> config)
        {
            _config = config;
        }
        public CreateTokenResponse CreateToken(CreateTokenRequest request)
        {
            CreateTokenResponse createTokenResponse = new CreateTokenResponse();
            if (request != null)
            {

                var claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Subject, request.User.Id.ToString()),
                    new Claim(JwtClaimTypes.Id, request.User.Id.ToString()),
                    new Claim(JwtClaimTypes.Email, request.User.Email),
                    new Claim(JwtClaimTypes.PreferredUserName, request.User.Email),
                    new Claim(JwtClaimTypes.TokenType, "Bearer"),
                    new Claim(CoreConstant.CLAIM_AVATAR, request.User.Avatar.GetValueOrDefault("")),
                    new Claim(CoreConstant.CLAIM_LASTNAME, request.User.LastName.GetValueOrDefault("")),
                    new Claim(CoreConstant.CLAIM_FIRSTNAME, request.User.FirstName.GetValueOrDefault("")),
                };
                if (request.Permissions != null && request.Permissions.Count>0)
                {
                    foreach (string permission in request.Permissions)
                    {
                        claims.Add(new Claim(CoreConstant.CLAIM_PERMISSIONS, permission));
                    }
                }
                else
                {
                    claims.Add(new Claim(CoreConstant.CLAIM_PERMISSIONS, string.Empty));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Value.GetJWTSecret64Symbol));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                createTokenResponse.IssuedAt = DateTime.UtcNow;
                createTokenResponse.ExpirationTime = DateTime.UtcNow.AddMinutes(_config.Value.GetTokenLifetimeMinutes);
                if (request.IsRememberMe)
                {
                    createTokenResponse.ExpirationTime = DateTime.UtcNow.AddDays(_config.Value.GetPermanentTokenLifetimeDays);
                }
                if (request.IsUpdateTokenDetail)
                {
                    createTokenResponse.IssuedAt = request.IssuedAt;
                    createTokenResponse.ExpirationTime = request.ExpirationTime;
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = createTokenResponse.ExpirationTime,
                    IssuedAt = createTokenResponse.IssuedAt,
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);

                createTokenResponse.Token = tokenHandler.WriteToken(token);
            }
            return createTokenResponse;
        }
    }
}