using AutoMapper;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.DTOs;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Helper.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {

            /*1.CreateMap<Product, ProductDTO>()
•	Đây là câu lệnh khởi tạo ánh xạ giữa lớp Product(lớp nguồn) và lớp ProductDTO(lớp đích).
•	AutoMapper sẽ tự động ánh xạ các thuộc tính có cùng tên và kiểu dữ liệu giữa hai lớp.Tuy nhiên, các thuộc tính cần xử lý đặc biệt sẽ được định nghĩa bằng ForMember.
-- -
2.ForMember(dest => dest.MainImage, opt => opt.MapFrom(src => src.ProductImages != null && src.ProductImages.Any() ? src.ProductImages.FirstOrDefault().ImageUrl : null))
•	dest.MainImage: Thuộc tính MainImage trong lớp ProductDTO.
•	src.ProductImages: Thuộc tính ProductImages trong lớp Product, thường là một danh sách hình ảnh(List<ProductImage>).
•	Ý nghĩa:
•	Kiểm tra xem ProductImages có null hoặc rỗng không:
•	Nếu không null và có phần tử, lấy ImageUrl của phần tử đầu tiên(FirstOrDefault()).
•	Nếu null hoặc rỗng, gán giá trị null cho MainImage.
•	Kết quả: MainImage trong ProductDTO sẽ chứa URL của hình ảnh đầu tiên trong danh sách ProductImages.
-- -
3.ForMember(dest => dest.AdditionalImages, opt => opt.MapFrom(src => src.ProductImages.Select(img => img.ImageUrl).ToList()))
•	dest.AdditionalImages: Thuộc tính AdditionalImages trong lớp ProductDTO, thường là một danh sách URL hình ảnh(List<string>).
•	src.ProductImages.Select(img => img.ImageUrl).ToList():
•	Duyệt qua danh sách ProductImages trong Product.
•	Lấy thuộc tính ImageUrl của từng phần tử trong danh sách.
•	Chuyển đổi kết quả thành một danh sách(List<string>).
•	Kết quả: AdditionalImages trong ProductDTO sẽ chứa danh sách URL của tất cả hình ảnh trong ProductImages.
-- -
4.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : string.Empty))
•	dest.CategoryName: Thuộc tính CategoryName trong lớp ProductDTO.
•	src.Category: Thuộc tính Category trong lớp Product, thường là một đối tượng liên kết(ví dụ: Category).
•	Ý nghĩa:
•	Kiểm tra xem Category có null không:
•	Nếu không null, lấy giá trị CategoryName từ đối tượng Category.
•	Nếu null, gán giá trị chuỗi rỗng(string.Empty) cho CategoryName.
•	Kết quả: CategoryName trong ProductDTO sẽ chứa tên danh mục của sản phẩm hoặc chuỗi rỗng nếu sản phẩm không có danh mục.
-- -
Tổng kết:
            Đoạn mã này ánh xạ các thuộc tính đặc biệt từ Product sang ProductDTO:
1.MainImage: URL của hình ảnh chính(phần tử đầu tiên trong danh sách ProductImages).
2.AdditionalImages: Danh sách URL của tất cả hình ảnh bổ sung.
3.CategoryName: Tên danh mục của sản phẩm hoặc chuỗi rỗng nếu không có danh mục.*/
            // Mapping Product to ProductDTO
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.MainImage, opt => opt.MapFrom(src => src.ProductImages != null && src.ProductImages.Any() ? src.ProductImages.FirstOrDefault().ImageUrl : null))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : string.Empty));

            // Mapping ProductDTO to Product
            CreateMap<ProductDTO, Product>()
                .ForMember(dest => dest.ProductImages, opt => opt.Ignore()) // Ignore ProductImages during reverse mapping
                .ForMember(dest => dest.Category, opt => opt.Ignore());    // Ignore Category during reverse mapping
        }
    }
}
