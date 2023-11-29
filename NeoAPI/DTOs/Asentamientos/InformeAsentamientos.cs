using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.Models;

namespace NeoAPI.DTOs.Asentamientos
{
    public class InformeConAsentamientos
    {
        public Models.InfoAse? InformaDeAsentamientos { get; set; } = null!;
        public List<Models.Asentum>? Asentamientos { get; set; } = null!;
    }
}