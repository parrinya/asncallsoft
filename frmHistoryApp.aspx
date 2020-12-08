<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmHistoryApp.aspx.vb" Inherits="Modules_Manager_Manage_Tsr_frmHistoryApp" %>
<%@ Register assembly="Infragistics35.WebUI.UltraWebGrid.ExcelExport.v8.3, Version=8.3.20083.1009, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" tagprefix="igtblexp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Infragistics35.WebUI.UltraWebGrid.v8.3, Version=8.3.20083.1009, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.UltraWebGrid" tagprefix="igtbl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin-top: 30px; color: #0066FF; font-weight: bold; font-size: 20px;">

   History App Case :<asp:Label ID="lblCase" runat="server" ForeColor="#FF6600" 
        Text="0"></asp:Label>
    </div>
   <div>
       <table style="width: 100%;" >
           <tr>
               <td style="text-align: right; font-weight: 700; background-color: #99CCFF;">
                   เลือก Team</td>
               <td>
                   <asp:DropDownList ID="ddSup" runat="server" AutoPostBack="True" CssClass="jamp" 
                       DataSourceID="SqlSup" DataTextField="SupName" DataValueField="userID">
                   </asp:DropDownList>
               </td>
               <td style="text-align: right; background-color: #99CCFF; font-weight: 700;">
                   เลือก Tsr</td>
               <td>
                   <asp:DropDownList ID="ddTsr" runat="server" 
                       CssClass="jamp" DataSourceID="SqlUser" DataTextField="TsrName" 
                       DataValueField="userID">
                       <asp:ListItem Value="0">All</asp:ListItem>
                   </asp:DropDownList>
               </td>
               

               <td style="text-align: right; font-weight: 700; background-color: #99CCFF;">
                   วันที่ Success</td>
               <td>
                   <asp:TextBox ID="txtdate1" runat="server" Width="80px"></asp:TextBox>
                   <asp:MaskedEditExtender ID="txtdate1_MaskedEditExtender" runat="server" 
                       CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                       CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                       CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                       Mask="99/99/9999" TargetControlID="txtdate1" MaskType="Date">
                   </asp:MaskedEditExtender>
                   <asp:CalendarExtender ID="txtdate1_CalendarExtender" runat="server" 
                       Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate1">
                   </asp:CalendarExtender>
                   -<asp:TextBox ID="txtdate2" runat="server" Width="80px"></asp:TextBox>
                   <asp:MaskedEditExtender ID="txtdate2_MaskedEditExtender" runat="server" 
                       CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                       CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                       CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                       Mask="99/99/9999" TargetControlID="txtdate2" MaskType="Date">
                   </asp:MaskedEditExtender>
                   <asp:CalendarExtender ID="txtdate2_CalendarExtender" runat="server" 
                       Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate2">
                   </asp:CalendarExtender>
               </td>
               <td>
                   <asp:Button ID="Button1" runat="server" Text="แสดง" />
                   <asp:Button ID="Button2" runat="server" Text="Export" />
               </td>
           </tr>
            <tr>
                 <td style="text-align: right; font-weight: 700; background-color: #99CCFF;">
                   ชำระงวดแรก</td>
                 <td>
                   <asp:DropDownList ID="ddpay" runat="server">
                                <asp:ListItem Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">ชำระแล้ว</asp:ListItem>
                                <asp:ListItem Value="2">ยังไม่ชำระ</asp:ListItem>
                    </asp:DropDownList>
                   </td>
                <td style="text-align: right; background-color: #99CCFF; font-weight: 700;">
                   เลือก บริษัทประกัน</td>
               <td>
                   <asp:DropDownList ID="ddCompanyIns" runat="server"  
                       CssClass="jamp" DataSourceID="SqlCompanyIns" DataTextField="ProTypeName" 
                       DataValueField="ProtypeID">
                   </asp:DropDownList>
               </td>
                 <td style="text-align: right; font-weight: 700; background-color: #99CCFF;">
                     <asp:CheckBox ID="ChkProtectDate" runat="server" />วันที่คุ้มครอง</td>
                 <td><asp:TextBox ID="txtProtecdateS" runat="server" Width="80px"></asp:TextBox>
                   <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                       CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                       CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                       CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                       Mask="99/99/9999" TargetControlID="txtProtecdateS" MaskType="Date">
                   </asp:MaskedEditExtender>
                   <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                       Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtProtecdateS">
                   </asp:CalendarExtender>
                   -<asp:TextBox ID="txtProtecdateE" runat="server" Width="80px"></asp:TextBox>
                   <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                       CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                       CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                       CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                       Mask="99/99/9999" TargetControlID="txtProtecdateE" MaskType="Date">
                   </asp:MaskedEditExtender>
                   <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                       Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtProtecdateE">
                   </asp:CalendarExtender></td>
            </tr>
       </table>
   
   </div>
   <div>
    <igtblexp:UltraWebGridExcelExporter ID="UltraWebGridExcelExporter1"  runat="server" ExportMode="Custom">
    </igtblexp:UltraWebGridExcelExporter>
                <igtbl:UltraWebGrid ID="UWGShowPayment" runat="server" Height="450px" 
                                                        Width="100%" DisplayLayout-ColFootersVisibleDefault="Yes" >
                                                        <displaylayout allowcolsizingdefault="Free" allowcolumnmovingdefault="OnServer" 
                                                            bordercollapsedefault="Separate" cellclickactiondefault="RowSelect" 
                                                            name="UWGShowPayment" nodatamessage="ไม่พบข้อมูล" rowheightdefault="20px" 
                                                            selecttyperowdefault="Single" stationarymargins="Header"  tablelayout="Fixed" 
                                                             version="4.00" >
                                                            <framestyle borderstyle="Solid" borderwidth="1px" height="450px" width="100%">
                                                            </framestyle>
                                                            <rowalternatestyledefault bordercolor="Gray" borderstyle="Solid" 
                                                                borderwidth="1px" cursor="Hand">
                                                            </rowalternatestyledefault>
                                                            <pager>
                                                                <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                <borderdetails colorleft="White" colortop="White" />
                                                                <borderdetails colorleft="White" colortop="White" />
                                                                </PagerStyle>                                                               
                                                            </pager>
                                                            <editcellstyledefault borderstyle="None" borderwidth="0px">
                                                            </editcellstyledefault>
                                                            <headerstyledefault bordercolor="Gray" borderstyle="Solid" borderwidth="1px" 
                                                                font-bold="True" font-names="Angsana New" font-size="15pt" forecolor="Black">
                                                                <borderdetails colorleft="White" colortop="White" />
                                                                <borderdetails colorleft="White" colortop="White" />
                                                            </headerstyledefault>
                                                          
                                                            <rowselectorstyledefault bordercolor="Gray" borderstyle="Solid" 
                                                                borderwidth="1px" cursor="Hand" font-bold="True" font-names="Angsana New">
                                                            </rowselectorstyledefault>
                                                            <rowstyledefault bordercolor="Gray" borderstyle="Solid" borderwidth="1px" 
                                                                cursor="Hand" font-names="Angsana New" font-size="15pt">
                                                                <padding left="3px" />
                                                                <padding left="3px" />
                                                            </rowstyledefault>
                                                            <selectedrowstyledefault bordercolor="Gray" borderstyle="Solid" 
                                                                borderwidth="1px" cursor="Hand">
                                                            </selectedrowstyledefault>
                                                            <addnewbox>
                                                                <boxstyle backcolor="LightGray" borderstyle="Solid" borderwidth="1px">
                                                                    <borderdetails colorleft="White" colortop="White" />
                                                                    <borderdetails colorleft="White" colortop="White" />
                                                                </boxstyle>
                                                            </addnewbox>
                                                            <activationobject bordercolor="" borderwidth="">
                                                            </activationobject>
                                                            <FooterStyleDefault backcolor="LightGray" borderstyle="Solid" borderwidth="1px">
                                                                              
                                                            </FooterStyleDefault>
                                                                                   
                                                        </displaylayout>
                                                        <bands>
                                                            <igtbl:UltraGridBand>
                                                                <addnewrow view="NotSet" visible="NotSet">
                                                                </addnewrow>
                                                            </igtbl:UltraGridBand>
                                                        </bands>
                                                                  
                   
                                                    </igtbl:UltraWebGrid>
   </div>

   <asp:SqlDataSource ID="SqlSup" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="
                if @UserLevel=3
                begin
                Select userID
                ,FName + ' ' + LName as SupName
                from TblUser
                Where UserID = @userID
              
                end
                Else if @UserLevel=2 or @UserLevel=1
                Begin
                Select userID
                ,FName + ' ' + LName as SupName
                from TblUser
                Where UserLevelID = 3 and UserStatus = 1
                End
                else 
                begin
                Select a2.userID
                ,a2.FName + ' ' + a2.LName as SupName
                from tbluser a1
                Inner Join Tbluser a2 on a1.SupID = a2.userID
                where a1.userID = @userID
                end
                ">
        <SelectParameters>
            <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
            <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCompanyIns" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="select ProtypeID,ProTypeName from Tbl_ProductType  where ProTypeStatus=1">
      
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlUser" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand=" 
                 if @UserLevel=3 or @UserLevel=2 or @UserLevel=1 
                 begin
                   Select userID
                ,FName + ' ' + LName as TsrName
               
                from TblUser
                Where SupID = @SupID and UserStatus = 1 and UserLevelID = 5

                 union 
                select 0 userID
                ,'All' as TsrName
                 end
                 else
                 begin
                   Select userID
                ,FName + ' ' + LName as TsrName
                
                from TblUser
                Where userID = @userID 
                 end
              

               ">
        <SelectParameters>
            <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
            <asp:ControlParameter ControlID="ddSup" Name="SupID" 
                PropertyName="SelectedValue" />
            <asp:CookieParameter CookieName="userID" Name="userID" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlHistoryApp" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand=" 
                Select 
                a3.FNameTH + ' ' + a3.LNameTH as CusName
                ,a1.SuccessDate
                ,a1.QcSuccessDate 
                ,a1.ProtectDate 
                ,a2.CarID 
                ,a6.FName + ' ' + a6.LName as TsrName
                ,a4.ProTypeName
                ,a5.TypeName 
                ,case a7.TypePay when 2 then 'credit' else 'Payment' end as TypePay
                ,case a1.Isprovalue when 1 then a1.ProValue else 0 end  + case a1.IsCarpet when 1 then a1.CarPet else 0 end as TotalValue
                ,a1.submitdate 
                ,a8.Astatusname 
                ,a9.PayDate 
                ,a9.PayValue
                ,a10.StatusCode
                from TblApplication a1
                Inner Join TblCar a2 on a2.idcar = a1.Idcar 
                Inner Join TblCustomer a3 on a2.CusID = a3.cusid
                Inner Join Tbl_ProductType a4 on a1.ProDuctID = a4.ProTypeID 
                Inner Join Tbl_Type a5 on a1.Typeprovalue = a5.Typeid 
                Inner Join TblUser a6 on a2.AssignTo = a6.UserID 
                Inner Join TblAppPay a7 on a1.appid = a7.AppID and PayID = 1
                Left Join Tbl_AStatus a8 on a1.Statusqc = a8.Astatusid 
                Left Join Tblpayment a9 on a1.AppID = a9.AppID and a9.PayNo = 1
                Inner Join TblStatus a10 on a2.CurStatus  = a10.StatusID
                Where CONVERT(VarChar,a1.successdate,111) between @date1 and @date2
                and case @userID when 0 then 0 else a6.UserID end = @userID and a6.SupID = @SupID and a2.CurStatus in(3,4)
                order by a1.SuccessDate
               ">
        <SelectParameters>
            <asp:Parameter Name="date1" />
            <asp:Parameter Name="date2" />
            <asp:ControlParameter ControlID="ddTsr" Name="userID" 
                PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="ddSup" Name="SupID" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    </asp:Content>

