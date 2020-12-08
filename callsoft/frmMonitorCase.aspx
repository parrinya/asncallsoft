<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmMonitorCase.aspx.vb" Inherits="Modules_Manager_Manage_Tsr_frmMonitorCase" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register assembly="Infragistics35.WebUI.UltraWebGrid.v8.3, Version=8.3.20083.1009, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.UltraWebGrid" tagprefix="igtbl" %>

<%@ Register assembly="Infragistics35.WebUI.WebDataInput.v8.3, Version=8.3.20083.1009, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin-top: 30px; color: #0066FF; font-weight: bold; font-size: 20px;">

    Monitor Case</div>
<div style="text-align: center; font-weight: 700; background-color: #99CCFF;">
   ระบุ TSR : <asp:DropDownList ID="ddTsr1" runat="server" AppendDataBoundItems="True" 
            AutoPostBack="True" CssClass="jamp" DataSourceID="SqlUser" 
            DataTextField="TsrName" DataValueField="UserID">
            <asp:ListItem Value="9999">[ระบุ Tsr]</asp:ListItem>
        </asp:DropDownList>
    ค้นหา ชื่อ-สกุล/ทะเบียน<asp:TextBox ID="txtSearch" runat="server" Width="250px"></asp:TextBox>
    <asp:Button ID="Button4" runat="server" Text="แสดง" />
    </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate >
        
        </ContentTemplate>
        </asp:UpdatePanel>
     <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
        Width="100%">
        <asp:TabPanel runat="server" HeaderText="NewCase" ID="TabPanel1">
            
        
<ContentTemplate >
        
            

        
            <asp:Panel ID="Panel2" runat="server" Height="400px" ScrollBars="Auto">
                
 <asp:GridView ID="GvNewCase" runat="server" AutoGenerateColumns="False" DataKeyNames="IdCar" 
                         DataSourceID="SqlNewCase" Width="100%">
     <Columns>
<asp:BoundField DataField="CusName" HeaderText="ชื่อ-สกุล" ReadOnly="True" 
                         SortExpression="CusName" />
<asp:BoundField DataField="CarID" HeaderText="ทะเบียน" SortExpression="CarID" />
<asp:BoundField DataField="CarBrand" HeaderText="ยี่ห้อ-รุ่น" ReadOnly="True" 
                         SortExpression="CarBrand" />
<asp:BoundField DataField="CarBuyDate" DataFormatString="{0:dd/MM/yyyy}" 
                         HeaderText="วันหมดประกัน" SortExpression="CarBuyDate" />
<asp:BoundField DataField="AssCreateDate" 
                         HeaderText="AssignDate" SortExpression="AssCreateDate" />
<asp:BoundField DataField="Groupname" HeaderText="Groupname" 
                         SortExpression="Groupname" />
<asp:BoundField DataField="createdate" DataFormatString="{0:dd/MM/yyyy}" 
                         HeaderText="createdate" SortExpression="createdate" />
</Columns>

<HeaderStyle BackColor="#99CCFF" />
</asp:GridView>
 
                 </asp:Panel>

         
        
        
</ContentTemplate>
        <HeaderTemplate >
 NewCase(<asp:Label ID="lblNewCase" runat="server" Text="0"></asp:Label>)
 
        
</HeaderTemplate>
            
        
</asp:TabPanel>
        
         <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Follow">
             
             
<ContentTemplate>
                 <asp:Panel ID="Panel1" runat="server" Height="400px" ScrollBars="Auto" 
                     Width="100%">
                     <asp:GridView ID="GvFollow" runat="server" AutoGenerateColumns="False" DataKeyNames="IdCar" 
                         DataSourceID="SqlFollow" Width="150%">
                 <Columns>
                     
                     <asp:TemplateField>
                         
                         <ItemTemplate>
                             

                             <asp:Button ID="Button3" runat="server" CommandArgument='<%# Eval("IdCar") %>' 
                                 CommandName="Select" Text="แก้ไข" />
                             
                         </ItemTemplate>
                         
                     </asp:TemplateField>
                     <asp:BoundField DataField="CusName" HeaderText="ชื่อ-สกุล" ReadOnly="True" 
                         SortExpression="CusName" />
                     <asp:BoundField DataField="CarID" HeaderText="ทะเบียน" SortExpression="CarID" />
                     <asp:BoundField DataField="CarBrand" HeaderText="ยี่ห้อ-รุ่น" ReadOnly="True" 
                         SortExpression="CarBrand" />
                     
                     <asp:BoundField DataField="CarBuyDate" DataFormatString="{0:dd/MM/yyyy}" 
                         HeaderText="วันหมดประกัน" SortExpression="CarBuyDate" />
                     

                     <asp:BoundField DataField="AppointDate" HeaderText="วันนัด" 
                         SortExpression="AppointDate" />
                     
                     <asp:BoundField DataField="CallDate" HeaderText="โทรล่าสุด" 
                         SortExpression="CallDate" />
                     
                     <asp:BoundField DataField="Comments" HeaderText="หมายเหตุ" 
                         SortExpression="Comments" />
                     

                     <asp:BoundField DataField="AssCreateDate" 
                         HeaderText="AssignDate" SortExpression="AssCreateDate" />
                     
                     <asp:BoundField DataField="Groupname" HeaderText="Groupname" 
                         SortExpression="Groupname" />
                     <asp:BoundField DataField="createdate" DataFormatString="{0:dd/MM/yyyy}" 
                         HeaderText="createdate" SortExpression="createdate" />
                 </Columns>
                 <HeaderStyle BackColor="#99CCFF" />
                         
                 </asp:GridView>
                     
                 </asp:Panel>
                
             
