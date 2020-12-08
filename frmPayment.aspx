<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPayment.aspx.vb" Inherits="Modules_Manager_Manage_Tsr_frmPayment" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../../../Styles/style.css" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptLocalization="true" EnableScriptGlobalization="true" >
    </asp:ScriptManager>
    <div style="background-color: #FFFFFF">
    <div style="text-align: center">ค้นหา วันที่ : 
        <asp:TextBox ID="txtdate1" runat="server" Width="100"></asp:TextBox>
        <asp:MaskedEditExtender ID="txtdate1_MaskedEditExtender" runat="server" 
            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate1">
        </asp:MaskedEditExtender>
        <asp:CalendarExtender ID="txtdate1_CalendarExtender" runat="server" 
            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate1">
        </asp:CalendarExtender>
        -<asp:TextBox ID="txtdate2" runat="server" Width="100"></asp:TextBox>
        <asp:MaskedEditExtender ID="txtdate2_MaskedEditExtender" runat="server" 
            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate2">
        </asp:MaskedEditExtender>
        <asp:CalendarExtender ID="txtdate2_CalendarExtender" runat="server" 
            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate2">
        </asp:CalendarExtender>
        <asp:Button ID="Button1" runat="server" Text="แสดง" />
        <asp:Button ID="Button2" runat="server" Text="Export" />
        </div>
    <div>
        <asp:GridView ID="GvAppPay" runat="server" Width="100%" 
            AutoGenerateColumns="False" DataKeyNames="RunID" DataSourceID="SqlAppPay" 
            EmptyDataText="ไม่พบข้อมูล">
            <Columns>
                <asp:BoundField DataField="CreateDate" HeaderText="วันที่" 
                    SortExpression="CreateDate" />
                <asp:BoundField DataField="CusName" HeaderText="ชื่อ-สกุล ลูกค้า" 
                    ReadOnly="True" SortExpression="CusName" />
                <asp:BoundField DataField="CarID" HeaderText="ทะเบียนรถ" 
                    SortExpression="CarID" />
                <asp:BoundField DataField="PayNo1" HeaderText="งวดเดิม" 
                    SortExpression="PayNo1" />
                <asp:BoundField DataField="PayNo2" HeaderText="งวดใหม่" 
                    SortExpression="PayNo2" />
                <asp:BoundField DataField="TsrName" HeaderText="TsrName" ReadOnly="True" 
                    SortExpression="TsrName" />
            </Columns>
            <HeaderStyle BackColor="#99CCFF" />
        </asp:GridView>
        </div>
    </div>


    <asp:SqlDataSource ID="SqlAppPay" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="select a1.* 
,a2.CarID 
,a3.FNameTH + ' ' + a3.LNameTH as CusName
,a4.FName + ' ' + a4.LName as TsrName
from TblLogAppPay a1
Inner Join TblCar a2 on a1.IdCar = a2 .IdCar 
Inner Join TblCustomer a3 on a2.CusID = a3.CusID 
Inner Join TblUser a4 on a1.CreateID = a4.UserID 
Where CONVERT(VarChar,a1.createDate ,111) between @date1 and  @date2
order by a1.CreateDate
">
        <SelectParameters>
            <asp:Parameter Name="date1" />
            <asp:Parameter Name="date2" />
        </SelectParameters>
    </asp:SqlDataSource>
    </form>
</body>
</html>
