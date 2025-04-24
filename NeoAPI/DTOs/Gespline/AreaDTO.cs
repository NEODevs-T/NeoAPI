using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Gespline;

public partial class AreaDTO
{
    public int IdArea { get; set; }

    public string AcodGes { get; set; } = null!;

    public string Aparte { get; set; } = null!;

    public string AsubParte { get; set; } = null!;
}
