<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmEditStatusCar.aspx.vb" Inherits="Modules_Manager_Manage_Case_frmEditStatusCar" %>

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
        <asp:DropDownList ID="ddSup" runat="server" CssClass="jamp" AutoPostBack="True"
            DataSourceID="SqlSup" DataTextField="SupName" DataValueField="userID">
        </asp:DropDownList>  TSR:        
         <asp:DropDownList ID="ddTsr" runat="server" CssClass="jamp" AutoPostBack="True"
            DataSourceID="SqlTsr" DataTextField="Name" DataValueField="userID">
        </asp:DropDownList> ชื่อ-สกุลลูกค้า/ทะเบียน :
        <asp:textbox runat="server" id="txtsearch"> </asp:textbox>
    <asp:button runat="server" text="ค้นหา" id="btnSearch"/>
 </div>
<asp:UpdatePanel ID="UpdatePanel3" runat="server">   
    <ContentTemplate>
       <div  Height="400px" ScrollBars="Auto">
        <asp:GridView runat="server" id="GVShow" AutoGenerateColumns="False" DataSourceID="SqlSearch5"
              DataKeyNames="IdCar"  Width="100%" AutoPostBack="True">      
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" Text="เลือก" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Edit"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button ID="Button2" runat="server" Text="บันทึก" CommandName="Update1"/>
                        <asp:Button ID="Button3" runat="server" Text="ยกเลิก" CommandName="Cancel"/>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# bind("idcar") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="Label11" runat="server" Text='<%# bind("idcar") %>'></asp:Label>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ทะเบียน">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# bind("Carid") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# bind("Carid") %>'></asp:Label>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ชื่อลูกค้า">
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# bind("nameCus") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# bind("nameCus") %>'></asp:Label>
                    </EditItemTemplate>
                 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ชื่อ TSR">
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# bind("nametsr") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="Label9" runat="server" Text='<%# bind("nametsr") %>'></asp:Label>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="สถานะปัจจุบัน">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# bind("StatusName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" 
                            DataSourceID="SqlStatusFrom" DataTextField="StatusName" 
                            DataValueField="StatusID" SelectedValue='<%# bind("CurStatus") %>'>
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="สถานะใหม่">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SqlStatusTo" 
                            DataTextField="StatusName" DataValueField="StatusID">
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>        
        
         <HeaderStyle CssClass="td-header" Height="30px" ForeColor="Black" 
            BackColor="#99CCFF" ></HeaderStyle></asp:GridView>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:SqlDataSource ID="SqlLead" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="
                if @UserLevel=3
                begin
                Select a2.userID
                ,a2.FName + ' ' + a2.LName as SupName
                from TblUser a1
                Inner Join Tbluser a2 on a1.LeaderID = a2.userID
                Where a1.UserID = @userID and a1.TypeTsr=3
                end
                Else if @UserLevel=2 
                Begin
                Select a1.userID
                ,a1.FName + ' ' + a1.LName as SupName
                from TblUser a1
                Where a1.userID = @userID and a1.TypeTsr=3
                End
                else 
                begin
                Select a1.userID
                ,a1.FName + ' ' + a1.LName as SupName
                from tbluser a1 
                where a1.UserLevelID in(2) and a1.UserStatus = 1 and a1.TypeTsr=3 end
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
                    Where a1.userID = @userID and a1.TypeTsr=3
               
                end
                
                else 
                    begin
                    Select a1.userID
                    ,a1.FName + ' ' + a1.LName as SupName
                    from tbluser a1 
                    where a1.UserLevelID in(3) and a1.UserStatus = 1 and a1.LeaderID = @LeaderID
                    and a1.TypeTsr=3
                 
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
        SelectCommand="Select a1.userID
                    ,a1.FName + ' ' + a1.LName as Name
                    from TblUser a1
                    Where a1.LeaderID = @LeaderID and a1.SupID=@SupID and a1.UserStatus = 1
                    and a1.TypeTsr=3 and a1.UserLevelID in(5)">
        <SelectParameters>
            <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
            <asp:ControlParameter ControlID="ddLead" Name="LeaderID" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="ddSup" Name="SupID" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>

   
    <asp:SqlDataSource ID="SqlStatusFrom" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT  [StatusID] ,[StatusName]     
        FROM [Car].[dbo].[TblStatus]
        where StatusID in(5,11)">        
    
    </asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlStatusTo" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT  [StatusID] ,[StatusName]     
        FROM [Car].[dbo].[TblStatus]
        where StatusID in(8,6)">        
        
    </asp:SqlDataSource>    
     <asp:SqlDataSource ID="SqlSearch5" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT  TblCar.IdCar, TblCar.CusID, TblCar.CurStatus, TblStatus.StatusName, 
        TblCustomerInit.InitTH+' '+TblCustomer.FNameTH+' '+ TblCustomer.LNameTH as  nameCus,
		TblCar.CarID,TblCar.AssignTo, TblUser.FName+' '+TblUser.LName  as  nameTsr
                        FROM   TblCar INNER JOIN
                         TblCustomer ON TblCar.CusID = TblCustomer.CusID INNER JOIN
                         TblCustomerInit ON TblCustomer.InitID = TblCustomerInit.InitID INNER JOIN
                         TblStatus ON TblCar.CurStatus = TblStatus.StatusID INNER JOIN
                         TblUser ON TblCar.AssignTo = TblUser.UserID
                        WHERE        
                            (TblCar.CurStatus IN (5, 11)) AND 
                            (TblUser.TypeTsr = 3) AND 
                            (TblUser.UserLevelID = 5) AND 
                            (TblUser.UserStatus = 1)  AND
                            TblUser.SupID=@SupID  AND  
                            TblUser.LeaderID=@LeaderID 
                             And   TblCar.AssignTo=@TsrID
                           and (TblCustomer.FNameTH+ ' ' +  TblCustomer.LNameTH like @txt or TblCar.CarID like @txt )
                "
                UpdateCommand="UPDATE tblcar SET CurStatus = @statusidnew,CntStatus=0  WHERE (idcar = @idcar)"
                 InsertCommand="  INSERT INTO tblLogEditstatusCar  select idcar,CurStatus,@statusidnew,@userID,getdate() from tblcar where  idcar=@idcar"
                >
        <SelectParameters>
            <asp:Parameter Name="txt" Type = "String" DefaultValue="0"/>
            <asp:ControlParameter ControlID="ddTsr" Name="TsrID" PropertyName="SelectedValue" />
            <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
            <asp:CookieParameter CookieName="TypeTsr" Name="TypeTsr" />
            <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
            <asp:ControlParameter ControlID="ddLead" Name="LeaderID" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="ddSup" Name="SupID" PropertyName="SelectedValue" />
        </SelectParameters>
         <UpdateParameters>
                <asp:Parameter Name="idcar" />
                <asp:Parameter Name="statusidnew" />
         </UpdateParameters>
          <InsertParameters>
            <asp:Parameter Name="idcar" />
            <asp:Parameter Name="statusidnew" />           
            <asp:CookieParameter Name="userID" CookieName ="userID" />           
        </InsertParameters>
    </asp:SqlDataSource>
</asp:Content>

