<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmSaleSource.aspx.vb" Inherits="Modules_Manager_Report_frmSaleSource" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <tr>
            <td>
           
                <asp:RadioButton id="rdt1" type="radio"  runat="server" AutoPostBack="True" />เลือก ตามSource  :
                <asp:DropDownList ID="ddSource" runat="server"
                 DataSourceID="SqlSource" DataTextField="Groupname" DataValueField="groupID"  
                AutoPostBack="True">
                </asp:DropDownList>      
      
            </td>
  
        </tr>
        <tr>
            <td>           
                <asp:RadioButton id="rdt2" type="radio"  runat="server" AutoPostBack="True" />เลือก ตามวันคุ้มครอง
                ระหว่าง วันที่ 
                <asp:TextBox ID="txtdate1" runat="server"></asp:TextBox>
                <asp:MaskedEditExtender ID="txtdate1_MaskedEditExtender" runat="server" 
                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate1">
                </asp:MaskedEditExtender>

                <asp:CalendarExtender ID="txtdate1_CalendarExtender" runat="server" 
                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate1">
                </asp:CalendarExtender>

                ถึง 

                <asp:TextBox ID="txtdate2" runat="server"></asp:TextBox>

                <asp:MaskedEditExtender ID="txtdate2_MaskedEditExtender" runat="server" 
                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate2">
                </asp:MaskedEditExtender>

                <asp:CalendarExtender ID="txtdate2_CalendarExtender" runat="server" 
                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate2">
                </asp:CalendarExtender>

                <asp:DropDownList ID="ddUser" runat="server"
                DataSourceID="SqlUser" DataTextField="nameuser" DataValueField="UserID" AutoPostBack="true">
                </asp:DropDownList>  

                <asp:DropDownList ID="ddSup" runat="server"
                DataSourceID="SqlSup" DataTextField="SupName" DataValueField="UserID"  AutoPostBack="true"
                >
                </asp:DropDownList>  
 
                <asp:DropDownList ID="ddSup2" runat="server"
                DataSourceID="SqlSup2" DataTextField="SupName" DataValueField="UserID"  AutoPostBack="true"
                >
                </asp:DropDownList>  

            </td>

        </tr>
    
        <tr>
            <td>           
                <asp:RadioButton id="rdt3" type="radio"  runat="server" AutoPostBack="true" />เลือก ตามAssignDate
                ระหว่าง วันที่ 
                <asp:TextBox ID="txtdate3" runat="server"></asp:TextBox>

                <asp:MaskedEditExtender ID="txtdate3_MaskedEditExtender" runat="server" 
                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate3">
                </asp:MaskedEditExtender>

                <asp:CalendarExtender ID="txtdate3_CalendarExtender" runat="server" 
                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate3">
                </asp:CalendarExtender>

                ถึง 

                <asp:TextBox ID="txtdate4" runat="server"></asp:TextBox>
                <asp:MaskedEditExtender ID="txtdate4_MaskedEditExtender" runat="server" 
                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate4">
                </asp:MaskedEditExtender>

                <asp:CalendarExtender ID="txtdate4_CalendarExtender" runat="server" 
                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate4">
                </asp:CalendarExtender>

                <asp:DropDownList ID="ddUser2" runat="server"
                DataSourceID="SqlUser2" DataTextField="nameuser" DataValueField="UserID" AutoPostBack="true">
                </asp:DropDownList>  

                <asp:DropDownList ID="ddSup3" runat="server"
                DataSourceID="SqlSup3" DataTextField="SupName" DataValueField="UserID"  AutoPostBack="true"
               >
               </asp:DropDownList>  

               <asp:DropDownList ID="ddSup4" runat="server"
               DataSourceID="SqlSup4" DataTextField="SupName" DataValueField="UserID"  AutoPostBack="true"
               >
               </asp:DropDownList>  

            </td>

            <td rowspan=2>  <asp:Button ID="btn1" runat="server" Text="แสดง" /></td>
        </tr>
    </table>
 <div>
    <iframe  src ="<%=strReport %>" frameborder="0" height="800" width="100%"></iframe>
