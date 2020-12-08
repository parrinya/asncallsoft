<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportSourcebyList.aspx.vb" Inherits="Modules_Manager_Report_ReportSourcebyList" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <div>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
       AutoDataBind="true" GroupTreeImagesFolderUrl="" HasRefreshButton="True" 
        Height="1269px" ToolbarImagesFolderUrl="" 
        ToolPanelView="None" ToolPanelWidth="200px" Width="350px" 
            EnableDatabaseLogonPrompt="False" 
            ReuseParameterValuesOnRefresh="True" ShowAllPageIds="True" />
     
    </div>
    </form>
</body>
</html>
