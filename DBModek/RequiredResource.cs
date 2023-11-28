using System;
using System.Collections.Generic;

namespace DBModels;

public partial class RequiredResource
{
    public int RequiredResourcesId { get; set; }

    public int? MaterialId { get; set; }

    public int? Year { get; set; }

    public int? Quarter { get; set; }

    public double? SizeOfResurces { get; set; }

    public virtual Material? Material { get; set; }
}
