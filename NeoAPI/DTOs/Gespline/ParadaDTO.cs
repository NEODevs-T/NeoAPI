using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Gespline;

public partial class ParadaDTO
{
    public string Codigoparada { get; set; } = null!;

    public string? Nombreparada { get; set; }

    public bool? Sionoprogramada { get; set; }

    public string? Codigogrupoparada { get; set; }

    public int? Estado { get; set; }

    public DateTime? Timespan { get; set; }

    public string? Codigoegp { get; set; }
    
}

public class ParadaActualDto
{
    public string CodRegistro { get; set; }
    public string CodigoGrupo { get; set; }
    public string NombreParada { get; set; }
    public string TiempoPerdido { get; set; }
    public string ParteNombre { get; set; }
    public string CodigoParte { get; set; }
}