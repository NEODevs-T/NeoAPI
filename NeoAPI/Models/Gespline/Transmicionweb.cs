using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Gespline;

public partial class Transmicionweb
{
    public string Codigopuesto { get; set; } = null!;

    public string? Codigoparada { get; set; }

    public int? Codigoentradaejecucion { get; set; }

    public string? Estadopuesto { get; set; }

    public string? Estadosoftware { get; set; }

    public double? Tiempoparado { get; set; }

    public DateTime? Horaultimodato { get; set; }

    public DateTime? Horaparada { get; set; }

    public DateTime? Timespan { get; set; }

    public virtual Entradaejecucion? CodigoentradaejecucionNavigation { get; set; }

    public virtual Parada? CodigoparadaNavigation { get; set; }

    public virtual Puestosdetrabajo CodigopuestoNavigation { get; set; } = null!;
}
