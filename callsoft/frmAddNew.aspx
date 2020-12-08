<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmAddNew.aspx.vb" Inherits="Modules_Manager_Manage_Case_frmAddNew" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            font-weight: bold;
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin-top: 25px"><font style="color: #0099FF; font-size: 20px">Add New Case</font></div>

<div style="text-align: center; font-weight: bold; background-color: #99CCFF;">

    check dup : ชื่อ-นามสกุล ลูกค้า :
    <asp:TextBox ID="txtFNameTH" runat="server"></asp:TextBox>
    -<asp:TextBox ID="txtLNameTH" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="Check" />

</div>
<div>
<asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="300px">
    <asp:GridView ID="GvCustomer" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="CusID" DataSourceID="SqlCheckDup" Width="100%">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="Button2" runat="server" CommandArgument='<%# Eval("CusID") %>' 
                        CommandName="Select" Text="เลือก" />
                </ItemTemplate>
                <ItemStyle Width="30px" />
            </asp:TemplateField>
            <asp:BoundField DataField="FNameTH" HeaderText="ชื่อ" 
                SortExpression="FNameTH" />
            <asp:BoundField DataField="LNameTH" HeaderText="นามสกุล" 
                SortExpression="LNameTH" />
            <asp:BoundField DataField="Tel" HeaderText="เบอร์บ้าน" SortExpression="Tel" />
            <asp:BoundField DataField="OTel" HeaderText="ที่ทำงาน" SortExpression="OTel" />
            <asp:BoundField DataField="Mobile" HeaderText="มือถือ" 
                SortExpression="Mobile" />
        </Columns>
        <HeaderStyle BackColor="#99CCFF" Height="30px" />
    </asp:GridView>
    </asp:Panel>
