using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class RespoReu
{
    public int IdResReu { get; set; }

    public string Rrnombre { get; set; } = null!;

    public bool Rresta { get; set; }

    public string? Rrdesc { get; set; }

    public string Rrenglish { get; set; } = null!;

    public virtual ICollection<ReuDium> ReuDia { get; set; } = new List<ReuDium>();
}