</div>
<asp:SqlDataSource ID="SqlSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand=" if @UserLevel=1 
        begin 
        	select  tblsourcegroup.groupID,tblsourcegroup.Groupname  from tblsourcegroup 
 		where year(tblsourcegroup.createdate) >= 2015 
        union  

  SELECT [SourceID] as 'groupID'
      ,'Add New( '+[SourceName]+' )' as 'Groupname'
  FROM Tbl_MSourceData
  WHERE [SourceStatus]=1
  order by groupid desc
 		
 	end
        Else if @UserLevel=2  begin
         	select  tblsourcegroup.groupID,tblsourcegroup.Groupname  from tblsourcegroup 
 		where year(tblsourcegroup.createdate) >= 2015 
        union  

  SELECT [SourceID] as 'groupID'
      ,'Add New( '+[SourceName]+' )' as 'Groupname'
  FROM Tbl_MSourceData
  WHERE [SourceStatus]=1
  order by groupid desc
 		
        end
	Else if @UserLevel=3  begin
         	select  tblsourcegroup.groupID,tblsourcegroup.Groupname  from tblsourcegroup 
 		where year(tblsourcegroup.createdate) >= 2015 
 		union  

  SELECT [SourceID] as 'groupID'
      ,'Add New( '+[SourceName]+' )' as 'Groupname'
  FROM Tbl_MSourceData
  WHERE [SourceStatus]=1
  order by groupid desc

        end
        ">
    <SelectParameters>
            <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
            <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
        </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlUser" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand=" if @UserLevel=1 
        begin 

        select distinct a2.FName + ' ' + a2.LName as nameuser,a2.UserID from TblUser a1
        inner join TblUser a2 on a1.LeaderID=a2.UserID
        where a1.UserLevelID=5 and a1.UserStatus=1 and a2.UserStatus=1
        union 
              select 
              'All' as nameuser ,0 as userID 
        end

          Else if @UserLevel=2  begin
            select distinct a2.FName + ' ' + a2.LName as nameuser,a2.UserID from TblUser a1
            inner join TblUser a2 on a1.LeaderID=a2.UserID
            where a1.UserLevelID=5 and a1.UserStatus=1 and a2.UserStatus=1
          end

Else if @UserLevel=3  begin
         select tbluser.UserID,tbluser.FName+' '+tbluser.LName as nameuser 
from tbluser where UserStatus=1 and UserLevelID=5 and  tbluser.SupID=@userID
        end
        ">
    <SelectParameters>
            <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
            <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
        </SelectParameters>
</asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlUser2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand=" if @UserLevel=1 
        begin 

        select distinct a2.FName + ' ' + a2.LName as nameuser,a2.UserID from TblUser a1
        inner join TblUser a2 on a1.LeaderID=a2.UserID
        where a1.UserLevelID=3 and a1.UserStatus=1 and a2.UserStatus=1
                     
               
        end

          Else if @UserLevel=2  begin
            select distinct a2.FName + ' ' + a2.LName as nameuser,a2.UserID from TblUser a1
            inner join TblUser a2 on a1.LeaderID=a2.UserID
            where a1.UserLevelID=3 and a1.UserStatus=1 and a2.UserStatus=1
          end

Else if @UserLevel=3  begin
         select tbluser.UserID,tbluser.FName+' '+tbluser.LName as nameuser 
from tbluser where UserStatus=1 and UserLevelID=5 and  tbluser.SupID=@userID
        end
        ">
    <SelectParameters>
            <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
            <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
        </SelectParameters>
</asp:SqlDataSource>

            <asp:SqlDataSource ID="SqlSup" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="
               if @UserLevel=3
                begin
                    Select a1.userID
                    ,a1.FName + ' ' + a1.LName as SupName
                    from TblUser a1
                    Where a1.userID = @userID
                end
           

                else if @UserLevel=1
                begin
                    Select a1.userID
                    ,a1.FName + ' ' + a1.LName as SupName
                    from tbluser a1 
                    where a1.UserLevelID in(3) and a1.UserStatus = 1 and a1.LeaderID = @LeaderID   
                
                union
                 select 0 as userID
                ,'All' as SupName               
                end

                
                ">
        <SelectParameters>
            <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
            <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
            <asp:ControlParameter ControlID="ddUser" Name="LeaderID" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>

            <asp:SqlDataSource ID="SqlSup2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="
                           
                if @UserLevel=2
                begin
                    Select a1.userID
                    ,a1.FName + ' ' + a1.LName as SupName
                    from tbluser a1 
                    where a1.UserLevelID in(3) and a1.UserStatus = 1 and a1.LeaderID = @userID          

                union
                 select 0 as userID
                ,'All' as SupName   
                  
                end   

                
                ">
        <SelectParameters>
            <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
            <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlSup3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="
               if @UserLevel=3
                begin
                    Select a1.userID
                    ,a1.FName + ' ' + a1.LName as SupName
                    from TblUser a1
                    Where a1.userID = @userID
                end
            

                else if @UserLevel=1
                begin
                    Select a1.userID
                    ,a1.FName + ' ' + a1.LName as SupName
                    from tbluser a1 
                    where a1.UserLevelID in(3) and a1.UserStatus = 1 and a1.LeaderID = @LeaderID                  
                end

                
                ">
        <SelectParameters>
            <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
            <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
            <asp:ControlParameter ControlID="ddUser2" Name="LeaderID" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>

            <asp:SqlDataSource ID="SqlSup4" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="
                           
                if @UserLevel=2
                begin
                    Select a1.userID
                    ,a1.FName + ' ' + a1.LName as SupName
                    from tbluser a1 
                    where a1.UserLevelID in(3) and a1.UserStatus = 1 and a1.LeaderID = @userID                  
                end   

                
                ">
        <SelectParameters>
            <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
            <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>