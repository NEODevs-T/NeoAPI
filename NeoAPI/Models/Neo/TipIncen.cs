using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class TipIncen
{
    public int IdTipIncen { get; set; }

    public string Tinombre { get; set; } = null!;

    public string? Tidesc { get; set; }

    public bool Tiesta { get; set; }

    public virtual ICollection<MontoBon> MontoBons { get; set; } = new List<MontoBon>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual ICollection<Resuman> Resumen { get; set; } = new List<Resuman>();
}
