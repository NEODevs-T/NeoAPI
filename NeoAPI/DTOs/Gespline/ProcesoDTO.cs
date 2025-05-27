using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Gespline;

public partial class ProcesoDTO
{
    public string Codigoproceso { get; set; } = null!;

    public string? Codigoarea { get; set; }

    public string? Descripcion { get; set; }

    public int? Estado { get; set; }

    public DateTime? Timespan { get; set; }

}
