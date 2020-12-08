<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmEditPackageRenew.aspx.vb" Inherits="Modules_Manager_Manage_Case_frmEditPackageRenew" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="border-top: 2px solid #66CCFF; text-align: center;">
<table width="100%" border="0"><tr><td>
        LEAD :
        <asp:DropDownList ID="ddLead" runat="server" CssClass="jamp" 
            DataSourceID="SqlLead" DataTextField="SupName" DataValueField="userID" 
            AutoPostBack="True">
        </asp:DropDownList></td><td>
&nbsp;SUP :
        <asp:DropDownList ID="ddSup" runat="server" CssClass="jamp" AutoPostBack="True"
            DataSourceID="SqlSup" DataTextField="SupName" DataValueField="userID">
       </asp:DropDownList></td><td>  TSR:        
         <asp:DropDownList ID="ddTsr" runat="server" CssClass="jamp" AutoPostBack="True"
            DataSourceID="SqlTsr" DataTextField="Name" DataValueField="userID">
        </asp:DropDownList></td>
        <td align="left">
            <input id="name" type="radio"  runat="server" />ชื่อ-สกุลลูกค้า/ทะเบียน          
            <asp:textbox runat="server" id="txtsearch"></asp:textbox></td>
       
    <td rowspan=2><asp:button runat="server" text="ค้นหา" id="btnSearch"/></td>
    </tr>
    <tr><td colspan=3 ></td><td align="left"> 
        <input id="successdate" type="radio"  runat="server"/>วันที่Success      
            <asp:TextBox ID="txtdate1" runat="server" Width="70px"></asp:TextBox>
                <asp:MaskedEditExtender ID="txtdate1_MaskedEditExtender" runat="server" 
                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate1">
                </asp:MaskedEditExtender>
                <asp:CalendarExtender ID="txtdate1_CalendarExtender" runat="server" 
                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate1">
                </asp:CalendarExtender>
                -<asp:TextBox ID="txtdate2" runat="server" Width="70px"></asp:TextBox>
                <asp:MaskedEditExtender ID="txtdate2_MaskedEditExtender" runat="server" 
                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate2">
                </asp:MaskedEditExtender>
                <asp:CalendarExtender ID="txtdate2_CalendarExtender" runat="server" 
                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate2">
                </asp:CalendarExtender>
        </td></tr>
    </table>
 </div>
<asp:UpdatePanel ID="UpdatePanel3" runat="server" >   
    <ContentTemplate>       
       <asp:GridView runat="server" id="GVShow" AutoGenerateColumns="False" 
              DataKeyNames="AppID"  Width="100%" AllowPaging="True" PageSize="10" 
            BackColor="White" AutoPostBack="True">      
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" Text="เลือก"  CommandName="select" CommandArgument="<%# Container.DataItemIndex %>"/>
                    </ItemTemplate>                   
                </asp:TemplateField>
                <asp:BoundField DataField="appid" HeaderText="AppID"></asp:BoundField>
                <asp:BoundField DataField="Carid" HeaderText="ทะเบียน"></asp:BoundField>
                <asp:BoundField DataField="Cusname" HeaderText="ชื่อลูกค้า"></asp:BoundField>
                <asp:BoundField DataField="Tsrname" HeaderText="ชื่อ TSR"></asp:BoundField>               
                <asp:BoundField DataField="SuccessDate" HeaderText="SuccessDate" />
                <asp:BoundField DataField="Astatusname" />
            </Columns>
         <HeaderStyle CssClass="td-header" Height="30px" ForeColor="Black" 
            BackColor="#99CCFF" ></HeaderStyle><SelectedRowStyle BackColor="#FFCCCC" /></asp:GridView>
    <div>
    <iframe src="<%= strLink %>" frameborder="0" height="350" width="100%"></iframe>
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
     <asp:SqlDataSource ID="SqlNameCarID" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT  
        TblApplication.AppID,
        TblApplication.AppStatus, 
        TblApplication.SuccessDate, 
        TblApplication.QcSuccessDate, 
        Tbl_AStatus.Astatusname, 
        TblApplication.IsSuccess, TblApplication.CreateID,
        TblUser.FName+' '+TblUser.LName as Tsrname, 
        TblCustomer.FNameTH+' '+TblCustomer.LNameTH as Cusname, 
        TblCar.CurStatus, 
        TblCar.CarID,TblApplication.Idcar
        FROM  TblApplication INNER JOIN
              TblCustomer ON TblApplication.cusid = TblCustomer.CusID INNER JOIN
              TblCar ON TblApplication.Idcar = TblCar.IdCar INNER JOIN
              TblUser ON TblApplication.CreateID = TblUser.UserID INNER JOIN
	          Tbl_AStatus ON TblApplication.Statusqc=Tbl_AStatus.Astatusid
        WHERE (TblApplication.Statusqc IN (0,2,3)) 
        AND (TblApplication.AppStatus = 1) And 
        TblUser.SupID=@SupID  AND  
        TblUser.LeaderID=@LeaderID And   
        TblApplication.CreateID=@TsrID
        AND (TblCar.CurStatus IN (3,4,25,26)) 
        and  (TblCustomer.FNameTH+ ' ' +  TblCustomer.LNameTH like @txt or TblCar.CarID like @txt )">
        <SelectParameters>
             <asp:Parameter Name="txt" Type = "String" DefaultValue="0"/>
            <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
            <asp:ControlParameter ControlID="ddLead" Name="LeaderID" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="ddSup" Name="SupID" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="ddTsr" Name="TsrID" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlSuccessDate" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT  
        TblApplication.AppID,
        TblApplication.AppStatus, 
        TblApplication.SuccessDate, 
        TblApplication.QcSuccessDate, 
        Tbl_AStatus.Astatusname, 
        TblApplication.IsSuccess, TblApplication.CreateID,
        TblUser.FName+' '+TblUser.LName as Tsrname, 
        TblCustomer.FNameTH+' '+TblCustomer.LNameTH as Cusname, 
        TblCar.CurStatus, 
        TblCar.CarID,TblApplication.Idcar
        FROM  TblApplication INNER JOIN
              TblCustomer ON TblApplication.cusid = TblCustomer.CusID INNER JOIN
              TblCar ON TblApplication.Idcar = TblCar.IdCar INNER JOIN
              TblUser ON TblApplication.CreateID = TblUser.UserID INNER JOIN
	          Tbl_AStatus ON TblApplication.Statusqc=Tbl_AStatus.Astatusid
        WHERE (TblApplication.Statusqc IN (0,2,3)) 
        AND (TblApplication.AppStatus = 1) And 
        TblUser.SupID=@SupID  AND  
        TblUser.LeaderID=@LeaderID And   
        TblApplication.CreateID=@TsrID
        AND (TblCar.CurStatus IN (3,4,25,26)) 
        and  Convert(VarChar,TblApplication.SuccessDate,111) between @date1 and @date2
        ">
        <SelectParameters>
           <asp:Parameter Name="date1" DefaultValue="0" />
           <asp:Parameter Name="date2" DefaultValue="0" />
            <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
            <asp:ControlParameter ControlID="ddLead" Name="LeaderID" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="ddSup" Name="SupID" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="ddTsr" Name="TsrID" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

