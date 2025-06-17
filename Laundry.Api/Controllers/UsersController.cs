using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Laundry.Api.Data;
using Laundry.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laundry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly LaundryDbContext _context;

        public UsersController(LaundryDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Get all users", Description = "Retrieves a list of all users.")]
        [SwaggerResponse(200, "List of users retrieved successfully.")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// Get a user by ID.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get user by ID", Description = "Retrieves a specific user by their ID.")]
        [SwaggerResponse(200, "User retrieved successfully.")]
        [SwaggerResponse(404, "User not found.")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        /// <summary>
        /// Update an existing user.
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a user", Description = "Updates the user details for the specified ID.")]
        [SwaggerResponse(204, "User updated successfully.")]
        [SwaggerResponse(400, "Invalid request. ID mismatch.")]
        [SwaggerResponse(404, "User not found.")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new user", Description = "Adds a new user to the system.")]
        [SwaggerResponse(201, "User created successfully.")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        /// <summary>
        /// Delete a user by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a user", Description = "Deletes the user with the specified ID.")]
        [SwaggerResponse(204, "User deleted successfully.")]
        [SwaggerResponse(404, "User not found.")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

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
