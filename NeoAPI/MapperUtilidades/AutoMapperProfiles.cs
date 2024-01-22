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
            CreateMap<CorteDiscDTO, CorteDi>().ReverseMap()
              .ForMember(dest => dest.CategoriaDTONavigation, opt => opt.MapFrom(src => src.IdCategoriNavigation))
              .ForMember(dest => dest.AsentumDTONavigation, opt => opt.MapFrom(src => src.IdAsentaNavigation));

            CreateMap<Categori, CategoriaDTO>().ReverseMap();

            CreateMap<AsentumDTO, Asentum>().ForMember(dest => dest.CorteDis, act => act.MapFrom(c => c.CorteDiscDTO));

            CreateMap<Asentum, AsentumDTO>()
            .ForMember(dest => dest.CorteDiscDTO, act => act.MapFrom(c => c.CorteDis))
            .ForMember(dest => dest.RangoDTONavigation, act => act.MapFrom(c => c.IdRangoNavigation))
            .ForMember(dest => dest.InfoAseDTONavigation, act => act.MapFrom(c => c.IdInfoAseNavigation))
            .ForPath(dest => dest.RangoDTONavigation.VariableDTONavigation.UnidadDTONavigation, opt => opt.MapFrom(src => src.IdRangoNavigation.IdVariableNavigation.IdUnidadNavigation))
            .ForPath(dest => dest.RangoDTONavigation.VariableDTONavigation, opt => opt.MapFrom(src => src.IdRangoNavigation.IdVariableNavigation))
            .ForPath(dest => dest.RangoDTONavigation.ProductoDTONavigation, opt => opt.MapFrom(src => src.IdRangoNavigation.IdProductoNavigation))
            .ForPath(dest => dest.RangoDTONavigation.VariableDTONavigation.SeccionDTONavigation, opt => opt.MapFrom(src => src.IdRangoNavigation.IdVariableNavigation.IdSeccionNavigation));
   

            CreateMap<Unidad, UnidadDTO>().ReverseMap();
            CreateMap<Seccion, SeccionDTO>().ReverseMap();
            CreateMap<Producto, ProductosDTO>().ReverseMap();
            CreateMap<InfoAse, InfoAseDTO>().ForMember(destino => destino.AsentaDTO, actual => actual.MapFrom(i => i.Asenta)).ReverseMap();

            CreateMap<Rango, RangoDTO>().ForMember(destino => destino.VariableDTONavigation, actual => actual.MapFrom(r => r.IdVariableNavigation)).ReverseMap();

            CreateMap<Variable, VariableDTO>().ForMember(destino => destino.UnidadDTONavigation, actual => actual.MapFrom(u => u.IdUnidadNavigation));

            CreateMap<CorteDi, CorteDiscDTO>()
           .ForMember(dest => dest.CategoriaDTONavigation, opt => opt.MapFrom(src => src.IdCategoriNavigation))
           .ForMember(dest => dest.AsentumDTONavigation, opt => opt.MapFrom(src => src.IdAsentaNavigation));
           //.ForPath(dest => dest.AsentumDTONavigation.RangoDTONavigation, opt => opt.MapFrom(src => src.IdAsentaNavigation.IdRangoNavigation))
           //.ForPath(dest => dest.AsentumDTONavigation.InfoAseDTONavigation, opt => opt.MapFrom(src => src.IdAsentaNavigation.IdInfoAseNavigation))
           //.ForPath(dest => dest.AsentumDTONavigation.RangoDTONavigation.VariableDTONavigation.UnidadDTONavigation, opt => opt.MapFrom(src => src.IdAsentaNavigation.IdRangoNavigation.IdVariableNavigation.IdUnidadNavigation))
           //.ForPath(dest => dest.AsentumDTONavigation.RangoDTONavigation.VariableDTONavigation, opt => opt.MapFrom(src => src.IdAsentaNavigation.IdRangoNavigation.IdVariableNavigation))
           //.ForPath(dest => dest.AsentumDTONavigation.RangoDTONavigation.VariableDTONavigation.SeccionDTONavigation, opt => opt.MapFrom(src => src.IdAsentaNavigation.IdRangoNavigation.IdVariableNavigation.IdSeccionNavigation))
           //.ReverseMap();



            //https://www.youtube.com/watch?v=pr_pemcmVAs
        }
    }
}
