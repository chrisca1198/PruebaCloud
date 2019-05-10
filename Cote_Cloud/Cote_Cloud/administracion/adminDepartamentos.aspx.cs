using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cote_Cloud.administracion
{
    public partial class adminDepartamentos : System.Web.UI.Page
    {
        transacciones.administracion llamada = new transacciones.administracion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillSpecialty();
            }
        }
        private void fillSpecialty()
        {
            gvSpecialty.DataSource = llamada.sacarEsp();
            gvSpecialty.DataBind();
        }
        private void fillCboEsp()
        {
            if (llamada.sacarEspeci().Count == 0)
            {
                ArrayList aux = new ArrayList();
                aux.Add("No registradas");
                cboEspecialidad.DataSource = aux;
                cboEspecialidad.DataBind();
                informativo.llenar("No hay ninguna especialidad registrada", "Importante!");
            }
            else
            {
                cboEspecialidad.DataSource = llamada.sacarEspeci();
                cboEspecialidad.DataBind();
                gvSubArea.DataSource = llamada.getSubArea(cboEspecialidad.Text);
                gvSubArea.DataBind();
            }
        }
        protected void btnTecnico_Click(object sender, EventArgs e)
        {
            btnTecnico.CssClass = "nav-link active btn btn-outline";
            btnAcademico.CssClass = "nav-link btn btn-outline";
            btnEspecialidades.CssClass = "nav-link active btn btn-success m-b-10 m-l-5";
            btnSubAreas.CssClass = "nav-link btn btn-outline";
            fillSpecialty();
            tabs.ActiveViewIndex = 0;
            tabsEspecialidades.ActiveViewIndex = 0;
        }
        protected void btnAcademico_Click(object sender, EventArgs e)
        {
            btnTecnico.CssClass = "nav-link btn btn-outline";
            btnAcademico.CssClass = "nav-link active btn btn-outline";
            gvMaterias.DataSource = llamada.getMateria();
            gvMaterias.DataBind();
            tabs.ActiveViewIndex = 1;
        }

        protected void btnEspecialidades_Click(object sender, EventArgs e)
        {
            btnEspecialidades.CssClass = "nav-link active btn btn-success m-b-10 m-l-5";
            btnSubAreas.CssClass = "nav-link btn btn-outline";
            fillSpecialty();
            tabsEspecialidades.ActiveViewIndex = 0;
        }

        protected void btnSubAreas_Click(object sender, EventArgs e)
        {
            btnEspecialidades.CssClass = "nav-link btn btn-outline";
            btnSubAreas.CssClass = "nav-link active btn btn-success m-b-10 m-l-5";
            fillCboEsp();
            tabsEspecialidades.ActiveViewIndex = 1;
        }
        protected void gvSpecialty_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id = gvSpecialty.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = (GridViewRow)gvSpecialty.Rows[e.RowIndex];
            TextBox can = (TextBox)row.Cells[0].Controls[0];
            if (!(can.Text == ""))
            {
                llamada.editEspecialidad(id, can.Text);
                completado.llenar("Se ha modificado la especialidad");
                completado.Visible = true;
                gvSpecialty.EditIndex = -1;
                fillSpecialty();
            }
            else
            {
                error.llenar("El campo de la especialidad debe de estar lleno");
                error.Visible = true;
            }
        }
        protected void gvSpecialty_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSpecialty.PageIndex = e.NewPageIndex;
            fillSpecialty();
        }

        protected void gvSpecialty_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSpecialty.EditIndex = -1;
            fillSpecialty();
        }
        protected void gvSpecialty_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSpecialty.EditIndex = e.NewEditIndex;
            fillSpecialty();
        }
        protected void gvSpecialty_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = gvSpecialty.DataKeys[e.RowIndex].Value.ToString();
            llamada.deleteEsp(id);
            fillSpecialty();
            completado.llenar("Se ha eliminado correctamente la especialidad");
            completado.Visible = true;
        }

        protected void btnAddEsp_Click(object sender, EventArgs e)
        {
            if (txtNomEspe.Text == "")
            {
                error.llenar("El campo de la especialidad debe de estar lleno");
                error.Visible = true;
            }
            else
            {
                if (llamada.existEsp(txtNomEspe.Text))
                {
                    error.llenar("La especialidad ingresada ya existe, por favor ingrese una diferente");
                    error.Visible = true;
                }
                else
                {
                    llamada.insertEspecialidades(txtNomEspe.Text);
                    txtNomEspe.Text = "";
                    completado.llenar("Se ha agregado la nueva especialidad");
                    completado.Visible = true;
                    fillSpecialty();
                }

            }
        }

        protected void cboEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvSubArea.DataSource = llamada.getSubArea(cboEspecialidad.Text);
            gvSubArea.DataBind();
        }

        protected void gvSubArea_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSubArea.PageIndex = e.NewPageIndex;
            gvSubArea.DataSource = llamada.getSubArea(cboEspecialidad.Text);
            gvSubArea.DataBind();
        }

        protected void gvSubArea_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSubArea.EditIndex = -1;
            gvSubArea.DataSource = llamada.getSubArea(cboEspecialidad.Text);
            gvSubArea.DataBind();
        }

        protected void gvSubArea_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = gvSubArea.DataKeys[e.RowIndex].Value.ToString();
            llamada.deleteMateria(id, "tecnica", cboEspecialidad.Text);
            completado.llenar("Se ha eliminado correctamente la subarea");
            completado.Visible = true;
            gvSubArea.DataSource = llamada.getSubArea(cboEspecialidad.Text);
            gvSubArea.DataBind();
        }
        protected void gvSubArea_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id = gvSubArea.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = (GridViewRow)gvSubArea.Rows[e.RowIndex];
            TextBox sub = (TextBox)row.Cells[0].Controls[0];
            if (!(sub.Text == ""))
            {
                llamada.editMateria(sub.Text, id, "tecnica", cboEspecialidad.Text);
                completado.llenar("Se ha modificado la sub área");
                completado.Visible = true;
                gvSubArea.EditIndex = -1;
                gvSubArea.DataSource = llamada.getSubArea(cboEspecialidad.Text);
                gvSubArea.DataBind();
            }
            else
            {
                error.llenar("El campo de la especialidad debe de estar lleno");
                error.Visible = true;
            }
        }

        protected void gvSubArea_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSubArea.EditIndex = e.NewEditIndex;
            gvSubArea.DataSource = llamada.getSubArea(cboEspecialidad.Text);
            gvSubArea.DataBind();
        }

        protected void btnAddSub_Click(object sender, EventArgs e)
        {

            if (txtNomSub.Text == "")
            {
                error.llenar("El campo de la sub área debe de estar lleno");
                error.Visible = true;
            }
            else
            {
                if (!llamada.existMateria(txtNomSub.Text, "tecnica", cboEspecialidad.Text))
                {
                    llamada.insertSubArea(txtNomSub.Text, cboEspecialidad.Text);
                    txtNomSub.Text = "";
                    completado.llenar("Se ha agregado la nueva sub área");
                    completado.Visible = true;
                    gvSubArea.DataSource = llamada.getSubArea(cboEspecialidad.Text);
                    gvSubArea.DataBind();
                }
                else
                {
                    error.llenar("La sub área ingresada ya existe, por favor ingrese una diferente");
                    error.Visible = true;
                    txtNomSub.Text = "";
                }
            }
        }
        protected void btnAddMateria_Click(object sender, EventArgs e)
        {

            if (txtNomMateria.Text == "")
            {
                error.llenar("El campo de la materia debe de estar lleno");
                error.Visible = true;
            }
            else
            {
                if (!llamada.existMateria(txtNomMateria.Text, "academica", ""))
                {
                    llamada.insertMateria(txtNomMateria.Text);
                    txtNomMateria.Text = "";
                    completado.llenar("Se ha agregado la materia");
                    completado.Visible = true;
                    gvMaterias.DataSource = llamada.getMateria();
                    gvMaterias.DataBind();
                }
                else
                {
                    error.llenar("La materia ingresada ya existe, por favor ingrese una diferente");
                    error.Visible = true;
                    txtNomMateria.Text = "";
                }
            }
        }

        protected void gvMaterias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMaterias.PageIndex = e.NewPageIndex;
            gvMaterias.DataSource = llamada.getMateria();
            gvMaterias.DataBind();
        }

        protected void gvMaterias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = gvMaterias.DataKeys[e.RowIndex].Value.ToString();
            llamada.deleteMateria(id, "academica","");
            gvMaterias.DataSource = llamada.getMateria();
            gvMaterias.DataBind();
        }

        protected void gvMaterias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMaterias.EditIndex = e.NewEditIndex;
            gvMaterias.DataSource = llamada.getMateria();
            gvMaterias.DataBind();
        }

        protected void gvMaterias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id = gvMaterias.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = (GridViewRow)gvMaterias.Rows[e.RowIndex];
            TextBox sub = (TextBox)row.Cells[0].Controls[0];
            if (!(sub.Text == ""))
            {
                llamada.editMateria(sub.Text, id, "academica", "");
                completado.llenar("Se ha modificado la materia");
                completado.Visible = true;
                gvMaterias.EditIndex = -1;
                gvMaterias.DataSource = llamada.getMateria();
                gvMaterias.DataBind();
            }
            else
            {
                error.llenar("El campo de la materia debe de estar lleno");
                error.Visible = true;
            }
        }

        protected void gvMaterias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMaterias.EditIndex = -1;
            gvMaterias.DataSource = llamada.getMateria();
            gvMaterias.DataBind();
        }
    }
}