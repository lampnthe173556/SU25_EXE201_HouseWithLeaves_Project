using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> GetAllCategory();
    }
}
