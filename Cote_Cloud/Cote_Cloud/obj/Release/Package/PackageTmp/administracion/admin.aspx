<%@ Page EnableEventValidation="true" Title="" Language="C#" MasterPageFile="~/administracion/admin.Master" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="Cote_Cloud.administracion.admin1" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    Inicio
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <h1 class="card-title">Prematrícula</h1>
    <div class="row">
        <div class="col-md-4">
            <div class="card p-30">
                <div class="media">
                    <div class="media-left meida media-middle">
                        <span><i class="fa fa-usd f-s-40 color-primary"></i></span>
                    </div>
                    <div class="media-body media-text-right">
                        <h2 runat="server" id="Ganancias">0</h2>
                        <p class="m-b-0">Ganancia estimada</p>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="card p-30">
                <div class="media">
                    <div class="media-left meida media-middle">
                        <span><i class="fa fa-user f-s-40 color-danger"></i></span>
                    </div>
                    <div class="media-body media-text-right">
                        <h2 id="estudiantesPrematriculados" runat="server">0</h2>
                        <p class="m-b-0">Estudiantes prematriculados</p>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="card p-30">
                <div class="media">
                    <div class="media-left meida media-middle">
                        <span><i class="fa fa-angle-double-down f-s-40 color-warning"></i></span>
                    </div>
                    <div class="media-body media-text-right">
                        <h2 runat="server" id="PinesDisponibles">0</h2>
                        <p class="m-b-0">Códigos pines disponibles</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-6">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Cantidad de estudiantes prematriculados, por especialidad</h4>
                    <div id="morris-bar-chart" style="position: relative; -webkit-tap-highlight-color: rgba(0, 0, 0, 0);">
                        <asp:Chart CssClass="table table-bordered table-condensed table-responsive" ID="Especialidad" runat="server"
                            ImageLocation="~/images/bookingSystem/2.png" ImageStorageMode="UseImageLocation"
                            Palette="Pastel" Width="635px" PaletteCustomColors="Red; Blue; Yellow; Lime" RightToLeft="Yes" BackGradientStyle="Center" BackHatchStyle="Wave" BackImageAlignment="Center" BackSecondaryColor="White" BorderlineColor="Black">
                            <Series>
                                <asp:Series Name="PrimeraOpcion" YValueType="Int32" Color="#334a60" IsVisibleInLegend="true" LabelForeColor="MediumBlue" Legend="Legend1" LegendText="Primera opción" MarkerBorderColor="255, 128, 128" MarkerColor="255, 128, 128" YValuesPerPoint="2">
                                    <Points>
                                    </Points>
                                </asp:Series>
                                <asp:Series Name="SegundaOpcion" YValueType="Int32" Color="#a2a8ad" IsVisibleInLegend="true" Legend="Legend1" YValuesPerPoint="2">
                                    <Points>
                                    </Points>
                                </asp:Series>
                            </Series>
                            <MapAreas>
                                <asp:MapArea Coordinates="0,0,0,0" />
                            </MapAreas>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                            </ChartAreas>
                            <Legends>
                                <asp:Legend BorderColor="Black" ItemColumnSeparator="Line" Name="Legend1" Title="Prematriculados">
                                </asp:Legend>
                            </Legends>
                            <Titles>
                                <asp:Title Name="Estudiantes prematriculados">
                                </asp:Title>
                            </Titles>
                            <BorderSkin BackColor="White" BorderColor="White" />
                        </asp:Chart>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-6">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Cantidad de estudiantes admitidos</h4>
                    <div id="morris-bar-chart" style="position: relative; -webkit-tap-highlight-color: rgba(0, 0, 0, 0);">
                        <asp:Chart CssClass="table table-bordered table-condensed table-responsive" ID="Admitidos" runat="server"
                            ImageLocation="~/images/bookingSystem/3.png" ImageStorageMode="UseImageLocation"
                            Palette="EarthTones" Width="790px" PaletteCustomColors="Red" RightToLeft="Yes">
                            <Series>
                                <asp:Series Name="Admitidos" YValueType="Int32" Color="#26dad2" IsVisibleInLegend="true" LabelForeColor="MediumBlue"
                                    MarkerBorderColor="255, 128, 128" MarkerColor="255, 128, 128" ChartType="SplineArea" YValuesPerPoint="4">
                                    <Points>
                                    </Points>
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                            </ChartAreas>
                            <Titles>
                                <asp:Title Name="Estudiantes admitidos">
                                </asp:Title>
                            </Titles>
                        </asp:Chart>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
