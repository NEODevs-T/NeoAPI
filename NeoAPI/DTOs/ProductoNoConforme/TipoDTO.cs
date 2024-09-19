using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.PNC;

public class TipoDTO
{
    public int IdTipo { get; set; }

    public string Tnombre { get; set; } = null!;

    public string? Tdescri { get; set; }

    public bool Testado { get; set; }
}