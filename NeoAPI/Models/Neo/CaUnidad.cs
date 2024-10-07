using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class CaUnidad
{
    public int IdCaUnidad { get; set; }

    public string Unombre { get; set; } = null!;

    public string? Udescri { get; set; }

    public bool Uestado { get; set; }

    public virtual ICollection<ProNoCon> ProNoCons { get; set; } = new List<ProNoCon>();
}
