using Laundry.Api.Data;
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
    public class AuthenticationController : ControllerBase
    {
        private readonly LaundryDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationController(LaundryDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
                return BadRequest("Email and password required.");

            // Find user by email
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
                return Unauthorized("Invalid credentials.");

            // TODO: Replace this with your password hash verification
            if (user.PasswordHash != loginDto.Password)
                return Unauthorized("Invalid credentials.");

            // Create claims
            // Create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),    // Unique user ID
                new Claim(ClaimTypes.Name, user.FullName),                       // Full name for display
                new Claim(ClaimTypes.Email, user.Email),                         // Email for identification
                new Claim(ClaimTypes.Role, user.Role),                           // Role (Customer, VendorAdmin, etc.)
                new Claim("isActive", user.IsActive.ToString())                  // Account status as claim (optional)
            };

            // Add optional claims conditionally
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

            // Remove any nulls caused by optional claims
            claims = claims.Where(c => c != null).ToList();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpiresInMinutes"] ?? "60"));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // Set JWT as HttpOnly cookie
            Response.Cookies.Append("AuthToken", tokenString, new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = true,
                Secure = false, // Set true in production (HTTPS), false for local dev
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
                Expires = expires
            });

            return Ok(new { message = "Login successful" });
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


            return Ok(new { userId, email,fullName, role, isActive, phone, address, vendorId , latitude, longitude });
        }
    }

    public class LoginDto
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
