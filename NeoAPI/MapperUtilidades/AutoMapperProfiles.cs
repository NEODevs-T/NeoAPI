using AutoMapper;
using NeoAPI.DTOs.Asentamientos;
using NeoAPI.Models;
using System.Collections.Generic;

namespace NeoAPI.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<CategoriaDTO, Categori>().ReverseMap();
            CreateMap<CorteDiscDTO, CorteDi>().ReverseMap();
            CreateMap<CorteDi, CorteDiscDTO>().ReverseMap();

            //https://www.youtube.com/watch?v=pr_pemcmVAs
        }
    }
}
