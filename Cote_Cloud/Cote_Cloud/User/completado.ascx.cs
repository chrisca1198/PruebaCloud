using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cote_Cloud.User
{
    public partial class completado : System.Web.UI.UserControl
    {
        static byte[] ne;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void llenar(string mensaje)
        {
            lblMensaje.Text = mensaje;
            this.Visible = true;
        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

    }
}