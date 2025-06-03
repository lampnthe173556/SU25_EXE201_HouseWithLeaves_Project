using System;
using System.Collections.Generic;

namespace ProjectHouseWithLeaves.Models;

public partial class ShippingMethod
{
    public int ShippingMethodId { get; set; }

    public string? MethodName { get; set; }

    public decimal? ShippingCost { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
