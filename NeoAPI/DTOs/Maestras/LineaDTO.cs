using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Maestra;

public class LineaDTO
{
    public int IdLinea { get; set; }

    public string Lnom { get; set; } = null!;

    public string? Ldetalle { get; set; }

    public bool Lestado { get; set; }

    public string? LcenCos { get; set; }

    public DateTime Lfecha { get; set; }

    public string? Lofic { get; set; }
}