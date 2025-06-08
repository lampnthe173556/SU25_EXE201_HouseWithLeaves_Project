using System;
using System.Collections.Generic;

namespace ProjectHouseWithLeaves.Models;

public partial class Contact
{
    public int ContactId { get; set; }

    public string EmailContact { get; set; } = null!;

    public string? DescriptionContact { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? SendAt { get; set; }
}
