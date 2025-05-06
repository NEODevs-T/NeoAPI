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

public class ParadaActual1TurnoDTO
{
    public string CodigoRegistro { get; set; }
    public string CodigoGrupoParada { get; set; }
    public string NombreParada { get; set; }
    public string TiempoPerdido { get; set; }
    public string ParteNombre { get; set; }
    public string CodigoParte { get; set; }
}

public class ParadaActual1TurnoAgrupadoDTO
{
    
    public string CodigoParada { get; set; }

    public string CodigoGrupoParada { get; set; }

    public string ACodGes { get; set; }

    public string NombreParada { get; set; }

    public string Aparte { get; set; }

    public string TiempoPerdido { get; set; }

}

public class ParadasActuales2turnoAntesDeLas0amDTO
{
    public string CodigoRegistro { get; set; }
    public string CodigoGrupoParada { get; set; }
    public string NombreParada { get; set; }
    public string TiempoPerdido { get; set; }
    public string ParteNombre { get; set; }
    public string CodigoParte { get; set; }
}

public class ParadasActuales2turnoDespuesDeLas0amDTO
{
    public string CodigoRegistro { get; set; }
    public string CodigoGrupoParada { get; set; }
    public string NombreParada { get; set; }
    public string TiempoPerdido { get; set; }
    public string ParteNombre { get; set; }
    public string CodigoParte { get; set; }
}

public class ParadasActuales2turnoAntesDeLas0amAgrupadasDTO
{
    public string CodigoParada { get; set; }

    public string CodigoGrupoParada { get; set; }

    public string ACodGes { get; set; }

    public string NombreParada { get; set; }

    public string Aparte { get; set; }

    public string TiempoPerdido { get; set; }

}

public class ParadasActuales2turnoDespuesDeLas0amAgrupadasDTO
{
    public string CodigoParada { get; set; }

    public string CodigoGrupoParada { get; set; }

    public string ACodGes { get; set; }

    public string NombreParada { get; set; }

    public string Aparte { get; set; }

    public string TiempoPerdido { get; set; }

}