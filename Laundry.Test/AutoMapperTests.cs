using AutoMapper;
using FluentAssertions;
using Laundry.Api.Data.AutoMapper;
using Laundry.Api.Models;
using Laundry.Shared.DTOs;
namespace Laundry.Tests
{
    
    public class AutoMapperTests
    {
        private readonly IMapper _mapper;

        public AutoMapperTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<LaundryMappingProfile>();
            });

            // Validate the configuration is correct
            config.AssertConfigurationIsValid();

            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Should_Map_Order_To_OrderDto_Correctly()
        {
            // Arrange
            var order = new Order
            {
                Id = 123,
                CustomerId = Guid.NewGuid(),
                VendorId = 456,
                Status = Laundry.Shared.Enums.OrderStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                OrderItems = new List<OrderItem>
            {
                new OrderItem { Id = 1, OrderId = 123, ServiceId = 10, QuantityKg = 5, UnitPrice = 10.5m }
            }
            };

            // Act
            var dto = _mapper.Map<OrderDto>(order);

            // Assert
            dto.Should().NotBeNull();
            dto.Id.Should().Be(order.Id);
            dto.CustomerId.Should().Be(order.CustomerId);
            dto.VendorId.Should().Be(order.VendorId);
            dto.Status.Should().Be(order.Status);
            dto.OrderItems.Should().HaveCount(1);
            dto.OrderItems[0].Id.Should().Be(1);
            dto.OrderItems[0].QuantityKg.Should().Be(5);
            dto.OrderItems[0].UnitPrice.Should().Be(10.5m);
        }

        [Fact]
        public void Should_Map_OrderDto_To_Order_Correctly()
        {
            // Arrange
            var orderDto = new OrderDto
            {
                Id = 123,
                CustomerId = Guid.NewGuid(),
                VendorId = 456,
                Status = Laundry.Shared.Enums.OrderStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                OrderItems = new List<OrderItemDto>
            {
                new OrderItemDto { Id = 1, OrderId = 123, ServiceId = 10, QuantityKg = 5, UnitPrice = 10.5m }
            }
            };

            // Act
            var order = _mapper.Map<Order>(orderDto);

            // Assert
            order.Should().NotBeNull();
            order.Id.Should().Be(orderDto.Id);
            order.CustomerId.Should().Be(orderDto.CustomerId);
            order.VendorId.Should().Be(orderDto.VendorId);
            order.Status.Should().Be(orderDto.Status);
            order.OrderItems.Should().HaveCount(1);
            order.OrderItems.First().Id.Should().Be(1);
            order.OrderItems.First().QuantityKg.Should().Be(5);
            order.OrderItems.First().UnitPrice.Should().Be(10.5m);
        }
    }

}