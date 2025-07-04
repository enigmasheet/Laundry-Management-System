using AutoMapper;
using Laundry.Api.Data;
using Laundry.Api.Models;
using Laundry.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;


namespace Laundry.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly LaundryDbContext _context;
        private readonly IMapper _mapper;

        public ServicesController(LaundryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all services for a specific vendor.
        /// </summary>
        [HttpGet("vendor/{vendorId}")]
        [SwaggerOperation(Summary = "Get services by vendor ID", Description = "Retrieves all services for a given vendor ID.")]
        [SwaggerResponse(200, "List of services retrieved successfully.")]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServicesByVendorId(int vendorId)
        {
            var services = await _context.Services
                                         .Where(s => s.VendorId == vendorId)
                                         .Include(s => s.Reviews)
                                         .ToListAsync();

            var serviceDtos = _mapper.Map<List<ServiceDto>>(services);
            return Ok(serviceDtos);
        }

        /// <summary>
        /// Get all available services.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Get all services", Description = "Retrieves a list of all laundry services.")]
        [SwaggerResponse(200, "List of services retrieved successfully.")]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServices()
        {
            var services = await _context.Services.Include(s => s.Reviews).ToListAsync();
            var serviceDtos = _mapper.Map<List<ServiceDto>>(services);
            return Ok(serviceDtos);
        }

        /// <summary>
        /// Get a specific service by ID.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a service by ID", Description = "Retrieves a specific service by its ID.")]
        [SwaggerResponse(200, "Service retrieved successfully.")]
        [SwaggerResponse(404, "Service not found.")]
        public async Task<ActionResult<ServiceDto>> GetService(int id)
        {
            var service = await _context.Services
                                        .Include(s => s.Reviews)
                                        .FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
                return NotFound();

            var serviceDto = _mapper.Map<ServiceDto>(service);
            return Ok(serviceDto);
        }

        /// <summary>
        /// Update an existing service.
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a service", Description = "Updates an existing service with the specified ID.")]
        [SwaggerResponse(204, "Service updated successfully.")]
        [SwaggerResponse(400, "Invalid request. ID mismatch.")]
        [SwaggerResponse(404, "Service not found.")]
        public async Task<IActionResult> PutService(int id, ServiceDto serviceDto)
        {
            if (id != serviceDto.Id)
                return BadRequest();

            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return NotFound();

            // Map updated values from DTO to entity
            _mapper.Map(serviceDto, service);

            _context.Entry(service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Create a new service.
        /// </summary>
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new service", Description = "Adds a new laundry service to the system.")]
        [SwaggerResponse(201, "Service created successfully.")]
        public async Task<ActionResult<ServiceDto>> PostService(ServiceDto serviceDto)
        {
            var service = _mapper.Map<Service>(serviceDto);

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            var createdDto = _mapper.Map<ServiceDto>(service);

            return CreatedAtAction(nameof(GetService), new { id = service.Id }, createdDto);
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
                return NotFound();

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
