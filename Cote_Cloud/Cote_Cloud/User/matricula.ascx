<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="matricula.ascx.cs" Inherits="Cote_Cloud.User.matricula" %>
<link href="../css/helper.css" rel="stylesheet"/>
<link href="../css/style.css" rel="stylesheet"/>
<link href="../css/bootstrap.min.css" rel="stylesheet"/>
<link href="../css/sweetalert.css" rel="stylesheet" />
<div class="sweet-overlay" tabindex="-1" style="opacity: 1; display: block; background: #ffffff">
    <div class="error-page" id="wrapper">
        <div class="error-box">
            <div class="error-body text-center">
                <h1>No disponible</h1>
                <h3 class="text-uppercase" id="encabezado" runat="server"></h3>
                <p class="text-muted m-t-30 m-b-30" id="mensaje" runat="server"></p>
                <a class="btn btn-info btn-rounded waves-effect waves-light m-b-40" href="http://www.cotepecos.co.cr">Volver a página de Cotepecos</a>
            </div>
            <footer class="footer text-center">© 2018 Informatica Gen18.</footer>
        </div>
    </div>
</div>