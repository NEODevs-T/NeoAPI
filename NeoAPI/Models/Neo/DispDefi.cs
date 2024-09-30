using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class DispDefi
{
    public int IdDisDefi { get; set; }

    public string Ddnombre { get; set; } = null!;

    public string? Dddescri { get; set; }

    public bool Ddestado { get; set; }

    public virtual ICollection<ProNoCon> ProNoCons { get; set; } = new List<ProNoCon>();
}
