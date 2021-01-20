<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCaseCall4.aspx.vb" Inherits="Modules_Sale_Phone_frmCaseCall4" %>

<%@ Register assembly="Infragistics35.WebUI.UltraWebGrid.v8.3, Version=8.3.20083.1009, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.UltraWebGrid" tagprefix="igtbl" %>

<%@ Register assembly="Infragistics35.Web.v8.3, Version=8.3.20083.1009, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <link rel="stylesheet" href="../../../Styles/stylefont.css" type="text/css" media="screen"/>
     <script language="javascript" type="text/javascript">

         function ShowHistory(IdCar) {
             window.open('frmHistory.aspx?IdCar=' + IdCar, "PhoneCall", 'height=500px ,width =600px');
         }

         function WebImageViewer1_ImageClick() {

             var dialog = $find("WebDialogWindow1");
             dialog.show();

         }

         function btnDialogWindow_onClick() {
             var dialog = $find("WebDialogWindow1");
             dialog.hide();
         }
       
    </script>
</head>
<body >
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate >
            <table style="width: 100%;" >
                <tr>
                    <td valign="top">
                       <table style="width: 100%;" cellspacing="0" class="art-article">
            <tr bgcolor="#CCFFFF">
                <td  class="style1">
                    &nbsp;
                    กำลังโทรภายใน/วินาที</td>
                <td  class="style1">
                    &nbsp;
                    ต้องการพักสาย</td>
                <td  class="style1">
                    สถานะ SoftPhone&nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;
                    <asp:Label ID="lblSec" runat="server" Font-Size="16pt" ForeColor="Red" 
                        Text="5"></asp:Label>
                </td>
                <td class="style1">
                    <asp:CheckBox ID="chkCall" runat="server" AutoPostBack="True" />
                    &nbsp;
                </td>
                <td class="style1">
                    <asp:Label ID="Label5" runat="server"></asp:Label>
                    &nbsp;
                </td>
            </tr>
            </table>
                    </td>
                    <td valign="top">
                        <asp:FormView ID="FormView1" runat="server" Width="100%" DataSourceID="SqlCall">
                            <ItemTemplate>
                                <table cellspacing="0" class="art-article" style="width: 100%;">
                                    <tr  bgcolor="#CCFFFF">
                                        <td bgcolor="#CCFFFF" class="style1" colspan="2">
                                            Talk</td>
                                        <td bgcolor="#CCFFFF" class="style1" colspan="2">
                                            Abandon</td>
                                        <td bgcolor="#CCFFFF" class="style1">
                                            &nbsp;</td>
                                        <td bgcolor="#CCFFFF" class="style1">
                                            &nbsp;</td>
                                    </tr>
                                    <tr  bgcolor="#CCFFFF">
                                        <td bgcolor="#CCFFFF" class="style1">
                                            Call</td>
                                        <td bgcolor="#CCFFFF" class="style1">
                                            CallTime</td>
                                        <td bgcolor="#CCFFFF" class="style1">
                                            Call</td>
                                        <td bgcolor="#CCFFFF" class="style1">
                                            SystemTime</td>
                                        <td bgcolor="#CCFFFF" class="style1">
                                            Productive Time</td>
                                        <td bgcolor="#CCFFFF" class="style1">
                                            TimeAVG</td>
                                    </tr>
                                    <tr>
                                        <td class="style1">
                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="14pt" 
                                                ForeColor="#FF6600" Text='<%# Eval("CallTalk") %>'></asp:Label>
                                        </td>
                                        <td class="style1">
                                            <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="14pt" 
                                                ForeColor="#FF6600" Text='<%# Eval("CallTime") %>'></asp:Label>
                                        </td>
                                        <td class="style1">
                                            <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="14pt" 
                                                ForeColor="#FF6600" Text='<%# Eval("SeatTalk") %>'></asp:Label>
                                        </td>
                                        <td class="style1">
                                            <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="14pt" 
                                                ForeColor="#FF6600" Text='<%# Eval("SystemTime") %>'></asp:Label>
                                        </td>
                                        <td class="style1">
                                            <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="14pt" 
                                                ForeColor="#FF6600" Text='<%# Eval("ProductiveTime") %>'></asp:Label>
                                        </td>
                                        <td class="style1">
                                            <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="14pt" 
                                                ForeColor="#FF6600" Text='<%# Eval("TimeAvg") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:FormView>
                    </td>
                </tr>
            </table>
        
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div>
        สถานะ : 
        <asp:DropDownList ID="ddStatusCall" runat="server" 
        DataTextField="StatusName" DataValueField="StatusID"  AutoPostBack="True" 
            DataSourceID="SqlStatusCall" >
        </asp:DropDownList>

    <asp:GridView ID="GvCase" 
            runat="server" AutoGenerateColumns="False" 
                        DataSourceID="SqlCase" Width="100%" 
            DataKeyNames="IdCar,IsNew,CusID,StatusPage,appid,PendingID" 
            CssClass="art-article">
        <AlternatingRowStyle BackColor="#D7EBFF" />
        <Columns>
                <asp:TemplateField><ItemTemplate>
                    <asp:Button ID="Button4" runat="server" 
                        CommandArgument="<%# Container.DataItemIndex %>" Text="โทร" 
                        CommandName="Phone" />
                    </ItemTemplate><ItemStyle Width="24px" /></asp:TemplateField><asp:TemplateField><ItemTemplate>
                    <asp:Button ID="Button3" runat="server" Text="ดูประวัติ"  OnClientClick ='<%# Eval("ShowHistory") %>'
                        CommandArgument='<%# Eval("IdCar") %>' CommandName="Select" /></ItemTemplate><ItemStyle Width="24px" /></asp:TemplateField><asp:TemplateField HeaderText="ชื่อ-สกุล" SortExpression="FNameTH"><ItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Bind("FNameTH") %>'></asp:Label>&#160;<asp:Label ID="Label2" runat="server" Text='<%# Eval("LNameTH") %>'></asp:Label>
                    &nbsp;<asp:Label ID="lblCarID" runat="server" Text='<%# Eval("CarID") %>'></asp:Label>
                    &nbsp;<asp:Label ID="lblCarBrand" runat="server" Text='<%# Eval("CarBrand") %>'></asp:Label>
                    &nbsp;<asp:Label ID="lblCarSeries" runat="server" Text='<%# Eval("CarSeries") %>'></asp:Label>
                    </ItemTemplate><EditItemTemplate><asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("FNameTH") %>'></asp:TextBox></EditItemTemplate><ItemStyle Width="170px" /></asp:TemplateField>
                <asp:BoundField DataField="StatusName" HeaderText="สถานะ" 
                            SortExpression="StatusName" ><ItemStyle Width="80px" /></asp:BoundField>
                <asp:BoundField DataField="ExpProtect" HeaderText="วันหมดประกัน" 
                            SortExpression="ExpProtect" ReadOnly="True" >
                <ItemStyle Width="80px" /></asp:BoundField><asp:TemplateField HeaderText="วันคุ้มครอง" SortExpression="ProtectDate"><ItemTemplate><asp:Label ID="Label3" runat="server" 
                                    Text='<%# Bind("ProtectDate", "{0:d}") %>'></asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("ProtectDate") %>'></asp:TextBox></EditItemTemplate>
                    <ItemStyle Width="70px" /></asp:TemplateField><asp:BoundField DataField="AppointDate" HeaderText="วันที่นัด" ReadOnly="True" 
                            SortExpression="AppointDate" ><ItemStyle Width="120px" /></asp:BoundField><asp:BoundField DataField="Comments" HeaderText="หมายเหตุ" ReadOnly="True" 
                            SortExpression="Comments" >
                <ItemStyle Width="150px" />
                </asp:BoundField></Columns>
            <HeaderStyle CssClass="td-header" Height="30px" ForeColor="Black" 
            BackColor="#99CCFF" ></HeaderStyle></asp:GridView>
    </div>
    <asp:SqlDataSource ID="SqlCase" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SelectCasePhone5" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:CookieParameter CookieName="userID" Name="userID" />
            <asp:ControlParameter ControlID="ddStatusCall" Name="callStatusID" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:Timer ID="Timer1" runat="server" Interval="2000" Enabled="False">
    </asp:Timer>
    <asp:SqlDataSource ID="SqlCall" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="select 
