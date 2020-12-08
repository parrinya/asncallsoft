<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmPending.aspx.vb" Inherits="Modules_Manager_Manage_Tsr_frmPending" %>

<%@ Register assembly="Infragistics35.WebUI.WebDataInput.v8.3, Version=8.3.20083.1009, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style2
        {
            width: 322px;
        }
        .style1
        {
            width: 139px;
        }
        </style>

        <script language="javascript" type="text/javascript">



            function change_parent_url(url) {
                document.location = url;
            }		
   
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin-top: 30px; color: #0066FF; font-weight: bold; font-size: 20px;">

    Pending Case</div>
    <div>
    
        <table style="width: 100%;" __designer:mapid="334b">
            <tr __designer:mapid="334c">
                <td class="style2" style="text-align: right" __designer:mapid="334d">
                    <p style="color: #0066FF; font-weight: bold" __designer:mapid="334e">
                        เลือก ประเภทงาน :
                    </p>
                </td>
                <td class="style1" __designer:mapid="334f">
                    <asp:DropDownList runat="server" CssClass="jamp" ID="ddPending">
                        <asp:ListItem Value="1">CallCenter</asp:ListItem>
                        <asp:ListItem Value="2">ถ่ายรูปรถ+ทวงหนี้</asp:ListItem>
                        <asp:ListItem Value="3">งานตีกลับ</asp:ListItem>
                        <asp:ListItem Value="4">งานQc</asp:ListItem>
                        <asp:ListItem Value="5">งานการเงิน</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td __designer:mapid="3355" 
                    style="text-align: right; font-weight: 700; color: #3E73FF;">
                    Team</td>
                <td __designer:mapid="3355">
    <asp:DropDownList ID="ddUser" runat="server" AutoPostBack="True" 
        CssClass="jamp" DataSourceID="SqlSup" DataTextField="SupName" 
        DataValueField="UserID" Visible="False">
    </asp:DropDownList>

                </td>
                <td __designer:mapid="3355">
                    <igtxt:WebImageButton runat="server" Text="แสดง" ID="WebImageButton1">
                        <Appearance>
                            <Image Url="../../../images/Icon/View.png">
                            </Image>
                        </Appearance>
                    </igtxt:WebImageButton>
                </td>
            </tr>
        </table>
    
    </div>

    <div>
    <iframe  src = "<%= LinkPending %>" frameborder="0" height="400" scrolling="auto" 
        width="100%"></iframe>
    </div>

    <asp:SqlDataSource ID="SqlSup" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="
        if @UserLevel = 3
        begin
        select UserID ,FName + ' ' + LName as SupName
        from tbluser
        where userID = @userID
        end
        else if @UserLevel = 2
         Select UserID ,FName + ' ' + LName  as SupName
        from tbluser
        Where UserLevelID = 3 and UserStatus = 1 and LeaderID = @userID
        else
        begin
        Select UserID ,FName + ' ' + LName  as SupName
        from tbluser
        Where UserLevelID = 3 and UserStatus = 1
        End
        ">
        <SelectParameters>
            <asp:CookieParameter CookieName="UserLevel" Name="UserLevel"/>
            <asp:CookieParameter CookieName="userID"  Name="userID" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

