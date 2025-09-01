using System;
using System.Collections.Generic;

namespace NeoAPI.DTO.Gespline;

public partial class EntradaejecucionDTO
{
    public int Codigoentradaejecucion { get; set; }

    public int? Codigotupla { get; set; }

    public string? Codigoturno { get; set; }

    public string? Codigosupervisor { get; set; }

    public DateTime? Fechaentrada { get; set; }

    public double? Horasejecutadas { get; set; }

    public double? Cantidadesechasenentrada { get; set; }

    public double? Cantidadesechassensor { get; set; }

    public double? Cantidadesechassensor2 { get; set; }

    public double? Cantidadesechassensor3 { get; set; }

    public double? Desperdicioxproduccion { get; set; }

    public double? Desperdicioxpuestamarcha { get; set; }

    public double? Avancex100enentrada { get; set; }

    public double? Standardelentradaejecutado { get; set; }

    public double? Standardelpuesto { get; set; }

    public double? Velocidadentrada { get; set; }

    public double? Productividadentrada { get; set; }

    public string? Horasdiurnas { get; set; }

    public string? Horasnocturnas { get; set; }

    public string? Horasfestivas { get; set; }

    public string? Lote { get; set; }

    public double? Unidadesreperocesadas { get; set; }

    public int? Cerrosinoturno { get; set; }

    public int? Sionoreprocesoretenidas { get; set; }

    public DateTime? FechaUltimoDato { get; set; }

    public string? Usuario { get; set; }

    public int? Sionocapturamanual { get; set; }

    public double? Unidadessensor1literal { get; set; }

    public DateTime? Timespan { get; set; }

    public double? Factormultiplicadorejecutado { get; set; }

    public bool? Modificafactormultiplicadorejecutado { get; set; }

}
