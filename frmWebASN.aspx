<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmWebASN.aspx.vb" Inherits="Modules_Manager_Report_frmWebASN" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: center">
<table>
    <tr><TD>
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
      
        <asp:Button ID="btn1" runat="server" Text="แสดง" />
        </TD>
        <td class="style1">
                Export</td>
            <td>
                <asp:DropDownList ID="ddExport" runat="server" >
                    <asp:ListItem Value="0">PDF</asp:ListItem>
                    <asp:ListItem Value="1">Excel</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnExport" runat="server" Text="Export" />
            </td>

    </tr>
</table>
  <asp:Panel ID="Panel1" runat="server" Height="500px" ScrollBars="Auto" 
        Width="100%">
<asp:GridView ID="GvSaleApprove" runat="server" 
            AutoGenerateColumns="False" Width="150%" DataSourceID="SqlSalewebasn">
        <Columns>
            <asp:TemplateField HeaderText="ลำดับ">
             <ItemTemplate>
                 <%# Container.DataItemIndex + 1 %>
             </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CusID" HeaderText="CusID" />
            <asp:BoundField DataField="NameTH" HeaderText="ชื่อ-สกุล ลูกค้า" />
            <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
            <asp:BoundField DataField="CarID" HeaderText="ทะเบียน" />
            <asp:BoundField DataField="CarBrand" HeaderText="ยี่ห้อ" />
            <asp:BoundField DataField="CarSeries" HeaderText="CarSeries" />
            <asp:BoundField DataField="StatusName" HeaderText="สถานะ" />
            <asp:BoundField DataField="listDATE" HeaderText="listDATE" DataFormatString="{0:dd/MM/yyyy}"/>
            <asp:BoundField DataField="AppID" HeaderText="AppID" />
            <asp:BoundField DataField="insureTYPE" HeaderText="insureTYPE" />
            <asp:BoundField DataField="AppStatus" HeaderText="AppStatus" />
            <asp:BoundField DataField="Protectdate" HeaderText="Protectdate" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="SuccessDate" HeaderText="SuccessDate" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="เบี้ยประกัน" HeaderText="เบี้ยประกัน" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="เบี้ยบังคับ" HeaderText="เบี้ยบังคับ" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="ยอดชำระเข้า" HeaderText="ยอดชำระเข้า" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="UpdateDate" HeaderText="UpdateDate" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="commentTSR" HeaderText="commentTSR" />
            <asp:BoundField DataField="tsrNAME" HeaderText="tsrNAME" />
            <asp:BoundField />
        </Columns>
        <HeaderStyle BackColor="#336699" ForeColor="White" />
    </asp:GridView>
    </asp:Panel>
 <div>
    <asp:SqlDataSource ID="SqlSalewebasn" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT * from  TbltmpSaleWebAgentReport" 
        InsertCommand="
         select a1.CusID,a1.FNameTH+' '+a1.LNameTH as NameTH,a1.Mobile,a2.CarID,a2.CarBrand,a2.CarSeries,a5.StatusName
         ,left(Replace(Convert(VarChar,a2.CreateDate,103),'',''),6)+ Convert(Varchar,(year(a2.CreateDate)+543)) as listDATE
         ,a3.AppID,a6.TypeName as insureTYPE,'AppStatus'= case a3.appstatus when 1 then 'A' when 0 then 'C' else '' end
         ,left(Replace(Convert(VarChar,a3.Protectdate,103),'',''),6)+ Convert(Varchar,(year(a3.Protectdate)+543)) as Protectdate
         ,left(Replace(Convert(VarChar,a3.SuccessDate,103),'',''),6)+ Convert(Varchar,(year(a3.SuccessDate)+543)) as SuccessDate
         ,'เบี้ยประกัน' = case a3.isprovalue when 1 then a3.provalue else 0 end,
         'เบี้ยบังคับ' = case a3.iscarpet when 1 then a3.carpet else 0 end,
         'ยอดชำระเข้า'=case when (select sum(payvalue) from tblpayment where appid=a3.appid) is null then 0 else (select sum(payvalue) from tblpayment where appid=a3.appid) end
         ,left(Replace(Convert(VarChar,a2.UpdateDate,103),'',''),6)+ Convert(Varchar,(year(a2.UpdateDate)+543)) as UpdateDate
         ,a2.Comments as commentTSR
         ,a7.FName + '  '+ a7.LName as tsrNAME 
         into TbltmpSaleWebAgentReport
from tblcustomer a1 inner join tblcar a2
  on a1.cusid = a2.cusid left join tblapplication a3
  on a2.idcar = a3.idcar left join tbl_producttype a4
  on a3.productid = a4.protypeid inner join tblstatus a5
  on a2.curstatus = a5.statusid left join tbl_type a6
  on a3.typeprovalue = a6.typeid left join TblUser a7
  on a2.AssignTo = a7.UserID
where a2.groupid =2866  
  and Convert(varchar,a2.createdate,111) between @date1 and @date2
order by a1.createdate"
DeleteCommand="DROP TABLE TbltmpSaleWebAgentReport"
>
       <InsertParameters>
            <asp:Parameter Name="date1" />
            <asp:Parameter Name="date2" />           
        </InsertParameters>
    </asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        DeleteCommand="DELETE FROM  TbltmpSaleWebAgentReport"
>
     
    </asp:SqlDataSource>


</div>
</div>
      
</asp:Content>

