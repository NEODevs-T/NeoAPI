using System;
using System.Collections.Generic;

namespace NeoAPI.Models;

public partial class InfoAse
{
    public int IdInfoAse { get; set; }

    public string Iagrupo { get; set; } = null!;

    public string Iaturno { get; set; } = null!;

    public string Iaficha { get; set; } = null!;

    public string? IafichaCor { get; set; }

    public string? Iaobser { get; set; }

    public DateTime IafechCrea { get; set; }

    public virtual ICollection<Asentum> Asenta { get; set; } = new List<Asentum>();

    public virtual ICollection<InfoCali> InfoCalis { get; set; } = new List<InfoCali>();
}
