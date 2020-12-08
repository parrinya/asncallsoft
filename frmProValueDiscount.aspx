<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmProValueDiscount.aspx.vb" Inherits="Modules_Manager_Report_frmProValueDiscount" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 288px;
            text-align: right;
        }
        .style2
        {
            width: 288px;
            text-align: right;
            height: 25px;
        }
        .style3
        {
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%;">
        <tr>
            <td class="style1">
                <div id="DivLead" runat="server">Lead</div>
               <%-- <div id="DivSup"  runat="server">Sup</div>--%>
            </td>
            <td>
                <asp:DropDownList ID="ddUser" runat="server" DataSourceID="SqlLead" 
                    DataTextField="SupName" DataValueField="userID" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style1"> <div id="Div3"  runat="server">Sup</div></td>
            <td>
             <asp:DropDownList ID="ddsup" runat="server"  DataSourceID="SqlSup" DataTextField="SupName" DataValueField="userID">
                </asp:DropDownList>
            </td>
        </tr>
        <div id ="typetsrhide" runat="server" >
        <tr>
            <td class="style2">
                เงื่อนไข</td>
            <td class="style3">
                <asp:DropDownList ID="ddcondition" runat="server" >
                    <asp:ListItem Value="0">วันที่ Qc Success</asp:ListItem>
                    <asp:ListItem Value="1">วันที่คุ้มครอง</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        </div>
        <tr>
            <td class="style1">
                <div id ="Div1" runat="server" >
                &nbsp;
                วันที่ Qc Success  
                </div>
            </td>
            <td>
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
            </td>
        </tr>
       <div id="Div2" runat="server">
       <tr>
            <td class="style1">
                Export</td>
            <td>
                <asp:DropDownList ID="ddExport" runat="server" >
                    <asp:ListItem Value="0">PDF</asp:ListItem>
                    <asp:ListItem Value="1">Excel</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        </div>       
        <tr>
            <td class="style1">
                &nbsp;
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Export" />
            </td>        
        </tr>
    </table>


        <asp:SqlDataSource ID="SqlUser" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="if @UserLevel = 1
                           
                            if @TypeTsr=3
                            begin
                                Select a1.FName + ' ' + a1.Lname as LeaderName
                                ,a1.userID
                                from TblUser a1
                                where a1.UserLevelID in(3) and a1.TypeTsr=3 and a1. UserStatus = 1
                                 union
                                select 'All' as LeaderName,0  as userID  
                            end
                            else 
                            begin
                                Select a1.FName + ' ' + a1.Lname as LeaderName
                                ,a1.userID
                                from TblUser a1
                                where a1.UserLevelID in(2) and a1. UserStatus = 1
                            end
                       else if @UserLevel = 2
                           
                             if @TypeTsr=3
                            begin
                                Select a1.FName + ' ' + a1.Lname as LeaderName
                                ,a1.userID
                                from TblUser a1
                                where a1.UserLevelID in(3) and a1.TypeTsr=3 and a1. UserStatus = 1
                                 union
                                select 'All' as LeaderName,0  as userID  
                            end
                            else 
                             begin
                                Select a1.FName + ' ' + a1.Lname as LeaderName
                                ,a1.userID
                                from TblUser a1
                                where a1.userID = @userID
                            end
                       else if @UserLevel = 3 and @TypeTsr=3
                            begin
                            Select a1.FName + ' ' + a1.Lname as LeaderName
                            ,a1.userID                            
                             from TblUser a1
                                where a1.UserLevelID in(3) and a1.TypeTsr=3 and a1. UserStatus = 1
                            union
                            select 'All' as LeaderName,0  as userID  
                            end                            
                            ">
            <SelectParameters>
                <asp:CookieParameter CookieName="TypeTsr" DefaultValue="0" Name="TypeTsr" />
                <asp:CookieParameter CookieName="UserLevel" DefaultValue="0" Name="UserLevel" />
                <asp:CookieParameter CookieName="userID" DefaultValue="0" Name="userID" />
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
            <asp:ControlParameter ControlID="ddUser" Name="LeaderID" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
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
</asp:Content>

