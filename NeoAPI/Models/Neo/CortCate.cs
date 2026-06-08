using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class CortCate
{
    public int IdCortCate { get; set; }

    public string Ccnombre { get; set; } = null!;

    public string? Ccdesc { get; set; }

    public string? Cccodigo { get; set; }
}
