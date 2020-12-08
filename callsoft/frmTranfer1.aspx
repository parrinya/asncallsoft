<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmTranfer1.aspx.vb" Inherits="Modules_Manager_Manage_Case_frmTranfer1" %>

<%@ Register assembly="Infragistics35.WebUI.WebDataInput.v8.3, Version=8.3.20083.1009, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            text-align: center;
            font-weight: 700;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin-top: 25px"><font style="color: #0066FF; font-size: 18px">โอน Case</font></div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate >
    <div>
   <table style="width: 70%;margin :auto;" cellspacing="0">
        <tr>
            <td bgcolor="#99CCFF" class="style1">
                จาก Tsr</td>
            <td bgcolor="#BAD3E9" class="style1">
                ถึง Tsr</td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;
                <asp:DropDownList ID="ddTsr1" runat="server" AutoPostBack="True" 
                    CssClass="jamp" DataSourceID="SqlUser" DataTextField="TsrName" 
                    DataValueField="UserID">
                </asp:DropDownList>
            </td>
            <td class="style1">
                <asp:DropDownList ID="ddTsr2" runat="server" CssClass="jamp" 
                    DataSourceID="SqlUser" DataTextField="TsrName" DataValueField="UserID">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style1" bgcolor="#A0CFFE">
                &nbsp;
                Data Source</td>
            <td class="style1" bgcolor="#BCD5EB">
                &nbsp;
                จำนวน Assign</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:DropDownList ID="ddSourceGroup" runat="server" AutoPostBack="True" 
                    CssClass="jamp" DataSourceID="SqlSourceGroup" DataTextField="GroupName" 
                    DataValueField="GroupID">
                    <asp:ListItem Value="0">All</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style1" bgcolor="#CACAAE">
                            <igtxt:WebNumericEdit ID="txtAssign" runat="server" DataMode="Int" 
                    Width="50px">
                            </igtxt:WebNumericEdit>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtAssign" ErrorMessage="*" ForeColor="Red" 
                                ValidationGroup="btnAssign"></asp:RequiredFieldValidator>
                        </td>
        </tr>
        <tr>
            <td class="style1" bgcolor="#A0CFFE">
                Status Case</td>
            <td class="style1">
                <asp:Button ID="Button1" runat="server" Text="Assign" />
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:DropDownList ID="ddStatus" runat="server" AppendDataBoundItems="True" 
                    AutoPostBack="True" CssClass="jamp" DataSourceID="SqlStatus" 
                    DataTextField="StatusName" DataValueField="StatusID">
                    <asp:ListItem Value="0">All</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style1">
                <asp:Button ID="btnAssignNew" runat="server" BackColor="#336699" 
                    Font-Bold="True" Font-Size="10pt" ForeColor="White" Text="Assign กองกลาง" />
            </td>
        </tr>
        <tr>
            <td class="style1" bgcolor="#A6D1FD">
                Record</td>
            <td class="style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                            <asp:DropDownList ID="ddRec" runat="server" 
                                AutoPostBack="True" CssClass="jamp" DataSourceID="SqlRec" 
                                DataTextField="RecCar" DataValueField="RecCar">
                                <asp:ListItem Value="0">ระบุ Source</asp:ListItem>
                            </asp:DropDownList>
            </td>
            <td class="style1">
                &nbsp;</td>
        </tr>
    </table>
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
   
    <asp:SqlDataSource ID="SqlUser" runat="server" 
            ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            
            SelectCommand="
                            if @UserLevel = 3
                             SELECT *
                            ,a1.FName + ' ' + a1.LName + '(' + a1.NName + ')' as TsrName
                            ,case a1.UserStatus when '0' then 'ยกเลิก' else 'ใช้งาน' end as UserStatus1
                            ,case a1.UserStatus when '0' then 'color:Red' else 'color:Green' end as UserStatusColor
                            FROM [TblUser] a1
                            Where a1.SupID = @userID and a1.UserStatus = 1 and a1.TypeTsr=@TypeTsr
                              order by a1.FName,a1.LName
                            else if @UserLevel = 2
                            begin
                             SELECT *
                            ,a1.FName + ' ' + a1.LName + '(' + a1.NName + ')' as TsrName
                            ,case a1.UserStatus when '0' then 'ยกเลิก' else 'ใช้งาน' end as UserStatus1
                            ,case a1.UserStatus when '0' then 'color:Red' else 'color:Green' end as UserStatusColor
                            FROM [TblUser] a1
                            Where a1.LeaderID = @userID and a1.UserStatus = 1 and a1.TypeTsr=@TypeTsr and a1.UserLevelID in(3,5)
                              order by a1.FName,a1.LName
                            end
                             else if @UserLevel = 1
                            begin
                             SELECT *
                            ,a1.FName + ' ' + a1.LName + '(' + a1.NName + ')' as TsrName
                            ,case a1.UserStatus when '0' then 'ยกเลิก' else 'ใช้งาน' end as UserStatus1
                            ,case a1.UserStatus when '0' then 'color:Red' else 'color:Green' end as UserStatusColor
                            FROM [TblUser] a1
                            Where  a1.UserStatus = 1 and a1.TypeTsr not in(3) and a1.UserLevelID in(3,5)
                              order by a1.FName,a1.LName
                            end
                            else
                             SELECT *
                            ,a1.FName + ' ' + a1.LName + '(' + a1.NName + ')' as TsrName
                            ,case a1.UserStatus when '0' then 'ยกเลิก' else 'ใช้งาน' end as UserStatus1
                            ,case a1.UserStatus when '0' then 'color:Red' else 'color:Green' end as UserStatusColor
                            FROM [TblUser] a1
                            Where a1.UserLevelID in(5) and a1.UserStatus = 1 and a1.TypeTsr=@TypeTsr
                             order by a1.FName,a1.LName
                           
            "
            
           
            UpdateCommand="UPDATE TblCar 
                     set AssignTo = case @Type when 1 then 0 else @AssignTo end
                     , AssCreateID = @userID
                     , AssCreateDate = GETDATE()
                     , AssUpdateID = @userID
                     , AssUpdateDate = GETDATE() 
                     , CurStatus = case @Type when 1 then 0 else CurStatus end
                     WHERE (IdCar = @IdCar)" InsertCommand="INSERT INTO Tblrestatus(CusID, CarID, Status_old, Status_new, Createid, HostAccess, reSTATUSID, comment, userOld, userNew) 
                                                    VALUES (@CusID, @CarID, @Status_old, @Status_new, @userID, @HostAccess, 3, @comment, @userOld, @userNew)" 
                     >
            <InsertParameters>
                <asp:Parameter Name="CusID" />
                <asp:Parameter Name="CarID" />
                <asp:Parameter Name="Status_old" />
                <asp:Parameter Name="Status_new" />
                <asp:Parameter Name="userID" />
                <asp:Parameter Name="HostAccess" />
                <asp:Parameter Name="comment" />
                <asp:Parameter Name="userOld" />
                <asp:Parameter Name="userNew" />
            </InsertParameters>
            <SelectParameters>
                <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
                <asp:CookieParameter  Name="userID" CookieName ="userID" />
                <asp:CookieParameter CookieName="TypeTsr" Name="TypeTsr" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="Type" />
                <asp:Parameter Name="AssignTo" />
                <asp:Parameter Name="userID" />
                <asp:Parameter Name="IdCar" />
            </UpdateParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlSourceGroup" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand=" Select 0 as GroupID,'All' as Groupname
                        union
                        SELECT distinct a1.GroupID, a1.GroupName FROM [TblSourceGroup] a1 
                        Inner Join TblCar a2 on a1.GroupID = a2.GroupID
                        Where a2.AssignTo = @userID and a2.CurStatus in (1,2,4,6,7,8)">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddTsr1" Name="userID" 
                    PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlStatus" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT [StatusID], [StatusName] FROM [TblStatus] Where StatusID in (1,2,4,6,7,8)"></asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlRec" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="
                        if @StatusCase = 0
                        begin
                        SELECT Count(a2.IdCar) as RecCar FROM [TblSourceGroup] a1 
                        Inner Join TblCar a2 on a1.GroupID = a2.GroupID 
                        Where case @GroupID when 0 then 0 else a2.GroupID end  =@GroupID 
                        and a2.CurStatus in (1,2,4,6,7,8) and a2.AssignTo = @userID
                        end
                        else
                        begin
                         SELECT Count(a2.IdCar) as RecCar FROM [TblSourceGroup] a1 
                        Inner Join TblCar a2 on a1.GroupID = a2.GroupID 
                        Where case @GroupID when 0 then 0 else a2.GroupID end  =@GroupID 
                        and a2.CurStatus in (@StatusCase) and a2.AssignTo = @userID
                        end
                         ">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddStatus" Name="StatusCase" 
                    PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="ddSourceGroup" Name="GroupID" 
                    PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="ddTsr1" Name="userID" 
                    PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>
</asp:Content>

