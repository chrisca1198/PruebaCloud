<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="recuperacion.ascx.cs" Inherits="Cote_Cloud.User.recuperacion" %>
<link href="../css/bootstrap.min.css" rel="stylesheet" />
<link href="../css/helper.css" rel="stylesheet" />
<link href="../css/style.css" rel="stylesheet" />
<link href="../css/sweetalert.css" rel="stylesheet" />
<div class="sweet-overlay" tabindex="-1" style="opacity: 1.04; display: block;">
    <div class="modal show" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" id="changePassModal" style="display: block;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <form>
                    <div class="modal-header">
                        <h5 class="modal-title">Recuperación</h5>
                    </div>
                    <div class="modal-body">
                        <div class="">
                            <div class="form-group">
                                <label for="oldPass">
                                    Nombre de usuario:
                                </label>
                                <asp:TextBox ID="txtNombre" placeholder="Nombre" runat="server" CssClass="form-control input-validation-error"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="newPass">
                                    Correo del usuario:
                                </label>
                                <asp:TextBox ID="txtEmail" runat="server" placeholder="Correo" TextMode="Email" CssClass="form-control input-validation-error"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnCambios" runat="server" Text="Recuperar" CssClass="btn btn-primary" OnClick="btnCambios_Click"/>
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-secondary" OnClick="btnCancelar_Click"/>
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>
