using System;
using System.Collections.Generic;

namespace InsuranceAPIApp.Models;

public partial class PolicyDetail
{
    public int PolicyId { get; set; }

    public string PolicyType { get; set; } = null!;

    public string Insurer { get; set; } = null!;

    public decimal Amount { get; set; }

    public virtual ICollection<UserPolicy> UserPolicies { get; set; } = new List<UserPolicy>();
}
