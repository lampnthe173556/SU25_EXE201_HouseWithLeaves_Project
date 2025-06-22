namespace ProjectHouseWithLeaves.Dtos
{
    public class OrderHistoryDetailsDtos
    {
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
