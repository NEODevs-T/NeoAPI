using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class MontosEscalonV
{
    public int IdPuesto { get; set; }

    public string Puesto { get; set; } = null!;

    public int? Escalon { get; set; }

    public double? Monto { get; set; }

    public string Moneda { get; set; } = null!;

    public string Linea { get; set; } = null!;

    public string? CentroCosto { get; set; }

    public string Centro { get; set; } = null!;
}
