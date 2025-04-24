using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Gespline;

public partial class Centro
{
    public string Codigocentro { get; set; } = null!;

    public string? Nombre { get; set; }

    public int? Estado { get; set; }

    public DateTime? Timespan { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
