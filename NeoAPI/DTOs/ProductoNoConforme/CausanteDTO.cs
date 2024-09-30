using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.PNC;
public class CausanteDTO
{
    public int IdCausante { get; set; }

    public string Cnombre { get; set; } = null!;

    public string? Cdescri { get; set; }

    public bool Cestado { get; set; }

}
