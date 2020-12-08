<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmCorenoteAndPayment.aspx.vb" Inherits="Modules_Manager_Manage_Case_frmCorenoteAndPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
    <tr>
        <td>ค้นหาตาม:</td>
        <td>
            <asp:CheckBox ID="chkAppID" runat="server" Text="AppID" />
        </td>
        <td>
            <asp:TextBox ID="txtAppID" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:Button ID="btnFind" runat="server" Text="ค้นหารายการ" />
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:CheckBox ID="chkCarID" runat="server" Text="ทะเบียนรถยนต์" />
        </td>
        <td>
            <asp:TextBox ID="txtCariD" runat="server"></asp:TextBox>
        </td>
        <td>
           
        </td>
    </tr>
     <tr>
        <td></td>
        <td>
            <asp:CheckBox ID="chkCusID" runat="server" Text="ชื่อ" />
        </td>
        <td>
            <asp:TextBox ID="txtcusname" runat="server"></asp:TextBox>
        </td>
        <td>
           
        </td>
    </tr>
      <tr><td colspan="4">
        <asp:Label ID="Label2" runat="server" Text="ผลจากการค้นหารายการ" BackColor="#FFCCCC" 
            Font-Bold="True" Font-Italic="False" Font-Size="Medium"></asp:Label></td></tr>
    <tr>
        <td colspan="4">
            <asp:GridView ID="GvData" runat="server" DataKeyNames="AppID" EmptyDataText="ไม่พบ รายการ"
                DataSourceID="SqlCustomer" AutoGenerateColumns="False" >
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnselect" runat="server" Text="เลือก" CommandArgument="<%# Container.DataItemIndex %>" CommandName="select" />
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:BoundField DataField="AppID" HeaderText="AppID" />
                      <asp:BoundField DataField="ProTypeName" HeaderText="บ.ประกัน" />
                      <asp:BoundField DataField="carid" HeaderText="ทะเบียน"/>
                      <asp:BoundField DataField="FNameTH"  HeaderText="ชื่อ"/>
                      <asp:BoundField DataField="LNameTH"  HeaderText="สกุล"/>
                      <asp:BoundField DataField="SuccessDate"  HeaderText="วันที่Success"/>

                
                </Columns>
                

            </asp:GridView>
        </td>
     </tr>
    <tr><td colspan="4">
        <asp:Label ID="Label1" runat="server" Text="รายการ File " BackColor="#FFCCCC" 
            Font-Bold="True" Font-Italic="False" Font-Size="Medium"></asp:Label></td></tr>
    <tr>
        <td colspan="4">
             <asp:GridView ID="GvCase" runat="server"  DataKeyNames="FileNames"  AutoGenerateColumns="False" EmptyDataText="ไม่พบ เอกสาร">
             <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="Button1" runat="server" Text="โหลดข้อมูล" CommandArgument="<%# Container.DataItemIndex %>" CommandName="download" />
                </ItemTemplate>


            </asp:TemplateField>
            <asp:BoundField DataField="APPID" />
            <asp:BoundField DataField="FileNames" />
            
        </Columns>


    </asp:GridView>
        </td>
    </tr>
</table>
<asp:SqlDataSource ID="SqlCustomer" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="select top 5  a1.AppID,a4.ProTypeName,a2.carid,a3.FNameTH,a3.LNameTH,convert(varchar,a1.SuccessDate,103) as SuccessDate,a1.Statusqc 
from tblapplication a1 
inner join tblcar a2 on a1.idcar=a2.idcar
inner join tblcustomer a3 on a1.cusid=a3.cusid
inner join Tbl_ProductType a4 on a1.ProDuctID=a4.ProTypeID
inner join tbluser a5 on a5.userid=a2.AssignTo
where a1.AppStatus=1 and a1.Statusqc in(1,7)
and a5.TypeTsr<>3  ">
       
    </asp:SqlDataSource>
</asp:Content>

