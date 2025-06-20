using Microsoft.EntityFrameworkCore;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public class ShippingMethodService : IShippingMethodService
    {
        private readonly MiniPlantStoreContext _context;

        public ShippingMethodService(MiniPlantStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShippingMethodDtos>> GetAllShippingMethod()
        {
            var shippingMethos = await _context.ShippingMethods
                                                .Select(x => new ShippingMethodDtos
                                                {
                                                    ShippingMethodId = x.ShippingMethodId,
                                                    MethodName = x.MethodName,
                                                    ShippingCost = x.ShippingCost,
                                                    Status = x.Status
                                                })
                                                .ToArrayAsync();
            return shippingMethos;
        }
    }
}
