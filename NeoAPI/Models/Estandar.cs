using System;
using System.Collections.Generic;

namespace NeoAPI.Models;

public partial class Estandar
{
    public int IdEstandar { get; set; }

    public string Enombre { get; set; } = null!;

    public double Edensid { get; set; }

    public double Econcent { get; set; }

    public string? Edescri { get; set; }

    public bool Eestado { get; set; }

    public DateTime EfechaCrea { get; set; }

    public virtual ICollection<Variable> Variables { get; set; } = new List<Variable>();
}
