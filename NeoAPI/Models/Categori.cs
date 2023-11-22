using System;
using System.Collections.Generic;

namespace NeoAPI.Models;

public partial class Categori
{
    public int IdCategori { get; set; }

    public string Cnombre { get; set; } = null!;

    public string? CfechaCrea { get; set; }

    public string Cdescri { get; set; } = null!;

    public bool? Cesta { get; set; }

    public virtual ICollection<CorteDi> CorteDis { get; set; } = new List<CorteDi>();
}
