using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Zappr.Api.Helpers
{
    public class TokenHelper
    {
        public IConfiguration Configuration { get; set; }
        private readonly string _secret;

        public TokenHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _secret = Configuration.GetSection("JWT")["secret"];
        }


        public string GenerateToken(int userId)
        {
            SymmetricSecurityKey mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secret));

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                //TODO do this based on userId
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", userId.ToString()),
                    new Claim("Role", "User"),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public bool ValidateCurrentToken(string token)
        {
            SymmetricSecurityKey mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secret));

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = mySecurityKey
                }, out var validatedToken);
            }
            catch { return false; }
            return true;
        }

        public string GetClaim(string token, string claimType)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            string stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
            return stringClaimValue;
        }
    }
}
