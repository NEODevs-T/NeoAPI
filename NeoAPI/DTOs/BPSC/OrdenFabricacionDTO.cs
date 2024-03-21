using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.BPSC;

public partial class OrdenFabricacionDTO
{
    public string CodProducto { get; set; } = null!;
    public string Status { get; set; } = null!;
}