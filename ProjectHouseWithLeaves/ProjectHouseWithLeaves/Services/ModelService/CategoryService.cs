using Microsoft.EntityFrameworkCore;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public class CategoryService : ICategoryService
    {
        private readonly MiniPlantStoreContext _context;

        public CategoryService(MiniPlantStoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }
    }
}
