using AutoMapper;
using Laundry.Api.Data;
using Laundry.Api.Models;
using Laundry.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Laundry.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly LaundryDbContext _context; 
        private readonly IMapper _mapper;

        public UsersController(LaundryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET api/users/profile
        [HttpGet("Profile")]
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var user = await _context.Users
                .Include(u => u.Vendor)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId.ToString() == userId);

            if (user == null)
                return NotFound();

            var dto = _mapper.Map<UserDto>(user);
            return Ok(dto);
        }

        // PUT api/users/profile
        [HttpPut("Profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var user = await _context.Users
                .Include(u => u.Vendor)
                .FirstOrDefaultAsync(u => u.UserId.ToString() == userId);

            if (user == null)
                return NotFound();

            // Map updated fields from DTO to entity
            _mapper.Map(dto, user);

            await _context.SaveChangesAsync();

            var updatedDto = _mapper.Map<UserDto>(user);
            return Ok(updatedDto);
        }

        // GET api/users
        [HttpGet]
        [SwaggerOperation(Summary = "Get all users", Description = "Retrieves a list of all users.")]
        [SwaggerResponse(200, "List of users retrieved successfully.")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _context.Users
                .Include(u => u.Vendor)
                .AsNoTracking()
                .ToListAsync();

            var userDtos = _mapper.Map<List<UserDto>>(users);
            return Ok(userDtos);
        }

        // GET api/users/{id}
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get user by ID", Description = "Retrieves a specific user by their ID.")]
        [SwaggerResponse(200, "User retrieved successfully.")]
        [SwaggerResponse(404, "User not found.")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            var user = await _context.Users
                .Include(u => u.Vendor)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
                return NotFound();

            var dto = _mapper.Map<UserDto>(user);
            return Ok(dto);
        }

        // PUT api/users/{id}
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a user", Description = "Updates the user details for the specified ID.")]
        [SwaggerResponse(204, "User updated successfully.")]
        [SwaggerResponse(400, "Invalid request. ID mismatch.")]
        [SwaggerResponse(404, "User not found.")]
        public async Task<IActionResult> PutUser(Guid id, [FromBody] UserDto dto)
        {
            if (id != dto.UserId)
                return BadRequest("ID mismatch");

            var user = await _context.Users
                .Include(u => u.Vendor)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
                return NotFound();

            _mapper.Map(dto, user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST api/users
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new user", Description = "Adds a new user to the system.")]
        [SwaggerResponse(201, "User created successfully.")]
        public async Task<ActionResult<UserDto>> PostUser([FromBody] UserDto dto)
        {
            var user = _mapper.Map<User>(dto);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var createdDto = _mapper.Map<UserDto>(user);

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, createdDto);
        }

        // DELETE api/users/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a user", Description = "Deletes the user with the specified ID.")]
        [SwaggerResponse(204, "User deleted successfully.")]
        [SwaggerResponse(404, "User not found.")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
