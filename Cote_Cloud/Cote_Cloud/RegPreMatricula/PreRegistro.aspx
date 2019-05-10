<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreRegistro.aspx.cs" Inherits="Cote_Cloud.RegPreMatricula.PreRegistro" %>

<%@ OutputCache Location="None" Duration="1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Src="~/User/error.ascx" TagPrefix="uc1" TagName="error" %>
<%@ Register Src="~/User/informativo.ascx" TagPrefix="uc1" TagName="informativo" %>
<%@ Register Src="~/User/prematricula.ascx" TagPrefix="uc1" TagName="prematricula" %>
<%@ Register Src="~/User/tutorial.ascx" TagPrefix="uc1" TagName="tutorial" %>
<%@ Register Src="~/User/completado.ascx" TagPrefix="uc1" TagName="completado" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />    
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>PreRegistro</title>
    <link href="bootstrap.css" rel="stylesheet" />
    <link href="sweetalert.css" rel="stylesheet" />
    <link rel="icon" type="image/png" sizes="16x16" href="../images/escudo.ico" />
</head>
<body>
    <section class="container-fluid">
        <form itemid="form1" id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <uc1:error runat="server" ID="error" Visible="false" />
            <uc1:informativo runat="server" ID="importante" Visible="false" />
            <uc1:prematricula runat="server" ID="prematricula" Visible="false" />
            <uc1:tutorial runat="server" ID="tutorial" Visible="false" />
            <br />
            <asp:Panel runat="server" ID="pnlAll">

                <div class="row">
                    <div class="col-10">
                        <h1 class="text-center">Pre-Registro</h1>
                    </div>
                    <div class="col-2">
                        <asp:Button ID="btnHelp" CssClass="btn btn-secondary" runat="server" Text="Ayuda" OnClick="btnHelp_Click" />
                    </div>
                </div>
                <hr />
                <div class="container">
                    <asp:Panel ID="pnlEstudiante" runat="server">
                        <h2>Datos personales del estudiante</h2>
                        <p>*Campos son requeridos</p>
                        <div class="row">
                            <div class="col-6 col-12-xsmall border ">
                                <br />
                                <asp:TextBox ID="txtNom" CssClass="form-control" placeholder="*Nombre" runat="server" MaxLength="50"></asp:TextBox><br />
                                <asp:TextBox ID="txtApe1" CssClass="form-control" placeholder="*Apellidos" runat="server" MaxLength="50"></asp:TextBox><br />
                                <input type="tel" id="txtCedula" placeholder="*Cédula" pattern="[0-9]{1,15}" class="form-control" runat="server" /><br />
                                *Fecha de nacimiento:<input type="date" id="txtFechaNacimiento" placeholder="Fecha de nacimiento"
                                    max="2099-12-31" min="1997-12-31" class="form-control" runat="server" /><br />
                                <input type="tel" id="txtTelefono" placeholder="*Teléfono" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                <asp:TextBox ID="txtPin" CssClass="form-control" placeholder="*Código-Pin" runat="server"></asp:TextBox><br />
                                <asp:TextBox ID="txtdir" CssClass="form-control" placeholder="*Dirección" runat="server" MaxLength="500"></asp:TextBox><br />
                                <asp:TextBox ID="txtProvincia" MaxLength="50" CssClass="form-control" placeholder="*Provincia de residencia" runat="server"></asp:TextBox><br />
                                <asp:TextBox ID="txtCanton" MaxLength="50" CssClass="form-control" placeholder="*Cantón de la residencia" runat="server"></asp:TextBox><br />
                                <asp:TextBox ID="txtDistrito" MaxLength="50" CssClass="form-control" placeholder="*Distrito de residencia" runat="server"></asp:TextBox><br />
                            </div>
                            <div class="col-6 col-12-xsmall border ">
                                <br />
                                <input type="tel" id="txtTelResi" placeholder="Teléfono de residencia" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                <asp:TextBox ID="txtEmail" MaxLength="50" CssClass="form-control" placeholder="*Correo" runat="server" TextMode="Email"></asp:TextBox><br />
                                <input type="number" id="txtSobre" placeholder="*Número consecutivo" min="1" max="999" class="form-control" runat="server" /><br />
                                <asp:TextBox ID="txtColegio" CssClass="form-control" placeholder="*Colegio procedencia" runat="server" MaxLength="100"></asp:TextBox><hr />
                                Primera opcion:
                                    <asp:DropDownList ID="cboOp1" CssClass="form-control-sm" runat="server">
                                        <asp:ListItem Value="Seleccione" Text="Seleccione">*Seleccione la primera opción</asp:ListItem>
                                    </asp:DropDownList><hr />
                                Segunda opción:&nbsp<asp:CheckBox ID="chkOpc2" runat="server" OnCheckedChanged="chkOpc2_CheckedChanged" AutoPostBack="True" />
                                <asp:DropDownList ID="cboOp2" CssClass="form-control-sm" runat="server" Visible="False">
                                    <asp:ListItem Value="Seleccione" Text="Seleccione">Segunda opción</asp:ListItem>
                                </asp:DropDownList><hr />
                                <asp:TextBox ID="txtNac" CssClass="form-control" placeholder="*Nacionalidad" runat="server"></asp:TextBox><br />
                                <input type="number" id="txtEdad" placeholder="*Edad" min="1" max="100" class="form-control" runat="server" /><br />
                                Sexo:
                                <br />
                                <div class="row">
                                    <div class="col-6">
                                        Masculino: &nbsp<input type="radio" id="rbMascul" runat="server" name="sexo" value="Masculino" />
                                    </div>
                                    <div class="col-6">
                                        Femenino: &nbsp<input type="radio" id="rbFeme" runat="server" name="sexo" value="Femenino" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <hr />
                <asp:UpdatePanel ID="update" runat="server">
                    <ContentTemplate>
                        <div class="container">
                            <h2>Datos personales de los padres o encargado</h2>
                            <p>*Al menos alguno de los datos personales de los padres o encargado deben estar llenos*</p>

                            <div class="col-lg-auto">
                                <asp:Button ID="btnTabEncargado" runat="server" CssClass="btn btn-light" Text="Datos del encargado" OnClick="btnTabEncargado_Click" />&nbsp
                    <asp:Button ID="btnTabPadre" runat="server" CssClass="btn btn-light" Text="Datos del padre" OnClick="btnTabPadre_Click" />&nbsp
                    <asp:Button ID="btnTabMadre" runat="server" CssClass="btn btn-light" Text="Datos de la madre" OnClick="btnTabMadre_Click" />&nbsp
                            </div>
                            <br />
                            <asp:MultiView ID="tabs" runat="server" ActiveViewIndex="0">
                                <asp:View runat="server">
                                    <h4 class="text-center">Datos del encargado</h4>
                                    <div class="row">
                                        <div class="col-6 col-12-xsmall border ">
                                            <br />
                                            <asp:TextBox ID="txtNomE" placeholder="*Nombre completo del encargado" MaxLength="200" CssClass="form-control" runat="server"></asp:TextBox><br />
                                            <input type="tel" id="txtCedulaE" placeholder="*Cédula del encargado" pattern="[0-9]{1,15}" class="form-control" runat="server" /><br />
                                            <input type="tel" id="txtTelE" placeholder="*Teléfono de Encargado" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                        </div>
                                        <div class="col-6 col-12-xsmall border">
                                            <br />
                                            <input type="tel" id="txtIngresoE" placeholder="Ingreso mensual del encargado" pattern="[0-9]{1,15}" class="form-control" runat="server" /><br />
                                            <asp:TextBox ID="txtOcupaE" placeholder="Ocupación del encargado" CssClass="form-control" MaxLength="200" runat="server"></asp:TextBox><br />
                                            <input type="tel" id="txtTelTrabajoE" placeholder="Teléfono del trabajo del encargado" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View runat="server">
                                    <h4 class="text-center">Datos del padre</h4>
                                    <div class="row">
                                        <div class="col-6 col-12-xsmall border">
                                            <br />
                                            <asp:TextBox ID="txtNomP" placeholder="*Nombre completo de la padre" MaxLength="200" CssClass="form-control" runat="server"></asp:TextBox><br />
                                            <input type="tel" id="txtCedulaP" placeholder="*Cédula del padre" pattern="[0-9]{1,15}" class="form-control" runat="server" /><br />
                                            <input type="tel" id="txtTelP" placeholder="*Teléfono de padre" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                        </div>
                                        <div class="col-6 col-12-xsmall border">
                                            <br />
                                            <input type="tel" id="txtIngresoP" placeholder="Ingreso mensual del padre" pattern="[0-9]{1,15}" class="form-control" runat="server" /><br />
                                            <asp:TextBox ID="txtOcupaP" placeholder="Ocupación del padre" MaxLength="200" CssClass="form-control" runat="server"></asp:TextBox><br />
                                            <input type="tel" id="txtTelTrabajoP" placeholder="Teléfono del trabajo del padre" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View runat="server">
                                    <h4 class="text-center">Datos de la madre</h4>
                                    <div class="row">
                                        <div class="col-6 col-12-xsmall border">
                                            <br />
                                            <asp:TextBox ID="txtnomM" placeholder="*Nombre completo de la madre" MaxLength="200" CssClass="form-control" runat="server"></asp:TextBox><br />
                                            <input type="tel" id="txtCedulaM" placeholder="*Cédula" pattern="[0-9]{1,15}" class="form-control" runat="server" /><br />
                                            <input type="tel" id="txtTelM" placeholder="*Teléfono de madre" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                        </div>
                                        <div class="col-6 col-12-xsmall border">
                                            <br />
                                            <input type="tel" id="txtIngresoM" placeholder="Ingreso mensual de la madre" pattern="[0-9]{1,15}" class="form-control" runat="server" /><br />
                                            <asp:TextBox ID="txtOcupaM" placeholder="Ocupación de la madre" MaxLength="200" CssClass="form-control" runat="server"></asp:TextBox><br />
                                            <input type="tel" id="txtTelTrabajoM" placeholder="Teléfono del trabajo de la madre" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                        </div>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                            <hr />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            <div class="container">
                <div class="col-lg-auto">
                    <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-success" Text="Registrarse" OnClick="btnAgregar_Click" />&nbsp
                   <asp:Button ID="btnFormatear" runat="server" Text="Vaciar" CssClass="btn btn-dark" OnClick="btnFormatear_Click" />
                </div>
                <hr />
            </div>
            <asp:Panel runat="server" ID="pnlReport" Visible="false">
                <div class="sweet-overlay" tabindex="-1" style="opacity: 1.04; display: block;">
                    <div class="sweet-alert showSweetAlert visible" data-animation="pop" data-timer="null" style="display: block; margin-top: -169px;">
                        <div class="sa-icon sa-error" style="display: none;">
                            <span class="sa-x-mark">
                                <span class="sa-line sa-left"></span>
                                <span class="sa-line sa-right"></span>
                            </span>
                        </div>
                        <div class="sa-icon sa-warning" style="display: none;">
                            <span class="sa-body"></span>
                            <span class="sa-dot"></span>
                        </div>
                        <div class="sa-icon sa-info" style="display: none;"></div>
                        <div class="sa-icon sa-success animate" style="display: block;">
                            <span class="sa-line sa-tip animateSuccessTip"></span>
                            <span class="sa-line sa-long animateSuccessLong"></span>

                            <div class="sa-placeholder"></div>
                            <div class="sa-fix"></div>
                        </div>
                        <div class="sa-icon sa-custom" style="display: none;"></div>
                        <h2>Completado</h2>
                        <p style="display: block;">
                            <asp:Label ID="lblMensaje" runat="server" Text="lbl"></asp:Label>
                        </p>
                        <fieldset>
                            <input type="text" tabindex="3" placeholder="">
                            <div class="sa-input-error"></div>
                        </fieldset>
                        <div class="sa-error-container">
                            <div class="icon">!</div>
                            <p>Not valid!</p>
                        </div>
                        <div class="sa-button-container">
                            <div class="row">
                                <div class="col-6">
                                    <button class="confirm" runat="server" onserverclick="btnOK_Click" tabindex="1" style="display: inline-block; background-color: rgb(46, 133, 40); box-shadow: rgba(32, 120, 25, 0.53) 0px 0px 2px, rgba(0, 0, 0, 0.05) 0px 0px 0px 1px inset;">
                                        Entendido, cerrar ventana</button>
                                </div>
                                <div class="col-6">
                                    <button class="confirm" runat="server" onserverclick="btnReporte_Click" tabindex="1" style="display: inline-block; background-color: rgb(221, 107, 85); box-shadow: rgba(221, 107, 85, 0.8) 0px 0px 2px, rgba(0, 0, 0, 0.05) 0px 0px 0px 1px inset;">
                                        Descargar formulario</button>
                                </div>
                            </div>
                            <div class="la-ball-fall">
                                <div></div>
                                <div></div>
                                <div></div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel runat="server" ID="pnlReporte" Visible="false">
                <rsweb:ReportViewer ID="report" runat="server"></rsweb:ReportViewer>
            </asp:Panel>
        </form>
    </section>
    <script src="bootstrap.js"></script>
    <script src="popper.js"></script>
    <script src="jquery-3.2.1.js"></script>
</body>
</html>
