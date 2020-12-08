<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmSuccessReport.aspx.vb" Inherits="Modules_Manager_Report_frmSuccessReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: center">ค้นหาวันที่
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
    <asp:Button ID="Button2" runat="server" Text="Export" />
    </div>
<div>
    <asp:GridView ID="GvCase" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlCase" Width="100%" ShowFooter="True">
        <Columns>
            <asp:BoundField DataField="GroupName" HeaderText="GroupName" 
                SortExpression="GroupName" />
            <asp:BoundField DataField="SubmitTotal" HeaderText="Total" ReadOnly="True" 
                SortExpression="SubmitTotal" />
            <asp:BoundField DataField="SubmitTypeProvalue1" HeaderText="ชั้น1" 
                ReadOnly="True" SortExpression="SubmitTypeProvalue1" />
            <asp:BoundField DataField="SubmitTypeProvalue" HeaderText="ชั้นอื่นๆ" 
                ReadOnly="True" SortExpression="SubmitTypeProvalue" />
            <asp:BoundField DataField="SubmitProValue" HeaderText="เบี้ยประกัน" 
                ReadOnly="True" SortExpression="SubmitProValue" 
                DataFormatString="{0:N2}" />
            <asp:BoundField DataField="SubmitCarpet" HeaderText="เบี้ย พรบ" ReadOnly="True" 
                SortExpression="SubmitCarpet" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="ProValueTotal" HeaderText="เบี้ยรวม" ReadOnly="True" 
                SortExpression="ProValueTotal" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="PayCase" HeaderText="Total" ReadOnly="True" 
                SortExpression="PayCase" />
            <asp:BoundField DataField="PayValue" HeaderText="Approve Value" ReadOnly="True" 
                SortExpression="PayValue" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="CancelCase" HeaderText="Total" ReadOnly="True" 
                SortExpression="CancelCase" />
            <asp:BoundField DataField="CancelProValue" HeaderText="Cancel Value" 
                ReadOnly="True" SortExpression="CancelProValue" 
                DataFormatString="{0:N2}" />
        </Columns>
        <HeaderStyle BackColor="#99CCFF" />
    </asp:GridView>
</div>
    <asp:SqlDataSource ID="SqlCase" runat="server" 
    ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="select a3.GroupName 
,count(case a1.StatusQc when 7 then a1.AppID end) as SubmitTotal
,count( case  when a1.StatusQc = 7 and a1.Typeprovalue = 1 and IsProvalue in(1)  then a1.AppID end) as SubmitTypeProvalue1
,sum(case  when a1.StatusQc = 7 and a1.Typeprovalue = 1 and IsProvalue in(1)  then a1.ProValue else 0 end) as  SubmitProValue1
,count( case  when a1.StatusQc = 7 and a1.Typeprovalue not in( 1 )then a1.AppID end) as SubmitTypeProvalue
,sum(case  when a1.StatusQc = 7 and IsProvalue in(1)   then a1.ProValue else 0 end) as  SubmitProValue
,sum(case  when a1.StatusQc = 7 and IsCarpet in(1)   then a1.Carpet else 0 end) as  SubmitCarpet
,sum((case  when a1.StatusQc = 7 and IsCarpet in(1)   then a1.Carpet else 0 end) + (case  when a1.StatusQc = 7 and IsProvalue in(1)   then a1.ProValue else 0 end) ) as ProValueTotal
 ,count(a4.AppID) as PayCase
 ,sum(case when a4.AppID is not null then  a4.PayValue  else 0 end)   as PayValue
 ,count(distinct(case when a1.IsProValue = 0 and a5.CancelType = 2 then   a5.AppID end)) as CancelCase
 ,sum( (case when a1.IsProValue = 0 and a5.AppID is not null then a5.ProValue  else 0 end)) as CancelProValue
 from tblapplication a1
Inner Join TblCar a2 on a1.IdCar = a2.IdCar
Inner Join TblSourceGroup a3 on a2.GroupID = a3.GroupID
Left Join TblPayment a4 on a1.AppID = a4.AppID and a4.PayNo = 1
Left Join  GetTblCancelApp(@date1,@date2) a5 on a1.AppID = a5.AppID
where Convert(VarChar, a1.SuccessDate,111) between @date1 and @date2
group by a3.groupname">
        <SelectParameters>
            <asp:Parameter Name="date1" />
            <asp:Parameter Name="date2" />
        </SelectParameters>
</asp:SqlDataSource>
</asp:Content>

