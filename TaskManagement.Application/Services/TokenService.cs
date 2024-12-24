using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TaskManagement.Application.DTOs.AppSettingsDtos;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettingsDto _jwtSettingsDto;

        public TokenService(JwtSettingsDto jwtSettingsDto)
        {
            _jwtSettingsDto = jwtSettingsDto;
        }

        public string GenerateJwtToken(Guid ownerId, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettingsDto.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, ownerId.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettingsDto.ValidIssuer,
                audience: _jwtSettingsDto.ValidAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_jwtSettingsDto.TokenExpiryInHours),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
