using System;
using System.Collections.Generic;

namespace DBModels;

public partial class SupplyContract
{
    public int SupplyContractsId { get; set; }

    public DateTime? DateOfConclusion { get; set; }

    public DateTime? DateOfDiliver { get; set; }

    public string? Supplyer { get; set; }

    public double? DiliverySize { get; set; }

    public int? MaterialId { get; set; }

    public int? EmployeeId { get; set; }

    public virtual ICollection<DeliveredResource> DeliveredResources { get; set; } = new List<DeliveredResource>();

    public virtual Employe? Employee { get; set; }

    public virtual Material? Material { get; set; }
}
