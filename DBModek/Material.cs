using System;
using System.Collections.Generic;

namespace DBModels;

public partial class Material
{
    public int MaterialId { get; set; }

    public string? MaterialType { get; set; }

    public string? NameOfStateStandart { get; set; }

    public string? StateStandart { get; set; }

    public string? Characteristics { get; set; }

    public string? MeasureOfMeasurement { get; set; }

    public virtual ICollection<RequiredResource> RequiredResources { get; set; } = new List<RequiredResource>();

    public virtual ICollection<SupplyContract> SupplyContracts { get; set; } = new List<SupplyContract>();
}
