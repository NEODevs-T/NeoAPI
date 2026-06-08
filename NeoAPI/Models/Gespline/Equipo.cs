using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Gespline;

public partial class Equipo
{
    public int Id { get; set; }

    public string Codigoequipo { get; set; } = null!;

    public string Nombreequipo { get; set; } = null!;

    public bool? Estado { get; set; }

    public DateTime? Timespan { get; set; }
}
