using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Gespline;

public partial class ParadasejecutadaDTO
{
    public string CodigoRegistro { get; set; }
    public string CodigoGrupoParada { get; set; }
    public string NombreParada { get; set; }
    public int TiempoPerdido { get; set; }
    public string? ParteNombre { get; set; }
    public string? CodigoParte { get; set; }
}
