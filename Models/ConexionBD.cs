using System.Data.SqlClient;
using System.Data;
namespace WebPruebaMasiv.Models
{
    public class ConexionBD
    {
        private SqlConnection Conexion = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=CasinoDB;Trusted_Connection=True;MultipleActiveResultSets=true");
        public SqlConnection OpenConexion()
        {
            if (Conexion.State == ConnectionState.Closed)
                Conexion.Open();
            return Conexion;
        }
        public SqlConnection CloseConexion()
        {
            if (Conexion.State == ConnectionState.Open)
                Conexion.Close();
            return Conexion;
        }

    }
}
