using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class PuesTrab
{
    public int IdPuesTrab { get; set; }

    public string Ptnombre { get; set; } = null!;

    public string? Ptdescri { get; set; }

    public bool? Ptesta { get; set; }

    public virtual ICollection<Monto> Montos { get; set; } = new List<Monto>();
}
