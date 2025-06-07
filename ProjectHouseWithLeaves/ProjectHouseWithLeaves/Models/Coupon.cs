using System;
using System.Collections.Generic;

namespace ProjectHouseWithLeaves.Models;

public partial class Coupon
{
    public int CouponId { get; set; }

    public string Code { get; set; } = null!;

    public int? MaxUsage { get; set; }

    public string DiscountType { get; set; } = null!;

    public decimal? DiscountPercent { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? MinOrderAmount { get; set; }

    public decimal? MaxOrderAmount { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
