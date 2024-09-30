using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class PropDisp
{
    public int IdProDisp { get; set; }

    public string Pdnombre { get; set; } = null!;

    public string? Pddescri { get; set; }

    public bool Pdestado { get; set; }

    public virtual ICollection<ProNoCon> ProNoCons { get; set; } = new List<ProNoCon>();
}
