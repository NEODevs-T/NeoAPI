using System;
using System.Collections.Generic;

namespace NeoAPI.Models.NeoVieja;

public partial class AreaCarga
{
    public int IdAreaCarg { get; set; }

    public string Acnombre { get; set; } = null!;

    public string? Acdetalle { get; set; }

    public bool Acestado { get; set; }

    public virtual ICollection<LibroNove> LibroNoves { get; set; } = new List<LibroNove>();
}
