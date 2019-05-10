using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cote_Cloud.User
{
    public partial class password : System.Web.UI.UserControl
    {
        transacciones.clLogIn llamada = new transacciones.clLogIn();
        static string user = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCambios_Click(object sender, EventArgs e)
        {
            if (txtContra.Text == "" || txtContraAnti.Text == "" || txtContraConf.Text == "")
            {
                Response.Write("<script language=javascript>alert('Algunos campos vacios') </script>");
            }
            else
            {
                if (txtContra.Text == txtContraConf.Text)
                {
                    string password = txtContra.Text;
                    if (!(password.Length < 8))
                    {
                        string[] datos = llamada.getUser(user);
                        if (datos[2] == txtContraAnti.Text)
                        {
                            llamada.modificarUser(user, txtContra.Text);
                            txtContra.Text = "";
                            txtContraAnti.Text = "";
                            txtContraConf.Text = "";
                            Response.Write("<script language=javascript>alert('Se ha modificado la contraseña') </script>");
                            this.Visible = false;
                        }
                        else
                        {
                            Response.Write("<script language=javascript>alert('Contraseña antigua incorrecta') </script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script language=javascript>alert('La contraseña nueva debe tener un mínimo de 8 caracteres') </script>");
                    }
                }
                else
                {
                    Response.Write("<script language=javascript>alert('No coinciden la nueva contraseña') </script>");
                }
            }
        }
        public void llenar(bool first,string usuario)
        {
            user = usuario;
            if (!first)
            {
                lblEnca.Text = "Cambiar contraseña";
            }
            else
            {
                lblEnca.Text = "Debe cambiar la contraseña predeterminada por motivos de seguridad";
            }
            this.Visible = true;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}