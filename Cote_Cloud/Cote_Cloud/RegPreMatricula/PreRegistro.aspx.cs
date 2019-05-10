using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Collections.Specialized;
using System.Web.Security;
using System.Security.Principal;
using System.Collections;
using System.Text;

namespace Cote_Cloud.RegPreMatricula
{
    public partial class PreRegistro : System.Web.UI.Page
    {
        transacciones.TransPreMatricula llamada = new transacciones.TransPreMatricula();
        transacciones.administracion admin = new transacciones.administracion();
        static string id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillCbo();
            }
            string[] data = admin.getDataProcesoAdmision(DateTime.Today.Year.ToString());
            string fecha = data[2];
            if (fecha == null)
            {
                prematricula.llenar("El proceso no está activo por el momento, se darán más detalles en unos meses...", "Proceso de admisión no activo");
                prematricula.Visible = true;
            }
            else
            {
                DateTime inicio = Convert.ToDateTime(data[2]).Date;
                DateTime final = Convert.ToDateTime(data[3]).Date;
                if (final < DateTime.Today.Date)
                {
                    prematricula.llenar("El proceso ha finalizado, en un año aproximadamente se habilitará nuevamente...", "Proceso de admisión concluido");
                    prematricula.Visible = true;
                }
                if (inicio > DateTime.Today.Date)
                {
                    prematricula.llenar("El proceso se habilitará la fecha: " + inicio.ToShortDateString(), "Proceso de admisión no activo");
                    prematricula.Visible = true;
                }
            }
        }
        private void fillCbo()
        {
            ArrayList aux = llamada.sacarEspeciDisponibles();
            ArrayList array = new ArrayList();
            array.Add("Seleccione la especialidad");
            array.Add("Informatica en desarrollo de software");
            cboOp1.DataSource = array;
            cboOp1.DataBind();
            array.RemoveAt(1);
            for (int a = 0; a < aux.Count; a++)
            {
                if (!(aux[a].ToString().Equals("Informatica en desarrollo de software")))
                {
                    array.Add(aux[a]);
                }
            }
            cboOp2.DataSource = array;
            cboOp2.DataBind();
        }
        private Boolean DatosEstudiantesLlenos()
        {
            if (txtApe1.Text == "" || txtCedula.Value == "" || txtColegio.Text == "" || txtEmail.Text == "" ||
                txtFechaNacimiento.Value == "" || txtNom.Text == "" || txtPin.Text == "" || txtSobre.Value == "" || txtTelefono.Value == ""
                || txtNac.Text == "" || txtdir.Text == "" || txtProvincia.Text == "" || txtCanton.Text == "" || txtDistrito.Text == ""
                || txtEmail.Text == "" || txtColegio.Text == "" || txtEdad.Value == "")
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
            txtSobre.Value = "";
            txtIngresoP.Value = "";
            txtIngresoM.Value = "";
            txtIngresoE.Value = "";
            txtFechaNacimiento.Value = "";
            txtEdad.Value = "";
            txtCedulaE.Value = "";
            txtCedula.Value = "";
            txtCedulaM.Value = "";
            txtCedulaP.Value = "";
            cboOp2.Visible = false;
            fillCbo();
        }
        private Boolean IsNumeric(string num)
        {
            bool nume = false;
            try
            {
                Convert.ToInt32(num);
                nume = true;
            }
            catch (Exception)
            { }
            return nume;
        }
        private String[] nums()
        {
            string[] numeros = new string[13];
            if (IsNumeric(txtTelResi.Value)) { numeros[0] = txtTelResi.Value; }
            else { numeros[0] = "0"; }
            if (IsNumeric(txtIngresoP.Value)) { numeros[1] = txtIngresoP.Value; }
            else { numeros[1] = "0"; }
            if (IsNumeric(txtTelTrabajoP.Value)) { numeros[2] = txtTelTrabajoP.Value; }
            else { numeros[2] = "0"; }
            if (IsNumeric(txtIngresoM.Value)) { numeros[3] = txtIngresoM.Value; }
            else { numeros[3] = "0"; }
            if (IsNumeric(txtTelTrabajoM.Value)) { numeros[4] = txtTelTrabajoM.Value; }
            else { numeros[4] = "0"; }
            if (IsNumeric(txtIngresoE.Value)) { numeros[5] = txtIngresoE.Value; }
            else { numeros[5] = "0"; }
            if (IsNumeric(txtTelTrabajoE.Value)) { numeros[6] = txtTelTrabajoE.Value; }
            else { numeros[6] = "0"; }
            if (IsNumeric(txtCedulaP.Value)) { numeros[7] = txtCedulaP.Value; }
            else { numeros[7] = "0"; }
            if (IsNumeric(txtTelP.Value)) { numeros[8] = txtTelP.Value; }
            else { numeros[8] = "0"; }
            if (IsNumeric(txtCedulaM.Value)) { numeros[9] = txtCedulaM.Value; }
            else { numeros[9] = "0"; }
            if (IsNumeric(txtTelM.Value)) { numeros[10] = txtTelM.Value; }
            else { numeros[10] = "0"; }
            if (IsNumeric(txtCedulaE.Value)) { numeros[11] = txtCedulaE.Value; }
            else { numeros[11] = "0"; }
            if (IsNumeric(txtTelE.Value)) { numeros[12] = txtTelE.Value; }
            else { numeros[12] = "0"; }
            return numeros;
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (DatosEstudiantesLlenos())
            {
                if (DatosEncargadosLlenos())
                {
                    if (!llamada.existStudent(txtCedula.Value))
                    {
                        if (!llamada.existEmail(txtEmail.Text))
                        {
                            if (cboOp1.Text == "Seleccione la especialidad")
                            {
                                error.llenar("La primera opción debe seleccionarse obligatoriamente");
                                error.Visible = true;
                            }
                            else
                            {
                                if (chkOpc2.Checked && !(cboOp2.Text == "Seleccione la especialidad") || !chkOpc2.Checked)
                                {
                                    if (!rbFeme.Checked && !rbMascul.Checked)
                                    {
                                        importante.llenar("Género no seleccionado", "Debe seleccionar el género del estudiante");
                                    }
                                    else
                                    {
                                        string opc2 = cboOp2.Text;
                                        if (cboOp2.Text == "Seleccione la especialidad") { opc2 = ""; }
                                        if (llamada.Contains(txtPin.Text) == true)
                                        {
                                            try
                                            {
                                                string genero = "";
                                                if (rbFeme.Checked) { genero = "Femenino"; }
                                                if (rbMascul.Checked) { genero = "Masculino"; }
                                                string[] numeros = nums();
                                                id = txtCedula.Value;
                                                //Reporte
                                                report.LocalReport.DataSources.Clear();
                                                report.LocalReport.DataSources.Add(new ReportDataSource("Datos", llamada.getReportStudent(txtCedula.Value)));
                                                report.LocalReport.ReportPath = "~/RegPreMatricula/ReportPreRegistro.rdlc";
                                                report.LocalReport.Refresh();
                                                Warning[] warnings;
                                                string[] streamids;
                                                string mimeType;
                                                string encoding;
                                                string extension;
                                                byte[] rep = report.LocalReport.Render("WORD", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                                                string path = Server.MapPath("~/datos/");
                                                path = Path.Combine(path, "Matricula de " + id + ".doc");
                                                FileStream fs = new FileStream(path, FileMode.Create);
                                                fs.Write(rep, 0, rep.Length);
                                                fs.Dispose();
                                                llamada.ingresarDatos(txtCedula.Value, txtNom.Text, txtApe1.Text, txtEmail.Text,
txtSobre.Value, cboOp1.Text, opc2, txtFechaNacimiento.Value, txtTelefono.Value, txtNac.Text, txtColegio.Text, genero, txtEdad.Value,
txtProvincia.Text, txtCanton.Text, txtDistrito.Text, numeros[0], txtdir.Text, txtNomP.Text,
txtnomM.Text, txtNomE.Text, numeros[7], numeros[9], numeros[11], numeros[10], numeros[12],
numeros[8], numeros[1], numeros[3], numeros[5], txtOcupaM.Text, txtOcupaP.Text, txtOcupaE.Text,
numeros[2], numeros[4], numeros[6], txtPin.Text);
                                                Cote_Cloud.User.email correo = new Cote_Cloud.User.email();
                                                correo.bodyHtml = true;
                                                correo.mensaje = "El estudiante " + txtNom.Text + " " + txtApe1.Text + " ha sido correctamente pre matrículado. Se adjunta la boleta de inscripción del curso lectivo " + DateTime.Today.Year.ToString();
                                                correo.MailTo = txtEmail.Text;
                                                correo.MailSubject = "Boleta de inscripción " + DateTime.Today.Year.ToString();
                                                correo.AddAttachment(path);
                                                correo.SendMail();
                                                string[] data = admin.getDataProcesoAdmision(DateTime.Today.Year.ToString());
                                                admin.editDataAdmission(DateTime.Today.Year.ToString(), "-1", data[4], data[2], data[3], "1");
                                                llamada.setUnavailablePin(txtPin.Text);
                                                vaciar(this);
                                                lblMensaje.Text = "Usted ha sido pre-matrículado. Sí desea descargar el formulario, presione el botón; además el mismo se envió al correo electrónico del estudiante.";
                                                pnlReport.Visible = true;
                                            }
                                            catch (Exception a)
                                            {
                                                Cote_Cloud.User.email correo = new Cote_Cloud.User.email();
                                                correo.mensaje = "Exception: " + a.ToString();
                                                correo.MailTo = System.Configuration.ConfigurationManager.AppSettings.Get("Destinatario").ToString();
                                                correo.MailSubject = "Excepción en registro de un estudiante";
                                                correo.SendMail();
                                                error.llenar("Sucedió un error al registrar el estudiante. Por favor verifique que los datos y formato de los mismos sean correctos. Sí el error persiste, envíe un correo a admisioncotepecos@gmail.com");
                                            }
                                        }
                                        else
                                        {
                                            error.llenar("El código pin no es válido o ha sido usado");
                                        }
                                    }
                                }
                                else
                                {
                                    error.llenar("La segunda opción se encuentra habilitada, por lo tanto debe seleccionarse obligatoriamente");
                                }
                            }
                        }
                        else
                        {
                            error.llenar("El correo ya está vinculado con otro estudiante");
                        }
                    }
                    else
                    {
                        error.llenar("Otro estudiante con el mismo numero de cedula ha sido prematriculado anteriormente, por favor registre otro estudiante");
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
            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();
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
        protected void chkOpc2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOpc2.Checked)
            {
                cboOp2.Visible = true;
            }
            else
            {
                cboOp2.Visible = false;
            }
        }
        protected void btnFormatear_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"..\RegPreMatricula\PreRegistro.aspx");
            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();
        }
        protected void btnHelp_Click(object sender, EventArgs e)
        {
            tutorial.Visible = true;
        }
        protected void btnReporte_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath("~/datos/");
            path = Path.Combine(path, "Matricula de " + id + ".doc");
            Response.Clear();
            Response.ContentType = "application/msword";
            Response.AddHeader("content-disposition", "attachment; filename= Matricula de estudiante "+id+".doc");
            Response.WriteFile(path);
            Response.End();
        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            pnlReport.Visible = false;
        }
    }
}