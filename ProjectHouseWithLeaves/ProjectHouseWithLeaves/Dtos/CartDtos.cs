using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Dtos
{
    public class CartDtos
    {
        public int CartId { get; set; }

        public int UserId { get; set; }
        public ICollection<CartItemDtos> CartItems { get; set; } = new List<CartItemDtos>();
        public decimal TotalAmount { get; set; }
    }
}
