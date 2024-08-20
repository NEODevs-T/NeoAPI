using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.ReunionDiaria;

public class CambFecDTO
{
    //TODO: cambiar atributos
    public int IdCambFec { get; set; }

    public string Tpcodigo { get; set; } = null!;

    public string Tpnombre { get; set; } = null!;

    public bool Tpestado { get; set; }
}