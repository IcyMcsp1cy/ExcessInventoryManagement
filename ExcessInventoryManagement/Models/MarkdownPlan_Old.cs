using System;
using System.Collections.Generic;

namespace ExcessInventoryManagement.Models;

public partial class MarkdownPlan_Old
{
    public int MarkdownPlanId { get; set; }

    public int ProductId { get; set; }

    public string? PlanName { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal? InitialPriceReduction { get; set; }

    public decimal? MidwayPriceReduction { get; set; }

    public decimal? FinalPriceReduction { get; set; }

    public virtual Product Product { get; set; } = null!;
}
