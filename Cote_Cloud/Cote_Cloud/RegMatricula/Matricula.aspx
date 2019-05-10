<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Matricula.aspx.cs" Inherits="Cote_Cloud.RegMatricula.Matricula" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Src="~/User/error.ascx" TagPrefix="uc1" TagName="error" %>
<%@ Register Src="~/User/informativo.ascx" TagPrefix="uc1" TagName="informativo" %>
<%@ Register Src="~/User/matricula.ascx" TagPrefix="uc1" TagName="matricula" %>
<%@ Register Src="~/User/tutorial.ascx" TagPrefix="uc1" TagName="tutorial" %>
<%@ Register Src="~/User/completado.ascx" TagPrefix="uc1" TagName="completado" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Matricula</title>
    <link href="~/RegPreMatricula/bootstrap.css" rel="stylesheet" />
    <link href="~/RegPreMatricula/sweetalert.css" rel="stylesheet" />
    <link rel="icon" type="image/png" sizes="16x16" href="~/images/escudo.ico" />
</head>
<body>
    <section class="container-fluid">
        <form itemid="form1" id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="update" runat="server">
                <ContentTemplate>
                    <uc1:completado runat="server" ID="completado" Visible="false"/>
                    <uc1:error runat="server" ID="error" Visible="false" />
                    <uc1:informativo runat="server" ID="importante" Visible="false" />
                    <uc1:matricula runat="server" ID="Matriculas" Visible="false" />
                    <br />
                    <h1 class="text-center">Formulario de matrícula</h1>
                    <p class="text-center">Compruebe que los datos ingresados en la pre matrícula son correctos.</p>
                    <hr />
                    <div class="container">
                        <div class="row">
                            <div class="col-6 col-12-xsmall border ">
                                <br />
                                Póliza
                                <asp:TextBox ID="txtPoliza" MaxLength="50" CssClass="form-control" placeholder="*Póliza estudiantil" runat="server"></asp:TextBox><br />
                            </div>
                            <div class="col-6 col-12-xsmall border ">
                                <br />
                                Cuota
                                <input  id="txtCuota" placeholder="*Cuota" pattern="[0-9]{0-7}" class="form-control" runat="server" /><br />
                            </div>
                        </div>
                        <br />
                        <div class="col-lg-auto">
                            <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-success" Text="Matricular" OnClick="btnAgregar_Click" />&nbsp
                   <asp:Button ID="btnFormatear" runat="server" Text="Vaciar" CssClass="btn btn-dark" OnClick="btnFormatear_Click" />
                        </div>
                        <hr />
                    </div>
                    <div class="container">
                        <asp:Panel ID="pnlEstudiante" runat="server">
                            <h2>Datos personales del estudiante ingresados en la pre matrícula</h2>
                            <p>Todos los campos son requeridos a excepción del teléfono de residencia.</p>
                            <div class="row">
                                <div class="col-md-6 border">
                                    <br />
                                    Nombre completo:
                                    <asp:TextBox ID="txtNom" CssClass="form-control" placeholder="*Nombre" runat="server" Enabled="false"></asp:TextBox><br />
                                    Cédula:
                                    <input  id="txtCedula" placeholder="*Cédula" pattern="[0-9]{1,15}" class="form-control" runat="server" disabled="disabled" /><br />
                                    Edad:                                   
                                    <asp:TextBox ID="txtEdad" CssClass="form-control" placeholder="Edad" runat="server" Enabled="false" TextMode="Number"></asp:TextBox><br />
                                    Télefono:
                                    <input  id="txtTelefono" placeholder="*Teléfono" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                    Correo electrónico:
                                    <asp:TextBox ID="txtEmail" MaxLength="50" CssClass="form-control" placeholder="*Correo" runat="server" TextMode="Email"></asp:TextBox><br />
                                </div>
                                <div class="col-md-6 border">
                                    <br />
                                    Dirección exacta:
                                    <asp:TextBox ID="txtdir" CssClass="form-control" placeholder="*Dirección" runat="server" MaxLength="500"></asp:TextBox><br />
                                    Teléfono residencia en caso de tenerlo:
                                    <input  id="txtTelResi" placeholder="Teléfono de residencia" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                    Provincia:
                                    <asp:TextBox ID="txtProvincia" MaxLength="50" CssClass="form-control" placeholder="*Provincia de residencia" runat="server"></asp:TextBox><br />
                                    Cantón:
                                    <asp:TextBox ID="txtCanton" MaxLength="50" CssClass="form-control" placeholder="*Cantón de la residencia" runat="server"></asp:TextBox><br />
                                    Distrito:
                                    <asp:TextBox ID="txtDistrito" MaxLength="50" CssClass="form-control" placeholder="*Distrito de residencia" runat="server"></asp:TextBox><br />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <hr />
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
                                    <div class="col-md-6 border">
                                        <br />
                                        Nombre completo:
                                        <asp:TextBox ID="txtNomE" placeholder="*Nombre completo del encargado" MaxLength="200" CssClass="form-control" runat="server"></asp:TextBox><br />
                                        Cédula:
                                        <input  id="txtCedulaE" placeholder="*Cédula del encargado" pattern="[0-9]{1,15}" class="form-control" runat="server" /><br />
                                        Teléfono:
                                        <input  id="txtTelE" placeholder="*Teléfono de Encargado" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                    </div>
                                    <div class="col-md-6 border">
                                        <br />
                                        Ingreso mensual:
                                        <input  id="txtIngresoE" placeholder="Ingreso mensual del encargado" pattern="[0-9]{1,15}" class="form-control" runat="server" /><br />
                                        Ocupación:
                                        <asp:TextBox ID="txtOcupaE" placeholder="Ocupación del encargado" CssClass="form-control" MaxLength="200" runat="server"></asp:TextBox><br />
                                        Teléfono trabajo:
                                        <input  id="txtTelTrabajoE" placeholder="Teléfono del trabajo del encargado" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                    </div>
                                </div>
                            </asp:View>
                            <asp:View runat="server">
                                <h4 class="text-center">Datos del padre</h4>
                                <div class="row">
                                    <div class="col-md-6 border">
                                        <br />
                                        Nombre completo:
                                        <asp:TextBox ID="txtNomP" placeholder="*Nombre completo de la padre" MaxLength="200" CssClass="form-control" runat="server"></asp:TextBox><br />
                                        Cédula:
                                        <input  id="txtCedulaP" placeholder="*Cédula del padre" pattern="[0-9]{1,15}" class="form-control" runat="server" /><br />
                                        Teléfono:
                                        <input  id="txtTelP" placeholder="*Teléfono de padre" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                    </div>
                                    <div class="col-md-6 border">
                                        <br />
                                        Ingreso mensual:
                                        <input  id="txtIngresoP" placeholder="Ingreso mensual del padre" pattern="[0-9]{1,15}" class="form-control" runat="server" /><br />
                                        Ocupación:
                                        <asp:TextBox ID="txtOcupaP" placeholder="Ocupación del padre" MaxLength="200" CssClass="form-control" runat="server"></asp:TextBox><br />
                                        Teléfono de trabajo:
                                        <input  id="txtTelTrabajoP" placeholder="Teléfono del trabajo del padre" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                    </div>
                                </div>
                            </asp:View>
                            <asp:View runat="server">
                                <h4 class="text-center">Datos de la madre</h4>
                                <div class="row">
                                    <div class="col-md-6 border">
                                        <br />
                                        Nombre completo:
                                        <asp:TextBox ID="txtnomM" placeholder="*Nombre completo de la madre" MaxLength="200" CssClass="form-control" runat="server"></asp:TextBox><br />
                                        Cédula:
                                        <input  id="txtCedulaM" placeholder="*Cédula" pattern="[0-9]{1,15}" class="form-control" runat="server" /><br />
                                        Teléfono:
                                        <input  id="txtTelM" placeholder="*Teléfono de madre" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                    </div>
                                    <div class="col-md-6 border">
                                        <br />
                                        Ingreso mensual:
                                        <input  id="txtIngresoM" placeholder="Ingreso mensual de la madre" pattern="[0-9]{1,15}" class="form-control" runat="server" /><br />
                                        Ocupación:
                                        <asp:TextBox ID="txtOcupaM" placeholder="Ocupación de la madre" MaxLength="200" CssClass="form-control" runat="server"></asp:TextBox><br />
                                        Teléfono de trabajo:
                                        <input  id="txtTelTrabajoM" placeholder="Teléfono del trabajo de la madre" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                        <hr />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
    </section>
    <script src="../RegPreMatricula/bootstrap.js"></script>
    <script src="../RegPreMatricula/popper.js"></script>
    <script src="../RegPreMatricula/jquery-3.2.1.js"></script>
</body>
</html>
