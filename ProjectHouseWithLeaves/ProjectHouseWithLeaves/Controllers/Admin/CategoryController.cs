using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Services.ModelService;
using ProjectHouseWithLeaves.Helper.Authorization;

namespace ProjectHouseWithLeaves.Controllers.Admin
{
    [Area("Admin")]
    [AdminAuthorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(
            ICategoryService categoryService,
            IProductService productService,
            ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _productService = productService;
            _logger = logger;
        }

        public async Task<IActionResult> Category()
        {
            var categories = await _categoryService.GetAllCategory();
            return View(categories);
        }

        
        [HttpPost]
        public async Task<JsonResult> Create([FromBody] Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Dữ liệu không hợp lệ" });
                }

                var newCategory = await _categoryService.CreateCategory(category);
                return Json(new { success = true, data = newCategory });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category");
                return Json(new { success = false, message = "Không thể tạo danh mục" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Update([FromBody] Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Dữ liệu không hợp lệ" });
                }

                var updatedCategory = await _categoryService.UpdateCategory(category);
                return Json(new { success = true, data = updatedCategory });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category");
                return Json(new { success = false, message = "Không thể cập nhật danh mục" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                await _categoryService.DeleteCategory(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category");
                return Json(new { success = false, message = "Không thể xóa danh mục" });
            }
        }
    }
} 