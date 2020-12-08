<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmListSendCVandPV.aspx.vb" Inherits="Modules_Manager_Manage_Tsr_frmListSendCVandPV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 267px;
        }
        .style2
        {
            width: 230px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin-top: 30px; color: #0066FF; font-weight: bold; font-size: 20px;">
    Waiting list 
    </div>
    <div style="border-top: 2px solid #66CCFF; text-align: center;">
    <table width="100%" border="1">
        <tr>
            <td>LEAD :
                <asp:DropDownList ID="ddLead" runat="server" CssClass="jamp" DataSourceID="SqlLead" DataTextField="SupName" DataValueField="userID"  AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td class="style2">SUP :
                <asp:DropDownList ID="ddSup" runat="server" CssClass="jamp" AutoPostBack="True" DataSourceID="SqlSup" DataTextField="SupName" DataValueField="userID">
                </asp:DropDownList>
            </td>
            <td class="style1">TSR :        
            <asp:DropDownList ID="ddTsr" runat="server" CssClass="jamp" AutoPostBack="True" DataSourceID="SqlTsr" DataTextField="Name" DataValueField="userID">
            </asp:DropDownList>

            </td>
            <td align="left">
            <input id="name" type="radio"  runat="server" />ชื่อ-สกุล</td>
            <td  align="left">  <asp:textbox runat="server" id="txtname" Width="100px"></asp:textbox></td>
       
    <td rowspan="4"><asp:button runat="server" text="ค้นหา" id="btnSearch"/></td>
    </tr>

    <tr>
        <td colspan="3"></td>
        <td align="left"><input id="carid" type="radio"  runat="server" />ทะเบียน</td>
        <td align="left"><asp:textbox runat="server" id="txtcarid"  Width="100px"></asp:textbox></td>
    </tr>
     <tr>
        <td colspan="3"></td>
        <td align="left"><input id="appid" type="radio"  runat="server" />AppID</td>
        <td align="left"><asp:textbox runat="server" id="txtappid"  Width="100px"></asp:textbox></td>
    </tr>
    <tr>
    <td colspan="3"></td>
    <td align="left"><input id="all" type="radio"  runat="server" />ทั้งหมด</td><td></td>
    </tr>

    </table>
 </div>
 <asp:UpdatePanel ID="UpdatePanel3" runat="server" >   
    <ContentTemplate>   
    <div style="width: 100%; height: 400px; overflow: scroll">
    <asp:GridView runat="server" id="GVShow" AutoGenerateColumns="False" 
              DataKeyNames="AppID"  Width="100%" style="height:100px; overflow:auto"
            BackColor="White" AutoPostBack="True" EmptyDataText="ไม่พบรายการ">      
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" Text="ส่ง"  
                        CommandName="select" CommandArgument="<%# Container.DataItemIndex %>"/>
                    </ItemTemplate>                   
                </asp:TemplateField>
                <asp:BoundField DataField="AppID" HeaderText="AppID"></asp:BoundField>
                 <asp:BoundField DataField="ProTypeName" HeaderText="บ.ประกันภัย"></asp:BoundField>
                <asp:BoundField DataField="Carid" HeaderText="ทะเบียน"></asp:BoundField>
                <asp:BoundField DataField="CusName" HeaderText="ชื่อลูกค้า"></asp:BoundField>
                <asp:BoundField DataField="Tsrname" HeaderText="ชื่อ TSR"></asp:BoundField>
                <asp:BoundField DataField="VMI" HeaderText="VMI"></asp:BoundField>
                <asp:BoundField DataField="CMI" HeaderText="CMI"></asp:BoundField>              
                <asp:BoundField DataField="SuccessDate" HeaderText="SuccessDate" />
                <asp:BoundField DataField="QcSuccessDate" HeaderText="QcSuccessDate" />
            </Columns>
         <HeaderStyle CssClass="td-header" Height="30px" ForeColor="Black" 
            BackColor="#99CCFF" ></HeaderStyle><SelectedRowStyle BackColor="#FFCCCC" /></asp:GridView>    
   </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:SqlDataSource ID="SqlFindData" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand=" 

select  tblapplication.AppID
,Tbl_ProductType.ProTypeName
,tblcar.Carid
,tblcustomer.FNameTH+' '+tblcustomer.LNameTH as CusName
,case when tblapplication.IsProvalue=1 then   tblapplication.ProValue else 0 end as VMI
,case when tblapplication.IsCarpet=1   then   tblapplication.CarPet else 0 end as CMI
,tblapplication.SuccessDate
,tblapplication.QcSuccessDate
,tbluser.FName+' '+tbluser.LName as Tsrname
from tblapplication 
inner join tblcar on tblapplication.idcar=tblcar.idcar
inner join tblcustomer on tblapplication.cusid=tblcustomer.cusid
inner join Tbl_ProductType on tblapplication.ProDuctID=Tbl_ProductType.ProTypeID
inner join tbluser on tblcar.AssignTo=tbluser.UserID
inner join tbluser tmp on tmp.UserID=tbluser.SupID

where tblapplication.appstatus=1
and tblapplication.Statusqc in(1,7) 
and tblcar.CurStatus in(3,4) 
and Case @SupID when -1 then -1 else tbluser.SupID end = @SupID
and Case @LeaderID  when -1 then -1 else tbluser.LeaderID end = @LeaderID 
and Case @TsrID  when -1 then -1 else tblcar.AssignTo end = @TsrID 
and tblapplication.Appid not in(select appid from TblLogSendCVandPV)
and tblapplication.flagsend=0
"
 InsertCommand="INSERT INTO TblLogSendCVandPV(AppID,CreateID) VALUES(@AppID,@CreateID)">
 <InsertParameters>           
            <asp:CookieParameter CookieName = "userID" Name="CreateID" />
            <asp:Parameter Name="AppID" />
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter Name="Data" Type = "String" DefaultValue="0"/>
            <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
            <asp:ControlParameter ControlID="ddLead" Name="LeaderID" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="ddSup" Name="SupID" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="ddTsr" Name="TsrID" PropertyName="SelectedValue" />
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
                select -1 as userID,'All'  as SupName
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
                      select -1 as userID,'All'  as SupName
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
        SelectCommand="
            select -1 as userID,'All'  as Name
            union 
            Select a1.userID
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

