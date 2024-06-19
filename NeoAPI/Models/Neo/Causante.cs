using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class Causante
{
    public int IdCausante { get; set; }

    public string Cnombre { get; set; } = null!;

    public string? Cdescri { get; set; }

    public bool Cestado { get; set; }

    public virtual ICollection<Causa> Causas { get; set; } = new List<Causa>();
}
