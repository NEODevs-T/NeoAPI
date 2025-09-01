using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.LibroNovedades;

public class TiParTpDTO
{
    public int IdTiParTp { get; set; }

    public string Tpcodigo { get; set; } = null!;

    public string Tpnombre { get; set; } = null!;

    public bool Tpestado { get; set; }
}