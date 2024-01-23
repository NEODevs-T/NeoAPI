using System;
using System.Collections.Generic;

namespace NeoAPI.ModelsViews;

public partial class ValoresDeAsentamientosV
{
    public int IdAsenta { get; set; }

    public int IdInfoAse { get; set; }

    public double Valor { get; set; }

    public bool IsActivo { get; set; }

    public int IdRango { get; set; }

    public string Producto { get; set; } = null!;

    public string TipoDeProducto { get; set; } = null!;

    public string NombreDeLaVariable { get; set; } = null!;

    public double Minimo { get; set; }

    public double Maximo { get; set; }

    public double Objetivo { get; set; }

    public double LimiteMinimo { get; set; }

    public double LimiteMaximo { get; set; }

    public int? Orden { get; set; }

    public string Pais { get; set; } = null!;

    public string Empresa { get; set; } = null!;

    public string Centro { get; set; } = null!;

    public string? División { get; set; }

    public string Linea { get; set; } = null!;

    public string Clasificación { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public string Sección { get; set; } = null!;

    public string Unidad { get; set; } = null!;

    public string EquipoDeMedición { get; set; } = null!;

    public bool IsObservable { get; set; }

    public DateTime FechaDeCreacionDelRango { get; set; }
}
