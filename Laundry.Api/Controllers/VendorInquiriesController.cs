using Laundry.Api.Data;
using Laundry.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laundry.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VendorInquiriesController : ControllerBase
    {
        private readonly LaundryDbContext _context;

        public VendorInquiriesController(LaundryDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all vendor inquiries.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Get all vendor inquiries", Description = "Retrieve a list of all vendor inquiries.")]
        [SwaggerResponse(200, "List of vendor inquiries retrieved successfully.")]
        public async Task<ActionResult<IEnumerable<VendorInquiry>>> GetVendorInquiries()
        {
            return await _context.VendorInquiries.ToListAsync();
        }

        /// <summary>
        /// Retrieves a vendor inquiry by ID.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get vendor inquiry by ID", Description = "Retrieve a specific vendor inquiry by its ID.")]
        [SwaggerResponse(200, "Vendor inquiry retrieved successfully.")]
        [SwaggerResponse(404, "Vendor inquiry not found.")]
        public async Task<ActionResult<VendorInquiry>> GetVendorInquiry(int id)
        {
            var vendorInquiry = await _context.VendorInquiries.FindAsync(id);

            if (vendorInquiry == null)
            {
                return NotFound();
            }

            return vendorInquiry;
        }

        /// <summary>
        /// Updates a vendor inquiry by ID.
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update vendor inquiry", Description = "Update an existing vendor inquiry with the specified ID.")]
        [SwaggerResponse(204, "Vendor inquiry updated successfully.")]
        [SwaggerResponse(400, "Invalid request - ID mismatch.")]
        [SwaggerResponse(404, "Vendor inquiry not found.")]
        public async Task<IActionResult> PutVendorInquiry(int id, VendorInquiry vendorInquiry)
        {
            if (id != vendorInquiry.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendorInquiry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorInquiryExists(id))
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
        /// Creates a new vendor inquiry.
        /// </summary>
        [HttpPost]
        [SwaggerOperation(Summary = "Create vendor inquiry", Description = "Add a new vendor inquiry.")]
        [SwaggerResponse(201, "Vendor inquiry created successfully.")]
        public async Task<ActionResult<VendorInquiry>> PostVendorInquiry(VendorInquiry vendorInquiry)
        {
            _context.VendorInquiries.Add(vendorInquiry);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVendorInquiry), new { id = vendorInquiry.Id }, vendorInquiry);
        }

        /// <summary>
        /// Deletes a vendor inquiry by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete vendor inquiry", Description = "Delete the vendor inquiry with the specified ID.")]
        [SwaggerResponse(204, "Vendor inquiry deleted successfully.")]
        [SwaggerResponse(404, "Vendor inquiry not found.")]
        public async Task<IActionResult> DeleteVendorInquiry(int id)
        {
            var vendorInquiry = await _context.VendorInquiries.FindAsync(id);
            if (vendorInquiry == null)
            {
                return NotFound();
            }

            _context.VendorInquiries.Remove(vendorInquiry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendorInquiryExists(int id)
        {
            return _context.VendorInquiries.Any(e => e.Id == id);
        }
    }
}
