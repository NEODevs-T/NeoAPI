using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Gespline;
public class ParadasActualesDTO
{
    public string CodigoRegistro { get; set; }
    public string CodigoGrupoParada { get; set; }
    public string NombreParada { get; set; }
    public double TiempoPerdido { get; set; }
    public string ParteNombre { get; set; }
    public string CodigoParte { get; set; }
}

public class ParadasActualesAgrupadasDTO
{

    public string CodigoParada { get; set; }

    public string CodigoGrupoParada { get; set; }

    public string ACodGes { get; set; }

    public string NombreParada { get; set; }

    public string Aparte { get; set; }

    public double TiempoPerdido { get; set; }

}

public class PrimeraParadaPorLineaDTO
{
    public string CodigoProceso {get; set;}

    public DateTime? FechaYHoraParada {get; set;}

    public DateTime? Timespan {get; set;}
}