using System;
using System.Collections.Generic;

namespace InsuranceAPIApp.Models;

public partial class PaymentDetail
{
    public int Id { get; set; }

    public string CardOwnerName { get; set; } = null!;

    public string CardNumber { get; set; } = null!;

    public string SecurityCode { get; set; } = null!;

    public DateTime ValidThrough { get; set; }

    public int UserId { get; set; }

    public virtual UserAuth User { get; set; } = null!;
}
