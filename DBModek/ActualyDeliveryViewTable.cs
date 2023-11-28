using System;
using System.Collections.Generic;

namespace DBModels;

public partial class ActualyDeliveryViewTable
{
    public int MaterialId { get; set; }

    public double? DeliveredForQuartal { get; set; }

    public double? UsedDeliveredForQuartal { get; set; }

    public int? YearOfDelivery { get; set; }

    public int? QuarterOfDelivery { get; set; }
}
