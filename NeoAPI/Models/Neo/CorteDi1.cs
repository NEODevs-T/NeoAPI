using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class CorteDi1
{
    public int IdCortDisc { get; set; }

    public string? CdcodProd { get; set; }

    public string? Cdequipo { get; set; }

    public string? Cdvariable { get; set; }

    public string? Cdexpres { get; set; }

    public double? Cdmuestra { get; set; }

    public double? Cdmin { get; set; }

    public double? Cdmax { get; set; }

    public int IdCortCate { get; set; }

    public DateTime CdfechaAsen { get; set; }

    public string? CdplanAcc { get; set; }

    public double? Cdnuevo { get; set; }

    public bool? Cdresuelto { get; set; }

    public string? Cdcentro { get; set; }

    public string? Cdgrupo { get; set; }

    public string? CdtipoAsen { get; set; }

    public string? Cdcategoria { get; set; }

    public DateTime? CdfecDisc { get; set; }

    public bool? Cdcorte { get; set; }

    public int? Cdturno { get; set; }

    public string? Cdsuperv { get; set; }
}
