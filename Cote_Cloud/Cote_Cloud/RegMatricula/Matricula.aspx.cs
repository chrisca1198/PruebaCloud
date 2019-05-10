using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cote_Cloud.RegMatricula
{
    public partial class Matricula : System.Web.UI.Page
    {
        transacciones.TransMatricula transac = new transacciones.TransMatricula();
        transacciones.administracion admin = new transacciones.administracion();
        transacciones.TransPreMatricula datos = new transacciones.TransPreMatricula();
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = Request.Url.Query.TrimStart('?');
            if (!IsPostBack)
            {
                Session["url"] = url;
            }
            if (Session["url"].ToString() != "")
            {
                try
                {
                    string[] allData = Session["url"].ToString().Split('?');
                    string[] dataID = allData[0].Split('/');
                    string[] dataEsp = allData[1].Split('/');
                    string id = "";
                    string esp = "";
                    for (int a = 0; a < dataID.Length; a++)
                    {
                        char[] letras = dataID[a].ToCharArray();
                        id += letras[(letras.Length - 1)];
                    }
                    for (int a = 0; a < dataEsp.Length; a++)
                    {
                        esp += dataEsp[a] + " ";
                    }
                    esp = esp.TrimEnd(' ');
                    if (id.Length == 0 || esp.Length == 0)
                        Matriculas.llenar("El enlace no contiene identificación de algún estudiante en el proceso", "Enlace vacío");
                    else
                    {
                        if (transac.getStudentAdmitido(id, esp) != null)
                        {
                            if (!IsPostBack)
                            {
                                DataRow dataStudent = datos.GetStudent(id).Rows[0];
                                txtCanton.Text = dataStudent["canton"].ToString();
                                txtCedula.Value = numbersInserted(dataStudent["cedula"].ToString());
                                txtCedulaE.Value = numbersInserted(dataStudent["cedEncargado"].ToString());
                                txtCedulaM.Value = numbersInserted(dataStudent["cedMadre"].ToString());
                                txtCedulaP.Value = numbersInserted(dataStudent["cedPadre"].ToString());
                                txtCuota.Value = "";
                                txtIngresoE.Value = numbersInserted(dataStudent["ingresoEncargado"].ToString());
                                txtIngresoM.Value = numbersInserted(dataStudent["ingresoMadre"].ToString());
                                txtIngresoP.Value = numbersInserted(dataStudent["ingresoPadre"].ToString());
                                txtTelE.Value = numbersInserted(dataStudent["telEncargado"].ToString());
                                txtTelefono.Value = numbersInserted(dataStudent["telefono"].ToString());
                                txtTelM.Value = numbersInserted(dataStudent["telMadre"].ToString());
                                txtTelP.Value = numbersInserted(dataStudent["telPadre"].ToString());
                                txtTelResi.Value = numbersInserted(dataStudent["telefonoResidencia"].ToString());
                                txtTelTrabajoE.Value = numbersInserted(dataStudent["telTrabajoEncargado"].ToString());
                                txtTelTrabajoM.Value = numbersInserted(dataStudent["telTrabajoMadre"].ToString());
                                txtTelTrabajoP.Value = numbersInserted(dataStudent["telTrabajoPadre"].ToString());
                                txtdir.Text = dataStudent["direccionResidencia"].ToString();
                                txtDistrito.Text = dataStudent["distrito"].ToString();
                                int edad = (DateTime.Now.Year - Convert.ToDateTime(dataStudent["nacimiento"].ToString()).Year);
                                if ((DateTime.Now.Month - Convert.ToDateTime(dataStudent["nacimiento"].ToString()).Month) <= 0)
                                {
                                    if ((DateTime.Now.Day - Convert.ToDateTime(dataStudent["nacimiento"].ToString()).Day) < 0)
                                    {
                                        edad--;
                                    }
                                }
                                txtEdad.Text = edad.ToString();
                                txtEmail.Text = dataStudent["correo"].ToString();
                                txtNom.Text = dataStudent["nombre"].ToString() + " " + dataStudent["apellidos"].ToString();
                                txtNomE.Text = dataStudent["nomEncargado"].ToString();
                                txtnomM.Text = dataStudent["nomMadre"].ToString();
                                txtNomP.Text = dataStudent["nomPadre"].ToString();
                                txtOcupaE.Text = dataStudent["ocupacionEncargado"].ToString();
                                txtOcupaM.Text = dataStudent["ocupacionMadre"].ToString();
                                txtOcupaP.Text = dataStudent["ocupacionPadre"].ToString();
                                txtPoliza.Text = "";
                                txtProvincia.Text = dataStudent["provincia"].ToString();
                                importante.llenar("Para poder completar el proceso de matrícula complete los datos de la póliza y cuota mensual. Además verifique que los datos ingresados en la pre matrícula sean correctos.","Matrícula estudiante.");
                            }
                        }
                        else
                        {
                            if (admin.ExistStudent(id)) Matriculas.llenar("El estudiante ya ha sido matrículado en la institución", "Estudiante matrículado.");
                            else Matriculas.llenar("El estudiante no existe en la lista de admitidos", "Estudiante inválido.");
                        }
                    }
                }
                catch (Exception a)
                {
                    Matriculas.llenar("El enlace no contiene identificación de algún estudiante en el proceso", "Enlace vacío");
                }
            }
            else
                Matriculas.llenar("El enlace no contiene identificación de algún estudiante en el proceso", "Enlace vacío");
        }
        private string numbersInserted(string number)
        {
            string value = "";
            if (number == "0" || number == "")
            {
                value = "";
            }
            else
            {
                value = number;
            }
            return value;
        }
        private Boolean DatosEstudiantesLlenos()
        {
            if (txtPoliza.Text==""||txtCuota.Value==""|| txtEmail.Text == "" || txtTelefono.Value == ""|| txtdir.Text == "" || txtProvincia.Text == "" || txtCanton.Text == "" || txtDistrito.Text == ""|| txtEmail.Text == "")
            {
                return false;
            }
            else
                return true;
        }
        private Boolean DatosEncargadosLlenos()
        {
            if ((txtNomP.Text != "" && txtCedulaP.Value != "" && txtTelP.Value != "") ||
                (txtnomM.Text != "" && txtCedulaM.Value != "" && txtTelM.Value != "") ||
                (txtNomE.Text != "" && txtCedulaE.Value != "" && txtTelE.Value != ""))
            {
                return true;
            }
            else
                return false;
        }
        private void vaciar(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if ((c.GetType() == typeof(TextBox)))
                {
                    ((TextBox)(c)).Text = "";
                }
                if ((c.GetType() == typeof(CheckBox)))
                {
                    ((CheckBox)(c)).Checked = false;
                }
                if ((c.GetType() == typeof(RadioButton)))
                {
                    ((RadioButton)(c)).Checked = false;
                }
                if ((c.GetType() == typeof(DropDownList)))
                {
                    ((DropDownList)(c)).SelectedIndex = 0;
                }
                if (c.HasControls())
                {
                    vaciar(c);
                }
            }
            txtTelefono.Value = "";
            txtTelE.Value = "";
            txtTelM.Value = "";
            txtTelP.Value = "";
            txtTelResi.Value = "";
            txtTelTrabajoE.Value = "";
            txtTelTrabajoM.Value = "";
            txtTelTrabajoP.Value = "";
            txtIngresoP.Value = "";
            txtIngresoM.Value = "";
            txtIngresoE.Value = "";
            txtCedulaE.Value = "";
            txtCedula.Value = "";
            txtCedulaM.Value = "";
            txtCedulaP.Value = "";
            txtCuota.Value = "";
            txtPoliza.Text = "";
        }
        protected void btnTabEncargado_Click(object sender, EventArgs e)
        {
            tabs.ActiveViewIndex = 0;
        }
        protected void btnTabPadre_Click(object sender, EventArgs e)
        {
            tabs.ActiveViewIndex = 1;
        }
        protected void btnTabMadre_Click(object sender, EventArgs e)
        {
            tabs.ActiveViewIndex = 2;
        }
        protected void btnFormatear_Click(object sender, EventArgs e)
        {
            vaciar(this);
            Response.Redirect(@"..\RegMatricula\Matricula.aspx");
            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (DatosEstudiantesLlenos())
            {
                if (DatosEncargadosLlenos())
                {
                    if (!transac.existEmail(txtEmail.Text,txtCedula.Value))
                    {
                        try
                        {
                            string id = txtCedula.Value;
                            transac.IngresarMatriculado(txtCedula.Value, txtEmail.Text, txtPoliza.Text, txtCuota.Value, txtTelefono.Value, txtEdad.Text, txtProvincia.Text, txtCanton.Text, txtDistrito.Text, txtTelResi.Value, txtdir.Text,
                                txtNomP.Text, txtnomM.Text, txtNomE.Text, txtCedulaP.Value, txtCedulaM.Value, txtCedulaE.Value, txtTelP.Value,
                                txtTelM.Value, txtTelE.Value, txtIngresoE.Value, txtIngresoM.Value, txtIngresoP.Value, txtOcupaM.Text, txtOcupaP.Text, txtOcupaE.Text, txtTelTrabajoP.Value,
                                txtTelTrabajoM.Value, txtTelTrabajoE.Value,txtNom.Text);
                            vaciar(this);
                            completado.llenar("El estudiante fue correctamente matrículado. Se le envió al correo electrónico  detalles de su usuario y contraseña de la plataforma web del COTEPECOS.");
                        }
                        catch (Exception a)
                        {
                            Cote_Cloud.User.email correo = new Cote_Cloud.User.email();
                            correo.mensaje = "Exception: " + a.ToString();
                            correo.MailTo = System.Configuration.ConfigurationManager.AppSettings.Get("Destinatario").ToString();
                            correo.MailSubject = "Excepción en matrícula de un estudiante";
                            correo.SendMail();
                            error.llenar("Sucedió un error al matricular el estudiante. Por favor verifique que los datos y formato de los mismos sean correctos. Sí el error persiste, envíe un correo a admisioncotepecos@gmail.com");
                        }
                    }
                    else
                    {
                        error.llenar("El correo ya está vinculado con otro estudiante");
                    }

                }
                else
                {
                    importante.llenar("Al menos alguno de los datos personales de los padres o encargado deben estar llenos", "Advertencia!");
                    importante.Visible = true;
                }
            }
            else
            {
                error.llenar("Datos de estudiantes que son requeridos no se han llenado");
                error.Visible = true;
            }
        }
    }
}