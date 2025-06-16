using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Laundry.Api.Data;
using Laundry.Api.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Laundry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly LaundryDbContext _context;

        public OrderItemsController(LaundryDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all order items.
        /// </summary>
        /// <returns>A list of all order items.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Get all order items", Description = "Retrieves a list of all order items from the database.")]
        [SwaggerResponse(200, "Returns the list of order items")]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItems()
        {
            return await _context.OrderItems.ToListAsync();
        }

        /// <summary>
        /// Get a specific order item by ID.
        /// </summary>
        /// <param name="id">The ID of the order item.</param>
        /// <returns>The requested order item.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get order item by ID", Description = "Retrieves a specific order item by its ID.")]
        [SwaggerResponse(200, "Returns the requested order item")]
        [SwaggerResponse(404, "Order item not found")]
        public async Task<ActionResult<OrderItem>> GetOrderItem(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            return orderItem;
        }

        /// <summary>
        /// Update an existing order item.
        /// </summary>
        /// <param name="id">The ID of the order item.</param>
        /// <param name="orderItem">The updated order item object.</param>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an order item", Description = "Updates an existing order item by ID.")]
        [SwaggerResponse(204, "Order item updated successfully")]
        [SwaggerResponse(400, "ID mismatch or invalid input")]
        [SwaggerResponse(404, "Order item not found")]
        public async Task<IActionResult> PutOrderItem(int id, OrderItem orderItem)
        {
            if (id != orderItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemExists(id))
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
        /// Create a new order item.
        /// </summary>
        /// <param name="orderItem">The order item to create.</param>
        /// <returns>The newly created order item.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new order item", Description = "Creates a new order item and returns it.")]
        [SwaggerResponse(201, "Order item created successfully")]
        public async Task<ActionResult<OrderItem>> PostOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderItem", new { id = orderItem.Id }, orderItem);
        }

        /// <summary>
        /// Delete an order item by ID.
        /// </summary>
        /// <param name="id">The ID of the order item to delete.</param>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete an order item", Description = "Deletes an existing order item by ID.")]
        [SwaggerResponse(204, "Order item deleted successfully")]
        [SwaggerResponse(404, "Order item not found")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderItemExists(int id)
        {
            return _context.OrderItems.Any(e => e.Id == id);
        }
    }
}
