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
            CreateMap<Categori, CategoriaDTO>().ReverseMap();

            CreateMap<CorteDi, CorteDiscDTO>()
            .ForMember(dest => dest.CategoriaDTONavigation, act => act.MapFrom(src => src.IdCategoriNavigation))
            .ReverseMap();

            //CreateMap<Categori, CategoriaDTO>().ForMember(dest => dest.CorteDisDTO, act => act.MapFrom(src => src.CorteDis))
            //.ReverseMap();

            //CreateMap<List<CorteDi>, CorteDiscDTO>()
            //.ForMember(dest => dest.IdCategoriNavigation, opt => opt.MapFrom(src => src.Select(c => c.IdCategoriNavigation).ToList()))
            //.ReverseMap();


         
            //https://www.youtube.com/watch?v=pr_pemcmVAs
        }
    }
}
