using AutoMapper;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Helper.Mapping
{
    public class OrderHistoryMappingProfile : Profile
    {
        public static string GetStatusClass(string status) => status.ToLower() switch
        {
            "accepted" => "accepted",
            "pending" => "pending",
            "cancelled" => "cancelled",
            _ => status
        };
        public OrderHistoryMappingProfile()
        {
            CreateMap<Order, OrderHistoryDtos>()
              .ForMember(dest => dest.Payment, otp => otp.MapFrom(src => src.PaymentMethod.MethodName))
              .ForMember(dest => dest.Shipping, otp => otp.MapFrom(src => src.ShippingMethod.MethodName))
              .ForMember(dest => dest.StatusClass, otp => otp.MapFrom(src => GetStatusClass(src.Status)))
              .ForMember(dest => dest.Details, otp => otp.MapFrom(src => src.OrderDetails));

            CreateMap<OrderDetail, OrderHistoryDetailsDtos>()
                .ForMember(dest => dest.ProductName, otp => otp.MapFrom(src => src.Product.ProductName));
        }
    }
}
