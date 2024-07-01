using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class ProyectoUsr
{
    public int IdProyecto { get; set; }

    public string Pnombre { get; set; } = null!;

    public bool Pestado { get; set; }

    public virtual ICollection<Nivel> Nivels { get; set; } = new List<Nivel>();
}
