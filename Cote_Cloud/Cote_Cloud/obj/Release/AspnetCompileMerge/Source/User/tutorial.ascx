<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tutorial.ascx.cs" Inherits="Cote_Cloud.User.tutorial" %>
<link href="../Registros/bootstrap.css" rel="stylesheet" />
<link href="../Registros/sweetalert.css" rel="stylesheet" />
<div class="sweet-overlay" tabindex="-1" style="opacity: 1.04; display: block;">
    <div class="modal-dialog modal-lg" role="banner">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLongTitle">Instrucciones para llenar formulario</h5>
                <button type="button" class="close" id="btnCerrar" onserverclick="btnCerrar_Click" runat="server">&times</button>
            </div>
            <div class="modal-body">
                <div class="container">
                    <video src="http://www.cotepecos.co.cr/Files/VideoTutorial.mp4" controls="controls" style="width:100%;height:100%;"/>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal" runat="server" onserverclick="btnCerrar_Click">Cerrar</button>
            </div>
        </div>
    </div>
</div>