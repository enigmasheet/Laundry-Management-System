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
    public class OrdersController : ControllerBase
    {
        private readonly LaundryDbContext _context;
        private readonly IMapper _mapper;

        public OrdersController(LaundryDbContext context, IMapper mapper)
        {
            _context = context; 
            _mapper = mapper;

        }

        /// <summary>
        /// Get all orders.
        /// </summary>
        [Authorize]
        [HttpGet]
        [SwaggerOperation(Summary = "Get all orders", Description = "Returns a list of all orders in the system.")]
        [SwaggerResponse(200, "Successfully retrieved list of orders.")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            var orderDto = _mapper.Map<List<OrderDto>>(orders);
            return Ok(orderDto);
        }

        /// <summary>
        /// Get a specific order by ID.
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get an order by ID", Description = "Returns a specific order by its ID.")]
        [SwaggerResponse(200, "Successfully retrieved the order.")]
        [SwaggerResponse(404, "Order not found.")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var orderAsync = await _context.Orders.FindAsync(id);//search by orderCode(future)

            if (orderAsync == null)
            {
                return NotFound();
            }
            var order = _mapper.Map<List<OrderDto>>(orderAsync);
            return Ok(order);
        }

        /// <summary>
        /// Update an existing order by ID.
        /// </summary>
        [Authorize]
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an order", Description = "Updates the order with the given ID.")]
        [SwaggerResponse(204, "Order updated successfully.")]
        [SwaggerResponse(400, "Invalid request (ID mismatch).")]
        [SwaggerResponse(404, "Order not found.")]
        public async Task<IActionResult> PutOrder(int id, OrderDto orderDto)
        {
            if (id != orderDto.Id)
            {
                return BadRequest();
            }

            var orderEntity = await _context.Orders.FindAsync(id);

            if (orderEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(orderDto, orderEntity);
            _context.Entry(orderEntity).State = EntityState.Modified; 

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
        [Authorize]
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new order", Description = "Adds a new order to the system.")]
        [SwaggerResponse(201, "Order created successfully.")]
        public async Task<ActionResult<OrderDto>> PostOrder(OrderDto orderDto)
        {
            var entity = _mapper.Map<Order>(orderDto);

            _context.Orders.Add(entity);
            await _context.SaveChangesAsync();
            var createdDto = _mapper.Map<OrderDto>(entity);
            return CreatedAtAction(nameof(GetOrder), new { id = entity.Id }, createdDto);
        }

        /// <summary>
        /// Delete an existing order by ID.
        /// </summary>
        [Authorize]
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
