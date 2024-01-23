using System;
using System.Collections.Generic;

namespace NeoAPI.ModelsViews;

public partial class VariablesAsentamientosV
{
    public int IdVariable { get; set; }

    public string NombreDeLaVariable { get; set; } = null!;

    public string? DescripcionDeLaVariable { get; set; }

    public bool IsObservable { get; set; }

    public string Tipo { get; set; } = null!;

    public string Clasificación { get; set; } = null!;

    public string Sección { get; set; } = null!;

    public string Unidad { get; set; } = null!;

    public string EquipoDeMedición { get; set; } = null!;

    public DateOnly FechaDeCreación { get; set; }
}
