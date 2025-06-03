﻿using System;
using System.Collections.Generic;

namespace ProjectHouseWithLeaves.Models;

public partial class FavoriteProduct
{
    public int FavoriteId { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
