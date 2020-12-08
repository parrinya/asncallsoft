<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmBlackList.aspx.vb" Inherits="Modules_Manager_Manage_Case_frmBlackList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: center; font-weight: 700; margin-top: 25px">
    ชื่อ-นามสกุล<asp:TextBox ID="txtFNameTH" runat="server"></asp:TextBox>
    -<asp:TextBox ID="txtLNameTH" runat="server"></asp:TextBox>
&nbsp;ทะเบียน<asp:TextBox ID="txtCarID" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="เพิ่ม" />
</div>
<div>
    <asp:Panel ID="Panel1" runat="server" Height="400px" ScrollBars="Auto" 
        Width="100%">
<asp:GridView ID="GvBlackList" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlBlackList" Width="100%" DataKeyNames="recNO">
        <AlternatingRowStyle BackColor="#DBE7F2" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="Button2" runat="server" CommandName="Delete" Text="ลบ" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FNameTH" HeaderText="ชื่อ" 
                SortExpression="FNameTH" />
            <asp:BoundField DataField="LNameTH" HeaderText="นามสกุล" 
                SortExpression="LNameTH" />
            <asp:BoundField DataField="CarID" HeaderText="ทะเบียน" SortExpression="CarID" />
            <asp:BoundField DataField="CreateDate" HeaderText="วันที่ลงข้อมูล" 
                SortExpression="CreateDate" />
        </Columns>
        <HeaderStyle BackColor="#336699" ForeColor="White" Height="30px" />
    </asp:GridView>
    </asp:Panel>
    
</div>
    <asp:SqlDataSource ID="SqlBlackList" runat="server" 
    ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
    SelectCommand="SELECT * FROM [TblBlackList] order by FNameTH,LNameTH" 
        DeleteCommand="DELETE FROM TblBlackList WHERE (recNO = @recNO)" 
        InsertCommand="INSERT INTO TblBlackList(FNameTH, LNameTH, CarID, CreateID) VALUES (@FNameTH, @LNameTH, @CarID, @CreateID)">
        <DeleteParameters>
            <asp:Parameter Name="recNO" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="FNameTH" />
            <asp:Parameter Name="LNameTH" />
            <asp:Parameter Name="CarID" />
            <asp:CookieParameter CookieName = "userID" Name="CreateID" />
        </InsertParameters>
    </asp:SqlDataSource>
</asp:Content>

