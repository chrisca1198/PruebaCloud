using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cote_Cloud.User
{
    public partial class tutorial : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}