using AutoMapper;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Helper.Mapping
{
    public class CartMappingProfile : Profile
    {
        public CartMappingProfile()
        {
            CreateMap<Cart, CartDtos>()
                .ForMember(dest => dest.TotalAmount, 
                                    otp => otp.MapFrom(src => src.CartItems.Sum(ci => ci.Quantity * ci.UnitPrice)));
            CreateMap<CartItem, CartItemDtos>()
                .ForMember(dest => dest.ProductName, otp => otp.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.ImgUrl, otp => otp.MapFrom(src =>
                    src.Product.ProductImages.Any()
                    ? src.Product.ProductImages.FirstOrDefault().ImageUrl
                    : null))
                .ForMember(dest => dest.SubTotal, otp => otp.MapFrom(src => src.Quantity * src.UnitPrice));

        }
    }
}
