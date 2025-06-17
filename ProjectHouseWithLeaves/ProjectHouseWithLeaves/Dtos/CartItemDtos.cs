namespace ProjectHouseWithLeaves.Dtos
{
    public class CartItemDtos
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ImgUrl { get; set; }
        public int Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
    }
}
