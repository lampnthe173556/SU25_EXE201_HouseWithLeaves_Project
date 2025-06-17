using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ProjectHouseWithLeaves.Dtos
{
    public class ProductDTO
    {
        public int Id { get; set; } 
        public string Name { get; set; } 
        public string Description { get; set; } 
        public string? Size { get; set; }

        public decimal Price { get; set; } 
        public int? Stock { get; set; } 
        public int? CategoryId { get; set; } 
        public string CategoryName { get; set; } 

        // Đường dẫn mặc định cho hình ảnh chính
        public string? MainImage { get; set; } = "";

        // Danh sách ảnh phụ
        public List<string> AdditionalImages { get; set; } = new List<string>();

        // Các giá trị mặc định
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }

   
} 