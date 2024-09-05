using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Maestra;

public partial class DivisionesVDTO
{
    public int IdCentro { get; set; }

    public int IdDivision { get; set; }

    public string? Ndivision { get; set; }

    public bool? Estado { get; set; }
}
