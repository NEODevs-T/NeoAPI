using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class UsuariosV
{
    public int IdNivel { get; set; }

    public int IdUsuario { get; set; }

    public int IdProyecto { get; set; }

    public int? IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Usuario { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string Proyecto { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public string Empresa { get; set; } = null!;

    public string Centro { get; set; } = null!;

    public string División { get; set; } = null!;

    public string Linea { get; set; } = null!;

    public int IdPais { get; set; }

    public int IdEmpresa { get; set; }

    public int IdCentro { get; set; }

    public int IdDivision { get; set; }

    public int IdLinea { get; set; }

    public int IdMaster { get; set; }

    public bool UsEstatus { get; set; }
}
