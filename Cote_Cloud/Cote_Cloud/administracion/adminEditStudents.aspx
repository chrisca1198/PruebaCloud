<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminEditStudents.aspx.cs" Inherits="Cote_Cloud.administracion.adminEditStudents" %>
<%@ Register Src="~/User/error.ascx" TagPrefix="uc1" TagName="error" %>
<%@ Register Src="~/User/informativo.ascx" TagPrefix="uc1" TagName="informativo" %>
<%@ Register Src="~/User/completado.ascx" TagPrefix="uc1" TagName="completado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="icon" type="image/png" sizes="16x16" href="../images/escudo.ico" />
    <link href="../css/lib/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/helper.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <link href="../css/sweetalert.css" rel="stylesheet" />
    <title>Edición estudiantes pre matrícula</title>
</head>
<body>
    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <form id="form1" runat="server">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdatePanel ID="update" runat="server">
                                <ContentTemplate>
                                    <uc1:completado runat="server" ID="completado" Visible="false" />
                                    <uc1:error runat="server" ID="error" Visible="false" />
                                    <uc1:informativo runat="server" ID="importante" Visible="false" />
                                    <div class="row">
                                        <div class="col-12">
                                            <h1 class="text-center">Editar estudiantes</h1>
                                            <p>Seleccione el estudiante que desee editar</p>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="table-responsive">
                                        <div class="dataTables_wrapper">
                                            <div id="example23_filter" class="dataTables_filter">
                                                <label>
                                                    Buscar:<asp:TextBox ID="txtNomEstudi" runat="server" OnTextChanged="txtNomEstudi_TextChanged"
                                                        AutoPostBack="True"></asp:TextBox></label>
                                            </div>
                                            <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns="False"
                                                DataKeyNames="cedula" OnPageIndexChanging="gvStudents_PageIndexChanging" CssClass="table table-bordered" AllowPaging="True" AllowSorting="True" PageSize="10" OnRowCommand="gvStudents_RowCommand" PagerStyle-BorderColor="#cccccc" PagerStyle-BorderStyle="Solid" PagerStyle-BorderWidth="0.5px" PagerStyle-Font-Size="Large">
                                                <EmptyDataTemplate>
                                                    <div class="alert alert-secondary">
                                                        <h4 class="alert-heading">Sin registro de estudiantes</h4>
                                                    </div>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField DataField="cedula" HeaderText="Cedula" ReadOnly="true" />
                                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" ReadOnly="true" />
                                                    <asp:BoundField DataField="apellidos" HeaderText="Apellidos" ReadOnly="true" />
                                                    <asp:ButtonField ControlStyle-CssClass="btn btn-info btn-outline m-b-10 m-l-5" ButtonType="Button"
                                                        CommandName="editar" HeaderText="Editar" Text="Editar" />
                                                </Columns>
                                                <PagerSettings Mode="Numeric" PageButtonCount="10" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <hr />
                                    <asp:Panel runat="server" ID="pnledicion" Visible="false">
                                        <div class="container">
                                            <asp:Panel ID="pnlEstudiante" runat="server">
                                                <h2>Datos personales del estudiante ingresados en la pre matrícula</h2>
                                                <p>Todos los campos son requeridos a excepción del teléfono de residencia.</p>
                                                <div class="row">
                                                    <div class="col-md-6 border">
                                                        <br />
                                                        Nombre:
                                    <asp:TextBox ID="txtNom" CssClass="form-control" placeholder="*Nombre" runat="server"></asp:TextBox><br />Apellidos
                                                        <asp:TextBox ID="txtApellidos" CssClass="form-control" placeholder="*Apellidos" runat="server" MaxLength="50"></asp:TextBox><br />
                                                        Cédula:
                                    <input type="tel" id="txtCedula" placeholder="*Cédula" pattern="[0-9]{1,15}" class="form-control" runat="server"/><br />
                                                        Edad:                                   
                                    <asp:TextBox ID="txtEdad" CssClass="form-control" placeholder="Edad" runat="server" Enabled="false" TextMode="Number"></asp:TextBox><br />
                                                        Télefono:
                                    <input type="tel" id="txtTelefono" placeholder="*Teléfono" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                                        Correo electrónico:
                                    <asp:TextBox ID="txtEmail" MaxLength="50" CssClass="form-control" placeholder="*Correo" runat="server" TextMode="Email"></asp:TextBox><br />
                                                    </div>
                                                    <div class="col-md-6 border">
                                                        <br />
                                                        Dirección exacta:
                                    <asp:TextBox ID="txtdir" CssClass="form-control" placeholder="*Dirección" runat="server" MaxLength="500"></asp:TextBox><br />
                                                        Teléfono residencia en caso de tenerlo:
                                    <input type="tel" id="txtTelResi" placeholder="Teléfono de residencia" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
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
                                        <input type="tel" id="txtCedulaE" placeholder="*Cédula del encargado" pattern="[0-9]{1,15}" class="form-control" runat="server" /><br />
                                                            Teléfono:
                                        <input type="tel" id="txtTelE" placeholder="*Teléfono de Encargado" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                                        </div>
                                                        <div class="col-md-6 border">
                                                            <br />
                                                            Ingreso mensual:
                                        <input type="tel" id="txtIngresoE" placeholder="Ingreso mensual del encargado" pattern="[0-9]{1,15}" class="form-control" runat="server" /><br />
                                                            Ocupación:
                                        <asp:TextBox ID="txtOcupaE" placeholder="Ocupación del encargado" CssClass="form-control" MaxLength="200" runat="server"></asp:TextBox><br />
                                                            Teléfono trabajo:
                                        <input type="tel" id="txtTelTrabajoE" placeholder="Teléfono del trabajo del encargado" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
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
                                        <input type="tel" id="txtCedulaP" placeholder="*Cédula del padre" pattern="[0-9]{1,15}" class="form-control" runat="server" /><br />
                                                            Teléfono:
                                        <input type="tel" id="txtTelP" placeholder="*Teléfono de padre" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                                        </div>
                                                        <div class="col-md-6 border">
                                                            <br />
                                                            Ingreso mensual:
                                        <input type="tel" id="txtIngresoP" placeholder="Ingreso mensual del padre" pattern="[0-9]{1,15}" class="form-control" runat="server" /><br />
                                                            Ocupación:
                                        <asp:TextBox ID="txtOcupaP" placeholder="Ocupación del padre" MaxLength="200" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                            Teléfono de trabajo:
                                        <input type="tel" id="txtTelTrabajoP" placeholder="Teléfono del trabajo del padre" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
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
                                        <input type="tel" id="txtCedulaM" placeholder="*Cédula" pattern="[0-9]{1,15}" class="form-control" runat="server" /><br />
                                                            Teléfono:
                                        <input type="tel" id="txtTelM" placeholder="*Teléfono de madre" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                                        </div>
                                                        <div class="col-md-6 border">
                                                            <br />
                                                            Ingreso mensual:
                                        <input type="tel" id="txtIngresoM" placeholder="Ingreso mensual de la madre" pattern="[0-9]{1,15}" class="form-control" runat="server" /><br />
                                                            Ocupación:
                                        <asp:TextBox ID="txtOcupaM" placeholder="Ocupación de la madre" MaxLength="200" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                            Teléfono de trabajo:
                                        <input type="tel" id="txtTelTrabajoM" placeholder="Teléfono del trabajo de la madre" pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                                        </div>
                                                    </div>
                                                </asp:View>
                                            </asp:MultiView>
                                            <hr />
                                        </div>
                                        <div class="col-lg-auto">
                                            <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-success" Text="Guardar" OnClick="btnAgregar_Click" />&nbsp
                   <asp:Button ID="btnFormatear" runat="server" Text="Cancelar" CssClass="btn btn-dark" OnClick="btnFormatear_Click" />
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdateProgress ID="progress" AssociatedUpdatePanelID="update" runat="server">
                                <ProgressTemplate>
                                    <div class="preloader" style="display: block;">
                                        <svg class="circular" viewBox="25 25 50 50">
                                            <circle class="path" cx="50" cy="50" r="20" fill="none" stroke-width="2" stroke-miterlimit="10" />
                                        </svg>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../js/lib/jquery/jquery.min.js"></script>
    <script src="../js/lib/bootstrap/js/popper.min.js"></script>
    <script src="../js/lib/bootstrap/js/bootstrap.min.js"></script>
    <script src="../js/jquery.slimscroll.js"></script>
    <script src="../js/sidebarmenu.js"></script>
    <script src="../js/lib/sticky-kit-master/dist/sticky-kit.min.js"></script>
    <script src="../js/custom.min.js"></script>
</body>
</html>
