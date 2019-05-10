<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="password.ascx.cs" Inherits="Cote_Cloud.User.password" %>
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
                        <h5 class="modal-title">
                            <asp:Label ID="lblEnca" runat="server" Text=" "></asp:Label></h5>
                    </div>
                    <div class="modal-body">
                        <div class="">
                            <div class="form-group">
                                <label for="oldPass">
                                    Contraseña antigua:
                                </label>
                                <asp:TextBox ID="txtContraAnti" placeholder="Antigua" runat="server" CssClass="form-control input-validation-error" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="newPass">
                                    New Password
                                </label>
                                <asp:TextBox ID="txtContra" runat="server" placeholder="Nueva" CssClass="form-control input-validation-error" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="confirmPass">
                                    Confirm Password
                                </label>
                                <asp:TextBox ID="txtContraConf" runat="server" placeholder="Repita la nueva" CssClass="form-control input-validation-error" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnCambios" runat="server" Text="Guardar cambio" CssClass="btn btn-primary" OnClick="btnCambios_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-secondary" OnClick="btnCancelar_Click" />
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>
