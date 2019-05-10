<%@ Page Title="" Language="C#" MasterPageFile="~/administracion/admin.Master" AutoEventWireup="true"
    CodeBehind="adminEstudiantes.aspx.cs" Inherits="Cote_Cloud.administracion.adminEstudiantes" %>

<%@ Register Src="~/User/completado.ascx" TagPrefix="uc1" TagName="completado" %>
<%@ Register Src="~/User/error.ascx" TagPrefix="uc1" TagName="error" %>
<%@ Register Src="~/User/informativo.ascx" TagPrefix="uc1" TagName="informativo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    Estudiantes
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="updating" runat="server">
        <ContentTemplate>
            <uc1:informativo runat="server" ID="importante" Visible="false" />
            <uc1:error runat="server" ID="error" Visible="false" />
            <uc1:completado runat="server" ID="completado" Visible="false" />
            <h4 class="card-title">Estudiantes de la institución</h4>
            <hr />
            <ul class="nav nav-tabs">
                <li class="nav-item active">
                    <asp:Button ID="btnAddStu" CssClass="nav-link active show btn btn-outline" runat="server"
                        Text="Agregar Nuevo" OnClick="btnAddStu_Click" /></li>
                <li class="nav-item">
                    <asp:Button ID="btnEditStud" CssClass="nav-link btn btn-outline" runat="server" Text="Editar existente"
                        OnClick="btnEditStud_Click" /></li>
            </ul>
            <div class="tab-content tabcontent-border">
                <div class="tab-pane active" id="home" role="tabpanel">
                    <div class="p-20">
                        <asp:MultiView ActiveViewIndex="0" runat="server">
                            <asp:View runat="server">
                                <h1>Agregar un estudiante nuevo</h1>
                            </asp:View>
                            <asp:View runat="server">
                                <h4>Seleccione el estudiante que desea editar.</h4>
                                <div class="table-responsive m-t-40">
                                    <div class="dataTables_wrapper">
                                        <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns="False"
                                            CssClass="display nowrap table table-hover table-striped table-bordered dataTable">
                                            <Columns>
                                            </Columns>
                                            <RowStyle CssClass="odd" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                        <asp:Panel ID="pnlEstudiante" runat="server">
                            <br />
                            <h3>Datos personales del estudiante</h3>
                            <p>*Campos son requeridos</p>
                            <div class="row">
                                <div class="col-6 col-12-xsmall border ">
                                    <br />
                                    <asp:TextBox ID="txtNom" CssClass="form-control" placeholder="*Nombre" runat="server"
                                        MaxLength="50"></asp:TextBox><br />
                                    <asp:TextBox ID="txtApe1" CssClass="form-control" placeholder="*Apellidos" runat="server"
                                        MaxLength="50"></asp:TextBox><br />
                                    <input type="tel" id="txtCedula" placeholder="*Cédula" pattern="[0-9]{1,15}" class="form-control"
                                        runat="server" /><br />
                                    *Fecha de nacimiento:<input type="date" id="txtFechaNacimiento" placeholder="Fecha de nacimiento"
                                        max="2099-12-31" min="1997-12-31" class="form-control" runat="server" /><br />
                                    <input type="tel" id="txtTelefono" placeholder="*Teléfono" pattern="[0-9]{8}" class="form-control"
                                        runat="server" /><br />
                                    <asp:TextBox ID="txtdir" CssClass="form-control" placeholder="*Dirección" runat="server"
                                        MaxLength="500"></asp:TextBox><br />
                                    <asp:TextBox ID="txtProvincia" MaxLength="50" CssClass="form-control" placeholder="*Provincia de residencia"
                                        runat="server"></asp:TextBox><br />
                                    <asp:TextBox ID="txtCanton" MaxLength="50" CssClass="form-control" placeholder="*Cantón de la residencia"
                                        runat="server"></asp:TextBox><br />
                                    <asp:TextBox ID="txtDistrito" MaxLength="50" CssClass="form-control" placeholder="*Distrito de residencia"
                                        runat="server"></asp:TextBox><br />
                                </div>
                                <div class="col-6 col-12-xsmall border ">
                                    <br />
                                    <input type="tel" id="txtTelResi" placeholder="Teléfono de residencia" pattern="[0-9]{8}"
                                        class="form-control" runat="server" /><br />
                                    <asp:TextBox ID="txtEmail" MaxLength="50" CssClass="form-control" placeholder="*Correo"
                                        runat="server" TextMode="Email"></asp:TextBox><br />
                                    <asp:TextBox ID="txtNac" CssClass="form-control" placeholder="*Nacionalidad" runat="server"></asp:TextBox><br />
                                    <input type="number" id="txtEdad" placeholder="*Edad" min="1" max="100" class="form-control"
                                        runat="server" /><br />
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

                            <hr />
                            <h2>Datos personales de los padres o encargado</h2>
                            <p>*Al menos alguno de los datos personales de los padres o encargado deben estar llenos*
                            </p>

                            <div class="col-lg-auto">
                                <asp:Button ID="btnTabEncargado" runat="server" CssClass="btn btn-light" Text="Datos del encargado"
                                    OnClick="btnTabEncargado_Click" />&nbsp
                    <asp:Button ID="btnTabPadre" runat="server" CssClass="btn btn-light" Text="Datos del padre"
                        OnClick="btnTabPadre_Click" />&nbsp
                    <asp:Button ID="btnTabMadre" runat="server" CssClass="btn btn-light" Text="Datos de la madre"
                        OnClick="btnTabMadre_Click" />&nbsp
                            </div>
                            <br />
                            <asp:MultiView ID="tabs" runat="server" ActiveViewIndex="0">
                                <asp:View runat="server">
                                    <h4 class="text-center">Datos del encargado</h4>
                                    <div class="row">
                                        <div class="col-6 col-12-xsmall border ">
                                            <br />
                                            <asp:TextBox ID="txtNomE" placeholder="*Nombre completo del encargado" CssClass="form-control"
                                                runat="server"></asp:TextBox><br />
                                            <input type="tel" id="txtCedE" placeholder="*Cédula del encargado" pattern="[0-9]{15}"
                                                class="form-control" runat="server" /><br />
                                            <input type="tel" id="txtTelE" placeholder="*Teléfono de Encargado" pattern="[0-9]{8}"
                                                class="form-control" runat="server" /><br />
                                        </div>
                                        <div class="col-6 col-12-xsmall border">
                                            <br />
                                            <input type="number" id="txtIngresoE" placeholder="Ingreso mensual del encargado"
                                                min="0" class="form-control" runat="server" /><br />
                                            <asp:TextBox ID="txtOcupaE" placeholder="Ocupación del encargado" CssClass="form-control"
                                                runat="server"></asp:TextBox><br />
                                            <input type="tel" id="txtTelTrabajoE" placeholder="Teléfono del trabajo del encargado"
                                                pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View runat="server">
                                    <h4 class="text-center">Datos del padre</h4>
                                    <div class="row">
                                        <div class="col-6 col-12-xsmall border">
                                            <br />
                                            <asp:TextBox ID="txtNomP" placeholder="*Nombre completo de la padre" CssClass="form-control"
                                                runat="server"></asp:TextBox><br />
                                            <input type="tel" id="txtCedP" placeholder="*Cédula del padre" pattern="[0-9]{15}"
                                                class="form-control" runat="server" /><br />
                                            <input type="tel" id="txtTelP" placeholder="*Teléfono de padre" pattern="[0-9]{8}"
                                                class="form-control" runat="server" /><br />
                                        </div>
                                        <div class="col-6 col-12-xsmall border">
                                            <br />
                                            <input type="number" id="txtIngresoP" placeholder="Ingreso mensual del padre" min="0"
                                                class="form-control" runat="server" /><br />
                                            <asp:TextBox ID="txtOcupaP" placeholder="Ocupación del padre" CssClass="form-control"
                                                runat="server"></asp:TextBox><br />
                                            <input type="tel" id="txtTelTrabajoP" placeholder="Teléfono del trabajo del padre"
                                                pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View runat="server">
                                    <h4 class="text-center">Datos de la madre</h4>
                                    <div class="row">
                                        <div class="col-6 col-12-xsmall border">
                                            <br />
                                            <asp:TextBox ID="txtnomM" placeholder="*Nombre completo de la madre" CssClass="form-control"
                                                runat="server"></asp:TextBox><br />
                                            <input type="tel" id="txtCedM" placeholder="*Cédula de la madre" pattern="[0-9]{15}"
                                                class="form-control" runat="server" /><br />
                                            <input type="tel" id="txtTelM" placeholder="*Teléfono de madre" pattern="[0-9]{8}"
                                                class="form-control" runat="server" /><br />
                                        </div>
                                        <div class="col-6 col-12-xsmall border">
                                            <br />
                                            <input type="number" id="txtIngresoM" placeholder="Ingreso mensual de la madre" pattern="[0-9]{8}"
                                                class="form-control" runat="server" /><br />
                                            <asp:TextBox ID="txtOcupaM" placeholder="Ocupación de la madre" CssClass="form-control"
                                                runat="server"></asp:TextBox><br />
                                            <input type="tel" id="txtTelTrabajoM" placeholder="Teléfono del trabajo de la madre"
                                                pattern="[0-9]{8}" class="form-control" runat="server" /><br />
                                        </div>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                            <div class="col-lg-auto">
                                <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-success" Text="Registrarse"
                                    OnClick="btnAgregar_Click" />&nbsp
                   <asp:Button ID="btnFormatear" runat="server" Text="Vaciar" CssClass="btn btn-dark"
                       PostBackUrl="~/administracion/adminEstudiantes.aspx" />
                            </div>
                            <hr />
                        </asp:Panel>
                        <hr />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="progress" AssociatedUpdatePanelID="updating" runat="server">
        <ProgressTemplate>
            <div class="preloader" style="display:block;">
                <svg class="circular" viewBox="25 25 50 50">
                    <circle class="path" cx="50" cy="50" r="20" fill="none" stroke-width="2" stroke-miterlimit="10" />
                </svg>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
