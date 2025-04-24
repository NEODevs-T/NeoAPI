using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Gespline;

public partial class Proceso
{
    public string Codigoproceso { get; set; } = null!;

    public string? Codigoarea { get; set; }

    public string? Descripcion { get; set; }

    public int? Estado { get; set; }

    public DateTime? Timespan { get; set; }

    public virtual ICollection<Productoporproceso> Productoporprocesos { get; set; } = new List<Productoporproceso>();
}
