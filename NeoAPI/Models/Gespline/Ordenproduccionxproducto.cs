using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Gespline;

public partial class Ordenproduccionxproducto
{
    public string Codigoordenproduccion { get; set; } = null!;

    public string Codigoproductos { get; set; } = null!;

    public double? Cantidad { get; set; }

    public DateTime? FechaInicioProducto { get; set; }

    public DateTime? FechaEntregaProducto { get; set; }

    public string? Usuario { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public double? DiasTotal { get; set; }

    public DateTime? Timespan { get; set; }

    public virtual OrdenesDeProduccion CodigoordenproduccionNavigation { get; set; } = null!;

    public virtual Producto CodigoproductosNavigation { get; set; } = null!;

    public virtual ICollection<Tuplaejecucion> Tuplaejecucions { get; set; } = new List<Tuplaejecucion>();
}
