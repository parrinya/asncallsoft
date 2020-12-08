<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmAssginReNew.aspx.vb" Inherits="Modules_Manager_Manage_Case_frmAssginReNew" %>

<%@ Register assembly="Infragistics35.WebUI.WebDataInput.v8.3, Version=8.3.20083.1009, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin-top: 25px"><font style="color: #0066FF; font-size: 18px">AssignCase</font></div>
<div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate >
    <table style="width: 100%;">
        <tr>
            <td>
                <table style="width:100%;">
                    <tr> 
                        <td style="font-weight: bold; background-color: #99CCFF">DataSource</td>
                        <td>
                            <asp:DropDownList ID="ddSourceGroup" runat="server" AppendDataBoundItems="True" 
                                AutoPostBack="True" CssClass="jamp" DataSourceID="SqlSourceGroup" 
                                DataTextField="GroupName" DataValueField="GroupID">
                                <asp:ListItem Value="0">ระบุ Source</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddRec" runat="server" AutoPostBack="True" CssClass="jamp" 
                                DataSourceID="SqlRec" DataTextField="RecCar" DataValueField="RecCar">
                                <asp:ListItem Value="0">ระบุ Source</asp:ListItem>
                            </asp:DropDownList>                           
                        </td>
                   </tr>
                   <tr>
                        <td style="font-weight: bold; background-color: #99CCFF">
                            ชั้นประกัน</td>
                        <td>
                            <asp:DropDownList ID="ddType" runat="server" AutoPostBack="True" 
                                CssClass="jamp" DataSourceID="SqlType" DataTextField="TypeName" 
                                DataValueField="Typeid">
                                <asp:ListItem Value="0">ไม่ระบุ</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold; background-color: #99CCFF">
                            บริษัทประกัน</td>
                        <td>
                            <asp:DropDownList ID="ddCompany" runat="server" AutoPostBack="True" 
                                CssClass="jamp" DataSourceID="SqlCompany" 
                                DataTextField="ProTypeName" 
                                DataValueField="ProTypeID">
                                <asp:ListItem Value="-1">ไม่ระบุ</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold; background-color: #99CCFF">
                            ซ่อม</td>
                        <td>
                            <asp:DropDownList ID="ddtypeRepair" runat="server" AutoPostBack="True" 
                                CssClass="jamp">
                                 <asp:ListItem Value="0">ไม่ระบุ</asp:ListItem>
                                <asp:ListItem Value="1">อู่</asp:ListItem>
                                <asp:ListItem Value="2">ห้าง</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold; background-color: #99CCFF">
                            การเคลม</td>
                        <td>
                            <asp:RadioButtonList ID="rbClam" runat="server" AutoPostBack="True" 
                                RepeatDirection="Horizontal" RepeatLayout="Flow">
                                 <asp:ListItem Selected="True" Value="0">ไม่ระบุ</asp:ListItem>
                                <asp:ListItem Value="1">ไม่เคย</asp:ListItem>
                                <asp:ListItem Value="2">เคย</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>                       
                         <td style="font-weight: bold; background-color: #99CCFF">
                            จำนวนAssign</td>                       
                         <td> 
                            <asp:TextBox ID="txtAssign" runat="server"  Width="50px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtAssign" ErrorMessage="*" ForeColor="Red" 
                                ValidationGroup="btnAssign"></asp:RequiredFieldValidator>
                            <asp:Button ID="Button1" runat="server" Text="Assign" 
                                ValidationGroup="btnAssign" />
                        </td>
                    </tr>
                </table>
            </td>
        <td>
                <asp:GridView ID="GvUser" runat="server" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
                    CellPadding="3" DataKeyNames="UserID" DataSourceID="SqlUser" ForeColor="Black" 
                    GridLines="Vertical" Width="100%">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:TemplateField>

                            <ItemTemplate>
                                <asp:CheckBox ID="ChkUser" runat="server" AutoPostBack="True" 
                                    oncheckedchanged="ChkUser_CheckedChanged" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                <asp:CheckBox ID="ChkAll" runat="server" AutoPostBack="True" 
                                    oncheckedchanged="ChkAll_CheckedChanged" Text="ทั้งหมด" />
                            </HeaderTemplate>
                            <ItemStyle Width="80px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="UserName" HeaderText="UserName" 
                            SortExpression="UserName">
                        <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="ชื่อ-สกุล" SortExpression="FName">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("FName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("FName") %>'></asp:Label>
                                &nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Eval("LName") %>'></asp:Label>
                                &nbsp;(&nbsp;(<asp:Label ID="Label3" runat="server" Text='<%# Eval("NName") %>'></asp:Label>
                                )
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="#99CCFF" BorderStyle="None" CssClass="td-header" 
                        Font-Size="10pt" Height="30px" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </td>
        </tr>       
        <tr><td valign=top>
        </td>
        <td><div>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                             <ProgressTemplate >
                                <div  style="color: #DF01A5; font: Helvetica 15pt;">
                                  กำลังดำเนินการ
                                </div>                               
                            </ProgressTemplate>
                           </asp:UpdateProgress>    
                           </div>  
        </td>
        </tr>
        
        
       
    </table>



</ContentTemplate>
    </asp:UpdatePanel>
    

</div>
    
<asp:SqlDataSource ID="SqlSourceGroupShow" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="  
                        If @UserLevel = 2 or @UserLevel = 3
                        begin
                         SELECT distinct a1.GroupID, a1.GroupName 
                          FROM [TblSourceGroup] a1 
                        Inner Join TblCar a2 on a1.GroupID = a2.GroupID 
                        Where a2.AssignTo = @userID and a1.dataid=3
                        order by a1.GroupName
                        end
                        else
                        begin
                          SELECT distinct a1.GroupID, a1.GroupName 
                          FROM [TblSourceGroup] a1 
                          Inner Join TblCar a2 on a1.GroupID = a2.GroupID
                          Left Join TblUser a3 on a1.createID = a3.userID
                          Where  a2.AssignTo in(0)
                          and a1.groupstatus = 1 and a1.dataid=3
                          order by a1.GroupName
                        end
                        ">
            <SelectParameters>
                <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
                <asp:CookieParameter  Name="userID" CookieName ="userID" />
            </SelectParameters>
        </asp:SqlDataSource>
<asp:SqlDataSource ID="SqlSourceGroup" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="  
                        If @UserLevel = 2 or @UserLevel = 3
                        begin
                         SELECT distinct a1.GroupID, a1.GroupName 
                          FROM [TblSourceGroup] a1 
                        Inner Join TblCar a2 on a1.GroupID = a2.GroupID 
                        Where a2.AssignTo = @userID and a2.CurStatus in (0,1)
                        order by a1.Groupname
                        end
                        else
                        begin
                          SELECT distinct a1.GroupID, a1.GroupName 
                          FROM [TblSourceGroup] a1 
                          Inner Join TblCar a2 on a1.GroupID = a2.GroupID
                          Left Join TblUser a3 on a1.createID = a3.userID
                          Where  a2.CurStatus in (0,1) and a2.AssignTo in(0)
                          and a1.groupstatus = 1
                          order by a1.GroupName
                        end
                        ">
            <SelectParameters>
                <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
                <asp:CookieParameter  Name="userID" CookieName ="userID" />
            </SelectParameters>
        </asp:SqlDataSource>
      <asp:SqlDataSource ID="SqlRec" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="
                        If @UserLevel = 2 or @UserLevel = 3
                        begin
                        SELECT Count(a2.IdCar) as RecCar FROM [TblSourceGroup] a1 
                        Inner Join TblCar a2 on a1.GroupID = a2.GroupID 
                        Where a2.GroupID=@GroupID  and a2.CurStatus in (0,1) and a2.AssignTo = @userID
                        end
                        else
                        begin
                        SELECT Count(a2.IdCar) as RecCar FROM [TblSourceGroup] a1 
                        Inner Join TblCar a2 on a1.GroupID = a2.GroupID                        
                        Where a2.GroupID=@GroupID  and a2.CurStatus in (0,1) and a2.AssignTo in(0)
                        end
                         ">
            <SelectParameters>
                <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
                <asp:ControlParameter ControlID="ddSourceGroup" Name="GroupID" 
                    PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="ddType" Name="ddType" 
                    PropertyName="SelectedValue" />
                <asp:CookieParameter CookieName="userID" Name="userID" />
            </SelectParameters>
        </asp:SqlDataSource>  
        
    <asp:SqlDataSource ID="SqlUser" runat="server" 
            ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand="if @UserAdmin = 1
                                    begin
                                     SELECT *
                                      FROM [TblUser] a1
                                        Where a1.UserStatus = 1 
                                         and a1.TypeTsr=@TypeTsr
                                         order by a1.UserLevelID,a1.Fname ,a1.LName
                                    end
                                else if @UserAdmin = 2
                                    begin
                                    SELECT *
                                        FROM [TblUser] a1
                                        Where UserLevelID= 3
                                        and LeaderID = @userID
                                         and a1.UserStatus = 1 
                                         and a1.TypeTsr=@TypeTsr
                                         order by a1.UserLevelID,a1.Fname ,a1.LName
                                    end
                                else if @UserAdmin = 3
                                    begin
                                    SELECT *
                                        FROM [TblUser] a1
                                        Where UserLevelID= 5
                                        and a1.SupID = @userID
                                         and a1.UserStatus = 1 
                                         and a1.TypeTsr=@TypeTsr
                                        order by a1.UserLevelID,a1.Fname ,a1.LName
                                    end
                               " 
        UpdateCommand="UPDATE TblCar 
                     set AssignTo = @AssignTo
                     , CurStatus = 1
                     , AssCreateID = @userID
                     , AssCreateDate = GETDATE()
                     , AssUpdateID = @userID
                     , AssUpdateDate = GETDATE() 
                     WHERE (IdCar = @IdCar)">
            <SelectParameters>               
                <asp:CookieParameter CookieName="UserLevel" Name="UserAdmin" />
                <asp:CookieParameter CookieName="TypeTsr" Name="TypeTsr" />
               <asp:CookieParameter  Name="userID" CookieName ="userID" />
               
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="AssignTo" />
                <asp:CookieParameter  Name="userID" CookieName ="userID" />
                <asp:Parameter Name="IdCar" />
            </UpdateParameters>
        </asp:SqlDataSource>


        <asp:SqlDataSource ID="SqlTeam" runat="server" 
            ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand="if @UserLevel = 3  
                           begin
                           Select FName + ' ' + LName as TeamName
                           ,userID
                           from tbluser
                           where userID = @userID and userstatus = 1
                           order by fname,lname
                           end
                           else if @UserLevel = 2
                           begin
                           Select FName + ' ' + LName as TeamName
                           ,userID
                           from tbluser
                           where UserStatus = 1 and UserLevelID in(3)
                           and LeaderID = @userID and userstatus = 1
                           order by fname,lname
                           end
                           else
                           begin
                            Select FName + ' ' + LName as TeamName
                           ,userID
                           from tbluser
                           where UserStatus = 1 and UserLevelID in(3) and userstatus = 1
                           order by fname,lname
                           end
            ">
            <SelectParameters>
                <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
               <asp:CookieParameter  Name="userID" CookieName ="userID" />
            </SelectParameters>
        </asp:SqlDataSource>
        
        <asp:SqlDataSource ID="SqlUserLevel" runat="server" 
            ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand="if @UserLevel = 3  
                           begin
                          Select * from tblUserLevel
                          where LevelID in(5)
                           end
                           else if @UserLevel = 2
                           begin
                          Select * from tblUserLevel
                          where LevelID in(5,3)
                           end
                           else 
                           begin
                            Select * from tblUserLevel
                          where LevelID in(5,3,2)
                           end
            ">
            <SelectParameters>
                <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
            </SelectParameters>
        </asp:SqlDataSource>
         <asp:SqlDataSource ID="SqlType" runat="server" 
            ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand="Select * from Tbl_Type
            ">
            
        </asp:SqlDataSource>
         <asp:SqlDataSource ID="SqlCompany" runat="server" 
            ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand="select -1 as ProTypeID,'ไม่ระบุ' as ProTypeName
	                        union SELECT 
	                         [ProTypeID]
	                        ,[ProTypeName]      
	                        FROM [Car].[dbo].[Tbl_ProductType]
	                        where ProTypeStatus=1
            ">
            
        </asp:SqlDataSource>
                 </asp:Content>



