using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.ReunionDiaria;


public class CambStatDTO
{
    public int IdCambStat { get; set; }

    public int IdReuDia { get; set; }

    public DateTime? Cbfecha { get; set; }

    public string? Cbstat { get; set; }

    public string? Cbuser { get; set; }
}