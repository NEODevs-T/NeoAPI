using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class TurnoTp
{
    public int IdTurnoTp { get; set; }

    public string ToperaFich { get; set; } = null!;

    public string Tturno { get; set; } = null!;

    public DateTime? Tfecha { get; set; }

    public string? TcodiProdu { get; set; }

    public double? Ttrabajado { get; set; }

    public double? Tperdido { get; set; }

    public int? Tpbueno { get; set; }

    public int? Tpmalo { get; set; }

    public double? Tvelocidad { get; set; }

    public double? Trendi { get; set; }

    public double? Tcalidad { get; set; }

    public double? Tdispo { get; set; }

    public double? Toee { get; set; }

    public virtual ICollection<ParsiOee> ParsiOees { get; set; } = new List<ParsiOee>();
}
