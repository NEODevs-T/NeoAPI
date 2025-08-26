using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.ReunionDiaria;

public partial class CarReuDTO
{
    public int IdEmpresa { get; set; }
    public string Empresa { get; set; } = null!;
    public string Centro { get; set; }
    public int IdCargoR { get; set; }
    public string Crnombre { get; set; } = null!;
    public bool Cresta { get; set; }
    public int IdTipReu { get; set; }
}