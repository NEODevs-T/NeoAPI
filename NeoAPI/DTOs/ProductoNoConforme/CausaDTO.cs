using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.PNC;


public partial class causaDTO
{
    public int IdCausa { get; set; }

    public int IdCausante { get; set; }

    public string Cnombre { get; set; } = null!;

    public string? Cdescri { get; set; }

    public bool Cestado { get; set; }
}
