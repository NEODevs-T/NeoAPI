using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.Models;
using NeoAPI.DTOs.Asentamientos;
namespace NeoAPI.DTOs.Asentamientos
{
    public class InformeConAsentamientosDTO
    {
        public InfoAseDTO InformaDeAsentamientosDTO { get; set; } = null!;
        //public List<Asentum>? Asentamientos { get; set; } = null!;
        public List<AsentumDTO>? AsentamientosDTO { get; set; } = null!;
    }
}