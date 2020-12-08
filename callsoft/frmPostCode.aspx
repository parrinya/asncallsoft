<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmPostCode.aspx.vb" Inherits="Modules_Manager_Manage_Case_frmPostCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin-top: 25px"><font style="color: #0066FF; font-size: 18px">เพิ่มที่อยู่</font></div>
 <div style="text-align: center; background-color: #DFEFFF;">ค้นหา/จังหวัด :                     <asp:DropDownList ID="ddProvince2" runat="server" CssClass="jamp" 
                        DataSourceID="SqlZipCode" DataTextField="Province" 
                        DataValueField="ProvinceID" AutoPostBack="True">
                    </asp:DropDownList>
                &nbsp;อำเภอ :
     <asp:TextBox ID="txtDist" runat="server"></asp:TextBox>
&nbsp;ตำบล :
     <asp:TextBox ID="txtSubDist" runat="server"></asp:TextBox>
&nbsp;รหัสไปรษณีย์ :
     <asp:TextBox ID="txtZipCode" runat="server"></asp:TextBox>
&nbsp;<asp:Button ID="Button1" runat="server" Text="เพิ่ม" 
         ValidationGroup="btnSave" />
                </div>
                <div>
                <asp:Panel ID="Panel1" runat="server" Height="500px" ScrollBars="Auto"  
        Width="100%">
             <asp:GridView ID="GvProvince" runat="server" BorderColor="Black" 
                 BorderStyle="Solid" BorderWidth="1px" DataSourceID="SqlZipCode2" 
                 AutoGenerateColumns="False" style="margin-left: 0px" 
                     Width="100%" EmptyDataText="ไม่พบข้อมูล">
                 <Columns>
                     <asp:BoundField DataField="Province" HeaderText="จังหวัด" 
                         SortExpression="Province" />
                     <asp:BoundField DataField="Dist" HeaderText="อำเภอ" SortExpression="Dist" />
                     <asp:BoundField DataField="SubDist" HeaderText="ตำบล" 
                         SortExpression="SubDist" />
                     <asp:BoundField DataField="ZipCode" HeaderText="รหัสไปรษณีย์" 
                         SortExpression="ZipCode" />
                 </Columns>
                 <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
                     CssClass="td-headder" BackColor="#CCFFCC" />
             </asp:GridView>
         </asp:Panel>
                </div>

                <asp:SqlDataSource ID="SqlZipCode" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT Distinct [Province],ProvinceID FROM [TblZipcode]  Order by Province"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlZipCode2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT [ZipCode], [Province], [Dist], [SubDist] 
                            FROM [TblZipcode] 
                            Where ProvinceID = @Province 
                            Order by Province,Dist,SubDist" 
        InsertCommand="INSERT INTO TblZipcode(ZipCode, ProvinceID, Province, Dist, SubDist) VALUES (@ZipCode, @ProvinceID, @Province, @Dist, @SubDist)">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddProvince2" Name="Province" 
                PropertyName="SelectedValue" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="ZipCode" />
            <asp:Parameter Name="ProvinceID" />
            <asp:Parameter Name="Province" />
            <asp:Parameter Name="Dist" />
            <asp:Parameter Name="SubDist" />
        </InsertParameters>
    </asp:SqlDataSource> 
</asp:Content>

