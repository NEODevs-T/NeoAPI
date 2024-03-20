using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class TipSuple
{
    public int IdTipSuple { get; set; }

    public string? Tscausa { get; set; }

    public string? Tsdescri { get; set; }

    public bool? Tsestado { get; set; }

    public virtual ICollection<Resuman> Resumen { get; set; } = new List<Resuman>();
}
