using System;
using System.Collections.Generic;

namespace ExcessInventoryManagement.Models;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public int ProductId { get; set; }

    public int? ProductCount { get; set; }

    public DateTime? Date { get; set; }

    public string? Description { get; set; }

    public virtual Product Product { get; set; } = null!;
}
