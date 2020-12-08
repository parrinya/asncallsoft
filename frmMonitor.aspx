<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmMonitor.aspx.vb" Inherits="Modules_Manager_Manage_Tsr_frmMonitor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin-top: 30px; color: #0066FF; font-weight: bold; font-size: 20px;">

    Monitor Tsr</div>
<div style="text-align: center; margin-top: 5px">

    เลือก Team :
    <asp:DropDownList ID="ddUser" runat="server" AutoPostBack="True" 
        CssClass="jamp" DataSourceID="SqlSup" DataTextField="SupName" 
        DataValueField="UserID">
    </asp:DropDownList>

</div>
<div style="margin-top: 11px">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate >
        <asp:GridView ID="GvCall" runat="server" AutoGenerateColumns="False" 
            DataSourceID="SqlCall" Width="100%" DataKeyNames="Exten" 
        EmptyDataText="ไม่พบข้อมูล">
            <Columns>
                <asp:TemplateField HeaderText="ชื่อ-สกุล" SortExpression="FName">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("FName") %>'></asp:Label>
                        &nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Bind("LName") %>'></asp:Label>                       
                    </ItemTemplate>                   
                    <ItemStyle Width="200px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Web" SortExpression="lastaccess">
                    <ItemTemplate>
                        <font style="<%# Eval("lastaccessColor") %>">
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("lastaccess") %>' ></asp:Label>
                        </font>
                    </ItemTemplate>
                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SoftPhone" SortExpression="StatusRegister">
                    <ItemTemplate>                    
                      <asp:Label ID="lblRegister" runat="server" Text=''></asp:Label>
                    </ItemTemplate>
                   
                </asp:TemplateField>
                <asp:TemplateField HeaderText="StatusCall" SortExpression="StatusCall">
                    <ItemTemplate>                    
                     <asp:Label ID="lblStatusCall" runat="server" Text='' width="100%" heigth="100%"></asp:Label>                     
                    </ItemTemplate>                    
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnCmd1" runat="server" Text="ประชุม" 
                            Enabled='' 
                            CommandArgument="<%# Container.DataItemIndex %>" CommandName="Call1" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnCmd2" runat="server" Text="ฟัง" 
                            Enabled='' 
                            CommandArgument="<%# Container.DataItemIndex %>" CommandName="Call2" />
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:BoundField DataField="CallTalk" HeaderText="Call" ReadOnly="True" 
                    SortExpression="CallTalk" />
                <asp:BoundField DataField="CallTime" HeaderText="CallTime" ReadOnly="True" 
                    SortExpression="CallTime" />               
                <asp:BoundField DataField="SeatTalk" HeaderText="Call" ReadOnly="True" 
                    SortExpression="SeatTalk" />
                <asp:BoundField DataField="SystemTime" HeaderText="SystemTime" ReadOnly="True" 
                    SortExpression="SystemTime" />
                <asp:BoundField DataField="cnPCall" HeaderText="Count"></asp:BoundField>
                <asp:BoundField DataField="stPCall" HeaderText="SumTime"></asp:BoundField>
                <asp:TemplateField HeaderText="ProductiveTime" SortExpression="ProductiveTime">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("ProductiveTime") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("ProductiveTime") %>'></asp:Label>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        Productive<br />
                        Time
                    </HeaderTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="TimeAvg" HeaderText="TimeAvg" ReadOnly="True" 
                    SortExpression="TimeAvg" />
                <asp:BoundField DataField="mintimein" DataFormatString="{0:HH:mm}" 
                    HeaderText="FirstCall" SortExpression="mintimein" />
            </Columns>
            <HeaderStyle BackColor="#99CCFF" ForeColor="Black" />
        </asp:GridView>
        
        <asp:Timer ID="Timer1" runat="server" Interval="6000">
    </asp:Timer>
       
        </ContentTemplate>
        
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            </Triggers>
        </asp:UpdatePanel>
</div>
<div>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate >
        <iframe src="<%=strPhoneCall %>"  style ="display :none ";   width = "300px" frameborder="0" height="100"></iframe>
        </ContentTemplate>
        </asp:UpdatePanel>
</div>
<div>
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
    
    <asp:SqlDataSource ID="SqlCall" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="Select distinct a.*
                      ,b1.FName
,b1.LName
,case b1.loginstatus  when 0 then 'offline' else 'onlline' end as lastaccess
,case b1.loginstatus when 0
	then 'color:Red' 
	else 'color:green' end as lastaccessColor
,b.cnPCall
,b.stPCall
 from
(select 
a1.Exten
,Convert(VarChar,dateadd(SS,sum(case   when a2.cctbillsec&gt;  0 then a2.calltime end),0),108) as CallTime
,sum(case when a2.cctbillsec &gt; 0 then 1 else 0 end) as CallTalk
,Convert(VarChar,dateadd(SS,avg(a2.calltime),0),108) as TimeAvg
,Convert(VarChar,dateadd(SS,sum(a2.calltime),0),108) as ProductiveTime
,Convert(VarChar,dateadd(SS,sum(case a2.cctbillsec when 0 then a2.calltime end) ,0),108) as SystemTime
,sum(case a2.cctbillsec when 0 then 1 else 0 end) as SeatTalk
,MIN(a2.ccttimein ) as mintimein
from TblUser a1
Left Join TblCallControl a2 on a2.userid = a1.userid   
and Convert(VarChar,a2.cctTimeIn,111) between Convert(VarChar,GetDate(),111) and Convert(VarChar,GetDate(),111)
Where 
 a1.SupID = @SupID and
 Exten not in('','0000','0')  and a1.userstatus = 1
group by a1.Exten ) a
Inner Join TblUser b1 on b1.Exten = a.Exten and b1.UserStatus = 1
Left Join 
(
select userID,count(*) as cnPCall ,Convert(VarChar,dateadd(SS,sum(DATEDIFF(SS,TblLogPhoneTime.datestart,TblLogPhoneTime.datestop)),0),108) as stPCall
from TblLogPhoneTime 
where  Convert(VarChar,TblLogPhoneTime.datestart,111) between Convert(VarChar,GetDate(),111) and Convert(VarChar,GetDate(),111)
group by userID
) b on b.userID=b1.UserID

" >
        <SelectParameters>
            <asp:Parameter Name="SupID" />
        </SelectParameters>
    </asp:SqlDataSource>
</div>
</asp:Content>

