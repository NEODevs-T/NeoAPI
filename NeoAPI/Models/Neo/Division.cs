using System;
using System.Collections.Generic;

namespace NeoAPI.Models;

public partial class Division
{
    public int IdDivision { get; set; }

    public string? Dnombre { get; set; }

    public string? Ddetalle { get; set; }

    public bool? Destado { get; set; }

    public virtual ICollection<Master> Masters { get; set; } = new List<Master>();
}
