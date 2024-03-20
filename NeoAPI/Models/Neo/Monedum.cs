using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class Monedum
{
    public int IdMoneda { get; set; }

    public string Mtipo { get; set; } = null!;

    public string? Mpais { get; set; }

    public bool Mestado { get; set; }

    public virtual ICollection<Monto> Montos { get; set; } = new List<Monto>();
}
