using System;
using System.Collections.Generic;

namespace ProjectHouseWithLeaves.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public int? CouponId { get; set; }

    public string? ShippingAddress { get; set; }

    public string? Ward { get; set; }

    public string? District { get; set; }

    public string? City { get; set; }

    public int? PaymentMethodId { get; set; }

    public int? ShippingMethodId { get; set; }

    public string? Status { get; set; }

    public virtual Coupon? Coupon { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual PaymentMethod? PaymentMethod { get; set; }

    public virtual ShippingMethod? ShippingMethod { get; set; }

    public virtual User? User { get; set; }
}
