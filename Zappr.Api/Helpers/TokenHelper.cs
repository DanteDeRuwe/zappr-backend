using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Zappr.Core.Interfaces;

namespace Zappr.Api.Helpers
{
    public class TokenHelper
    {
        public IConfiguration Configuration { get; set; }
        private readonly string _secret;
        private readonly IUserRepository _userRepository;

        public TokenHelper(IConfiguration configuration, IUserRepository userRepository)
        {
            Configuration = configuration;
            _secret = Configuration.GetSection("JWT")["secret"];
            _userRepository = userRepository;
        }


        public string GenerateToken(int userId)
        {
            SymmetricSecurityKey mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secret));

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            var user = _userRepository.GetById(userId);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", userId.ToString()),
                    new Claim("Role", user.Role),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
