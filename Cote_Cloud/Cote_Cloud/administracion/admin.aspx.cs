using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Collections;
namespace Cote_Cloud.administracion
{
    public partial class admin1 : System.Web.UI.Page
    {
        transacciones.administracion llamada = new transacciones.administracion();
        protected void Page_Load(object sender, EventArgs e)
        {
            fillDataAdmitidos();
            fillDataPreMatriculados();
            fillDatos();
        }
        private void fillDatos()
        {
            string[] datos = llamada.getDataProcesoAdmision(DateTime.Today.Year.ToString());
            estudiantesPrematriculados.InnerText = datos[1];
            int ganancia = Convert.ToInt32(datos[4]) * Convert.ToInt32(datos[1]);
            Ganancias.InnerText = Convert.ToString(ganancia);
            PinesDisponibles.InnerText = datos[5];
        }
        private void fillDataPreMatriculados()
        {
            ArrayList esp = llamada.sacarEspecialidadesDisponibles();
            for (int a = 0; a < esp.Count; a++)
            {
                Especialidad.Series["PrimeraOpcion"].Points.AddXY(esp[a].ToString(), llamada.StudentsFirstOption(esp[a].ToString()));
                Especialidad.Series["SegundaOpcion"].Points.AddXY(esp[a].ToString(), llamada.StudentsSecondOption(esp[a].ToString()));
            }
        }
        private void fillDataAdmitidos()
        {
            ArrayList esp = llamada.sacarEspecialidadesDisponibles();
            for (int a = 0; a < esp.Count; a++)
            {
                Admitidos.Series["Admitidos"].Points.AddXY(esp[a].ToString(), llamada.sacarAdmitidos(esp[a].ToString()).Rows.Count);
            }
        }
    }
}