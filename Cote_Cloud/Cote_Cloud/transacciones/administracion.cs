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
using System.Net.Mail;
using System.Collections.Specialized;
using System.Net;

namespace Cote_Cloud.transacciones
{
    public class administracion
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
        public void ingresarProfe(string id, string nom, string apellidos, string correo, string tel, string mat1, string mat2, string mat3,
            string area, string esp)
        {
            int materia1 = getIDMaterias(mat1, area, esp);
            int materia2 = getIDMaterias(mat2, area, esp);
            int materia3 = getIDMaterias(mat3, area, esp);
            con.Open();
            SqlCommand cmd = new SqlCommand("spInsProfe", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt64(id);
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nom;
            cmd.Parameters.Add("@apellidos", SqlDbType.VarChar).Value = apellidos;
            cmd.Parameters.Add("@tel", SqlDbType.BigInt).Value = Convert.ToInt32(tel);
            cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = correo;
            cmd.Parameters.Add("@mat", SqlDbType.Int).Value = materia1;
            cmd.ExecuteNonQuery();
            if (!(mat2 == ""))
            {
                cmd = new SqlCommand("spInsSubArea2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt64(id);
                cmd.Parameters.Add("@mat", SqlDbType.Int).Value = materia2;
                cmd.ExecuteNonQuery();
            }
            if (!(mat3 == ""))
            {
                cmd = new SqlCommand("spInsSubArea3", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt64(id);
                cmd.Parameters.Add("@mat", SqlDbType.Int).Value = materia3;
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }
        public void editProfe(string id, string nom, string apellidos, string tel, string mat1, string mat2, string mat3,
    string area, string esp)
        {
            int materia1 = getIDMaterias(mat1, area, esp);
            int materia2 = getIDMaterias(mat2, area, esp);
            int materia3 = getIDMaterias(mat3, area, esp);
            con.Open();
            SqlCommand cmd = new SqlCommand("spEditProfe", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt64(id);
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nom;
            cmd.Parameters.Add("@apellidos", SqlDbType.VarChar).Value = apellidos;
            cmd.Parameters.Add("@tel", SqlDbType.BigInt).Value = Convert.ToInt32(tel);
            cmd.Parameters.Add("@mat", SqlDbType.Int).Value = materia1;
            cmd.ExecuteNonQuery();
            if (!(mat2 == ""))
            {
                cmd = new SqlCommand("spEditProfeSubArea2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt64(id);
                cmd.Parameters.Add("@mat", SqlDbType.Int).Value = materia2;
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd = new SqlCommand("spDelSubArea2Profe", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt64(id);
                cmd.ExecuteNonQuery();
            }
            if (!(mat3 == ""))
            {
                cmd = new SqlCommand("spEditProfeSubArea3", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt64(id);
                cmd.Parameters.Add("@mat", SqlDbType.Int).Value = materia3;
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd = new SqlCommand("spDelSubArea3Profe", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt64(id);
                cmd.ExecuteNonQuery();

            }
            con.Close();
        }

        public void deleteProfe(string id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spDelProfe", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt64(id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public DataTable getProfesores()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetProfe", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter cmdLeer = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cmdLeer.Fill(dt);
            con.Close();
            return dt;
        }
        private DataTable getMateriasProfesores(string id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetMateriasProfe", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt64(id);
            SqlDataAdapter cmdLeer = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cmdLeer.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable getProfesAcademicos()
        {
            DataTable dt = getProfesores();
            DataTable aux = new DataTable();
            aux.Columns.Add("cedula");
            aux.Columns.Add("nombre");
            aux.Columns.Add("apellidos");
            aux.Columns.Add("telefono");
            aux.Columns.Add("correo");
            aux.Columns.Add("materia");
            if (dt.Rows.Count != 0)
            {
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    DataRow row = dt.Rows[a];
                    string cedula = row["Cedula"].ToString();
                    string nom = row["Nombre"].ToString();
                    string apellidos = row["Apellidos"].ToString();
                    string telefono = row["Telefono"].ToString();
                    string correo = row["Correo"].ToString();
                    DataTable materias = getMateriasProfesores(cedula);
                    DataRow materiass = materias.Rows[0];
                    string materia = materiass["Materia1"].ToString();
                    string esp = materiass["Especialidad"].ToString();
                    if (esp == "")
                    {
                        DataRow fila = aux.NewRow();
                        fila["cedula"] = cedula;
                        fila["nombre"] = nom;
                        fila["apellidos"] = apellidos;
                        fila["telefono"] = telefono;
                        fila["correo"] = correo;
                        fila["materia"] = materia;
                        aux.Rows.Add(fila);
                    }
                }
            }
            return aux;
        }
        public DataTable sacarPrematriculados(string especial)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetPrematriculados", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@especialidad", SqlDbType.VarChar).Value = especial;
            DataTable dt = new DataTable();
            SqlDataAdapter cmdLeer = new SqlDataAdapter(cmd);
            cmdLeer.Fill(dt);
            con.Close();
            return dt;
        }
        public int StudentsFirstOption(string esp)
        {
            int cant = 0;
            DataTable table = sacarPrematriculados(esp);
            foreach (DataRow row in table.Rows)
            {
                if (row["opcion1"].ToString() == esp)
                {
                    cant++;
                }
            }
            return cant;
        }
        public int StudentsSecondOption(string esp)
        {
            int cant = 0;
            DataTable table = sacarPrematriculados(esp);
            foreach (DataRow row in table.Rows)
            {
                if (row["opcion2"].ToString() == esp)
                {
                    cant++;
                }
            }
            return cant;
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
        public DataTable getProfesTecnicos(string especialidad)
        {
            DataTable dt = getProfesores();
            DataTable aux = new DataTable();
            aux.Columns.Add("cedula");
            aux.Columns.Add("nombre");
            aux.Columns.Add("apellidos");
            aux.Columns.Add("telefono");
            aux.Columns.Add("correo");
            aux.Columns.Add("materia1");
            aux.Columns.Add("materia2");
            aux.Columns.Add("materia3");
            if (dt.Rows.Count != 0)
            {
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    DataRow row = dt.Rows[a];
                    string cedula = row["Cedula"].ToString();
                    string nom = row["Nombre"].ToString();
                    string apellidos = row["Apellidos"].ToString();
                    string telefono = row["Telefono"].ToString();
                    string correo = row["Correo"].ToString();
                    DataTable materias = getMateriasProfesores(cedula);
                    DataRow materiass = materias.Rows[0];
                    string materia = materiass["Materia1"].ToString();
                    string materia2 = materiass["Materia2"].ToString();
                    string materia3 = materiass["Materia3"].ToString();
                    string esp = materiass["Especialidad"].ToString();
                    if (esp == especialidad)
                    {
                        DataRow fila = aux.NewRow();
                        fila["cedula"] = cedula;
                        fila["nombre"] = nom;
                        fila["apellidos"] = apellidos;
                        fila["telefono"] = telefono;
                        fila["correo"] = correo;
                        fila["materia1"] = materia;
                        fila["materia2"] = materia2;
                        fila["materia3"] = materia3;
                        aux.Rows.Add(fila);
                    }
                }
            }
            return aux;
        }
        public Boolean existProfe(string ced)
        {
            bool exist = false;
            DataTable dt = getProfesores();
            if (dt.Rows.Count != 0)
            {
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    DataRow row = dt.Rows[a];
                    string ced1 = row["Cedula"].ToString();
                    if (ced1 == ced)
                    {
                        exist = true;
                    }
                }
            }
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetAllUsers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader lector = cmd.ExecuteReader();
            while (lector.Read())
            {
                if (Convert.ToInt64(ced) == lector.GetInt64(1))
                {
                    exist = true;
                }
            }
            lector.Close();
            con.Close();
            return exist;
        }
        public void insertEspecialidades(string especialidades)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spInsEspeciali", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@especialidad", SqlDbType.VarChar).Value = especialidades;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void deleteEsp(string especialidades)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spDelEsp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@espe", SqlDbType.VarChar).Value = especialidades;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void editEspecialidad(string especialidades, string newespe)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spEditEspecia", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@newespe", SqlDbType.VarChar).Value = newespe;
            cmd.Parameters.Add("@espe", SqlDbType.VarChar).Value = especialidades;
            cmd.Parameters.Add("@disponi", SqlDbType.VarChar).Value = "cerrado";
            cmd.Parameters.Add("@cant", SqlDbType.Int).Value = 0;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public ArrayList sacarEspeci()
        {
            ArrayList lista = new ArrayList();
            DataTable tabla = sacarEsp();
            foreach (DataRow row in tabla.Rows)
            {
                lista.Add(row[0].ToString());
            }
            return lista;
        }
        public ArrayList sacarEspecialidadesDisponibles()
        {
            ArrayList lista = new ArrayList();
            DataTable tabla = sacarEsp();
            foreach (DataRow row in tabla.Rows)
            {
                if (row[1].ToString() == "abierto")
                    lista.Add(row[0].ToString());
            }
            return lista;
        }
        public Boolean existEsp(string especialidad)
        {
            bool exist = false;
            for (int a = 0; a < sacarEspeci().Count; a++)
            {
                if (sacarEspeci()[a].ToString() == especialidad)
                {
                    exist = true;
                }
            }
            return exist;
        }
        public DataTable sacarEsp()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetEsp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter cmdLeer = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cmdLeer.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable getMaterias(string area)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetMaterias", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@area", SqlDbType.VarChar).Value = area;
            SqlDataAdapter cmdLeer = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cmdLeer.Fill(dt);
            con.Close();
            return dt;
        }
        private int getIDMaterias(string mat, string area, string especialidad)
        {
            DataTable dt = getMaterias(area);
            int id = 0;
            if (dt.Rows.Count != 0)
            {
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    DataRow row = dt.Rows[a];
                    string esp = row["especialidad"].ToString();
                    string materia = row["materia"].ToString();
                    if (esp == especialidad && mat == materia)
                    {
                        id = Convert.ToInt32(row["id"].ToString());
                    }
                }
            }
            return id;
        }
        public DataTable getMateria()
        {
            DataTable dt = getMaterias("academica");
            DataTable aux = new DataTable();
            aux.Columns.Add("materia");
            if (dt.Rows.Count != 0)
            {
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    DataRow row = dt.Rows[a];
                    string esp = row["especialidad"].ToString();
                    string materia = row["materia"].ToString();
                    if (esp == "")
                    {
                        DataRow fila = aux.NewRow();
                        fila["materia"] = materia;
                        aux.Rows.Add(fila);
                    }
                }
            }
            return aux;
        }
        public ArrayList getListMaterias()
        {
            DataTable dt = getMateria();
            ArrayList list = new ArrayList();
            foreach (DataRow row in dt.Rows)
            {
                string materia = row["materia"].ToString();
                list.Add(materia);
            }
            return list;
        }
        public DataTable getSubArea(string especialidad)
        {
            DataTable dt = getMaterias("tecnica");
            DataTable aux = new DataTable();
            aux.Columns.Add("materia");
            if (dt.Rows.Count != 0)
            {
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    DataRow row = dt.Rows[a];
                    string esp = row["especialidad"].ToString();
                    string materia = row["materia"].ToString();
                    if (esp == especialidad)
                    {
                        DataRow fila = aux.NewRow();
                        fila["materia"] = materia;
                        aux.Rows.Add(fila);
                    }
                }
            }
            return aux;
        }
        public ArrayList getListSubAreas(string esp)
        {
            DataTable dt = getSubArea(esp);
            ArrayList list = new ArrayList();
            foreach (DataRow row in dt.Rows)
            {
                string materia = row["materia"].ToString();
                list.Add(materia);
            }
            return list;
        }
        public void deleteMateria(string materia, string area, string especialidad)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spDelMateria", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@materia", SqlDbType.VarChar).Value = materia;
            cmd.Parameters.Add("@area", SqlDbType.VarChar).Value = area;
            cmd.Parameters.Add("@especialidad", SqlDbType.VarChar).Value = especialidad;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void editMateria(string nuevo, string materia, string area, string especialidad)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spEditMateria", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@materiaN", SqlDbType.VarChar).Value = nuevo;
            cmd.Parameters.Add("@materia", SqlDbType.VarChar).Value = materia;
            cmd.Parameters.Add("@area", SqlDbType.VarChar).Value = area;
            cmd.Parameters.Add("@especialidad", SqlDbType.VarChar).Value = especialidad;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void insertSubArea(string materia, string especialidad)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spInsSubArea", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@materia", SqlDbType.VarChar).Value = materia;
            cmd.Parameters.Add("@area", SqlDbType.VarChar).Value = "tecnica";
            cmd.Parameters.Add("@especialidad", SqlDbType.VarChar).Value = especialidad;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void insertMateria(string materia)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spInsMateria", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@materia", SqlDbType.VarChar).Value = materia;
            cmd.Parameters.Add("@area", SqlDbType.VarChar).Value = "academica";
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public Boolean existMateria(string materia, string area, string especialidad)
        {
            bool exist = false;
            DataTable dt = getMaterias(area);
            if (dt.Rows.Count != 0)
            {
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    DataRow row = dt.Rows[a];
                    string esp = row["especialidad"].ToString();
                    string subj = row["materia"].ToString();
                    if (esp == especialidad && subj == materia)
                    {
                        exist = true;
                    }
                }
            }
            return exist;
        }
        public void createProcessAdmission(string year)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spInsNewProceso", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public Boolean existProcess(string year)
        {
            string[] dataAdmision = getDataProcesoAdmision(year);
            if (dataAdmision[0] == year)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string[] getDataProcesoAdmision(string year)
        {
            string[] data = new string[6];
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetDataProceso", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
            SqlDataReader lector = cmd.ExecuteReader();
            while (lector.Read())
            {
                data[0] = lector.GetString(0);
                data[1] += lector.GetInt32(1);
                data[2] = lector.GetString(2);
                data[3] = lector.GetString(3);
                data[4] += lector.GetInt32(4);
                data[5] += lector.GetInt32(5);
            }
            lector.Close();
            con.Close();
            return data;
        }
        public DataTable getAllStudents()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetAllPreMatriculado", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter cmdLeer = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cmdLeer.Fill(dt);
            con.Close();
            return dt;
        }
        public void ResetPreMatricula()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spDelPreMatricula", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public Boolean editDataAdmission(string year, string cantPines, string priceSobre, string inicio, string final, string cantStudents)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spEditProcess", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
                cmd.Parameters.Add("@cant", SqlDbType.Int).Value = Convert.ToInt32(cantStudents);
                cmd.Parameters.Add("@pines", SqlDbType.Int).Value = Convert.ToInt32(cantPines);
                cmd.Parameters.Add("@sobre", SqlDbType.Int).Value = Convert.ToInt32(priceSobre);
                cmd.Parameters.Add("@final", SqlDbType.VarChar).Value = final;
                cmd.Parameters.Add("@inicio", SqlDbType.VarChar).Value = inicio;
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception)
            {
                con.Close();
                return false;
            }
        }
        public void AddStudent(string id, string nom, string ape1, string correo,string fechaNac, 
        string telefono, string nacionalidad, string sexo, string edad,string provincia, string canton, 
        string distrito, string telResi, string direccion, string nomP, string nomM, string nomE, string cedP,
        string cedM, string cedE, string telM, string telE, string telP, string ingresoP, string ingresoM, string ingresoE, 
        string telWorkP, string telWorkM, string telWorkE)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spInsStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt64(id);
            cmd.Parameters.Add("@tel", SqlDbType.Int).Value = telefono;
            cmd.Parameters.Add("@nom", SqlDbType.VarChar).Value = nom;
            cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = correo;
            cmd.Parameters.Add("@nacionalidad", SqlDbType.VarChar).Value = nacionalidad;
            cmd.Parameters.Add("@ape1", SqlDbType.VarChar).Value = ape1;
            cmd.Parameters.Add("@nacimiento", SqlDbType.DateTime).Value = fechaNac;
            cmd.Parameters.Add("@sexo", SqlDbType.VarChar).Value = sexo;
            cmd.Parameters.Add("@edad", SqlDbType.Int).Value = Convert.ToInt32(edad);
            cmd.Parameters.Add("@provincia", SqlDbType.VarChar).Value = provincia;
            cmd.Parameters.Add("@canton", SqlDbType.VarChar).Value = canton;
            cmd.Parameters.Add("@distrito", SqlDbType.VarChar).Value = distrito;
            cmd.Parameters.Add("@telRes", SqlDbType.Int).Value = Convert.ToInt32(telResi);
            cmd.Parameters.Add("@direccion", SqlDbType.VarChar).Value = direccion;
            cmd.Parameters.Add("@nomP", SqlDbType.VarChar).Value = nomP;
            cmd.Parameters.Add("@nomM", SqlDbType.VarChar).Value = nomM;
            cmd.Parameters.Add("@nomE", SqlDbType.VarChar).Value = nomE;
            cmd.Parameters.Add("@cedP", SqlDbType.BigInt).Value = Convert.ToInt64(cedP);
            cmd.Parameters.Add("@cedM", SqlDbType.BigInt).Value = Convert.ToInt64(cedM);
            cmd.Parameters.Add("@cedE", SqlDbType.BigInt).Value = Convert.ToInt64(cedE);
            cmd.Parameters.Add("@telP", SqlDbType.Int).Value = Convert.ToInt32(telP);
            cmd.Parameters.Add("@telM", SqlDbType.Int).Value = Convert.ToInt32(telM);
            cmd.Parameters.Add("@telE", SqlDbType.Int).Value = Convert.ToInt32(telE);
            cmd.Parameters.Add("@ingresoE", SqlDbType.Int).Value = Convert.ToInt32(ingresoE);
            cmd.Parameters.Add("@ingresoM", SqlDbType.Int).Value = Convert.ToInt32(ingresoM);
            cmd.Parameters.Add("@ingresoP", SqlDbType.Int).Value = Convert.ToInt32(ingresoP);
            cmd.Parameters.Add("@telTm", SqlDbType.Int).Value = Convert.ToInt32(telWorkM);
            cmd.Parameters.Add("@telTe", SqlDbType.Int).Value = Convert.ToInt32(telWorkE);
            cmd.Parameters.Add("@telTp", SqlDbType.Int).Value = Convert.ToInt32(telWorkP);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void EditStudent(string id, string nom, string ape1, string correo, string fechaNac,
string telefono, string nacionalidad, string sexo, string edad, string provincia, string canton,
string distrito, string telResi, string direccion, string nomP, string nomM, string nomE, string cedP,
string cedM, string cedE, string telM, string telE, string telP, string ingresoP, string ingresoM, string ingresoE,
string telWorkP, string telWorkM, string telWorkE, string idNuevo)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spInsStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt64(id);
            cmd.Parameters.Add("@idNuevo", SqlDbType.BigInt).Value = Convert.ToInt64(idNuevo);
            cmd.Parameters.Add("@tel", SqlDbType.Int).Value = telefono;
            cmd.Parameters.Add("@nom", SqlDbType.VarChar).Value = nom;
            cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = correo;
            cmd.Parameters.Add("@nacionalidad", SqlDbType.VarChar).Value = nacionalidad;
            cmd.Parameters.Add("@ape1", SqlDbType.VarChar).Value = ape1;
            cmd.Parameters.Add("@nacimiento", SqlDbType.DateTime).Value = fechaNac;
            cmd.Parameters.Add("@sexo", SqlDbType.VarChar).Value = sexo;
            cmd.Parameters.Add("@edad", SqlDbType.Int).Value = Convert.ToInt32(edad);
            cmd.Parameters.Add("@provincia", SqlDbType.VarChar).Value = provincia;
            cmd.Parameters.Add("@canton", SqlDbType.VarChar).Value = canton;
            cmd.Parameters.Add("@distrito", SqlDbType.VarChar).Value = distrito;
            cmd.Parameters.Add("@telRes", SqlDbType.Int).Value = Convert.ToInt32(telResi);
            cmd.Parameters.Add("@direccion", SqlDbType.VarChar).Value = direccion;
            cmd.Parameters.Add("@nomP", SqlDbType.VarChar).Value = nomP;
            cmd.Parameters.Add("@nomM", SqlDbType.VarChar).Value = nomM;
            cmd.Parameters.Add("@nomE", SqlDbType.VarChar).Value = nomE;
            cmd.Parameters.Add("@cedP", SqlDbType.BigInt).Value = Convert.ToInt64(cedP);
            cmd.Parameters.Add("@cedM", SqlDbType.BigInt).Value = Convert.ToInt64(cedM);
            cmd.Parameters.Add("@cedE", SqlDbType.BigInt).Value = Convert.ToInt64(cedE);
            cmd.Parameters.Add("@telP", SqlDbType.Int).Value = Convert.ToInt32(telP);
            cmd.Parameters.Add("@telM", SqlDbType.Int).Value = Convert.ToInt32(telM);
            cmd.Parameters.Add("@telE", SqlDbType.Int).Value = Convert.ToInt32(telE);
            cmd.Parameters.Add("@ingresoE", SqlDbType.Int).Value = Convert.ToInt32(ingresoE);
            cmd.Parameters.Add("@ingresoM", SqlDbType.Int).Value = Convert.ToInt32(ingresoM);
            cmd.Parameters.Add("@ingresoP", SqlDbType.Int).Value = Convert.ToInt32(ingresoP);
            cmd.Parameters.Add("@telTm", SqlDbType.Int).Value = Convert.ToInt32(telWorkM);
            cmd.Parameters.Add("@telTe", SqlDbType.Int).Value = Convert.ToInt32(telWorkE);
            cmd.Parameters.Add("@telTp", SqlDbType.Int).Value = Convert.ToInt32(telWorkP);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public Boolean ExistStudent(string ced)
        {
            bool exist = false;
            foreach(DataRow row in getDataStudents().Rows)
            {
                if (row["cedula"].ToString() == ced)
                {
                    exist = true;
                }
            }
            return exist;
        }
        public Boolean ExistEmail(string email)
        {
            bool exist = false;
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetAllUsers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader lector = cmd.ExecuteReader();
            while (lector.Read())
            {
                if (email == lector.GetString(6))
                {
                    exist = true;
                }
            }
            lector.Close();
            con.Close();
            return exist;
        }
        public DataTable getDataStudents()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetStudents", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter cmdLeer = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cmdLeer.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable getAllPreMatriculados(string especial)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spSacarPreMatriculados", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@especialidad", SqlDbType.VarChar).Value = especial;
            DataTable dt = new DataTable();
            SqlDataAdapter cmdLeer = new SqlDataAdapter(cmd);
            cmdLeer.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable getReportStudents(string especial,string opcion)
        {
            DataTable table = new DataTable("PreMatriculados");
            table.Columns.Add("Cédula",typeof(string));
            table.Columns.Add("Nombre", typeof(string));
            table.Columns.Add("Apellidos", typeof(string));
            table.Columns.Add("Correo", typeof(string));
            table.Columns.Add("Opcion1", typeof(string));
            table.Columns.Add("Opcion2", typeof(string));
            table.Columns.Add("Consecutivo");
            foreach(DataRow row in getAllPreMatriculados(especial).Rows)
            {
                if (row["opcion1"].ToString() == especial&&opcion=="1")
                {
                    string ced = row["cedula"].ToString();
                    string nom = row["nombre"].ToString();
                    string ape = row["apellidos"].ToString();
                    string email = row["correo"].ToString();
                    string opc2 = row["opcion2"].ToString();
                    string sobre = row["sobre"].ToString();
                    table.Rows.Add(new Object[] { ced, nom, ape, email, especial, opc2,sobre });
                }
                if (row["opcion2"].ToString() == especial && opcion == "2")
                {
                    string ced = row["cedula"].ToString();
                    string nom = row["nombre"].ToString();
                    string ape = row["apellidos"].ToString();
                    string email = row["correo"].ToString();
                    string opc1 = row["opcion1"].ToString();
                    string sobre = row["sobre"].ToString();
                    table.Rows.Add(new Object[] { ced, nom, ape, email, especial, opc1, sobre });
                }
            }
            return table;
        }
        public DataTable getReportStudentsInAdmission(string especial, string opcion)
        {
            DataTable table = new DataTable("PreMatriculados");
            table.Columns.Add("Consecutivo");
            table.Columns.Add("Cédula", typeof(string));
            table.Columns.Add("Nombre", typeof(string));
            table.Columns.Add("Apellidos", typeof(string));
            table.Columns.Add("Conducta", typeof(string));
            table.Columns.Add("Asistencia", typeof(string));
            table.Columns.Add("Notas", typeof(string));
            table.Columns.Add("Examen", typeof(string));
            table.Columns.Add("Entrevista", typeof(string));
            table.Columns.Add("Final", typeof(string));
            table.Columns.Add("SalarioEncargado", typeof(string));
            table.Columns.Add("SalarioPadre", typeof(string));
            table.Columns.Add("SalarioMadre", typeof(string));
            DataTable data = new DataTable();
            if (opcion == "1")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spSacarPreAdmitidosOpcion1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@especialidad", SqlDbType.VarChar).Value = especial;
                DataTable dt = new DataTable();
                SqlDataAdapter cmdLeer = new SqlDataAdapter(cmd);
                cmdLeer.Fill(data);
                con.Close();
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spSacarPreAdmitidosOpcion2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@especialidad", SqlDbType.VarChar).Value = especial;
                DataTable dt = new DataTable();
                SqlDataAdapter cmdLeer = new SqlDataAdapter(cmd);
                cmdLeer.Fill(data);
                con.Close();
            }
            foreach (DataRow row in data.Rows)
            {
                string ced = row["cedula"].ToString();
                string nom = row["nombre"].ToString();
                string ape = row["apellidos"].ToString();
                string conducta = row["conducta"].ToString();
                if (conducta != "")
                {
                    conducta = (Convert.ToDecimal(conducta) * Convert.ToDecimal(0.05)).ToString("##.##");
                }
                string ausencias = row["ausencias"].ToString();
                string notas = row["notas"].ToString();
                string examen = row["examen"].ToString();
                string entrevista = row["entrevista"].ToString();
                string resultado = row["resultado"].ToString();
                string encargado = row["ingresoEncargado"].ToString();
                string padre = row["ingresoPadre"].ToString();
                string madre = row["ingresoMadre"].ToString();
                string sobre = row["sobre"].ToString();
                table.Rows.Add(new Object[] {sobre, ced, nom, ape, conducta, ausencias, notas, examen,entrevista,resultado,encargado,padre,madre});
            }
            return table;
        }
        public DataTable getPreMatriculadosOpcion2(string especial)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spSacarPreMatriculadosOpcion2", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@especialidad", SqlDbType.VarChar).Value = especial;
            DataTable dt = new DataTable();
            SqlDataAdapter cmdLeer = new SqlDataAdapter(cmd);
            cmdLeer.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable getPreMatriculadosOpcion1(string especial)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spSacarPreMatriculadosOpcion1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@especialidad", SqlDbType.VarChar).Value = especial;
            DataTable dt = new DataTable();
            SqlDataAdapter cmdLeer = new SqlDataAdapter(cmd);
            cmdLeer.Fill(dt);
            con.Close();
            return dt;
        }
    }
}