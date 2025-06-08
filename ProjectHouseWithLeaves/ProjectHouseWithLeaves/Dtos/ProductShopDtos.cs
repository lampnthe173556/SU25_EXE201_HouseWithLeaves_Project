namespace ProjectHouseWithLeaves.Dtos
{
    public class ProductShopDtos
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public List<string> ImageUrls { get; set; } = null!;
        public int? CategoryId { get; set; }
    }
}
