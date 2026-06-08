using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Gespline;

public partial class Puestotrabajosegunproducto
{
    public string Codigoproductos { get; set; } = null!;

    public string Codigoproceso { get; set; } = null!;

    public string Codigopuesto { get; set; } = null!;

    public int? Predeterminada { get; set; }

    public double? Velocidadestandar { get; set; }

    public double? Timpoesperap { get; set; }

    public double? Tiemposalidaparada { get; set; }

    public double? Unidadessalidaparada { get; set; }

    public string? Codmatdesperdppal { get; set; }

    public double? Multiplicadorsensor { get; set; }

    public double? Factorconversiondesperdicio { get; set; }

    public double? Factorconversiondesmppal { get; set; }

    public double? Factorconversionproduccionfinal { get; set; }

    public double? Factorvariable1 { get; set; }

    public double? Multiplicadorsensor2y3 { get; set; }

    public DateTime? Timespan { get; set; }

    public double? Factorvariable2 { get; set; }

    public virtual Puestosdetrabajo CodigopuestoNavigation { get; set; } = null!;

    public virtual Productoporproceso Productoporproceso { get; set; } = null!;

    public virtual ICollection<Tuplaejecucion> Tuplaejecucions { get; set; } = new List<Tuplaejecucion>();
}
