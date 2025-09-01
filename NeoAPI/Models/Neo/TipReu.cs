using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class TipReu
{
    public int IdTipReu { get; set; }

    public string Tpnombre { get; set; } = null!;

    public string? Tpdescri { get; set; }

    public DateTime Tpfecha { get; set; }

    public bool Tpestado { get; set; }

    public virtual ICollection<Reunion> Reunions { get; set; } = new List<Reunion>();
}
