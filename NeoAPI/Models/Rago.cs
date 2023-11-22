using System;
using System.Collections.Generic;

namespace NeoAPI.Models;

public partial class Rago
{
    public int IdRango { get; set; }

    public int IdMaster { get; set; }

    public int IdProducto { get; set; }

    public int IdVariable { get; set; }

    public double Rmin { get; set; }

    public double Rmax { get; set; }

    public double Robj { get; set; }

    public double RlimMin { get; set; }

    public double RlimMax { get; set; }

    public int Rorden { get; set; }

    public DateOnly RfechaCrea { get; set; }

    public bool Ractivo { get; set; }

    public DateOnly? RfechaDesa { get; set; }

    public string? RmotiDesa { get; set; }

    public virtual ICollection<Asentum> Asenta { get; set; } = new List<Asentum>();

    public virtual ICollection<BloqRang> BloqRangs { get; set; } = new List<BloqRang>();

    public virtual Master IdMasterNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Variable IdVariableNavigation { get; set; } = null!;
}
