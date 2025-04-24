using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Gespline;

public partial class Tipocargospersonal
{
    public string Codidocargospersonal { get; set; } = null!;

    public string? Nombredelcargo { get; set; }

    public int? Estado { get; set; }

    public DateTime? Timespan { get; set; }

    public virtual ICollection<Personal> Personals { get; set; } = new List<Personal>();
}
