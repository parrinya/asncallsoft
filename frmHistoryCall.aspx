<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmHistoryCall.aspx.vb" Inherits="Modules_Manager_Manage_Tsr_frmHistoryCall" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style1
        {
            text-align: right;
            font-weight: bold;
        }
        .style2
        {
            text-align: center;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="margin-top: 30px; color: #0066FF; font-weight: bold; font-size: 20px;">

   History Call</div>
   <div>
   
       <table style="width: 100%;">
           <tr>
               <td bgcolor="#99CCFF" class="style1">
                   &nbsp; Team</td>
               <td>
                   <asp:DropDownList ID="ddSup" runat="server" AutoPostBack="True" CssClass="jamp" 
                       DataSourceID="SqlSup" DataTextField="SupName" DataValueField="userID">
                   </asp:DropDownList>
               </td>
           </tr>
           <tr>
               <td bgcolor="#99CCFF" class="style1">
                   &nbsp; Tsr</td>
               <td>
                   <asp:DropDownList ID="ddTsr" runat="server" 
                       CssClass="jamp" DataSourceID="SqlUser" DataTextField="TsrName" 
                       DataValueField="userID">
                       <asp:ListItem Value="0">All</asp:ListItem>
                   </asp:DropDownList>
               </td>
           </tr>
           <tr>
               <td bgcolor="#99CCFF" class="style1">
                   สถานะ</td>
               <td>
                   <asp:DropDownList ID="ddStatus" runat="server" 
                       CssClass="jamp" DataSourceID="SqlStatus" DataTextField="StatusCode" 
                       DataValueField="StatusID" AppendDataBoundItems="True">
                       <asp:ListItem Value="0">All</asp:ListItem>
                   </asp:DropDownList>
               </td>
           </tr>
           <tr>
               <td bgcolor="#99CCFF" class="style1">
                   &nbsp; วันที่</td>
               <td>
                   <asp:TextBox ID="txtdate1" runat="server" Width="80px"></asp:TextBox>
                   <asp:MaskedEditExtender ID="txtdate1_MaskedEditExtender" 
                       runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                       CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                       CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                       Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate1">
                </asp:MaskedEditExtender>
                   <asp:calendarextender ID="txtdate1_CalendarExtender" 
                       runat="server" Enabled="True" Format="dd/MM/yyyy" 
                       TargetControlID="txtdate1">
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
               </td>
           </tr>
           <tr>
               <td bgcolor="#99CCFF" class="style1">
                   &nbsp;</td>
               <td>
                   <asp:Button ID="Button1" runat="server" Text="แสดง" />
                   <asp:Button ID="btnBilling" runat="server" Text="Export Billing" />
               </td>
           </tr>
       </table>
   
   </div>
   <div>
   <asp:FormView ID="FormView1" runat="server" DataSourceID="SqlTotalTime" 
            Width="100%">
             <ItemTemplate>
                 <table style="width:100%;" cellspacing="0" class="tb1" align="center">
                     <tr bgcolor="#99FF99" style="color: #000000; font-weight: bold">
                         <td class="style2">
                             เวลาทั้งหมด</td>
                         <td class="style2">
                             เวลาเฉลี่ย(นาที/Record)</td>
                         <td class="style2">
                             เวลา Max</td>
                         <td class="style2">
                             เวลา Min</td>
                     </tr>
                     <tr>
                         <td class="style2">
                             <asp:Label ID="Label7" runat="server" Text='<%# Eval("CallTime") %>'></asp:Label>
                         </td>
                         <td class="style2">
                             <asp:Label ID="lblTimeAvg" runat="server" Text='<%# Eval("TimeAVG") %>'></asp:Label>
                         </td>
                         <td class="style2">
                             <asp:Label ID="Label8" runat="server" Text='<%# Eval("TimeMax") %>'></asp:Label>
                         </td>
                         <td class="style2">
                             <asp:Label ID="Label9" runat="server" Text='<%# Eval("TimeMin") %>'></asp:Label>
                         </td>
                     </tr>
                 </table>
             </ItemTemplate>
         </asp:FormView>
   </div>

   <div>
   <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="100%" 
            Height="500px">
            <asp:GridView ID="GvCaseCall" runat="server" AutoGenerateColumns="False" 
                Width="100%" DataKeyNames="cctRecoder" DataSourceID="SqlHistory" 
                AllowPaging="True" PageSize="100">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ชื่อ-สกุล(ลูกค้า)" SortExpression="FnameTH">
                       
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" 
                                Text='<%# Bind("FnameTH") %>'></asp:Label>
                            &nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Bind("LNameTH") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("FnameTH") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ชื่อ-สกุล(TSR)" SortExpression="FName">
                        <ItemTemplate>
                            <asp:Label ID="lblFName" runat="server" Text='<%# Bind("FName") %>'></asp:Label>
                            &nbsp;<asp:Label ID="lblLnameTH" runat="server" Text='<%# Bind("LName") %>'></asp:Label>
                            &nbsp;(<asp:Label ID="lblNName" runat="server" Text='<%# Bind("NName") %>'></asp:Label>
                            )
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="cctTimeIn" HeaderText="วันที่โทร" 
                        SortExpression="CreateDate" />
                    <asp:BoundField DataField="cctCallerID" HeaderText="เบอร์โทร" 
                        SortExpression="cctCallerID" />
                    <asp:BoundField DataField="CallTime" HeaderText="CallTime" 
                        SortExpression="CallTime" />
                    <asp:HyperLinkField DataNavigateUrlFields="LinkFileDownload" 
                        DataTextField="cctRecoder" HeaderText="Record" Target="_blank" />
                </Columns>
                <HeaderStyle CssClass="td-header" Font-Size="10pt" ForeColor="Black" 
                    Height="30px" BackColor="#99CCFF" />
            </asp:GridView>
        </asp:Panel>
   </div>
   <asp:SqlDataSource ID="SqlHistory" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="
        If @userID = 0
        Begin
                    select distinct
                    a1.FName
                    ,a1.LName
                    ,a1.NName
                    ,a2.cctTimeIn
                    ,a2.cctCallerID
                    ,a2.cctRecoder
                    ,'http://10.17.1.222/Tm4/Record/' + Convert(VarChar,a2.cctExten) + '/' + a2.cctRecoder as LinkFileDownload
                    ,Convert(VarChar,dateadd(SS,a2.calltime,0),108) as CallTime
                    ,a3.FnameTH
                    ,a3.LNameTH
                    from TblUser a1
                    Inner Join TblCallControl a2 on a1.userID = a2.userID
                    Left Join TblCar a4 on Convert(VarChar,a4.idCar) = a2.Refer1
                    Left Join TblCustomer a3 on a4.CusID = a3.CusID 
                    where a2.cctbillsec &gt; 0 and a1.SupID=@SupID
                    and Convert(VarChar,a2.cctTimeIn,111) between @date1 and @date2
                    and Case @Status when 0 then 0 else a4.CurStatus end = @Status
                    order by a2.cctTimeIn
         End
         Else
         Begin
         select  distinct
                    a1.FName
                    ,a1.LName
                    ,a1.NName
                    ,a2.cctTimeIn
                    ,a2.cctCallerID
                    ,a2.cctRecoder
                    ,'http://10.17.1.222/Tm4/Record/' + Convert(VarChar,a2.cctExten) + '/' + a2.cctRecoder as LinkFileDownload
                    ,Convert(VarChar,dateadd(SS,a2.calltime,0),108) as CallTime
                    ,a3.FnameTH
                    ,a3.LNameTH
                    from TblUser a1
                    Inner Join TblCallControl a2 on a1.userID = a2.userID
                    Left Join TblCar a4 on Convert(VarChar,a4.idCar) = a2.Refer1
                    Inner Join TblCustomer a3 on a4.CusID = a3.CusID 
                    where a2.cctbillsec &gt; 0 and a1.SupID=@SupID and a1.userID=@userID
                    and Convert(VarChar,a2.cctTimeIn,111) between @date1 and @date2
                    and Case @Status when 0 then 0 else a4.CurStatus end = @Status
                    order by a2.cctTimeIn
         End          
">
        <SelectParameters>
            <asp:Parameter Name="userID" />
            <asp:Parameter Name="SupID" />
            <asp:Parameter Name="date1" />
            <asp:Parameter Name="date2" />
            <asp:ControlParameter ControlID="ddStatus" Name="Status" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlSup" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="if @UserLevel=3
                begin
                Select userID
                ,FName + ' ' + LName as SupName
                from TblUser
                Where UserID = @userID
                end
                Else
                Begin
                Select userID
                ,FName + ' ' + LName as SupName
                from TblUser
                Where UserLevelID = 3 and UserStatus = 1
                End">
        <SelectParameters>
            <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
            <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlUser" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="
                (Select 0 as userID
                ,'All' as TsrName)
                union
                 (Select userID
                ,FName + ' ' + LName as TsrName
                
                from TblUser
                Where SupID = @SupID and UserStatus = 1 and UserLevelID = 5)

               ">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddSup" Name="SupID" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlStatus" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="
                Select * from tblstatus

               ">
        
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlTotalTime" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="
        If @userID = 0
        Begin       Select
                   Convert(VarChar,dateadd(SS,Sum(a2.calltime),0),108) as CallTime
                    ,Convert(VarChar,dateadd(SS,Min(a2.calltime),0),108) as TimeMin
                    ,Convert(VarChar,dateadd(SS,Max(a2.calltime),0),108) as TimeMax
                    ,Convert(VarChar,dateadd(SS,Avg(a2.calltime),0),108) as TimeAVG
                    from TblUser a1
                    Inner Join TblCallControl a2 on a1.userID = a2.userID
                    where a2.cctbillsec &gt; 0 and a1.SupID=@SupID
                    and Convert(VarChar,a2.cctTimeIn,111) between @date1 and @date2
         End
         Else
         Begin
         select 
                    Convert(VarChar,dateadd(SS,Sum(a2.calltime),0),108) as CallTime
                    ,Convert(VarChar,dateadd(SS,Min(a2.calltime),0),108) as TimeMin
                    ,Convert(VarChar,dateadd(SS,Max(a2.calltime),0),108) as TimeMax
                    ,Convert(VarChar,dateadd(SS,Avg(a2.calltime),0),108) as TimeAVG
                    from TblUser a1
                    Inner Join TblCallControl a2 on a1.userID = a2.userID
                    where a2.cctbillsec &gt; 0 and a1.SupID=@SupID and a1.userID=@userID
                    and Convert(VarChar,a2.cctTimeIn,111) between @date1 and @date2
         End          
">
        <SelectParameters>
            <asp:Parameter Name="userID" />
            <asp:Parameter Name="SupID" />
            <asp:Parameter Name="date1" />
            <asp:Parameter Name="date2" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

