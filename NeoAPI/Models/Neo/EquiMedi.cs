using System;
using System.Collections.Generic;

namespace NeoAPI.Models;

public partial class EquiMedi
{
    public int IdEquiMedi { get; set; }

    public string Emnombre { get; set; } = null!;

    public string? Emdescri { get; set; }

    public DateTime EmfechCrea { get; set; }

    public bool Emestado { get; set; }

    public virtual ICollection<Variable> Variables { get; set; } = new List<Variable>();
}
