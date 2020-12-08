<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmReportSourcebyList.aspx.vb" Inherits="Modules_Manager_Report_frmReportSourcebyList" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="border-top: 2px solid #66CCFF; text-align: center;">
        LEAD :
        <asp:DropDownList ID="ddLead" runat="server" CssClass="jamp" 
            DataSourceID="SqlLead" DataTextField="SupName" DataValueField="userID" 
            AutoPostBack="True">
        </asp:DropDownList>
&nbsp;SUP :
        <asp:DropDownList ID="ddSup" runat="server" CssClass="jamp" 
            DataSourceID="SqlSup" DataTextField="SupName" DataValueField="userID">
        </asp:DropDownList>
        ค้นหาตามวันที่SubmitDate :
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
        &nbsp;<asp:Button ID="Button1" runat="server" Text="แสดง" />
    </div>
    <div>
    <iframe  src ="<%=strReport %>" frameborder="0" height="800" width="100%"></iframe>
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
               
                  union
                select 0 as userID
                ,'All' as SupName
                end
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
            <asp:CookieParameter CookieName="TypeTsr" Name="TypeTsr" />
            <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
            <asp:ControlParameter ControlID="ddLead" Name="LeaderID" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