Convert(VarChar,dateadd(SS,sum(case   when a2.cctbillsec&gt;  0 then a2.calltime end),0),108) as CallTime
,sum(case when a2.cctbillsec &gt; 0 then 1 else 0 end) as CallTalk
,Convert(VarChar,dateadd(SS,avg(a2.calltime),0),108) as TimeAvg
,Convert(VarChar,dateadd(SS,sum(a2.calltime),0),108) as ProductiveTime
,Convert(VarChar,dateadd(SS,sum(case a2.cctbillsec when 0 then a2.calltime end) ,0),108) as SystemTime
,sum(case a2.cctbillsec when 0 then 1 else 0 end) as SeatTalk
from TblUser a1
Left Join TblCallControl a2 on a2.userid = a1.userid  
and Convert(VarChar,a2.cctTimeIn,111) between Convert(VarChar,GetDate(),111) and Convert(VarChar,GetDate(),111)
Where a1.UserID = @userID
" 
       >
        <SelectParameters>
            <asp:CookieParameter CookieName="userID" Name="userID" />

        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="SqlStatusCall" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT StatusID ,StatusName FROM TblStatus where StatusID in (6,7,8) 
    union 
    SELECT '0' as StatusID ,'All' as StatusName
    order by StatusID " >
      
    </asp:SqlDataSource>
    </form>
</body>
</html>
