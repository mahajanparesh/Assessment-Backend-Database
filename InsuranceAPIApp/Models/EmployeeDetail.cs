using System;
using System.Collections.Generic;

namespace InsuranceAPIApp.Models;

public partial class EmployeeDetail
{
    public int EmpId { get; set; }

    public string Name { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public int? UserId { get; set; }

    public string? Phone { get; set; }

    public DateTime? JoinDate { get; set; }

    public virtual UserAuth? User { get; set; }
}
