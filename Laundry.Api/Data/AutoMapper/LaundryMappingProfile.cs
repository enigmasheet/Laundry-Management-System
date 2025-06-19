using AutoMapper;
using Laundry.Api.Models;
using Laundry.Shared.DTO;
namespace Laundry.Api.Data.AutoMapper
{
    public class LaundryMappingProfile : Profile
    {
        public LaundryMappingProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore()); // Computed property, ignored

            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore());

            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Vendor, VendorDto>().ReverseMap();
            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<VendorInquiry, VendorInquiryDto>().ReverseMap();
        }
    }

}
