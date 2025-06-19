﻿using Laundry.Api.Data;
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
    public class VendorsController : ControllerBase
    {
        private readonly LaundryDbContext _context;

        public VendorsController(LaundryDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all vendors.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Get all vendors", Description = "Retrieves a list of all vendors.")]
        [SwaggerResponse(200, "List of vendors retrieved successfully.")]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendors()
        {
            return await _context.Vendors.ToListAsync();
        }

        /// <summary>
        /// Get a vendor by ID.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get vendor by ID", Description = "Retrieves a specific vendor by their ID.")]
        [SwaggerResponse(200, "Vendor retrieved successfully.")]
        [SwaggerResponse(404, "Vendor not found.")]
        public async Task<ActionResult<Vendor>> GetVendor(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);

            if (vendor == null)
            {
                return NotFound();
            }

            return vendor;
        }

        /// <summary>
        /// Update an existing vendor.
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a vendor", Description = "Updates the vendor details for the specified ID.")]
        [SwaggerResponse(204, "Vendor updated successfully.")]
        [SwaggerResponse(400, "Invalid request. ID mismatch.")]
        [SwaggerResponse(404, "Vendor not found.")]
        public async Task<IActionResult> PutVendor(int id, Vendor vendor)
        {
            if (id != vendor.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
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
        /// Create a new vendor.
        /// </summary>
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new vendor", Description = "Adds a new vendor to the system.")]
        [SwaggerResponse(201, "Vendor created successfully.")]
        public async Task<ActionResult<Vendor>> PostVendor(Vendor vendor)
        {
            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVendor), new { id = vendor.Id }, vendor);
        }

        /// <summary>
        /// Delete a vendor by ID.
        /// </summary>
        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a vendor", Description = "Deletes the vendor with the specified ID.")]
        [SwaggerResponse(204, "Vendor deleted successfully.")]
        [SwaggerResponse(404, "Vendor not found.")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }

            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.Id == id);
        }
    }
}