</ContentTemplate>
              <HeaderTemplate >
Follow(<asp:Label ID="lblFollow" runat="server" Text="0"></asp:Label>)
 
        
</HeaderTemplate>
             
         
</asp:TabPanel>
        
         <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="CallBack">
             
         
<ContentTemplate >
         <asp:Panel ID="Panel3" runat="server" Height="400px" ScrollBars="Auto" 
                     Width="100%">
             
<asp:GridView ID="GvCallBack" runat="server" AutoGenerateColumns="False" DataKeyNames="IdCar" 
                         DataSourceID="SqlCallBack" Width="150%">
                 <Columns>
                     

                     <asp:TemplateField>
                         

                         <ItemTemplate>
                             

                             <asp:Button ID="Button3" runat="server" CommandArgument='<%# Eval("IdCar") %>' 
                                 CommandName="Select" Text="แก้ไข" />
                             

                         </ItemTemplate>
                         

                     </asp:TemplateField>
                     

                     <asp:BoundField DataField="CusName" HeaderText="ชื่อ-สกุล" ReadOnly="True" 
                         SortExpression="CusName" />
                     

                     <asp:BoundField DataField="CarID" HeaderText="ทะเบียน" SortExpression="CarID" />
                     

                     <asp:BoundField DataField="CarBrand" HeaderText="ยี่ห้อ-รุ่น" ReadOnly="True" 
                         SortExpression="CarBrand" />
                     

                     <asp:BoundField DataField="CarBuyDate" DataFormatString="{0:dd/MM/yyyy}" 
                         HeaderText="วันหมดประกัน" SortExpression="CarBuyDate" />
                     

                     <asp:BoundField DataField="AppointDate" HeaderText="วันนัด" 
                         SortExpression="AppointDate" />
                     
                      <asp:BoundField DataField="CallDate" HeaderText="โทรล่าสุด" 
                         SortExpression="CallDate" />
                     <asp:BoundField DataField="Comments" HeaderText="หมายเหตุ" 
                         SortExpression="Comments" />
                     

                     <asp:BoundField DataField="AssCreateDate" 
                         HeaderText="AssignDate" SortExpression="AssCreateDate" />
                     

                     <asp:BoundField DataField="Groupname" HeaderText="Groupname" 
                         SortExpression="Groupname" />
                     

                     <asp:BoundField DataField="createdate" DataFormatString="{0:dd/MM/yyyy}" 
                         HeaderText="createdate" SortExpression="createdate" />
                     

                 </Columns>
                 

                 <HeaderStyle BackColor="#99CCFF" />
                 

                 </asp:GridView>
                 

                 </asp:Panel>
             
 
         
</ContentTemplate>
         <HeaderTemplate >
             
 CallBack(<asp:Label ID="lblCallBack" runat="server" Text="0"></asp:Label>)
 
        
</HeaderTemplate>
             
         
</asp:TabPanel>
        
         <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="NoContact">
             
         
<ContentTemplate >
             

         <asp:Panel ID="Panel4" runat="server" Height="400px" ScrollBars="Auto" 
                     Width="100%">
             
<asp:GridView ID="GvNoContact" runat="server" AutoGenerateColumns="False" DataKeyNames="IdCar" 
                         DataSourceID="SqlNoContact" Width="150%">
                 

                 <Columns>
                     

                     <asp:TemplateField>
                         

                         <ItemTemplate>
                             

                             <asp:Button ID="Button3" runat="server" CommandArgument='<%# Eval("IdCar") %>' 
                                 CommandName="Select" Text="แก้ไข" />
                             

                         </ItemTemplate>
                         

                     </asp:TemplateField>
                     

                     <asp:BoundField DataField="CusName" HeaderText="ชื่อ-สกุล" ReadOnly="True" 
                         SortExpression="CusName" />
                     

                     <asp:BoundField DataField="CarID" HeaderText="ทะเบียน" SortExpression="CarID" />
                     

                     <asp:BoundField DataField="CarBrand" HeaderText="ยี่ห้อ-รุ่น" ReadOnly="True" 
                         SortExpression="CarBrand" />
                     

                     <asp:BoundField DataField="CarBuyDate" DataFormatString="{0:dd/MM/yyyy}" 
                         HeaderText="วันหมดประกัน" SortExpression="CarBuyDate" />
                     

                     <asp:BoundField DataField="AppointDate" HeaderText="วันนัด" 
                         SortExpression="AppointDate" />
                      <asp:BoundField DataField="CallDate" HeaderText="โทรล่าสุด" 
                         SortExpression="CallDate" />

                     <asp:BoundField DataField="Comments" HeaderText="หมายเหตุ" 
                         SortExpression="Comments" />
                     

                     <asp:BoundField DataField="AssCreateDate" 
                         HeaderText="AssignDate" SortExpression="AssCreateDate" />
                     

                     <asp:BoundField DataField="Groupname" HeaderText="Groupname" 
                         SortExpression="Groupname" />
                     

                     <asp:BoundField DataField="createdate" DataFormatString="{0:dd/MM/yyyy}" 
                         HeaderText="createdate" SortExpression="createdate" />
                     

                 </Columns>
                 

                 <HeaderStyle BackColor="#99CCFF" />
                 

                 </asp:GridView>
                 

                 </asp:Panel>
             
 
         
