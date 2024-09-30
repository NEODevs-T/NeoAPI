using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class Identifi
{
    public int IdIdentif { get; set; }

    public string Inombre { get; set; } = null!;

    public string? Idescri { get; set; }

    public bool Iestado { get; set; }

    public virtual ICollection<ProNoCon> ProNoCons { get; set; } = new List<ProNoCon>();
}
