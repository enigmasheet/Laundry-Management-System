using Laundry.Api.Data;
using Laundry.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Laundry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly LaundryDbContext _context;

        public OrdersController(LaundryDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all orders.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Get all orders", Description = "Returns a list of all orders in the system.")]
        [SwaggerResponse(200, "Successfully retrieved list of orders.")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        /// <summary>
        /// Get a specific order by ID.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get an order by ID", Description = "Returns a specific order by its ID.")]
        [SwaggerResponse(200, "Successfully retrieved the order.")]
        [SwaggerResponse(404, "Order not found.")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        /// <summary>
        /// Update an existing order by ID.
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an order", Description = "Updates the order with the given ID.")]
        [SwaggerResponse(204, "Order updated successfully.")]
        [SwaggerResponse(400, "Invalid request (ID mismatch).")]
        [SwaggerResponse(404, "Order not found.")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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
        /// Create a new order.
        /// </summary>
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new order", Description = "Adds a new order to the system.")]
        [SwaggerResponse(201, "Order created successfully.")]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        /// <summary>
        /// Delete an existing order by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete an order", Description = "Removes the order with the given ID.")]
        [SwaggerResponse(204, "Order deleted successfully.")]
        [SwaggerResponse(404, "Order not found.")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
