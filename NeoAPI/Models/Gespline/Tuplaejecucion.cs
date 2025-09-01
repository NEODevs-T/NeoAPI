using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Gespline;

public partial class Tuplaejecucion
{
    public int Codigotupla { get; set; }

    public string Codigoordenproduccion { get; set; } = null!;

    public string Codigoproductos { get; set; } = null!;

    public string Codigoproductosasparte { get; set; } = null!;

    public string Codigoproceso { get; set; } = null!;

    public string Codigopuesto { get; set; } = null!;

    public DateTime? Timespan { get; set; }

    public virtual ICollection<Entradaejecucion> Entradaejecucions { get; set; } = new List<Entradaejecucion>();

    public virtual Ordenproduccionxproducto Ordenproduccionxproducto { get; set; } = null!;

    public virtual Productoporproceso Productoporproceso { get; set; } = null!;

    public virtual Puestotrabajosegunproducto Puestotrabajosegunproducto { get; set; } = null!;
}
