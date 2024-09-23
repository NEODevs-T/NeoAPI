using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.PNC;

public  class IdentifDTO
{
    public int IdIdentif { get; set; }

    public string Inombre { get; set; } = null!;

    public string? Idescri { get; set; }

    public bool Iestado { get; set; }

}
