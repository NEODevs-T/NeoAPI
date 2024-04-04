using IBM.Data.Db2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System.Data;
using NeoAPI.Models.AS400;
using NeoAPI.Models.Neo;
using NeoAPI.ModelsViews;
using Microsoft.AspNetCore.Hosting.Server;
using System.Data.OleDb;
using System.Text.Json.Serialization;

namespace NeoAPI.Controllers.AS400
{
    [Route("api/[controller]")]
    [ApiController]
    public class BPCSController : ControllerBase
    {
        private readonly DbBPCSContext _context;

        public BPCSController(DbBPCSContext context)
        {
            _context = context;

        }


        //[HttpGet]
        //public async Task<ActionResult<List<NMPP088>>> GetMiTabla()
        //{
        //    List<NMPP088> listaNMPP088 = new List<NMPP088>();

        //    DB2Command MyDB2Command = null;

        //    String MyDb2ConnectionString = " server = APPN.DESARROL; uid = NOMINASPI; pwd = NOMINASPI; database = SPIDATA;";
        //    DB2Connection MyDb2Connection = new DB2Connection(MyDb2ConnectionString);
        //    MyDb2Connection.Open();
        //    MyDB2Command = MyDb2Connection.CreateCommand();
        //    MyDB2Command.CommandText = "SELECT TOP 1 * NMPP088JL ";
        //    Console.WriteLine(MyDB2Command.CommandText);

        //    DB2DataReader MyDb2DataReader = null;
        //    MyDb2DataReader = MyDB2Command.ExecuteReader();

        //    while (MyDb2DataReader.Read())
        //    {

        //        NMPP088 entidad = new NMPP088
        //        {
        //            CIAMBA = MyDb2DataReader.IsDBNull(0) ? null : MyDb2DataReader.GetString(0),
        //            MODMBA = MyDb2DataReader.IsDBNull(1) ? null : MyDb2DataReader.GetString(1),
        //            FICMBA = MyDb2DataReader.IsDBNull(2) ? null : MyDb2DataReader.GetString(2),

        //        };

        //        listaNMPP088.Add(entidad);
        //    }
        //    MyDb2DataReader.Close();
        //    MyDB2Command.Dispose();
        //    MyDb2Connection.Close();



        //    return listaNMPP088;
        //    ///return  await _context.Nmpp088m
        //    //.AsNoTracking()
        //    //.ToListAsync();
        //}
        private readonly string _connectionString = "Provider=IBMDA400.DataSource.1;Data Source=APPN.DESARROL;Password=NOMINASPI;User ID=NOMINASPI;Initial Catalog=Provider=IBMDA400.DataSource.1;Data Source=APPN.DESARROL;Password=NOMINASPI;User ID=NOMINASPI;Initial Catalog=I20a237w;Library List=SPIDATA";
        private OleDbCommand obj = new OleDbCommand();
        OleDbDataReader objResult;

        [HttpGet]
        public async Task<ActionResult<List<NMPP088>>> GetOLDB()
        {
            var listaNMPP088 = new List<NMPP088>();

            // Crear un objeto de conexión
            obj.Connection = new OleDbConnection(_connectionString);

            try
            {
                // Abrir la conexión
                obj.Connection.Open();

                // Establecer la consulta
                obj.CommandText = "SELECT * FROM NMPP088";

                // Ejecutar la consulta
                objResult = obj.ExecuteReader();

                // Cargar los resultados en un DataTable
                var dataTable = new DataTable();
                dataTable.Load(objResult);

                // Procesar los resultados según sea necesario
                // ...

                // Cerrar la conexión
                obj.Connection.Close();

                return listaNMPP088;
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción
                return BadRequest($"Ocurrió un error: {ex.Message}");
            }
        }



        //private readonly string _connectionString = "Provider=IBMDA400.DataSource.1;Data Source=APPN.DESARROL;Password=NOMINASPI;User ID=NOMINASPI";
        //private OleDbCommand obj = new OleDbCommand();
        //OleDbDataReader objResult;
        //[HttpGet]
        //public  async Task<ActionResult<List<NMPP088>>> GetOLDB()
        //{
        //    var listaNMPP088 = new List<NMPP088>();
        //    obj.Connection=new OleDbConnection(_connectionString);


        //    obj.CommandText = "SELECT TOP 1 * FROM NMPP088JL";
        //    objResult = obj.ExecuteReader();
        //    var dataTable = new DataTable();
        //    dataTable.Load(objResult);

        //    return listaNMPP088;
        //}


        //    [HttpGet]
        //    public async Task<ActionResult<List<NMPP088>>> DB2Core()
        //    {
        //        var listaNMPP088 = new List<NMPP088>();

        //        using (var connection = new DB2Connection(" server = APPN.DESARROL; uid = CRODRIGUEZ; pwd = TURBO599; database = SPIDATA;"))
        //        {
        //            await connection.OpenAsync();
        //            var command = new DB2Command("SELECT * FROM NMPP088JL FETCH FIRST 1 ROWS ONLY", connection);

        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    var entidad = new NMPP088
        //                    {
        //                        CIAMBA = reader.IsDBNull(0) ? null : reader.GetString(0),
        //                        MODMBA = reader.IsDBNull(1) ? null : reader.GetString(1),
        //                        FICMBA = reader.IsDBNull(2) ? null : reader.GetString(2),
        //                    };

        //                    listaNMPP088.Add(entidad);
        //                }
        //            }
        //        }

        //        return listaNMPP088;
        //    }

        //}
    }
}
