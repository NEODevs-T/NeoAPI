using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.PNC;

public class CaUnidadDTO
{
    public int IdCaUnidad { get; set; }

    public string UNombre { get; set; } = null!;

    public string? UDescri { get; set; }

    public bool UEstado { get; set; }

}
