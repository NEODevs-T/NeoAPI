using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.ReunionDiaria;

public class StatsAsisDto
{
    public string Cargo { get; set; } = string.Empty;
    public double Asistencias { get; set; }
    public double Total { get; set; }
}

