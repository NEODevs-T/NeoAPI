using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Maestra;

public class DivisionDTO
{
    public int IdDivision { get; set; }

    public string Dnombre { get; set; } = null!;

    public string? Ddetalle { get; set; }

    public bool Destado { get; set; }

    public DateTime Dfecha { get; set; }
}