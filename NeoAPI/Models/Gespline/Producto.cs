using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Gespline;

/// <summary>
/// Tabla que relaciona los productos
/// </summary>
public partial class Producto
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

    public string? Referencia { get; set; }

    public string? Grupoarticulos { get; set; }

    public virtual Centro? CodigocentroNavigation { get; set; }

    public virtual ICollection<Ordenproduccionxproducto> Ordenproduccionxproductos { get; set; } = new List<Ordenproduccionxproducto>();

    public virtual ICollection<Productoporproceso> Productoporprocesos { get; set; } = new List<Productoporproceso>();

    public virtual ICollection<Standarparada> Standarparada { get; set; } = new List<Standarparada>();
}
