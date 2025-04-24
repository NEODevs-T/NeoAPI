using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Gespline;

public partial class ParadasejecutadaDTO
{
    public int Codigoregistrso { get; set; }

    public int? Codigoentradaejecucion { get; set; }

    public string? Codigoparada { get; set; }

    public string? Codigopersonalatiende { get; set; }

    public DateTime? Fechayhoraparada { get; set; }

    public double? Demoraparada { get; set; }

    public string? Diurnaonocturnaofestiva { get; set; }

    public double? Estandarprogramadas { get; set; }

    public string? Comentario { get; set; }

    public DateTime? Timespan { get; set; }

}
