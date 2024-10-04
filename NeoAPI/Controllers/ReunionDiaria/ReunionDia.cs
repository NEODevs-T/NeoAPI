using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.DTOs.ReunionDiaria;
using NeoAPI.Models.Neo;

namespace NeoAPI.Controllers.GetCentroDiv;

public class ReuClass : ControllerBase
{
    public Master? centrodiscrepancia { get; set; } = new Master();
    private readonly DbNeoIiContext _neocontext;

    public ReuClass(DbNeoIiContext DbNeo)
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
}