</div>
<div>
<asp:Panel ID="Panel1" runat="server" Visible="False">
    <div><font style="color: #0099FF; font-size: 20px">กรอกรายละเอียด</font></div>    
    <div>
    
        <asp:FormView ID="frmCustomer" runat="server" DataSourceID="SqlCar" 
            DataKeyNames="CusID" Width="100%">
            <ItemTemplate>
                <table>
                    <tr>
                        <td> 
                        <table style="width:100%;">
                    <tr>
                        <td class="style1" bgcolor="#99CCFF">
                            ชื่อ-นามสกุล</td>
                        <td>
                            <asp:TextBox ID="txtFNameTH" runat="server" Width="120px" 
                                Text='<%# Bind("FNameTH") %>'></asp:TextBox>
                            -<asp:TextBox ID="txtLNameTH" runat="server" Width="120px" 
                                Text='<%# Bind("LNameTH") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" bgcolor="#99CCFF">
                            เบอร์บ้าน</td>
                        <td>
                            <asp:TextBox ID="txtTel" runat="server" Width="100px" Text='<%# Bind("Tel") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" bgcolor="#99CCFF">
                            ที่ทำงาน</td>
                        <td>
                            <asp:TextBox ID="txtOTel" runat="server" Width="100px" 
                                Text='<%# Bind("OTel") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" bgcolor="#99CCFF">
                            มือถือ</td>
                        <td>
                            <asp:TextBox ID="txtMobile" runat="server" Width="100px" 
                                Text='<%# Bind("Mobile") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" bgcolor="#99CCFF">
                            เลขทะเบียน</td>
                        <td>
                            <asp:TextBox ID="txtCarID" runat="server" Width="100px"></asp:TextBox>
                            <asp:DropDownList ID="ddCarID" runat="server" CssClass="jamp" 
                                DataSourceID="SqlCarProvince" DataTextField="province1" 
                                DataValueField="province2">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddBrand" runat="server" AutoPostBack="True"  style="width: 80px;" 
                                                                    CssClass="jamp" DataSourceID="SqlCarBrand" DataTextField="carBRAND" 
                                                                    DataValueField="carBRAND" 
                                                                    onselectedindexchanged="ddBrand_SelectedIndexChanged">
                                                                </asp:DropDownList>
                            <asp:DropDownList ID="ddSeries" runat="server"  
                             CssClass="jamp" style="width: 80px;" >
                            </asp:DropDownList>
                        </td>
                    </tr>
                      <tr>
                        <td class="style1" bgcolor="#99CCFF">
                            วันคุ้มครอง</td>
                        <td>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate >
                            <asp:TextBox ID="txtCarbuyDate" runat="server" Width="80px" ></asp:TextBox>
                            <asp:MaskedEditExtender ID="txtCarbuyDate_MaskedEditExtender" runat="server" 
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtCarbuyDate">
                                            </asp:MaskedEditExtender>
                                            
                                            <asp:CalendarExtender ID="txtCarbuyDate_CalendarExtender" runat="server"  
                                                Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtCarbuyDate"  >
                                            </asp:CalendarExtender>
                                            </ContentTemplate></asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" bgcolor="#99CCFF">
                            Assign Tsr</td>
                        <td>
                            <asp:DropDownList ID="ddUser" runat="server" CssClass="jamp" 
                                DataSourceID="SqlUser" DataTextField="FullName" DataValueField="userID">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table></td>
                        <td valign="top">
                            <table>
                                <tr>
                                    <td class="style1" bgcolor="#99CCFF">แหล่งที่มา :</td>
                                    <td>                                       
                                        <asp:DropDownList ID="ddListStation" runat="server" CssClass="jamp" 
                                            DataSourceID="SqlListStation" 
                                            DataTextField="SourceName" 
                                            DataValueField="SourceID"
                                            SelectedValue='<%# Bind("SourceDataID") %>' > 
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                 <tr>
                                    <td class="style1" bgcolor="#99CCFF">ชื่อ-สกุล ลูกค้าที่แนะนำ:</td>
                                    <td>                                      
                                        <asp:TextBox ID="txtFNameTHSource" runat="server"   Text='<%# Bind("Fname") %>'></asp:TextBox>
                                        <asp:TextBox ID="txtLNameTHSource" runat="server"   Text='<%# Bind("Lname") %>'></asp:TextBox>                                </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
               
            </ItemTemplate>
            <EmptyDataTemplate >
            <table>
                <tr>
                    <td>
                     <table style="width:100%;">
                    <tr>
                        <td class="style1" bgcolor="#99CCFF">
                            ชื่อ-นามสกุล</td>
                        <td>
                            <asp:TextBox ID="txtFNameTH" runat="server" Width="120px" 
                                Text=''></asp:TextBox>
                            -<asp:TextBox ID="txtLNameTH" runat="server" Width="120px" 
                                Text=''></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" bgcolor="#99CCFF">
                            เบอร์บ้าน</td>
                        <td>
                            <asp:TextBox ID="txtTel" runat="server" Width="100px" Text=''></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" bgcolor="#99CCFF">
                            ที่ทำงาน</td>
                        <td>
                            <asp:TextBox ID="txtOTel" runat="server" Width="100px" 
                                Text=''></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" bgcolor="#99CCFF">
                            มือถือ</td>
                        <td>
                            <asp:TextBox ID="txtMobile" runat="server" Width="100px" 
                                Text=''></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" bgcolor="#99CCFF">
                            เลขทะเบียน</td>
                        <td>
                            <asp:TextBox ID="txtCarID" runat="server" Width="100px"></asp:TextBox>
                            <asp:DropDownList ID="ddCarID" runat="server" CssClass="jamp" 
                                DataSourceID="SqlCarProvince" DataTextField="province1" 
                                DataValueField="province2">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddBrand" runat="server" AutoPostBack="True"  style="width: 80px;" 
                                                                    CssClass="jamp" DataSourceID="SqlCarBrand" DataTextField="carBRAND" 
                                                                    DataValueField="carBRAND" 
                                                                    onselectedindexchanged="ddBrand_SelectedIndexChanged">
                                                                </asp:DropDownList>
                            <asp:DropDownList ID="ddSeries" runat="server"  
                             CssClass="jamp" style="width: 80px;" >
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" bgcolor="#99CCFF">
                            วันคุ้มครอง</td>
                        <td>
                        
                            <asp:TextBox ID="txtCarbuyDate" runat="server" Width="80px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="txtCarbuyDate_MaskedEditExtender" runat="server" 
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtCarbuyDate">
                                            </asp:MaskedEditExtender>
                                            
                                            <asp:CalendarExtender ID="txtCarbuyDate_CalendarExtender" runat="server"  
                                                Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtCarbuyDate"  >
                                            </asp:CalendarExtender>
                                          
                        </td>
                    </tr>


                    <tr>
                        <td class="style1" bgcolor="#99CCFF">
                            Assign Tsr</td>
                        <td>
                            <asp:DropDownList ID="ddUser" runat="server" CssClass="jamp" 
                                DataSourceID="SqlUser" DataTextField="FullName" DataValueField="userID">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                    </td>
                    <td valign="top">
                     <table >
                      <tr>
                                    <td class="style1" bgcolor="#99CCFF">แหล่งที่มา :</td>
                                    <td>                                       
                                        <asp:DropDownList ID="ddListStation" runat="server" CssClass="jamp" 
                                            DataSourceID="SqlListStation" 
                                            DataTextField="SourceName" 
                                            DataValueField="SourceID">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                 <tr>
                                    <td class="style1" bgcolor="#99CCFF">ชื่อ-สกุล ลูกค้าที่แนะนำ:</td>
                                    <td>                                      
                                        <asp:TextBox ID="txtFNameTHSource" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtLNameTHSource" runat="server"></asp:TextBox>                                </td>
                                </tr>
                            </table>
                    </td>
                </tr>
            
            </table>

           
            </EmptyDataTemplate>
        </asp:FormView>
    
    </div>
    
    <div style="text-align: center">
    
        <asp:Button ID="Button3" runat="server" Text="AddNew" />
    
    </div>
    </asp:Panel>
