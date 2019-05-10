using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cote_Cloud.administracion
{
    public partial class adminProfesores : System.Web.UI.Page
    {
        transacciones.administracion llamada = new transacciones.administracion();
        transacciones.clLogIn logIn = new transacciones.clLogIn();
        static string opc1 = "", opc2 = "", opc3 = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void vaciar(Control control)
        {
            foreach (Control a in control.Controls)
            {
                if (a.GetType() == typeof(TextBox))
                {
                    ((TextBox)(a)).Text = "";
                }
                if (a.GetType() == typeof(CheckBox))
                {
                    ((CheckBox)(a)).Checked = false;
                }
                if (a.GetType() == typeof(RadioButton))
                {
                    ((RadioButton)(a)).Checked = false;
                }
            }
            pnlSubareas.Visible = false;
            pnlTecnico.Visible = false;
            cboMateria.Visible = false;
        }
        private Boolean Vacio(Control control)
        {
            bool empty = false;
            foreach (Control a in control.Controls)
            {
                if (a.GetType() == typeof(TextBox))
                {
                    if (((TextBox)(a)).Text == "")
                    {
                        empty = true;
                    }
                }
            }
            return empty;
        }
        private void fillCbo(DropDownList cbo, ArrayList list, string instruc, string op1, string op2)
        {
            ArrayList lis = new ArrayList();
            lis.Add(instruc);
            for (int a = 0; a < list.Count; a++)
            {
                lis.Add(list[a]);
            }
            if (lis.Contains(op1))
            {
                lis.Remove(op1);
            }
            if (lis.Contains(op2))
            {
                lis.Remove(op2);
            }
            cbo.DataSource = lis;
            cbo.DataBind();
        }
        protected void txtCed_TextChanged(object sender, EventArgs e)
        {
            string txt = txtCed.Text;
            if (txt.Length > 15)
            {
                txtCed.Text = "";
                error.llenar("La cédula debe de tener una cantidad máxima de 15 digitos");
                error.Visible = true;
            }
        }
        protected void txtTel_TextChanged(object sender, EventArgs e)
        {
            string txt = txtTel.Text;
            if (txt.Length > 8)
            {
                txtTel.Text = "";
                error.llenar("El telefono del profesor debe de tener una cantidad máxima de 8 digitos");
                error.Visible = true;
            }
        }
        protected void btnFormatear_Click(object sender, EventArgs e)
        {
            vaciar(pnlAgregar);
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Vacio(pnlAgregar))
            {
                error.llenar("Datos del profesor no completos");
                error.Visible = true;
            }
            else
            {
                if (!(rbAcademico.Checked) && !(rbTecnico.Checked))
                {
                    error.llenar("El profesor debe de estar en algún departamento, técnico o académico");
                    error.Visible = true;
                }
                else
                {
                    if (rbTecnico.Checked)
                    {
                        if (cboEspecialidad.Text == "Seleccione la especialidad del profesor")
                        {
                            error.llenar("Debe seleccionar la especialidad del profesor");
                            error.Visible = true;
                        }
                        else
                        {
                            if (cboSubAreas1.Text == "Seleccione la sub área 1 del profesor")
                            {
                                error.llenar("Debe seleccionar la sub área 1 del profesor obligatoriamente");
                                error.Visible = true;
                            }
                            else
                            {
                                bool select = true;
                                string mensaje = "";
                                string opcion2 = "";
                                string opcion3 = "";
                                if (chkSub2.Checked && cboSubAreas2.Text == "Seleccione la sub área 2 del profesor")
                                {
                                    if (chkSub3.Checked && cboSubAreas3.Text == "Seleccione la sub área 3 del profesor")
                                    {
                                        mensaje = "Se habilitó la segunda y tercera opción, por favor seleccione la sub área respectiva";
                                    }
                                    else
                                    {
                                        mensaje = "Se habilitó la segunda opción, por favor seleccione la sub área respectiva";
                                    }
                                    select = false;
                                }
                                else if (chkSub2.Checked && cboSubAreas2.Text != "Seleccione la sub área 2 del profesor")
                                {
                                    opcion2 = cboSubAreas2.Text;
                                }
                                if (chkSub3.Checked && cboSubAreas3.Text == "Seleccione la sub área 3 del profesor")
                                {
                                    if (chkSub2.Checked && cboSubAreas2.Text == "Seleccione la sub área 2 del profesor")
                                    {
                                        mensaje = "Se habilitó la segunda y tercera opción, por favor seleccione la sub área respectiva";
                                    }
                                    else
                                    {
                                        mensaje = "Se habilitó la tercera opción, por favor seleccione la sub área respectiva";
                                    }
                                    select = false;
                                }
                                else if (chkSub3.Checked && cboSubAreas3.Text != "Seleccione la sub área 3 del profesor")
                                {
                                    opcion3 = cboSubAreas3.Text;
                                }
                                if (select)
                                {
                                    if (llamada.existProfe(txtCed.Text))
                                    {
                                        error.llenar("Otro profesor con esa número de cédula ya está registrado");
                                        error.Visible = true;
                                    }
                                    else
                                    {
                                        llamada.ingresarProfe(txtCed.Text, txtNom.Text, txtApe1.Text, txtCorreo.Text, txtTel.Text, cboSubAreas1.Text, opcion2, opcion3, "tecnica", cboEspecialidad.Text);
                                        string contra = logIn.contra();
                                        logIn.ingresarUsuarios(txtCed.Text, txtNom.Text + "" + txtCed.Text, contra, "profesor", txtCorreo.Text);
                                        Cote_Cloud.User.email correo = new Cote_Cloud.User.email();
                                        correo.bodyHtml = true;
                                        correo.informativo = false;
                                        correo.mensaje = "Usted ha sido registrado como profesor en el Colegio Técnico Profesional en Educación Comercial y de Servicios. Se agregan los detalles de su usuario y contraseña de la plataforma web. Ingresar y cambiar la contraseña debido a que es generada automáticamente";
                                        correo.contra = contra;
                                        correo.usuario = txtNom.Text + "" + txtCed.Text;
                                        correo.MailTo = txtCorreo.Text;
                                        correo.MailSubject = "Registro correcto";
                                        correo.SendMail();
                                        completado.llenar("Se ha ingresado correctamente el profesor y se le envió un correo de confirmación");
                                        vaciar(pnlAgregar);
                                    }
                                }
                                else
                                {
                                    error.llenar(mensaje);
                                }
                            }
                        }
                    }
                    else if (rbAcademico.Checked)
                    {
                        if (cboMateria.Text == "Seleccione la materia")
                        {
                            error.llenar("Debe seleccionar una materia para el profesor");
                            error.Visible = true;
                        }
                        else
                        {
                            if (llamada.existProfe(txtCed.Text))
                            {
                                error.llenar("Otro profesor con esa número de cédula ya está registrado");
                                error.Visible = true;
                            }
                            else
                            {
                                llamada.ingresarProfe(txtCed.Text, txtNom.Text, txtApe1.Text, txtCorreo.Text, txtTel.Text, cboMateria.Text, "", "", "academica", "");
                                completado.llenar("Se ha ingresado correctamente el profesor");
                                vaciar(pnlAgregar);
                            }
                        }
                    }
                }
            }
        }

        protected void rbAcademico_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAcademico.Checked)
            {
                fillCbo(cboMateria, llamada.getListMaterias(), "Seleccione la materia", "", "");
                rbTecnico.Checked = false;
                cboMateria.Visible = true;
                pnlTecnico.Visible = false;
            }
        }

        protected void rbTecnico_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTecnico.Checked)
            {
                fillCbo(cboEspecialidad, llamada.sacarEspeci(), "Seleccione la especialidad del profesor", "", "");
                fillCbo(cboSubAreas1, llamada.getListSubAreas(cboEspecialidad.Text), "Seleccione la sub área 1 del profesor", "", "");
                fillCbo(cboSubAreas2, llamada.getListSubAreas(cboEspecialidad.Text), "Seleccione la sub área 2 del profesor", "", "");
                fillCbo(cboSubAreas3, llamada.getListSubAreas(cboEspecialidad.Text), "Seleccione la sub área 3 del profesor", "", "");
                rbAcademico.Checked = false;
                cboMateria.Visible = false;
                pnlTecnico.Visible = true;
            }
        }

        protected void cboEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEspecialidad.Text == "Seleccione la especialidad del profesor") { pnlSubareas.Visible = false; }
            else
            {
                fillCbo(cboSubAreas1, llamada.getListSubAreas(cboEspecialidad.Text), "Seleccione la sub área 1 del profesor", "", "");
                fillCbo(cboSubAreas2, llamada.getListSubAreas(cboEspecialidad.Text), "Seleccione la sub área 2 del profesor", "", "");
                fillCbo(cboSubAreas3, llamada.getListSubAreas(cboEspecialidad.Text), "Seleccione la sub área 3 del profesor", "", "");
                pnlSubareas.Visible = true;
            }
        }

        protected void chkSub2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSub2.Checked)
            {
                cboSubAreas2.Visible = true;
            }
            else
            {
                cboSubAreas2.Visible = false;
            }
        }

        protected void chkSub3_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSub3.Checked)
            {
                cboSubAreas3.Visible = true;
            }
            else
            {
                cboSubAreas3.Visible = false;
            }
        }

        protected void cboSubAreas1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem item1 = cboSubAreas2.SelectedItem;
            ListItem item2 = cboSubAreas3.SelectedItem;
            fillCbo(cboSubAreas2, llamada.getListSubAreas(cboEspecialidad.Text), "Seleccione la sub área 2 del profesor", cboSubAreas3.Text, cboSubAreas1.Text);
            int index1 = cboSubAreas2.Items.IndexOf(item1);
            cboSubAreas2.SelectedIndex = index1;
            fillCbo(cboSubAreas3, llamada.getListSubAreas(cboEspecialidad.Text), "Seleccione la sub área 3 del profesor", cboSubAreas1.Text, cboSubAreas2.Text);
            int index2 = cboSubAreas3.Items.IndexOf(item2);
            cboSubAreas3.SelectedIndex = index2;
        }

        protected void cboSubAreas2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem item1 = cboSubAreas1.SelectedItem;
            ListItem item2 = cboSubAreas3.SelectedItem;
            fillCbo(cboSubAreas1, llamada.getListSubAreas(cboEspecialidad.Text), "Seleccione la sub área 1 del profesor", cboSubAreas3.Text, cboSubAreas2.Text);
            int index1 = cboSubAreas1.Items.IndexOf(item1);
            cboSubAreas1.SelectedIndex = index1;
            fillCbo(cboSubAreas3, llamada.getListSubAreas(cboEspecialidad.Text), "Seleccione la sub área 3 del profesor", cboSubAreas1.Text, cboSubAreas2.Text);
            int index2 = cboSubAreas3.Items.IndexOf(item2);
            cboSubAreas3.SelectedIndex = index2;
        }

        protected void cboSubAreas3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem item1 = cboSubAreas1.SelectedItem;
            ListItem item2 = cboSubAreas2.SelectedItem;
            fillCbo(cboSubAreas1, llamada.getListSubAreas(cboEspecialidad.Text), "Seleccione la sub área 1 del profesor", cboSubAreas3.Text, cboSubAreas2.Text);
            int index1 = cboSubAreas1.Items.IndexOf(item1);
            cboSubAreas1.SelectedIndex = index1;
            fillCbo(cboSubAreas2, llamada.getListSubAreas(cboEspecialidad.Text), "Seleccione la sub área 2 del profesor", cboSubAreas3.Text, cboSubAreas1.Text);
            int index2 = cboSubAreas2.Items.IndexOf(item2);
            cboSubAreas2.SelectedIndex = index2;
        }
        protected void btnAddProfe_Click(object sender, EventArgs e)
        {
            btnAddProfe.CssClass = "nav-link active btn btn-outline";
            btnEditProfe.CssClass = "nav-link btn btn-outline";
            vaciar(pnlAgregar);
            tabs.ActiveViewIndex = 0;
        }

        protected void btnEditProfe_Click(object sender, EventArgs e)
        {
            btnEditProfe.CssClass = "nav-link active btn btn-outline";
            btnAddProfe.CssClass = "nav-link btn btn-outline";
            gvEditAcademico.DataSource = llamada.getProfesAcademicos();
            gvEditAcademico.DataBind();
            tabs.ActiveViewIndex = 1;
            tabEditProfe.ActiveViewIndex = 0;
            btnEditTecnicas.CssClass = "nav-link active btn btn-success m-b-10 m-l-5";
            btnEditAcademicos.CssClass = "nav-link btn btn-outline";
            fillCbo(cboEditEspecialidad, llamada.sacarEspeci(), "Seleccione la especialidad", "", "");
            fillGridProfesTecnicos(cboEditEspecialidad.Text);
        }
        protected void btnEditAcademicos_Click(object sender, EventArgs e)
        {
            btnEditAcademicos.CssClass = "nav-link active btn btn-success m-b-10 m-l-5";
            btnEditTecnicas.CssClass = "nav-link btn btn-outline";
            gvEditAcademico.DataSource = llamada.getProfesAcademicos();
            gvEditAcademico.DataBind();
            tabEditProfe.ActiveViewIndex = 1;
        }

        protected void btnEditTecnicas_Click(object sender, EventArgs e)
        {
            btnEditTecnicas.CssClass = "nav-link active btn btn-success m-b-10 m-l-5";
            btnEditAcademicos.CssClass = "nav-link btn btn-outline";
            tabEditProfe.ActiveViewIndex = 0;
            fillCbo(cboEditEspecialidad, llamada.sacarEspeci(), "Seleccione la especialidad", "", "");
            fillGridProfesTecnicos(cboEditEspecialidad.Text);
        }
        private void fillGridProfesTecnicos(string especialidad)
        {
            DataTable table = new DataTable();
            foreach (DataColumn colum in llamada.getProfesTecnicos(especialidad).Columns)
            {
                table.Columns.Add(colum.ColumnName.ToString());
            }
            foreach (DataRow row in llamada.getProfesTecnicos(especialidad).Rows)
            {
                DataRow rowAux = table.NewRow();
                foreach (DataColumn colum in llamada.getProfesTecnicos(especialidad).Columns)
                {
                    if (colum.ColumnName == "materia2")
                    {
                        if (row["materia2"].ToString() == "")
                            rowAux["materia2"] = "No especificada";
                        else
                            rowAux[colum.ColumnName] = row[colum.ColumnName].ToString();
                    }
                    else if (colum.ColumnName == "materia3")
                    {
                        if (row["materia3"].ToString() == "")
                            rowAux["materia3"] = "No especificada";
                        else
                            rowAux[colum.ColumnName] = row[colum.ColumnName].ToString();
                    }
                    else
                    {
                        rowAux[colum.ColumnName] = row[colum.ColumnName].ToString();
                    }
                }
                table.Rows.Add(rowAux);
            }
            gvEditTecnico.DataSource = table;
            gvEditTecnico.DataBind();
        }
        protected void gvEditAcademico_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEditAcademico.PageIndex = e.NewPageIndex;
            gvEditAcademico.DataSource = llamada.getProfesAcademicos();
            gvEditAcademico.DataBind();
        }

        protected void gvEditAcademico_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEditAcademico.EditIndex = -1;
            gvEditAcademico.Columns[4].Visible = true;
            gvEditAcademico.Columns[5].Visible = false;
            gvEditAcademico.DataSource = llamada.getProfesAcademicos();
            gvEditAcademico.DataBind();
        }

        protected void gvEditAcademico_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = gvEditAcademico.Rows[e.NewEditIndex];
            gvEditAcademico.EditIndex = e.NewEditIndex;
            gvEditAcademico.DataSource = llamada.getProfesAcademicos();
            gvEditAcademico.DataBind();
            gvEditAcademico.Columns[4].Visible = false;
            gvEditAcademico.Columns[5].Visible = true;
        }

        protected void gvEditAcademico_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            llamada.deleteProfe(gvEditAcademico.DataKeys[e.RowIndex].Value.ToString());
            completado.llenar("Se ha eliminado correctamente el profesor");
            gvEditAcademico.DataSource = llamada.getProfesAcademicos();
            gvEditAcademico.DataBind();
        }

        protected void gvEditAcademico_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id = gvEditAcademico.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = (GridViewRow)gvEditAcademico.Rows[e.RowIndex];
            TextBox nombre = (TextBox)row.Cells[1].Controls[0];
            TextBox apellidos = (TextBox)row.Cells[2].Controls[0];
            TextBox telefono = (TextBox)row.Cells[3].Controls[0];
            DropDownList cbo = (DropDownList)row.FindControl("cboMateriaProfe");
            if (nombre.Text == "" || apellidos.Text == "" || telefono.Text == "")
            {
                error.llenar("Los datos del profesor deben de estar completamente llenos");
            }
            else
            {
                string tel = telefono.Text;
                if (tel.Length > 8)
                {
                    error.llenar("El telefono del profesor debe de tener una cantidad máxima de 8 digitos");
                }
                else
                {
                    if (cbo.SelectedIndex == 0)
                    {
                        error.llenar("Debe seleccionar la nueva materia del profesor");
                    }
                    else
                    {
                        llamada.editProfe(id,nombre.Text,apellidos.Text,tel,cbo.Text,"","","academica","");
                        gvEditAcademico.EditIndex = -1;
                        gvEditAcademico.Columns[4].Visible = true;
                        gvEditAcademico.Columns[5].Visible = false;
                        completado.llenar("Se ha modificado correctamente el profesor");
                        gvEditAcademico.DataSource = llamada.getProfesAcademicos();
                        gvEditAcademico.DataBind();
                    }
                }
            }
        }
        protected void gvEditAcademico_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList DropDownList1 = (e.Row.FindControl("cboMateriaProfe") as DropDownList);
                DropDownList1.DataSource = llamada.getListMaterias();
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("Seleccione la nueva materia", "0"));
            }
        }
        protected void cboEditEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboEditEspecialidad.Text== "Seleccione la especialidad")
            {
                gvEditTecnico.DataSource = null;
                gvEditTecnico.DataBind();
            }
            else
            {
                fillGridProfesTecnicos(cboEditEspecialidad.Text);
            }
        }
        protected void gvEditTecnico_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEditTecnico.PageIndex = e.NewPageIndex;
            fillGridProfesTecnicos(cboEditEspecialidad.Text);
        }

        protected void gvEditTecnico_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEditTecnico.EditIndex = -1;
            gvEditTecnico.Columns[4].Visible = true;
            gvEditTecnico.Columns[5].Visible = true;
            gvEditTecnico.Columns[6].Visible = true;
            gvEditTecnico.Columns[7].Visible = false;
            gvEditTecnico.Columns[8].Visible = false;
            gvEditTecnico.Columns[9].Visible = false;
            fillGridProfesTecnicos(cboEditEspecialidad.Text);
        }

        protected void gvEditTecnico_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = gvEditTecnico.Rows[e.NewEditIndex];
            gvEditTecnico.EditIndex = e.NewEditIndex;
            gvEditTecnico.Columns[4].Visible = false;
            gvEditTecnico.Columns[5].Visible = false;
            gvEditTecnico.Columns[6].Visible = false;
            gvEditTecnico.Columns[7].Visible = true;
            gvEditTecnico.Columns[8].Visible = true;
            gvEditTecnico.Columns[9].Visible = true;
            fillGridProfesTecnicos(cboEditEspecialidad.Text);
        }

        protected void gvEditTecnico_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            llamada.deleteProfe(gvEditTecnico.DataKeys[e.RowIndex].Value.ToString());
            completado.llenar("Se ha eliminado correctamente el profesor");
            fillGridProfesTecnicos(cboEditEspecialidad.Text);
        }

        protected void gvEditTecnico_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id = gvEditTecnico.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = (GridViewRow)gvEditTecnico.Rows[e.RowIndex];
            TextBox nombre = (TextBox)row.Cells[1].Controls[0];
            TextBox apellidos = (TextBox)row.Cells[2].Controls[0];
            TextBox telefono = (TextBox)row.Cells[3].Controls[0];
            DropDownList cbo = (DropDownList)row.FindControl("cboSubArea1");
            DropDownList cbo2 = (DropDownList)row.FindControl("cboSubArea2");
            DropDownList cbo3 = (DropDownList)row.FindControl("cboSubArea3");
            if (nombre.Text == "" || apellidos.Text == "" || telefono.Text == "")
            {
                error.llenar("Los datos del profesor deben de estar completamente llenos");
            }
            else
            {
                string tel = telefono.Text;
                if (tel.Length > 8)
                {
                    error.llenar("El telefono del profesor debe de tener una cantidad máxima de 8 digitos");
                }
                else
                {
                    if (cbo.SelectedIndex == 0)
                    {
                        error.llenar("Debe seleccionar la primer sub área obligatoriamente");
                    }
                    else
                    {
                        if (cbo.Text == cbo2.Text || cbo.Text == cbo3.Text || cbo2.Text == cbo3.Text)
                        {
                            error.llenar("Las sub áreas deben ser distintas");
                        }
                        else
                        {
                            string opc2, opc3;
                            if (cbo2.SelectedIndex == 0) { opc2 = ""; } else { opc2 = cbo2.Text; }
                            if (cbo3.SelectedIndex == 0) { opc3 = ""; } else { opc3 = cbo3.Text; }
                            llamada.editProfe(id, nombre.Text, apellidos.Text, tel, cbo.Text, opc2, opc3, "tecnica", cboEditEspecialidad.Text);
                            gvEditTecnico.EditIndex = -1;
                            gvEditTecnico.Columns[4].Visible = true;
                            gvEditTecnico.Columns[5].Visible = true;
                            gvEditTecnico.Columns[6].Visible = true;
                            gvEditTecnico.Columns[7].Visible = false;
                            gvEditTecnico.Columns[8].Visible = false;
                            gvEditTecnico.Columns[9].Visible = false;
                            completado.llenar("Se ha modificado correctamente el profesor");
                            fillGridProfesTecnicos(cboEditEspecialidad.Text);
                        }
                    }
                }
            }
        }
        protected void gvEditTecnico_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList DropDownList1 = (e.Row.FindControl("cboSubArea1") as DropDownList);
                DropDownList1.DataSource = llamada.getListSubAreas(cboEditEspecialidad.Text);
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("Seleccione la nueva sub área 1", "Seleccione la nueva sub área 1"));
                DropDownList DropDownList2 = (e.Row.FindControl("cboSubArea2") as DropDownList);
                DropDownList2.DataSource = llamada.getListSubAreas(cboEditEspecialidad.Text);
                DropDownList2.DataBind();
                DropDownList2.Items.Insert(0, new ListItem("Seleccione la nueva sub área 2", "Seleccione la nueva sub área 2"));
                DropDownList DropDownList3 = (e.Row.FindControl("cboSubArea3") as DropDownList);
                DropDownList3.DataSource = llamada.getListSubAreas(cboEditEspecialidad.Text);
                DropDownList3.DataBind();
                DropDownList3.Items.Insert(0, new ListItem("Seleccione la nueva sub área 3", "Seleccione la nueva sub área 3"));
            }
        }
    }
}