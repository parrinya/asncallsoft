<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmHistorySendCVandPV.aspx.vb" Inherits="Modules_Manager_Manage_Tsr_frmHistorySendCVandPV" %>
<%@ Register assembly="Infragistics35.WebUI.UltraWebGrid.ExcelExport.v8.3, Version=8.3.20083.1009, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" tagprefix="igtblexp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Infragistics35.WebUI.UltraWebGrid.v8.3, Version=8.3.20083.1009, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.UltraWebGrid" tagprefix="igtbl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div style="margin-top: 30px; color: #0066FF; font-weight: bold; font-size: 20px;">
    History Send list 
 </div>
  <div style="border-top: 2px solid #66CCFF; text-align: center;">
    <table width="100%" border="1">
        <tr>
            <td>LEAD :
                <asp:DropDownList ID="ddLead" runat="server" CssClass="jamp" DataSourceID="SqlLead" DataTextField="SupName" DataValueField="userID"  AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>SUP :
                <asp:DropDownList ID="ddSup" runat="server" CssClass="jamp" AutoPostBack="True" DataSourceID="SqlSup" DataTextField="SupName" DataValueField="userID">
                </asp:DropDownList>
            </td>
            <td>TSR :        
            <asp:DropDownList ID="ddTsr" runat="server" CssClass="jamp" AutoPostBack="True" DataSourceID="SqlTsr" DataTextField="Name" DataValueField="userID">
            </asp:DropDownList>

            </td>
            <td align="left">
            <input id="name" type="radio"  runat="server" />ชื่อ-สกุล ลูกค้า</td>
            <td align="left"><asp:textbox runat="server" id="txtname" Width="150px"></asp:textbox></td>
       
    <td rowspan="4">
    <asp:button runat="server" text="ค้นหา" id="btnSearch"/>
    <asp:Button ID="btnExport" runat="server" Text="Export" />
    </td>
    </tr>

    <tr>
        <td colspan="3"></td>
        <td align="left"><input id="carid" type="radio"  runat="server" />ทะเบียน</td>
        <td align="left"><asp:textbox runat="server" id="txtcarid" Width="150px"></asp:textbox></td>
    </tr>
     <tr>
        <td colspan="3"></td>
        <td align="left"><input id="appid" type="radio"  runat="server" />AppID</td>
        <td align="left"><asp:textbox runat="server" id="txtappid" Width="150px"></asp:textbox></td>
    </tr>
    <tr>
    <td colspan="3"></td>
    <td align="left"><input id="all" type="radio"  runat="server" />วันที่ส่ง</td>
    <td align="left"><asp:TextBox ID="txtdate1" runat="server" Width="80px"></asp:TextBox>
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
                   </asp:CalendarExtender></td>
    </tr>

    </table>
 </div>
  <div>
    <igtblexp:UltraWebGridExcelExporter ID="UltraWebGridExcelExporter1"  runat="server" ExportMode="Custom">
    </igtblexp:UltraWebGridExcelExporter>
                <igtbl:UltraWebGrid ID="UWGShowPayment" runat="server" Height="450px"  Width="100%" DisplayLayout-ColFootersVisibleDefault="Yes" >
                                                        <displaylayout allowcolsizingdefault="Free" allowcolumnmovingdefault="OnServer" 
                                                            bordercollapsedefault="Separate"  
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
                                                                <borderdetails colorleft="Gray" colortop="Gray" />
                                                                <borderdetails colorleft="Gray" colortop="Gray" />
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
                                                     <asp:Label ID="lblcount" runat="server" Text=""></asp:Label>
   </div>
   
<asp:SqlDataSource ID="SqlLead" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="
                if @UserLevel=3
                begin
                 
                Select a2.userID
                ,a2.FName + ' ' + a2.LName as SupName
                from TblUser a1
                Inner Join Tbluser a2 on a1.LeaderID = a2.userID
                Where a1.UserID = @userID and a1.UserStatus = 1
                end
                Else if @UserLevel=2 
                Begin
                
                Select a1.userID
                ,a1.FName + ' ' + a1.LName as SupName
                from TblUser a1
                Where a1.userID = @userID and a1.UserStatus = 1
                End
                else 
                begin
                 select -1 as userID,'All' as SupName
 union
                Select a1.userID
                ,a1.FName + ' ' + a1.LName as SupName
                from tbluser a1 
                where a1.UserLevelID in(2) and a1.UserStatus = 1  end
               ">
        <SelectParameters>
            <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
            <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlSup" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="
               if  @UserLevel=3
                    begin
                     
                    Select a1.userID
                    ,a1.FName + ' ' + a1.LName as SupName
                    from TblUser a1
                    Where a1.userID = @userID and a1.UserStatus = 1
               
                end
                
                else 
                    begin
                     select -1 as userID,'All' as SupName
 union
                    Select a1.userID
                    ,a1.FName + ' ' + a1.LName as SupName
                    from tbluser a1 
                    where a1.UserLevelID in(3) and a1.UserStatus = 1 and a1.LeaderID = @LeaderID
                    
                 
                end
                ">
        <SelectParameters>
            <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
            <asp:CookieParameter CookieName="TypeTsr" Name="TypeTsr" />
            <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
            <asp:ControlParameter ControlID="ddLead" Name="LeaderID" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlTsr" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand=" select -1 as userID,'All'  as Name
 union Select a1.userID
                    ,a1.FName + ' ' + a1.LName as Name
                    from TblUser a1
                    Where a1.LeaderID = @LeaderID and a1.SupID=@SupID and a1.UserStatus = 1
                    and a1.UserLevelID in(5)">
        <SelectParameters>
            <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
            <asp:ControlParameter ControlID="ddLead" Name="LeaderID" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="ddSup" Name="SupID" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

