using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Gespline;

public partial class Standarparada
{
    public int IdStandar { get; set; }

    public string? Codigoparada { get; set; }

    public string? Codigopuesto { get; set; }

    public string? Codigoproductos { get; set; }

    public double? Standarparada1 { get; set; }

    public DateTime? Timespan { get; set; }

    public virtual Parada? CodigoparadaNavigation { get; set; }

    public virtual Producto? CodigoproductosNavigation { get; set; }

    public virtual Puestosdetrabajo? CodigopuestoNavigation { get; set; }
}
