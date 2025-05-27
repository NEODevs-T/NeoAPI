using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Gespline;

public partial class TuplaejecucionDTO
{
    public int Codigotupla { get; set; }

    public string Codigoordenproduccion { get; set; } = null!;

    public string Codigoproductos { get; set; } = null!;

    public string Codigoproductosasparte { get; set; } = null!;

    public string Codigoproceso { get; set; } = null!;

    public string Codigopuesto { get; set; } = null!;

    public DateTime? Timespan { get; set; }


}
