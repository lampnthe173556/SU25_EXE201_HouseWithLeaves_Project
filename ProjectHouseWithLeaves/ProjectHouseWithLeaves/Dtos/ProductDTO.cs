using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ProjectHouseWithLeaves.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; } // ID của sản phẩm
        public string Name { get; set; } // Tên sản phẩm
        public string Description { get; set; } // Mô tả sản phẩm
        public string? Size { get; set; }

        public decimal Price { get; set; } // Giá sản phẩm
        public int? Stock { get; set; } // Số lượng tồn kho
        public int? CategoryId { get; set; } // ID danh mục
        public string CategoryName { get; set; } // Tên danh mục

        // Đường dẫn mặc định cho hình ảnh chính
        public string? MainImage { get; set; } = "";

        // Các giá trị mặc định
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }

   
} 