using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Cote_Cloud.transacciones
{
    public class TransMatricula
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
        TransPreMatricula datos = new TransPreMatricula();
        public void ingresarAdmitidos(string id,string especialidad)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spInsertarAdmitido", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
            cmd.Parameters.Add("@especialidad", SqlDbType.VarChar).Value = especialidad;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private String generationLinksMatricula(string id,string especialidad)
        {
            Random obj = new Random();
            string posibles = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890qwertyuiopasdfghjklzxcvbnm";
            int longitud = posibles.Length;
            char letra;
            char[] caracteres = id.ToCharArray();
            string link = "http://admisioncotepecos.azurewebsites.net/RegMatricula/Matricula.aspx?";
            for (int a = 0; a < id.Length; a++)
            {
                string nuevacadena = "";
                for (int i = 0; i < 5; i++)
                {
                    letra = posibles[obj.Next(longitud)];
                    nuevacadena += letra.ToString();
                }
                link += nuevacadena + caracteres[a].ToString() + "/";
            }
            link = link.TrimEnd('/');
            link += "?";
            string[] dataEsp = especialidad.Split(' ');
            for(int a = 0; a < dataEsp.Length; a++)
            {
                link += dataEsp[a] + "/";
            }
            link = link.TrimEnd('/');
            return link;
        }
        public void enviarCorreosMatricula(string especialidad)
        {
            foreach (DataRow data in sacarAdmitidos(especialidad).Rows)
            {
                string id = data["id"].ToString();
                string link = generationLinksMatricula(id,especialidad);
                Cote_Cloud.User.email correo = new Cote_Cloud.User.email();
                correo.mensaje = "Para completar con el proceso de matrícula ingrese al siguiente enlace: <a href='" + link + "' style='color:blue; font-size:18px;'>LINK</a>";
                correo.MailTo = data["correo"].ToString();
                correo.MailSubject = "Matrícula";
                correo.SendMail();
            }
        }
        public void sendFormToAdmit(string id, string esp)
        {
            foreach (DataRow data in sacarAdmitidos(esp).Rows)
            {
                if (id == data["id"].ToString())
                {
                    string link = generationLinksMatricula(id,esp);
                    Cote_Cloud.User.email correo = new Cote_Cloud.User.email();
                    correo.mensaje = "Para completar con el proceso de matrícula ingrese al siguiente enlace: <a href='" + link + "' style='color:blue; font-size:18px;'>LINK</a>";
                    correo.MailTo = data["correo"].ToString();
                    correo.MailSubject = "Matrícula";
                    correo.SendMail();
                }
            }
        }
        public void enviarCorreoAdmitidos(string especialidad)
        {
            foreach (DataRow data in sacarAdmitidos(especialidad).Rows)
            {
                string id = data["id"].ToString();
                Cote_Cloud.User.email correo = new Cote_Cloud.User.email();
                correo.mensaje = "El estudiante " + data["nombre"].ToString() + " " + data["apellidos"].ToString() + " fue admito a nuestra institución. En los próximos días se le enviará un enlace del formulario de matrícula con el fin de concluir el proceso.";
                correo.MailTo = data["correo"].ToString();
                correo.MailSubject = "Estudiante admitido";
                correo.SendMail();
            }
        }
        public void sendEmailtoAdmit(string id,string esp)
        {
            DataRow data = getStudentAdmitido(id,esp);
            Cote_Cloud.User.email correo = new Cote_Cloud.User.email();
            correo.mensaje = "El estudiante " + data["nombre"].ToString() + " " + data["apellidos"].ToString() + " fue admito a nuestra institución. En los próximos días se le enviará un enlace del formulario de matrícula con el fin de concluir el proceso.";
            correo.MailTo = data["correo"].ToString();
            correo.MailSubject = "Estudiante admitido";
            correo.SendMail();
        }
        public DataRow getStudentAdmitido(string id,string esp)
        {
            foreach (DataRow data in sacarAdmitidos(esp).Rows)
            {
                if(id== data["id"].ToString())
                {
                    return data;
                }
            }
            return null;
        }
        public int cantidadStudents(string especialidad)
        {
            int cant = 0;
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetCantEsp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@especialidad", SqlDbType.VarChar).Value = especialidad;
            SqlDataReader lee= cmd.ExecuteReader();
            while (lee.Read())
            {
                cant += lee.GetInt32(0);
            }
            con.Close();
            
            return cant;
        }
        public DataTable sacarNotas(string espec)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetNotas", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@especialidad", SqlDbType.VarChar).Value = espec;
            SqlDataAdapter cmds = new SqlDataAdapter(cmd);
            DataTable a = new DataTable();
            cmds.Fill(a);
            con.Close();
            return a;
        }
        public DataTable sacarNotasOpcion1(string espec)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetNotasOpcion1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@especialidad", SqlDbType.VarChar).Value = espec;
            SqlDataAdapter cmds = new SqlDataAdapter(cmd);
            DataTable a = new DataTable();
            cmds.Fill(a);
            con.Close();
            return a;
        }
        public DataTable sacarNotasOpcion2(string espec)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetNotasOpcion2", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@especialidad", SqlDbType.VarChar).Value = espec;
            SqlDataAdapter cmds = new SqlDataAdapter(cmd);
            DataTable a = new DataTable();
            cmds.Fill(a);
            con.Close();
            return a;
        }
        public DataTable sacarAdmitidos(string espec)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetAdmitidos", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@especialidad", SqlDbType.VarChar).Value = espec;
            SqlDataAdapter cmds = new SqlDataAdapter(cmd);
            DataTable a = new DataTable();
            cmds.Fill(a);
            con.Close();
            return a;
        }
        private string numbersInserted(string number)
        {
            string value = "";
            if (number == "" || number == " ")
            {
                value = "0";
            }
            else
            {
                value = number;
            }
            return value;
        }
        public bool IngresarMatriculado(string id,string correo, string poliza,string cuota, string telefono, string edad, string provincia,
            string canton, string distrito, string telResi, string direccion, string nomP, string nomM, string nomE,
            string cedP, string cedM, string cedE, string telP, string telM, string telE, string ingresoE,
            string ingresoM, string ingresoP, string ocupacionM, string ocupacionP, string ocupacionE, string telWorkP, string telWorkM,
            string telWorkE, string nombre)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spInsMatriculado", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt64(numbersInserted(id));
                cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = correo;
                cmd.Parameters.Add("@poliza", SqlDbType.VarChar).Value = poliza;
                cmd.Parameters.Add("@cuota", SqlDbType.Int).Value = Convert.ToInt32(numbersInserted(cuota));
                cmd.Parameters.Add("@tel", SqlDbType.Int).Value = telefono;
                cmd.Parameters.Add("@edad", SqlDbType.Int).Value = Convert.ToInt32(numbersInserted(edad));
                cmd.Parameters.Add("@provincia", SqlDbType.VarChar).Value = provincia;
                cmd.Parameters.Add("@canton", SqlDbType.VarChar).Value = canton;
                cmd.Parameters.Add("@distrito", SqlDbType.VarChar).Value = distrito;
                cmd.Parameters.Add("@telRes", SqlDbType.Int).Value = Convert.ToInt32(numbersInserted(telResi));
                cmd.Parameters.Add("@direccion", SqlDbType.Text).Value = direccion;
                cmd.Parameters.Add("@nomP", SqlDbType.VarChar).Value = nomP;
                cmd.Parameters.Add("@nomM", SqlDbType.VarChar).Value = nomM;
                cmd.Parameters.Add("@nomE", SqlDbType.VarChar).Value = nomE;
                cmd.Parameters.Add("@cedP", SqlDbType.BigInt).Value = Convert.ToInt64(numbersInserted(cedP));
                cmd.Parameters.Add("@cedM", SqlDbType.BigInt).Value = Convert.ToInt64(numbersInserted(cedM));
                cmd.Parameters.Add("@cedE", SqlDbType.BigInt).Value = Convert.ToInt64(numbersInserted(cedE));
                cmd.Parameters.Add("@telP", SqlDbType.Int).Value = Convert.ToInt32(numbersInserted(telP));
                cmd.Parameters.Add("@telM", SqlDbType.Int).Value = Convert.ToInt32(numbersInserted(telM));
                cmd.Parameters.Add("@telE", SqlDbType.Int).Value = Convert.ToInt32(numbersInserted(telE));
                cmd.Parameters.Add("@ingresoE", SqlDbType.Int).Value = Convert.ToInt32(numbersInserted(ingresoE));
                cmd.Parameters.Add("@ingresoM", SqlDbType.Int).Value = Convert.ToInt32(numbersInserted(ingresoM));
                cmd.Parameters.Add("@ingresoP", SqlDbType.Int).Value = Convert.ToInt32(numbersInserted(ingresoP));
                cmd.Parameters.Add("@ocupacionM", SqlDbType.VarChar).Value = ocupacionM;
                cmd.Parameters.Add("@ocupacionP", SqlDbType.VarChar).Value = ocupacionP;
                cmd.Parameters.Add("@ocupacionE", SqlDbType.VarChar).Value = ocupacionE;
                cmd.Parameters.Add("@telTm", SqlDbType.Int).Value = Convert.ToInt32(numbersInserted(telWorkM));
                cmd.Parameters.Add("@telTe", SqlDbType.Int).Value = Convert.ToInt32(numbersInserted(telWorkE));
                cmd.Parameters.Add("@telTp", SqlDbType.Int).Value = Convert.ToInt32(numbersInserted(telWorkP));
                cmd.ExecuteNonQuery();
                con.Close();
                string contras = contra();
                string usuario = "Usuario" + id;
                ingresarUsuarios(id, usuario, contras, "estudiante", correo);
                Cote_Cloud.User.email email = new Cote_Cloud.User.email();
                email.informativo = false;
                email.mensaje = "El estudiante " + nombre + " ahora forma parte de la familia del COTEPECOS. El usuario y contraseña de la plataforma web se específica abajo. Por motivos de seguridad ingrese y cambie su usuario y contraseña.";
                email.usuario = usuario;
                email.contra = contras;
                email.MailTo = correo;
                email.MailSubject = "Confirmación de matrícula.";
                email.SendMail();
            }catch(Exception a)
            {
                Console.Write(a);
            }
            return true;
        }
        public string contra()
        {
            Random obj = new Random();
            string posibles = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890qwertyuiopasdfghjklzxcvbnm";
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
        public bool existEmail(string email,string id)
        {
            bool fal = false;
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetAllUsers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader cmdleer = cmd.ExecuteReader();
            while (cmdleer.Read())
            {
                if (cmdleer.GetString(6) == email)
                {
                    fal = true;
                }
            }
            cmdleer.Close();
            cmd = new SqlCommand("spGetAllPreMatriculado", con);
            cmdleer = cmd.ExecuteReader();
            while (cmdleer.Read())
            {
                if (cmdleer.GetString(3) == email && cmdleer.GetInt64(0).ToString()!=id)
                {
                    fal = true;
                }
            }
            con.Close();
            return fal;
        }
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
    }   
}