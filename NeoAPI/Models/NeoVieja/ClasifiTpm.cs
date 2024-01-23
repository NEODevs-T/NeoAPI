using System;
using System.Collections.Generic;

namespace NeoAPI.Models.NeoVieja;

public partial class ClasifiTpm
{
    public int IdCtpm { get; set; }

    public string Ctpmnom { get; set; } = null!;

    public bool Ctpmestado { get; set; }

    public virtual ICollection<LibroNove> LibroNoves { get; set; } = new List<LibroNove>();
}
