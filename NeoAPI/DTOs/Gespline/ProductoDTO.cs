using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Gespline;

/// <summary>
/// Tabla que relaciona los productos
/// </summary>
public partial class ProductoDTO
{
    public string Codigoproductos { get; set; } = null!;

    public string? Nombreproducto { get; set; }

    public string? Unidades { get; set; }

    public double? Pesoproducto { get; set; }

    public double? Volumenproducto { get; set; }

    public double? Presentacionproducto { get; set; }

    public int? Estadoproducto { get; set; }

    public string? Codigobarras { get; set; }

    public string? Codigocentro { get; set; }

    public DateTime? Timespan { get; set; }

}
