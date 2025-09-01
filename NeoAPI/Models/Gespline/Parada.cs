using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Gespline;

public partial class Parada
{
    public string Codigoparada { get; set; } = null!;

    public string? Nombreparada { get; set; }

    public bool? Sionoprogramada { get; set; }

    public string? Codigogrupoparada { get; set; }

    public int? Estado { get; set; }

    public DateTime? Timespan { get; set; }

    public string? Codigoegp { get; set; }

    public virtual Gruposdeparada? CodigogrupoparadaNavigation { get; set; }

    public virtual ICollection<Paradasejecutada> Paradasejecutada { get; set; } = new List<Paradasejecutada>();

    public virtual ICollection<Standarparada> Standarparada { get; set; } = new List<Standarparada>();

    public virtual ICollection<Transmicionweb> Transmicionwebs { get; set; } = new List<Transmicionweb>();
}
