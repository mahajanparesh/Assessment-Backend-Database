using System;
using System.Collections.Generic;

namespace InsuranceAPIApp.Models;

public partial class UserAuth
{
    public int Userid { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public virtual EmployeeDetail? EmployeeDetail { get; set; }

    public virtual ICollection<PaymentDetail> PaymentDetails { get; set; } = new List<PaymentDetail>();

    public virtual ICollection<UserPolicy> UserPolicies { get; set; } = new List<UserPolicy>();
}
