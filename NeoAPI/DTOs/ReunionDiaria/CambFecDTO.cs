using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.ReunionDiaria;

public class CambFecDTO
{
    public int IdCambFec { get; set; }

    public int IdReuDia { get; set; }

    public DateTime Cffec { get; set; }

    public DateTime CffecNew { get; set; }

    public string Cfuser { get; set; } = null!;

    public virtual ReuDiumDTO? IdReuDiaNavigation { get; set; } = null!;

}