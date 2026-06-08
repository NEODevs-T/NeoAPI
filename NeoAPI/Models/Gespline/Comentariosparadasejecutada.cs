using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Gespline;

public partial class Comentariosparadasejecutada
{
    public int Id { get; set; }

    public int Idparadaejecutada { get; set; }

    public string? Comentarios { get; set; }

    public DateTime? Timespan { get; set; }

    public virtual Paradasejecutada IdparadaejecutadaNavigation { get; set; } = null!;
}
