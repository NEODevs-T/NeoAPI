using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.PNC;

public class UnidadeDTO
{
    public int IdUnidad { get; set; }

    public string Unombre { get; set; } = null!;

    public string? Udescri { get; set; }

    public bool Uestado { get; set; }

}
