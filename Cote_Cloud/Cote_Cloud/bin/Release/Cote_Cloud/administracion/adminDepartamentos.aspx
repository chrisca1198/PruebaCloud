<%@ Page Title="" EnableEventValidation="false"  Language="C#" MasterPageFile="~/administracion/admin.Master" AutoEventWireup="true" CodeBehind="adminDepartamentos.aspx.cs" Inherits="Cote_Cloud.administracion.adminDepartamentos" %>

<%@ Register Src="~/User/completado.ascx" TagPrefix="uc1" TagName="completado" %>
<%@ Register Src="~/User/error.ascx" TagPrefix="uc1" TagName="error" %>
<%@ Register Src="~/User/informativo.ascx" TagPrefix="uc1" TagName="informativo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    Departamentos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="updating" runat="server">
        <ContentTemplate>
            <uc1:informativo runat="server" ID="informativo" Visible="false" />
            <uc1:error runat="server" ID="error" Visible="false" />
            <uc1:completado runat="server" ID="completado" Visible="false" />

            <h4 class="card-title">Departamentos en la institución</h4>
            <ul class="nav nav-tabs">
                <li class="nav-item active">
                    <asp:Button ID="btnTecnico" CssClass="nav-link active btn btn-outline" runat="server" Text="Área Técnica" OnClick="btnTecnico_Click" /></li>
                <li class="nav-item">
                    <asp:Button ID="btnAcademico" CssClass="nav-link btn btn-outline" runat="server" Text="Área Académica" OnClick="btnAcademico_Click" /></li>
            </ul>
            <div class="tab-content tabcontent-border">
                <div class="tab-pane active" id="home" role="tabpanel">
                    <div class="p-20">
                        <asp:MultiView ID="tabs" runat="server" ActiveViewIndex="0">
                            <asp:View ID="tabTecnica" runat="server">
                                <ul class="nav nav-tabs">
                                    <li class="nav-item active">
                                        <asp:Button ID="btnEspecialidades" CssClass="nav-link active btn btn-success m-b-10 m-l-5" runat="server" Text="Especialidades" OnClick="btnEspecialidades_Click" /></li>
                                    <li class="nav-item">
                                        <asp:Button ID="btnSubAreas" CssClass="nav-link btn btn-outline" runat="server" Text="Sub Áreas" OnClick="btnSubAreas_Click" /></li>
                                </ul>
                                <div class="tab-content tabcontent-border">
                                    <div class="tab-pane active" id="home" role="tabpanel">
                                        <div class="p-20">
                                            <asp:MultiView ActiveViewIndex="0" runat="server" ID="tabsEspecialidades">
                                                <asp:View runat="server" ID="tabEsp">
                                                    <div class="card-body p-b-0">
                                                        <h4 class="card-title">Mantenimiento de especialidades</h4>
                                                        <div class="table-responsive m-t-40">
                                                            <div class="dataTables_wrapper">
                                                                <asp:GridView ID="gvSpecialty" runat="server" AutoGenerateColumns="False" DataKeyNames="especialidad"
                                                                    CssClass="display nowrap table table-hover table-striped table-bordered dataTable"
                                                                    OnRowCancelingEdit="gvSpecialty_RowCancelingEdit" OnPageIndexChanging="gvSpecialty_PageIndexChanging"
                                                                    OnRowDeleting="gvSpecialty_RowDeleting" OnRowEditing="gvSpecialty_RowEditing" OnRowUpdating="gvSpecialty_RowUpdating">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="especialidad" HeaderText="Nombre especialidad" ControlStyle-CssClass="sorting_asc" />
                                                                        <asp:CommandField ShowEditButton="true" HeaderText="Edición" ControlStyle-CssClass="sorting_asc" />
                                                                        <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminación" ControlStyle-CssClass="sorting_asc" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <asp:Panel ID="pnlAgregarEsp" runat="server">
                                                            <h3>Agregar especialidad</h3>
                                                            <table class="table-active">
                                                                <tr>
                                                                    <td>Nombre de especialidad:
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtNomEspe" placeholder="Nombre" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnAddEsp" CssClass="btn square light-blue txt-white padd-sm" runat="server" Text="Agregar" OnClick="btnAddEsp_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </div>
                                                </asp:View>
                                                <asp:View runat="server" ID="tabSubAreas">
                                                    <div class="card-body p-b-0">
                                                        <h4 class="card-title">Mantenimiento de sub áreas</h4>
                                                        <asp:DropDownList ID="cboEspecialidad" CssClass="form-control-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboEspecialidad_SelectedIndexChanged"></asp:DropDownList><hr />
                                                        <div class="table-responsive m-t-40">
                                                            <div class="dataTables_wrapper">
                                                                <asp:GridView ID="gvSubArea" runat="server" CssClass="display nowrap table table-hover table-striped table-bordered dataTable"
                                                                    AllowPaging="True" AllowSorting="True" PageSize="7" AutoGenerateColumns="False" DataKeyNames="materia"
                                                                    OnPageIndexChanging="gvSubArea_PageIndexChanging" OnRowCancelingEdit="gvSubArea_RowCancelingEdit"
                                                                    OnRowDeleting="gvSubArea_RowDeleting" OnRowEditing="gvSubArea_RowEditing" OnRowUpdating="gvSubArea_RowUpdating">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="materia" HeaderText="Sub Área" ControlStyle-CssClass="sorting_asc" />
                                                                        <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminación" ControlStyle-CssClass="sorting_asc" />
                                                                        <asp:CommandField ShowEditButton="true" HeaderText="Edición" ControlStyle-CssClass="sorting_asc" />
                                                                    </Columns>
                                                                    <RowStyle CssClass="odd" />
                                                                    <PagerSettings Mode="NextPrevious" PageButtonCount="6" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <asp:Panel ID="pnlAgregarSub" runat="server">
                                                            <h3>Agregar sub área</h3>
                                                            <table class="table-active">
                                                                <tr>
                                                                    <td>Nombre de sub área:
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtNomSub" placeholder="Nombre" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnAddSub" CssClass="btn square light-blue txt-white padd-sm" runat="server" Text="Agregar" OnClick="btnAddSub_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </div>
                                                </asp:View>
                                            </asp:MultiView>
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                            <asp:View ID="tabAcademica" runat="server">
                                <div class="card-body p-b-0">
                                    <h4 class="card-title">Mantenimiento de materias</h4>
                                    <div class="table-responsive m-t-40">
                                        <div class="dataTables_wrapper">
                                            <asp:GridView ID="gvMaterias" runat="server" CssClass="display nowrap table table-hover table-striped table-bordered dataTable"
                                                AllowPaging="True" AllowSorting="True" PageSize="7" AutoGenerateColumns="False" DataKeyNames="materia" OnPageIndexChanging="gvMaterias_PageIndexChanging" OnRowCancelingEdit="gvMaterias_RowCancelingEdit" OnRowDeleting="gvMaterias_RowDeleting" OnRowEditing="gvMaterias_RowEditing" OnRowUpdating="gvMaterias_RowUpdating">
                                                <Columns>
                                                    <asp:BoundField DataField="materia" HeaderText="Sub Área" ControlStyle-CssClass="sorting_asc" />
                                                    <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminación" ControlStyle-CssClass="sorting_asc" />
                                                    <asp:CommandField ShowEditButton="true" HeaderText="Edición" ControlStyle-CssClass="sorting_asc" />
                                                </Columns>
                                                <RowStyle CssClass="odd" />
                                                <PagerSettings Mode="NextPrevious" PageButtonCount="6" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <hr />
                                    <asp:Panel ID="pnlAgregarMateria" runat="server">
                                        <h3>Agregar materia</h3>
                                        <table class="table-active">
                                            <tr>
                                                <td>Nombre materia:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNomMateria" placeholder="Nombre" CssClass="form-control" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnAddMateria" CssClass="btn square light-blue txt-white padd-sm" runat="server" Text="Agregar" OnClick="btnAddMateria_Click"/>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="progress" AssociatedUpdatePanelID="updating" runat="server">
        <ProgressTemplate>
            <div class="preloader">
                <svg class="circular" viewBox="25 25 50 50">
                    <circle class="path" cx="50" cy="50" r="20" fill="none" stroke-width="2" stroke-miterlimit="10" />
                </svg>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
