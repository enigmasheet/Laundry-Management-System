using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Laundry.Api.Data;
using Laundry.Api.Models;

namespace Laundry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorInquiriesController : ControllerBase
    {
        private readonly LaundryDbContext _context;

        public VendorInquiriesController(LaundryDbContext context)
        {
            _context = context;
        }

        // GET: api/VendorInquiries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendorInquiry>>> GetVendorInquiries()
        {
            return await _context.VendorInquiries.ToListAsync();
        }

        // GET: api/VendorInquiries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VendorInquiry>> GetVendorInquiry(int id)
        {
            var vendorInquiry = await _context.VendorInquiries.FindAsync(id);

            if (vendorInquiry == null)
            {
                return NotFound();
            }

            return vendorInquiry;
        }

        // PUT: api/VendorInquiries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
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

        // POST: api/VendorInquiries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VendorInquiry>> PostVendorInquiry(VendorInquiry vendorInquiry)
        {
            _context.VendorInquiries.Add(vendorInquiry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendorInquiry", new { id = vendorInquiry.Id }, vendorInquiry);
        }

        // DELETE: api/VendorInquiries/5
        [HttpDelete("{id}")]
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
