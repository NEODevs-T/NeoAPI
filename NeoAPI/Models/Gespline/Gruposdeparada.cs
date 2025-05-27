using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Gespline;

public partial class Gruposdeparada
{
    public string Codigogrupoparada { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public string? Codigoegp { get; set; }

    public DateTime? Timespan { get; set; }

    public virtual ICollection<Parada> Parada { get; set; } = new List<Parada>();
}
