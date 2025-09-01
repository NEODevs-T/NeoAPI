using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Gespline;

public partial class ParteDTO
{
    public int PartesId { get; set; }

    public string ParteNombre { get; set; } = null!;

    public string? Codigo { get; set; }
}
