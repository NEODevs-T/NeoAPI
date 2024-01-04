using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Asentamientos;

public partial class AsentumDTO
{
    public int IdAsenta { get; set; }

    public int IdRango { get; set; }

    public int IdInfoAse { get; set; }

    public double Avalor { get; set; }

    public bool AisActivo { get; set; }
}