namespace ProjectHouseWithLeaves.Dtos
{
    public class ShippingMethodDtos
    {
        public int ShippingMethodId { get; set; }

        public string? MethodName { get; set; }

        public decimal? ShippingCost { get; set; }

        public int? Status { get; set; }
    }
}
