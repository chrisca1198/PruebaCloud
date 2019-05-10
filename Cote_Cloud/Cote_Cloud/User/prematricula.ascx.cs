using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cote_Cloud.User
{
    public partial class prematricula : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void llenar(string mensajes, string encabezados)
        {
            mensaje.InnerText = mensajes;
            encabezado.InnerText = encabezados;
       }
    }
}