using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.ReunionDiaria;

public partial class CarReuDTO
{
    public int IdPais { get; set; }
    public string Pais { get; set; }
    public string Empresa { get; set; } = null!;
    public string Centro { get; set; }
    public int IdCargoR { get; set; }
    public string Crnombre { get; set; } = null!;
    public bool Cresta { get; set; }
    public int IdTipReu { get; set; }
}