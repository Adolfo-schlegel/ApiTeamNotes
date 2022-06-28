using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeamsNotesApi.Models.Common;
using TeamsNotesApi.Models.UserLogin;
using TeamsNotesApi.Tools.Interface;

namespace TeamsNotesApi.Tools
{
    public class JwtAuth : IJwtAuth
    {
        private readonly AppSettings? _appSettings;
        public JwtAuth(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public string GetNewToken(User User)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity
                (
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, User.id.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, User.user.ToString()),
                    }
                ),

                Expires = DateTime.UtcNow.AddDays(365),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave),SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
