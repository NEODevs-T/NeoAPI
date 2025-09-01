using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.ReunionDiaria;

public partial class CausaCalDTO
{
    public int IdCausaCal { get; set; }

    public string Ccnombre { get; set; } = null!;

    public string? Ccdescri { get; set; }

    public DateTime Ccfecha { get; set; }

    public bool Ccestado { get; set; }

    public string Ccenglish { get; set; } = null!;
    
}