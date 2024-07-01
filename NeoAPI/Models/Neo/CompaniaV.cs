using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class CompaniaV
{
    public int IdDimcompania { get; set; }

    public string? DescripcionCia { get; set; }

    public string? DescripcionCia2 { get; set; }

    public string? PaisDescripcionEsp { get; set; }

    public string? PaisDescripcionEng { get; set; }

    public string? GrupoPaisDescripcion { get; set; }

    public int CodRegion { get; set; }

    public string? RegionPapelera { get; set; }
}
