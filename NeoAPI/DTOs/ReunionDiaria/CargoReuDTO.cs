using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.ReunionDiaria;

public partial class CargoReuDTO
{

    public int IdCargoR { get; set; }

    public string Crnombre { get; set; } = null!;

    public bool Cresta { get; set; }

    public string Crempresa { get; set; } = null!;

    public string? Crarea { get; set; }

    public int? Crbloque { get; set; }
}
