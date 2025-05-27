using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Gespline;

public partial class Tiposdeturno
{
    public string Codigoturno { get; set; } = null!;

    public string? Turno { get; set; }

    public int? Estado { get; set; }

    public double? Horas { get; set; }

    public DateTime? Fechaturno { get; set; }

    public DateTime? Timespan { get; set; }

    public DateTime? Turnofecha { get; set; }

    public bool? Cierreautomatico { get; set; }

    public virtual ICollection<Entradaejecucion> Entradaejecucions { get; set; } = new List<Entradaejecucion>();
}
