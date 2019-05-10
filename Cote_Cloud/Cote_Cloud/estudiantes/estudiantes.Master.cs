using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.UI.HtmlControls;

namespace Cote_Cloud.estudiantes
{
    public partial class estudiantes : System.Web.UI.MasterPage
    {
        IIdentity id;
        IPrincipal principal;
        static string user = "",ced="",email=""; 
        transacciones.clLogIn llamada = new transacciones.clLogIn();
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies["InicioSesion"];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                string[] data = authTicket.UserData.Split(new char[] { '|' });
                if (data[0] == "estudiante")
                {
                    if (llamada.LogCorrect(data[1], data[2]))
                    {
                        id = new FormsIdentity(authTicket);
                        principal = new GenericPrincipal(id, data);
                        Context.User = principal;
                        user = data[1];
                        string[] datos = llamada.getUser(user);
                        ced = datos[1];
                        email = datos[5];
                        NombreUsuario.InnerText = user;
                        byte[] array = llamada.getFotoUser(user);
                        if (array == null) { fotoPerfil.Src = "images/bookingSystem/perfil.png"; fotoPerfil2.Src = "images/bookingSystem/perfil.png"; }
                        else
                        {
                            var base64 = Convert.ToBase64String(array);
                            var imgSrc = String.Format("data:image/jpg;base64," + base64);

                            fotoPerfil.Src = imgSrc;
                            fotoPerfil2.Src = imgSrc;
                        }
                        if (Convert.ToBoolean(datos[3]) == false)
                        {
                            password.llenar(true, user);
                        }
                    }
                    else
                    {
                        Response.Write("<script language=javascript>alert('Ocurrió un error de autenticación, vuelva a iniciar sesión')</script>");
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
        private void LogOut()
        {
            HttpCookie authCookie = Request.Cookies["InicioSesion"];
            authCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(authCookie);
            FormsAuthentication.SignOut();
            Response.Redirect("~/LogIn/InicioSesion.aspx");
        }
        protected void btn_Cerrar(object sender, EventArgs e)
        {
            LogOut();
        }
        protected void btn_Conf(object sender, EventArgs e)
        {
            configuration.llenar(ced,user,email,llamada.getNumberUser(ced));
        }
        protected void btn_Pass(object sender, EventArgs e)
        {
            password.llenar(false, user);
        }
    }
}