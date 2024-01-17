using NeoAPI.Models;
using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Asentamientos;

public partial class AsentumDTO
{
    public int IdAsenta { get; set; }

    public int IdRango { get; set; }

    public int IdInfoAse { get; set; }

    public double Avalor { get; set; }

    public bool AisActivo { get; set; }
    public virtual ICollection<CorteDiscDTO> CorteDiscDTO { get; set; } = new List<CorteDiscDTO>();
    public virtual InfoAseDTO? InfoAseDTONavigation { get; set; } = null!;
    public virtual RangoDTO? RangoDTONavigation { get; set; } = null!;

}