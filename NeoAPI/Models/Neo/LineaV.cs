using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class LineaV
{
    public int IdDivision { get; set; }

    public int IdLinea { get; set; }

    public string Linea { get; set; } = null!;

    public string LcenCos { get; set; } = null!;

    public bool Estado { get; set; }
}
