<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmSaleApprove.aspx.vb" Inherits="Modules_Manager_Report_frmSaleApprove" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
<div>
<table><tr>
                <td>LEAD :</td>
                <td><asp:DropDownList ID="ddLead" runat="server" CssClass="jamp"   DataSourceID="SqlLead" DataTextField="SupName" DataValueField="userID" AutoPostBack="True"></asp:DropDownList></td>
                <td>SUP :</td>
                <td><asp:DropDownList ID="ddSup" runat="server" CssClass="jamp"   DataSourceID="SqlSup" DataTextField="SupName" DataValueField="userID"></asp:DropDownList></td>
                <td>ค้นหาวันที่ :<asp:TextBox ID="txtdate1" runat="server" Width="80px"></asp:TextBox>
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
    </asp:CalendarExtender></td>
                <td><asp:DropDownList ID="ddTypedate" runat="server">
            <asp:ListItem Value="0">วันที่ชำระ</asp:ListItem>
            <asp:ListItem Value="1">วันคุ้มครอง</asp:ListItem>
        </asp:DropDownList></td>
                <td><asp:Button ID="Button1" runat="server" Text="แสดง" Visible="false" /></td>
                <td><asp:Button ID="btn_showList" runat="server" Text="แสดง" /></td>
                <td><asp:Button ID="Button2" runat="server" Text="Export" /></td>
              </tr>
              <tr>
                <td>บริษัทประกัน</td>
                <td colspan="5"><asp:DropDownList ID="ddCompanyIns" runat="server"   DataSourceID="SqlCompanyIns" DataTextField="ProTypeName" DataValueField="ProtypeID">
       </asp:DropDownList></td>
              </tr>
              </table>
</div>
<div>
    <p style="text-align:center;"><asp:label runat="server" ID="lbRecordNoFound" Visible="false" Text="ไม่พบข้อมูล" ForeColor="Red"></asp:label></p>
    <asp:Panel ID="Panel1" runat="server" Height="500px" ScrollBars="Auto" 
        Width="100%">
<asp:GridView ID="GvSaleApprove" runat="server" 
            AutoGenerateColumns="False" Width="150%" ShowFooter="true">
        <Columns>
            <asp:BoundField DataField="SupName" HeaderText="SupName" 
                SortExpression="SupName" />
            <asp:BoundField DataField="Tsrname" HeaderText="Tsrname" 
                SortExpression="Tsrname" />
            <asp:BoundField DataField="GroupName" HeaderText="GroupName" 
                SortExpression="GroupName" />
            <asp:BoundField DataField="SuccessDate" DataFormatString="{0:dd/MM/yyyy}" 
                HeaderText="SuccessDate" SortExpression="SuccessDate" />
            <asp:BoundField DataField="CusName" HeaderText="ชื่อ-สกุล" 
                SortExpression="CusName" />
            <asp:BoundField DataField="CarID" HeaderText="ทะเบียน" SortExpression="CarID" />
            <asp:BoundField DataField="CarBrand" HeaderText="ยี่ฮ้อ" SortExpression="CarBrand" />
            <asp:BoundField DataField="CarSeries" HeaderText="รุ่นรถ" SortExpression="CarSeries" />
            <asp:BoundField DataField="ProTypeName" HeaderText="บริษัท" 
                SortExpression="ProTypeName" />
            <asp:BoundField DataField="IsProvalue" HeaderText="ประกัน" 
                SortExpression="IsProvalue" />
            <asp:BoundField DataField="IsCarpet" HeaderText="พรบ." 
                SortExpression="IsCarpet" />
            <asp:BoundField DataField="YearPay" DataFormatString="{0:N2}" 
                HeaderText="เบี้ยเต็ม" SortExpression="YearPay" />
            <asp:BoundField DataField="ProValue" DataFormatString="{0:N2}" 
                HeaderText="เบี้ยขาย" SortExpression="ProValue" />
            <asp:BoundField DataField="ProCarpet" DataFormatString="{0:N2}" 
                HeaderText="เบี้ย พรบ." SortExpression="ProCarpet" />
            <asp:BoundField DataField="DiffProValue" DataFormatString="{0:N2}" 
                HeaderText="Diff" SortExpression="DiffProValue" />
            <asp:BoundField DataField="ProtectDate" DataFormatString="{0:dd/MM/yyyy}" 
                HeaderText="วันคุ้มครอง" SortExpression="ProtectDate" />
            <asp:BoundField DataField="Payname" HeaderText="การชำระ" SortExpression="Payname" />
            <asp:BoundField DataField="payment" HeaderText="สถานะการชำระ" 
                SortExpression="payment" />
            <asp:BoundField DataField="PayDate" DataFormatString="{0:dd/MM/yyyy}" 
                HeaderText="วันที่ชำระ" SortExpression="PayDate" />
            <asp:BoundField DataField="flagSend"  HeaderText="ปณ.อื่นๆ" SortExpression="flagSend" />
            <asp:BoundField DataField="PackageNamePA"  HeaderText="PA" SortExpression="PackageNamePA" />
        </Columns>
        <HeaderStyle BackColor="#336699" ForeColor="White" />
        <footerstyle BackColor="#e0e0e0" Font-Bold="true" />
    </asp:GridView>
    </asp:Panel>
    