</ContentTemplate>
         <HeaderTemplate >
             
 NoContact(<asp:Label ID="lblNoContact" runat="server" Text="0"></asp:Label>)
 
        
</HeaderTemplate>
             
         
</asp:TabPanel>

         <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="Wait">
             
         
<ContentTemplate >
             

         <asp:Panel ID="Panel5" runat="server" Height="400px" ScrollBars="Auto" 
                     Width="100%">
             
<asp:GridView ID="GvWait" runat="server" AutoGenerateColumns="False" DataKeyNames="IdCar" 
                         DataSourceID="SqlWait" Width="150%">
                 

                 <Columns>
                     

                     <asp:TemplateField Visible="False">
                         

                         <ItemTemplate>
                             

                             <asp:Button ID="Button3" runat="server" CommandArgument='<%# Eval("IdCar") %>' 
                                 CommandName="Select" Text="แก้ไข" />
                             

                         </ItemTemplate>
                         

                     </asp:TemplateField>
                     

                     <asp:BoundField DataField="CusName" HeaderText="ชื่อ-สกุล" ReadOnly="True" 
                         SortExpression="CusName" />
                     

                     <asp:BoundField DataField="CarID" HeaderText="ทะเบียน" SortExpression="CarID" />
                     

                     <asp:BoundField DataField="CarBrand" HeaderText="ยี่ห้อ-รุ่น" ReadOnly="True" 
                         SortExpression="CarBrand" />
                     

                     <asp:BoundField DataField="CarBuyDate" DataFormatString="{0:dd/MM/yyyy}" 
                         HeaderText="วันหมดประกัน" SortExpression="CarBuyDate" />
                     

                     <asp:BoundField DataField="AppointDate" HeaderText="วันนัด" 
                         SortExpression="AppointDate" />
                      <asp:BoundField DataField="CallDate" HeaderText="โทรล่าสุด" 
                         SortExpression="CallDate" />

                     <asp:BoundField DataField="Comments" HeaderText="หมายเหตุ" 
                         SortExpression="Comments" />
                     

                     <asp:BoundField DataField="AssCreateDate" 
                         HeaderText="AssignDate" SortExpression="AssCreateDate" />
                     

                     <asp:BoundField DataField="Groupname" HeaderText="Groupname" 
                         SortExpression="Groupname" />
                     

                     <asp:BoundField DataField="createdate" DataFormatString="{0:dd/MM/yyyy}" 
                         HeaderText="createdate" SortExpression="createdate" />
                     

                 </Columns>
                 

                 <HeaderStyle BackColor="#99CCFF" />
                 

                 </asp:GridView>
                 

                 </asp:Panel>
             
 
         
</ContentTemplate>
         <HeaderTemplate >
             
 Wait(<asp:Label ID="lblWait" runat="server" Text="0"></asp:Label>)
 
        
</HeaderTemplate>
             
         
</asp:TabPanel>

         <asp:TabPanel ID="TabPanel6" runat="server" HeaderText="Refuse">
             
         
<ContentTemplate >
             

         <asp:Panel ID="Panel6" runat="server" Height="400px" ScrollBars="Auto" 
                     Width="100%">
             
<asp:GridView ID="GvRefuse" runat="server" AutoGenerateColumns="False" DataKeyNames="IdCar" 
                         DataSourceID="SqlRefuse" Width="150%" AllowPaging="True" 
                 PageSize="100">
                 

                 <Columns>
                     
                     
 
                     

                     
                     <asp:TemplateField>
                         

                         <ItemTemplate>
                             

                             <asp:Button ID="Button5" runat="server" CommandArgument='<%# Eval("IdCar") %>' 
                                 CommandName="Select" Text="แก้ไข" />
                             

                         </ItemTemplate>
                         

                     </asp:TemplateField>
                     
                     
 
                     

                     
                     <asp:BoundField DataField="CusName" HeaderText="ชื่อ-สกุล" ReadOnly="True" 
                         SortExpression="CusName" />
                     

                     <asp:BoundField DataField="CarID" HeaderText="ทะเบียน" SortExpression="CarID" />
                     

                     <asp:BoundField DataField="CarBrand" HeaderText="ยี่ห้อ-รุ่น" ReadOnly="True" 
                         SortExpression="CarBrand" />
                     

                     <asp:BoundField DataField="CarBuyDate" DataFormatString="{0:dd/MM/yyyy}" 
                         HeaderText="วันหมดประกัน" SortExpression="CarBuyDate" />
                     

                     <asp:BoundField DataField="AppointDate" HeaderText="วันนัด" 
                         SortExpression="AppointDate" />
                      <asp:BoundField DataField="CallDate" HeaderText="โทรล่าสุด" 
                         SortExpression="CallDate" />

                     <asp:BoundField DataField="Comments" HeaderText="หมายเหตุ" 
                         SortExpression="Comments" />
                     

                     <asp:BoundField DataField="AssCreateDate" 
                         HeaderText="AssignDate" SortExpression="AssCreateDate" />
                     

                     <asp:BoundField DataField="Groupname" HeaderText="Groupname" 
                         SortExpression="Groupname" />
                     

                     <asp:BoundField DataField="createdate" DataFormatString="{0:dd/MM/yyyy}" 
                         HeaderText="createdate" SortExpression="createdate" />
                     

                 </Columns>
                 

                 <HeaderStyle BackColor="#99CCFF" />
                 

                 </asp:GridView>
                 

                 </asp:Panel>
             
 
         
