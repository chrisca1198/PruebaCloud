using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cote_Cloud.administracion
{
    public partial class adminEditStudents : System.Web.UI.Page
    {
        transacciones.TransPreMatricula llamada = new transacciones.TransPreMatricula();
        transacciones.TransMatricula llamada2 = new transacciones.TransMatricula();
        transacciones.administracion admin = new transacciones.administracion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvStudents.DataSource = llamada.getAllDataPrematricula();
                gvStudents.DataBind();
            }
        }
        private void searchGrids(GridView grid, DataTable table, string text)
        {
            if (text == "")
            {
                grid.DataSource = table;
                grid.DataBind();
            }
            else
            {
                try
                {
                    DataTable salida = new DataTable();
                    bool contain = false;
                    foreach (DataColumn colum in table.Columns)
                    {
                        salida.Columns.Add(colum.ColumnName);
                    }
                    foreach (DataRow row in table.Rows)
                    {
                        foreach (DataColumn colum in table.Columns)
                        {
                            if (row[colum].ToString().Contains(text))
                            {
                                contain = true;
                                break;
                            }
                        }
                        if (contain)
                        {
                            DataRow newRow = salida.NewRow();
                            foreach (DataColumn colum in salida.Columns)
                            {
                                newRow[colum.ColumnName] = row[colum.ColumnName];
                            }
                            salida.Rows.Add(newRow);
                            contain = false;
                        }
                    }
                    grid.DataSource = salida;
                    grid.DataBind();
                }
                catch (Exception a)
                {
                    string au = a.ToString();
                }
            }
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
            if (txtEmail.Text == "" || txtTelefono.Value == "" || txtdir.Text == "" || txtProvincia.Text == "" || txtCanton.Text == "" || txtDistrito.Text == "" || txtEmail.Text == "")
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
        }
        private void llenarDatos(string id)
        {
            DataRow dataStudent = llamada.GetStudent(id).Rows[0];
            txtCanton.Text = dataStudent["canton"].ToString();
            txtCedula.Value = numbersInserted(dataStudent["cedula"].ToString());
            txtCedulaE.Value = numbersInserted(dataStudent["cedEncargado"].ToString());
            txtCedulaM.Value = numbersInserted(dataStudent["cedMadre"].ToString());
            txtCedulaP.Value = numbersInserted(dataStudent["cedPadre"].ToString());
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
            txtNom.Text = dataStudent["nombre"].ToString();
            txtApellidos.Text = dataStudent["apellidos"].ToString();
            txtNomE.Text = dataStudent["nomEncargado"].ToString();
            txtnomM.Text = dataStudent["nomMadre"].ToString();
            txtNomP.Text = dataStudent["nomPadre"].ToString();
            txtOcupaE.Text = dataStudent["ocupacionEncargado"].ToString();
            txtOcupaM.Text = dataStudent["ocupacionMadre"].ToString();
            txtOcupaP.Text = dataStudent["ocupacionPadre"].ToString();
            txtProvincia.Text = dataStudent["provincia"].ToString();
            pnledicion.Visible = true;
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (DatosEstudiantesLlenos())
            {
                if (DatosEncargadosLlenos())
                {
                    if (!llamada.existStudentEdit(txtCedula.Value))
                    {
                        if (!llamada2.existEmail(txtEmail.Text, txtCedula.Value))
                        {
                            try
                            {
                                llamada.editDataPreMatriculado(txtCedula.Value, txtEmail.Text, txtNom.Text, txtApellidos.Text, txtTelefono.Value, txtEdad.Text, txtProvincia.Text, txtCanton.Text, txtDistrito.Text, txtTelResi.Value, txtdir.Text,
                                    txtNomP.Text, txtnomM.Text, txtNomE.Text, txtCedulaP.Value, txtCedulaM.Value, txtCedulaE.Value, txtTelP.Value,
                                    txtTelM.Value, txtTelE.Value, txtIngresoE.Value, txtIngresoM.Value, txtIngresoP.Value, txtOcupaM.Text, txtOcupaP.Text, txtOcupaE.Text, txtTelTrabajoP.Value,
                                    txtTelTrabajoM.Value, txtTelTrabajoE.Value);
                                vaciar(this);
                                completado.llenar("El estudiante fue correctamente editado.");
                                pnledicion.Visible = false;
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
        }
        protected void btnFormatear_Click(object sender, EventArgs e)
        {
            vaciar(pnledicion);
            pnledicion.Visible = false;
            searchGrids(gvStudents, llamada.getAllDataPrematricula(), txtNomEstudi.Text);
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
        protected void gvStudents_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStudents.PageIndex = e.NewPageIndex;
            searchGrids(gvStudents, llamada.getAllDataPrematricula(), txtNomEstudi.Text);
        }
        private void llenarDatos()
        {

        }
        protected void gvStudents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "editar")
            {
                GridViewRow row = gvStudents.Rows[index];
                llenarDatos(row.Cells[0].Text);
                Session["IdEditStudent"] = row.Cells[0].Text;
            }
        }
        protected void txtNomEstudi_TextChanged(object sender, EventArgs e)
        {
            searchGrids(gvStudents, llamada.getAllDataPrematricula(), txtNomEstudi.Text);
        }
    }
}