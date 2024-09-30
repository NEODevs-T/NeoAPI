using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.GlobalCo;

public class PersonalDTO
{
    public string primerNombre { get; set; } = "";
    public string segundoNombre { get; set;} = "";
    public string primerApellido { get; set;} = "";
    public string segundoApellido { get; set;} = "";
    public string departamento { get; set;} = ""; 
    public string cargo { get; set;} = "";
    public string compania { get; set;} = "";
}