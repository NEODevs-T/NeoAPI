using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class LineaV
{
    public int IdMaster { get; set; }

    public int IdDivision { get; set; }

    public int IdLinea { get; set; }

    public string Linea { get; set; } = null!;

    public string? LcenCos { get; set; }

    public bool Estado { get; set; }
}
