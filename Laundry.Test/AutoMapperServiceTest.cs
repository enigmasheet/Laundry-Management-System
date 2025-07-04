using AutoMapper;
using FluentAssertions;
using Laundry.Api.Data.AutoMapper;
using Laundry.Api.Models;
using Laundry.Shared.DTOs;
using Laundry.Shared.Enums;
using Xunit;

namespace Laundry.Tests
{
    public class AutoMapperServiceTests
    {
        private readonly IMapper _mapper;

        public AutoMapperServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<LaundryMappingProfile>();
            });

            config.AssertConfigurationIsValid(); // ensures mapping is valid at startup

            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Should_Map_Service_To_ServiceDto_Correctly()
        {
            // Arrange
            var service = new Service
            {
                Id = 1,
                Name = "Dry Cleaning",
                Description = "High quality dry cleaning",
                Price = 12.5m,
                Unit = ServiceUnit.PerItem,
                VendorId = 99,
                Reviews = new List<Review>
                {
                    new Review { Id = 1, Rating = 5, Comment = "Excellent!" }
                }
            };

            // Act
            var dto = _mapper.Map<ServiceDto>(service);

            // Assert
            dto.Should().NotBeNull();
            dto.Id.Should().Be(service.Id);
            dto.Name.Should().Be(service.Name);
            dto.Description.Should().Be(service.Description);
            dto.Price.Should().Be(service.Price);
            dto.Unit.Should().Be(service.Unit.ToString());
            dto.VendorId.Should().Be(service.VendorId);
            dto.Reviews.Should().NotBeNull();
            dto.Reviews.Should().HaveCount(1);
            dto.Reviews[0].Rating.Should().Be(5);
        }

        [Fact]
        public void Should_Map_ServiceDto_To_Service_Correctly()
        {
            // Arrange
            var dto = new ServiceDto
            {
                Id = 2,
                Name = "Wash & Fold",
                Description = "Fast wash and fold",
                Price = 6.75m,
                Unit = "PerKg",
                VendorId = 101
            };

            // Act
            var entity = _mapper.Map<Service>(dto);

            // Assert
            entity.Should().NotBeNull();
            entity.Id.Should().Be(dto.Id);
            entity.Name.Should().Be(dto.Name);
            entity.Description.Should().Be(dto.Description);
            entity.Price.Should().Be(dto.Price);
            entity.Unit.Should().Be(ServiceUnit.PerKg);
            entity.VendorId.Should().Be(dto.VendorId);
            entity.Reviews.Should().BeEmpty(); // ignored in mapping
        }

        [Fact]
        public void Should_Handle_Invalid_Enum_String_With_Fallback()
        {
            // Arrange
            var dto = new ServiceDto
            {
                Id = 3,
                Name = "Steam Iron",
                Description = "Smooth finish",
                Price = 9.0m,
                Unit = "InvalidUnit",
                VendorId = 102
            };

            // Act
            var entity = _mapper.Map<Service>(dto);

            // Assert
            entity.Unit.Should().Be(ServiceUnit.PerKg); // fallback default
        }
    }
}
