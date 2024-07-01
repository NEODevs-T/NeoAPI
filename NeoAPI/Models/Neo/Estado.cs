using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class Estado
{
    public int IdEstado { get; set; }

    public string Enombre { get; set; } = null!;

    public string? Edescri { get; set; }

    public bool Estatus { get; set; }
}
