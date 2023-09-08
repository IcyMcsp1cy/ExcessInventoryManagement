using System;
using System.Collections.Generic;

namespace ExcessInventoryManagement.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public decimal? Price { get; set; }

    public decimal? Cost { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<MarkdownPlan> MarkdownPlans { get; set; } = new List<MarkdownPlan>();
}