</div>
<div>
    <font style="color: #000080; font-weight: bold; font-size: 13px">หมายเหตุการใช้งาน</font>
        <br />
        <font color="#003366">- กรอกชื่อนามสกุลลูกค้า แล้ว Check Dup ก่อน<br /> - 
        หากเจอรายชื่อลูกค้าที่มีอยู่ในระบบ ให้เลือกลูกค้าที่ต้องการ 
        จากนั้นจึงกรอกรายละเอียดรถยนต์ด้านล่าง<br /> - 
        หากไม่พบชื่อลูกค้าให้กรอกรายละเอียดด้านล่างได้เลยครับ</font></div>
 <asp:SqlDataSource ID="SqlUser" runat="server" 
            ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand="
                            if @UserLevel = 2
                            begin
                                                        SELECT  
                                                        a1.userID
                                                        ,a1.FName + ' ' + a1.LName + '(' + a1.NName + ')' as FullName
                                                        FROM [TblUser] a1
                                                        Where a1.LeaderID = @userID and a1.UserStatus = 1
                                                        order by a1.FName,a1.LName
                            end
                            else if @UserLevel = 1
                            begin
                                                         SELECT  
                                                        a1.userID
                                                        ,a1.FName + ' ' + a1.LName + '(' + a1.NName + ')' as FullName
                                                        FROM [TblUser] a1
                                                        Where    a1.UserStatus = 1 and UserLevelID in(5)
                                                        order by a1.FName,a1.LName
                            end

                            else
                            begin
                                                        SELECT  
                                                        a1.userID
                                                        ,a1.FName + ' ' + a1.LName + '(' + a1.NName + ')' as FullName
                                                        FROM [TblUser] a1
                                                        Where a1.SupID = @userID and a1.UserStatus = 1
                                                        order by a1.FName,a1.LName
                            end
                           
            ">
            <SelectParameters>
                <asp:CookieParameter CookieName="UserLevel" Name="UserLevel" />
                <asp:CookieParameter CookieName="userID" Name="userID" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlCustomer" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        
            SelectCommand="SELECT Top 1 * from TblCustomer Where CreateID = @userID Order by CreateDate DESC " 
            
        InsertCommand="INSERT INTO TblCustomer(FNameTH, LNameTH, Tel, OTel, Mobile, CreateID) VALUES (@FNameTH, @LNameTH, @Tel, @OTel, @Mobile, @userID)" 
        UpdateCommand="UPDATE TblCustomer SET Tel = @Tel, OTel = @OTel, Mobile = @Mobile WHERE (CusID = @cusID)">
            <SelectParameters>
               <asp:CookieParameter  Name="userID" CookieName ="userID" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="Tel" />
                <asp:Parameter Name="OTel" />
                <asp:Parameter Name="Mobile" />
                <asp:Parameter Name="cusID" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="FNameTH" />
                <asp:Parameter Name="LNameTH" />
                <asp:Parameter Name="Tel" />
                <asp:Parameter Name="OTel" />
                <asp:Parameter Name="Mobile" />
                <asp:CookieParameter  Name="userID" CookieName ="userID" />
            </InsertParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlCar" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="Select tblCustomer.FNameTH,tblCustomer.LNameTH,tblCustomer.Tel,tblCustomer.OTel,tblCustomer.Mobile
