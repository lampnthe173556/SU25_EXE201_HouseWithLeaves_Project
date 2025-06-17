using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public interface ICartService
    {
        public Task<ApiResponse<CartDtos?>> GetOrCreareCartByUserAsync(int userId);
        public Task<ApiResponse<CartDtos?>> AddItemToCartAsync(int userId, int productId, int quantity);
        public Task<ApiResponse<CartDtos?>> DecreaseItemCartAsync(int userId, int productId, int quantity);
        public Task<ApiResponse<CartDtos?>> RemoveItemFromCartAsync(int userId, int productId);
        public Task<ApiResponse<CartDtos?>> ClearCartAsync(int userId);
    }
}
