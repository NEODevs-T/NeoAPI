using NeoAPI.ModelsDOCIng;

namespace NeoAPI.Interface 
{
    public interface IRotacionLogic
    {
        DateTime ObtenerFechaBPCS(int idEmpresa);
        public DateTime? ConversionHorarios(int idPais);
        public RotaCalidum Rotacion(int idPais,int idCentro);
    }
}