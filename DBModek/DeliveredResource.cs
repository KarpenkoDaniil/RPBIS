using System;
using System.Collections.Generic;

namespace DBModels;

public partial class DeliveredResource
{
    public int DeliveredResourcesId { get; set; }

    public int? YearOfDelivery { get; set; }

    public int? QuarterOfDelivery { get; set; }

    public double? SizeOfResourseUsed { get; set; }

    public int? SupplyContractsId { get; set; }

    public virtual SupplyContract? SupplyContracts { get; set; }
}
