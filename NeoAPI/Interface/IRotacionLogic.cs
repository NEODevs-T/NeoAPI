namespace NeoAPI.Interface 
{
    public interface IRotacionLogic
    {
        DateTime ObtenerFechaBPCS(int idEmpresa);
        public DateTime? ConversionHorarios(int idPais);
    }
}