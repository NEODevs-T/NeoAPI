using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class CausaCal
{
    public int IdCausaCal { get; set; }

    public string Ccnombre { get; set; } = null!;

    public string? Ccdescri { get; set; }

    public DateTime Ccfecha { get; set; }

    public bool Ccestado { get; set; }

    public string Ccenglish { get; set; } = null!;

    public virtual ICollection<Reunion> Reunions { get; set; } = new List<Reunion>();
}
