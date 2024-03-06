using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class ClasiVar
{
    public int IdClasiVar { get; set; }

    public string Cvnombre { get; set; } = null!;

    public string? Cvdescri { get; set; }

    public DateTime CvfechCrea { get; set; }

    public bool Cvestado { get; set; }

    public virtual ICollection<Variable> Variables { get; set; } = new List<Variable>();
}
