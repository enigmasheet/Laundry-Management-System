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
            var orders = await _context.Orders
                                       .Include(o => o.OrderItems)
                                       .ToListAsync();
            var orderDtos = _mapper.Map<List<OrderDto>>(orders);
            return Ok(orderDtos);
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
            var orderEntity = await _context.Orders
                                            .Include(o => o.OrderItems)
                                            .FirstOrDefaultAsync(o => o.Id == id);

            if (orderEntity == null)
            {
                return NotFound();
            }

            var orderDto = _mapper.Map<OrderDto>(orderEntity);
            return Ok(orderDto);
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
                return BadRequest("ID mismatch");

            var orderEntity = await _context.Orders
                                            .Include(o => o.OrderItems)
                                            .FirstOrDefaultAsync(o => o.Id == id);

            if (orderEntity == null)
                return NotFound();

            // Map top-level fields
            _mapper.Map(orderDto, orderEntity);

            // --- Sync OrderItems manually ---
            var incomingItems = orderDto.OrderItems ?? new List<OrderItemDto>();

            // Remove deleted items
            var incomingIds = incomingItems.Select(i => i.Id).ToList();
            var removedItems = orderEntity.OrderItems
                                          .Where(e => !incomingIds.Contains(e.Id))
                                          .ToList();

            foreach (var item in removedItems)
            {
                _context.OrderItems.Remove(item);
            }

            // Add or update items
            foreach (var dto in incomingItems)
            {
                // Avoid updating nested Service object
                dto.Service = null;

                var existingItem = orderEntity.OrderItems
                                              .FirstOrDefault(e => e.Id == dto.Id);

                if (existingItem != null)
                {
                    _mapper.Map(dto, existingItem);
                }
                else
                {
                    var newItem = _mapper.Map<OrderItem>(dto);
                    orderEntity.OrderItems.Add(newItem);
                }
            }

            await _context.SaveChangesAsync();
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
            // Manually nullify nested service objects to prevent tracking issues
            foreach (var item in orderDto.OrderItems)
            {
                item.Service = null;
            }

            var entity = _mapper.Map<Order>(orderDto);

            // Ensure EF Core treats each OrderItem as a new entity
            foreach (var item in entity.OrderItems)
            {
                _context.Entry(item).State = EntityState.Added;
            }

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
