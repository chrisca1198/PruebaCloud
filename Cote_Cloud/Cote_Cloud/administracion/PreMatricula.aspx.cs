using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Globalization;

namespace Cote_Cloud.administracion
{
    public partial class PreMatricula : System.Web.UI.Page
    {
        transacciones.TransPreMatricula llamada = new transacciones.TransPreMatricula();
        transacciones.TransMatricula llamada2 = new transacciones.TransMatricula();
        transacciones.generarPines data1 = new transacciones.generarPines();
        report reporte = new report();
        static ArrayList cedulas = new ArrayList();
        static DataTable estudiantes, admitidos;
        transacciones.administracion admin = new transacciones.administracion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string[] data = admin.getDataProcesoAdmision(DateTime.Today.Year.ToString());
                string fecha = data[2];
                if (fecha == "")
                {
                    informativo.llenar("El proceso de admisión no está activo por el momento.", "Proceso inactivo...");
                    pnlIniciarNuevoProceso.Visible = true;
                    pnlPreMatricula.Enabled = false;
                    btnEditFechasAdmission.Enabled = false;
                }
                else
                {
                    txtStartProcess.Text = Convert.ToDateTime(data[2]).ToString("yyyy-MM-dd");
                    txtFinishProcess.Text = Convert.ToDateTime(data[3]).ToString("yyyy-MM-dd");
                    txtPriceSobre.Text = data[4];
                }
            }
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
        private void vaciar(Control con)
        {
            foreach (Control a in con.Controls)
            {
                if (a.GetType() == typeof(TextBox))
                {
                    ((TextBox)(a)).Text = "";
                }
                if (a.GetType() == typeof(DropDownList))
                {
                    ((DropDownList)(a)).SelectedIndex = 0;
                }
                if (a.GetType() == typeof(CheckBox))
                {
                    ((CheckBox)(a)).Checked = false;
                }
            }
        }
        public bool Vacio(Control con)
        {
            bool empty = false;
            foreach (Control a in con.Controls)
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
        private void vaciarNotas()
        {
            txtSetAusIn.Value = "";
            txtSetCien.Value = "";
            txtSetCiv.Value = "";
            txtSetCond.Value = "";
            txtSetEspa.Value = "";
            txtSetEst.Value = "";
            txtSetIng.Value = "";
            txtSetMate.Value = "";

            txtOctAusIn.Value = "";
            txtOctCien.Value = "";
            txtOctCiv.Value = "";
            txtOctCond.Value = "";
            txtOctEspa.Value = "";
            txtOctEst.Value = "";
            txtOctIng.Value = "";
            txtOctMate.Value = "";

            txtNovAusIn.Value = "";
            txtNovCien.Value = "";
            txtNovCiv.Value = "";
            txtNovCond.Value = "";
            txtNovEspa.Value = "";
            txtNovEst.Value = "";
            txtNovIng.Value = "";
            txtNovMate.Value = "";
        }
        private bool NotasVacias()
        {
            if (txtSetAusIn.Value == "" || txtSetCien.Value == "" || txtSetCiv.Value == "" || txtSetCond.Value == "" || txtSetEspa.Value == "" ||
            txtSetEst.Value == "" || txtSetIng.Value == "" || txtSetMate.Value == "" || txtOctAusIn.Value == "" || txtOctCien.Value == "" ||
            txtOctCiv.Value == "" || txtOctCond.Value == "" || txtOctEspa.Value == "" || txtOctEst.Value == "" || txtOctIng.Value == "" ||
            txtOctMate.Value == "" || txtNovAusIn.Value == "" || txtNovCien.Value == "" || txtNovCiv.Value == "" || txtNovCond.Value == "" ||
            txtNovEspa.Value == "" || txtNovEst.Value == "" || txtNovIng.Value == "" || txtNovMate.Value == "")
            {
                return true;
            }
            else { return false; }
        }
        public decimal promedio(DataTable table)
        {
            decimal promedio = 0, promedioSetimo = 0, promedioNoveno = 0, promedioOctavo = 0;
            if (table.Rows.Count == 0) { }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn colum in table.Columns)
                    {
                        if (colum.ColumnName == "ConductaOct" || colum.ColumnName == "ConductaNov" || colum.ColumnName == "ConductaSet" || colum.ColumnName == "AusInjustSet" || colum.ColumnName == "AusInjustOct" || colum.ColumnName == "AusInjustNov" || colum.ColumnName == "cedula")
                        {

                        }
                        else
                        {
                            decimal aux = Convert.ToDecimal(row[colum.ColumnName].ToString());
                            if (colum.ColumnName == "EspaSet" || colum.ColumnName == "InglesSet" || colum.ColumnName == "MateSet"
                                || colum.ColumnName == "EstudiosSet" || colum.ColumnName == "CivicaSet" || colum.ColumnName == "CienciasSet")
                            {
                                promedioSetimo += aux;
                            }
                            if (colum.ColumnName == "EspaOct" || colum.ColumnName == "InglesOct" || colum.ColumnName == "MateOct"
                               || colum.ColumnName == "EstudiosOct" || colum.ColumnName == "CivicaOct" || colum.ColumnName == "CienciasOct")
                            {
                                promedioOctavo += aux;
                            }
                            if (colum.ColumnName == "EspaNov" || colum.ColumnName == "InglesNov" || colum.ColumnName == "MateNov"
                               || colum.ColumnName == "EstudiosNov" || colum.ColumnName == "CivicaNov" || colum.ColumnName == "CienciasNov")
                            {
                                promedioNoveno += aux;
                            }
                        }
                    }
                }
            }
            promedioSetimo = promedioSetimo / 6;
            promedioOctavo = promedioOctavo / 6;
            promedioNoveno = promedioNoveno / 6;
            promedio = (promedioSetimo + promedioOctavo + promedioNoveno) / 3;
            return promedio;
        }
        public int ausencias(DataTable table)
        {
            int suma = 0;
            if (table.Rows.Count == 0) { }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn colum in table.Columns)
                    {
                        if (colum.ColumnName == "AusInjustSet" || colum.ColumnName == "AusInjustOct" || colum.ColumnName == "AusInjustNov")
                        {
                            int aux = Convert.ToInt32(row[colum.ColumnName].ToString());
                            suma += aux;
                        }
                    }
                }
            }
            return suma;
        }
        public decimal promedioConducta(DataTable table)
        {
            decimal promedio = 0, suma = 0;
            if (table.Rows.Count == 0) { }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn colum in table.Columns)
                    {
                        if (colum.ColumnName == "ConductaOct" || colum.ColumnName == "ConductaNov" || colum.ColumnName == "ConductaSet")
                        {
                            decimal aux = Convert.ToDecimal(row[colum.ColumnName].ToString());
                            suma += aux;
                        }
                    }
                }
            }

            promedio = suma / 3;
            return promedio;
        }
        private Boolean IsDecimal(string num)
        {
            bool nume = false;
            try
            {
                Convert.ToDecimal(num);
                nume = true;
            }
            catch (Exception)
            { }
            return nume;
        }
        private void refreshAll()
        {
            refreshGrid();
            cboFecha.DataSource = data1.getDate();
            cboFecha.DataBind();
            cboHora.DataSource = data1.getHours(cboFecha.Text);
            cboHora.DataBind();
        }
        protected void btnCodigos_Click(object sender, EventArgs e)
        {
            btnCodigos.CssClass = "nav-link active show btn btn-outline";
            btnEstudiantes.CssClass = "nav-link btn btn-outline";
            btnEspecialidades.CssClass = "nav-link btn btn-outline";
            tabs.ActiveViewIndex = 0;
            btnAgregarCodigos.CssClass = "nav-link active show btn btn-outline";
            btnDelCodigos.CssClass = "nav-link btn btn-outline";
            btnReporte.CssClass = "nav-link btn btn-outline";
            tabsCodigos.ActiveViewIndex = 0;
        }
        protected void btnEspecialidades_Click(object sender, EventArgs e)
        {
            btnCodigos.CssClass = "nav-link btn btn-outline";
            btnEstudiantes.CssClass = "nav-link btn btn-outline";
            btnEspecialidades.CssClass = "nav-link active show btn btn-outline";
            if (llamada.sacarEspeci().Count == 0)
            {
                informativo.llenar("No se ha habilitado ninguna especialidad para el proceso todavia", "Importante!");
            }
            tabs.ActiveViewIndex = 1;
            refreshGrid();
        }
        protected void btnEstudiantes_Click(object sender, EventArgs e)
        {
            btnEspecialidades.CssClass = "nav-link btn btn-outline";
            btnCodigos.CssClass = "nav-link btn btn-outline";
            btnEstudiantes.CssClass = "nav-link active show btn btn-outline";
            tabs.ActiveViewIndex = 2;
            btnEstNotas.CssClass = "nav-link active btn btn-info m-b-10 m-l-5";
            btnEstAdmitir.CssClass = "nav-link btn btn-outline";
            cboEspecialidad.DataSource = llamada.sacarEspeciDisponibles();
            cboEspecialidad.DataBind();
            btnOpcion1.CssClass = "btn btn-dropbox";
            btnOpcion2.CssClass = "btn btn-light";
            if (cboEspecialidad.Items.Count == 0)
            {
                informativo.llenar("No se ha habilitado ninguna especialidad para el proceso todavia", "Importante!");
            }
            else
            {
                if (admin.getAllPreMatriculados(cboEspecialidad.Text).Rows.Count == 0)
                {
                    informativo.llenar("No hay registro de estudiantes en la especialidad de: " + cboEspecialidad.Text, "Importante!");
                    informativo.Visible = true;
                }
            }
            fillGridStudentsByOption("primer");
            tabsStudents.ActiveViewIndex = 0;
            vaciar(pnlEditNotas);
        }
        protected void btnAgregarCodigos_Click(object sender, EventArgs e)
        {
            btnAgregarCodigos.CssClass = "nav-link active show btn btn-outline";
            btnDelCodigos.CssClass = "nav-link btn btn-outline";
            btnReporte.CssClass = "nav-link btn btn-outline";
            btnUsadosPines.CssClass = "nav-link btn btn-outline";
            btnDisponiblesPines.CssClass = "nav-link btn btn-outline";
            tabsCodigos.ActiveViewIndex = 0;
        }
        protected void btnDelCodigos_Click(object sender, EventArgs e)
        {
            btnDelCodigos.CssClass = "nav-link active show btn btn-outline";
            btnAgregarCodigos.CssClass = "nav-link btn btn-outline";
            btnReporte.CssClass = "nav-link btn btn-outline";
            btnUsadosPines.CssClass = "nav-link btn btn-outline";
            btnDisponiblesPines.CssClass = "nav-link btn btn-outline";
            cboFechaCod.DataSource = data1.getDate();
            cboFechaCod.DataBind();
            cboHoraCod.DataSource = data1.getHours(cboFechaCod.Text);
            cboHoraCod.DataBind();
            if (data1.getDate().Count == 0)
            {
                informativo.llenar("Todavia no existe registro de pines generados", "Importante!");
                informativo.Visible = true;
            }
            tabsCodigos.ActiveViewIndex = 1;
        }
        protected void btnReporte_Click(object sender, EventArgs e)
        {
            btnReporte.CssClass = "nav-link active show btn btn-outline";
            btnDelCodigos.CssClass = "nav-link btn btn-outline";
            btnAgregarCodigos.CssClass = "nav-link btn btn-outline";
            btnUsadosPines.CssClass = "nav-link btn btn-outline";
            btnDisponiblesPines.CssClass = "nav-link btn btn-outline";
            cboFecha.DataSource = data1.getDate();
            cboFecha.DataBind();
            cboHora.DataSource = data1.getHours(cboFecha.Text);
            cboHora.DataBind();
            if (data1.getDate().Count == 0)
            {
                informativo.llenar("Todavia no existe registro de pines generados", "Importante!");
                informativo.Visible = true;
            }
            tabsCodigos.ActiveViewIndex = 2;
        }
        protected void btnUsadosPines_Click(object sender, EventArgs e)
        {
            btnUsadosPines.CssClass = "nav-link active show btn btn-outline";
            btnDelCodigos.CssClass = "nav-link btn btn-outline";
            btnReporte.CssClass = "nav-link btn btn-outline";
            btnAgregarCodigos.CssClass = "nav-link btn btn-outline";
            btnDisponiblesPines.CssClass = "nav-link btn btn-outline";
            tabsCodigos.ActiveViewIndex = 3;
            gvPinesUsados.DataSource = data1.sacarpinesUsados();
            gvPinesUsados.DataBind();
            txtBusCodUsado.Text = "";
        }
        protected void btnDisponiblesPines_Click(object sender, EventArgs e)
        {
            btnDisponiblesPines.CssClass = "nav-link active show btn btn-outline";
            btnUsadosPines.CssClass = "nav-link btn btn-outline";
            btnDelCodigos.CssClass = "nav-link btn btn-outline";
            btnReporte.CssClass = "nav-link btn btn-outline";
            btnAgregarCodigos.CssClass = "nav-link btn btn-outline";
            tabsCodigos.ActiveViewIndex = 4;
            gvPinesDisponibles.DataSource = data1.sacarpines();
            gvPinesDisponibles.DataBind();
            txtBusCodDisponible.Text = "";
        }
        protected void btnEstNotas_Click(object sender, EventArgs e)
        {
            btnEstNotas.CssClass = "nav-link active show btn btn-info m-b-10 m-l-5";
            btnEstAdmitir.CssClass = "nav-link btn btn-outline";
            if (btnOpcion1.CssClass == "btn btn-dropbox")
            {
                fillGridStudentsByOption("primer");
            }
            else
            {
                fillGridStudentsByOption("segunda");
            }
            if (admin.getAllPreMatriculados(cboEspecialidad.Text).Rows.Count == 0)
            {
                informativo.llenar("No hay registro de estudiantes en la especialidad de: " + cboEspecialidad.Text, "Importante!");
                informativo.Visible = true;
            }
            tabsStudents.ActiveViewIndex = 0;
            vaciar(pnlEditNotas);
        }
        protected void btnEstAdmitir_Click(object sender, EventArgs e)
        {
            btnEstAdmitir.CssClass = "nav-link active show btn btn-info m-b-10 m-l-5";
            btnEstNotas.CssClass = "nav-link btn btn-outline";
            if (btnOpcion1.CssClass == "btn btn-dropbox")
            {
                fillGridAdmitidosByOption("primer");
            }
            else
            {
                fillGridAdmitidosByOption("segunda");
            }
            cedulas = new ArrayList();
            if (llamada2.sacarNotas(cboEspecialidad.Text).Rows.Count == 0)
            {
                informativo.llenar("No hay registro de estudiantes en la especialidad de: " + cboEspecialidad.Text, "Importante!");
                informativo.Visible = true;
            }
            btnAdnuevosEst.CssClass = "nav-link active btn btn-success m-b-10 m-l-5";
            btnAdEstAdmitidos.CssClass = "nav-link btn btn-outline";
            tabsStudents.ActiveViewIndex = 1;
            tabsAdmitidos.ActiveViewIndex = 0;
            vaciar(pnlEditNotas);
        }
        protected void btnAdnuevosEst_Click(object sender, EventArgs e)
        {
            btnAdnuevosEst.CssClass = "nav-link active btn btn-success m-b-10 m-l-5";
            btnAdEstAdmitidos.CssClass = "nav-link btn btn-outline";
            gvStudentsAdmitidos.DataSource = llamada2.sacarNotas(cboEspecialidad.Text);
            gvStudentsAdmitidos.DataBind();
            tabsAdmitidos.ActiveViewIndex = 0;
        }
        protected void btnAdEstAdmitidos_Click(object sender, EventArgs e)
        {
            btnAdnuevosEst.CssClass = "nav-link btn btn-outline";
            btnAdEstAdmitidos.CssClass = "nav-link active btn btn-success m-b-10 m-l-5";
            gvEditAdmitidos.DataSource = llamada2.sacarAdmitidos(cboEspecialidad.Text);
            gvEditAdmitidos.DataBind();
            tabsAdmitidos.ActiveViewIndex = 1;
        }
        private void fillGridAdmitidosByOption(string option)
        {
            if (option == "primer")
            {
                gvStudentsAdmitidos.DataSource = llamada2.sacarNotasOpcion1(cboEspecialidad.Text);
                gvStudentsAdmitidos.DataBind();
                admitidos = llamada2.sacarNotasOpcion1(cboEspecialidad.Text);
            }
            else
            {
                gvStudentsAdmitidos.DataSource = llamada2.sacarNotasOpcion2(cboEspecialidad.Text);
                gvStudentsAdmitidos.DataBind();
                admitidos = llamada2.sacarNotasOpcion2(cboEspecialidad.Text);
            }
        }
        private void fillGridStudentsByOption(string option)
        {
            if (option == "primer")
            {
                gvStudents.DataSource = admin.getPreMatriculadosOpcion1(cboEspecialidad.Text);
                gvStudents.DataBind();
                estudiantes = admin.getPreMatriculadosOpcion1(cboEspecialidad.Text);
            }
            else
            {
                gvStudents.DataSource = admin.getPreMatriculadosOpcion2(cboEspecialidad.Text);
                gvStudents.DataBind();
                estudiantes = admin.getPreMatriculadosOpcion2(cboEspecialidad.Text);
            }
        }
        protected void cboEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (btnOpcion1.CssClass == "btn btn-dropbox")
            {
                fillGridStudentsByOption("primer");
                fillGridAdmitidosByOption("primer");
            }
            else
            {
                fillGridStudentsByOption("segunda");
                fillGridAdmitidosByOption("segunda");
            }
            gvEditAdmitidos.DataSource = llamada2.sacarAdmitidos(cboEspecialidad.Text);
            gvEditAdmitidos.DataBind();
        }
        protected void btnGeneracion_Click(object sender, EventArgs e)
        {
            if (IsNumeric(txtcantidad.Text))
            {
                int menos = Convert.ToInt32(txtcantidad.Text);
                string[] data = admin.getDataProcesoAdmision(DateTime.Today.Year.ToString());
                admin.editDataAdmission(DateTime.Today.Year.ToString(), Convert.ToString(menos), data[4], data[2], data[3], "0");
                data1.pines(txtcantidad.Text);
                completado.llenar("Se generaron correctamente los " + txtcantidad.Text + " pines");
                completado.Visible = true;
                txtcantidad.Text = "";
                refreshAll();
            }
            else
            {
                error.llenar("Debe ser numero");
                error.Visible = true;
                txtcantidad.Text = "";
            }
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtCod.Text == "")
            {
                error.llenar("Debe de estar lleno");
                error.Visible = true;
            }
            else
            {
                if (data1.existPin(txtCod.Text))
                {
                    int menos = -1;
                    string[] data = admin.getDataProcesoAdmision(DateTime.Today.Year.ToString());
                    admin.editDataAdmission(DateTime.Today.Year.ToString(), Convert.ToString(menos), data[4], data[2], data[3], "0");
                    data1.eliminarPin(txtCod.Text);
                    completado.llenar("Se ha eliminado correctamente el pin");
                    completado.Visible = true;
                }
                else
                {
                    error.llenar("El pin digitado no existe");
                    error.Visible = true;
                }
            }
            txtCod.Text = "";
        }
        protected void btnElimCodigos_Click(object sender, EventArgs e)
        {
            if (cboFechaCod.Text == "")
            {
                error.llenar("No existe registro de pines");
                error.Visible = true;
            }
            else
            {
                string dato = cboFechaCod.Text;
                dato += " " + cboHoraCod.Text;
                DateTime date = Convert.ToDateTime(dato);
                DataTable tabla = data1.sacarpinesDate(cboFechaCod.Text, cboHoraCod.Text);
                int menos = 0;
                menos -= tabla.Rows.Count;
                string[] data = admin.getDataProcesoAdmision(DateTime.Today.Year.ToString());
                admin.editDataAdmission(DateTime.Today.Year.ToString(), Convert.ToString(menos), data[4], data[2], data[3], "0");
                data1.eliminarPinFechas(date);
                completado.llenar("Se han eliminado correctamente los pines de la fecha: " + cboFechaCod.Text + " con hora: " + cboHoraCod.Text);
                completado.Visible = true;
                cboFechaCod.DataSource = data1.getDate();
                cboFechaCod.DataBind();
                cboHoraCod.DataSource = data1.getHours(cboFechaCod.Text);
                cboHoraCod.DataBind();
            }
        }
        protected void cboFecha_TextChanged(object sender, EventArgs e)
        {
            cboHora.DataSource = data1.getHours(cboFecha.Text);
            cboHora.DataBind();
        }
        protected void cboFechaCod_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboHoraCod.DataSource = data1.getHours(cboFechaCod.Text);
            cboHoraCod.DataBind();
        }
        protected void btnGenR_Click(object sender, EventArgs e)
        {
            if (cboFecha.Text == "")
            {
                error.llenar("No existe registro de pines");
            }
            else
            {
                datos.clReport reportes = new datos.clReport();
                reportes.SetMode("Table");
                reportes.asignarT(data1.sacarpines());
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../report.aspx');", true);
            }
        }
        protected void btnGenerarR_Click(object sender, EventArgs e)
        {
            if (cboFecha.Text == "")
            {
                error.llenar("No existe registro de pines");
            }
            else
            {
                datos.clReport reportes = new datos.clReport();
                reportes.SetMode("Table");
                reportes.asignarT(data1.sacarpinesDate(cboFecha.Text, cboHora.Text));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../report.aspx');", true);
            }
        }
        protected void btnReportPinUnavailable_Click(object sender, EventArgs e)
        {
            datos.clReport reportes = new datos.clReport();
            reportes.SetMode("Table");
            reportes.asignarT(data1.sacarpinesUsados());
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../report.aspx');", true);
        }
        protected void txtBusCodUsado_TextChanged(object sender, EventArgs e)
        {
            searchGrids(gvPinesUsados, data1.sacarpinesUsados(), txtBusCodUsado.Text);
        }
        protected void gvPinesUsados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPinesUsados.PageIndex = e.NewPageIndex;
            searchGrids(gvPinesUsados, data1.sacarpinesUsados(), txtBusCodUsado.Text);
        }
        protected void gvPinesUsados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvPinesUsados.Rows[index];
            if (e.CommandName == "habilitar")
            {
                string pin = row.Cells[1].Text;
                data1.setAvailablePin(pin);
                completado.llenar("Se ha habilitado el pin: " + pin);
                searchGrids(gvPinesUsados, data1.sacarpinesUsados(), txtBusCodUsado.Text);
            }
        }
        protected void txtBusCodDisponible_TextChanged(object sender, EventArgs e)
        {
            searchGrids(gvPinesDisponibles, data1.sacarpines(), txtBusCodDisponible.Text);
        }
        protected void gvPinesDisponibles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPinesDisponibles.PageIndex = e.NewPageIndex;
            searchGrids(gvPinesDisponibles, data1.sacarpines(), txtBusCodDisponible.Text);
        }
        protected void gvPinesDisponibles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (index <= 9 && index > 0)
            {
                GridViewRow row = gvPinesDisponibles.Rows[index];
                if (e.CommandName == "Deshabilitar")
                {
                    string pin = row.Cells[1].Text;
                    data1.setUnavailablePin(pin);
                    completado.llenar("Se ha deshabilitado el pin: " + pin);
                    searchGrids(gvPinesDisponibles, data1.sacarpines(), txtBusCodDisponible.Text);
                }
            }
        }
        private void refreshGrid()
        {
            DataTable t = llamada.sacarEsp();
            DataTable tabla = new DataTable();
            tabla.Columns.Add(t.Columns[0].ToString(), typeof(string));
            tabla.Columns.Add(t.Columns[1].ToString(), typeof(bool));
            tabla.Columns.Add(t.Columns[2].ToString(), typeof(string));
            foreach (DataRow fila in t.Rows)
            {
                string espe = fila["especialidad"].ToString();
                string disp = fila["disponible"].ToString();
                string canti = fila["cantidad"].ToString();
                bool dis = false;
                if (disp == "abierto")
                {
                    dis = true;
                }
                tabla.Rows.Add(espe, dis, canti);
            }
            gvSpecialty.DataSource = tabla;
            gvSpecialty.DataBind();
            cboEspecialidad.DataSource = llamada.sacarEspeci();
            cboEspecialidad.DataBind();
        }
        protected void gvSpecialty_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
            string id = gvSpecialty.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = (GridViewRow)gvSpecialty.Rows[e.RowIndex];
            CheckBox dis = (CheckBox)row.Cells[1].Controls[0];
            TextBox can = (TextBox)row.Cells[2].Controls[0];
            if (IsNumeric(can.Text) || Convert.ToInt32(can.Text) < 0)
            {
                string disp = "";
                string mensaje = "";
                if (dis.Checked)
                {
                    disp = "abierto";
                    mensaje = "Se ha habilitado la especialidad de " + id + " para el proceso de admisión";
                }
                else
                {
                    disp = "cerrado";
                    mensaje = "Se ha deshabilitado la especialidad de " + id + " para el proceso de admisión";
                }
                llamada.insertEspecialidades(id, disp, Convert.ToInt32(can.Text));
                completado.llenar(mensaje);
                completado.Visible = true;
                gvSpecialty.EditIndex = -1;
                refreshGrid();
            }
            else
            {
                error.llenar("El campo de la cantidad debe ser un numero y mayor a 0");
                error.Visible = true;
            }
        }
        protected void gvSpecialty_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSpecialty.PageIndex = e.NewPageIndex;
            refreshGrid();
        }
        protected void gvSpecialty_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSpecialty.EditIndex = -1;
            refreshGrid();
        }
        protected void gvSpecialty_RowEditing1(object sender, GridViewEditEventArgs e)
        {
            gvSpecialty.EditIndex = e.NewEditIndex;
            refreshGrid();
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
        protected void txtNomEstudi_TextChanged(object sender, EventArgs e)
        {
            searchGrids(gvStudents, estudiantes, txtNomEstudi.Text);
        }
        protected void txtNomAdmitido_TextChanged(object sender, EventArgs e)
        {
            searchGrids(gvEditAdmitidos, admitidos, txtNomAdmitido.Text);
        }
        protected void txtNombreEstudiante_TextChanged(object sender, EventArgs e)
        {
            searchGrids(gvStudentsAdmitidos, llamada2.sacarNotas(cboEspecialidad.Text), txtNombreEstudiante.Text);
        }
        protected void gvStudents_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string userid = gvStudents.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = (GridViewRow)gvStudents.Rows[e.RowIndex];
            TextBox entrevista = (TextBox)row.Cells[3].Controls[0];
            TextBox examen = (TextBox)row.Cells[4].Controls[0];
            if (entrevista.Text == "" || examen.Text == "")
            {
                error.llenar("Están vacíos los espacios de entrevista y examen");
                error.Visible = true;
            }
            else
            {
                if (IsDecimal(examen.Text) && IsDecimal(entrevista.Text))
                {
                    if (Convert.ToDecimal(entrevista.Text) >= 0
                        && Convert.ToDecimal(entrevista.Text) <= 50 && Convert.ToDecimal(examen.Text) >= 0
                        && Convert.ToDecimal(examen.Text) <= 15)
                    {
                        decimal pro = promedio(llamada.getNotasEstudiante(userid));
                        Session["promedioNuevo"] = Convert.ToString(pro);
                        decimal promedioConduct = promedioConducta(llamada.getNotasEstudiante(userid));
                        string conducta = promedioConduct.ToString();
                        int cantAusencias = ausencias(llamada.getNotasEstudiante(userid));
                        string ausencia = cantAusencias.ToString();
                        if (llamada.ingresarPromedios(Session["promedioNuevo"].ToString(), examen.Text, entrevista.Text, Convert.ToInt64(userid), conducta, ausencia))
                        {
                            completado.llenar("Notas del estudiante ingresadas");
                            completado.Visible = true;
                            gvStudents.EditIndex = -1;
                            if (btnOpcion1.CssClass == "btn btn-dropbox")
                            {
                                fillGridStudentsByOption("primer");
                            }
                            else
                            {
                                fillGridStudentsByOption("segunda");
                            }
                            searchGrids(gvStudents, estudiantes, txtNomEstudi.Text);
                            vaciar(pnlEditNotas);
                            vaciar(pnlNotas);
                            pnlEditNotas.Visible = false;
                        }
                        else
                        {
                            error.llenar("Ocurrio un error");
                            error.Visible = true;
                        }
                    }
                    else
                    {
                        error.llenar("El promedio del examen no debe ser mayor a 15. Además el promedio de entrevista no debe ser mayor a 50");
                        error.Visible = true;
                    }
                }
                else
                {
                    error.llenar("Las notas deben ser numeros");
                    error.Visible = true;
                }
            }
        }
        protected void gvStudents_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvStudents.EditIndex = e.NewEditIndex;
            Session["promedioNotas"] = gvStudents.Rows[e.NewEditIndex].Cells[5].Text;
            if (btnOpcion1.CssClass == "btn btn-dropbox")
            {
                fillGridStudentsByOption("primer");
            }
            else
            {
                fillGridStudentsByOption("segunda");
            }
            searchGrids(gvStudents, estudiantes, txtNomEstudi.Text);
        }
        protected void gvStudents_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStudents.PageIndex = e.NewPageIndex;
            if (btnOpcion1.CssClass == "btn btn-dropbox")
            {
                fillGridStudentsByOption("primer");
            }
            else
            {
                fillGridStudentsByOption("segunda");
            }
            searchGrids(gvStudents, estudiantes, txtNomEstudi.Text);
        }
        protected void gvStudents_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvStudents.EditIndex = -1;
            vaciar(pnlEditNotas);
            if (btnOpcion1.CssClass == "btn btn-dropbox")
            {
                fillGridStudentsByOption("primer");
            }
            else
            {
                fillGridStudentsByOption("segunda");
            }
            searchGrids(gvStudents, estudiantes, txtNomEstudi.Text);
        }
        protected void gvStudents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (index <= 9 && index >= 0)
            {

                GridViewRow row = (GridViewRow)gvStudents.Rows[index];
                if (e.CommandName == "notas")
                {
                    Session["id"] = gvStudents.Rows[index].Cells[0].Text;
                    Session["NotaEntrevista"] = gvStudents.Rows[index].Cells[3].Text;
                    Session["NotaExamen"] = gvStudents.Rows[index].Cells[4].Text;
                    Session["promedioNotas"] = gvStudents.Rows[index].Cells[5].Text;
                    fillNotas(llamada.getNotasEstudiante(Session["id"].ToString()));
                    pnlEditNotas.Visible = true;
                }
                if (e.CommandName == "report")
                {
                    string cedula = row.Cells[0].Text;
                    report repor = new report();
                    repor.setName(cedula, "true");
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../report.aspx');", true);
                }
                if (e.CommandName == "delete")
                {
                    string id = row.Cells[0].Text;
                    llamada.deletePreMatriculado(id);
                    if (btnOpcion1.CssClass == "btn btn-dropbox")
                    {
                        fillGridStudentsByOption("primer");
                    }
                    else
                    {
                        fillGridStudentsByOption("segunda");
                    }
                    searchGrids(gvStudents, estudiantes, txtNomEstudi.Text);
                    completado.llenar("Se ha eliminado el estudiante");
                }
            }
        }
        protected void gvStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void btnAceptarStude_Click(object sender, EventArgs e)
        {
            if (gvStudentsAdmitidos.Rows.Count == 0)
            {
                error.llenar("No existe registro de estudiantes en la especialidad de: " + cboEspecialidad.Text);
            }
            else
            {
                int cant = llamada2.cantidadStudents(cboEspecialidad.Text);
                int resi = 0;
                if (cant == 0)
                {
                    error.llenar("La especialidad de: " + cboEspecialidad.Text + " no tiene cupos disponibles");
                }
                else
                {
                    DataTable dt = llamada2.sacarNotas(cboEspecialidad.Text);
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        if (x < cant)
                        {
                            DataRow row = dt.Rows[x];
                            string id = row["cedula"].ToString();
                            llamada2.ingresarAdmitidos(id, cboEspecialidad.Text);
                            if (x == dt.Rows.Count - 1)
                            {
                                resi = cant - (x + 1);
                                cant = x + 1;
                            }
                        }
                        else
                        {
                            cant = x;
                            break;
                        }
                    }
                    completado.llenar("La cantidad de: " + cant + " estudiantes en la especialidad de " + cboEspecialidad.Text + " han sido admitidos. Quedan " + resi + " cupos " +
                        "disponibles");
                    completado.Visible = true;
                    if (btnOpcion1.CssClass == "btn btn-dropbox")
                    {
                        fillGridAdmitidosByOption("primer");
                    }
                    else
                    {
                        fillGridAdmitidosByOption("segunda");
                    }
                }
            }
        }
        protected void btnAceptarStude2_Click(object sender, EventArgs e)
        {
            if (gvStudentsAdmitidos.Rows.Count == 0)
            {
                error.llenar("No existe registro de estudiantes en la especialidad de: " + cboEspecialidad.Text);
            }
            else
            {
                int cant = llamada2.cantidadStudents(cboEspecialidad.Text);
                int resi = 0;
                if (cant == 0)
                {
                    error.llenar("La especialidad de: " + cboEspecialidad.Text + " no tiene cupos disponibles");
                }
                else
                {
                    foreach (GridViewRow row in gvStudentsAdmitidos.Rows)
                    {
                        CheckBox chk = row.FindControl("chkSel") as CheckBox;
                        if (chk.Checked)
                        {
                            if (!(cedulas.Contains(row.Cells[3].Text)))
                                cedulas.Add(row.Cells[3].Text);
                        }
                        else
                        {
                            if (cedulas.Contains(row.Cells[3].Text))
                                cedulas.Remove(row.Cells[3].Text);
                        }
                    }
                    if (cedulas.Count > cant)
                    {
                        error.llenar("La cantidad de estudiantes seleccionados es de: " + cedulas.Count + " y los cupos disponibles en esta especialidad son: " + cant);
                        error.Visible = true;
                    }
                    else if (cedulas.Count == 0)
                    {
                        error.llenar("No hay estudiantes seleccionados para la especialidad de " + cboEspecialidad.Text);
                        error.Visible = true;
                    }
                    else
                    {
                        for (int x = 0; x < cedulas.Count; x++)
                        {
                            if (x < cant)
                            {
                                string id = cedulas[x].ToString();
                                llamada2.ingresarAdmitidos(id, cboEspecialidad.Text);
                                if (x == cedulas.Count - 1)
                                {
                                    resi = cant - (x + 1);
                                    cant = x + 1;
                                }
                            }
                            else
                            {
                                cant = x;
                                break;
                            }
                        }
                        completado.llenar("La cantidad de: " + cant + " estudiantes en la especialidad de " + cboEspecialidad.Text + " han sido admitidos. Quedan " + resi + " cupos " +
                            "disponibles");
                        completado.Visible = true;
                        if (btnOpcion1.CssClass == "btn btn-dropbox")
                        {
                            fillGridAdmitidosByOption("primer");
                        }
                        else
                        {
                            fillGridAdmitidosByOption("segunda");
                        }
                        cedulas = new ArrayList();
                    }
                }
            }
        }
        private void fillGvAdmitidos()
        {
            foreach (GridViewRow row in gvStudentsAdmitidos.Rows)
            {
                CheckBox chk = row.FindControl("chkSel") as CheckBox;
                if (chk.Checked)
                {
                    if (!(cedulas.Contains(row.Cells[3].Text)))
                        cedulas.Add(row.Cells[3].Text);
                }
                else
                {
                    if (cedulas.Contains(row.Cells[3].Text))
                        cedulas.Remove(row.Cells[3].Text);
                }
            }
            if (btnOpcion1.CssClass == "btn btn-dropbox")
            {
                fillGridAdmitidosByOption("primer");
            }
            else
            {
                fillGridAdmitidosByOption("segunda");
            }
            foreach (GridViewRow row in gvStudentsAdmitidos.Rows)
            {
                if (cedulas.Contains(row.Cells[3].Text))
                {
                    CheckBox chk = row.FindControl("chkSel") as CheckBox;
                    chk.Checked = true;
                }
            }
        }
        protected void gvStudentsAdmitidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStudentsAdmitidos.PageIndex = e.NewPageIndex;
            fillGvAdmitidos();
        }
        protected void gvEditAdmitidos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)gvEditAdmitidos.Rows[e.RowIndex];
            string id = row.Cells[3].Text;
            llamada.deleteAdmitidos(id, cboEspecialidad.Text);
            gvEditAdmitidos.DataSource = llamada2.sacarAdmitidos(cboEspecialidad.Text);
            gvEditAdmitidos.DataBind();
            completado.llenar("Se ha eliminado el estudiante admitido");
            completado.Visible = true;
        }
        protected void gvEditAdmitidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEditAdmitidos.PageIndex = e.NewPageIndex;
            gvEditAdmitidos.DataSource = llamada2.sacarAdmitidos(cboEspecialidad.Text);
            gvEditAdmitidos.DataBind();
        }
        protected void gvEditAdmitidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "notify")
            {
                llamada2.sendEmailtoAdmit(gvEditAdmitidos.Rows[index].Cells[3].Text,cboEspecialidad.Text);
                completado.llenar("Se envió la notificación al correo del estudiante");
            }
            if (e.CommandName == "matricula")
            {
                llamada2.sendFormToAdmit(gvEditAdmitidos.Rows[index].Cells[3].Text, cboEspecialidad.Text);
                completado.llenar("Se envió el enlace del formulario al correo del estudiante");
            }
        }
        protected void btnReporteAdmitidos_Click(object sender, EventArgs e)
        {
            if (llamada2.sacarAdmitidos(cboEspecialidad.Text).Rows.Count == 0)
            {
                informativo.llenar("En la especialidad de " + cboEspecialidad.Text + " no hay estudiantes admitidos", "No existen estudiantes");
            }
            else
            {
                datos.clReport reportes = new datos.clReport();
                reportes.SetMode("Table");
                reportes.asignarT(llamada2.sacarAdmitidos(cboEspecialidad.Text));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../report.aspx');", true);
            }
        }
        protected void btnFormsMatricula_Click(object sender, EventArgs e)
        {
            if (llamada2.sacarAdmitidos(cboEspecialidad.Text).Rows.Count == 0)
                informativo.llenar("En la especialidad de " + cboEspecialidad.Text + " no hay estudiantes admitidos", "No existen estudiantes");
            else
            {
                llamada2.enviarCorreosMatricula(cboEspecialidad.Text);
                completado.llenar("Se enviaron los formularios a los correos de los estudiantes");
            }
        }
        protected void btnNotifyStudens_Click(object sender, EventArgs e)
        {
            if (llamada2.sacarAdmitidos(cboEspecialidad.Text).Rows.Count == 0)
                informativo.llenar("En la especialidad de " + cboEspecialidad.Text + " no hay estudiantes admitidos", "No existen estudiantes");
            else
            {
                llamada2.enviarCorreoAdmitidos(cboEspecialidad.Text);
                completado.llenar("Se enviaron las notificaciones a los correos de los estudiantes");
            }
        }
        private void fillNotas(DataTable table)
        {
            vaciarNotas();
            if (table.Rows.Count == 0)
            {
                vaciarNotas();
            }
            else
            {
                DataRow row = table.Rows[0];
                txtSetAusIn.Value = row["AusInjustSet"].ToString();
                txtSetCien.Value = Convert.ToDecimal(row["CienciasSet"].ToString()).ToString("##.##");
                txtSetCiv.Value = Convert.ToDecimal(row["CivicaSet"].ToString()).ToString("##.##");
                txtSetCond.Value = Convert.ToDecimal(row["ConductaSet"].ToString()).ToString("##.##");
                txtSetEspa.Value = Convert.ToDecimal(row["EspaSet"].ToString()).ToString("##.##");
                txtSetEst.Value = Convert.ToDecimal(row["EstudiosSet"].ToString()).ToString("##.##");
                txtSetIng.Value = Convert.ToDecimal(row["InglesSet"].ToString()).ToString("##.##");
                txtSetMate.Value = Convert.ToDecimal(row["MateSet"].ToString()).ToString("##.##");

                txtOctAusIn.Value = row["AusInjustOct"].ToString();
                txtOctCien.Value = Convert.ToDecimal(row["CienciasOct"].ToString()).ToString("##.##");
                txtOctCiv.Value = Convert.ToDecimal(row["CivicaOct"].ToString()).ToString("##.##");
                txtOctCond.Value = Convert.ToDecimal(row["ConductaOct"].ToString()).ToString("##.##");
                txtOctEspa.Value = Convert.ToDecimal(row["EspaOct"].ToString()).ToString("##.##");
                txtOctEst.Value = Convert.ToDecimal(row["EstudiosOct"].ToString()).ToString("##.##");
                txtOctIng.Value = Convert.ToDecimal(row["InglesOct"].ToString()).ToString("##.##");
                txtOctMate.Value = Convert.ToDecimal(row["MateOct"].ToString()).ToString("##.##");

                txtNovAusIn.Value = row["AusInjustNov"].ToString();
                txtNovCien.Value = Convert.ToDecimal(row["CienciasNov"].ToString()).ToString("##.##");
                txtNovCiv.Value = Convert.ToDecimal(row["CivicaNov"].ToString()).ToString("##.##");
                txtNovCond.Value = Convert.ToDecimal(row["ConductaNov"].ToString()).ToString("##.##");
                txtNovEspa.Value = Convert.ToDecimal(row["EspaNov"].ToString()).ToString("##.##");
                txtNovEst.Value = Convert.ToDecimal(row["EstudiosNov"].ToString()).ToString("##.##");
                txtNovIng.Value = Convert.ToDecimal(row["InglesNov"].ToString()).ToString("##.##");
                txtNovMate.Value = Convert.ToDecimal(row["MateNov"].ToString()).ToString("##.##");
            }
        }
        protected void btnAgregarNotas_Click(object sender, EventArgs e)
        {
            if (NotasVacias())
            {
                error.llenar("Todas las notas se deben de ingresar obligatoriamente");
            }
            else
            {
                if (llamada.getNotasEstudiante(Session["id"].ToString()).Rows.Count == 0)
                {
                    if (llamada.ingresarNotas(Session["id"].ToString(), txtSetEspa.Value, txtSetIng.Value, txtSetMate.Value, txtSetEst.Value, txtSetCiv.Value, txtSetCien.Value, txtSetAusIn.Value, txtSetCond.Value,
                        txtOctEspa.Value, txtOctIng.Value, txtOctMate.Value, txtOctEst.Value, txtOctCiv.Value, txtOctCien.Value, txtOctAusIn.Value, txtOctCond.Value, txtNovEspa.Value,
                        txtNovIng.Value, txtNovMate.Value, txtNovEst.Value, txtNovCiv.Value, txtNovCien.Value, txtNovAusIn.Value, txtNovCond.Value))
                    {
                        vaciarNotas();
                    }
                }
                else
                {
                    if (llamada.editNotas(Session["id"].ToString(), txtSetEspa.Value, txtSetIng.Value, txtSetMate.Value, txtSetEst.Value, txtSetCiv.Value, txtSetCien.Value, txtSetAusIn.Value, txtSetCond.Value,
                       txtOctEspa.Value, txtOctIng.Value, txtOctMate.Value, txtOctEst.Value, txtOctCiv.Value, txtOctCien.Value, txtOctAusIn.Value, txtOctCond.Value, txtNovEspa.Value,
                       txtNovIng.Value, txtNovMate.Value, txtNovEst.Value, txtNovCiv.Value, txtNovCien.Value, txtNovAusIn.Value, txtNovCond.Value))
                    {
                        vaciarNotas();
                    }
                }
                decimal pro = promedio(llamada.getNotasEstudiante(Session["id"].ToString()));
                Session["promedioNuevo"] = Convert.ToString(pro);
                decimal promedioConduct = promedioConducta(llamada.getNotasEstudiante(Session["id"].ToString()));
                string conducta = promedioConduct.ToString();
                int cantAusencias = ausencias(llamada.getNotasEstudiante(Session["id"].ToString()));
                string ausencia = cantAusencias.ToString();
                if (llamada.ingresarPromedios(Session["promedioNuevo"].ToString(), Session["NotaExamen"].ToString(), Session["NotaEntrevista"].ToString(), Convert.ToInt64(Session["id"].ToString()), conducta, ausencia))
                {
                    if (btnOpcion1.CssClass == "btn btn-dropbox")
                    {
                        fillGridStudentsByOption("primer");
                    }
                    else
                    {
                        fillGridStudentsByOption("segunda");
                    }
                    searchGrids(gvStudents, estudiantes, txtNomEstudi.Text);
                    completado.llenar("Se ingresaron las notas del estudiante");
                    pnlEditNotas.Visible = false;
                }
            }
        }
        protected void btnCancelarNotas_Click(object sender, EventArgs e)
        {
            pnlEditNotas.Visible = false;
            Session["id"] = "";
            Session["promedioNotas"] = "";
            vaciarNotas();
        }
        protected void btnOpcion1_Click(object sender, EventArgs e)
        {
            fillGridStudentsByOption("primer");
            fillGridAdmitidosByOption("primer");
            btnOpcion1.CssClass = "btn btn-dropbox";
            btnOpcion2.CssClass = "btn btn-light";
        }
        protected void btnReportEstudiantes_Click(object sender, EventArgs e)
        {
            string opc = "";
            if (btnOpcion1.CssClass == "btn btn-dropbox")
                opc = "1";
            else
                opc = "2";
            if (admin.getReportStudents(cboEspecialidad.Text, opc).Rows.Count == 0)
            {
                informativo.llenar("En la especialidad de " + cboEspecialidad.Text + " no hay estudiantes registrados", "No existen estudiantes");
            }
            else
            {
                datos.clReport reportes = new datos.clReport();
                reportes.SetMode("Table");
                reportes.asignarT(admin.getReportStudents(cboEspecialidad.Text, opc));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../report.aspx');", true);
            }
        }
        protected void btnReportStudents_Click(object sender, EventArgs e)
        {
            string opc = "";
            if (btnOpcion1.CssClass == "btn btn-dropbox")
                opc = "1";
            else
                opc = "2";
            if (admin.getReportStudents(cboEspecialidad.Text, opc).Rows.Count == 0)
            {
                informativo.llenar("En la especialidad de " + cboEspecialidad.Text + " no hay estudiantes registrados", "No existen estudiantes");
            }
            else
            {
                datos.clReport reportes = new datos.clReport();
                reportes.SetMode("Table");
                reportes.asignarT(admin.getReportStudentsInAdmission(cboEspecialidad.Text, opc));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../report.aspx');", true);
            }
        }
        protected void btnOpcion2_Click(object sender, EventArgs e)
        {
            fillGridStudentsByOption("segunda");
            fillGridAdmitidosByOption("segunda");
            btnOpcion1.CssClass = "btn btn-light";
            btnOpcion2.CssClass = "btn btn-dropbox";
        }
        protected void btnAdmission_Click(object sender, EventArgs e)
        {
            string[] data = admin.getDataProcesoAdmision(DateTime.Today.Year.ToString());
            if (btnAdmission.Text == "Iniciar")
            {
                string finish = "31/12/";
                finish += DateTime.Today.Year.ToString();
                if (admin.editDataAdmission(DateTime.Today.Year.ToString(), "0", "0", DateTime.Today.ToShortDateString(), finish, "0"))
                {
                    admin.ResetPreMatricula();
                    pnlIniciarNuevoProceso.Visible = false;
                    pnlPreMatricula.Enabled = true;
                    btnEditFechasAdmission.Enabled = true;
                    completado.llenar("Proceso de admisión " + DateTime.Today.Year.ToString() + " iniciado correctamente. Por favor configure las fechas de disponibilidad del formulario de pre-matrícula. Nota: Se eliminaron los datos del proceso pasado.");
                }
                else
                {
                    error.llenar("Ocurrió un error al iniciar el proceso de admisión");
                }
            }
        }
        protected void btnSaveAdmission_Click(object sender, EventArgs e)
        {
            string[] data = admin.getDataProcesoAdmision(DateTime.Today.Year.ToString());
            string fecha = data[2];
            if (txtFinishProcess.Text == "" || txtStartProcess.Text == "" || txtPriceSobre.Text == "")
            {
                error.llenar("Los tres datos deben de estar llenos");
            }
            else
            {
                if (Convert.ToInt32(txtPriceSobre.Text) > 0)
                {
                    if (admin.editDataAdmission(DateTime.Today.Year.ToString(), "0", txtPriceSobre.Text, txtStartProcess.Text, txtFinishProcess.Text, "0"))
                    {
                        pnlConfigAdmission.Visible = false;
                        completado.llenar("Se editaron las fechas correctamente.");
                    }
                    else
                    {
                        error.llenar("Sucedió un error en la edición");
                    }
                }
                else
                {
                    error.llenar("El precio del sobre debe ser mayor a 0");
                }
            }
        }
        protected void btnEditFechasAdmission_Click(object sender, EventArgs e)
        {
            string[] data = admin.getDataProcesoAdmision(DateTime.Today.Year.ToString());
            txtStartProcess.Text = Convert.ToDateTime(data[2]).ToString("yyyy-MM-dd");
            txtFinishProcess.Text = Convert.ToDateTime(data[3]).ToString("yyyy-MM-dd");
            txtPriceSobre.Text = data[4];
            pnlConfigAdmission.Visible = true;
        }
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            pnlConfigAdmission.Visible = false;
        }
    }
}