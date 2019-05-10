using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cote_Cloud.User
{
    public partial class recuperacion : System.Web.UI.UserControl
    {
        transacciones.clLogIn cl = new transacciones.clLogIn();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public Boolean isTrue()
        {
            string[] datos = cl.getUser(txtNombre.Text);
            if(datos[5] == txtEmail.Text)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btnCambios_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "" || txtNombre.Text == "")
            {
                Response.Write("<script language=javascript>alert('Algunos campos vacios') </script>");
            }
            else
            {
                if (isTrue())
                {
                    string[] datos = cl.getUser(txtNombre.Text);
                    Cote_Cloud.User.email correo = new Cote_Cloud.User.email();
                    correo.bodyHtml = false;
                    correo.mensaje = "La contraseña del usuario "+txtNombre+" es: "+datos[2]+" por favor ingrese a la plataforma y cambie la clave de acceso por una que recuerde más fácilmente.";
                    correo.MailTo = txtEmail.Text;
                    correo.MailSubject = "Recuperación de contraseña";
                    correo.SendMail();
                    txtNombre.Text = "";
                    txtEmail.Text = "";
                    Response.Write("<script language=javascript>alert('Se ha enviado la contraseña a su correo') </script>");
                    this.Visible = false;
                }
                else
                {
                    Response.Write("<script language=javascript>alert('El correo no coincide con el usuario') </script>");
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}