namespace ProjectHouseWithLeaves.Dtos
{
    public class OrderDtos
    {
        public int? UserId { get; set; }
        public AdressDtos? AddressDetail { get; set; }
        public List<OrderItemDtos>? Items { get; set; }
        public int? PaymentMethodId { get; set; }
        public ShippingDtos? Shipping { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
