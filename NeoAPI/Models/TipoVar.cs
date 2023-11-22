using System;
using System.Collections.Generic;

namespace NeoAPI.Models;

public partial class TipoVar
{
    public int IdTipoVar { get; set; }

    public string Tvnombre { get; set; } = null!;

    public string? Tvdescri { get; set; }

    public DateTime TvfechCrea { get; set; }

    public bool Tvestado { get; set; }

    public virtual ICollection<Variable> Variables { get; set; } = new List<Variable>();
}