</ContentTemplate>
         <HeaderTemplate >
             
 Refuse(<asp:Label ID="lblRefuse" runat="server" Text="0"></asp:Label>)
 
        
</HeaderTemplate>
             
         
</asp:TabPanel>

         <asp:TabPanel ID="TabPanel7" runat="server" HeaderText="NotUpdate">
             
         
<ContentTemplate >
             

         <asp:Panel ID="Panel7" runat="server" Height="400px" ScrollBars="Auto" 
                     Width="100%">
             
<asp:GridView ID="GvNotUpdate" runat="server" AutoGenerateColumns="False" DataKeyNames="IdCar" 
                         DataSourceID="SqlNotUpdate" Width="150%" AllowPaging="True" 
                 PageSize="100">
                 

                 <Columns>
                     
                     
 
                     

                     
                     <asp:TemplateField>
                         

                         <ItemTemplate>
                             

                             <asp:Button ID="Button7" runat="server" CommandArgument='<%# Eval("IdCar") %>' 
                                 CommandName="Select" Text="แก้ไข" />
                             

                         </ItemTemplate>
                         

                     </asp:TemplateField>
                     
                     
 
                     

                     
                     <asp:BoundField DataField="CusName" HeaderText="ชื่อ-สกุล" ReadOnly="True" 
                         SortExpression="CusName" />
                     

                     <asp:BoundField DataField="CarID" HeaderText="ทะเบียน" SortExpression="CarID" />
                     

                     <asp:BoundField DataField="CarBrand" HeaderText="ยี่ห้อ-รุ่น" ReadOnly="True" 
                         SortExpression="CarBrand" />
                     

                     <asp:BoundField DataField="CarBuyDate" DataFormatString="{0:dd/MM/yyyy}" 
                         HeaderText="วันหมดประกัน" SortExpression="CarBuyDate" />
                     

                     <asp:BoundField DataField="AppointDate" HeaderText="วันนัด" 
                         SortExpression="AppointDate" />
                      <asp:BoundField DataField="CallDate" HeaderText="โทรล่าสุด" 
                         SortExpression="CallDate" />

                     <asp:BoundField DataField="Comments" HeaderText="หมายเหตุ" 
                         SortExpression="Comments" />
                     

                     <asp:BoundField DataField="AssCreateDate" 
                         HeaderText="AssignDate" SortExpression="AssCreateDate" />
                     

                     <asp:BoundField DataField="Groupname" HeaderText="Groupname" 
                         SortExpression="Groupname" />
                     

                     <asp:BoundField DataField="createdate" DataFormatString="{0:dd/MM/yyyy}" 
                         HeaderText="createdate" SortExpression="createdate" />
                     

                 </Columns>
                 

                 <EmptyDataTemplate>
                     

                     <asp:Button ID="Button6" runat="server" CommandArgument='<%# Eval("IdCar") %>' 
                         CommandName="Select" Text="แก้ไข" />
                     

                 </EmptyDataTemplate>
                 

                 <HeaderStyle BackColor="#99CCFF" />
                 

                 </asp:GridView>
                 

                 </asp:Panel>
             
 
         
</ContentTemplate>
         <HeaderTemplate >
             
 NotUpdate(<asp:Label ID="lblNotUpdate" runat="server" Text="0"></asp:Label>)
 
        
</HeaderTemplate>
             
         
</asp:TabPanel>
        
         <asp:TabPanel ID="TabPanel8" runat="server" HeaderText="List Total">
         <ContentTemplate >
             

         <asp:Panel ID="Panel8" runat="server" Height="400px" ScrollBars="Auto" 
                     Width="100%">
             
<asp:GridView ID="GvListTotal" runat="server" AutoGenerateColumns="False" 
                         DataSourceID="SqlListTotal" Width="100%">
                 

                 <Columns>
                     

                     <asp:BoundField DataField="TsrName" HeaderText="TsrName" 
                         SortExpression="TsrName" />
                     

                     <asp:BoundField DataField="LevelNameEng" HeaderText="LevelNameEng" 
                         SortExpression="LevelNameEng" />
                     

                     <asp:BoundField DataField="NewCase" HeaderText="NewCase" 
                         SortExpression="NewCase" />
                     

                     <asp:BoundField DataField="FollowCase" HeaderText="FollowCase" 
                         SortExpression="FollowCase" />
                     

                     <asp:BoundField DataField="NoContact" HeaderText="NoContact" 
                         SortExpression="NoContact" />
                     

                     <asp:BoundField DataField="CallBack" HeaderText="CallBack" 
                         SortExpression="CallBack" />
                     

                     <asp:BoundField DataField="Wait" HeaderText="Wait" SortExpression="Wait" />
                     

                     <asp:BoundField DataField="NotUpdate" HeaderText="NotUpdate" 
                         SortExpression="NotUpdate" />
                     

                     <asp:BoundField DataField="Refuse" HeaderText="Refuse" 
                         SortExpression="Refuse" />
                     

                 </Columns>
                 

                 <HeaderStyle BackColor="#99CCFF" />
                 

                 </asp:GridView>
                 

                 </asp:Panel>
             
 
         
