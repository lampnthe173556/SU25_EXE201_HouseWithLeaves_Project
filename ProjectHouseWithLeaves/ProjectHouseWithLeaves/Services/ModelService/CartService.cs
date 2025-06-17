using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public class CartService : ICartService
    {
        private readonly MiniPlantStoreContext _context;
        private readonly IMapper _mapper;
        public CartService(MiniPlantStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        private async Task<Cart?> GetCartWithItemsAndProductAsync(int userId)
        {
            return await _context.Carts.Include(x => x.CartItems)
                                       .ThenInclude(x => x.Product)
                                       .ThenInclude(x => x.ProductImages)
                                       .SingleOrDefaultAsync(x => x.UserId == userId);
        }
        public async Task<ApiResponse<CartDtos?>> AddItemToCartAsync(int userId, int productId, int quantity)
        {
            var cart = await GetOrCreareCartByUserAsync(userId);
            var existingCartItem = cart.Result.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            var product = await _context.Products.FindAsync(productId);
            var actualCart = await _context.Carts
                                    .Include(c => c.CartItems)
                                    .SingleOrDefaultAsync(c => c.CartId == cart.Result.CartId);
            if (actualCart == null)
            {
                return new ApiResponse<CartDtos?>
                {
                    Success = false,
                    Message = "Cart not found after creation/retrieval logic."
                };
            }
            if (existingCartItem != null)
            {
                
                var itemToUpdate = actualCart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
                if (itemToUpdate != null)
                {
                    itemToUpdate.Quantity += quantity;
                }
            }
            else
            {
                
                actualCart.CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    UnitPrice = product.Price, 
                    CartId = actualCart.CartId
                });
            }

            actualCart.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            var updatedCart = await GetCartWithItemsAndProductAsync(userId);
            var cartDto = _mapper.Map<CartDtos>(updatedCart);
            return new ApiResponse<CartDtos?> 
            {
                Success = true, 
                Result = cartDto, 
                Message = "Item added/updated in cart successfully." 
            };
        }
        public async Task<ApiResponse<CartDtos?>> DecreaseItemCartAsync(int userId, int productId, int quantity)
        {
            var cart = await GetOrCreareCartByUserAsync(userId);
            var existingCartItem = cart.Result.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            var product = await _context.Products.FindAsync(productId);
            var actualCart = await _context.Carts
                                    .Include(c => c.CartItems)
                                    .SingleOrDefaultAsync(c => c.CartId == cart.Result.CartId);
            if (actualCart == null)
            {
                return new ApiResponse<CartDtos?>
                {
                    Success = false,
                    Message = "Cart not found after creation/retrieval logic."
                };
            }
            if (existingCartItem != null)
            {

                var itemToUpdate = actualCart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
                if (itemToUpdate != null)
                {
                    itemToUpdate.Quantity -= quantity;
                }
            }
            else
            {

                actualCart.CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    UnitPrice = product.Price,
                    CartId = actualCart.CartId
                });
            }

            actualCart.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            var updatedCart = await GetCartWithItemsAndProductAsync(userId);
            var cartDto = _mapper.Map<CartDtos>(updatedCart);
            return new ApiResponse<CartDtos?>
            {
                Success = true,
                Result = cartDto,
                Message = "Item added/updated in cart successfully."
            };
        }
        public async Task<ApiResponse<CartDtos?>> ClearCartAsync(int userId)
        {
            var cart = await GetCartWithItemsAndProductAsync(userId);
            if (cart == null)
            {
                return new ApiResponse<CartDtos?> 
                { 
                    Success = false,
                    Message = "Cart not found." 
                };
            }

            _context.CartItems.RemoveRange(cart.CartItems); 
            cart.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            var cartDto = _mapper.Map<CartDtos>(cart);
            return new ApiResponse<CartDtos?> 
            { 
                Success = true,
                Result = cartDto, 
                Message = "Cart cleared successfully." 
            };
        }
        public async Task<ApiResponse<CartDtos?>> GetOrCreareCartByUserAsync(int userId)
        {
            var cart = await GetCartWithItemsAndProductAsync(userId);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CreatedAt = DateTime.Now
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }
            var cartDto = _mapper.Map<CartDtos>(cart);
            return new ApiResponse<CartDtos?>
            {
                Success = true,
                Result = cartDto,
                Message = "Cart retrieved successfully."
            };
        }
        public async Task<ApiResponse<CartDtos?>> RemoveItemFromCartAsync(int userId, int productId)
        {
            var cart = await GetCartWithItemsAndProductAsync(userId);
            if (cart == null)
            {
                return new ApiResponse<CartDtos?> 
                { 
                    Success = false, 
                    Message = "Cart not found." 
                };
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem == null)
            {
                return new ApiResponse<CartDtos?> 
                { 
                    Success = false, 
                    Message = "Item not found in cart."
                };
            }

            _context.CartItems.Remove(cartItem); 
            cart.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            var updatedCart = await GetCartWithItemsAndProductAsync(userId);
            var cartDto = _mapper.Map<CartDtos>(updatedCart);
            return new ApiResponse<CartDtos?> 
            { 
                Success = true,
                Result = cartDto, 
                Message = "Item removed from cart successfully."
            };
        }
    }
}
