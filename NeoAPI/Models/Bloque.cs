using System;
using System.Collections.Generic;

namespace NeoAPI.Models;

public partial class Bloque
{
    public int IdBloque { get; set; }

    public TimeOnly BhoraInici { get; set; }

    public TimeOnly BhoraFinal { get; set; }

    public string? Bobserv { get; set; }

    public DateTime BfechaCrea { get; set; }

    public bool Bestado { get; set; }

    public virtual ICollection<BloqRang> BloqRangs { get; set; } = new List<BloqRang>();
}
