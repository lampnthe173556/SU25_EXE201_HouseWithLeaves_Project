using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public interface IShippingMethodService
    {
        Task<IEnumerable<ShippingMethodDtos>> GetAllShippingMethod();
    }
}
