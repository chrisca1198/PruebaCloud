using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Globalization;

namespace Cote_Cloud.transacciones
{
    public class TransPreMatricula
    {
        generarPines clase = new generarPines();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
        public void deletePreMatriculado(string id)
        {
            SqlCommand cmd = new SqlCommand("spDelPrematriculado", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt64(id);
            cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = DateTime.Today.Year.ToString();
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
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
        public ArrayList sacarEspeci()
        {
            ArrayList lista = new ArrayList();
            DataTable tabla = sacarEsp();
            foreach(DataRow row in tabla.Rows)
            {
                lista.Add(row[0].ToString());
            }
            return lista;
        }
        public ArrayList sacarEspeciDisponibles()
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
        public Boolean Contains(string cod)
        {
            bool tiene = false;
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetPinesDisponibles", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader lee = cmd.ExecuteReader();
            while (lee.Read())
            {
                if (cod == lee.GetString(0))
                {
                    lee.Close();
                    tiene = true;
                    break;
                }
            }
            con.Close();
            return tiene;
        }
        public void insertEspecialidades(string especialidades,string disponi,int cant)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spModifEspecialidad", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@especialidad", SqlDbType.VarChar).Value = especialidades;
            cmd.Parameters.Add("@disponible", SqlDbType.VarChar).Value = disponi;
            cmd.Parameters.Add("@cant", SqlDbType.Int).Value = cant;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void deleteAdmitidos(string id,string espe)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spDelAdmitido", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt64(id);
            cmd.Parameters.Add("@especialidad", SqlDbType.VarChar).Value = espe;
            cmd.ExecuteNonQuery();
            con.Close();
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
        public Boolean ingresarPromedios(string notas, string examen, string entrevista,long id,string conduct,string ausencias)
        {
            try
            {
                decimal promedioNotas= Convert.ToDecimal(notas)*Convert.ToDecimal(0.25);
                decimal promedioConducta = Convert.ToDecimal(conduct) * Convert.ToDecimal(0.05);
                int ausencia = 0;
                if (Convert.ToInt32(ausencias)>=0&& Convert.ToInt32(ausencias)<=10)
                {
                    ausencia += 5;
                }
                if (Convert.ToInt32(ausencias) >= 11 && Convert.ToInt32(ausencias) <= 30)
                {
                    ausencia += 4;
                }
                if (Convert.ToInt32(ausencias) >= 31 && Convert.ToInt32(ausencias) <= 50)
                {
                    ausencia += 3;
                }
                if (Convert.ToInt32(ausencias) >= 51 && Convert.ToInt32(ausencias) <= 70)
                {
                    ausencia += 2;
                }
                if (Convert.ToInt32(ausencias) >= 71 && Convert.ToInt32(ausencias) <= 90)
                {
                    ausencia += 1;
                }
                decimal result = (promedioNotas + Convert.ToDecimal(entrevista) + Convert.ToDecimal(examen) +promedioConducta+ Convert.ToDecimal(ausencia));
                con.Open();
                SqlCommand cmd = new SqlCommand("spInsertResults", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@notas", SqlDbType.Decimal).Value = Convert.ToDecimal(notas);
                cmd.Parameters.Add("@entrevista", SqlDbType.Decimal).Value = entrevista;
                cmd.Parameters.Add("@examen", SqlDbType.Decimal).Value = examen;
                cmd.Parameters.Add("@resultado", SqlDbType.Decimal).Value = result;
                cmd.Parameters.Add("@ced", SqlDbType.BigInt).Value = id;
                cmd.Parameters.Add("@conducta", SqlDbType.Decimal).Value = conduct;
                cmd.Parameters.Add("@ausencias", SqlDbType.Int).Value = Convert.ToInt32(ausencias);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception a)
            {
                string error = a.ToString();
                return false;
            }
        }
        public Boolean ingresarNotas(string ced,string setimoEspañol, string setimoIngles, string setimoMate,string setimoEstudios,string setimoCivica,
            string setimoCiencias,string setimoAusencias,string setimoConducta, string octavoEspañol, string octavoIngles, string octavoMate, string octavoEstudios, string octavoCivica,
            string octavoCiencias, string octavoAusencias, string octavoConducta, string novenoEspañol, string novenoIngles, string novenoMate, string novenoEstudios, string novenoCivica,
            string novenoCiencias, string novenoAusencias, string novenoConducta)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spInsNotasAdmitidos", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ced", SqlDbType.BigInt).Value = Convert.ToInt64(ced);
                cmd.Parameters.Add("@setEspa", SqlDbType.Decimal).Value = decimal.Parse(setimoEspañol, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@setIng", SqlDbType.Decimal).Value = decimal.Parse(setimoIngles, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@setMate", SqlDbType.Decimal).Value = decimal.Parse(setimoMate, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@setEst", SqlDbType.Decimal).Value = decimal.Parse(setimoEstudios, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@setCiv", SqlDbType.Decimal).Value = decimal.Parse(setimoCivica, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@setCien", SqlDbType.Decimal).Value = decimal.Parse(setimoCiencias, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@setAusIn", SqlDbType.Int).Value = Convert.ToInt32(setimoAusencias);
                cmd.Parameters.Add("@setCond", SqlDbType.Decimal).Value = decimal.Parse(setimoConducta, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@octEspa", SqlDbType.Decimal).Value = decimal.Parse(octavoEspañol, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@octIng", SqlDbType.Decimal).Value = decimal.Parse(octavoIngles, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@octMate", SqlDbType.Decimal).Value = decimal.Parse(octavoMate, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@octEst", SqlDbType.Decimal).Value = decimal.Parse(octavoEstudios, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@octCiv", SqlDbType.Decimal).Value = decimal.Parse(octavoCivica, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@octCien", SqlDbType.Decimal).Value = decimal.Parse(octavoCiencias, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@octAusIn", SqlDbType.Int).Value = Convert.ToInt32(octavoAusencias);
                cmd.Parameters.Add("@octCond", SqlDbType.Decimal).Value = decimal.Parse(octavoConducta, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@novEspa", SqlDbType.Decimal).Value = decimal.Parse(novenoEspañol, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@novIng", SqlDbType.Decimal).Value = decimal.Parse(novenoIngles, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@novMate", SqlDbType.Decimal).Value = decimal.Parse(novenoMate, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@novEst", SqlDbType.Decimal).Value = decimal.Parse(novenoEstudios, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@novCiv", SqlDbType.Decimal).Value = decimal.Parse(novenoCivica, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@novCien", SqlDbType.Decimal).Value = decimal.Parse(novenoCiencias, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@novAusIn", SqlDbType.Int).Value = Convert.ToInt32(novenoAusencias);
                cmd.Parameters.Add("@novCond", SqlDbType.Decimal).Value = decimal.Parse(novenoConducta, CultureInfo.InvariantCulture);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Boolean editNotas(string ced, string setimoEspañol, string setimoIngles, string setimoMate, string setimoEstudios, string setimoCivica,
string setimoCiencias, string setimoAusencias, string setimoConducta, string octavoEspañol, string octavoIngles, string octavoMate, string octavoEstudios, string octavoCivica,
string octavoCiencias, string octavoAusencias, string octavoConducta, string novenoEspañol, string novenoIngles, string novenoMate, string novenoEstudios, string novenoCivica,
string novenoCiencias, string novenoAusencias, string novenoConducta)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spEditNotasAdmitidos", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ced", SqlDbType.BigInt).Value = Convert.ToInt64(ced);
                cmd.Parameters.Add("@setEspa", SqlDbType.Decimal).Value = decimal.Parse(setimoEspañol, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@setIng", SqlDbType.Decimal).Value = decimal.Parse(setimoIngles, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@setMate", SqlDbType.Decimal).Value = decimal.Parse(setimoMate, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@setEst", SqlDbType.Decimal).Value = decimal.Parse(setimoEstudios, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@setCiv", SqlDbType.Decimal).Value = decimal.Parse(setimoCivica, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@setCien", SqlDbType.Decimal).Value = decimal.Parse(setimoCiencias, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@setAusIn", SqlDbType.Int).Value = Convert.ToInt32(setimoAusencias);
                cmd.Parameters.Add("@setCond", SqlDbType.Decimal).Value = decimal.Parse(setimoConducta, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@octEspa", SqlDbType.Decimal).Value = decimal.Parse(octavoEspañol, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@octIng", SqlDbType.Decimal).Value = decimal.Parse(octavoIngles, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@octMate", SqlDbType.Decimal).Value = decimal.Parse(octavoMate, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@octEst", SqlDbType.Decimal).Value = decimal.Parse(octavoEstudios, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@octCiv", SqlDbType.Decimal).Value = decimal.Parse(octavoCivica, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@octCien", SqlDbType.Decimal).Value = decimal.Parse(octavoCiencias, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@octAusIn", SqlDbType.Int).Value = Convert.ToInt32(octavoAusencias);
                cmd.Parameters.Add("@octCond", SqlDbType.Decimal).Value = decimal.Parse(octavoConducta, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@novEspa", SqlDbType.Decimal).Value = decimal.Parse(novenoEspañol, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@novIng", SqlDbType.Decimal).Value = decimal.Parse(novenoIngles, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@novMate", SqlDbType.Decimal).Value = decimal.Parse(novenoMate, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@novEst", SqlDbType.Decimal).Value = decimal.Parse(novenoEstudios, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@novCiv", SqlDbType.Decimal).Value = decimal.Parse(novenoCivica, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@novCien", SqlDbType.Decimal).Value = decimal.Parse(novenoCiencias, CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@novAusIn", SqlDbType.Int).Value = Convert.ToInt32(novenoAusencias);
                cmd.Parameters.Add("@novCond", SqlDbType.Decimal).Value = decimal.Parse(novenoConducta, CultureInfo.InvariantCulture);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception a)
            {
                return false;
            }
        }
        public DataTable getNotasEstudiante(string id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetNotasEstudiante", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt64(id);
            DataTable dt = new DataTable();
            SqlDataAdapter cmdLeer = new SqlDataAdapter(cmd);
            cmdLeer.Fill(dt);
            con.Close();
            return dt;
        }
        public bool existStudentEdit(string id)
        {
            bool fal = false;
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetPreMatriculado", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt64(id);
            SqlDataReader cmdleer = cmd.ExecuteReader();
            while (cmdleer.Read())
            {
                if (cmdleer.GetInt64(0) == Convert.ToInt64(id) && id !=HttpContext.Current.Session["IdEditStudent"].ToString())
                {
                    fal = true;
                }
            }
            con.Close();
            return fal;
        }
        public bool existStudent(string id)
        {
            bool fal = false;
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetPreMatriculado", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt64(id);
            SqlDataReader cmdleer = cmd.ExecuteReader();
            while (cmdleer.Read())
            {
                if (cmdleer.GetInt64(0) == Convert.ToInt64(id))
                {
                    fal = true;
                }
            }
            con.Close();
            return fal;
        }
        public DataTable getAllDataPrematricula()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetAllPreMatriculado", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable GetStudent(string id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetPreMatriculado", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt64(id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable getReportStudent(string id)
        {
            DataTable data = GetStudent(id);
            DataTable regresar = new DataTable();
            foreach(DataColumn col in data.Columns)
            {
                regresar.Columns.Add(col.ColumnName);
            }
            foreach(DataRow row in data.Rows)
            {
                DataRow newRow = regresar.NewRow();
                foreach (DataColumn col in data.Columns)
                {
                    if (row[col.ColumnName].ToString() == "0")
                    {
                        newRow[col.ColumnName] = " ";
                    }
                    else
                    {
                        newRow[col.ColumnName] = row[col.ColumnName];
                    }
                }
                regresar.Rows.Add(newRow);
            }
            return regresar;
        }
        public bool existEmail(string email)
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
                if (cmdleer.GetString(3) == email)
                {
                    fal = true;
                }
            }
            con.Close();
            return fal;
        }
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
        public void ingresarDatos(string id, string nom, string ape1, string correo,
    string numSobre, string opc1, string opc2,
    string fechaNac, string telefono, string nacionalidad, string colegioProce, string sexo, string edad,
    string provincia, string canton, string distrito, string telResi, string direccion, string nomP, string nomM, string nomE, string cedP,
    string cedM, string cedE, string telM, string telE, string telP, string ingresoP, string ingresoM, string ingresoE, string ocupacionM, string ocupacionP,
    string ocupacionE, string telWorkP, string telWorkM, string telWorkE, string pin)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spInPreadmitido", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt64(id);
            cmd.Parameters.Add("@sobre", SqlDbType.Int).Value = Convert.ToInt32(numSobre);
            cmd.Parameters.Add("@tel", SqlDbType.Int).Value = telefono;
            cmd.Parameters.Add("@nom", SqlDbType.VarChar).Value = nom;
            cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = correo;
            cmd.Parameters.Add("@opcion1", SqlDbType.VarChar).Value = opc1;
            cmd.Parameters.Add("@opcion2", SqlDbType.VarChar).Value = opc2;
            cmd.Parameters.Add("@nacionalidad", SqlDbType.VarChar).Value = nacionalidad;
            cmd.Parameters.Add("@coleprocedencia", SqlDbType.Text).Value = colegioProce;
            cmd.Parameters.Add("@ape1", SqlDbType.VarChar).Value = ape1;
            cmd.Parameters.Add("@nacimiento", SqlDbType.DateTime).Value = fechaNac;
            cmd.Parameters.Add("@sexo", SqlDbType.VarChar).Value = sexo;
            cmd.Parameters.Add("@edad", SqlDbType.Int).Value = Convert.ToInt32(edad);
            cmd.Parameters.Add("@provincia", SqlDbType.VarChar).Value = provincia;
            cmd.Parameters.Add("@canton", SqlDbType.VarChar).Value = canton;
            cmd.Parameters.Add("@distrito", SqlDbType.VarChar).Value = distrito;
            cmd.Parameters.Add("@telRes", SqlDbType.Int).Value = Convert.ToInt32(telResi);
            cmd.Parameters.Add("@direccion", SqlDbType.Text).Value = direccion;
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
            cmd.Parameters.Add("@ocupacionM", SqlDbType.VarChar).Value = ocupacionM;
            cmd.Parameters.Add("@ocupacionP", SqlDbType.VarChar).Value = ocupacionP;
            cmd.Parameters.Add("@ocupacionE", SqlDbType.VarChar).Value = ocupacionE;
            cmd.Parameters.Add("@telTm", SqlDbType.Int).Value = Convert.ToInt32(telWorkM);
            cmd.Parameters.Add("@telTe", SqlDbType.Int).Value = Convert.ToInt32(telWorkE);
            cmd.Parameters.Add("@telTp", SqlDbType.Int).Value = Convert.ToInt32(telWorkP);
            cmd.Parameters.Add("@pin", SqlDbType.VarChar).Value = pin;
            cmd.ExecuteNonQuery();
            con.Close();
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
        public bool editDataPreMatriculado(string id, string correo, string nombre, string apellidos, string telefono, string edad, string provincia,
    string canton, string distrito, string telResi, string direccion, string nomP, string nomM, string nomE,
    string cedP, string cedM, string cedE, string telP, string telM, string telE, string ingresoE,
    string ingresoM, string ingresoP, string ocupacionM, string ocupacionP, string ocupacionE, string telWorkP, string telWorkM,
    string telWorkE)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spEditPreMatriculado", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt64(HttpContext.Current.Session["IdEditStudent"].ToString());
                cmd.Parameters.Add("@Newid", SqlDbType.BigInt).Value = Convert.ToInt64(numbersInserted(id));
                cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = correo;
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre;
                cmd.Parameters.Add("@apellidos", SqlDbType.VarChar).Value = apellidos;
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
            }
            catch (Exception a)
            {
                Console.Write(a);
            }
            return true;
        }
    }
}