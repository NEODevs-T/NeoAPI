using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class Causa
{
    public int IdCausa { get; set; }

    public int IdCausante { get; set; }

    public string Cnombre { get; set; } = null!;

    public string? Cdescri { get; set; }

    public bool Cestado { get; set; }

    public virtual Causante IdCausanteNavigation { get; set; } = null!;
}