</div>
    <asp:SqlDataSource ID="SqlLead" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="
                if @UserLevel=3
                begin
                Select a2.userID
                ,a2.FName + ' ' + a2.LName as SupName
                from TblUser a1
                Inner Join Tbluser a2 on a1.LeaderID = a2.userID
                Where a1.UserID = @userID
                end
                Else if @UserLevel=2 
                Begin
                Select a1.userID
                ,a1.FName + ' ' + a1.LName as SupName
                from TblUser a1
                Where a1.userID = @userID
                End
                else 
                begin
                Select a1.userID
                ,a1.FName + ' ' + a1.LName as SupName
                from tbluser a1 
                where a1.UserLevelID in(2) and a1.UserStatus = 1
               
                 
                end
                ">
        <SelectParameters>
            <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
            <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlSup" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="
                if @UserLevel=3
                begin
                Select a1.userID
                ,a1.FName + ' ' + a1.LName as SupName
                from TblUser a1
                Where a1.userID = @userID
                end
               
                else 
                begin
                Select a1.userID
                ,a1.FName + ' ' + a1.LName as SupName
                from tbluser a1 
                where a1.UserLevelID in(3) and a1.UserStatus = 1 and a1.LeaderID = @LeaderID
                
                union
                 select 0 as userID
                ,'All' as SupName
                end
                ">
        <SelectParameters>
            <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
            <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
            <asp:ControlParameter ControlID="ddLead" Name="LeaderID" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>


    <asp:SqlDataSource ID="SqlSaleApprove" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="
        select a6.FName + ' ' + a6.LName as Tsrname
,a6.UserID 
,a6.SupID 
,a8.FName + ' ' + a8.LName as SupName
,a9.FName + ' ' + a9.LName as LeadName
,a6.LeaderID 
,a3.FNameTH + ' ' + a3.LNameTH as CusName
,sg.GroupName
,a2.CarID 
,a2.CarBrand
,a2.CarSeries
,a4.ProTypeName + ' ' + a5.TypeName as ProTypeName
,case a1.IsProvalue when 1 then 'X' else '-' end as IsProvalue
,case a1.IsCarpet  when 1 then 'X' else '-' end as  IsCarpet
,case a1.IsProvalue when 1 then a1.ProValue  else 0 end as ProValue
,case a1.IsCarpet  when 1 then a1.CarPet  else 0 end as  ProCarpet
,case a1.IsProvalue when 1 then a1.YearPay   else 0 end as YearPay
,case a1.IsProvalue when 1 then a1.YearPay - a1.ProValue    else 0 end as DiffProValue
,a1.SuccessDate 
,a1.ProtectDate 
,case when a7.AppID is null then 'รอชำระ' else  'ชำระเรียบร้อย' end as payment
,a7.PayDate 
,a11.Payname
,case when a1.flagSend =0 then 'X' else '' end as flagSend
,isnull(a13.PackageName +'('+ convert(varchar,a13.NetPremium)+')','-') as PackageNamePA

 from TblApplication  a1
