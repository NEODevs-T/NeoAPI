using System.Reflection.Metadata.Ecma335;
using NeoAPI.Interface;
using NeoAPI.ModelsDOCIng;
using NeoAPI.Models;

namespace NeoAPI.Logic
{
    public class RotacionLogic : IRotacionLogic
    {
        public (int PAVECA, int CHEMPRO, int PANASA, int PAINSA) empresas = (PAVECA: 1,CHEMPRO: 2, PANASA: 3,PAINSA: 4);
        private (int K10, int K129) centroPAINSA {get; set;} = (K10: 18,K129: 19);

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
        public DateTime? ConversionHorarios(int idEmpresa)
        {
            DateTime date = DateTime.Now;
            DateTime dateReal = new DateTime();

            if(idEmpresa == empresas.PAVECA){
                dateReal = date;
            }else if(idEmpresa == empresas.CHEMPRO){
                dateReal = date;
            }else if(idEmpresa == empresas.PANASA){
                dateReal = date.AddHours(-1);
            }else if(idEmpresa == empresas.PAINSA){
                dateReal = date.AddHours(-2);
            }else{
                return null;
            }
            return dateReal;
        }

        public RotaCalidum Rotacion(int idEmpresa,int idCentro)
        {
            
            DateTime dateReal = this.ConversionHorarios(idEmpresa) ?? new DateTime();
            int hora = dateReal.Hour;
            int turno = 0;
            int fecha = 0;

            if(idEmpresa == this.empresas.PAVECA){
                if(hora >= 6 && hora < 18){
                    turno = 1;
                    fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                }else if(hora >= 0 && hora < 6){
                    turno = 2;
                    fecha = int.Parse(dateReal.AddDays(-1).ToString("yyyyMMdd"));
                }else{
                    turno = 2;
                    fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                }

            }else if(idEmpresa == this.empresas.CHEMPRO){
                if(hora >= 6 && hora < 18){
                    turno = 1;
                    fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                }else if(hora >= 0 && hora < 6){
                    turno = 2;
                    fecha = int.Parse(dateReal.AddDays(-1).ToString("yyyyMMdd"));
                }else{
                    turno = 2;
                    fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                }

            }else if(idEmpresa == this.empresas.PANASA){
                if(hora >= 6 && hora < 14){
                    turno = 1;
                    fecha  = int.Parse(dateReal.ToString("yyyyMMdd"));
                }else if(hora >= 14 && hora < 22){
                    turno = 2;
                    fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                }else{
                    turno = 3;
                    if(hora >= 22 && hora < 24){
                        fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                    }else{
                        fecha = int.Parse(dateReal.AddDays(-1).ToString("yyyyMMdd"));
                    }
                }

            }else if(idEmpresa == this.empresas.PAINSA){
                if(idCentro == this.centroPAINSA.K10){

                    if(hora >= 6 && hora < 13){
                        turno = 1;
                        fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                    }else if(hora >= 13 && hora < 20){
                        turno = 2;
                        fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                    }else{
                        turno = 3;
                        if(hora >= 20 && hora < 24){
                            fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                        }else{
                            fecha = int.Parse(dateReal.AddDays(-1).ToString("yyyyMMdd"));
                        }
                    }

                }else if(idCentro == this.centroPAINSA.K129){
                    
                    if(hora >= 7 && hora < 15){
                        turno = 1;
                        fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                    }else if(hora >= 15 && hora < 23){
                        turno = 2;
                        fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                    }else{
                        turno = 3;
                        if(hora >= 23 && hora < 24){
                            fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                        }else{
                            fecha = int.Parse(dateReal.AddDays(-1).ToString("yyyyMMdd"));
                        }
                    }

                }else{
                    return null;
                }

            }else{
                return null;
            }
                RotaCalidum rotaCalidum = new RotaCalidum();
                rotaCalidum.RcidRotCal = 0;
                rotaCalidum.Rcfecha = fecha;
                rotaCalidum.Rcturno = turno;
                rotaCalidum.Rcgrupo = "0";
                return rotaCalidum;
        }
    }
}