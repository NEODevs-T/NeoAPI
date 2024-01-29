namespace NeoAPI.Logic
{

    public interface IRotacionLogic
    {
        DateTime ObtenerFechaBPCS(int idEmpresa);
    }
    public class RotacionLogic : IRotacionLogic
    {
        public (int PAVECA, int CHEMPRO, int PANASA, int PAINSA) empresas = (PAVECA: 1,CHEMPRO: 2, PANASA: 3,PAINSA: 4);
        public DateTime ObtenerFechaBPCS(int idEmpresa)
        {
            DateTime fecha = DateTime.Now;
            if(idEmpresa ==  empresas.PAVECA){
                if(fecha.Hour > 18 && fecha.Hour <= 23){
                    fecha = fecha.AddDays(1);
                }
            }else if(idEmpresa == empresas.CHEMPRO){

            }else if(idEmpresa == empresas.PANASA){

            }else if(idEmpresa == empresas.PAINSA){

            }
            return fecha;
        }
    }
}