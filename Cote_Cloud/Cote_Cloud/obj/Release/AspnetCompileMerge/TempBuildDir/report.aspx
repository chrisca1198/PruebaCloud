<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="report.aspx.cs" Inherits="Cote_Cloud.report" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte</title>
    <link rel="icon" type="image/png" sizes="16x16" href="images/escudo.ico" />
    <link href="css/lib/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link href="css/helper.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container">
            <div class="row">
                <div class="col-10">
                    <rsweb:ReportViewer ID="reporte" ClientIDMode="AutoID" Width="100%" Height="900px" runat="server" Visible="true"></rsweb:ReportViewer>
                </div>
                <div class="col-2">
                    <h3 class="title">Guardar archivo como:</h3>
                    <asp:Button ID="btnWord" runat="server" Text="Word" CssClass="btn btn-primary" OnClick="btnWord_Click" />
                    <asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="btn btn-success" OnClick="btnExcel_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
