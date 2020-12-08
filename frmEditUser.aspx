<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmEditUser.aspx.vb" Inherits="Modules_Manager_Manage_Tsr_frmEditUser" %>

<%@ Register assembly="Infragistics35.WebUI.WebDataInput.v8.3, Version=8.3.20083.1009, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            text-align: right;
        }
    </style>
    <script language="javascript" type="text/javascript">

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin-top: 15px">
        <asp:FormView ID="frmUser" runat="server" DataSourceID="SqlUser" Width="500px" 
            style="margin :auto;" DataKeyNames="UserID,UserName,userstatus"> 
            <ItemTemplate>
                <table style="width:100%;" cellspacing="0">
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            ชื่อ-นามสกุล</td>
                        <td class="style3">
                            <asp:TextBox ID="txtFNameTH" runat="server" Width="120px" 
                                Text='<%# Bind("FName") %>'></asp:TextBox>
                            -<asp:TextBox ID="txtLNameTH" runat="server" Width="120px" 
                                Text='<%# Bind("LName") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            ชื่อเล่น</td>
                        <td class="style3">
                            <asp:TextBox ID="txtNName" runat="server" Width="120px" 
                                Text='<%# Bind("NName") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            ระดับ User</td>
                        <td class="style3">
                            <asp:DropDownList ID="ddUserLevel" runat="server" CssClass="jamp" 
                                DataSourceID="SqlLevelID" DataTextField="LevelNameEng" DataValueField="LevelID" 
                                SelectedValue='<%# Bind("UserLevelID") %>'>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            Lead</td>
                        <td class="style3">
                            <asp:DropDownList ID="ddLead" runat="server" AppendDataBoundItems="True" 
                                CssClass="jamp" DataSourceID="SqlLead" DataTextField="LeadName" 
                                DataValueField="UserID" SelectedValue='<%# Bind("LeaderID1") %>'>
                                <asp:ListItem Value="0">ไม่ระบุ</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            Sup</td>
                        <td class="style3">
                            <asp:DropDownList ID="ddSup" runat="server" AppendDataBoundItems="True" 
                                CssClass="jamp" DataSourceID="SqlSup" DataTextField="SupName" 
                                DataValueField="UserID" SelectedValue='<%# Bind("SupID1") %>'>
                                <asp:ListItem Value="0">ไม่ระบุ</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            ประเภท User</td>
                        <td class="style3">
                            <asp:DropDownList ID="ddGroup" runat="server" AppendDataBoundItems="True" 
                                CssClass="jamp" DataSourceID="SqlTypeTsr" DataTextField="TypeName" 
                                DataValueField="TypeNo" SelectedValue='<%# Bind("TypeTsr") %>'>
                                <asp:ListItem Value="0">ไม่ระบุ</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            Exten</td>
                        <td class="style3">
                            <asp:DropDownList ID="ddExten" runat="server" AppendDataBoundItems="True" 
                                CssClass="jamp" DataSourceID="SqlExten" DataTextField="ExtenID" 
                                DataValueField="ExtenID" SelectedValue='<%# Bind("Exten") %>'>
                                <asp:ListItem Value="0" Selected="True">ไม่ระบุ</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            Target</td>
                        <td class="style3">
                            <igtxt:WebNumericEdit ID="txtTarget" runat="server" Value =<%# Bind("TargetSale") %>>
                            </igtxt:WebNumericEdit>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            Group</td>
                        <td class="style3">
                            <asp:DropDownList ID="ddtsrGRADE" runat="server" AppendDataBoundItems="True" 
                                CssClass="jamp" SelectedValue='<%# Bind("tsrGRADE") %>'>
                                <asp:ListItem Value="OJT">OJT</asp:ListItem>
                                <asp:ListItem Value="A"></asp:ListItem>
                                <asp:ListItem Value="B"></asp:ListItem>
                                <asp:ListItem Value="C"></asp:ListItem>
                                <asp:ListItem Value="D"></asp:ListItem>
                                <asp:ListItem Value="E"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            Username</td>
                        <td class="style3">
                            <asp:TextBox ID="txtUserName" runat="server" Width="120px" 
                                Text='<%# Bind("UserName") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            Password</td>
                        <td class="style3">
                            <asp:TextBox ID="txtPassword" runat="server" Width="120px"></asp:TextBox>
                            <asp:CheckBox ID="chkPassword" runat="server" ForeColor="Red" 
                                Text="ต้องการแก้ไข" />
                        </td>
                    </tr>
                    <%--update StartWork--%>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            วันที่เริ่มงาน</td>
                        <td class="style3">
                                 <asp:TextBox ID="txtStartWork" runat="server" Width="80px" Text='<%# Bind("StartWork")%>'></asp:TextBox>
                            <asp:MaskedEditExtender ID="txtStartWork_add_MaskedEditExtender" runat="server" 
                              CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                              CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                              CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                              Mask="99/99/9999" MaskType="Date" TargetControlID="txtStartWork">
                            </asp:MaskedEditExtender>
                            <asp:CalendarExtender ID="txtStartWork_CalendarExtender" runat="server" 
                              Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtStartWork">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <%--end update StartWork--%>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            สถานะ</td>
                        <td class="style3">
                            <asp:CheckBox ID="chkStatus" runat="server" 
                                ondatabinding="chkStatus_DataBinding" />
                            <asp:Label ID="lbltxt" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
            <EmptyDataTemplate >
            <table style="width:100%;" cellspacing="0">
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            ชื่อ-นามสกุล</td>
                        <td class="style3">
                            <asp:TextBox ID="txtFNameTH" runat="server" Width="120px"></asp:TextBox>
                            -<asp:TextBox ID="txtLNameTH" runat="server" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            ชื่อเล่น</td>
                        <td class="style3">
                            <asp:TextBox ID="txtNName" runat="server" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            ระดับ User</td>
                        <td class="style3">
                            <asp:DropDownList ID="ddUserLevel" runat="server" CssClass="jamp" 
                                DataSourceID="SqlLevelID1" DataTextField="LevelNameEng" 
                                DataValueField="LevelID">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            Lead</td>
                        <td class="style3">
                            <asp:DropDownList ID="ddLead" runat="server" AppendDataBoundItems="True" 
                                CssClass="jamp" DataSourceID="SqlLead" DataTextField="LeadName" 
                                DataValueField="UserID" SelectedValue='<%# Bind("LeaderID") %>'>
                                <asp:ListItem Value="0">ไม่ระบุ</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            Sup</td>
                        <td class="style3">
                            <asp:DropDownList ID="ddSup" runat="server" AppendDataBoundItems="True" 
                                CssClass="jamp" DataSourceID="SqlSup" DataTextField="SupName" 
                                DataValueField="UserID" SelectedValue='<%# Bind("SupID") %>'>
                                <asp:ListItem Value="0">ไม่ระบุ</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            ประเภท User</td>
                        <td class="style3">
                            <asp:DropDownList ID="ddGroup" runat="server" AppendDataBoundItems="True" 
                                CssClass="jamp" DataSourceID="SqlTypeTsr" DataTextField="TypeName" 
                                DataValueField="TypeNo">
                                <asp:ListItem Value="0" Selected="True">ไม่ระบุ</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            Exten</td>
                        <td class="style3">
                            <asp:DropDownList ID="ddExten" runat="server" AppendDataBoundItems="True" 
                                CssClass="jamp" DataSourceID="SqlExten" DataTextField="ExtenID" 
                                DataValueField="ExtenID">
                                <asp:ListItem Value="0">ไม่ระบุ</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            Target</td>
                        <td class="style3">
                            <igtxt:WebNumericEdit ID="txtTarget" runat="server" 
                               >
                            </igtxt:WebNumericEdit>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            Group</td>
                        <td class="style3">
                            <asp:DropDownList ID="ddtsrGRADE" runat="server" AppendDataBoundItems="True" 
                                CssClass="jamp" >
                                <asp:ListItem Value="OJT">OJT</asp:ListItem>
                                <asp:ListItem Value="A"></asp:ListItem>
                                <asp:ListItem Value="B"></asp:ListItem>
                                <asp:ListItem Value="C"></asp:ListItem>
                                <asp:ListItem Value="D"></asp:ListItem>
                                <asp:ListItem Value="E"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            Username</td>
                        <td class="style3">
                            <asp:TextBox ID="txtUserName" runat="server" Width="120px" 
                                Text='<%# Eval("UserName") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            Password</td>
                        <td class="style3">
                            <asp:TextBox ID="txtPassword" runat="server" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <%--add StartWork--%>
                    <tr>
                        <td bgcolor="#99CCFF" class="style1" style="color: #000000; font-weight: bold;">
                            วันที่เริ่มงาน</td>
                        <td class="style3">
                            <asp:TextBox ID="txtStartWork_add" runat="server" Width="80px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="txtStartWork_add_MaskedEditExtender" runat="server" 
                              CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                              CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                              CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                              Mask="99/99/9999" MaskType="Date" TargetControlID="txtStartWork_add">
                            </asp:MaskedEditExtender>
                            <asp:CalendarExtender ID="txtStartWork_add_CalendarExtender" runat="server" 
                              Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtStartWork_add">
                            </asp:CalendarExtender>
                            <asp:Label ID="lblstartwork" runat="server" Text="Please choose a calendar" ForeColor="Red" Visible="false"></asp:Label>                                                 
                        </td>
                    </tr>
                    <%--end add StartWork--%>
                </table>
            </EmptyDataTemplate>
        </asp:FormView>
    </div>

    <div style="text-align: center">
        <asp:Button ID="Button1" runat="server" Text="บันทึก" />
        <asp:Button ID="Button2" runat="server" Text="ยกเลิก" />
    </div>

    <asp:SqlDataSource ID="SqlUser" runat="server" 
    ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
    SelectCommand="SELECT a1.*,a2.LevelNameEng 
                     ,a1.FName + ' ' + a1.LName + '(' + a1.NName + ')' as TsrName
                     ,case when a3.userid is null then '0' else a3.userid end as SupID1
                     ,case when a4.userid is null then '0' else a4.userid end as LeaderID1
                     FROM [TblUser] a1
                     Inner Join TblUserLevel a2 on a1.UserLevelID = a2.LevelID
                     Left Join TblUser a3 on a1.SupID = a3.userid and a3.userstatus = 1 and a3.userlevelid = 3
                     Left Join TblUser a4 on a1.LeaderID = a4.userID and a4.userstatus = 1 and a4.userlevelid = 2
                     where a1.userID = @TsrID" 
                     
                     
      UpdateCommand="UPDATE TblUser
                     SET UserName = @UserName
                    , UserPassword = case @chkPass when 'True' then @UserPassword else UserPassword end 
                    , FName = @FName
                    , LName = @LName
                    , NName = @NName
                    , UserLevelID = @UserLevelID
                    , LeaderID = @LeaderID
                    , SupID = @SupID
                    , UserStatus = @UserStatus
                    , TypeTsr = @UserType
                    , UpdateID = @userID1
                    , UpdateDate = GETDATE()
                    , Exten = @Exten
                    , PassAsterisk = '@sn9' + @Exten 
                    ,TargetSale=@TargetSale
                    ,tsrGRADE=@tsrGRADE
                    ,ipasterisk = (select ipaddr from tblextension where extenid=@Exten )
                    ,StartWork = @StartWork
                    WHERE (UserID = @UserID)" 
        InsertCommand="INSERT INTO TblUser(UserName, UserPassword, FName, LName, NName, UserLevelID, LeaderID, SupID, TypeTsr, CreateID, Exten, PassAsterisk,tsrGRADE,TargetSale,ipasterisk,StartWork) 
                        VALUES (@UserName, @UserPassword, @FName, @LName, @NName, @UserLevelID, @LeaderID, @SupID, @UserType, @userID1, @Exten, '@sn9' + @Exten,@tsrGRADE,@TargetSale,(select ipaddr from tblextension where extenid=@Exten ),@StartWork)">
    <SelectParameters>

        <asp:QueryStringParameter Name="TsrID" QueryStringField="TsrID" 
            DefaultValue="1" />

    </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="UserName" />
            <asp:Parameter Name="chkPass" />
            <asp:Parameter Name="UserPassword" />
            <asp:Parameter Name="FName" />
            <asp:Parameter Name="LName" />
            <asp:Parameter Name="NName" />
            <asp:Parameter Name="UserLevelID" />
            <asp:Parameter Name="LeaderID" />
            <asp:Parameter Name="SupID" />
            <asp:Parameter Name="UserStatus" />
            <asp:Parameter Name="UserType" />
            <asp:CookieParameter CookieName = "userID" Name="userID1" />
            <asp:Parameter Name="Exten" />
            <asp:Parameter Name="TargetSale" />
            <asp:Parameter Name="tsrGRADE" />
            <asp:Parameter Name="UserID" />
            <asp:Parameter Name="StartWork" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="UserName" />
            <asp:Parameter Name="UserPassword" />
            <asp:Parameter Name="FName" />
            <asp:Parameter Name="LName" />
            <asp:Parameter Name="NName" />
            <asp:Parameter Name="UserLevelID" />
            <asp:Parameter Name="LeaderID" />
            <asp:Parameter Name="SupID" />
            <asp:Parameter Name="UserType" />
            <asp:CookieParameter CookieName = "userID" Name="userID1" />
            <asp:Parameter Name="Exten" />
            <asp:Parameter Name="tsrGRADE" />
            <asp:Parameter Name="TargetSale" />
            <asp:Parameter Name="StartWork" />
        </InsertParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlLead" runat="server" 
    ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
    SelectCommand="if @UserLevel = 1
                        begin
                      
                      SELECT a1.*,a2.LevelNameEng 
                     ,a1.FName + ' ' + a1.LName + '(' + a1.NName + ')' as LeadName
                     FROM [TblUser] a1
                     Inner Join TblUserLevel a2 on a1.UserLevelID = a2.LevelID
                     where a2.LevelID = 2
                     and a1.userstatus = 1
                     order by a1.fname,a1.lname
                      end
                      
                      else if @UserLevel = 2
                      begin
                       SELECT a1.*,a2.LevelNameEng 
                     ,a1.FName + ' ' + a1.LName + '(' + a1.NName + ')' as LeadName
                     FROM [TblUser] a1
                     Inner Join TblUserLevel a2 on a1.UserLevelID = a2.LevelID
                     where a1.userID = @userID
                     and a1.userstatus = 1
                     order by a1.fname,a1.lname
                      end
                      
                        else
                        
                       
                        begin
                      
                      SELECT a3.UserID
                     ,a3.FName + ' ' + a3.LName + '(' + a3.NName + ')' as LeadName
                     FROM [TblUser] a1
                     Inner Join TblUserLevel a2 on a1.UserLevelID = a2.LevelID
                     Inner Join TblUser a3 on a1.LeaderID = a3.userID
                     where a1.userID = @userID                  
                     order by a1.fname,a1.lname
                     
                      end  ">
    <SelectParameters>
        
        <asp:CookieParameter CookieName="userID" Name="userID" />
        <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
    </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlSup" runat="server" 
    ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
    SelectCommand="if @UserLevel = 1
                        begin
                      
                      SELECT a1.*,a2.LevelNameEng 
                     ,a1.FName + ' ' + a1.LName + '(' + a1.NName + ')' as SupName
                     FROM [TblUser] a1
                     Inner Join TblUserLevel a2 on a1.UserLevelID = a2.LevelID
                     where a2.LevelID = 3
                     and a1.userstatus = 1 
                     order by a1.fname,a1.lname
                      end
                      
                      else if @UserLevel = 2
                      begin
                       SELECT a1.*,a2.LevelNameEng 
                     ,a1.FName + ' ' + a1.LName + '(' + a1.NName + ')' as SupName
                     FROM [TblUser] a1
                     Inner Join TblUserLevel a2 on a1.UserLevelID = a2.LevelID
                     where a1.LeaderID = @userID and LevelID = 3
                      and a1.userstatus = 1 
                     order by a1.fname,a1.lname
                      end
                      
                        else
                        
                       
                        begin
                      
                      SELECT a1.*,a2.LevelNameEng 
                     ,a1.FName + ' ' + a1.LName + '(' + a1.NName + ')' as SupName
                     FROM [TblUser] a1
                     Inner Join TblUserLevel a2 on a1.UserLevelID = a2.LevelID
                     where a1.userID = @userID
                      and a1.userstatus = 1 
                     order by a1.fname,a1.lname
                     
                      end  ">
    <SelectParameters>
        
        <asp:CookieParameter CookieName="userID" Name="userID" />
        <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
    </SelectParameters>
