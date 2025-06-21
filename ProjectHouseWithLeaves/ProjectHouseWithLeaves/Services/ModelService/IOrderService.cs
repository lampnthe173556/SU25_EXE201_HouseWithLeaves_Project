using ProjectHouseWithLeaves.Dtos;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public interface IOrderService
    {
        Task<bool> CreateOrder(OrderDtos order);
    }
}
