using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Gespline;

public partial class OrdenesDeProduccion
{
    public string Codigoordenproduccion { get; set; } = null!;

    public string? CodigoBodega { get; set; }

    public string? Nombre { get; set; }

    public string? Codigopedido { get; set; }

    public string? Codigobarras { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public int? Estado { get; set; }

    public string? DocumentoRemicion { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? Usuario { get; set; }

    public DateTime? Timespan { get; set; }

    public virtual ICollection<Ordenproduccionxproducto> Ordenproduccionxproductos { get; set; } = new List<Ordenproduccionxproducto>();
}
