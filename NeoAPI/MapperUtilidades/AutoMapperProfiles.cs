using AutoMapper;
using NeoAPI.DTOs.Asentamientos;
using NeoAPI.DTOs.Bonificaciones;
using NeoAPI.DTOs.LibroNovedades;
using NeoAPI.DTOs.BPSC;
using NeoAPI.Models.Neo;
using NeoAPI.Models.PolybaseBPCSVen;
using NeoAPI.Models.PolybaseBPCSCol;
using NeoAPI.Models.PolybaseBPCSCen;
using System.Collections.Generic;
using NeoAPI.DTOs.ReunionDiaria;
using NeoAPI.DTOs.Maestra;

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

            CreateMap<LibroNoveDTO, LibroNove>().ReverseMap();

            CreateMap<ClasifiTpmDTO, ClasifiTpm>().ReverseMap();

            CreateMap<TiParTpDTO, TiParTp>().ReverseMap();

            CreateMap<CambStatDTO, CambStat>().ReverseMap();

            CreateMap<CambFecDTO, CambFec>().ReverseMap();

            CreateMap<ReuDiumDTO, ReuDium>().ReverseMap();

            CreateMap<CentrosVDTO, CentrosV>().ReverseMap();

            CreateMap<DivisionesVDTO, DivisionesV>().ReverseMap();

            CreateMap<EmpresasVDTO, EmpresasV>().ReverseMap();

            CreateMap<EquipoEamDTO, EquipoEam>().ReverseMap();

            CreateMap<LineaVDTO, LineaV>().ReverseMap();

            CreateMap<MasterDTO, Master>().ReverseMap();

            CreateMap<PaiDTO, Pai>().ReverseMap();

            CreateMap<CargoReuDTO, CargoReu>().ReverseMap();

            CreateMap<KsfDTO, Ksf>().ReverseMap();

            CreateMap<RespoReuDTO, RespoReu>().ReverseMap();

            CreateMap<Resuman, ResumenGeneralDTO>()
                .ForMember(dest => dest.Nombre, act => act.MapFrom(src => src.IdPersonalNavigation.PeNombre))
                .ForMember(dest => dest.Apellido, act => act.MapFrom(src => src.IdPersonalNavigation.PeApellido))
                .ForMember(dest => dest.Ficha, act => act.MapFrom(src => src.IdPersonalNavigation.PeFicha))
                .ForMember(dest => dest.Grupo, act => act.MapFrom(src => src.IdPersonalNavigation.PeGrupo))
                .ForMember(dest => dest.Turno, act => act.MapFrom(src => src.Rturno))
                .ForMember(dest => dest.Concepto, act => act.MapFrom(src => src.IdTipIncenNavigation.Tinombre))
                .ForMember(dest => dest.Suplencia, act => act.MapFrom(src => src.IdTipSupleNavigation.Tscausa))
                .ForMember(dest => dest.FichaSuplida, act => act.MapFrom(src => src.Rsuplido))
                .ForMember(dest => dest.Pais, act => act.MapFrom(src => src.IdMontosNavigation.IdLineaNavigation.Master.IdPaisNavigation.Pnombre))
                .ForMember(dest => dest.Empresa, act => act.MapFrom(src => src.IdMontosNavigation.IdLineaNavigation.Master.IdEmpresaNavigation.Enombre))
                .ForMember(dest => dest.Centro, act => act.MapFrom(src => src.IdMontosNavigation.IdLineaNavigation.Master.IdCentroNavigation.Cnom))
                .ForMember(dest => dest.Linea, act => act.MapFrom(src => src.IdMontosNavigation.IdLineaNavigation.Lnom))
                .ForMember(dest => dest.PuestoTrabajo, act => act.MapFrom(src => src.IdMontosNavigation.IdPuesTrabNavigation.Ptnombre))
                .ForMember(dest => dest.Monto, act => act.MapFrom(src => src.IdMontosNavigation.Mmonto))
                .ForMember(dest => dest.Moneda, act => act.MapFrom(src => src.IdMontosNavigation.IdMonedaNavigation.Mtipo))
                .ForMember(dest => dest.FechaResumen, act => act.MapFrom(src => src.Rfecha))
                .ForMember(dest => dest.FechaPago, act => act.MapFrom(src => src.RfecPago))
                .ForMember(dest => dest.FichaResumen, act => act.MapFrom(src => src.RuserVali))
                .ForMember(dest => dest.FichaPago, act => act.MapFrom(src => src.RuserPago))
                .ReverseMap();

            CreateMap<LibroNove, LibroNoveDTO>()
                .ForMember(dest => dest.Linea, act => act.MapFrom(src => src.IdMasterNavigation.IdLineaNavigation.Lnom))
                .ForMember(dest => dest.AreaCarga, act => act.MapFrom(src => src.IdAreaCarNavigation.Acnombre));

            CreateMap<EquipoEam, EquipoEamDTO>()
                .ForMember(dest => dest.Linea, act => act.MapFrom(src => src.IdLineaNavigation));

            CreateMap<Models.PolybaseBPCSVen.Fso, OrdenFabricacionDTO>()
                .ForMember(dest => dest.CodProducto, act => act.MapFrom(src => src.Sprod))
                .ForMember(dest => dest.Status, act => act.MapFrom(src => src.Sstat))
                .ReverseMap();

            CreateMap<Models.PolybaseBPCSCol.Fso, OrdenFabricacionDTO>()
                .ForMember(dest => dest.CodProducto, act => act.MapFrom(src => src.Sprod))
                .ForMember(dest => dest.Status, act => act.MapFrom(src => src.Sstat))
                .ReverseMap();

            CreateMap<Models.PolybaseBPCSCen.Fso, OrdenFabricacionDTO>()
                .ForMember(dest => dest.CodProducto, act => act.MapFrom(src => src.Sprod))
                .ForMember(dest => dest.Status, act => act.MapFrom(src => src.Sstat))
                .ReverseMap();


            // CreateMap<List<NeoAPI.Models.Neo.Pai>, List<NeoAPI.DTOs.Maestra.PaiDTO>>()
            //     .ConvertUsing(src => src.Select(pai => _mapper.Map<PaiDTO>(pai)));

            //https://www.youtube.com/watch?v=pr_pemcmVAs
        }
    }
}
