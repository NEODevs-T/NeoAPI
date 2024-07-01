using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class Tipo
{
    public int IdTipo { get; set; }

    public string Tnombre { get; set; } = null!;

    public string? Tdescri { get; set; }

    public bool Testado { get; set; }
}
