using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Maestra;

public partial class DivisionesVDTO
{
    public int IdCentro { get; set; }

    public int IdDivision { get; set; }

    public string Ndivision { get; set; } = null!;

    public bool Estado { get; set; }

    public int IdLinea { get; set; }

    public int? IdMaster { get; set; }

    public string Lnom { get; set; } = null!;
}
