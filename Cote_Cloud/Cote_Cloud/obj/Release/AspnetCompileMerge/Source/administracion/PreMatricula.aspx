<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/administracion/admin.Master"
    AutoEventWireup="true" CodeBehind="PreMatricula.aspx.cs" Inherits="Cote_Cloud.administracion.PreMatricula" %>

<%@ Register Src="~/User/completado.ascx" TagPrefix="uc1" TagName="completado" %>
<%@ Register Src="~/User/error.ascx" TagPrefix="uc1" TagName="error" %>
<%@ Register Src="~/User/informativo.ascx" TagPrefix="uc1" TagName="informativo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    Pre-Matricula
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="updating" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                function pulsar(e) {
                    tecla = (document.all) ? e.keyCode : e.which;
                    return (tecla != 13);
                }
            </script>
            <uc1:informativo runat="server" ID="informativo" Visible="false" />
            <uc1:error runat="server" ID="error" Visible="false" />
            <uc1:completado runat="server" ID="completado" Visible="false" />
            <div class="row">
                <div class="col-5">
                    <h4 class="card-title">Propiedades del proceso de admisión</h4>
                </div>
                <div class="col-2">
                    <asp:Panel runat="server" ID="pnlIniciarNuevoProceso" Visible="false">
                        <h5>Iniciar proceso</h5>
                        <asp:Button ID="btnAdmission" runat="server" Text="Iniciar" CssClass="btn btn-success btn-rounded m-b-10 m-l-5"
                            OnClick="btnAdmission_Click" />
                    </asp:Panel>
                </div>
                <div class="col-5">
                    <h5>
                        <a href="#"><i class="ti-settings"></i>&nbsp Configuraciones del proceso</a>
                        <asp:Button ID="btnEditFechasAdmission" runat="server" Text="Edición" CssClass="btn btn-warning btn-rounded m-b-10 m-l-5"
                            OnClick="btnEditFechasAdmission_Click" />
                    </h5>
                </div>
            </div>
            <hr />
            <asp:Panel runat="server" ID="pnlPreMatricula">
                <p>
                    <i class="fa fa-tablet"></i>&nbsp<a href="../RegPreMatricula/PreRegistro.aspx"
                        aria-expanded="false"><span class="hide-menu">Formulario de prematricula</span></a>
                </p>
                <ul class="nav nav-tabs">
                    <li class="nav-item active">
                        <asp:Button ID="btnCodigos" CssClass="nav-link active show btn btn-outline" runat="server"
                            Text="Codigos" OnClick="btnCodigos_Click" /></li>
                    <li class="nav-item">
                        <asp:Button ID="btnEspecialidades" CssClass="nav-link btn btn-outline" runat="server"
                            Text="Especialidades" OnClick="btnEspecialidades_Click" /></li>
                    <li class="nav-item">
                        <asp:Button ID="btnEstudiantes" CssClass="nav-link btn btn-outline" runat="server"
                            Text="Estudiantes" OnClick="btnEstudiantes_Click" /></li>
                </ul>
                <div class="tab-content tabcontent-border">
                    <div class="tab-pane active" id="home" role="tabpanel">
                        <div class="p-20">
                            <asp:MultiView ID="tabs" runat="server" ActiveViewIndex="0">
                                <asp:View ID="tabCodigos" runat="server">
                                    <div class="vtabs">
                                        <ul class="nav nav-tabs tabs-vertical">
                                            <li class="nav-item active">
                                                <asp:Button ID="btnAgregarCodigos" CssClass="nav-link active show btn btn-outline"
                                                    runat="server" Text="Agregar" OnClick="btnAgregarCodigos_Click" /></li>
                                            <li class="nav-item">
                                                <asp:Button ID="btnDelCodigos" CssClass="nav-link btn btn-outline" runat="server"
                                                    Text="Eliminar" OnClick="btnDelCodigos_Click" /></li>
                                            <li class="nav-item">
                                                <asp:Button ID="btnReporte" CssClass="nav-link btn btn-outline" runat="server" Text="Reporte"
                                                    OnClick="btnReporte_Click" /></li>
                                            <li class="nav-item">
                                                <asp:Button ID="btnUsadosPines" CssClass="nav-link btn btn-outline" runat="server"
                                                    Text="Usados" OnClick="btnUsadosPines_Click" /></li>
                                            <li class="nav-item">
                                                <asp:Button ID="btnDisponiblesPines" CssClass="nav-link btn btn-outline" runat="server"
                                                    Text="Disponibles" OnClick="btnDisponiblesPines_Click" /></li>
                                        </ul>
                                        <div class="tab-content">
                                            <div class="tab-pane active">
                                                <div class="p-20">
                                                    <asp:MultiView ID="tabsCodigos" runat="server" ActiveViewIndex="0">
                                                        <asp:View runat="server" ID="tabAgregarCod">
                                                            <h2>Generar nuevos codigos:</h2>
                                                            <h4>Cantidad:</h4>
                                                            <br />
                                                            <asp:TextBox ID="txtcantidad" CssClass="input square" runat="server"></asp:TextBox>
                                                            <asp:Button ID="btnGeneracion" CssClass="btn square light-blue txt-white padd-sm"
                                                                runat="server" Text="Aceptar" OnClick="btnGeneracion_Click" />
                                                        </asp:View>
                                                        <asp:View ID="tabDelCod" runat="server">
                                                            <h2>Eliminar Codigos generados por fecha y hora:</h2>
                                                            <div class="p-10">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <h4>Fecha:</h4>
                                                                        </td>
                                                                        <td>
                                                                            <h4>Hora:</h4>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:DropDownList ID="cboFechaCod" CssClass="input square" runat="server" AutoPostBack="True"
                                                                                OnSelectedIndexChanged="cboFechaCod_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="cboHoraCod" CssClass="input square" runat="server"></asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Button ID="btnElimCodigos" CssClass="btn square light-blue txt-white padd-sm"
                                                                                runat="server" Text="Eliminar codigos" OnClick="btnElimCodigos_Click"
                                                                                OnClientClick="return confirm('¿Estás seguro de eliminar los pines?');" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <hr />
                                                            <asp:TextBox ID="txtCod" runat="server" placeholder="Eliminar codigo especifico"></asp:TextBox>
                                                            <asp:Button ID="btnEliminar" CssClass="btn square light-blue txt-white padd-sm" runat="server"
                                                                Text="Eliminar" OnClick="btnEliminar_Click" OnClientClick="return confirm('¿Estás seguro de eliminar el pin?');" />
                                                        </asp:View>
                                                        <asp:View ID="tabReport" runat="server">
                                                            <h2>Crear un reporte de los codigos disponibles:</h2>
                                                            <h4>Fecha:</h4>
                                                            <asp:DropDownList ID="cboFecha" CssClass="input square" runat="server" AutoPostBack="True"
                                                                OnTextChanged="cboFecha_TextChanged">
                                                            </asp:DropDownList>
                                                            <h4>Hora:</h4>
                                                            <asp:DropDownList ID="cboHora" CssClass="input square" runat="server"></asp:DropDownList>
                                                            <asp:Button ID="btnGenerarR" runat="server" CssClass="btn square light-blue txt-white padd-sm"
                                                                Text="Generar" OnClick="btnGenerarR_Click" />
                                                            <asp:Button ID="btnGenR" runat="server" CssClass="btn square light-blue txt-white padd-sm"
                                                                Text="Generar Reporte General" OnClick="btnGenR_Click" />
                                                        </asp:View>
                                                        <asp:View ID="tabCodunavailable" runat="server">
                                                            <h2>Pines usados en el proceso de pre matrícula:</h2>
                                                            <div class="table-responsive">
                                                                <div class="dataTables_wrapper">
                                                                    <div class="dt-buttons">
                                                                        <asp:Button ID="btnReportPinUnavailable" runat="server" Text="Reporte" CssClass="dt-button buttons-copy buttons-html5"
                                                                            OnClick="btnReportPinUnavailable_Click" />
                                                                    </div>
                                                                    <div class="dataTables_filter">
                                                                        <label>
                                                                            Buscar:<asp:TextBox ID="txtBusCodUsado" runat="server" OnTextChanged="txtBusCodUsado_TextChanged"
                                                                                AutoPostBack="True"></asp:TextBox></label>
                                                                    </div>
                                                                    <asp:GridView ID="gvPinesUsados" runat="server"
                                                                        OnPageIndexChanging="gvPinesUsados_PageIndexChanging" CssClass="table table-bordered"
                                                                        AllowPaging="True" AllowSorting="True"
                                                                        PageSize="10" OnRowCommand="gvPinesUsados_RowCommand">
                                                                        <EmptyDataTemplate>
                                                                            <div class="alert alert-secondary">
                                                                                <h4 class="alert-heading">Sin registro de estudiantes</h4>
                                                                                <p>Por el momento no hay ningún pin usado.</p>
                                                                            </div>
                                                                        </EmptyDataTemplate>
                                                                        <Columns>
                                                                            <asp:ButtonField ControlStyle-CssClass="btn btn-info btn-outline m-b-10 m-l-5" ButtonType="Button"
                                                                                CommandName="habilitar" HeaderText="Habilitar pines" Text="Habilitar" />
                                                                        </Columns>
                                                                        <PagerSettings Mode="Numeric" PageButtonCount="10" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </asp:View>
                                                        <asp:View ID="tabDisponibles" runat="server">
                                                            <h2>Pines disponibles en el proceso de pre matrícula:</h2>
                                                            <div class="table-responsive">
                                                                <div class="dataTables_wrapper">
                                                                    <div class="dataTables_filter">
                                                                        <label>
                                                                            Buscar:<asp:TextBox ID="txtBusCodDisponible" runat="server" OnTextChanged="txtBusCodDisponible_TextChanged"
                                                                                AutoPostBack="True"></asp:TextBox></label>
                                                                    </div>
                                                                    <asp:GridView ID="gvPinesDisponibles" runat="server"
                                                                        OnPageIndexChanging="gvPinesDisponibles_PageIndexChanging" CssClass="table table-bordered"
                                                                        AllowPaging="True" AllowSorting="True"
                                                                        PageSize="10" OnRowCommand="gvPinesDisponibles_RowCommand" PagerStyle-BorderColor="#cccccc" PagerStyle-BorderStyle="Solid" PagerStyle-BorderWidth="0.5px" PagerStyle-Font-Size="Large">
                                                                        <EmptyDataTemplate>
                                                                            <div class="alert alert-secondary">
                                                                                <h4 class="alert-heading">Sin registro de estudiantes</h4>
                                                                                <p>Por el momento no hay ningún pin disponible.</p>
                                                                            </div>
                                                                        </EmptyDataTemplate>
                                                                        <Columns>
                                                                            <asp:ButtonField ControlStyle-CssClass="btn btn-info btn-outline m-b-10 m-l-5" ButtonType="Button"
                                                                                CommandName="Deshabilitar" HeaderText="Deshabilitar pines" Text="Deshabilitar" />
                                                                        </Columns>
                                                                        <PagerSettings Mode="Numeric" PageButtonCount="10" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </asp:View>
                                                    </asp:MultiView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View ID="tabsEspecialidades" runat="server">
                                    <h3>Habilitar especialidad de la institución en el proceso de admision</h3>
                                    <div class="table-responsive m-t-40">
                                        <div class="dataTables_wrapper">
                                            <asp:GridView ID="gvSpecialty" runat="server" AutoGenerateColumns="False" DataKeyNames="especialidad"
                                                OnPageIndexChanging="gvSpecialty_PageIndexChanging" OnRowCancelingEdit="gvSpecialty_RowCancelingEdit"
                                                OnRowUpdating="gvSpecialty_RowUpdating1" OnRowEditing="gvSpecialty_RowEditing1"
                                                CssClass="display nowrap table table-hover table-striped table-bordered dataTable" PagerStyle-BorderColor="#cccccc" PagerStyle-BorderStyle="Solid" PagerStyle-BorderWidth="0.5px" PagerStyle-Font-Size="Large">
                                                <Columns>
                                                    <asp:BoundField ReadOnly="true" DataField="especialidad" HeaderText="Nombre especialidad"
                                                        ControlStyle-CssClass="sorting_asc" />
                                                    <asp:CheckBoxField HeaderText="Disponibilidad en proceso de admision" DataField="disponible"
                                                        ControlStyle-CssClass="sorting_asc" />
                                                    <asp:BoundField DataField="cantidad" HeaderText="Cantidad de cupos disponibles" ControlStyle-CssClass="sorting_asc" />
                                                    <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="sorting_asc" />
                                                </Columns>
                                                <RowStyle CssClass="odd" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View ID="tabsEstudiantes" runat="server">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            Seleccione la especialidad:       
            <asp:DropDownList CssClass="form-control" ID="cboEspecialidad" runat="server" AutoPostBack="True"
                OnSelectedIndexChanged="cboEspecialidad_SelectedIndexChanged">
            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-6">
                                            Filtro de selección:
                                            <div class="row">
                                                <div class="col-6">
                                                    <asp:Button ID="btnOpcion1" runat="server" Text="Primera opción"
                                                        OnClick="btnOpcion1_Click" CssClass="btn btn-dropbox" />
                                                </div>
                                                <div class="col-6">
                                                    <asp:Button ID="btnOpcion2" runat="server" Text="Segunda opción"
                                                        OnClick="btnOpcion2_Click" CssClass="btn btn-light" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <hr />

                                    <ul class="nav nav-tabs customtab2">
                                        <li class="nav-item active">
                                            <asp:Button ID="btnEstNotas" CssClass="nav-link active btn btn-info m-b-10 m-l-5"
                                                runat="server" Text="Notas" OnClick="btnEstNotas_Click" /></li>
                                        <li class="nav-item">
                                            <asp:Button ID="btnEstAdmitir" CssClass="nav-link btn btn-outline" runat="server"
                                                Text="Admitir" OnClick="btnEstAdmitir_Click" /></li>
                                    </ul>
                                    <div class="tab-content tabcontent-border">
                                        <div class="tab-pane active" id="home" role="tabpanel">
                                            <div class="p-20">
                                                <asp:MultiView ActiveViewIndex="0" runat="server" ID="tabsStudents">
                                                    <asp:View runat="server" ID="tabNotas">
                                                        <h3>Agregar resultados de los estudiantes en el proceso</h3>
                                                        <br />
                                                        <p>Seleccione el estudiante:</p>
                                                        <div class="table-responsive">
                                                            <div class="dataTables_wrapper">
                                                                <div class="dt-buttons">
                                                                    <asp:Button ID="btnReportEstudiantes" runat="server" Text="Reporte" CssClass="dt-button buttons-copy buttons-html5"
                                                                        OnClick="btnReportEstudiantes_Click" />
                                                                </div>
                                                                <div id="example23_filter" class="dataTables_filter">
                                                                    <label>
                                                                        Buscar:<asp:TextBox ID="txtNomEstudi" runat="server" OnTextChanged="txtNomEstudi_TextChanged"
                                                                            AutoPostBack="True"></asp:TextBox></label>
                                                                </div>
                                                                <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns="False"
                                                                    DataKeyNames="cedula" OnPageIndexChanging="gvStudents_PageIndexChanging"
                                                                    OnRowCancelingEdit="gvStudents_RowCancelingEdit" OnRowEditing="gvStudents_RowEditing"
                                                                    OnRowUpdating="gvStudents_RowUpdating" CssClass="table table-bordered" AllowPaging="True"
                                                                    AllowSorting="True"
                                                                    PageSize="10" OnRowCommand="gvStudents_RowCommand" OnRowDeleting="gvStudents_RowDeleting" PagerStyle-BorderColor="#cccccc" PagerStyle-BorderStyle="Solid" PagerStyle-BorderWidth="0.5px" PagerStyle-Font-Size="Large">
                                                                    <EmptyDataTemplate>
                                                                        <div class="alert alert-secondary">
                                                                            <h4 class="alert-heading">Sin registro de estudiantes</h4>
                                                                            <p>Por el momento no se ha registro ningún estudiante para esta especialidad</p>
                                                                        </div>
                                                                    </EmptyDataTemplate>
                                                                    <Columns>
                                                                        <asp:BoundField DataField="cedula" HeaderText="Cedula" ReadOnly="true" />
                                                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" ReadOnly="true" />
                                                                        <asp:BoundField DataField="apellidos" HeaderText="Apellidos" ReadOnly="true" />
                                                                        <asp:BoundField DataField="entrevista" HeaderText="Porcentaje de entrevista" ItemStyle-Width="100px"
                                                                            ControlStyle-Width="100px">
                                                                            <ControlStyle Width="100px" />
                                                                            <ItemStyle Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="examen" HeaderText="Promedio del examen" ItemStyle-Width="100px"
                                                                            ControlStyle-Width="100px">
                                                                            <ControlStyle Width="100px" />
                                                                            <ItemStyle Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField ReadOnly="true" DataField="notas" HeaderText="Porcentaje de notas"
                                                                            ItemStyle-Width="100px" ControlStyle-Width="100px">
                                                                            <ControlStyle Width="100px" />
                                                                            <ItemStyle Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField ReadOnly="true" DataField="conducta" HeaderText="Porcentaje de conducta"
                                                                            ItemStyle-Width="100px" ControlStyle-Width="100px">
                                                                            <ControlStyle Width="100px" />
                                                                            <ItemStyle Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField ReadOnly="true" DataField="ausencias" HeaderText="Porcentaje de ausencias"
                                                                            ItemStyle-Width="100px" ControlStyle-Width="100px">
                                                                            <ControlStyle Width="100px" />
                                                                            <ItemStyle Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField ReadOnly="true" DataField="resultado" HeaderText="Nota final"
                                                                            ItemStyle-Width="100px" ControlStyle-Width="100px">
                                                                            <ControlStyle Width="100px" />
                                                                            <ItemStyle Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField ReadOnly="true" DataField="sobre" HeaderText="Consecutivo"
                                                                            ItemStyle-Width="100px" ControlStyle-Width="100px">
                                                                            <ControlStyle Width="100px" />
                                                                            <ItemStyle Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:CommandField HeaderText="Edición" ShowEditButton="true" ItemStyle-Width="100px"
                                                                            ControlStyle-Width="100px">
                                                                            <ControlStyle Width="100px" />
                                                                            <ItemStyle Width="100px" />
                                                                        </asp:CommandField>
                                                                        <asp:ButtonField ControlStyle-CssClass="btn btn-info btn-outline m-b-10 m-l-5" ButtonType="Button"
                                                                            CommandName="notas" HeaderText="Editar notas" Text="Editar" />

                                                                        <asp:TemplateField HeaderText="Eliminar estudiantes">
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnDeleteEstudiante" runat="server" CommandName="delete"
                                                                                    Text="Eliminar" CssClass="btn btn-info btn-danger m-b-10 m-l-5" OnClientClick="return confirm('¿Está seguro de eliminar el estudiante?. Todos los datos se elimnarán, además se habilitará el pin.');"
                                                                                    CommandArgument="<%#Container.DataItemIndex %>" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:ButtonField ControlStyle-CssClass="btn btn-info btn-outline m-b-10 m-l-5" ButtonType="Button"
                                                                            CommandName="report" HeaderText="Datos personales" Text="Reporte" />
                                                                    </Columns>
                                                                    <PagerSettings Mode="Numeric" PageButtonCount="10" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <asp:Panel ID="pnlEditNotas" runat="server" Visible="false">
                                                            <section class="container-fluid">
                                                                <div class="container">
                                                                    <asp:Panel runat="server" ID="pnlNotas">
                                                                        <h4>Notas del estudiante</h4>
                                                                        <form>
                                                                            <div class="row">
                                                                                <div class="col-4 col-12-xsmall border ">
                                                                                    <br />
                                                                                    <h5>Sétimo año</h5>
                                                                                    <p>
                                                                                        Promedio anual de las notas de sétimo
                                                                                    </p>
                                                                                    <hr />
                                                                                    Español:&nbsp                                                                               
                                                                                <input type="number" min="0" max="100" runat="server" id="txtSetEspa" placeholder="Anual español"
                                                                                    class="form-control" step=".01" onkeypress="return pulsar(event)" /><br />
                                                                                    Inglés:&nbsp                                                                               
                                                                                <input type="number" min="0" max="100" runat="server" id="txtSetIng" placeholder="Anual inglés"
                                                                                    class="form-control" step=".01" onkeypress="return pulsar(event)" /><br />

                                                                                    Matemáticas:&nbsp                                                                               
                                                                                <input type="number" min="0" max="100" runat="server" id="txtSetMate" placeholder="Anual matemáticas"
                                                                                    class="form-control" step=".01" onkeypress="return pulsar(event)" /><br />

                                                                                    Estudios sociales:&nbsp                                                                               
                                                                                <input type="number" min="0" max="100" runat="server" id="txtSetEst" placeholder="Anual estudios sociales"
                                                                                    class="form-control" step=".01" onkeypress="return pulsar(event)" /><br />

                                                                                    Cívica&nbsp                                                                               
                                                                                <input type="number" min="0" max="100" runat="server" id="txtSetCiv" placeholder="Anual cívica"
                                                                                    class="form-control" step=".01" onkeypress="return pulsar(event)" /><br />

                                                                                    Ciencias:&nbsp                                                                               
                                                                                <input type="number" min="0" max="100" runat="server" id="txtSetCien" placeholder="Anual ciencias"
                                                                                    class="form-control" step=".01" onkeypress="return pulsar(event)" /><hr />
                                                                                    Conducta:&nbsp                                                                               
                                                                                <input type="number" step=".01" min="0" max="100" runat="server" id="txtSetCond" placeholder="Anual Conducta"
                                                                                    class="form-control" onkeypress="return pulsar(event)" /><hr />
                                                                                    Ausencias:&nbsp 
                                                                            <input type="number" min="0" runat="server" id="txtSetAusIn" placeholder="Ausencias de sétimos año"
                                                                                class="form-control" onkeypress="return pulsar(event)" /><br />
                                                                                </div>
                                                                                <div class="col-4 col-12-xsmall border ">
                                                                                    <br />
                                                                                    <h5>Octavo año</h5>
                                                                                    <p>
                                                                                        Promedio anual de las notas de octavo
                                                                                    </p>
                                                                                    <hr />
                                                                                    Español:&nbsp                                                                               
                                                                                <input type="number" min="0" max="100" runat="server" id="txtOctEspa" placeholder="Anual español"
                                                                                    class="form-control" step=".01" onkeypress="return pulsar(event)" /><br />
                                                                                    Inglés:&nbsp                                                                               
                                                                                <input type="number" min="0" max="100" runat="server" id="txtOctIng" placeholder="Anual inglés"
                                                                                    class="form-control" step=".01" onkeypress="return pulsar(event)" /><br />

                                                                                    Matemáticas:&nbsp                                                                               
                                                                                <input type="number" min="0" max="100" runat="server" id="txtOctMate" placeholder="Anual matemáticas"
                                                                                    class="form-control" step=".01" onkeypress="return pulsar(event)" /><br />

                                                                                    Estudios sociales:&nbsp                                                                               
                                                                                <input type="number" min="0" max="100" runat="server" id="txtOctEst" placeholder="Anual estudios sociales"
                                                                                    class="form-control" step=".01" onkeypress="return pulsar(event)" /><br />

                                                                                    Cívica&nbsp                                                                               
                                                                                <input type="number" min="0" max="100" runat="server" id="txtOctCiv" placeholder="Anual cívica"
                                                                                    class="form-control" step=".01" onkeypress="return pulsar(event)" /><br />

                                                                                    Ciencias:&nbsp                                                                               
                                                                                <input type="number" min="0" max="100" runat="server" id="txtOctCien" placeholder="Anual ciencias"
                                                                                    class="form-control" step=".01" onkeypress="return pulsar(event)" /><hr />
                                                                                    Conducta:&nbsp                                                                               
                                                                                <input type="number" min="0" max="100" runat="server" id="txtOctCond" placeholder="Anual Conducta"
                                                                                    class="form-control" step=".01" onkeypress="return pulsar(event)" /><hr />
                                                                                    Ausencias: &nbsp                                                                           
                                                                            <input type="number" min="0" runat="server" id="txtOctAusIn" placeholder="Ausencias de octavo año"
                                                                                class="form-control" onkeypress="return pulsar(event)" /><br />
                                                                                </div>
                                                                                <div class="col-4 col-12-xsmall border ">
                                                                                    <br />
                                                                                    <h5>Noveno año</h5>
                                                                                    <p>
                                                                                        Promedio de los primeros dos trimestres
                                                                                    </p>
                                                                                    <hr />
                                                                                    Español:&nbsp                                                                               
                                                                                <input type="number" min="0" max="100" runat="server" id="txtNovEspa" placeholder="Promedio español"
                                                                                    class="form-control" step=".01" onkeypress="return pulsar(event)" /><br />
                                                                                    Inglés:&nbsp                                                                               
                                                                                <input type="number" min="0" max="100" runat="server" id="txtNovIng" placeholder="Promedio inglés"
                                                                                    class="form-control" step=".01" onkeypress="return pulsar(event)" /><br />
                                                                                    Matemáticas:&nbsp                                                                               
                                                                                <input type="number" min="0" max="100" runat="server" id="txtNovMate" placeholder="Promedio matemáticas"
                                                                                    class="form-control" step=".01" onkeypress="return pulsar(event)" /><br />
                                                                                    Estudios sociales:&nbsp                                                                               
                                                                                <input type="number" min="0" max="100" runat="server" id="txtNovEst" placeholder="Promedio estudios sociales"
                                                                                    class="form-control" step=".01" onkeypress="return pulsar(event)" /><br />
                                                                                    Cívica&nbsp                                                                               
                                                                                <input type="number" min="0" max="100" runat="server" id="txtNovCiv" placeholder="Promedio cívica"
                                                                                    class="form-control" step=".01" onkeypress="return pulsar(event)" /><br />
                                                                                    Ciencias:&nbsp                                                                               
                                                                                <input type="number" min="0" max="100" runat="server" id="txtNovCien" placeholder="Promedio ciencias"
                                                                                    class="form-control" step=".01" onkeypress="return pulsar(event)" /><hr />
                                                                                    Conducta:&nbsp                                                                               
                                                                                <input type="number" min="0" max="100" runat="server" id="txtNovCond" placeholder="Promedio Conducta"
                                                                                    class="form-control" step=".01" onkeypress="return pulsar(event)" /><hr />
                                                                                    Ausencias:&nbsp                                                                           
                                                                            <input type="number" min="0" runat="server" id="txtNovAusIn" placeholder="Ausencias de noveno año"
                                                                                class="form-control" onkeypress="return pulsar(event)" /><br />
                                                                                </div>
                                                                            </div>
                                                                    </asp:Panel>
                                                                    <hr />
                                                                    <div class="col-lg-auto">
                                                                        <asp:Button ID="btnAgregarNotas" runat="server" CssClass="btn btn-success" Text="Guardar notas"
                                                                            OnClick="btnAgregarNotas_Click" />&nbsp
                   <asp:Button ID="btnCancelarNotas" runat="server" Text="Cancelar" CssClass="btn btn-dark"
                       OnClick="btnCancelarNotas_Click" />
                                                                    </div>
                                                                </div>
                                                            </section>
                                                        </asp:Panel>
                                                    </asp:View>
                                                    <asp:View runat="server" ID="tabAdmitir">
                                                        <h4 class="card-title">Opciones de admisión</h4>
                                                        <ul class="nav nav-tabs customtab" role="tablist">
                                                            <li class="nav-item"><span class="hidden-sm-up"><i class="ti-user"></i></span>
                                                                <asp:Button ID="btnAdnuevosEst" runat="server" Text="Admitir nuevos" CssClass="nav-link active btn btn-success m-b-10 m-l-5"
                                                                    OnClick="btnAdnuevosEst_Click" />
                                                            </li>
                                                            <li class="nav-item">
                                                                <span class="hidden-sm-up"><i class="ti-user"></i></span>
                                                                <asp:Button ID="btnAdEstAdmitidos" runat="server" Text="Estudiantes admitidos" CssClass="nav-link btn btn-outline"
                                                                    OnClick="btnAdEstAdmitidos_Click" />
                                                            </li>
                                                        </ul>
                                                        <div class="tab-content tabcontent-border">
                                                            <div class="tab-pane active" id="home" role="tabpanel">
                                                                <div class="p-20">
                                                                    <asp:MultiView ID="tabsAdmitidos" runat="server" ActiveViewIndex="0">
                                                                        <asp:View runat="server" ID="tabAdmitirs">
                                                                            <h3>Admitir estudiantes</h3>
                                                                            <p>
                                                                                Seleccione los estudiantes que serán admitidos en la institución en el próximo curso
                                                                                lectivo.
                                                                            </p>
                                                                            <div class="table-responsive m-t-35">
                                                                                <div class="dataTables_wrapper">
                                                                                    <div class="dt-buttons">
                                                                                        <asp:Button ID="btnReportStudents" runat="server" Text="Reporte" CssClass="dt-button buttons-copy buttons-html5"
                                                                                            OnClick="btnReportStudents_Click" />
                                                                                    </div>
                                                                                    <div id="example23_filter" class="dataTables_filter">
                                                                                        <label>
                                                                                            Buscar:<asp:TextBox ID="txtNombreEstudiante" runat="server" AutoPostBack="True"
                                                                                                OnTextChanged="txtNombreEstudiante_TextChanged"></asp:TextBox></label>
                                                                                    </div>
                                                                                    <asp:GridView ID="gvStudentsAdmitidos" runat="server" CssClass="display nowrap table table-hover 
                                                                                table-striped table-bordered dataTable"
                                                                                        CellSpacing="0" Width="100%" Style="width: 100%;"
                                                                                        AllowPaging="True" AllowSorting="True" PageSize="10" OnPageIndexChanging="gvStudentsAdmitidos_PageIndexChanging" PagerStyle-BorderColor="#cccccc" PagerStyle-BorderStyle="Solid" PagerStyle-BorderWidth="0.5px" PagerStyle-Font-Size="Large">
                                                                                        <EmptyDataTemplate>
                                                                                            <div class="alert alert-secondary">
                                                                                                <h4 class="alert-heading">Sin registro de estudiantes</h4>
                                                                                                <p>Por el momento no se ha registro ningún estudiante para esta especialidad</p>
                                                                                            </div>
                                                                                        </EmptyDataTemplate>
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Seleccione" ControlStyle-CssClass="sorting_asc">
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="chkSel" runat="server" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <RowStyle CssClass="odd" />
                                                                                        <PagerSettings Mode="Numeric" PageButtonCount="6" />
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </div>
                                                                            <hr />
                                                                            <asp:Button ID="btnAceptarStude" runat="server" CssClass="btn square light-blue txt-white padd-sm"
                                                                                Text="Aceptar estudiantes con los mejores promedios" OnClick="btnAceptarStude_Click" />&nbsp
                                                                            <asp:Button ID="btnAceptarStude2" runat="server" CssClass="btn square light-blue txt-white padd-sm"
                                                                                Text="Aceptar estudiantes seleccionados" OnClick="btnAceptarStude2_Click" />
                                                                        </asp:View>
                                                                        <asp:View runat="server" ID="tabeditarad">
                                                                            <h3>Editar estudiantes admitidos</h3>
                                                                            <p>
                                                                                Elimine los estudiantes ya admitidos en la institución o genere un reporte de los
                                                                                mismos
                                                                            </p>
                                                                            <div class="table-responsive m-t-40">
                                                                                <div class="dataTables_wrapper">
                                                                                    <div class="dt-buttons">
                                                                                        <asp:Button ID="btnReporteAdmitidos" runat="server" Text="Reporte" CssClass="dt-button buttons-copy buttons-html5"
                                                                                            OnClick="btnReporteAdmitidos_Click" />
                                                                                    </div>
                                                                                    <div id="example23_filter" class="dataTables_filter">
                                                                                        <label>
                                                                                            Buscar:<asp:TextBox ID="txtNomAdmitido" runat="server" AutoPostBack="True"
                                                                                                OnTextChanged="txtNomAdmitido_TextChanged"></asp:TextBox></label>
                                                                                    </div>
                                                                                    <asp:GridView ID="gvEditAdmitidos" runat="server" CssClass="display nowrap table table-hover 
                                                                                table-striped table-bordered dataTable"
                                                                                        AllowPaging="True" AllowSorting="True" PageSize="10" OnPageIndexChanging="gvEditAdmitidos_PageIndexChanging"
                                                                                        OnRowDeleting="gvEditAdmitidos_RowDeleting" PagerStyle-BorderColor="#cccccc" PagerStyle-BorderStyle="Solid" PagerStyle-BorderWidth="0.5px" PagerStyle-Font-Size="Large">
                                                                                        <EmptyDataTemplate>
                                                                                            <div class="alert alert-secondary">
                                                                                                <h4 class="alert-heading">Sin registro de estudiantes admitidos</h4>
                                                                                                <p>Por el momento no se ha admitido ningún estudiante para esta especialidad</p>
                                                                                            </div>
                                                                                        </EmptyDataTemplate>
                                                                                        <Columns>
                                                                                            <asp:CommandField HeaderText="Elimine" ShowDeleteButton="true" ControlStyle-CssClass="sorting_asc" />
                                                                                        </Columns>
                                                                                        <RowStyle CssClass="odd" />
                                                                                        <PagerSettings Mode="Numeric" PageButtonCount="6" />
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
                                </asp:View>
                            </asp:MultiView>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlConfigAdmission" runat="server" Visible="false">
                <div class="sweet-overlay" tabindex="-1" style="opacity: 1.04; display: block;">
                    <div class="modal-dialog modal-lg" role="banner">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLongTitle">Editar configuraciones del proceso de admisión</h5>
                                <button type="button" class="btn btn-secondary" style="position: absolute; top: 6px; right: 5px; padding: .75rem 1.2rem;" id="btnCerrar" onserverclick="btnCerrar_Click" runat="server">X</button>
                            </div>
                            <div class="modal-body">
                                <div class="container">
                                    <div class="row">
                                        <div class="col-3">
                                            Inicio:
                        <asp:TextBox ID="txtStartProcess" runat="server" CssClass="form-control"
                            TextMode="Date"></asp:TextBox>
                                        </div>
                                        <div class="col-3">
                                            Finaliza:<asp:TextBox ID="txtFinishProcess" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        </div>
                                        <div class="col-3">
                                            Costo de sobre:<asp:TextBox ID="txtPriceSobre" runat="server" CssClass="form-control"
                                                TextMode="Number"></asp:TextBox>
                                        </div>
                                        <div class="col-3">
                                            Guardar propiedades nuevas:<asp:Button ID="btnSaveAdmission" runat="server" Text="Guardar"
                                                CssClass="btn btn-success btn-rounded m-b-10 m-l-5" OnClick="btnSaveAdmission_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal" runat="server" onserverclick="btnCerrar_Click">Cerrar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
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
