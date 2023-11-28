using System;
using System.Collections.Generic;

namespace DBModels;

public partial class RemaningResourcesViewTable
{
    public int? Year { get; set; }

    public double? RemaningResources { get; set; }

    public int MaterialId { get; set; }
}
