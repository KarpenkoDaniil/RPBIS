using System;
using System.Collections.Generic;

namespace DBModels;

public partial class Employe
{
    public int EmployeeId { get; set; }

    public string? Post { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public virtual ICollection<SupplyContract> SupplyContracts { get; set; } = new List<SupplyContract>();
}
