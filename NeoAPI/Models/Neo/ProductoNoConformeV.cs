using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class ProductoNoConformeV
{
    public int IdRegistro { get; set; }

    public string CodigoDeProducto { get; set; } = null!;

    public string DescripcionDeProducto { get; set; } = null!;

    public string? Lote { get; set; }

    public string? OrdenDeFabricacion { get; set; }

    public string NoConformidad { get; set; } = null!;

    public int Cantidad { get; set; }

    public string CausaDeLiberación { get; set; } = null!;

    public string IndicacionDeLiberacion { get; set; } = null!;

    public string FichaDelCargador { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public string DisposicionDefinitiva { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string Causante { get; set; } = null!;

    public string Unidad { get; set; } = null!;

    public string Identificacion { get; set; } = null!;

    public string AlternativaPropuestoDeDisposicion { get; set; } = null!;

    public string LugarEvento { get; set; } = null!;

    public DateTime Fecha { get; set; }
}
