using Laundry.Api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Laundry.Shared.DTOs;
using Laundry.Api.Models;
using System.Threading.Tasks;

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
            

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto registerDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var normalizedEmail = registerDto.Email.Trim().ToLower();

            var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail 
                    || u.Phone == registerDto.PhoneNumber);


            if (existingUser != null)
            {
                if (existingUser.Email.ToLower() == registerDto.Email.ToLower())
                    return Conflict("A user with this email already exists.");
                else if (existingUser.Phone == registerDto.PhoneNumber)
                    return Conflict("A user with this phone number already exists.");
            }

            var user = new User
            {
                Email = registerDto.Email,
                FullName = registerDto.FullName,
                Phone = registerDto.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = "User", // Default role, can be changed later
                IsActive = true // Default active status

            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(); 
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

            return Ok(new RegisterResponse
            {
                Token = tokenString,
                ExpiresIn = expiresInMinutes * 60,
                User = new UserDto
                {
                    Email = user.Email,
                    Phone = user.Phone ?? "",
                    Role = user.Role,
                    VendorId = user.VendorId
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

 
}
