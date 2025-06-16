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
    public class ServicesController : ControllerBase
    {
        private readonly LaundryDbContext _context;

        public ServicesController(LaundryDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all available services.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Get all services", Description = "Retrieves a list of all laundry services.")]
        [SwaggerResponse(200, "List of services retrieved successfully.")]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
            return await _context.Services.ToListAsync();
        }

        /// <summary>
        /// Get a specific service by ID.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a service by ID", Description = "Retrieves a specific service by its ID.")]
        [SwaggerResponse(200, "Service retrieved successfully.")]
        [SwaggerResponse(404, "Service not found.")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }

        /// <summary>
        /// Update an existing service.
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a service", Description = "Updates an existing service with the specified ID.")]
        [SwaggerResponse(204, "Service updated successfully.")]
        [SwaggerResponse(400, "Invalid request. ID mismatch.")]
        [SwaggerResponse(404, "Service not found.")]
        public async Task<IActionResult> PutService(int id, Service service)
        {
            if (id != service.Id)
            {
                return BadRequest();
            }

            _context.Entry(service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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
        /// Create a new service.
        /// </summary>
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new service", Description = "Adds a new laundry service to the system.")]
        [SwaggerResponse(201, "Service created successfully.")]
        public async Task<ActionResult<Service>> PostService(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetService), new { id = service.Id }, service);
        }

        /// <summary>
        /// Delete a service by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a service", Description = "Removes the service with the specified ID.")]
        [SwaggerResponse(204, "Service deleted successfully.")]
        [SwaggerResponse(404, "Service not found.")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Checks if a service exists by ID.
        /// </summary>
        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
