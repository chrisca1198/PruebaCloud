using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security.Principal;

namespace Cote_Cloud.administracion
{
    public partial class admin : System.Web.UI.MasterPage
    {
        IIdentity id;
        IPrincipal principal;
        static string user = "";
        transacciones.clLogIn llamada = new transacciones.clLogIn();
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies["InicioSesion"];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                user = authTicket.Name;
                string[] data = authTicket.UserData.Split(new char[] { '|' });
                if (data[0] == "admin")
                {
                    if (llamada.LogCorrect(data[1], data[2]))
                    {
                        id = new FormsIdentity(authTicket);
                        principal = new GenericPrincipal(id, data);
                        Context.User = principal;
                    }
                    else
                    {
                        LogOut();
                    }
                }
                else
                {
                    Response.Redirect("~/LogIn/InicioSesion.aspx");
                }
            }
            else
            {
                Response.Redirect("~/LogIn/InicioSesion.aspx");
            }
        }
        protected void btn_Cerrar(object sender, EventArgs e)
        {
            LogOut();
        }
        private void LogOut()
        {
            HttpCookie authCookie = Request.Cookies["InicioSesion"];
            authCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(authCookie);
            FormsAuthentication.SignOut();
            Response.Redirect("~/LogIn/InicioSesion.aspx");
        }
    }
}