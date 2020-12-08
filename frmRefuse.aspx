<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmRefuse.aspx.vb" Inherits="Modules_Manager_Manage_Tsr_frmRefuse" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
        Width="100%">
        <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
        <ContentTemplate >
          <div style="text-align: center; font-weight: bold; background-color: #99CCFF;">ค้นหาข้อมูล วันคุ้มครอง : 
      <asp:TextBox ID="txtdate1" runat="server" Width="80px"></asp:TextBox>
      <asp:MaskedEditExtender ID="txtdate1_MaskedEditExtender" runat="server" 
          CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
          CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
          CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
          Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate1">
      </asp:MaskedEditExtender>
      <asp:CalendarExtender ID="txtdate1_CalendarExtender" runat="server" 
          Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate1">
      </asp:CalendarExtender>
      -<asp:TextBox ID="txtdate2" runat="server" Width="80px"></asp:TextBox>
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
      จำนวน Case :
      
    </div>
    <div style="overflow-y: scroll; ">
    <asp:GridView ID="GvRefuse" runat="server" AutoGenerateColumns="False" DataKeyNames="IdCar" 
                         DataSourceID="SqlRefuse" Width="100%"  
                 PageSize="100">
                 <AlternatingRowStyle BackColor="#E8F3FF" />
                 <Columns>
                     
                     <asp:TemplateField>
                         <ItemTemplate>
                             <asp:Button ID="Button2" runat="server" CommandName="Select" Text="เลือก" 
                                 CommandArgument='<%# Eval("IdCar") %>' />
                         </ItemTemplate>
                     </asp:TemplateField>
                     
                     <asp:TemplateField>
                         <ItemTemplate>
                             <asp:Button ID="Button3" runat="server" CommandName="update" Text="Assign" />
                             <asp:ConfirmButtonExtender ID="Button3_ConfirmButtonExtender" runat="server" 
                                 ConfirmText="คุณต้องการ Assign Case ไปให้ Tsrคนอื่นหรือไม่" 
                                 TargetControlID="Button3">
                             </asp:ConfirmButtonExtender>
                         </ItemTemplate>
                     </asp:TemplateField>
                     
                     <asp:BoundField DataField="UpdateDate" HeaderText="วันที่โทร" 
                         SortExpression="UpdateDate" />
                     
                     <asp:BoundField DataField="CusName" HeaderText="ชื่อ-สกุล" ReadOnly="True" 
                         SortExpression="CusName" />
                     <asp:BoundField DataField="CarBrand" HeaderText="ยี่ห้อ-รุ่น" ReadOnly="True" 
                         SortExpression="CarBrand" />
                     <asp:BoundField DataField="AppointDate" HeaderText="วันนัด" 
                         SortExpression="AppointDate" />
                     <asp:BoundField DataField="Comments" HeaderText="หมายเหตุ" 
                         SortExpression="Comments" />
                     <asp:BoundField DataField="CarBuyDate" DataFormatString="{0:dd/MM/yyyy}" 
                         HeaderText="วันหมดประกัน" SortExpression="CarBuyDate" />
                     <asp:TemplateField HeaderText="TsrName" SortExpression="TsrName">
                         <ItemTemplate>
                             <asp:DropDownList ID="ddTsr" runat="server" CssClass="jamp" 
                                 DataSourceID="SqlUser" DataTextField="TsrName" DataValueField="userID" 
                                 SelectedValue='<%# Bind("userID") %>'>
                             </asp:DropDownList>
                         </ItemTemplate>
                        
                     </asp:TemplateField>
                 </Columns>
                 <HeaderStyle BackColor="#99CCFF" />
                 <SelectedRowStyle BackColor="#BFDFFF" />
                 </asp:GridView>
    </div>
        </ContentTemplate>
        <HeaderTemplate >
        Refuse Case(<asp:Label ID="lblCase" runat="server" ForeColor="#FF3300" Text="0"></asp:Label>)
        </HeaderTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
        <ContentTemplate >
        <asp:GridView ID="GvFollow" runat="server" AutoGenerateColumns="False" DataKeyNames="IdCar" 
                         DataSourceID="SqlFollow" Width="100%" 
                 PageSize="100">
                 <AlternatingRowStyle BackColor="#E8F3FF" />
                 <Columns>
                     
                     <asp:TemplateField>
                         <ItemTemplate>
                             <asp:Button ID="Button2" runat="server" CommandName="Select" Text="เลือก" 
                                 CommandArgument='<%# Eval("IdCar") %>' />
                         </ItemTemplate>
                     </asp:TemplateField>
                     
                     <asp:TemplateField>
                         <ItemTemplate>
                             <asp:Button ID="Button3" runat="server" CommandName="update" Text="Assign" />
                             <asp:ConfirmButtonExtender ID="Button3_ConfirmButtonExtender" runat="server" 
                                 ConfirmText="คุณต้องการ Assign Case ไปให้ Tsrคนอื่นหรือไม่" 
                                 TargetControlID="Button3">
                             </asp:ConfirmButtonExtender>
                         </ItemTemplate>
                     </asp:TemplateField>
                     
                     <asp:BoundField DataField="UpdateDate" HeaderText="วันที่โทร" 
                         SortExpression="UpdateDate" />
                     
                     <asp:BoundField DataField="CusName" HeaderText="ชื่อ-สกุล" ReadOnly="True" 
                         SortExpression="CusName" />
                     <asp:BoundField DataField="CarBrand" HeaderText="ยี่ห้อ-รุ่น" ReadOnly="True" 
                         SortExpression="CarBrand" />
                     <asp:BoundField DataField="AppointDate" HeaderText="วันนัด" 
                         SortExpression="AppointDate" />
                     <asp:BoundField DataField="Comments" HeaderText="หมายเหตุ" 
                         SortExpression="Comments" />
                     <asp:BoundField DataField="CarBuyDate" DataFormatString="{0:dd/MM/yyyy}" 
                         HeaderText="วันหมดประกัน" SortExpression="CarBuyDate" />
                     <asp:TemplateField HeaderText="TsrName" SortExpression="TsrName">
                         <ItemTemplate>
                             <asp:DropDownList ID="ddTsr" runat="server" CssClass="jamp" 
                                 DataSourceID="SqlUser" DataTextField="TsrName" DataValueField="userID" 
                                 SelectedValue='<%# Bind("userID") %>'>
                             </asp:DropDownList>
                         </ItemTemplate>
                        
                     </asp:TemplateField>
                 </Columns>
                 <HeaderStyle BackColor="#99CCFF" />
                 <SelectedRowStyle BackColor="#BFDFFF" />
                 </asp:GridView>
        </ContentTemplate>
         <HeaderTemplate >
        Follow Case(<asp:Label ID="lblFollow" runat="server" ForeColor="#FF3300" Text="0"></asp:Label>)
        </HeaderTemplate>
        </asp:TabPanel>
    </asp:TabContainer>


    <asp:SqlDataSource ID="SqlRefuse" runat="server"   ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand=" Select 
                            a2.FnameTH  + ' ' + a2.LNameTH  + ' ทะเบียนรถ : ' + a1.CarID as CusName
                            ,a2.Tel + ' ' + a2.TelExt as Tel
                            ,a2.OTel + ' ' + a2.OtelExt as Otel
                            ,a2.Mobile
                            ,a1.AssCreateDate
                            , (case 
         when datediff(year,a1.carbuydate,getdate()) &lt;= 0 then a1.carbuydate 
         when datediff(year,a1.carbuydate,getdate()) = 1 then dateadd(year,1,a1.carbuydate) 
         when datediff(year,a1.carbuydate,getdate()) = 2 then dateadd(year,2,a1.carbuydate) 
         when datediff(year,a1.carbuydate,getdate()) = 3 then dateadd(year,3,a1.carbuydate) 
         when datediff(year,a1.carbuydate,getdate()) = 4 then dateadd(year,4,a1.carbuydate) 
         when datediff(year,a1.carbuydate,getdate()) = 5 then dateadd(year,5,a1.carbuydate) 
         when datediff(year,a1.carbuydate,getdate()) = 6 then dateadd(year,6,a1.carbuydate) 
         when datediff(year,a1.carbuydate,getdate()) = 7 then dateadd(year,7,a1.carbuydate)
		 when datediff(year,a1.carbuydate,getdate()) &gt; 7 then dateadd(year,8,a1.carbuydate)
		  end )  as carbuydate
                            ,a1.CarID
                            ,a4.Groupname
                            ,a1.CarBrand + ' ' +a1.CarSeries as CarBrand
                            ,a1.IdCar
                            ,a1.AppointDate
                            ,a1.Comments
                            ,a3.FName + ' ' + a3.LName as TsrName
                            ,a3.userID
                            ,a1.UpdateDate
                            From TblCar a1
                            Inner Join TblCustomer a2 on a1.CusID = a2.CusID
                            Inner Join TblUser a3 on a1.AssignTo = a3.userID
                             Inner Join TblSourceGroup a4 on a1.GroupID = a4.GroupID
                         Where a1.CurStatus in (5) and a3.TypeTsr=3 
                            --and a3.SupID=@userID
                            and Convert(VarChar,(case 
         when datediff(year,a1.carbuydate,getdate()) &lt;= 0 then a1.carbuydate 
         when datediff(year,a1.carbuydate,getdate()) = 1 then dateadd(year,1,a1.carbuydate) 
         when datediff(year,a1.carbuydate,getdate()) = 2 then dateadd(year,2,a1.carbuydate) 
         when datediff(year,a1.carbuydate,getdate()) = 3 then dateadd(year,3,a1.carbuydate) 
         when datediff(year,a1.carbuydate,getdate()) = 4 then dateadd(year,4,a1.carbuydate) 
         when datediff(year,a1.carbuydate,getdate()) = 5 then dateadd(year,5,a1.carbuydate) 
         when datediff(year,a1.carbuydate,getdate()) = 6 then dateadd(year,6,a1.carbuydate) 
         when datediff(year,a1.carbuydate,getdate()) = 7 then dateadd(year,7,a1.carbuydate)
		 when datediff(year,a1.carbuydate,getdate()) &gt; 7 then dateadd(year,8,a1.carbuydate)
		  end ),111) between @date1 and @date2
                            and Year(a1.UpdateDate) = Year(GetDate())
                           order by a1.UpdateDate DESC, a1.CarBuyDate,a2.FNameTH,a2.LNameTH
                            " 
        UpdateCommand="UPDATE TblCar
                         SET AssignTo = @userID 
                         WHERE (IdCar = @IdCar)">
            <SelectParameters>
                <asp:CookieParameter CookieName="userID" Name="userID" />
                <asp:Parameter Name="date1" />
                <asp:Parameter Name="date2" />
            </SelectParameters>
            
            <UpdateParameters>
                <asp:Parameter Name="userID" />
                <asp:Parameter Name="IdCar" />
            </UpdateParameters>
            
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlFollow" runat="server"   ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand=" Select 
                            a2.FnameTH  + ' ' + a2.LNameTH  + ' ทะเบียนรถ : ' + a1.CarID as CusName
                            ,a2.Tel + ' ' + a2.TelExt as Tel
                            ,a2.OTel + ' ' + a2.OtelExt as Otel
                            ,a2.Mobile
                            ,a1.AssCreateDate
                            ,DateAdd(Year,1,a1.CarBuyDate) as CarBuyDate
                            ,a1.CarID
                            ,a4.Groupname
                            ,a1.CarBrand + ' ' +a1.CarSeries as CarBrand
                            ,a1.IdCar
                            ,a1.AppointDate
                            ,a1.Comments
                            ,a3.FName + ' ' + a3.LName as TsrName
                            ,a3.userID
                            ,a1.UpdateDate
                            From TblCar a1
                            Inner Join TblCustomer a2 on a1.CusID = a2.CusID
                            Inner Join TblUser a3 on a1.AssignTo = a3.userID
                             Inner Join TblSourceGroup a4 on a1.GroupID = a4.GroupID
                            Where a1.CurStatus in (6) and a3.TypeTsr=@TypeTsr and a3.SupID=@userID
                            and DATEADD(day,15 ,getdate()) &gt; DATEADD(year,1 ,CarBuyDate )
                            and Year(a1.AppointDate) = Year(GetDate())
                           order by  a1.CarBuyDate,a2.FNameTH,a2.LNameTH
                            " 
        UpdateCommand="UPDATE TblCar
                         SET AssignTo = @userID 
                         WHERE (IdCar = @IdCar)">
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

