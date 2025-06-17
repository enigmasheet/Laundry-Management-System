using Laundry.Api.Data;
using Laundry.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Laundry.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LaundryDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(LaundryDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
                return BadRequest("Email and password are required.");

            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Email.ToLower() == loginDto.Email.ToLower());

            if (user == null || user.PasswordHash != loginDto.Password)
                return Unauthorized("Invalid credentials.");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.FullName ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role ?? string.Empty),
                new Claim("isActive", user.IsActive.ToString())
            };

            if (!string.IsNullOrEmpty(user.Phone))
                claims.Add(new Claim("phone", user.Phone));
            if (!string.IsNullOrEmpty(user.Address))
                claims.Add(new Claim("address", user.Address));
            if (user.VendorId.HasValue)
                claims.Add(new Claim("vendorId", user.VendorId.Value.ToString()));
            if (user.Latitude.HasValue)
                claims.Add(new Claim("latitude", user.Latitude.Value.ToString()));
            if (user.Longitude.HasValue)
                claims.Add(new Claim("longitude", user.Longitude.Value.ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresInMinutes = double.TryParse(_configuration["Jwt:ExpiresInMinutes"], out var parsedMinutes) ? parsedMinutes : 60;
            var expires = DateTime.UtcNow.AddMinutes(expiresInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            Response.Cookies.Append("AuthToken", tokenString, new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = true,
                Secure = HttpContext.Request.IsHttps,
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
                Expires = expires
            });

            return Ok(new
            {
                token = tokenString,
                expiresIn = expiresInMinutes * 60,
                user = new
                {
                    user.UserId,
                    user.FullName,
                    user.Email,
                    user.Role
                }
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");
            return Ok(new { message = "Logged out successfully" });
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = User.FindFirstValue(ClaimTypes.Email) ?? "Unknown";
            var fullName = User.FindFirstValue(ClaimTypes.Name) ?? "Unknown";
            var role = User.FindFirstValue(ClaimTypes.Role) ?? "Unknown";
            var isActive = User.FindFirstValue("isActive") == "True";
            var phone = User.FindFirstValue("phone") ?? "Not provided";
            var address = User.FindFirstValue("address") ?? "Not provided";
            var vendorId = User.FindFirstValue("vendorId");
            var latitude = User.FindFirstValue("latitude");
            var longitude = User.FindFirstValue("longitude");

            return Ok(new
            {
                userId,
                email,
                fullName,
                role,
                isActive,
                phone,
                address,
                vendorId,
                latitude,
                longitude
            });
        }
    }

    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
