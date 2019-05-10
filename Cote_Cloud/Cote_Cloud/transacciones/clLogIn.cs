using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Collections;
using System.IO;

namespace Cote_Cloud.transacciones
{
    public class clLogIn
    {
        administracion admin = new administracion();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
        public void ingresarUsuarios(string id, string nom, string contra, string rol, string correo)
        {
            SqlCommand cmd = new SqlCommand("spInsertarUsuario", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToUInt64(id);
            cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = nom;
            cmd.Parameters.Add("@contra", SqlDbType.VarChar).Value = contra;
            cmd.Parameters.Add("@rol", SqlDbType.VarChar).Value = rol;
            cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = correo;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public string contra()
        {
            Random obj = new Random();
            string posibles = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int longitud = posibles.Length;
            char letra;
            int longitudnuevacadena = 8;
            string nuevacadena = "";
            for (int i = 0; i < longitudnuevacadena; i++)
            {
                letra = posibles[obj.Next(longitud)];
                nuevacadena += letra.ToString();
            }
            return nuevacadena;
        }
        public String[] logIn(string usuario, string contra)
        {
            con.Open();
            string[] data = new string[4];
            SqlCommand cmd = new SqlCommand("spGetUsers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usuario;
            SqlDataReader leer = cmd.ExecuteReader();
            string contras = "", user = "", rol = "", id = "";
            bool inicio = false;
            while (leer.Read())
            {
                user = leer.GetString(0);
                id += leer.GetInt64(1);
                contras = leer.GetString(2);
                inicio = leer.GetBoolean(3);
                rol = leer.GetString(4);
            }
            if (user == usuario)
            {
                if (contra == contras)
                {
                    data[0] = "SI";
                    data[1] = rol;
                    data[2] += inicio;
                    data[3] = id;
                }
                else
                {
                    data[0] = "NO";
                }
            }
            else
            {
                data[0] = "IN";
            }
            con.Close();
            return data;
        }
        public Boolean LogCorrect(string usuario, string contra)
        {
            string[] data = getUser(usuario);
            if (usuario == data[0] && contra == data[2])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void modificarUser(string usuario,  string contra)
        {
            SqlCommand cmd = new SqlCommand("spEditUser", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario;
            cmd.Parameters.Add("@iniciado", SqlDbType.Bit).Value = true;
            cmd.Parameters.Add("@contra", SqlDbType.VarChar).Value = contra;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void modifyDataUser(string newuser, string usuario, string email,byte[] foto,string id,string cel)
        {
            SqlCommand cmd = new SqlCommand("spEditDataUser", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@cel", SqlDbType.Int).Value = Convert.ToInt32(cel);
            cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt64(id);
            cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario;
            cmd.Parameters.Add("@Nuevousuario", SqlDbType.VarChar).Value = newuser;
            cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = email;
            cmd.Parameters.Add("@foto", SqlDbType.Image).Value = foto;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public String[] getUser(string usuario)
        {
            string[] array = new string[6];
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetUsers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usuario;
            SqlDataReader leer = cmd.ExecuteReader();
            while (leer.Read())
            {
                array[0] = leer.GetString(0);
                array[1] += leer.GetInt64(1);
                array[2] = leer.GetString(2);
                array[3] += leer.GetBoolean(3);
                array[4] = leer.GetString(4);
                array[5] = leer.GetString(6);
            }
            con.Close();
            return array;
        }
        public Boolean ExistUser(string user)
        {
            string[] data = getUser(user);
            if (user == data[0]) { return true; } else { return false; }
        }
        public Byte[] getFotoUser(string usuario)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetUsers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = usuario;
            SqlDataAdapter leer = new SqlDataAdapter(cmd);
            DataSet dt = new DataSet("usuario");
            leer.Fill(dt,"usuario");
            byte[] array = new byte[0];
            DataRow row = dt.Tables["usuario"].Rows[0];
            array = row["foto"] as byte[];
            con.Close();
            return array;
        }
        public string getNumberUser(string id)
        {
            
            DataTable dt = admin.getDataStudents();
            string list = "";
            foreach (DataRow row in dt.Rows)
            {
                string ced = row["cedula"].ToString();
                if (ced == id) { list = row["telefono"].ToString(); }
            }
            return list;
        }

        //public void eliminarUser(string id, string nom, string contra)
        //{
        //    SqlCommand cmd = new SqlCommand("spDelUser", con);
        //    con.Open();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = nom;
        //    cmd.Parameters.Add("@contra", SqlDbType.VarChar).Value = contra;
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}
    }
}