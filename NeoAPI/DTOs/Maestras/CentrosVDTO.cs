using System;
using System.Collections.Generic;

namespace DTOs.Maestra;

public class CentrosVDTO
{
    public int IdCentro { get; set; }

    public string Cnom { get; set; } = null!;

    public string? Cdetalle { get; set; }

    public bool Cestado { get; set; }

    public DateTime Cfecha { get; set; }
}