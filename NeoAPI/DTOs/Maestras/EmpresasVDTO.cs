using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Maestra;

public partial class EmpresasVDTO
{
    public int IdPais { get; set; }

    public int IdEmpresa { get; set; }

    public string Empresa { get; set; } = null!;

    public bool Estado { get; set; }
}
