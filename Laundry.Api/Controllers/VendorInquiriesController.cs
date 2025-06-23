using AutoMapper;
using Laundry.Api.Data;
using Laundry.Api.Models;
using Laundry.Shared.DTOs; // Make sure your DTO namespace is included
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
        private readonly IMapper _mapper;

        public VendorInquiriesController(LaundryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all vendor inquiries.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Get all vendor inquiries", Description = "Retrieve a list of all vendor inquiries.")]
        [SwaggerResponse(200, "List of vendor inquiries retrieved successfully.")]
        public async Task<ActionResult<IEnumerable<VendorInquiryDto>>> GetVendorInquiries()
        {
            var inquiries = await _context.VendorInquiries
                .Include(v => v.Vendor)   // Include related vendor info if needed
                .Include(c => c.Customer) // Include related customer info if needed
                .ToListAsync();

            var dtoList = _mapper.Map<List<VendorInquiryDto>>(inquiries);
            return Ok(dtoList);
        }

        /// <summary>
        /// Retrieves a vendor inquiry by ID.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get vendor inquiry by ID", Description = "Retrieve a specific vendor inquiry by its ID.")]
        [SwaggerResponse(200, "Vendor inquiry retrieved successfully.")]
        [SwaggerResponse(404, "Vendor inquiry not found.")]
        public async Task<ActionResult<VendorInquiryDto>> GetVendorInquiry(int id)
        {
            var inquiry = await _context.VendorInquiries
                .Include(v => v.Vendor)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inquiry == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<VendorInquiryDto>(inquiry);
            return Ok(dto);
        }

        /// <summary>
        /// Updates a vendor inquiry by ID.
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update vendor inquiry", Description = "Update an existing vendor inquiry with the specified ID.")]
        [SwaggerResponse(204, "Vendor inquiry updated successfully.")]
        [SwaggerResponse(400, "Invalid request - ID mismatch.")]
        [SwaggerResponse(404, "Vendor inquiry not found.")]
        public async Task<IActionResult> PutVendorInquiry(int id, VendorInquiryDto vendorInquiryDto)
        {
            if (id != vendorInquiryDto.Id)
            {
                return BadRequest("ID in URL and body do not match.");
            }

            var inquiryEntity = await _context.VendorInquiries.FindAsync(id);
            if (inquiryEntity == null)
            {
                return NotFound();
            }

            // Map updated fields from DTO to entity
            _mapper.Map(vendorInquiryDto, inquiryEntity);

            _context.Entry(inquiryEntity).State = EntityState.Modified;

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
        public async Task<ActionResult<VendorInquiryDto>> PostVendorInquiry(VendorInquiryDto vendorInquiryDto)
        {
            var entity = _mapper.Map<VendorInquiry>(vendorInquiryDto);
            _context.VendorInquiries.Add(entity);
            await _context.SaveChangesAsync();

            var createdDto = _mapper.Map<VendorInquiryDto>(entity);

            return CreatedAtAction(nameof(GetVendorInquiry), new { id = entity.Id }, createdDto);
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
