<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmAgentCall.aspx.vb" Inherits="Modules_Manager_Report_frmAgentCall" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="margin-top: 25px">
<font style="color: #0099FF; font-weight: bold; font-size: 18px">Agent Call</font>
<font style="color: Black ; font-weight: bold; font-size: 18px">Report</font>
</div>
    <div style="border-top: 2px solid #66CCFF; text-align: center;">
        ค้นหาวันที่ :
        <asp:TextBox ID="txtdate1" runat="server" Width="80px"></asp:TextBox>
        <asp:MaskedEditExtender ID="txtdate1_MaskedEditExtender" 
            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate1">
    </asp:MaskedEditExtender>
        <asp:CalendarExtender ID="txtdate1_CalendarExtender" 
            runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate1">
    </asp:CalendarExtender>
        -<asp:TextBox ID="txtdate2" runat="server" Width="80px"></asp:TextBox>
        <asp:MaskedEditExtender ID="txtdate2_MaskedEditExtender" 
            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate2">
    </asp:MaskedEditExtender>
        <asp:CalendarExtender ID="txtdate2_CalendarExtender" 
            runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate2">
    </asp:CalendarExtender>
        &nbsp;<asp:Button ID="Button1" runat="server" Text="แสดง" />
    </div>

    <div>
<iframe  src ="<%=strReport %>" frameborder="0" height="800" width="100%"></iframe>
</div>
</asp:Content>

