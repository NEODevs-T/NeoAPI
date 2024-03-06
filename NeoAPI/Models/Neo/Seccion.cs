using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class Seccion
{
    public int IdSeccion { get; set; }

    public string Snombre { get; set; } = null!;

    public string? Sdescri { get; set; }

    public DateTime SfechaCrea { get; set; }

    public bool Sestado { get; set; }

    public virtual ICollection<Variable> Variables { get; set; } = new List<Variable>();
}
