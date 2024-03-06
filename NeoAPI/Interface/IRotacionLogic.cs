using NeoAPI.ModelsDOCIng;

namespace NeoAPI.Interface 
{
    public interface IRotacionLogic
    {
        DateTime ObtenerFechaBPCS(int idEmpresa);
        public DateTime? ConversionHorarios(int idEmpresa);
        public RotaCalidum Rotacion(int idPais,int idCentro);
    }
}