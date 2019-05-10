using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cote_Cloud.LogIn
{
    public partial class InicioSesion : System.Web.UI.Page
    {

        transacciones.clLogIn log = new transacciones.clLogIn();
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies["InicioSesion"];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                string[] data = authTicket.UserData.Split(new char[] { '|' });
                if (log.LogCorrect(data[1], data[2]))
                {
                    if (data[0] == "estudiante")
                    {
                        Response.Redirect("~/estudiantes/estudiantes.aspx");
                    }
                    else if (data[0] == "admin")
                    {
                        Response.Redirect("~/administracion/admin.aspx");
                    }
                    else if (data[0] == "profesor")
                    {
                        Response.Redirect("~/profesores/profes.aspx");
                    }
                    else if (data[0] == "coordinador")
                    {

                    }
                }
                else
                {
                    Response.Write("<script language=javascript>alert('Ocurrió un error de autenticación, vuelva a iniciar sesión')</script>");
                    FormsAuthentication.SignOut();
                }
            }
        }
        protected void Recuperacion_Click(object sender, EventArgs e)
        {
            recuperacion.Visible = true;
        }
        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "" || txtContra.Value == "")
            {
                Response.Write("<script language=javascript>alert('Campos vacios')</script>");
            }
            else
            {
                string[] datos = log.logIn(txtUsuario.Text, txtContra.Value);
                if (datos[0] == "SI")
                {
                    string userData = datos[1] + "|" + txtUsuario.Text + "|" + txtContra.Value;
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, FormsAuthentication.FormsCookieName, DateTime.Now, DateTime.Now.AddYears(2),
                        chkRemember.Checked, userData, FormsAuthentication.FormsCookiePath);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);
                    Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUsuario.Text, chkRemember.Checked));
                }
                else if (datos[0] == "NO")
                {
                    Response.Write("<script language=javascript>alert('Contraseña incorrecta')</script>");

                }
                else
                {
                    Response.Write("<script language=javascript>alert('No existe el usuario " + txtUsuario.Text + "')</script>");
                }
            }
        }
    }
}