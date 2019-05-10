<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/estudiantes/estudiantes.Master" AutoEventWireup="true" CodeBehind="estudiantes.aspx.cs" Inherits="Cote_Cloud.estudiantes.estudiantes1" %>

<%@ Register Src="~/User/error.ascx" TagPrefix="uc1" TagName="error" %>
<%@ Register Src="~/User/completado.ascx" TagPrefix="uc1" TagName="completado" %>
<%@ Register Src="~/User/informativo.ascx" TagPrefix="uc1" TagName="informativo" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:error runat="server" ID="error" Visible="false"/>
    <uc1:completado runat="server" ID="completado" Visible="false"/>
    <uc1:informativo runat="server" ID="informativo" Visible="false"/>
</asp:Content>