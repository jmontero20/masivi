using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebPruebaMasiv.Class;

namespace WebPruebaMasiv.Models
{
    public class RouletteModelClass
    {
        private readonly ConexionBD _conexion = new ConexionBD();

        public int Create() {

            try
            {
                SqlConnection conexion = _conexion.OpenConexion();
                int newID;
                string cadena = "insert into roulette(name,estado) values ('roulette',0)";
                SqlCommand comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                comando.CommandText = "SELECT @@IDENTITY";
                newID = Convert.ToInt32(comando.ExecuteScalar());
                return newID;
            }
            catch (Exception e)
            {
                return 0;
            }

        }


        public int Update( Roulette roulette) {
           
            try
            {
                SqlConnection conexion = _conexion.OpenConexion();
                string cadena = "UPDATE roulette SET name = @name, estado = @estado Where id = @id";
                SqlCommand comando = new SqlCommand(cadena, conexion);
                comando.Parameters.AddWithValue("@name", roulette.name);
                comando.Parameters.AddWithValue("@estado", roulette.estado);
                comando.Parameters.AddWithValue("@id", roulette.id);
                comando.ExecuteNonQuery();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public Roulette GetRoulette(int id) {

            Roulette roulette = new Roulette();
            SqlConnection conexion = _conexion.OpenConexion();
            string cadena = "select * from roulette where id = "+id;
            SqlCommand comando = new SqlCommand(cadena, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            while (registros.Read())
            {
                roulette.id = (int)registros["id"];
                roulette.name = registros["name"].ToString();
                roulette.estado = (bool)registros["estado"];
            }
            return roulette;
        }

        public List<Roulette> All()
        {

            SqlConnection conexion = _conexion.OpenConexion();
            string cadena = "select * from roulette";
            SqlCommand comando = new SqlCommand(cadena, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            List<Roulette> roulettes = new List<Roulette>();
            while (registros.Read())
            {
                Roulette roulette = new Roulette();
                roulette.id = (int)registros["id"];
                roulette.name = registros["name"].ToString();
                roulette.estado = (bool)registros["estado"];
                roulettes.Add(roulette);
            }
            return roulettes;

        }


    }
}
