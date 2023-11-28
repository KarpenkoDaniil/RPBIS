using System;
using System.Collections.Generic;

namespace DBModels;

public partial class EmployesWhoCantCompleteContractToTime
{
    public string? LastName { get; set; }

    public string? Post { get; set; }

    public int EmployeeId { get; set; }

    public int SupplyContractsId { get; set; }
}
