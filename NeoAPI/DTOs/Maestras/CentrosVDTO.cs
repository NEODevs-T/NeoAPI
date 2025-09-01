using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Maestra;

public partial class CentrosVDTO
{
    public int IdEmpresa { get; set; }

    public int IdCentro { get; set; }

    public string Centro { get; set; } = null!;

    public bool Estado { get; set; }
}
