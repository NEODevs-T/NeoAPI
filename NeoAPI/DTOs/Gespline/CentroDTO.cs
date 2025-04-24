using System;
using System.Collections.Generic;

namespace NeoAPI.ModelsDTO.Gespline;

public partial class CentroDTO
{
    public string Codigocentro { get; set; } = null!;

    public string? Nombre { get; set; }

    public int? Estado { get; set; }

    public DateTime? Timespan { get; set; }

}
