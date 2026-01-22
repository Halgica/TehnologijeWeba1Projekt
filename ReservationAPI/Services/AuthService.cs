using DAL.DB;
using DAL.Models;
using ReservationAPI.Settings;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

using Microsoft.IdentityModel.Tokens;

namespace ReservationAPI.Services
{
    public class AuthService
    {
        private readonly IConfiguration _config;
        private readonly TW1DbContext _dbContext;

        public AuthService(IConfiguration config, TW1DbContext context)
        {
            _config = config;
            _dbContext = context;
        }

        public string GenerateJwtToken(AuthUser user)
        {
            var jwtSettings = _config.GetSection("JwtSettings").Get<JwtSettings>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.ExpiryMinutes),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GenerateRefreshToken(AuthUser user)
        {
            var refreshToken = Guid.NewGuid().ToString();
            user.RefreshToken = refreshToken;

            _dbContext.Update(user);
            _dbContext.SaveChanges();

            return refreshToken;
        }
    }
}