,isnull(TblAddNewSourceData.SourceDataID,5) as 'SourceDataID',TblAddNewSourceData.Fname,TblAddNewSourceData.Lname,tblCustomer.Cusid
from tblCustomer 
left join TblAddNewSourceData on tblCustomer.Cusid=TblAddNewSourceData.CusId
Where tblCustomer.CusID = @CusID" 
            InsertCommand="INSERT INTO TblCar(CusID, GroupID, DataID, AssignTo, CreateID, UpdateID, CurStatus, CarBuyDate,CarID,CarBrand,CarSeries) 
                            VALUES (@CusID, case @TypeTsr when 6 then 2866 when 206 then 346 else 99 end, case @TypeTsr when 206 then 6 else 1 end, @AssignTo, @userID, @userID, 1, @CarBuyDate,@CarID,@CarBrand,@CarSeries)">
                    <SelectParameters>
                        <asp:Parameter Name="CusID" />
                    </SelectParameters>
                    <InsertParameters>
                        <asp:Parameter Name="CusID" />
                        <asp:CookieParameter CookieName = "TypeTsr" Name="TypeTsr" />
                        <asp:Parameter Name="AssignTo" />
                        <asp:CookieParameter  Name="userID" CookieName ="userID" />
                        <asp:Parameter Name="CarBrand" />
                        <asp:Parameter Name="CarSeries" />
                        <asp:Parameter Name="CarID" />
                        <asp:Parameter Name="CarBuyDate" />
                    </InsertParameters>
        </asp:SqlDataSource>

          <asp:SqlDataSource ID="SqlCar_1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        
            InsertCommand="INSERT INTO TblCar(CusID, GroupID, DataID, AssignTo, CreateID, UpdateID, CurStatus, CarBuyDate,CarID,CarSize,CarNo,CarBoxNo,CarType,CarYear,CarBrand,CarSeries,CarBuyDate) 
                            VALUES (@CusID, case @TypeTsr when 6 then 2866 when 206 then 346 else 99 end, case @TypeTsr when 206 then 6 else 1 end, @AssignTo, @userID, @userID, 1, GETDATE(),@CarID,@CarSize,@CarNo,@CarBoxNo,@CarType,@CarYear,@CarBrand,@CarSeries,@CarBuyDate)">
                   
                    <InsertParameters>
                        <asp:Parameter Name="CusID" />
                        <asp:CookieParameter CookieName = "TypeTsr" Name="TypeTsr" />
                        <asp:Parameter Name="AssignTo" />
                        <asp:CookieParameter  Name="userID" CookieName ="userID" />
                         <asp:Parameter Name="CarID" />

                          <asp:Parameter Name="CarSize" />
                          <asp:Parameter Name="CarNo" />
                          <asp:Parameter Name="CarBoxNo" />
                          <asp:Parameter Name="CarType" />
                          <asp:Parameter Name="CarYear" />
                          <asp:Parameter Name="CarBrand" />
                          <asp:Parameter Name="CarSeries" />
                          <asp:Parameter Name="CarBuyDate" />
                          
                    </InsertParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlCarProvince" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT [province1] + '[' + Province2 + ']' as province1, [province2] FROM [Tbl_ProvinceCarid]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlCheckDup" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        
            SelectCommand="SELECT a1.FNameTH 
                            ,a1.LNameTH
                            ,a1.CusID
                            ,a1.Tel
                            ,a1.OTel
                            ,a1.Mobile
                      
                            from TblCustomer a1
                            Where a1.FNameTH = @FNameTH and a1.LNameTH = @LNameTH
                            "
            
            >
            <SelectParameters>
                <asp:Parameter Name="FNameTH" />
                <asp:Parameter Name="LNameTH" />
            </SelectParameters>

        </asp:SqlDataSource>
         <asp:SqlDataSource ID="SqlCarBrand" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT distinct [carBRAND] FROM [TblCarBrand] Order by carBRAND">
    </asp:SqlDataSource>

      <asp:SqlDataSource ID="SqlListStation" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT  [SourceID] ,[SourceName]  FROM [Tbl_MSourceData] where [SourceStatus]=1  Order by SourceID ">
    </asp:SqlDataSource>
</asp:Content>

