<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmRptCallControl.aspx.vb" Inherits="Modules_Manager_Report_frmRptCallControl" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="text-align: center">
ค้นหาวันที่ : 
    <asp:TextBox ID="txtdate1" runat="server"></asp:TextBox>
    <asp:MaskedEditExtender ID="txtdate1_MaskedEditExtender" runat="server" 
        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
        Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate1">
    </asp:MaskedEditExtender>
    <asp:CalendarExtender ID="txtdate1_CalendarExtender" runat="server" 
        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate1">
    </asp:CalendarExtender>
    <asp:Button ID="Button1" runat="server" Text="แสดง" />
    <asp:Button ID="Button2" runat="server" Text="Export" />
</div>
<div>
    <asp:GridView ID="GvCallControl" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlCallControl" Width="100%">
        <Columns>
            <asp:BoundField DataField="cctime" HeaderText="ช่วงเวลา" 
                SortExpression="cctime" />
            <asp:BoundField DataField="ip" HeaderText="ip" SortExpression="ip" />
            <asp:BoundField DataField="project" HeaderText="project" 
                SortExpression="project" />
            <asp:BoundField DataField="cctid" HeaderText="จำนวนCall" 
                SortExpression="cctid" />
            <asp:BoundField DataField="Chanel" HeaderText="Chanel" 
                SortExpression="Chanel" />
            <asp:BoundField DataField="cctime1" HeaderText="เวลา" 
                SortExpression="cctime1" DataFormatString="{0:HH:mm:ss}" />
            <asp:BoundField DataField="tsronline" HeaderText="tsronline" 
                SortExpression="tsronline" />
            <asp:BoundField DataField="ExtenCall" HeaderText="จำนวนExten" 
                SortExpression="ExtenCall" />
        </Columns>
        <HeaderStyle BackColor="#99CCFF" />
    </asp:GridView>
</div>
    <asp:SqlDataSource ID="SqlCallControl" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Callcontrol %>" SelectCommand="
        select a1.*
,a2.ExtenCall
,a2.Chanel
 from [dbo].[RptCallControl](@date1) a1
Inner Join TblServer a2 on a1.ip = a2.ServerIP
order by cctime">
        <SelectParameters>
            <asp:Parameter Name="date1" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>

