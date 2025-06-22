namespace ProjectHouseWithLeaves.Dtos
{
    public class OrderHistoryDtos
    {
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public String? Payment {  get; set; }
        public String? Shipping {  get; set; }
        public string? Status { get; set; }
        public string? StatusClass { get; set; }
        public List<OrderHistoryDetailsDtos>? Details { get; set; }
    }
}
