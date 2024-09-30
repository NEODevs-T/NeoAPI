using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.PNC;


public class DisDefiDTO
{
    public int IdDisDefi { get; set; }

    public string Ddnombre { get; set; } = null!;

    public string? Dddescri { get; set; }

    public bool Ddestado { get; set; }

}
