using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class SeccionesV
{
    public int IdMaster { get; set; }

    public int IdLinea { get; set; }

    public int IdSeccion { get; set; }

    public string Seccion { get; set; } = null!;

    public bool Estado { get; set; }
}
