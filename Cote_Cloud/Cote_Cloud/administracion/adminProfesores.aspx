<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/administracion/admin.Master" AutoEventWireup="true" CodeBehind="adminProfesores.aspx.cs" Inherits="Cote_Cloud.administracion.adminProfesores" %>

<%@ Register Src="~/User/completado.ascx" TagPrefix="uc1" TagName="completado" %>
<%@ Register Src="~/User/error.ascx" TagPrefix="uc1" TagName="error" %>
<%@ Register Src="~/User/informativo.ascx" TagPrefix="uc1" TagName="informativo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    Profesores
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="updating" runat="server">
        <ContentTemplate>
            <uc1:informativo runat="server" ID="informativo" Visible="false" />
            <uc1:error runat="server" ID="error" Visible="false" />
            <uc1:completado runat="server" ID="completado" Visible="false" />
            <h4 class="card-title">Profesores de la institución</h4>
            <ul class="nav nav-tabs">
                <li class="nav-item active">
                    <asp:Button ID="btnAddProfe" CssClass="nav-link active btn btn-outline" runat="server" Text="Agrega nuevo" OnClick="btnAddProfe_Click" /></li>
                <li class="nav-item">
                    <asp:Button ID="btnEditProfe" CssClass="nav-link btn btn-outline" runat="server" Text="Editar existente" OnClick="btnEditProfe_Click" /></li>
            </ul>
            <div class="tab-content tabcontent-border">
                <div class="tab-pane active" id="home" role="tabpanel">
                    <div class="p-20">
                        <asp:MultiView ID="tabs" runat="server" ActiveViewIndex="0">
                            <asp:View ID="tabAgregarPro" runat="server">
                                <h5 class="card-title">Datos personales y requeridos del profesor</h5>
                                <hr />
                                <asp:Panel ID="pnlAgregar" runat="server">
                                    <div class="row">
                                        <div class="col-12">
                                            <asp:TextBox ID="txtCed" AutoPostBack="True" CssClass="form-control" placeholder="*Cedula" runat="server" TextMode="Number" OnTextChanged="txtCed_TextChanged"></asp:TextBox><br />
                                            <asp:TextBox ID="txtNom" CssClass="form-control" placeholder="*Nombre" runat="server"></asp:TextBox><br />
                                            <asp:TextBox ID="txtApe1" CssClass="form-control" placeholder="*Apellidos" runat="server"></asp:TextBox><br />
                                            <asp:TextBox ID="txtTel" CssClass="form-control" placeholder="*Telefono" runat="server" TextMode="Number" AutoPostBack="True" OnTextChanged="txtTel_TextChanged"></asp:TextBox><br />
                                            <asp:TextBox ID="txtCorreo" CssClass="form-control" placeholder="*Correo" TextMode="Email" runat="server"></asp:TextBox><br />
                                            Departamento:
                                            <br />
                                            <asp:RadioButton ID="rbAcademico" Text="Académico" runat="server" OnCheckedChanged="rbAcademico_CheckedChanged" AutoPostBack="True" />
                                            &nbsp<asp:RadioButton ID="rbTecnico" Text="Técnico" runat="server" OnCheckedChanged="rbTecnico_CheckedChanged" AutoPostBack="True" /><br />
                                            <asp:DropDownList ID="cboMateria" Visible="false" runat="server" CssClass="form-control-sm">
                                            </asp:DropDownList>
                                            <asp:Panel ID="pnlTecnico" runat="server" Visible="false">
                                                Especialidad:&nbsp<asp:DropDownList ID="cboEspecialidad" CssClass="form-control-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboEspecialidad_SelectedIndexChanged">
                                                    <asp:ListItem Text="Seleccione la especialidad" Value="Seleccione la especialidad" />
                                                </asp:DropDownList>
                                                <asp:Panel ID="pnlSubareas" runat="server" Visible="false">
                                                    <hr />
                                                    Sub área 1: &nbsp<asp:DropDownList ID="cboSubAreas1" CssClass="form-control-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboSubAreas1_SelectedIndexChanged">
                                                    </asp:DropDownList><br />
                                                    Sub área 2:&nbsp<asp:CheckBox ID="chkSub2" runat="server" CssClass="checkbox-wrapper-mail" AutoPostBack="True" OnCheckedChanged="chkSub2_CheckedChanged" />
                                                    &nbsp<asp:DropDownList ID="cboSubAreas2" CssClass="form-control-sm" runat="server" AutoPostBack="True" Visible="False" OnSelectedIndexChanged="cboSubAreas2_SelectedIndexChanged">
                                                    </asp:DropDownList><br />
                                                    Sub área 3:&nbsp<asp:CheckBox ID="chkSub3" runat="server" CssClass="checkbox-wrapper-mail" AutoPostBack="True" OnCheckedChanged="chkSub3_CheckedChanged" />
                                                    &nbsp<asp:DropDownList ID="cboSubAreas3" CssClass="form-control-sm" runat="server" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="cboSubAreas3_SelectedIndexChanged">
                                                    </asp:DropDownList><br />
                                                </asp:Panel>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <hr />
                                <div class="row">
                                    <div class="col-lg-auto">
                                        <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-success" Text="Agregar" OnClick="btnAgregar_Click" />&nbsp
                   <asp:Button ID="btnFormatear" runat="server" Text="Vaciar" CssClass="btn btn-dark" OnClick="btnFormatear_Click" />
                                    </div>
                                </div>
                            </asp:View>
                            <asp:View ID="tabEditPro" runat="server">
                                <h4 class="card-title">Mantenimiento de profesores</h4>
                                <ul class="nav nav-tabs">
                                    <li class="nav-item active">
                                        <asp:Button ID="btnEditTecnicas" CssClass="nav-link active btn btn-success m-b-10 m-l-5" runat="server" Text="Técnico" OnClick="btnEditTecnicas_Click" /></li>
                                    <li class="nav-item">
                                        <asp:Button ID="btnEditAcademicos" CssClass="nav-link btn btn-outline" runat="server" Text="Académico" OnClick="btnEditAcademicos_Click" /></li>
                                </ul>
                                <div class="tab-content tabcontent-border">
                                    <div class="tab-pane active" id="home" role="tabpanel">
                                        <div class="p-20">
                                            <asp:MultiView ID="tabEditProfe" runat="server" ActiveViewIndex="0">
                                                <asp:View runat="server" ID="tabEditTecnico">
                                                    <asp:DropDownList ID="cboEditEspecialidad" CssClass="form-control-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboEditEspecialidad_SelectedIndexChanged"></asp:DropDownList><hr />
                                                    <div class="table-responsive m-t-40">
                                                        <div class="dataTables_wrapper">
                                                            <asp:GridView ID="gvEditTecnico" runat="server" CssClass="display nowrap table table-hover table-striped table-bordered dataTable"
                                                                AllowPaging="True" AllowSorting="True" PageSize="7" AutoGenerateColumns="False" DataKeyNames="cedula"
                                                                OnPageIndexChanging="gvEditTecnico_PageIndexChanging" OnRowCancelingEdit="gvEditTecnico_RowCancelingEdit"
                                                                OnRowDeleting="gvEditTecnico_RowDeleting" OnRowEditing="gvEditTecnico_RowEditing" 
                                                                OnRowUpdating="gvEditTecnico_RowUpdating" OnRowDataBound="gvEditTecnico_RowDataBound">
                                                                <Columns>
                                                                    <asp:BoundField ReadOnly="true" DataField="cedula" HeaderText="Cédula" ControlStyle-CssClass="sorting_asc" />
                                                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" ControlStyle-CssClass="sorting_asc" />
                                                                    <asp:BoundField DataField="apellidos" HeaderText="Apellidos" ControlStyle-CssClass="sorting_asc" />
                                                                    <asp:BoundField DataField="telefono" HeaderText="Teléfono" ControlStyle-CssClass="sorting_asc" />
                                                                    <asp:BoundField DataField="materia1" HeaderText="Sub área 1" ControlStyle-CssClass="sorting_asc" />
                                                                    <asp:BoundField DataField="materia2" HeaderText="Sub área 2" ControlStyle-CssClass="sorting_asc" />
                                                                    <asp:BoundField DataField="materia3" HeaderText="Sub área 3" ControlStyle-CssClass="sorting_asc" />
                                                                    <asp:TemplateField HeaderText="Sub área 1" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="cboSubArea1" runat="server">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sub área 2" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="cboSubArea2" runat="server">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sub área 3" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="cboSubArea3" runat="server">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:CommandField ShowEditButton="true" HeaderText="Edición" ControlStyle-CssClass="sorting_asc" />
                                                                    <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminación" ControlStyle-CssClass="sorting_asc" />
                                                                </Columns>
                                                                <RowStyle CssClass="odd" />
                                                                <PagerSettings Mode="NextPrevious" PageButtonCount="6" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </asp:View>
                                                <asp:View runat="server" ID="tabEditAcademico">
                                                    <div class="table-responsive m-t-40">
                                                        <div class="dataTables_wrapper">
                                                            <asp:GridView ID="gvEditAcademico" runat="server" CssClass="display nowrap table table-hover table-striped table-bordered dataTable"
                                                                AllowPaging="True" AllowSorting="True" PageSize="7" AutoGenerateColumns="False" DataKeyNames="cedula"
                                                                OnPageIndexChanging="gvEditAcademico_PageIndexChanging" OnRowCancelingEdit="gvEditAcademico_RowCancelingEdit"
                                                                OnRowDeleting="gvEditAcademico_RowDeleting" OnRowEditing="gvEditAcademico_RowEditing" OnRowUpdating="gvEditAcademico_RowUpdating" OnRowDataBound="gvEditAcademico_RowDataBound">
                                                                <Columns>
                                                                    <asp:BoundField ReadOnly="true" DataField="cedula" HeaderText="Cédula" ControlStyle-CssClass="sorting_asc" />
                                                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" ControlStyle-CssClass="sorting_asc" />
                                                                    <asp:BoundField DataField="apellidos" HeaderText="Apellidos" ControlStyle-CssClass="sorting_asc" />
                                                                    <asp:BoundField DataField="telefono" HeaderText="Teléfono" ControlStyle-CssClass="sorting_asc" />
                                                                    <asp:BoundField DataField="materia" HeaderText="Materia" ControlStyle-CssClass="sorting_asc" />
                                                                    <asp:TemplateField HeaderText="Materia" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="cboMateriaProfe" runat="server">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:CommandField ShowEditButton="true" HeaderText="Edición" ControlStyle-CssClass="sorting_asc" />
                                                                    <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminación" ControlStyle-CssClass="sorting_asc" />
                                                                </Columns>
                                                                <RowStyle CssClass="odd" />
                                                                <PagerSettings Mode="NextPrevious" PageButtonCount="6" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </asp:View>
                                            </asp:MultiView>
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
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
