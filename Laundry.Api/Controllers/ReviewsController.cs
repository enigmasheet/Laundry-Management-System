using Laundry.Api.Data;
using Laundry.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Laundry.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly LaundryDbContext _context;

        public ReviewsController(LaundryDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all reviews.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Get all reviews", Description = "Retrieves a list of all customer reviews with related data.")]
        [SwaggerResponse(200, "List of reviews retrieved successfully.")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            var reviews = await _context.Reviews
                .Include(r => r.Customer)
                .Include(r => r.Vendor)
                .Include(r => r.Service)
                .ToListAsync();

            return Ok(reviews);
        }


        /// <summary>
        /// Get a review by ID.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a review by ID", Description = "Retrieves a specific review by its unique ID.")]
        [SwaggerResponse(200, "Review retrieved successfully.")]
        [SwaggerResponse(404, "Review not found.")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        /// <summary>
        /// Update an existing review.
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a review", Description = "Updates the review with the specified ID.")]
        [SwaggerResponse(204, "Review updated successfully.")]
        [SwaggerResponse(400, "Bad request. The ID does not match.")]
        [SwaggerResponse(404, "Review not found.")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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
        /// Create a new review.
        /// </summary>
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new review", Description = "Adds a new review to the system.")]
        [SwaggerResponse(201, "Review created successfully.")]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdStr == null)
                return Unauthorized();

            review.CustomerId = Guid.Parse(userIdStr);
            review.CreatedAt = DateTime.UtcNow;

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReview", new { id = review.Id }, review);
        }


        /// <summary>
        /// Delete a review by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a review", Description = "Deletes the review with the specified ID.")]
        [SwaggerResponse(204, "Review deleted successfully.")]
        [SwaggerResponse(404, "Review not found.")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Check if a review exists.
        /// </summary>
        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
