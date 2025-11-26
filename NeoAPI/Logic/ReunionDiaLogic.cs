using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.DTOs.Maestra;
using NeoAPI.Models.Neo;
using NeoAPI.Interface;
using NeoAPI.DTOs.ReunionDiaria;
using AutoMapper;

namespace NeoAPI.Logic.ReunionDia;

public class ReunionDiaLogic : IReunionDiaLogic
{
    public Master? centrodiscrepancia { get; set; } = new Master();
    private readonly DbNeoIiContext _neocontext;
    private readonly IMapper _mapper;

    public ReunionDiaLogic(DbNeoIiContext DbNeo)
    {
        _neocontext = DbNeo;
    }
    public async Task<CentroDivisionDTO> GetCentroDivi(string centro, string division, int tipo)
    {
        CentroDivisionDTO CD = new CentroDivisionDTO();


        if (tipo == 0)
        {
            int divisionInt;
            if (!int.TryParse(division, out divisionInt))
            {
                throw new FormatException("El valor de 'division' no es un número válido.");
            }

            centrodiscrepancia = await _neocontext.Masters
                .Include(c => c.IdCentroNavigation)
                .Include(d => d.IdDivisionNavigation)  // Asegúrate de incluir también IdDivisionNavigation
                .Where(d => d.IdDivision == divisionInt)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (centrodiscrepancia == null)
            {
                throw new NullReferenceException("No se encontró ninguna coincidencia para el centro o división proporcionados.");
            }

            // Verificar que las propiedades de navegación no sean nulas
            if (centrodiscrepancia.IdCentroNavigation == null)
            {
                throw new NullReferenceException("La propiedad 'IdCentroNavigation' es nula.");
            }
            if (centrodiscrepancia.IdDivisionNavigation == null)
            {
                throw new NullReferenceException("La propiedad 'IdDivisionNavigation' es nula.");
            }

            CD.IdCentro = centrodiscrepancia.IdCentroNavigation.IdCentro;
            CD.IdDivision = centrodiscrepancia.IdDivision;
            CD.Cnom = centrodiscrepancia.IdCentroNavigation.Cnom;
            CD.Dnombre = centrodiscrepancia.IdDivisionNavigation.Dnombre;
        }
        else if (tipo == 1)
        {
            centrodiscrepancia = await _neocontext.Masters
                .Include(c => c.IdCentroNavigation)
                .Include(d => d.IdDivisionNavigation)  // Asegúrate de incluir también IdDivisionNavigation
                .Where(d => d.IdDivisionNavigation.Dnombre == division && d.IdCentroNavigation.Cnom == centro)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (centrodiscrepancia == null)
            {
                throw new NullReferenceException("No se encontró ninguna coincidencia para el centro o división proporcionados.");
            }

            // Verificar que las propiedades de navegación no sean nulas
            if (centrodiscrepancia.IdCentroNavigation == null)
            {
                throw new NullReferenceException("La propiedad 'IdCentroNavigation' es nula.");
            }
            if (centrodiscrepancia.IdDivisionNavigation == null)
            {
                throw new NullReferenceException("La propiedad 'IdDivisionNavigation' es nula.");
            }

            CD.IdCentro = centrodiscrepancia.IdCentroNavigation.IdCentro;
            CD.IdDivision = centrodiscrepancia.IdDivision;
            CD.Cnom = centrodiscrepancia.IdCentroNavigation.Cnom;
            CD.Dnombre = centrodiscrepancia.IdDivisionNavigation.Dnombre;
        }


        return CD;
    }

    public async Task<List<CambFec>> GetPendientesQuincenal(CentroDivisionDTO centrodiv)
    {

        string centro = centrodiv.Cnom;
        string div = centrodiv.Dnombre;
        int reunionDiaria = 1;

        List<Reunion> discs = await _neocontext.Reunions
            .Include(b => b.IdksfNavigation)
            .Include(b => b.IdResReuNavigation)

.Where(h => h.Rdcentro == centro &&
            h.Rddiv == div &&
            h.IdTipReu == reunionDiaria &&
            h.RdfecTra.HasValue &&
            h.RdfecTra.Value.Date < DateTime.Now.Date &&
            (h.Rdstatus == "Pendiente" || h.Rdstatus == "Pendiente/Responsable" || h.Rdstatus == "Listo"))

            .ToListAsync();

        List<CambFec> result = new List<CambFec>();

        foreach (var iten in discs)
        {
            List<CambFec> filt = await _neocontext.CambFecs
                .Where(h => h.IdReuDia == iten.IdReuDia)
                .ToListAsync();

            var grouped = filt.GroupBy(x => x.IdReuDia)
                .Where(g => g.Count() > 3)
                .SelectMany(g => g)
                .ToList();

            result.AddRange(grouped);
        }

        return result;
    }


    public CentroDivisionDTO BuildCentroDivisionDTO(Master centrodiscrepancia)
    {
        return new CentroDivisionDTO
        {
            IdCentro = centrodiscrepancia.IdCentroNavigation.IdCentro,
            IdDivision = centrodiscrepancia.IdDivision,
            Cnom = centrodiscrepancia.IdCentroNavigation.Cnom,
            Dnombre = centrodiscrepancia.IdDivisionNavigation.Dnombre
        };
    }

    public async Task<List<ReunionDTO>> GetPendientesxFechaProgramada(int idmaster, DateTime FechaP)
    {
        List<Reunion> disc = await _neocontext.Reunions
            .Include(b => b.IdksfNavigation)
            .Include(b => b.IdResReuNavigation)
            .Where(h => h.RdcodDis == "2" && h.RdfecReu.Value.Date == FechaP.Date)
            .ToListAsync();
        if (disc == null)
            throw new Exception("not found!");

        return _mapper.Map<List<ReunionDTO>>(disc);

    }

    public async Task<bool> UpdateDiscrepanciaXFechaP(List<ReunionDTO> Reu, DateTime FechaP)
    {
        bool isUpdated = false;
        if (Reu.Count() > 0)
        {
            return isUpdated = false;
        }
        else
        {
            foreach (var d in Reu)
            {
                var entity = await _neocontext.Reunions.FirstOrDefaultAsync(sh => sh.IdReuDia == d.IdReuDia);
                d.RdfecReu = FechaP;

                // Mapeo de los cambios de d a la entidad cargada
                _mapper.Map(d, entity);
                await _neocontext.SaveChangesAsync();
            }

            return isUpdated = true;
        }

        return isUpdated ? true : false;

    }
}