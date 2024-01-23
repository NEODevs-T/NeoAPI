using System;
using System.Collections.Generic;

namespace NeoAPI.Models;

public partial class Unidad
{
    public int IdUnidad { get; set; }

    public string Unombre { get; set; } = null!;

    public string? Udescri { get; set; }

    public DateTime UfechaCrea { get; set; }

    public bool Uestado { get; set; }

    public virtual ICollection<Variable> Variables { get; set; } = new List<Variable>();
}
