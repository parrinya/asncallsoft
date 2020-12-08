<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmUser.aspx.vb" Inherits="Modules_Manager_Manage_Tsr_frmUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin-top: 30px; color: #0066FF; font-weight: bold; font-size: 20px;">

    Manage User</div>

    <div>
    
        <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
            Width="100%">
        <asp:TabPanel HeaderText ="TSR" ID="TabPanel1" runat="server" >
        <ContentTemplate ><div style="text-align: center">เลือก Team : <asp:DropDownList ID="ddUser" runat="server" AutoPostBack="True" 
        CssClass="jamp" DataSourceID="SqlSup" DataTextField="SupName" 
        DataValueField="UserID"></asp:DropDownList><asp:Button ID="Button2" runat="server" Text="เพิ่ม" /></div><div><asp:GridView ID="GvUser" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="UserID,Exten" DataSourceID="SqlUser" Width="100%"><Columns><asp:TemplateField><ItemTemplate><asp:Button ID="Button1" runat="server" 
                    CommandArgument="<%# Container.DataItemIndex %>" Text="เลือก" 
                    CommandName="Select" /></ItemTemplate><ItemStyle Width="40px" /></asp:TemplateField><asp:BoundField DataField="SupName" HeaderText="SupName" 
            SortExpression="SupName" /><asp:BoundField DataField="UserName" HeaderText="UserName" 
            SortExpression="UserName" /><asp:BoundField DataField="TsrName" HeaderText="ชื่อ-สกุล" ReadOnly="True" 
            SortExpression="TsrName" /><asp:BoundField DataField="Exten" HeaderText="Exten" SortExpression="Exten" /><asp:BoundField DataField="LevelNameEng" HeaderText="Level" 
            SortExpression="LevelNameEng" />
                    <asp:CheckBoxField DataField="UserStatus1" HeaderText="สถานะ" 
            SortExpression="UserStatus" /></Columns><HeaderStyle BackColor="#99CCFF" ForeColor="Black" Height="30px" /></asp:GridView><asp:SqlDataSource ID="SqlUser" runat="server" 
    ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
    SelectCommand="
                      
                         SELECT a1.userid,a1.exten,CAST((case a1.userstatus when 1 then 'True' else 'False' end) as bit) as userstatus1,a2.LevelNameEng ,a1.username
                         ,a2.levelid
                         ,a1.FName + ' ' + a1.LName + '(' + a1.NName + ')' as TsrName
                         ,a3.FName + ' ' + a3.LName + '(' + a3.NName + ')' as SupName
                         ,a1.fname
                         ,a1.lname
                         ,a3.fname
                         ,a3.lname
                         FROM [TblUser] a1
                         Inner Join TblUserLevel a2 on a1.UserLevelID = a2.LevelID
                         Left Join TblUser a3 on a1.SupID = a3.userID
                         where  case @userID when 0 then 0 else  a1.SupID end = @userID
                         and a1.UserLevelID in(5,12)
                         Order by a1.UserStatus DESC,a2.LevelID,a3.FName + ' ' + a3.LName + '(' + a3.NName + ')',a1.FName,a1.LName
                       
                        
                    
                    "><SelectParameters><asp:ControlParameter ControlID="ddUser" Name="userID" 
            PropertyName="SelectedValue" /></SelectParameters></asp:SqlDataSource></div></ContentTemplate>
        </asp:TabPanel>
            <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Sup">
            <ContentTemplate ><div style="text-align: center">เลือก Team : <asp:DropDownList ID="ddLead" runat="server" AutoPostBack="True" 
        CssClass="jamp" DataSourceID="SqlLead" DataTextField="SupName" 
        DataValueField="UserID"></asp:DropDownList><asp:Button ID="Button3" runat="server" Text="เพิ่ม" /></div><div><asp:GridView ID="GvSup" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="UserID,Exten" DataSourceID="SqlUserSup" Width="100%"><Columns><asp:TemplateField><ItemTemplate><asp:Button ID="Button1" runat="server" 
                    CommandArgument="<%# Container.DataItemIndex %>" Text="เลือก" 
                    CommandName="Select" /></ItemTemplate><ItemStyle Width="40px" /></asp:TemplateField><asp:BoundField DataField="LeadName" HeaderText="LeadName" 
            SortExpression="LeadName" /><asp:BoundField DataField="UserName" HeaderText="UserName" 
            SortExpression="UserName" /><asp:BoundField DataField="TsrName" HeaderText="ชื่อ-สกุล" ReadOnly="True" 
            SortExpression="TsrName" /><asp:BoundField DataField="Exten" HeaderText="Exten" SortExpression="Exten" /><asp:BoundField DataField="LevelNameEng" HeaderText="Level" 
            SortExpression="LevelNameEng" />
                        <asp:CheckBoxField DataField="UserStatus1" HeaderText="สถานะ" 
            SortExpression="UserStatus" /></Columns><HeaderStyle BackColor="#99CCFF" ForeColor="Black" Height="30px" /></asp:GridView>
            <asp:SqlDataSource ID="SqlUserSup" runat="server" 
             ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
             SelectCommand="
                      
                         SELECT a1.*,a2.LevelNameEng ,CAST((case a1.userstatus when 1 then 'True' else 'False' end) as bit) as userstatus1
                         ,a1.FName + ' ' + a1.LName + '(' + a1.NName + ')' as TsrName
                         ,a3.FName + ' ' + a3.LName + '(' + a3.NName + ')' as LeadName
                         FROM [TblUser] a1
                         Inner Join TblUserLevel a2 on a1.UserLevelID = a2.LevelID
                         Left Join TblUser a3 on a1.LeaderID = a3.userID
                         where  case @userID when 0 then 0 else  a1.LeaderID end = @userID and a1.UserLevelID in(3)
                         Order by a1.UserStatus DESC,a2.LevelID,a3.FName + ' ' + a3.LName + '(' + a3.NName + ')' ,a1.FName,a1.LName
                       
                        
                    
                    "><SelectParameters><asp:ControlParameter ControlID="ddLead" Name="userID" 
            PropertyName="SelectedValue" /></SelectParameters></asp:SqlDataSource></div></ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Lead">
            <ContentTemplate ><div style="text-align: center"><asp:Button ID="Button4" runat="server" Text="เพิ่ม" /></div><div><asp:GridView ID="GvLead" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="UserID,Exten" DataSourceID="SqlUserLead" Width="100%"><Columns><asp:TemplateField><ItemTemplate><asp:Button ID="Button1" runat="server" 
                    CommandArgument="<%# Container.DataItemIndex %>" Text="เลือก" 
                    CommandName="Select" /></ItemTemplate><ItemStyle Width="40px" /></asp:TemplateField><asp:BoundField DataField="UserName" HeaderText="UserName" 
            SortExpression="UserName" /><asp:BoundField DataField="TsrName" HeaderText="ชื่อ-สกุล" ReadOnly="True" 
            SortExpression="TsrName" /><asp:BoundField DataField="Exten" HeaderText="Exten" SortExpression="Exten" /><asp:BoundField DataField="LevelNameEng" HeaderText="Level" 
            SortExpression="LevelNameEng" />
                    <asp:CheckBoxField DataField="UserStatus1" HeaderText="สถานะ" 
            SortExpression="UserStatus" /></Columns><HeaderStyle BackColor="#99CCFF" ForeColor="Black" Height="30px" /></asp:GridView><asp:SqlDataSource ID="SqlUserLead" runat="server" 
    ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
    SelectCommand="
                      if @UserLevel = 1
                      begin
                      SELECT a1.*,a2.LevelNameEng ,CAST((case a1.userstatus when 1 then 'True' else 'False' end) as bit) as userstatus1
                         ,a1.FName + ' ' + a1.LName + '(' + a1.NName + ')' as TsrName
                         FROM [TblUser] a1
                         Inner Join TblUserLevel a2 on a1.UserLevelID = a2.LevelID
                         where   UserLevelID in(1,2)
                         Order by a1.UserStatus DESC,a2.LevelID,a1.FName,a1.LName
                      end
                    "><SelectParameters><asp:CookieParameter CookieName="UserLevel" Name="UserLevel"/></SelectParameters></asp:SqlDataSource></div></ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Report">
                <ContentTemplate><div><asp:Button ID="Button6" runat="server" Text="Export" />&nbsp;แสดงรายชื่อ user ที่ยังคงใช้งานอยู่ และมี exten</div><div><asp:GridView ID="GvUserReport" runat="server" AutoGenerateColumns="False" 
                            DataSourceID="SqlUserReport" Width="100%"><Columns><asp:BoundField DataField="fname" HeaderText="fname" SortExpression="fname" /><asp:BoundField DataField="lname" HeaderText="lname" SortExpression="lname" /><asp:BoundField DataField="LevelNameEng" HeaderText="LevelNameEng" 
                                    SortExpression="LevelNameEng" /><asp:BoundField DataField="exten" HeaderText="exten" SortExpression="exten" /><asp:BoundField DataField="supname" HeaderText="supname" ReadOnly="True" 
                                    SortExpression="supname" /><asp:BoundField DataField="leadname" HeaderText="leadname" ReadOnly="True" 
                                    SortExpression="leadname" /></Columns><HeaderStyle BackColor="#99CCFF" /></asp:GridView></div></ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>

    </div>


    