</asp:SqlDataSource>


<asp:SqlDataSource ID="SqlLevelID" runat="server" 
    ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
    SelectCommand="if @UserLevel = 2 or  @UserLevel = 1 or @UserLevel=8
                        begin
                            SELECT a1.LevelNameEng ,a1.LevelID from TblUserLevel a1 
                       end
                        else
                        begin
                        SELECT a1.LevelNameEng ,a1.LevelID from TblUserLevel a1 where  a1.LevelID in(5,12)
                    end">
    <SelectParameters>
        <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
    </SelectParameters>
    </asp:SqlDataSource>
<asp:SqlDataSource ID="SqlLevelID1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
    SelectCommand="if @UserLevel = 2 or  @UserLevel = 1 or @UserLevel=8
                        begin
                            SELECT a1.LevelNameEng ,a1.LevelID from TblUserLevel a1 where  a1.LevelID not in (1)
                       end
                        else
                        begin
                        SELECT a1.LevelNameEng ,a1.LevelID from TblUserLevel a1 where  a1.LevelID in(5,12)
                    end">
    <SelectParameters>
        <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
    </SelectParameters>
    </asp:SqlDataSource>    
    
    
    
    <asp:SqlDataSource ID="SqlExten" runat="server" 
    ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
    SelectCommand="if @UserLevel = 1
                    begin
                    select a1.* from tblextension a1
                    Where (a1.ExtenID not in (select Exten from tbluser where exten is not null))
                    or a1.ExtenID in(@Exten)
                    end
                    else if @UserLevel = 8
                    begin
                       select a1.* from tblextension a1
                    Where (a1.ExtenID not in (select Exten from tbluser where exten is not null) and a1.TypeTsr = @TypeTsr and a1.UserLevelID in(8))
                    or a1.ExtenID in(@Exten)
                    end
                    else
                    begin
                     select a1.* from tblextension a1
                    Where (a1.ExtenID not in (select Exten from tbluser where exten is not null) and a1.TypeTsr = case @TypeTsr when 13 then 1 else @TypeTsr end and a1.UserLevelID in(0))
                    or a1.ExtenID in(@Exten)

                      
                    end">
        <SelectParameters>
            <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
            <asp:CookieParameter CookieName="TypeTsr" Name="TypeTsr" />
            <asp:QueryStringParameter Name="Exten" QueryStringField="Exten"  DefaultValue ="0"/>
        </SelectParameters>
    
    </asp:SqlDataSource>


    <asp:SqlDataSource ID="SqlTypeTsr" runat="server" 
    ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
    SelectCommand="
                   if @UserLevel = 1
                   begin
                   select * from TblTypeTsr  
                   end
                   else if @UserLevel = 2
                    begin
                   select * from TblTypeTsr 
                   where TypeNo in(1,3,6,11,12,13,15)
                   end
                   else
                   begin
                   select * from TblTypeTsr 
                   where TypeNo in(@TypeTsr,206,101)
                   end
                   ">
        <SelectParameters>
            <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
            <asp:CookieParameter CookieName="TypeTsr" Name="TypeTsr" />
        </SelectParameters>        
    </asp:SqlDataSource>
</asp:Content>

