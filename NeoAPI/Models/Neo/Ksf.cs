using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class Ksf
{
    public int Idksf { get; set; }

    public string KsfNombre { get; set; } = null!;

    public bool KsfEsta { get; set; }

    public virtual ICollection<ReuDium> ReuDia { get; set; } = new List<ReuDium>();
}
