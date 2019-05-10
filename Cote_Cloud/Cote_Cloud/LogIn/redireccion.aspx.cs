using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cote_Cloud.LogIn
{
    public partial class redireccion : System.Web.UI.Page
    {
        transacciones.clLogIn llamada = new transacciones.clLogIn();
        transacciones.administracion admin = new transacciones.administracion();
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies["InicioSesion"];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                string[] data = authTicket.UserData.Split(new char[] { '|' });
                if (llamada.LogCorrect(data[1], data[2]))
                {
                    if (data[0] == "estudiante")
                    {
                        Response.Redirect("~/estudiantes/estudiantes.aspx");
                    }
                    else if (data[0] == "admin")
                    {
                        if (!admin.existProcess(DateTime.Today.Year.ToString()))
                        {
                            admin.createProcessAdmission(DateTime.Today.Year.ToString());
                        }
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
                    Response.Redirect("~/LogIn/InicioSesion.aspx");
                }
            }
            else
            {
                Response.Redirect("InicioSesion.aspx");
            }
        }
    }
}