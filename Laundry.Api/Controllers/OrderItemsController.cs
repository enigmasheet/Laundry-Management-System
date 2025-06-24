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
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly LaundryDbContext _context;
        private readonly IMapper _mapper;

        public OrderItemsController(LaundryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all order items.
        /// </summary>
        /// <returns>A list of all order items.</returns>
        [Authorize]
        [HttpGet]
        [SwaggerOperation(Summary = "Get all order items", Description = "Retrieves a list of all order items from the database.")]
        [SwaggerResponse(200, "Returns the list of order items")]
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetOrderItems()
        {
            var items = await _context.OrderItems.ToListAsync();
            var orderItemDTO = _mapper.Map<List<OrderItemDto>>(items);
            return Ok(orderItemDTO);
        }

        /// <summary>
        /// Get a specific order item by ID.
        /// </summary>
        /// <param name="id">The ID of the order item.</param>
        /// <returns>The requested order item.</returns>
        [Authorize]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get order item by ID", Description = "Retrieves a specific order item by its ID.")]
        [SwaggerResponse(200, "Returns the requested order item")]
        [SwaggerResponse(404, "Order item not found")]
        public async Task<ActionResult<OrderItemDto>> GetOrderItem(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);

            if (orderItem == null)
            {
                return NotFound();
            }
            var orderItemList = _mapper.Map<List<OrderItemDto>>(orderItem);

            return Ok(orderItemList);
        }

        /// <summary>
        /// Update an existing order item.
        /// </summary>
        /// <param name="id">The ID of the order item.</param>
        /// <param name="orderItemDto">The updated order item object.</param>
        [Authorize]
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an order item", Description = "Updates an existing order item by ID.")]
        [SwaggerResponse(204, "Order item updated successfully")]
        [SwaggerResponse(400, "ID mismatch or invalid input")]
        [SwaggerResponse(404, "Order item not found")]
        public async Task<IActionResult> PutOrderItem(int id, OrderItemDto orderItemDto)
        {
            if (id != orderItemDto.Id)
            {
                return BadRequest();
            }

            var OrderItemEntity = await _context.OrderItems.FindAsync(id);
            if (OrderItemEntity == null)
            {
                return NotFound();
            }

            // Map updated fields from DTO to entity
            _mapper.Map(orderItemDto, OrderItemEntity);

            _context.Entry(OrderItemEntity).State = EntityState.Modified;

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
        /// <param name="orderItemDto">The order item to create.</param>
        /// <returns>The newly created order item.</returns>
        [Authorize]
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new order item", Description = "Creates a new order item and returns it.")]
        [SwaggerResponse(201, "Order item created successfully")]
        public async Task<ActionResult<OrderItemDto>> PostOrderItem(OrderItemDto orderItemDto)
        {
            var entity = _mapper.Map<OrderItem>(orderItemDto);

            _context.OrderItems.Add(entity);
            await _context.SaveChangesAsync();
            var createdDto = _mapper.Map<OrderItemDto>(entity);

            return CreatedAtAction(nameof(GetOrderItem), new { id = entity.Id }, createdDto);

        }

        /// <summary>
        /// Delete an order item by ID.
        /// </summary>
        /// <param name="id">The ID of the order item to delete.</param>
        [Authorize]
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
