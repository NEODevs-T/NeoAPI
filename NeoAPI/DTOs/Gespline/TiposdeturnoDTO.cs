using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Gespline;

public partial class TiposdeturnoDTO
{
    public string Codigoturno { get; set; } = null!;

    public string? Turno { get; set; }

    public int? Estado { get; set; }

    public double? Horas { get; set; }

    public DateTime? Fechaturno { get; set; }

    public DateTime? Timespan { get; set; }

    public DateTime? Turnofecha { get; set; }

    public bool? Cierreautomatico { get; set; }

}
