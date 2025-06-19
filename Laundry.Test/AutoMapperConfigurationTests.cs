using AutoMapper;
using Laundry.Api.Data.AutoMapper;

namespace Laundry.Tests
{
    public class AutoMapperConfigurationTests
    {
        private readonly IConfigurationProvider _configuration;

        public AutoMapperConfigurationTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<LaundryMappingProfile>();
            });
        }

        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            _configuration.AssertConfigurationIsValid(); // Throws if mapping is invalid
        }
    }
}
