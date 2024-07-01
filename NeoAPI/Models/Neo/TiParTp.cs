using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class TiParTp
{
    public int IdTiParTp { get; set; }

    public string Tpcodigo { get; set; } = null!;

    public string Tpnombre { get; set; } = null!;

    public bool Tpestado { get; set; }

    public virtual ICollection<LibroNove> LibroNoves { get; set; } = new List<LibroNove>();

    public virtual ICollection<ParaTp> ParaTps { get; set; } = new List<ParaTp>();
}