Inner Join TblCar a2 on a1.Idcar = a2.idcar
Inner Join TblCustomer a3 on a2.CusID = a3.CusID 
Inner join Tbl_ProductType a4 on a1.ProDuctID = a4.ProTypeID 
Inner Join Tbl_Type a5 on a1.Typeprovalue = a5.Typeid 
Inner Join TblUser a6 on a2.AssignTo = a6.userid
Inner Join Tblpayment a7 on a1.AppID = a7.AppID and a7.PayNo = 1
inner Join TblUser a8 on a6.SupID = a8.UserID 
inner join TblUser a9 on a6.LeaderID = a9.UserID 
inner join TblSourceGroup sg on sg.GroupID=a2.GroupID
left join TblAppPay a10 on a10.AppID = a7.AppID and a10.PayID = 1
left join Tbl_Pay a11 on a10.Typepay = a11.Payid
Left Join TblApplicationPA a12 on a1.AppID=a12.AppID
Left Join TblPackagePA a13 on a13.PackageID=a12.PackageID 

where CONVERT (Varchar,a7.PayDate,111) between @date1 and @date2
and a1.AppStatus = 1
and a2.CurStatus in(3,4)
and a1.Statusqc not in(0,2,3)
and  case @SupID when 0 then 0 else a6.SupID end = @SupID
and a6.LeaderID = @LeadID
and case @ComIns when 0 then 0 else a1.ProDuctID end =@ComIns
order by 
a9.FName + ' ' + a9.LName 
,a8.FName + ' ' + a8.LName
,a6.FName + ' ' + a6.LName
,a1.SuccessDate 

        ">
        <SelectParameters>
            <asp:Parameter Name="date1" />
            <asp:Parameter Name="date2" />
            <asp:Parameter Name="ComIns" />
            <asp:ControlParameter ControlID="ddSup" Name="SupID" 
                PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="ddLead" Name="LeadID" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlSaleApprove1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="
        select a6.FName + ' ' + a6.LName as Tsrname
,a6.UserID 
,a6.SupID 
,a8.FName + ' ' + a8.LName as SupName
,a9.FName + ' ' + a9.LName as LeadName
,a6.LeaderID 
,a3.FNameTH + ' ' + a3.LNameTH as CusName
,sg.GroupName
,a2.CarID 
,a2.CarBrand
,a2.CarSeries
,a4.ProTypeName + ' ' + a5.TypeName as ProTypeName
,case a1.IsProvalue when 1 then 'X' else '-' end as IsProvalue
,case a1.IsCarpet  when 1 then 'X' else '-' end as  IsCarpet
,case a1.IsProvalue when 1 then a1.ProValue  else 0 end as ProValue
,case a1.IsCarpet  when 1 then a1.CarPet  else 0 end as  ProCarpet
,case a1.IsProvalue when 1 then a1.YearPay   else 0 end as YearPay
,case a1.IsProvalue when 1 then a1.YearPay - a1.ProValue    else 0 end as DiffProValue
,a1.SuccessDate 
,a1.ProtectDate 
,case when a7.AppID is null then 'รอชำระ' else  'ชำระเรียบร้อย' end as payment
,a7.PayDate 
,a11.Payname
,case when a1.flagSend =0 then 'X' else '' end as flagSend
,isnull(a13.PackageName +'('+ convert(varchar,a13.NetPremium)+')','-') as PackageNamePA
 from TblApplication  a1
Inner Join TblCar a2 on a1.Idcar = a2.idcar
Inner Join TblCustomer a3 on a2.CusID = a3.CusID 
Inner join Tbl_ProductType a4 on a1.ProDuctID = a4.ProTypeID 
Inner Join Tbl_Type a5 on a1.Typeprovalue = a5.Typeid 
Inner Join TblUser a6 on a2.AssignTo = a6.userid
Inner Join Tblpayment a7 on a1.AppID = a7.AppID and a7.PayNo = 1
inner Join TblUser a8 on a6.SupID = a8.UserID 
inner join TblUser a9 on a6.LeaderID = a9.UserID 
inner join TblSourceGroup sg on sg.GroupID=a2.GroupID
left join TblAppPay a10 on a10.AppID = a7.AppID and a10.PayID = 1
left join Tbl_Pay a11 on a10.Typepay = a11.Payid
Left Join TblApplicationPA a12 on a1.AppID=a12.AppID
Left Join TblPackagePA a13 on a13.PackageID=a12.PackageID 

