using AutoMapper;
using FluentAssertions;
using Laundry.Api.Data.AutoMapper;
using Laundry.Api.Models;
using Laundry.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

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

            // Validate the AutoMapper configuration is valid
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
                    new OrderItem
                    {
                        Id = 1,
                        OrderId = 123,
                        ServiceId = 10,
                        Quantity = 5,
                        UnitPrice = 10.5m,
                        TotalPrice = 52.5m
                        // Add other fields if needed
                    }
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
            dto.OrderItems[0].Quantity.Should().Be(5);
            dto.OrderItems[0].UnitPrice.Should().Be(10.5m);
            dto.OrderItems[0].TotalPrice.Should().Be(52.5m);
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
                    new OrderItemDto
                    {
                        Id = 1,
                        OrderId = 123,
                        ServiceId = 10,
                        Quantity = 5,
                        UnitPrice = 10.5m,
                        TotalPrice = 52.5m
                        // Add other fields if needed
                    }
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

            var orderItem = order.OrderItems.First();
            orderItem.Id.Should().Be(1);
            orderItem.Quantity.Should().Be(5);
            orderItem.UnitPrice.Should().Be(10.5m);
            orderItem.TotalPrice.Should().Be(52.5m);
        }
    }
}
