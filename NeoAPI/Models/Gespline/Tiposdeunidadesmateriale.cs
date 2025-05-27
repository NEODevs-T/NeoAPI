using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Gespline;

public partial class Tiposdeunidadesmateriale
{
    public string Codigo { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Usuario { get; set; }

    public DateTime? Fecharegistro { get; set; }

    public DateTime? Timespan { get; set; }
}
