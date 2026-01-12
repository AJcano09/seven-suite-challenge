<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteClientes.aspx.cs" Inherits="SevenSuite.Web.ReporteClientes" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager
        ID="ScriptManager1"
        runat="server" />
        <rsweb:ReportViewer
            ID="rvClientes"
            runat="server"
            Width="100%"
            Height="600px"
            ProcessingMode="Local" />
    </form>
</body>
</html>
