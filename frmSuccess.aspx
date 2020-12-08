<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmSuccess.aspx.vb" Inherits="Modules_Manager_Manage_Tsr_frmSuccess" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="margin-top: 30px;">

 <font style="color: #0066FF; font-weight: bold; font-size: 20px;">Pending Success</font>
     
      <font style="font-weight: 700"> จำนวน Case :</font><asp:Label ID="lblCase" runat="server" ForeColor="#FF3300" Text="0"></asp:Label>
    </div>
 <div>
     <asp:Panel ID="Panel1" runat="server" Width="100%" Height="500" ScrollBars="Auto">
<asp:GridView ID="GvSuccess" runat="server" AutoGenerateColumns="False" 
         DataKeyNames="AppID,IdCar" DataSourceID="SqlSuccess" Width="100%">
         <Columns>
             <asp:TemplateField>
                 <ItemTemplate>
                     <asp:Button ID="Button1" runat="server" 
                         CommandArgument="<%# Container.DataItemIndex %>" CommandName="Select" 
                         Text="เลือก" />
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField>
                 <ItemTemplate>
                     <asp:Button ID="Button2" runat="server" CommandName="Update" Text="Assign" />
                     <asp:ConfirmButtonExtender ID="Button2_ConfirmButtonExtender" runat="server" 
                         ConfirmText="คุณต้องการ Assign ต่อ หรือไม่" Enabled="True" 
                         TargetControlID="Button2">
                     </asp:ConfirmButtonExtender>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:BoundField DataField="ProtectDate" HeaderText="วันคุ้มครอง" 
                 SortExpression="ProtectDate" DataFormatString="{0:dd/MM/yyyy}" />
             <asp:BoundField DataField="DiffProtectDate" HeaderText="วัน" 
                 SortExpression="DiffProtectDate" />
             <asp:BoundField DataField="CusName" HeaderText="ชื่อ-สกุล" 
                 SortExpression="CusName" />
             <asp:BoundField DataField="CarID" HeaderText="ทะเบียนรถ" 
                 SortExpression="CarID" />
             <asp:BoundField DataField="CarBrand" HeaderText="ยี้ห้อ-รุ่น" 
                 SortExpression="CarBrand" />
             <asp:BoundField DataField="ProTypename" HeaderText="บริษัทประกัน" 
                 SortExpression="ProTypename" />
             <asp:BoundField DataField="TypeName" HeaderText="ประเภท" 
                 SortExpression="TypeName" />
             <asp:BoundField DataField="ProValue" DataFormatString="{0:N2}" 
                 HeaderText="เบี้ยขาย" SortExpression="ProValue" />
             <asp:BoundField DataField="CarPet" DataFormatString="{0:N2}" HeaderText="พรบ" 
                 SortExpression="CarPet" />
             <asp:TemplateField>
                 <ItemTemplate>
                     <asp:DropDownList ID="ddTsr" runat="server" CssClass="jamp" 
                         DataSourceID="SqlUser" DataTextField="TsrName" DataValueField="userID" 
                         SelectedValue='<%# Bind("userID") %>'>
                     </asp:DropDownList>
                 </ItemTemplate>
             </asp:TemplateField>
         </Columns>
         <HeaderStyle BackColor="#99CCFF" Height="30px" />
         <SelectedRowStyle BackColor="#99CCFF" />
     </asp:GridView>
     </asp:Panel>
     
 </div>

<asp:SqlDataSource ID="SqlSuccess" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="select a1.ProtectDate
                        ,a1.AppID
                        ,a1.ProValue 
                        ,a1.CarPet
                        ,a2.CarID
                        ,a2.CarBrand + '-' + a2.CarSeries as CarBrand
                        ,a3.FnameTH + ' ' + a3.LNameTH as CusName
                        ,a5.UserID
                        ,a2.AppointDate 
                        , DATEDIFF (day,GETDATE() ,a1.ProtectDate ) as DiffProtectDate
                        ,a6.ProTypename
                        ,a7.TypeName
                        ,a2.IdCar
                        from TblApplication a1
                        Inner Join TblCar a2 on a1.Idcar = a2.IdCar 
                        Inner Join TblCustomer a3 on a2.CusID = a3.CusID 
                        Left Join Tblpayment a4 on  a4.payno = 1 and a1.appid = a4.appid
                        Inner Join TblUser a5 on a2.AssignTo = a5.UserID 
                        Inner Join Tbl_ProductType a6 on a1.ProductID = a6.ProTypeID
                        Inner Join Tbl_Type a7 on a1.TypeProvalue = a7.TypeID
                        Where a5.TypeTsr = @TypeTsr
                        and a4.AppID is null 
                        and DATEDIFF (day,GETDATE() ,a1.ProtectDate) between 0 and 7
                        and a1.AppStatus = 1
                        and a2.CurStatus = 3 and a1.StatusQc not in(0,2,3)
                        and a5.SupID = @userID
                        order by Convert(VarChar,ProtectDate,111) desc,a3.FNameTH,a3.LNameTH"
                        
    UpdateCommand="UPDATE TblCar
                         SET AssignTo = @userID 
                         WHERE (IdCar = @IdCar)"
                         >
    <SelectParameters>
        <asp:CookieParameter CookieName="TypeTsr" Name="TypeTsr" />
        <asp:CookieParameter CookieName="userID" Name="userID" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="userID" />
        <asp:Parameter Name="IdCar" />
    </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlUser" runat="server"   ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand=" Select 
                            a1.FName + ' ' + a1.LName as TsrName
                            ,a1.userID
                            from TblUser a1
                            where TypeTsr in(3) and UserLevelID = 5
                            Order By a1.FName,a1.LName
                            ">
         
            
        </asp:SqlDataSource>
</asp:Content>


