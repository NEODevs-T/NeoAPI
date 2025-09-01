using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Gespline;

public partial class Productoporproceso
{
    public string Codigoproductos { get; set; } = null!;

    public string Codigoproceso { get; set; } = null!;

    public DateTime? Timespan { get; set; }

    public virtual Proceso CodigoprocesoNavigation { get; set; } = null!;

    public virtual Producto CodigoproductosNavigation { get; set; } = null!;

    public virtual ICollection<Puestotrabajosegunproducto> Puestotrabajosegunproductos { get; set; } = new List<Puestotrabajosegunproducto>();

    public virtual ICollection<Tuplaejecucion> Tuplaejecucions { get; set; } = new List<Tuplaejecucion>();
}
