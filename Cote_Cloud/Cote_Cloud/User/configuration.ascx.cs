using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cote_Cloud.User
{
    public partial class configuration : System.Web.UI.UserControl
    {
        static string ced = "",user="";
        transacciones.clLogIn llamada = new transacciones.clLogIn();
        estudiantes.estudiantes1 estu = new estudiantes.estudiantes1();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnCambios_Click(object sender, EventArgs e)
        {
            if (txtPhone.Text == "" || txtNomUser.Text == "" || txtEmail.Text == "")
                Response.Write("<script language=javascript>alert('Los tres campos deben de estar llenos y una foto seleccionada') </script>");
            else
            { string number = txtPhone.Text;
                if (number.Length > 8)
                    Response.Write("<script language=javascript>alert('El teléfono debe de tener un máximo de 8 dígitos') </script>");
                else
                {
                    string usuario = txtNomUser.Text;
                    if (usuario.Length > 30)
                    {
                        Response.Write("<script language=javascript>alert('El nombre de usuario debe de tener un máximo de 30 dígitos') </script>");
                    }
                    else
                    {
                        byte[] fotoUsuario = llamada.getFotoUser(user);
                        if (!(foto.FileBytes.Length == 0))
                            fotoUsuario = foto.FileBytes;
                        if (foto.FileBytes.Length == 0 && fotoUsuario.Length == 0)
                        {
                            fotoUsuario = llamada.getFotoUser("AdministracionCote");
                        }
                        if (user == txtNomUser.Text)
                        {
                            llamada.modifyDataUser(txtNomUser.Text, user, txtEmail.Text, fotoUsuario, ced, txtPhone.Text);
                            Response.Write("<script language=javascript>alert('Se modificó correctamente el usuario, recargue la página') </script>");
                            this.Visible = false;
                        }
                        else
                        {
                            if (llamada.ExistUser(txtNomUser.Text))
                            {
                                Response.Write("<script language=javascript>alert('El nombre de usuario ingresado existe. Por favor ingrese otro que no haya sido usado') </script>");
                                txtNomUser.Text = user;
                            }
                            else
                            {
                                llamada.modifyDataUser(txtNomUser.Text, user, txtEmail.Text, fotoUsuario, ced, txtPhone.Text);
                                Response.Write("<script language=javascript>alert('Se modificó correctamente el usuario, recargue la página') </script>");
                                this.Visible = false;
                            }
                        }
                    }
                }
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
        public void llenar(string id, string users, string email, string cel)
        {
            user = users;
            ced = id;
            txtNomUser.Text = users;
            txtEmail.Text = email;
            txtPhone.Text = cel;
            this.Visible = true;
        }
        protected void btnFoto_Click(object sender, EventArgs e)
        {
            pnlFoto.Visible = true;
        }
    }
}