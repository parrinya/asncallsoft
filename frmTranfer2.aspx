<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmTranfer2.aspx.vb" Inherits="Modules_Manager_Manage_Case_frmTranfer2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="margin-top: 25px"><font style="color: #0066FF; font-size: 18px">โอน Case พิเศษ</font></div>
<div><table style="width:100%;" cellspacing="0" class="tb1">
            <tr>
                <td class="style1" bgcolor="#99CCFF" 
                    style="color: #000000; font-weight: bolder">
                    ค้นหา ชื่อ/สกุล</td>
                <td>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="inputtxt" Width="250px"></asp:TextBox>
                    <asp:Button ID="Button2" runat="server" Height="22px" Text="ค้นหา" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1" bgcolor="#99CCFF" 
                    style="color: #000000; font-weight: bolder">
                    โอนให้ TSR</td>
                <td>
                    <asp:DropDownList ID="ddTsr1" runat="server" AutoPostBack="True" 
                        CssClass="jamp" DataSourceID="SqlUser" DataTextField="TsrName" 
                        DataValueField="UserID">
                    </asp:DropDownList>
                    <asp:Button ID="Button3" runat="server" Text="Assign" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            </table></div>
            <div>
            <asp:GridView ID="GvCus" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
                CellPadding="3" DataKeyNames="IdCar" DataSourceID="SqlCustomer" 
                ForeColor="Black" GridLines="Vertical" style="margin-left: 0px" 
                Width="100%">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="ChkUser" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ชื่อ-สกุล" SortExpression="FNameTH">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("FNameTH") %>'></asp:Label>
                            &nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Eval("LNameTH") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("FNameTH") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CarID" HeaderText="ทะเบียน" SortExpression="CarID" />
                    <asp:BoundField DataField="StatusCode" HeaderText="สถานะ" 
                        SortExpression="StatusCode" />
                    <asp:BoundField DataField="TsrName" HeaderText="Tsr" SortExpression="TsrName" />
                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BorderStyle="None" CssClass="td-header" Font-Size="10pt" 
                    Height="30px" BackColor="#99CCFF" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            </div>

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
                            Where a1.LeaderID = @userID and a1.UserStatus = 1 and a1.TypeTsr=@TypeTsr
                              order by a1.FName,a1.LName
                            end
                            else
                             SELECT *
                            ,a1.FName + ' ' + a1.LName + '(' + a1.NName + ')' as TsrName
                            ,case a1.UserStatus when '0' then 'ยกเลิก' else 'ใช้งาน' end as UserStatus1
                            ,case a1.UserStatus when '0' then 'color:Red' else 'color:Green' end as UserStatusColor
                            FROM [TblUser] a1
                            Where a1.UserLevelID in(5) and a1.UserStatus = 1 and a1.TypeTsr not in(3)
                             order by a1.FName,a1.LName
                           
            "
            
            UpdateCommand="UPDATE TblCar 
                     set AssignTo = @AssignTo
                     , AssCreateID = @userID
                     , AssCreateDate = GETDATE()
                     , AssUpdateID = @userID
                     , AssUpdateDate = GETDATE() 
                     WHERE (IdCar = @IdCar)" InsertCommand="INSERT INTO TblRestatus(CusID, CarID, Status_old, Status_new, Createid, HostAccess, reSTATUSID, comment, userOld, userNew) VALUES (@CusID, @CarID, @Status_old, @Status_new, @Createid, @HostAccess, 3, @comment, @userOld, @userNew)"
                     >
                <InsertParameters>
                    <asp:Parameter Name="CusID" />
                    <asp:Parameter Name="CarID" />
                    <asp:Parameter Name="Status_old" />
                    <asp:Parameter Name="Status_new" />
                    <asp:Parameter Name="Createid" />
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
                <asp:Parameter Name="AssignTo" />
                <asp:Parameter Name="userID" />
                <asp:Parameter Name="IdCar" />
            </UpdateParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlCustomer" runat="server" 
            ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand=" 
                            if @UserLevel = 3
                            begin
                            Select a2.FNameTH
                            ,a2.LNameTH
                            ,a1.CarID
                            ,a4.StatusCode
                            ,a3.FName + ' ' + a3.LName as TsrName
                            ,a1.IdCar
                            from TblCar a1
                            Inner Join TblCustomer a2 on a1.CusID = a2.CusID
                            Inner Join TblUser a3 on a1.AssignTo = a3.userID
                            Inner Join TblStatus a4 on a1.CurStatus = a4.StatusID
                            Where a2.FNameTH + ' ' + a2.LNameTH + ' ' + a1.CarID like @SearchTH
                            and (a3.SupID = @userID)
                            order by a2.FNameTH,a2.LNameTH
                            end
                            else
                            begin
                            Select a2.FNameTH
                            ,a2.LNameTH
                            ,a1.CarID
                            ,a4.StatusCode
                            ,a3.FName + ' ' + a3.LName as TsrName
                            ,a1.IdCar
                            from TblCar a1
                            Inner Join TblCustomer a2 on a1.CusID = a2.CusID
                            Inner Join TblUser a3 on a1.AssignTo = a3.userID
                            Inner Join TblStatus a4 on a1.CurStatus = a4.StatusID
                            Where a2.FNameTH + ' ' + a2.LNameTH + ' ' + a1.CarID like @SearchTH
                           
                            order by a2.FNameTH,a2.LNameTH
                            end
                            
                            " UpdateCommand="update tblapplication
                                            set createid = @userID
                                            where idcar = @IdCar and appstatus = 1 ">
            <SelectParameters>
                <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
                <asp:Parameter Name="SearchTH" type="String" />
                 <asp:CookieParameter  Name="userID" CookieName ="userID" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="userID" />
                <asp:Parameter Name="IdCar" />
            </UpdateParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlStatus" runat="server"   ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand=" select  * from TblStatus
                            Where StatusID in(4,7,8,6)
                            ">
         
            
        </asp:SqlDataSource>
</asp:Content>

