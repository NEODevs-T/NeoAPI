using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Bono;

public partial class ResumEspecial
{
    public int IdEspecial { get; set; }

    public int IdResumen { get; set; }

    public string Motivo { get; set; } = null!;

    public int IdEstado { get; set; }

    public DateTime FechaSolicitud { get; set; }

    public string UsuarioSolicita { get; set; } = null!;

    public virtual Resuman IdResumenNavigation { get; set; } = null!;

    public virtual ICollection<ResumEspecialAproba> ResumEspecialAprobas { get; set; } = new List<ResumEspecialAproba>();
}
