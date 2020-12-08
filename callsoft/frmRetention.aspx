<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmRetention.aspx.vb" Inherits="Modules_Manager_Report_frmRetention" %>
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
        &nbsp;บริษัท :
        <asp:DropDownList ID="ddcomp" runat="server"
         DataSourceID="SqlComp" DataTextField="ProTypeName" DataValueField="ProTypeID"  AutoPostBack="True"
        >
        </asp:DropDownList>
        &nbsp;Sup Name :
        <asp:DropDownList ID="ddsup" runat="server"
         DataSourceID="Sqlsup" DataTextField="name" DataValueField="UserID"  AutoPostBack="True"
         >
        </asp:DropDownList>
        <asp:Button ID="btn1" runat="server" Text="แสดง" />
        </TD>
    </tr>
</table>
 <div>
<iframe  src ="<%=strReport %>" frameborder="0" height="800" width="100%"></iframe>

</div>
</div>
<asp:SqlDataSource ID="SqlComp" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="select ProTypeID,ProTypeName from Tbl_ProductType where ProTypeStatus=1">
        <SelectParameters>
            <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
            <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
        </SelectParameters>
    </asp:SqlDataSource>

<asp:SqlDataSource ID="Sqlsup" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="   SELECT 0 as UserID,'ทั้งหมด' as name union SELECT [UserID],[FName]+' '+ [LName]+case when [NName]is null then '' else ' ('+[NName]+')' end as name
      
  FROM [Car].[dbo].[TblUser]
  where [UserLevelID]=3  and [TypeTsr]=3 and [UserStatus]=1">
        <SelectParameters>
            <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
            <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

