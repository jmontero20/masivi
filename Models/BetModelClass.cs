using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebPruebaMasiv.Class;

namespace WebPruebaMasiv.Models
{
    public class BetModelClass
    {
        private readonly ConexionBD _conexion = new ConexionBD();

        public int Create( Bet bet)
        {

            try
            {
                SqlConnection conexion = _conexion.OpenConexion();
                int newID;
                string cadena = "insert into bet(number,color,value,result,id_roulette,winner) values (" + (bet.number == null ? "Null": bet.number.ToString()) +"," + (bet.color == null ? "NULL" : "'"+bet.color+"'") + ","+bet.value+",NULL,"+bet.id_roulette+",NULL)";
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

        public int Update(Bet bet)
        {

            try
            {
                SqlConnection conexion = _conexion.OpenConexion();
                string cadena = "UPDATE bet SET winner = @winner, result = @result Where id = @id";
                SqlCommand comando = new SqlCommand(cadena, conexion);
                comando.Parameters.AddWithValue("@winner", bet.winner);
                comando.Parameters.AddWithValue("@result", bet.result);
                comando.Parameters.AddWithValue("@id", bet.id);
                comando.ExecuteNonQuery();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }

        }

        public Bet GetBet(int id)
        {

            Bet bet = new Bet();
            SqlConnection conexion = _conexion.OpenConexion();
            string cadena = "select * from bet where id = " + id;
            SqlCommand comando = new SqlCommand(cadena, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            while (registros.Read())
            {

                bet.id = (int)registros["id"];
                bet.number = Convert.IsDBNull(registros["number"]) ? null : (int?)registros["number"];
                bet.color = registros["color"].ToString();
                bet.value = (double)registros["value"];
                bet.result = Convert.IsDBNull(registros["result"]) ? false : (bool)registros["result"];
                bet.id_roulette = (int)registros["id_roulette"];
                bet.winner = Convert.IsDBNull(registros["winner"]) ? null : (double?)registros["winner"];

            }
            return bet;
        }

        public List<Bet> AllRoulette(int id)
        {

            SqlConnection conexion = _conexion.OpenConexion();
            string cadena = "select * from bet where id_roulette = "+id;
            SqlCommand comando = new SqlCommand(cadena, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            List<Bet> bets = new List<Bet>();
            while (registros.Read())
            {
                Bet bet = new Bet();
                bet.id = (int)registros["id"];
                bet.number = (int)registros["number"];
                bet.color = registros["color"].ToString();
                bet.value = (double)registros["value"];
                bet.result = (bool)registros["result"];
                bet.id_roulette = (int)registros["id_roulette"];
                bet.winner = (double)registros["winner"];
                bets.Add(bet);
            }
            return bets;

        }

        public List<Bet> AllRouletteClose(int id)
        {

            SqlConnection conexion = _conexion.OpenConexion();
            string cadena = "select * from bet where result IS NULL and id_roulette = " + id;
            SqlCommand comando = new SqlCommand(cadena, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            List<Bet> bets = new List<Bet>();
            while (registros.Read())
            {
                Bet bet = new Bet();
                bet.id = (int)registros["id"];
                bet.number = Convert.IsDBNull(registros["number"]) ? null : (int?)registros["number"];
                bet.color = registros["color"].ToString();
                bet.value = (double)registros["value"];
                bet.result = Convert.IsDBNull(registros["result"]) ? false : (bool)registros["result"];            
                bet.id_roulette = (int)registros["id_roulette"];
                bet.winner = Convert.IsDBNull(registros["winner"]) ? null : (double?)registros["winner"];
                bets.Add(bet);
            }
            return bets;

        }

    }
}
