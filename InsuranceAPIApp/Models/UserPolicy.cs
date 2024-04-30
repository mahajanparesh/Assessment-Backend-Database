using System;
using System.Collections.Generic;

namespace InsuranceAPIApp.Models;

public partial class UserPolicy
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PolicyId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual PolicyDetail Policy { get; set; } = null!;

    public virtual UserAuth User { get; set; } = null!;
}