<asp:SqlDataSource ID="SqlSup" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="
        if @UserLevel = 3
        begin
        select UserID ,FName + ' ' + LName as SupName
        from tbluser
        where userID = @userID
        order by fname,lname
        end
        else if @UserLevel = 2
         Select UserID ,FName + ' ' + LName  as SupName
        from tbluser
        Where UserLevelID = 3 and UserStatus = 1 and LeaderID = @userID
        order by fname,lname
        else if @UserLevel = 1
        (select 0 as UserID
        ,'All' as SupName)
        union(
         Select UserID ,FName + ' ' + LName  as SupName
        from tbluser
        Where UserLevelID = 3 and UserStatus = 1 
       ) order  by supname
        else
        begin
        Select UserID ,FName + ' ' + LName  as SupName
        from tbluser
        Where UserLevelID = 3 and UserStatus = 1
        order by fname,lname
        End
        ">
        <SelectParameters>
            <asp:CookieParameter CookieName="UserLevel" Name="UserLevel"/>
            <asp:CookieParameter CookieName="userID"  Name="userID" />
        </SelectParameters>
    </asp:SqlDataSource>
<asp:SqlDataSource ID="SqlLead" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="
        if @UserLevel = 2
        begin
        select UserID ,FName + ' ' + LName as SupName
        from tbluser
        where userID = @userID  and UserStatus = 1
        order by fname,lname
        end
     
        else if @UserLevel = 1
        begin
         (select 0 as UserID
        ,'All' as SupName)
        union(
         Select UserID ,FName + ' ' + LName  as SupName
        from tbluser
        Where UserLevelID in(1, 2) and UserStatus = 1 
       ) order  by supname
        End
        ">
        <SelectParameters>
            <asp:CookieParameter CookieName="UserLevel" Name="UserLevel"/>
            <asp:CookieParameter CookieName="userID"  Name="userID" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlUserReport" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="
        select a1.fname
,a1.lname
,a4.LevelNameEng 
,a1.exten
,a2.fname + ' ' + a2.lname as supname
,a3.fname + ' ' + a3.lname as leadname
 from tbluser a1
left join tbluser a2 on a1.supid = a2.userid 
left join tbluser a3 on a1.LeaderID = a3.userid
inner join TblUserLevel a4 on a1.userlevelid = a4.levelid
where a1.userstatus = 1 and a1.userlevelid in(2,3,5) and a1.TypeTsr in(1,13)
and a1.Exten not in(0)
order by a1.UserLevelID
,a3.fname + ' ' + a3.lname
,a2.fname + ' ' + a2.lname
        ">
        
    </asp:SqlDataSource>
</asp:Content>

