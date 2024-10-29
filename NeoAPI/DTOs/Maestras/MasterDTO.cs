using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Maestra;

public class MasterDTO
{
    public int IdMaster { get; set; }

    public int IdCentro { get; set; }

    public int IdDivision { get; set; }

    public int IdPais { get; set; }

    public int IdEmpresa { get; set; }

    public int IdLinea { get; set; }

    public CentroDTO? Centro {get; set;}

    public DivisionDTO? Division {get; set;}

    public LineaDTO? Linea {get; set;}

}