</ContentTemplate>
      
             
         
</asp:TabPanel>
        
         <asp:TabPanel ID="TabPanel9" runat="server" HeaderText="วันคุ้มครอง">
         <ContentTemplate >
         <ContentTemplate ><iframe id ="Iframe2"  src = "<%= linkCarBuy %>" frameborder="0" height="500" scrolling="auto" 
        width="100%"></iframe></ContentTemplate>
         
</ContentTemplate>
         
</asp:TabPanel>
        
    </asp:TabContainer>
    </div>
     <div style="margin-top: 9px; color: #0066FF; font-weight: bold; font-size: 20px;">

     เปลี่ยนวันนัด</div>
     <div style="text-align: center; background-color: #99CCFF; font-weight: bold; font-size: 13px;">
         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
         <ContentTemplate >
         
         </ContentTemplate>
         </asp:UpdatePanel>
         
         <asp:FormView ID="frmCustomer" runat="server" DataKeyNames="IdCar,CurStatus" 
             DataSourceID="SqlCustomer">
             <ItemTemplate>
                 ชื่อ-นามสกุล :
                 <asp:Label ID="Label2" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                 <asp:DropDownList ID="ddStatus" runat="server" CssClass="jamp" 
                     SelectedValue='<%# Bind("StatusID") %>' DataSourceID="SqlStatus" 
                     DataTextField="StatusName" DataValueField="StatusID">
                 </asp:DropDownList>
                 เปลี่ยนวันนัด :
                 <asp:TextBox ID="txtAppoint" runat="server" Width="80px" 
                     Text='<%# Bind("AppointDate","{0:dd/MM/yyyy}") %>'></asp:TextBox>
                 <asp:MaskedEditExtender ID="txtAppoint_MaskedEditExtender" runat="server" 
                     CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                     CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                     CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                     Mask="99/99/9999" MaskType="Date" TargetControlID="txtAppoint">
                 </asp:MaskedEditExtender>
                 <asp:CalendarExtender ID="txtAppoint_CalendarExtender" runat="server" 
                     Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtAppoint">
                 </asp:CalendarExtender>
                 :<igtxt:WebNumericEdit ID="txtHour" runat="server" DataMode="Int" MaxLength="2" 
                     Width="30px" ValueText='<%# Eval("AppointDate", "{0:HH}") %>'>
                 </igtxt:WebNumericEdit>
                 :<igtxt:WebNumericEdit ID="txtMin" runat="server" DataMode="Int" Width="40px" 
                     ValueText='<%# Eval("AppointDate", "{0:mm}") %>' MaxLength="2">
                 </igtxt:WebNumericEdit>
                
                 <asp:Button ID="Button2" runat="server" Text="บันทึก" onclick="Button2_Click" />
                
             </ItemTemplate>
         </asp:FormView>
     </div>
     <div>
     <iframe src = "../../Sale/Phone/frmHistory.aspx?IdCar=<%= IdCar %>" 
                    frameborder="0" height="300" width="100%" ></iframe>
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
                            Where a1.SupID = @userID and a1.UserStatus = 1
                              order by a1.FName,a1.LName
                            else if @UserLevel = 2
                             SELECT *
                            ,a1.FName + ' ' + a1.LName + '(' + a1.NName + ')' as TsrName
                            ,case a1.UserStatus when '0' then 'ยกเลิก' else 'ใช้งาน' end as UserStatus1
                            ,case a1.UserStatus when '0' then 'color:Red' else 'color:Green' end as UserStatusColor
                            FROM [TblUser] a1
                            Where a1.LeaderID = @userID and a1.UserStatus = 1
                              order by a1.FName,a1.LName
                            else
                             SELECT *
                            ,a1.FName + ' ' + a1.LName + '(' + a1.NName + ')' as TsrName
                            ,case a1.UserStatus when '0' then 'ยกเลิก' else 'ใช้งาน' end as UserStatus1
                            ,case a1.UserStatus when '0' then 'color:Red' else 'color:Green' end as UserStatusColor
                            FROM [TblUser] a1
                            Where a1.UserLevelID in(5) and a1.UserStatus = 1
                             order by a1.FName,a1.LName
                           
            ">
            <SelectParameters>
                <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
                <asp:CookieParameter  Name="userID" CookieName ="userID" />
            </SelectParameters>
        </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlCustomer" runat="server" 
            ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            
            SelectCommand="
                           Select a2.FNameTH + ' ' + a2.LNameTH as CustomerName
                           ,a1.AppointDate
                           ,a1.IdCar
                           ,case when a3.StatusID is null then 6 else a3.StatusID end as StatusID
                           ,a1.CurStatus
                          
                            from tblCar a1
                           Inner join tblCustomer a2 on a1.cusid = a2.cusid 
                           Left Join TblStatus a3 on a1.CurStatus = a3.StatusID and StatusID in(4,7,8,6)
                         
                           where idcar = @IdCar
                           
            " 
        
        UpdateCommand="UPDATE TblCar SET AppointDate = @AppointDate,CurStatus=@CurStatus WHERE (IdCar = @IdCar)" 
        InsertCommand="INSERT INTO TblReStatus(Status_Old, Status_New, CreateID, HostAccess, CarID) VALUES (@StatusOld, @StatusNew, @userID, @IpLog, @IdCar)">
            <InsertParameters>
                <asp:Parameter Name="StatusOld" />
                <asp:Parameter Name="StatusNew" />
                <asp:CookieParameter CookieName ="userID" Name="userID" />
                <asp:Parameter Name="IpLog" />
                <asp:Parameter Name="IdCar" />
            </InsertParameters>
            <SelectParameters>
                <asp:Parameter Name="IdCar" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="AppointDate" />
                <asp:Parameter Name="CurStatus" />
                <asp:Parameter Name="IdCar" />
            </UpdateParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlNewCase" runat="server"   ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand=" Select 
                            Convert(VarChar,a2.FnameTH) + ' ' + Convert(VarChar,a2.LNameTH) as CusName
                            ,a2.Tel + ' ' + a2.TelExt as Tel
                            ,a2.OTel + ' ' + a2.OtelExt as Otel
                            ,a2.Mobile
                            ,a1.AssCreateDate
                            ,a1.CarBuyDate
                            ,a1.CarID
                            ,a4.Groupname
                            ,a1.CarBrand + ' ' +a1.CarSeries as CarBrand
                            ,a1.IdCar
                            ,a1.createdate
                            ,a6.createdate as CallDate
                            From TblCar a1
                            Inner Join TblCustomer a2 on a1.CusID = a2.CusID
                            Inner Join TblUser a3 on a1.AssignTo = a3.userID
                             Inner Join TblSourceGroup a4 on a1.GroupID = a4.GroupID
                            Left Join GetTblCallMax() a5 on a1.IdCar = a5.CarID
                           Left Join TblCall a6 on a5.CallID = a6.CallID
                            Where a1.CurStatus in (1) 
                            and  case @TypeSearch when 0 then a1.AssignTO else 0 end= case @TypeSearch when 0 then @AssignTo else 0  end
                            and  case @TypeSearch when 0 then '0' else a2.FNameTH  + ' ' + a2.LNameTH + ' ' + a1.CarID end like  case @TypeSearch when 0 then '0' else @Search end
                            --and a2.FNameTH  + ' ' + a2.LNameTH like @Search
                             --and a3.TypeTsr = @TypeTsr
                           order by a2.FNameTH,a2.LNameTH 
                            ">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="TypeSearch" />
                <asp:ControlParameter ControlID="ddTsr1" Name="AssignTo" 
                    PropertyName="SelectedValue" />
                <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
                <asp:Parameter DefaultValue="0" Name="Search" Type = "String"  />
                <asp:CookieParameter CookieName="TypeTsr" DefaultValue="" Name="TypeTsr" />
            </SelectParameters>
            
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlFollow" runat="server"   ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand=" Select 
                            a2.FnameTH  + ' ' + a2.LNameTH as CusName
                            ,a2.Tel + ' ' + a2.TelExt as Tel
                            ,a2.OTel + ' ' + a2.OtelExt as Otel
                            ,a2.Mobile
                            ,a1.AssCreateDate
                            ,a1.CarBuyDate
                            ,a1.CarID
                            ,a4.Groupname
                            ,a1.CarBrand + ' ' +a1.CarSeries as CarBrand
                            ,a1.IdCar
                            ,a1.AppointDate
                            ,a1.Comments
                            ,a1.createdate
                            ,a6.createdate as CallDate
                            From TblCar a1
                            Inner Join TblCustomer a2 on a1.CusID = a2.CusID
                            Inner Join TblUser a3 on a1.AssignTo = a3.userID
                             Inner Join TblSourceGroup a4 on a1.GroupID = a4.GroupID
                            Left Join GetTblCallMax() a5 on a1.IdCar = a5.CarID
                           Left Join TblCall a6 on a5.CallID = a6.CallID
                            Where a1.CurStatus in (6) 
                            and  case @TypeSearch when 0 then a1.AssignTO else 0 end= case @TypeSearch when 0 then @AssignTo else 0  end
                            and  case @TypeSearch when 0 then '0' else a2.FNameTH  + ' ' + a2.LNameTH + a1.CarID end like  case @TypeSearch when 0 then '0' else @Search end
                            --and a3.TypeTsr = @TypeTsr
                           order by a1.AppointDate,a2.FNameTH,a2.LNameTH
                            ">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="TypeSearch" />
                <asp:ControlParameter ControlID="ddTsr1" Name="AssignTo" 
                    PropertyName="SelectedValue" />
                <asp:Parameter DefaultValue="0" Name="Search" />
                <asp:CookieParameter CookieName="TypeTsr" DefaultValue="" Name="TypeTsr" />
            </SelectParameters>
            
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlCallBack" runat="server"   ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand=" Select 
                            a2.FnameTH  + ' ' + a2.LNameTH as CusName
                            ,a2.Tel + ' ' + a2.TelExt as Tel
                            ,a2.OTel + ' ' + a2.OtelExt as Otel
                            ,a2.Mobile
                            ,a1.AssCreateDate
                            ,a1.CarBuyDate
                            ,a1.CarID
                            ,a4.Groupname
                            ,a1.CarBrand + ' ' +a1.CarSeries as CarBrand
                            ,a1.IdCar
                            ,a1.AppointDate
                            ,a1.Comments
                            ,a1.CreateDate
                            ,a6.createdate as CallDate
                            From TblCar a1
                            Inner Join TblCustomer a2 on a1.CusID = a2.CusID
                            Inner Join TblUser a3 on a1.AssignTo = a3.userID
                             Inner Join TblSourceGroup a4 on a1.GroupID = a4.GroupID
                            Left Join GetTblCallMax() a5 on a1.IdCar = a5.CarID
                           Left Join TblCall a6 on a5.CallID = a6.CallID
                            Where a1.CurStatus in (8)
                             and  case @TypeSearch when 0 then a1.AssignTO else 0 end= case @TypeSearch when 0 then @AssignTo else 0  end
                            and  case @TypeSearch when 0 then '0' else a2.FNameTH  + ' ' + a2.LNameTH + a1.CarID  end like  case @TypeSearch when 0 then '0' else @Search end
                            --and a3.TypeTsr = @TypeTsr
                           order by a1.AppointDate,a2.FNameTH,a2.LNameTH
                            ">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="TypeSearch" />
                <asp:ControlParameter ControlID="ddTsr1" Name="AssignTo" 
                    PropertyName="SelectedValue" />
                <asp:Parameter DefaultValue="0" Name="Search" />
                <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
                <asp:CookieParameter CookieName="TypeTsr" DefaultValue="" Name="TypeTsr" />
            </SelectParameters>
            
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlNoContact" runat="server"   ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand=" Select 
                            a2.FnameTH  + ' ' + a2.LNameTH as CusName
                            ,a2.Tel + ' ' + a2.TelExt as Tel
                            ,a2.OTel + ' ' + a2.OtelExt as Otel
                            ,a2.Mobile
                            ,a1.AssCreateDate
                            ,a1.CarBuyDate
                            ,a1.CarID
                            ,a4.Groupname
                            ,a1.CarBrand + ' ' +a1.CarSeries as CarBrand
                            ,a1.IdCar
                            ,a1.AppointDate
                            ,a1.Comments
                            ,a1.createdate
                           ,a6.createdate as CallDate
                            From TblCar a1
                            Inner Join TblCustomer a2 on a1.CusID = a2.CusID
                            Inner Join TblUser a3 on a1.AssignTo = a3.userID
                             Inner Join TblSourceGroup a4 on a1.GroupID = a4.GroupID
                            Left Join GetTblCallMax() a5 on a1.IdCar = a5.CarID
                           Left Join TblCall a6 on a5.CallID = a6.CallID
                            Where a1.CurStatus in (7) 
                             and  case @TypeSearch when 0 then a1.AssignTO else 0 end= case @TypeSearch when 0 then @AssignTo else 0  end
                            and  case @TypeSearch when 0 then '0' else a2.FNameTH  + ' ' + a2.LNameTH + a1.CarID end like  case @TypeSearch when 0 then '0' else @Search end
                           order by a1.AppointDate,a2.FNameTH,a2.LNameTH
                            ">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="TypeSearch" />
                <asp:ControlParameter ControlID="ddTsr1" Name="AssignTo" 
                    PropertyName="SelectedValue" />
                <asp:Parameter DefaultValue="0" Name="Search" />
                <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
                <asp:CookieParameter CookieName="TypeTsr" DefaultValue="" Name="TypeTsr" />
            </SelectParameters>
            
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlWait" runat="server"   ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand=" Select 
                            a2.FnameTH  + ' ' + a2.LNameTH as CusName
                            ,a2.Tel + ' ' + a2.TelExt as Tel
                            ,a2.OTel + ' ' + a2.OtelExt as Otel
                            ,a2.Mobile
                            ,a1.AssCreateDate
                            ,a1.CarBuyDate
                            ,a1.CarID
                            ,a4.Groupname
                            ,a1.CarBrand + ' ' +a1.CarSeries as CarBrand
                            ,a1.IdCar
                            ,a1.AppointDate
                            ,a1.Comments
                            ,a1.createdate
                            ,a6.createdate as CallDate
                            From TblCar a1
                            Inner Join TblCustomer a2 on a1.CusID = a2.CusID
                            Inner Join TblUser a3 on a1.AssignTo = a3.userID
                             Inner Join TblSourceGroup a4 on a1.GroupID = a4.GroupID
                            Left Join GetTblCallMax() a5 on a1.IdCar = a5.CarID
                           Left Join TblCall a6 on a5.CallID = a6.CallID
                            Where a1.CurStatus in (4) 
                             and  case @TypeSearch when 0 then a1.AssignTO else 0 end= case @TypeSearch when 0 then @AssignTo else 0  end
                            and  case @TypeSearch when 0 then '0' else a2.FNameTH  + ' ' + a2.LNameTH + a1.CarID end like  case @TypeSearch when 0 then '0' else @Search end
                            --and a3.TypeTsr = @TypeTsr
                           order by a1.AppointDate,a2.FNameTH,a2.LNameTH

                            ">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="TypeSearch" />
                <asp:ControlParameter ControlID="ddTsr1" Name="AssignTo" 
                    PropertyName="SelectedValue" />
                <asp:Parameter DefaultValue="0" Name="Search" />
                <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
                <asp:CookieParameter CookieName="TypeTsr" DefaultValue="" Name="TypeTsr" />
            </SelectParameters>
            
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlRefuse" runat="server"   ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand=" Select 
                            a2.FnameTH  + ' ' + a2.LNameTH as CusName
                            ,a2.Tel + ' ' + a2.TelExt as Tel
                            ,a2.OTel + ' ' + a2.OtelExt as Otel
                            ,a2.Mobile
                            ,a1.AssCreateDate
                            ,a1.CarBuyDate
                            ,a1.CarID
                            ,a4.Groupname
                            ,a1.CarBrand + ' ' +a1.CarSeries as CarBrand
                            ,a1.IdCar
                            ,a1.AppointDate
                            ,a1.Comments
                            ,a1.createdate
                           ,a6.createdate as CallDate
                            From TblCar a1
                            Inner Join TblCustomer a2 on a1.CusID = a2.CusID
                            Inner Join TblUser a3 on a1.AssignTo = a3.userID
                             Inner Join TblSourceGroup a4 on a1.GroupID = a4.GroupID
                            Left Join GetTblCallMax() a5 on a1.IdCar = a5.CarID
                           Left Join TblCall a6 on a5.CallID = a6.CallID
                            Where a1.CurStatus in (5) 
                             and  case @TypeSearch when 0 then a1.AssignTO else 0 end= case @TypeSearch when 0 then @AssignTo else 0  end
                            and  case @TypeSearch when 0 then '0' else a2.FNameTH  + ' ' + a2.LNameTH + a1.CarID end like  case @TypeSearch when 0 then '0' else @Search end
                            --and a3.TypeTsr = @TypeTsr
                           order by a1.AppointDate,a2.FNameTH,a2.LNameTH
                            ">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="TypeSearch" />
                <asp:ControlParameter ControlID="ddTsr1" Name="AssignTo" 
                    PropertyName="SelectedValue" />
                <asp:Parameter DefaultValue="0" Name="Search" />
                <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
                <asp:CookieParameter CookieName="TypeTsr" DefaultValue="" Name="TypeTsr" />
            </SelectParameters>
            
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlNotUpdate" runat="server"   ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand=" Select 
                            a2.FnameTH  + ' ' + a2.LNameTH as CusName
                            ,a2.Tel + ' ' + a2.TelExt as Tel
                            ,a2.OTel + ' ' + a2.OtelExt as Otel
                            ,a2.Mobile
                            ,a1.AssCreateDate
                            ,a1.CarBuyDate
                            ,a1.CarID
                            ,a4.Groupname
                            ,a1.CarBrand + ' ' +a1.CarSeries as CarBrand
                            ,a1.IdCar
                            ,a1.AppointDate
                            ,a1.Comments
                            ,a1.createdate
                           ,a6.createdate as CallDate
                            From TblCar a1
                            Inner Join TblCustomer a2 on a1.CusID = a2.CusID
                            Inner Join TblUser a3 on a1.AssignTo = a3.userID
                             Inner Join TblSourceGroup a4 on a1.GroupID = a4.GroupID
                            Left Join GetTblCallMax() a5 on a1.IdCar = a5.CarID
                           Left Join TblCall a6 on a5.CallID = a6.CallID
                            Where a1.CurStatus in (9) 
                             and  case @TypeSearch when 0 then a1.AssignTO else 0 end= case @TypeSearch when 0 then @AssignTo else 0  end
                            and  case @TypeSearch when 0 then '0' else a2.FNameTH  + ' ' + a2.LNameTH end like  case @TypeSearch when 0 then '0' else @Search end
                            --and a3.TypeTsr = @TypeTsr
                           order by a1.AppointDate,a2.FNameTH,a2.LNameTH
                            ">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="TypeSearch" />
                <asp:ControlParameter ControlID="ddTsr1" Name="AssignTo" 
                    PropertyName="SelectedValue" />
                <asp:Parameter DefaultValue="0" Name="Search" />
                <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
                <asp:CookieParameter CookieName="TypeTsr" DefaultValue="" Name="TypeTsr" />
            </SelectParameters>
            
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlListTotal" runat="server"   ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand=" select  
                            a2.FName + ' ' + a2.LName as TsrName
                            , count (case a1.CurStatus when 1 then 1 end )  as NewCase
                            , count (case a1.CurStatus when 6 then 1 end)   as FollowCase
                            , count (case a1.CurStatus when 7 then 1 end)   as NoContact
                            , count (case a1.CurStatus when 8 then 1 end)   as CallBack
                            , count (case a1.CurStatus when 9 then 1 end)   as NotUpdate
                            , count (case a1.CurStatus when 4 then 1 end)   as Wait
                            , count (case a1.CurStatus when 5 then 1 end)   as Refuse
                            ,a3.LevelNameEng
                            ,a3.LevelID
                            from TblCar a1
                            Inner Join TblUser a2 on a1.AssignTo = a2.UserID 
                            Inner Join TblUserLevel a3 on a2.UserLevelID = a3.LevelID
                            where  UserStatus = 1 and a1.CurStatus in(1,6,7,8,9,4,5) 
                            and case @UserLevel when 3 then a2.SupID when 2 then a2.LeaderID else @userID end = @userID 
                            and case @UserLevel when 1 then  a2.TypeTsr else @TypeTsr end = @TypeTsr
                            group by a3.LevelNameEng ,a3.LevelID,a2.FName ,a2.LName  
                            order by a3.LevelID, a2.FName,a2.LName
                            
                            ">
            <SelectParameters>
                <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
                <asp:CookieParameter CookieName="userID" Name="userID" />
                <asp:CookieParameter CookieName="TypeTsr" Name="TypeTsr" />
            </SelectParameters>
            
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlStatus" runat="server"   ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand=" select  * from TblStatus
                            Where StatusID in(4,7,8,6)
                            ">
         
            
        </asp:SqlDataSource>

</asp:Content>

