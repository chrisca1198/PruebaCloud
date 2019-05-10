using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cote_Cloud.User
{
    public partial class informativo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void llenar(string mensaje, string enca)
        {
            lblEnca.Text = enca;
            lblMensaje.Text = mensaje;
            this.Visible = true;
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}