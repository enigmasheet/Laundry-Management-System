using AutoMapper;
using Laundry.Api.Models;
using Laundry.Shared.DTOs;

namespace Laundry.Api.Data.AutoMapper
{
    public class LaundryMappingProfile : Profile
    {
        public LaundryMappingProfile()
        {
            // Order → OrderDto
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.OrderCode, opt => opt.MapFrom(src => $"{src.VendorId}-{src.Id}"))
                .ForMember(dest => dest.FinalAmount, opt => opt.MapFrom(src =>
                    src.TotalAmount + src.ExpressCharge + src.TaxAmount - src.DiscountAmount))
                .ForMember(dest => dest.IsPaid, opt => opt.MapFrom(src => src.PaymentStatus == Shared.Enums.PaymentStatus.Paid))
                .ReverseMap()
                .ForMember(dest => dest.OrderCode, opt => opt.Ignore())
                .ForMember(dest => dest.FinalAmount, opt => opt.Ignore())
                .ForMember(dest => dest.IsPaid, opt => opt.Ignore());

            // OrderItem Mapping
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();

            // User Mapping
            CreateMap<User, UserDto>().ReverseMap();

            // Vendor Mapping
            CreateMap<Vendor, VendorDto>().ReverseMap();

            // Other Mappings
            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<VendorInquiry, VendorInquiryDto>().ReverseMap();
        }
    }
}