where CONVERT (Varchar,a7.PayDate,111) between @date1 and @date2
and a1.AppStatus = 1
and a2.CurStatus in(3,4,20)
and a1.Statusqc not in(0,2,3)
and  case @SupID when 0 then 0 else a6.SupID end = @SupID
and a6.LeaderID = @LeadID
and case @ComIns when 0 then 0 else a1.ProDuctID end =@ComIns
order by 
a9.FName + ' ' + a9.LName 
,a8.FName + ' ' + a8.LName
,a6.FName + ' ' + a6.LName
,a1.SuccessDate 

        ">
        <SelectParameters>
            <asp:Parameter Name="date1" />
            <asp:Parameter Name="date2" />
             <asp:Parameter Name="ComIns" />
            <asp:ControlParameter ControlID="ddSup" Name="SupID" 
                PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="ddLead" Name="LeadID" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlSaleApprove2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="
        select a6.FName + ' ' + a6.LName as Tsrname
,a6.UserID 
,a6.SupID 
,a8.FName + ' ' + a8.LName as SupName
,a9.FName + ' ' + a9.LName as LeadName
,a6.LeaderID 
,a3.FNameTH + ' ' + a3.LNameTH as CusName
,sg.GroupName
,a2.CarID 
,a2.CarBrand
,a2.CarSeries
,a4.ProTypeName + ' ' + a5.TypeName as ProTypeName
,case a1.IsProvalue when 1 then 'X' else '-' end as IsProvalue
,case a1.IsCarpet  when 1 then 'X' else '-' end as  IsCarpet
,case a1.IsProvalue when 1 then a1.ProValue  else 0 end as ProValue
,case a1.IsCarpet  when 1 then a1.CarPet  else 0 end as  ProCarpet
,case a1.IsProvalue when 1 then a1.YearPay   else 0 end as YearPay
,case a1.IsProvalue when 1 then a1.YearPay - a1.ProValue    else 0 end as DiffProValue
,a1.SuccessDate 
,a1.ProtectDate 
,case when a7.AppID is null then 'รอชำระ' else  'ชำระเรียบร้อย' end as payment
,a7.PayDate 
,a11.Payname
,case when a1.flagSend =0 then 'X' else '' end as flagSend
,isnull(a13.PackageName +'('+ convert(varchar,a13.NetPremium)+')','-') as PackageNamePA
 from TblApplication  a1
Inner Join TblCar a2 on a1.Idcar = a2.idcar
Inner Join TblCustomer a3 on a2.CusID = a3.CusID 
Inner join Tbl_ProductType a4 on a1.ProDuctID = a4.ProTypeID 
Inner Join Tbl_Type a5 on a1.Typeprovalue = a5.Typeid 
Inner Join TblUser a6 on a2.AssignTo = a6.userid
Inner Join Tblpayment a7 on a1.AppID = a7.AppID and a7.PayNo = 1
inner Join TblUser a8 on a6.SupID = a8.UserID 
inner join TblUser a9 on a6.LeaderID = a9.UserID 
inner join TblSourceGroup sg on sg.GroupID=a2.GroupID
left join TblAppPay a10 on a10.AppID = a7.AppID and a10.PayID = 1
left join Tbl_Pay a11 on a10.Typepay = a11.Payid
Left Join TblApplicationPA a12 on a1.AppID=a12.AppID
Left Join TblPackagePA a13 on a13.PackageID=a12.PackageID 

where CONVERT (Varchar,a1.ProtectDate,111) between @date1 and @date2
and a1.AppStatus = 1
and a2.CurStatus in(3,4,20)
and a1.Statusqc not in(0,2,3)
and  case @SupID when 0 then 0 else a6.SupID end = @SupID
and case @ComIns when 0 then 0 else a1.ProDuctID end =@ComIns
and a6.LeaderID = @LeadID
order by 
a9.FName + ' ' + a9.LName 
,a8.FName + ' ' + a8.LName
,a6.FName + ' ' + a6.LName
,a1.SuccessDate 

        ">
        <SelectParameters>
            <asp:Parameter Name="date1" />
            <asp:Parameter Name="date2" />
             <asp:Parameter Name="ComIns" />
            <asp:ControlParameter ControlID="ddSup" Name="SupID" 
                PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="ddLead" Name="LeadID" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlCompanyIns" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="select ProtypeID,ProTypeName from Tbl_ProductType  where ProTypeStatus=1">
      
    </asp:SqlDataSource>
</asp:Content>

