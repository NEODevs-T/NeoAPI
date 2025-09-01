using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Maestra;

public class EmpresaDTO
{
    public int IdEmpresa { get; set; }

    public string Enombre { get; set; } = null!;

    public string? Edescri { get; set; }

    public bool Eestado { get; set; }

    public int IdCompania { get; set; }

    public DateTime Efecha { get; set; }
}