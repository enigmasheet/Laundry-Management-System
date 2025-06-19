using Laundry.Api.Data;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Laundry.Api.Service
{
    public class JwtTokenService
    {
        private readonly LaundryDbContext _context;
        private readonly IConfiguration _configuration;

        public JwtTokenService(LaundryDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        public string GenerateToken(string userId, string userName)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = _configuration["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Create claims (you can add more)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId), // Subject (user id)
                new Claim(JwtRegisteredClaimNames.UniqueName, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // JWT ID
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Token valid for 1 hour
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
