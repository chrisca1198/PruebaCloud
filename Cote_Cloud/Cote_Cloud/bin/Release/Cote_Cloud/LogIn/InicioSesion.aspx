<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="InicioSesion.aspx.cs" Inherits="Cote_Cloud.LogIn.InicioSesion" %>

<%@ Register Src="~/User/recuperacion.ascx" TagPrefix="uc1" TagName="recuperacion" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="icon" type="image/png" sizes="16x16" href="../images/cote/logo/Escudo.png" />
    <title>Inicio de Sesion</title>
    
    <link href="../css/style.css" rel="stylesheet" />
    <link href="../css/helper.css" rel="stylesheet" />
    <link href="../css/lib/bootstrap/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="fix-header fix-sidebar mini-sidebar">
    <div class="preloader" style="display: none;">
        <svg class="circular" viewBox="25 25 50 50">
            <circle class="path" cx="50" cy="50" r="20" fill="none" stroke-width="2" stroke-miterlimit="10"></circle>
        </svg>
    </div>
    <div class="container">
        <div class="row">
            <div class="col"></div>
            <div class="col border mt-5 p-5 ">
                <h4 class="text-center">Iniciar Sesion</h4>
                <form id="form1" class="" runat="server">
                    <uc1:recuperacion runat="server" id="recuperacion" Visible="false"/>
                    <div class="form-group ">
                        <label>Nombre de usuario</label>
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Usuario"></asp:TextBox>
                    </div>
                    <div class="form-group ">
                        <label>Contraseña</label>
                        <input type="password" class="form-control" placeholder="Contraseña" runat="server" id="txtContra" />
                    </div>
                    <div class="checkbox ">
                        <label>
                            <asp:CheckBox ID="chkRemember" runat="server" />
                            Recuerdame
                        </label>
                        <label class="pull-right">
                            <a runat="server" onserverclick="Recuperacion_Click" href="#">¿Haz olvidado tu contraseña?</a>
                        </label>
                    </div>
                    <asp:Button ID="btnLogIn" runat="server" Text="Iniciar" CssClass="btn btn-primary btn-flat m-b-30 m-t-30" OnClick="btnLogIn_Click" />
                </form>
            </div>
            <div class="col"></div>

        </div>
    </div>
</body>
</html>
