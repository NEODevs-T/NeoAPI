using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class TabuladoresBonoV
{
    public int IdMontoBon { get; set; }

    public double Monto { get; set; }

    public string Moneda { get; set; } = null!;

    public string? TipoIncidencia { get; set; }

    public string Linea { get; set; } = null!;

    public string Planta { get; set; } = null!;

    public int IdMaster { get; set; }

    public string Puesto { get; set; } = null!;

    public int Escalon { get; set; }

    public double PorcentajeMin { get; set; }

    public double PorcentajeMax { get; set; }

    public string AreaEscalon { get; set; } = null!;

    public bool Estado { get; set; }
}
