using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public class CategoryService : ICategoryService
    {
        private readonly MiniPlantStoreContext _context;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(MiniPlantStoreContext context, ILogger<CategoryService> logger)
        {
            _context = context;
            _logger = logger;

        }
        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }
        //admin
        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found");
            }

            return category;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            try
            {
                category.Status = 1; // Active by default
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category");
                throw;
            }
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            try
            {
                var existingCategory = await _context.Categories.FindAsync(category.CategoryId);
                if (existingCategory == null)
                {
                    throw new KeyNotFoundException($"Category with ID {category.CategoryId} not found");
                }

                existingCategory.CategoryName = category.CategoryName;
                existingCategory.Description = category.Description;
                existingCategory.Status = category.Status;

                await _context.SaveChangesAsync();
                return existingCategory;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category");
                throw;
            }
        }

        public async Task DeleteCategory(int id)
        {
            try
            {
                var category = await _context.Categories
                    .Include(c => c.Products)
                    .FirstOrDefaultAsync(c => c.CategoryId == id);

                if (category == null)
                {
                    throw new KeyNotFoundException($"Category with ID {id} not found");
                }

                // Check if category has products
                if (category.Products.Any())
                {
                    throw new InvalidOperationException("Cannot delete category with associated products");
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category");
                throw;
            }
        }
    }
}
