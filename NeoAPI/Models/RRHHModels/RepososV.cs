using System;
using System.Collections.Generic;

namespace NeoAPI.RRHHModels;

public partial class RepososV
{
    public string? Ficha { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Departamento { get; set; }

    public DateOnly? FechaDesde { get; set; }

    public DateOnly? Reintegro { get; set; }

    public DateOnly? FechaHasta { get; set; }

    public string? DescripcionCausa { get; set; }

    public int? CanDiasReposo { get; set; }

    public string? OrigenReposo { get; set; }

    public string? Compania { get; set; }

    public string? TipoNomina { get; set; }
}
