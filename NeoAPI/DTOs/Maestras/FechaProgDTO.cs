using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Maestra;

public partial class FechaProgDTO
{
    public int IdFechaPr { get; set; }

    public DateTime Fpprogra { get; set; }

    public bool Fpestado { get; set; }

    public DateTime Fpcrea { get; set; }

    public DateTime Fpmodic { get; set; }

    public string? Fpdesc { get; set; }

    public int IdMaster { get; set; }

}
