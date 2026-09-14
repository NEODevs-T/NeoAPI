using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Bono;

public partial class EstaAproba
{
    public int IdEstado { get; set; }

    public string? NombreEstado { get; set; }

    public virtual ICollection<ResumEspecial> ResumEspecials { get; set; } = new List<ResumEspecial>();
}
