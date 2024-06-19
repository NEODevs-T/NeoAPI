using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class Rol
{
    public int IdRol { get; set; }

    public string Rnombre { get; set; } = null!;

    public bool Restado { get; set; }

    public string? Rdescri { get; set; }

    public virtual ICollection<Nivel> Nivels { get; set; } = new List<Nivel>();
}
