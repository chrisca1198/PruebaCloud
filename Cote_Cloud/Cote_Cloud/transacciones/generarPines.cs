using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace Cote_Cloud.transacciones
{
    public class generarPines
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
        public DataTable sacarpines()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetPinesDisponibles", con);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            SqlDataAdapter cmdLeer = new SqlDataAdapter(cmd);
            cmdLeer.Fill(dt);
            con.Close();
            return dt;
        }
        public void setUnavailablePin(string cod)
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("editPin", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add("@pin", SqlDbType.VarChar).Value = cod;
            cmd2.Parameters.Add("@estado", SqlDbType.VarChar).Value = "usado";
            cmd2.ExecuteNonQuery();
            con.Close();
        }
        public void setAvailablePin(string cod)
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("editPin", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add("@pin", SqlDbType.VarChar).Value = cod;
            cmd2.Parameters.Add("@estado", SqlDbType.VarChar).Value = "disponible";
            cmd2.ExecuteNonQuery();
            con.Close();
        }
        public DataTable sacarpinesUsados()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetAllDataPines", con);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            SqlDataAdapter cmdLeer = new SqlDataAdapter(cmd);
            cmdLeer.Fill(dt);
            con.Close();
            DataTable returning = new DataTable();
            returning.Columns.Add("Pin");
            foreach (DataRow row in dt.Rows)
            {
                if (row["seleccionado"].ToString().Equals("usado"))
                {
                    string pin = row["pin"].ToString();
                    DataRow newRow = returning.NewRow();
                    newRow["Pin"] = pin;
                    returning.Rows.Add(newRow);
                }
            }
            return returning;
        }
        public void pines(string cant)
        {
            DataTable dt = sacarpines();
            con.Open();
            Random obj = new Random();
            string posibles = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int longitud = posibles.Length;
            char letra;
            int longitudnuevacadena = 10;
            string nuevacadena = "";
            for (int a = 0; a < Convert.ToInt32(cant); a++)
            {
                nuevacadena = "";
                for (int i = 0; i < longitudnuevacadena; i++)
                {
                    letra = posibles[obj.Next(longitud)];
                    nuevacadena += letra.ToString();
                }
                bool exist = false;
                foreach (DataRow rows in dt.Rows)
                {
                    if (rows[0].ToString() == nuevacadena)
                    {
                        a--;
                        exist = true;
                    }
                }
                if (!exist)
                {
                    SqlCommand cmd = new SqlCommand("spInPines", con);
                    cmd.Parameters.Add("@pin", SqlDbType.VarChar).Value = nuevacadena;
                    cmd.Parameters.Add("@seleccion", SqlDbType.VarChar).Value = "disponible";
                    TimeZoneInfo zona = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
                    DateTime fecha = TimeZoneInfo.ConvertTime(DateTime.Now, zona);
                    cmd.Parameters.Add("@fecha", SqlDbType.SmallDateTime).Value = fecha;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
            con.Close();
        }
        public ArrayList getDate()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetFechasDisponibles", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader leer = cmd.ExecuteReader();
            ArrayList dt = new ArrayList();
            DateTime date = new DateTime();
            string aux = "";
            while (leer.Read())
            {
                date = leer.GetDateTime(0);
                if (!(dt.Contains(date.ToShortDateString())))
                {
                    aux = date.ToShortDateString();
                    dt.Add(aux);
                }
            }
            con.Close();
            return dt;
        }
        public ArrayList getHours(string dates)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetFechasDisponibles", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader leer = cmd.ExecuteReader();
            ArrayList dt = new ArrayList();
            DateTime date = new DateTime();
            string aux = "";
            while (leer.Read())
            {
                date = leer.GetDateTime(0);
                if (date.ToShortDateString() == dates)
                {
                    if (!(dt.Contains(date.ToLongTimeString())))
                    {
                        aux = date.ToLongTimeString();
                        dt.Add(aux);
                    }
                }
            }
            con.Close();
            return dt;
        }
        public Boolean existPin(string pin)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetAllPines", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader leer = cmd.ExecuteReader();
            bool aux = false;
            while (leer.Read())
            {
                if (pin == leer.GetString(0))
                {
                    aux = true;
                }
            }
            con.Close();
            return aux;
        }
        public void eliminarPin(string pin)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delPin", con);
            cmd.Parameters.Add("@pin", SqlDbType.VarChar).Value = pin;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void eliminarPinFechas(DateTime fecha)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spDelPinFechas", con);
            cmd.Parameters.Add("@fecha", SqlDbType.SmallDateTime).Value = fecha;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public DataTable sacarpinesDate(string fecha, string hora)
        {
            con.Open();
            string dato = fecha;
            dato += " " + hora;
            DateTime date = Convert.ToDateTime(dato);
            SqlCommand cmd = new SqlCommand("spGetPinesFechas", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@fecha", SqlDbType.SmallDateTime).Value = date;
            DataTable dt = new DataTable();
            SqlDataAdapter cmdLeer = new SqlDataAdapter(cmd);     
            cmdLeer.Fill(dt);
            con.Close();
            return dt;
        }
    }
}