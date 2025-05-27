using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Gespline;

public partial class ProductoporprocesoDTO
{
    public string Codigoproductos { get; set; } = null!;

    public string Codigoproceso { get; set; } = null!;

    public DateTime? Timespan { get; set; }
    
    }
