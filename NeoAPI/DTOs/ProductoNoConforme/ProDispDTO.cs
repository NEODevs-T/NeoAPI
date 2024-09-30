using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.PNC;

public class ProDispDTO
{
    public int IdProDisp { get; set; }

    public string Pdnombre { get; set; } = null!;

    public string? Pddescri { get; set; }

    public bool Pdestado { get; set; }

}