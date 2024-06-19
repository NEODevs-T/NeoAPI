using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class AsentamientosFueraRangoV
{
    public DateTime? Fecha { get; set; }

    public int IdAsenta { get; set; }

    public string Turno { get; set; } = null!;

    public double Valor { get; set; }

    public bool Activo { get; set; }

    public double Min { get; set; }

    public double Max { get; set; }

    public string Variable { get; set; } = null!;

    public string Unidad { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public string Empresa { get; set; } = null!;

    public string Centro { get; set; } = null!;

    public string? Division { get; set; }

    public string Linea { get; set; } = null!;

    public string Grupo { get; set; } = null!;

    public string Ficha { get; set; } = null!;

    public string? FichaCorte { get; set; }

    public string? Onservacion { get; set; }
}
