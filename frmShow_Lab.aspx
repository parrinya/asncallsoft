<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmShow_Lab.aspx.vb" Inherits="Modules_Manager_Import_frmShow_Lab" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
    <table width="100%">
        <tr align="center">
        <td align="right" >เลือกเดือน : 
            <asp:dropdownlist runat="server" id="ddmonth">
                <asp:ListItem Value=1>มกราคม</asp:ListItem>
                <asp:ListItem Value=2>กุมภาพันธ์</asp:ListItem>
                <asp:ListItem Value=3>มีนาคม</asp:ListItem>
                <asp:ListItem Value=4>เมษายน</asp:ListItem>
                <asp:ListItem Value=5>พฤษภาคม</asp:ListItem>
                <asp:ListItem Value=6>มิถุนายน</asp:ListItem>
                <asp:ListItem Value=7>กรกฎาคม</asp:ListItem>
                <asp:ListItem Value=8>สิงหาคม</asp:ListItem>
                <asp:ListItem Value=9>กันยายน</asp:ListItem>
                <asp:ListItem Value=10>ตุลาคม</asp:ListItem>
                <asp:ListItem Value=11>พฤศจิกายน</asp:ListItem>
                <asp:ListItem Value=12>ธันวาคม</asp:ListItem>
            </asp:dropdownlist>            
        </td>
        <td align=left>เลือกปี : 
        <asp:dropdownlist runat="server" id="ddyear">                
            </asp:dropdownlist>  
            <asp:button runat="server" text="ค้นหา" id="btnshow"/>          
        </td>
        </tr>
        </table>
</div>
<div>
       <table style="width: 100%;">
        <tr>
         <td colspan=2 align="center"  > 
             <asp:gridview runat="server" id="gv_lab" AutoGenerateColumns="False" 
                 DataSourceID="SqlshowL" CellPadding="4" ForeColor="#333333"  Width="100%"
                 GridLines="None">
                 <AlternatingRowStyle BackColor="White" />
                 <Columns>
                     <asp:BoundField DataField="nameuser" HeaderText="ชื่อ Tsr"></asp:BoundField>
                     <asp:BoundField DataField="namesup" HeaderText="ชื่อ Sup"></asp:BoundField>
                     <asp:BoundField DataField="RewardDate" HeaderText="Reward Date">
                     </asp:BoundField>
                     <asp:BoundField DataField="Reward" HeaderText="Reward" DataFormatString="{0:N}"></asp:BoundField>
                     <asp:BoundField DataField="type" HeaderText="ประเภท"></asp:BoundField>
                 </Columns>
                 <EditRowStyle BackColor="#7C6F57" />
                 <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                 <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                 <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                 <RowStyle BackColor="#E3EAEB" />
                 <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                 <sortedascendingcellstyle backcolor="#F8FAFA" />
                 <sortedascendingheaderstyle backcolor="#246B61" />
                 <sorteddescendingcellstyle backcolor="#D4DFE1" />
                 <sorteddescendingheaderstyle backcolor="#15524A" />

<SortedAscendingCellStyle BackColor="#F8FAFA"></SortedAscendingCellStyle>

<SortedAscendingHeaderStyle BackColor="#246B61"></SortedAscendingHeaderStyle>

<SortedDescendingCellStyle BackColor="#D4DFE1"></SortedDescendingCellStyle>

<SortedDescendingHeaderStyle BackColor="#15524A"></SortedDescendingHeaderStyle>
             </asp:gridview>
         </td>  
        </tr>                               
</table>
 </div>
 <div>
       <table style="width: 100%;">
        <tr>
         <td colspan=2 align="center"  > 
             <asp:gridview runat="server" id="gv_labM" AutoGenerateColumns="False"  Width="100%"
                 DataSourceID="SqlshowM" CellPadding="4" ForeColor="#333333" 
                 GridLines="None">
                  <AlternatingRowStyle BackColor="White" />
                  <Columns>
                     <asp:BoundField DataField="nameuser" HeaderText="ชื่อ Tsr"></asp:BoundField>
                     <asp:BoundField DataField="namesup" HeaderText="ชื่อ Sup"></asp:BoundField>
                      <asp:BoundField DataField="namelead" HeaderText="ชื่อ Lead"></asp:BoundField>
                     <asp:BoundField DataField="RewardDate" HeaderText="Reward Date">
                     </asp:BoundField>
                     <asp:BoundField DataField="Reward" HeaderText="Reward" DataFormatString="{0:N}"></asp:BoundField>
                     <asp:BoundField DataField="type" HeaderText="ประเภท"></asp:BoundField>
                 </Columns>
                  <EditRowStyle BackColor="#7C6F57" />
                  <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                  <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                  <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                  <RowStyle BackColor="#E3EAEB" />
                  <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                  <sortedascendingcellstyle backcolor="#F8FAFA" />
                  <sortedascendingheaderstyle backcolor="#246B61" />
                  <sorteddescendingcellstyle backcolor="#D4DFE1" />
                  <sorteddescendingheaderstyle backcolor="#15524A" />

<SortedAscendingCellStyle BackColor="#F8FAFA"></SortedAscendingCellStyle>

<SortedAscendingHeaderStyle BackColor="#246B61"></SortedAscendingHeaderStyle>

<SortedDescendingCellStyle BackColor="#D4DFE1"></SortedDescendingCellStyle>

<SortedDescendingHeaderStyle BackColor="#15524A"></SortedDescendingHeaderStyle>
             </asp:gridview>
 </td>  
        </tr>                               
</table>
 </div>

<asp:SqlDataSource ID="SqlshowL" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="select b.FName+' '+b.LName as nameuser ,c.FName+' '+c.LName as namesup,
				a.[RewardDate],a.[Reward],case a.[Typereword] when 1 then 'reward' else case a.[Typereword] when 2 then 'incentive' end end as type
				from TblRewardTsr a
				inner join TblUser b on a.Userid=b.Userid
				inner join TblUser c on b.SupID =c.Userid
				where c.LeaderID =@userID and 
                month(a.[RewardDate])=@month and year(a.[RewardDate])=@year
                order by a.[RewardDate] 
                 ">
        <SelectParameters>            
            <asp:CookieParameter CookieName="userID"  Name="userID" />
            <asp:Parameter Name="month" Type="Int32"/>
            <asp:Parameter Name="year" Type="Int32"/>
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlshowM" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="select b.FName+' '+b.LName as nameuser ,c.FName+' '+c.LName as namesup
        ,d.FName+' '+d.LName as namelead,

				a.[RewardDate],a.[Reward],case a.[Typereword] when 1 then 'reward' else case a.[Typereword] when 2 then 'incentive' end end as type
				from TblRewardTsr a
				inner join TblUser b on a.Userid=b.Userid
				inner join TblUser c on b.SupID =c.Userid
                inner join TblUser d on c.LeaderID =d.Userid
				where  month(a.[RewardDate])=@month and year(a.[RewardDate])=@year
                order by a.[RewardDate] 
                ">
        <SelectParameters>            
            
            <asp:Parameter Name="month" Type="Int32" />
            <asp:Parameter Name="year"  Type="Int32"/>
        </SelectParameters>
    </asp:SqlDataSource>
</div>

</asp:Content>

