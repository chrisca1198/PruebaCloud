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


namespace Cote_Cloud.administracion
{
    public partial class adminEstudiantes : System.Web.UI.Page
    {
        transacciones.administracion llamada = new transacciones.administracion();
        transacciones.TransMatricula llamada2 = new transacciones.TransMatricula();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private Boolean DatosEstudiantesLlenos()
        {
            if (txtApe1.Text == "" || txtCedula.Value == "" || txtEmail.Text == "" ||
                txtFechaNacimiento.Value == "" || txtNom.Text == ""|| txtTelefono.Value == ""
                || txtNac.Text == "" || txtdir.Text == "" || txtProvincia.Text == "" || txtCanton.Text == "" || txtDistrito.Text == ""
                || txtEmail.Text == ""|| txtEdad.Value == "")
            {
                return false;
            }
            else
                return true;
        }
        private Boolean DatosEncargadosLlenos()
        {
            if (!(txtNomP.Text == "" && txtCedP.Value == "" && txtTelP.Value == "") ||
                !(txtnomM.Text == "" && txtCedM.Value == "" && txtTelM.Value == "") ||
                !(txtNomE.Text == "" && txtCedE.Value == "" && txtTelE.Value == ""))
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
            txtFechaNacimiento.Value = "";
            txtEdad.Value = "";
            txtCedula.Value = "";
            txtCedP.Value = "";
            txtCedM.Value = "";
            txtCedE.Value = "";
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
            if (IsNumeric(txtCedP.Value)) { numeros[7] = txtCedP.Value; }
            else { numeros[7] = "0"; }
            if (IsNumeric(txtTelP.Value)) { numeros[8] = txtTelP.Value; }
            else { numeros[8] = "0"; }
            if (IsNumeric(txtCedM.Value)) { numeros[9] = txtCedM.Value; }
            else { numeros[9] = "0"; }
            if (IsNumeric(txtTelM.Value)) { numeros[10] = txtTelM.Value; }
            else { numeros[10] = "0"; }
            if (IsNumeric(txtCedula.Value)) { numeros[11] = txtCedula.Value; }
            else { numeros[11] = "0"; }
            if (IsNumeric(txtTelE.Value)) { numeros[12] = txtTelE.Value; }
            else { numeros[12] = "0"; }
            return numeros;
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
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (DatosEstudiantesLlenos())
            {
                if (DatosEncargadosLlenos())
                {
                    if (!llamada.ExistStudent(txtCedula.Value))
                    {
                        if (!llamada.ExistEmail(txtEmail.Text))
                        {
                            if (!rbFeme.Checked && !rbMascul.Checked)
                            {
                                importante.llenar("Género no seleccionado", "Debe seleccionar el género del estudiante");
                            }
                            else
                            {
                                string genero = "";
                                if (rbFeme.Checked) { genero = "Femenino"; }
                                if (rbMascul.Checked) { genero = "Masculino"; }
                                string[] numeros = nums();
                                llamada.AddStudent(txtCedula.Value, txtNom.Text, txtApe1.Text, txtEmail.Text,
                      txtFechaNacimiento.Value, txtTelefono.Value, txtNac.Text, genero, txtEdad.Value,
                      txtProvincia.Text, txtCanton.Text, txtDistrito.Text, numeros[0], txtdir.Text, txtNomP.Text,
                      txtnomM.Text, txtNomE.Text, numeros[7], numeros[9], numeros[11], numeros[10], numeros[12],
                      numeros[8], numeros[1], numeros[3], numeros[5], numeros[2], numeros[4], numeros[6]);
                                string user = txtnomM.Text.Substring(0, 2) + "" + txtApe1.Text.Substring(0, 2) + "" + txtCedula.Value;
                                string contra = llamada2.contra();
                                Cote_Cloud.User.email correo = new Cote_Cloud.User.email();
                                correo.bodyHtml = true;
                                correo.informativo = false;
                                correo.mensaje = "El estudiante " + txtNom.Text + " " + txtApe1.Text + " ha sido ingresado correctamente. Se agregan los detalles de su usuario y contraseña de la plataforma web. Ingresar y cambiar la contraseña debido a que es generada automáticamente";
                                correo.contra = contra;
                                correo.usuario = user;
                                correo.MailTo = txtEmail.Text;
                                correo.MailSubject = "Registro correcto";
                                correo.SendMail();
                                llamada2.ingresarUsuarios(txtCedula.Value, user, contra, "estudiante", txtEmail.Text);
                            }
                        }
                        else
                        {
                            error.llenar("El correo ya está vinculado con algún usuario");
                            error.Visible = true;
                        }
                    }
                    else
                    {
                        error.llenar("Otro estudiante en el sistema tiene el mismo número de cédula, por favor digite otro número de cédula");
                        error.Visible = true;
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

        protected void btnAddStu_Click(object sender, EventArgs e)
        {

        }
        protected void btnEditStud_Click(object sender, EventArgs e)
        {

        }
    }
}