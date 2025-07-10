using AutoMapper;
using Laundry.Api.Models;
using Laundry.Shared.DTOs;
using Laundry.Shared.Enums;

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
                .ForMember(dest => dest.IsPaid, opt => opt.MapFrom(src => src.PaymentStatus == PaymentStatus.Paid))
                .ReverseMap()
                .ForMember(dest => dest.OrderCode, opt => opt.Ignore())
                .ForMember(dest => dest.FinalAmount, opt => opt.Ignore())
                .ForMember(dest => dest.IsPaid, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())      // 🧠 Add this
                .ForMember(dest => dest.Vendor, opt => opt.Ignore())        // 🧠 And this
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore());

            // OrderItem Mapping
            CreateMap<OrderItem, OrderItemDto>()
                .ReverseMap()
                .ForMember(dest => dest.Service, opt => opt.Ignore()); // ✅ Ignore nested Service

            // User Mapping
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Vendor, opt => opt.MapFrom(src => src.Vendor))
                .ReverseMap();

            CreateMap<UserDto, UserDto>();

            // Vendor Mapping
            CreateMap<Vendor, VendorDto>()
                .ForMember(dest => dest.Users, opt => opt.Ignore())
                .ForMember(dest => dest.Services, opt => opt.Ignore())
                .ForMember(dest => dest.Orders, opt => opt.Ignore())
                .ForMember(dest => dest.Inquiries, opt => opt.Ignore())
                .ForMember(dest => dest.Reviews, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Users, opt => opt.Ignore())
                .ForMember(dest => dest.Services, opt => opt.Ignore())
                .ForMember(dest => dest.Orders, opt => opt.Ignore())
                .ForMember(dest => dest.Inquiries, opt => opt.Ignore())
                .ForMember(dest => dest.Reviews, opt => opt.Ignore());
            CreateMap<Vendor, VendorInfoDto>();

            // Service Mapping
            CreateMap<Service, ServiceDto>()
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.ToString()))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
                .ForMember(dest => dest.Vendor, opt => opt.MapFrom(src => src.Vendor)); // ✅ FIXED

            CreateMap<ServiceDto, Service>()
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => ParseServiceUnit(src.Unit)))
                .ForMember(dest => dest.Reviews, opt => opt.Ignore())
                .ForMember(dest => dest.Vendor, opt => opt.MapFrom(src => src.Vendor)); // ✅ FIXED

            // Review Mapping
            CreateMap<Review, ReviewDto>().ReverseMap();

            // VendorInquiry Mapping
            CreateMap<VendorInquiry, VendorInquiryDto>().ReverseMap();
        }

        private static ServiceUnit ParseServiceUnit(string unit)
        {
            return string.IsNullOrWhiteSpace(unit)
                ? ServiceUnit.PerKg
                : Enum.TryParse<ServiceUnit>(unit, true, out var parsedUnit) ? parsedUnit : ServiceUnit.PerKg;
        }
    }
}
