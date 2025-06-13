using AutoMapper;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Helper.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // Mapping Product to ProductDTO
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.MainImage, opt => opt.MapFrom(src => src.ProductImages != null && src.ProductImages.Any() ? src.ProductImages.FirstOrDefault().ImageUrl : null))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : string.Empty));

            // Mapping ProductDTO to Product
            CreateMap<ProductDTO, Product>()
                .ForMember(dest => dest.ProductImages, opt => opt.Ignore()) 
                .ForMember(dest => dest.Category, opt => opt.Ignore());   
        }
    }
}
