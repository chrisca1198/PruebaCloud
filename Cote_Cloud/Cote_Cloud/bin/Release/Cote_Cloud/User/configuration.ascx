<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="configuration.ascx.cs" Inherits="Cote_Cloud.User.configuration" %>
<%@ Register Src="~/User/completado.ascx" TagPrefix="uc1" TagName="completado" %>


<link href="../css/style.css" rel="stylesheet" />
<link href="../css/bootstrap.min.css" rel="stylesheet" />
<link href="../css/helper.css" rel="stylesheet" />
<link href="../css/sweetalert.css" rel="stylesheet" />
<uc1:completado runat="server" ID="completado" Visible="false"/>
<div class="sweet-overlay" tabindex="-1" style="opacity: 1.04; display: block;">
    <div class="modal show" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" id="changePassModal" style="display: block;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <form>
                    <div class="modal-header">
                        <h5 class="modal-title">
                            <asp:Label ID="lblEnca" runat="server" Text="Configuraciones del estudiante"></asp:Label></h5>
                    </div>
                    <div class="modal-body">
                        <div class="">
                            <div class="form-group">
                                <label>
                                    Nombre de usuario:
                                </label>
                                <asp:TextBox ID="txtNomUser" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    Correo:
                                </label>
                                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control form-control-line"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    Número de teléfono:
                                </label>
                                <asp:TextBox ID="txtPhone" TextMode="Number" runat="server" CssClass="form-control form-control-line"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    Cambiar foto de perfil
                                </label>
                                <asp:Button ID="btnFoto" runat="server" Text="Cambiar" CssClass="btn btn-info m-b-10 m-l-5" OnClick="btnFoto_Click" />
                                <hr />
                                <asp:Panel ID="pnlFoto" runat="server" Visible="false">
                                    <h5>Seleccione la imagen</h5>
                                    <asp:FileUpload ID="foto" runat="server" CssClass="btn btn-warning btn-rounded m-b-10 m-l-5" />
                                </asp:Panel>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnCambios" runat="server" Text="Guardar cambio" CssClass="btn btn-success" OnClick="btnCambios_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-secondary" OnClick="btnCancelar_Click" />
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>
