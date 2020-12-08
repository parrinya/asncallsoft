<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEditCreditDetail.aspx.vb" Inherits="Modules_Manager_Manage_Case_frmEditCreditDetail" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Infragistics35.WebUI.WebDataInput.v8.3, Version=8.3.20083.1009, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../../../Styles/style.css" type="text/css" media="screen"/>
    <style type="text/css">
        .style1
        {
            text-align: right;
            font-weight: bold;
        }
        .style3
        {
            text-align: left;
        }
        </style>
</head>
<body>     
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
       <div style="border-top: 3px solid #3399FF; margin-top: 5px; color :Black ;"  >     
        <table cellspacing="0" style="width:100%;">
            <div id="T1" runat=server>
            <tr>
                <td>
                    <table cellspacing="0" style="width:100%;">                       
                        
                  
                </td>
            </tr>
            <tr>
                <td bgcolor="#DDE8E4">
                    <asp:FormView ID="frmOldInsure" runat="server" DataKeyNames="AppID,SuccessDate,Statusqc" 
                        DataSourceID="SqlApplication">
                        <ItemTemplate>
                            TextBox6 :<asp:TextBox ID="TextBox6" runat="server"  Text='<%# bind("AppID") %>'></asp:TextBox>
                            TextBox7 :<asp:TextBox ID="TextBox7" runat="server"  Text='<%# bind("SuccessDate") %>'></asp:TextBox>                           
                        </ItemTemplate>                        
                    </asp:FormView>
                </td>
            </tr>
            </div>
            <tr >
           <td>
           <asp:UpdatePanel ID="UpdatePanel10" runat="server">
            <ContentTemplate >
            <asp:FormView ID="frmCusName" runat="server" DataSourceID="SqlCustomer" 
            Width="100%" DataKeyNames="CusID,FNameTH,LNameTH" BackColor="#D7EBFF">
            <ItemTemplate>
                <table style="width:100%;">
                    <tr>    
                    <td>ชื่อ-นามสกุล ผู้เอาประกัน </td>
                    <td><asp:TextBox ID="txtFNameTH" runat="server" Text='<%# Bind("FNameTH") %>' Width="100px" MaxLength="150"></asp:TextBox>
                    -<asp:TextBox ID="txtLNameTH" runat="server" Text='<%# Bind("LNameTH") %>'  Width="120px" MaxLength="150"></asp:TextBox>
                    </td>
                    </tr>                                     
                </table>
            </ItemTemplate>
        </asp:FormView>
       </ContentTemplate>
       </asp:UpdatePanel>
       </td>
       </tr>
       <tr>
         
                            <td bgcolor="#DDE8E4" valign="top">
                                <asp:FormView ID="frmCar" runat="server" DataSourceID="SqlCar" 
                                    DataKeyNames="IdCar,CarSeries,CarYear,CarBuyDate,CurStatus,CarID" Width="100%" BackColor="#D7EBFF">
                                <ItemTemplate>  
                                <table>                                  
                                   <tr>
                                        <td class="style1" valign="top">เลขทะเบียน</td>
                                        <td><asp:Label ID="txtCarID" runat="server" Text='<%# Bind("CarID1") %>'></asp:Label></td>
                                    </tr>
                                </table> 
                                </ItemTemplate>
                                </asp:FormView>
                           
                    <asp:FormView ID="frmProType" runat="server" DataSourceID="SqlPackage" 
                        Width="100%" DataKeyNames="ProTypeID" BackColor="#D7EBFF">
                        <ItemTemplate>
                          
        ประเภทประกันภัยรถยนต์ :
                            <asp:Label ID="Label46" runat="server" Text='<%# Eval("ProtypeName") %>' 
                                Font-Bold="True" Font-Size="13pt"></asp:Label>
                           
                            &nbsp;<asp:Label ID="Label43" runat="server" Text='<%# Eval("Detail") %>'></asp:Label>
                            &nbsp;<asp:Label ID="Label45" runat="server" Text='<%# Eval("IsFixIn") %>' 
                                Font-Bold="True" Font-Size="13pt"></asp:Label>
                        </ItemTemplate>
                    </asp:FormView>
                </td>
            </tr>
            <tr>
                <td bgcolor="#DDE8E4">
                    <asp:FormView ID="frmAppRela" runat="server" Width="100%" BackColor="#D7EBFF"
                        DataSourceID="SqlApplication" DataKeyNames="IsCarpet,CreateDate,SuccessDate">
                        <ItemTemplate>
                            <table style="width:100%;">                                
                                <tr>
                                    <td>
                                        ระยะเวลาประกันภัย(เริ่มต้น)
                                        <asp:TextBox ID="txtProtectDate" runat="server" 
                                            Text='<%# Bind("ProtectDate", "{0:d}") %>' Width="100px"></asp:TextBox>
                                        <asp:MaskedEditExtender ID="txtProtectDate_MaskedEditExtender" runat="server" 
                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtProtectDate">
                                        </asp:MaskedEditExtender>
                                        <asp:CalendarExtender ID="txtProtectDate_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd/MM/yyyy" PopupButtonID="" 
                                            TargetControlID="txtProtectDate">
                                        </asp:CalendarExtender>
                                        ระยะเวลาประกันภัย พรบ.(เริ่มต้น)&nbsp;<asp:TextBox ID="txtProtectDateCarpet" 
                                            runat="server" Text='<%# Bind("ProtectDateCarpet", "{0:d}") %>' Width="100px"></asp:TextBox>
                                        <asp:MaskedEditExtender ID="txtProtectDateCarpet_MaskedEditExtender" 
                                            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtProtectDateCarpet">
                                        </asp:MaskedEditExtender>
                                        <asp:CalendarExtender ID="txtProtectDateCarpet_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd/MM/yyyy" PopupButtonID="" 
                                            TargetControlID="txtProtectDateCarpet">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>                        
                    </asp:FormView>
                </td>
            </tr>
             <tr>
                <td bgcolor="#DDE8E4">
                <asp:FormView ID="frmPackage" runat="server" DataSourceID="SqlPackage" Width="100%" 
          DataKeyNames="Detail1,Detail2,Detail3,Detail4,Detail5,Detail6,Detail7,Detail8,Detail9,Detail10,Detail11,Detail12,value1,Preminum,Carpet,ProValue,ProTypeID,TypeID,PetValue,PetVat,Driver1,Driver2,FixIn,AppsubmitID,StatusCarpet">
              <ItemTemplate>
                  <table style="width:100%;">
                     
                     
                      <tr bgcolor="#CCFFFF" style="color: #000000; font-weight: bold">
                         
                          <td class="style13" style="text-align: center">
                              พรบ. <asp:Label ID="lblCappet" runat="server" Text='<%# Eval("Carpet", "{0:N}") %>'></asp:Label>
                          </td>
                          <td style="text-align: center">
                              เบี้ยขาย. <asp:Label ID="txtTotalValue" runat="server" Text='<%# Eval("Preminum2", "{0:N2}") %>'></asp:Label>
                            <br/>
                          </td>
                      </tr>
                  </table>
              </ItemTemplate>
          </asp:FormView></td>
            </tr>
            <tr>
                <td bgcolor="#003366" 
                    style="color: #FFFFFF; font-weight: bold; font-size: 20px; text-align: center;">
                    คำนวนการชำระเงิน       
            </td>
            <tr>
                <td bgcolor="#B9D0DD" style="text-align: center; font-weight: 700">
                     <asp:UpdatePanel ID="upAppPay" runat="server"  >
                 <ContentTemplate > ประเภทการชำระ :
                    <asp:DropDownList ID="ddTypePay" runat="server" CssClass="jamp">
                        <asp:ListItem Value="1">เงินสด</asp:ListItem>
                        <asp:ListItem Value="2">บัตรเครดิต</asp:ListItem>
                    </asp:DropDownList>
&nbsp;งวดการชำระ :
                    <asp:DropDownList ID="ddPay" runat="server" CssClass="jamp">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                    </asp:DropDownList>
&nbsp;<asp:CheckBox ID="chkPay" runat="server" Text="งวดแรก 3000" Visible="False" />
&nbsp;<asp:CheckBox ID="chkCredit" runat="server" Text="เครดิตทุกงวด" />
                &nbsp;
               
                   <asp:Button ID="Button14" runat="server" Text="คำนวน" />
                 
                     <font color="#FF3300">*ส่วนนี้เป็นการคำนวน ข้อมูลจริงให้ดูในตารางด้านล่างนี้</font>
                 </ContentTemplate>
                 </asp:UpdatePanel>
              
                </td>
            </tr>
            <tr>
                <td bgcolor="white">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate >                   
                    <asp:GridView ID="GvPay" runat="server" AutoGenerateColumns="False" 
                Width="100%" DataKeyNames="Typepay,ProValue">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="งวดที่">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label></ItemTemplate><EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></EditItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="วันที่ชำระ">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAppoint" runat="server" ReadOnly="True" 
                                Text='<%# Bind("AppointDate", "{0:dd}/{0:MM}/{0:yyyy}") %>' Width="80px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="txtAppoint_MaskedEditExtender" runat="server" 
                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtAppoint">
                            </asp:MaskedEditExtender>
                            <asp:CalendarExtender ID="txtAppoint_CalendarExtender" runat="server" 
                                Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtAppoint">
                            </asp:CalendarExtender>
                        </ItemTemplate><EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("PayDate") %>'></asp:TextBox></EditItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="จำนวนเงิน">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("ProValue","{0:N}") %>' 
                                ForeColor="#00CC00"></asp:Label></ItemTemplate><EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("ProValue") %>'></asp:TextBox></EditItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="ค่าธรรมเนียม">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("ProVat") %>' 
                                ForeColor="#00CC00"></asp:Label></ItemTemplate><EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("ProVat") %>'></asp:TextBox></EditItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="จำนวนเงินที่ชำระ">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalPay" runat="server" Text='<%# Bind("TotalPay","{0:N}") %>' 
                                Font-Bold="True" ForeColor="Red"></asp:Label></ItemTemplate><EditItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("TotalPay") %>'></asp:TextBox></EditItemTemplate></asp:TemplateField>
                    <asp:BoundField DataField="TypeName" HeaderText="การชำระ" />
                        </Columns>
                        <HeaderStyle Font-Size="10pt" ForeColor="Black" 
                    Height="30px" BackColor="#B9D0DD" />
            </asp:GridView>
            <center><asp:Button ID="btnsavepay"  runat="server" Text="บันทึกการคำนวน"  /></center>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
            </tr>
            <tr>
                <td bgcolor="#003366">
                
                </td>
            </tr> 
            <tr>
                <td bgcolor="#E0C9A7">
                    <table cellspacing="0" style="width:100%;">
                        <tr>
                            <td style="text-align: center; font-weight: 700">
                             ข้อมูลบัตรเครดิต 
                            </td>
                                         
                        </tr>
                        <tr >
                        <td bgcolor="#F4ECDF" >
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate >
                                <asp:FormView ID="frmAppCard" runat="server" DataSourceID="SqlAppCard" 
                    DataKeyNames="AppCardId" Width="100%">
                    <ItemTemplate>
                        <table style="width:100%;">
                            <tr>
                                <td class="style1">
                                    เลขบัตรเครดิต</td><td class="style3">
                                    <asp:TextBox ID="txtCardID" runat="server" Width="151px" 
                                        Text='<%# Bind("CardNo1") %>'></asp:TextBox><asp:MaskedEditExtender ID="txtCardID_MaskedEditExtender" runat="server" 
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        Mask="9999-9999-9999-9999" TargetControlID="txtCardID">
                                    </asp:MaskedEditExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    ชื่อผู้ถือบัตร</td><td class="style3">
                                    <asp:TextBox ID="txtCardName" runat="server" Text='<%# Bind("Cardname") %>' 
                                        Width="151px" BackColor="#FFFFCC"></asp:TextBox><asp:Button ID="Button8" runat="server" Text="..." onclick="Button8_Click1" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="txtCardName" ErrorMessage="ชื่อผู้ถือบัตร" 
                                        ForeColor="#FF3300" ValidationGroup="CardID">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    เลข 3 ตัวท้าย</td><td class="style3">
                                    <asp:TextBox ID="txtCardID3" runat="server" MaxLength="3" 
                                        Text='<%# Bind("CardNo2") %>' Width="50px"></asp:TextBox></td></tr><tr>
                                <td class="style1">
                                    วันหมดอายุ</td><td class="style3">
                                    <asp:TextBox ID="txtExpCard" runat="server" MaxLength="2" Width="50px" 
                                        Text='<%# Bind("CardExp") %>'></asp:TextBox><asp:MaskedEditExtender ID="txtExpCard_MaskedEditExtender" runat="server" 
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        Mask="99-99" TargetControlID="txtExpCard">
                                    </asp:MaskedEditExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    ประเภทบัตร</td><td class="style3">
                                    <asp:DropDownList ID="ddTypeCard" runat="server" CssClass="jamp" 
                                        DataSourceID="SqlTypeCard" DataTextField="CardName" 
                                        DataValueField="CardId" SelectedValue='<%# Bind("Cardid") %>'>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    ธนาคาร</td><td class="style3">
                                    <asp:DropDownList ID="ddBank" runat="server" CssClass="jamp" 
                                        DataSourceID="SqlBank" DataTextField="BankName" DataValueField="BankID" 
                                        SelectedValue='<%# Bind("Bankid") %>'>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    เบอร์โทร</td><td class="style3">
                                    <asp:TextBox ID="txtTel" runat="server" Text='<%# Bind("Tel1") %>'></asp:TextBox></td></tr><tr>
                                <td class="style1">
                                    มือถือ</td><td class="style3">
                                    <asp:TextBox ID="txtMobile" runat="server" Text='<%# Bind("Mobile") %>'></asp:TextBox></td></tr><tr>
                                <td class="style1">
                                    <asp:CheckBox ID="chdFristPay" runat="server" Text="ตัดบัตรงวดแรก" 
                                        Checked='<%# Bind("IsPayDate") %>' />
                                </td>
                                <td class="style3">
                                    <asp:TextBox ID="txtFristPay" runat="server" Width="80px" 
                                        Text='<%# Bind("PayDate", "{0:dd/MM/yyyy}") %>'></asp:TextBox><asp:MaskedEditExtender ID="txtFristPay_MaskedEditExtender" runat="server" 
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        Mask="99/99/9999" TargetControlID="txtFristPay" MaskType="Date">
                                    </asp:MaskedEditExtender>
                                    <asp:CalendarExtender ID="txtFristPay_CalendarExtender" runat="server" 
                                        Enabled="True" Format="dd/MM/yyyy" 
                                        TargetControlID="txtFristPay">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                           
                            <tr>
                                <td class="style1" valign="top">
                                    &nbsp;</td><td class="style3">
                                    <asp:Button ID="btnSaveCard" runat="server" Text="บันทึก" 
                                        onclick="btnSaveCard_Click1" ValidationGroup="CardID" />
                                    <asp:Button ID="Button3" runat="server" Text="ยกเลิก" onclick="Button3_Click" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table style="width:100%;">
                            <tr>
                                <td class="style1">
                                    เลขบัตรเครดิต</td><td class="style3">
                                    <asp:TextBox ID="txtCardID" runat="server" Width="151px"></asp:TextBox><asp:MaskedEditExtender ID="txtCardID_MaskedEditExtender" runat="server" 
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        Mask="9999-9999-9999-9999" TargetControlID="txtCardID">
                                    </asp:MaskedEditExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    ชื่อผู้ถือบัตร</td><td class="style3">
                                    <asp:TextBox ID="txtCardName" runat="server" Width="151px" BackColor="#FFFFCC"></asp:TextBox><asp:Button ID="Button8" runat="server" Text="..." onclick="Button8_Click1" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="txtCardName" ErrorMessage="ชื่อผู้ถือบัตร" 
                                        ForeColor="#FF3300" ValidationGroup="CardID">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    เลข 3 ตัวท้าย</td><td class="style3">
                                    <asp:TextBox ID="txtCardID3" runat="server" MaxLength="3" Width="50px"></asp:TextBox></td></tr><tr>
                                <td class="style1">
                                    วันหมดอายุ</td><td class="style3">
                                    <asp:TextBox ID="txtExpCard" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="txtCardID3_MaskedEditExtender" runat="server" 
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        Mask="99-99" TargetControlID="txtCardID3">
                                    </asp:MaskedEditExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    ประเภทบัตร</td><td class="style3">
                                    <asp:DropDownList ID="ddTypeCard" runat="server" CssClass="jamp" 
                                        DataSourceID="SqlTypeCard" DataTextField="CardName" DataValueField="CardId">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    ธนาคาร</td><td class="style3">
                                    <asp:DropDownList ID="ddBank" runat="server" CssClass="jamp" 
                                        DataSourceID="SqlBank" DataTextField="BankName" DataValueField="BankID" SelectedValue='<%# Bind("Bankid") %>' 
                                        >
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    เบอร์โทร</td><td class="style3">
                                    <asp:TextBox ID="txtTel" runat="server"></asp:TextBox></td></tr><tr>
                                <td class="style1">
                                    มือถือ</td><td class="style3">
                                    <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="txtMobile_MaskedEditExtender" runat="server" 
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        Mask="99-99999999" TargetControlID="txtMobile">
                                    </asp:MaskedEditExtender>
                                </td></tr><tr>
                                <td class="style1">
                                    <asp:CheckBox ID="chdFristPay" runat="server" Text="ตัดบัตรงวดแรก" />
                                </td>
                                <td class="style3">
                                    <asp:TextBox ID="txtFristPay" runat="server" Width="80px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="txtFristPay_MaskedEditExtender" runat="server" 
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtFristPay">
                                    </asp:MaskedEditExtender>
                                    <asp:CalendarExtender ID="txtFristPay_CalendarExtender" runat="server" 
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFristPay">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1" valign="top">
                                    &nbsp;</td><td class="style3">
                                    <asp:Button ID="btnSaveCard" runat="server" Text="เพิ่ม" 
                                        onclick="btnSaveCard_Click" ValidationGroup="CardID" />
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:FormView>
                </ContentTemplate>
                </asp:UpdatePanel>
                </td>
                </tr>
                </table>
                </td>
            </tr>
            <tr>
                <td bgcolor="White">
                 <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                 <ContentTemplate >
                 <asp:GridView ID="GvAppCard" runat="server" AutoGenerateColumns="False" 
                Width="100%" DataKeyNames="CardNo1,CardNo2,CardExp,Cardname,PayTypeId,Cardid,Bankid,PayDate,IsPayDate,Tel1,Mobile,AppCardID" 
                   EmptyDataText="ไม่พบข้อมูลบัตรเครดิต" DataSourceID="SqlAppCard2">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="Button3" runat="server" 
                                CommandArgument="<%# Container.DataItemIndex %>" CommandName="Select" 
                                Text="เลือก" Visible='<%# Bind("StatusEdit") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" runat="server" CommandName="Remove" Text="ลบ" 
                                CommandArgument="<%# Container.DataItemIndex %>" 
                                Visible='<%# Bind("StatusEdit") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ลำดับที่" SortExpression="CardRun">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label></ItemTemplate><EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CardRun") %>'></asp:TextBox></EditItemTemplate></asp:TemplateField><asp:BoundField DataField="Cardname" HeaderText="ชื่อผู้ถือบัตร" 
                        SortExpression="Cardname" />
                    <asp:BoundField DataField="CardNo1" HeaderText="เลขบัตรเครดิต" 
                        SortExpression="CardNo1" />
                    <asp:BoundField DataField="BankName" HeaderText="ธนาคาร" 
                        SortExpression="BankName" />
                </Columns>
                <HeaderStyle Font-Size="10pt" ForeColor="Black" 
                    Height="30px" BackColor="#B9D0DD" />
            </asp:GridView>

                                </ContentTemplate>
                                </asp:UpdatePanel>
                    </td>
            </tr>
            <tr><td><center>
                <center></td></tr>
            </table>
         
        
       
     </div>
     <asp:SqlDataSource ID="SqlCustomer" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT distinct a1.*  FROM [TblCustomer] a1  
        inner join tblapplication a2 on a1.cusid=a2.cusid where a2.appid =@AppID">       
    <SelectParameters>
        <asp:QueryStringParameter Name="AppID" QueryStringField="AppID" />
    </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlInit" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT [InitID], [InitTH] FROM [TblCustomerInit]"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlProvince" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT Distinct [Province] FROM [TblZipcode] Order by Province "></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlCar" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="
                        SELECT distinct a1.* 
                        ,a3.Code
                        ,a7.CodeType
                       ,'CarID1' = case a1.CarID when '' then '' when 'ป้ายแดง' then '' 
                        else case when a4.province2 IS Not Null then LEFT(a1.CarID,len(a1.CarID)-2) else a1.CarID  end end 
                        ,case when a1.CarID = 'ป้ายแดง' then 'ป้ายแดง' when a4.Province2 is null and a1.CarID &lt;&gt; 'ป้ายแดง' then 'กท' else a4.Province2 end as CarID2      
                        ,case when a5.Province is null then '[ไม่ระบุ]' else a5.Province end  as DBornProvince2
                        ,case when a6.Province is null then '[ไม่ระบุ]' else a6.Province end  as DBornProvince1
                        FROM [TblCar] a1
                        Inner Join TBlapplication a10 on a1.CusID = a10.CusID
                        Inner Join TblCustomer a2 on a1.CusID = a2.CusID
                        Left Join TblInsur_carType a3 on a1.CarType = a3.CarTypeID
                        Left Join Tbl_CarType as a7 on a1.CarType = a7.CarTypeID
                        left Join Tbl_ProvinceCarID a4 on Right(a1.CarID,2) = a4.Province2
                        left join TblZipCode a5 on a1.DBornAddr2 = a5.Province
                         left join TblZipCode a6 on a1.DBornAddr1 = a6.Province
                        Where a10.AppID = @AppID">
        <SelectParameters>
            <asp:QueryStringParameter Name="AppID" QueryStringField="AppID" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlCarProvince" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT distinct [province1] + '[' + province2 + ']' as province1 ,province2 FROM [Tbl_ProvinceCarid] ">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlApplication" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        
        SelectCommand="
                      
                        SELECT a1.* 
                        ,case when a4.FName  is null then '' else a4.FName end   as AppRelaTH    
                        ,case when a5.ProTypeName is null then '[ไม่ระบุ]' else a5.ProTypeName end as OldInsureTH  
                        ,case a1.chkDeviceAdd when 1 then 'True' else 'False' end as chkDeviceStatus    
                        ,a10.AStatusname
                        FROM [TblApplication] a1
                        Inner Join TblCar a2 on a1.IdCar = a2.IdCar
                        Inner Join TblCustomer a3 on a3.CusID = a2.CusID
                        Left Join TblFinance  a4 on a1.AppRela = a4.FName
                        Left Join Tbl_ProductType a5 on a1.Old_Insu = a5.ProTypeName
                        Left Join Tbl_aStatus a10 on a1.StatusQc = a10.Astatusid 
                        where a1.AppID = @AppID
                      " >
        <SelectParameters>            
            <asp:QueryStringParameter DefaultValue="0" Name="AppID" QueryStringField="AppID" />          
        </SelectParameters>    
    </asp:SqlDataSource>
  

    <asp:SqlDataSource ID="SqlPackage" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand=" 
                        SELECT					a1.Lost_Life1  as Detail1
						,a1.Lost_Life2 as Detail2
						,a1.Lost_Prop1 as Detail3
                        ,a1.Lost_Prop2 as Detail13
						,a1.Lost_Car1 as Detail7
						,a1.Lost_Car2 as Detail9
						,a1.Car_Fire as Detail8
						,a1.Acc_Lost1 as Driver1
						,a1.Acc_Lost2 as Detail10
						,a1.Acc_Lost3 as Driver2
						,a1.Maintain as Detail5
						,a1.Insure as Detail6
						,a1.YearPay as Preminum
						,a1.Carpet as Carpet
						,a1.ProValue as Preminum2
						,'' as Detail11
						,'' as Detail12
						,'' as Detail4
						,'' as value1
						,a1.Carpet + a1.ProValue as ProValue
                        ,a6.ProTypeID
                        ,a6.ProTypeName
                        ,a7.TypeName
                        ,a1.TypeProValue as TypeID
                        ,a1.CarpetValue as PetValue
                        ,'' as PetVat
                        ,case a2.CarFixIn when '1' then 'ซ่อมอู่'  else 'ซ่อมห้าง' end as IsFixIn 
                        ,a1.IsCarpet  as IsCarpet 
                        ,case when a4.FName  is null then '' else a4.FName end   as AppRelaTH    
                        ,case when a5.ProTypeName is null then '[ไม่ระบุ]' else a5.ProTypeName end as OldInsureTH 
                        ,a2.CarFixIn  as FixIn
                        ,a1.PkgID as   AppsubmitID
                        ,'' as Driver1
                        ,'' as Driver2    
                        ,a8.Detail  
                        ,case a1.IsCarpet when 1 then 'True' else 'False' end  as StatusCarpet
                        ,a1.IsProValue as IsProValue
                        FROM [TblApplication] a1
                        Inner Join TblCar a2 on a1.IdCar = a2.IdCar
                        Inner Join TblCustomer a3 on a3.CusID = a2.CusID
                        Inner Join Tbl_ProductType a6 on a1.ProductID = a6.ProTypeID
                        Inner Join Tbl_Type a7 on a1.TypeProValue = a7.TypeID
                        Left Join TblFinance  a4 on a1.AppRela = a4.FName
                        Left Join Tbl_ProductType a5 on a1.Old_Insu = a5.ProTypeName
                        Inner Join TblAppSubmit a8 on a1.PkgID = a8.AppSubmitID
                        Inner join Tbl_CarType a9 on a8.CarTypeID = a9.CarNo
                        where a1.AppID=@AppID
                       ">
        <SelectParameters>

            <asp:QueryStringParameter DefaultValue="0" Name="Buy" QueryStringField="Buy" />

            <asp:Parameter Name="IsCappet" DefaultValue="0" />

            <asp:QueryStringParameter Name="SubmitID" QueryStringField="AppsubmitID" 
                DefaultValue="0" />
            <asp:Parameter DefaultValue="0" Name="changeCarpet" />
            <asp:QueryStringParameter DefaultValue="0" Name="AppID" 
                QueryStringField="AppID" />
            <asp:QueryStringParameter Name="IdCar" QueryStringField="IdCar" 
                DefaultValue="0" />

        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlProduct" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT [ProTypeName] FROM [Tbl_ProductType]   order by  ProTypeName"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlAppPay" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand ="Select * 
                        ,case TypePay when '2' then TotalPay else TotalPay - 10 end as ProValue
                        ,case TypePay when '2' then 0 else 10 end as ProVat
                        ,case TypePay when 2 then 'credit' else 'Payment' end as TypeName
                        From TblAppPay
                         Where AppID = @AppID
                        order by PayId
                        " 
        DeleteCommand="DELETE FROM TblAppPay WHERE (AppID = @AppID)" 
        InsertCommand="INSERT INTO TblAppPay(PayID, AppID, TotalPay, CreateID, AppointDate, UpdateID, UpdateDate, Typepay ) 
                        VALUES (@PayID, @AppID, @TotalPay, @userID, @AppointDate, @userID, GETDATE(), @Typepay)"
        >
         <SelectParameters>
              <asp:QueryStringParameter DefaultValue="0" Name="AppID" QueryStringField="AppID" />
         </SelectParameters>
         <DeleteParameters>
             <asp:Parameter Name="AppID" />
         </DeleteParameters>
         <InsertParameters>
             <asp:Parameter Name="PayID" />
             <asp:Parameter Name="AppID" />
             <asp:Parameter Name="TotalPay" />
             <asp:Parameter Name="AppointDate" />
             <asp:Parameter Name="Typepay" />
             <asp:CookieParameter Name = "userID" CookieName = "userID" />
         </InsertParameters>
        
        
     </asp:SqlDataSource>

     <asp:SqlDataSource ID="SqlAppCard" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand ="Select * 
                        ,Left(CardExp,2) as ExpCard1
                        ,Right(CardExp,2) as ExpCard2
                         From TblAppCard
                         Where AppCardId = @AppCardId" 
        InsertCommand="INSERT INTO TblLogAppPay(AppID, IdCar, PayNo1, PayNo2, CreateID) 
                        VALUES (@AppID, @IdCar, @PayNo1, @PayNo2, @userID)">
         <InsertParameters>
             <asp:Parameter Name="AppID" />
             <asp:QueryStringParameter QueryStringField = "IdCar" Name="IdCar" />
             <asp:Parameter Name="PayNo1" />
             <asp:Parameter Name="PayNo2" />
             <asp:CookieParameter CookieName = "userID" Name="userID" />
         </InsertParameters>
         <SelectParameters>
             <asp:Parameter Name="AppCardId" />
         </SelectParameters>
        
        
     </asp:SqlDataSource>

     <asp:SqlDataSource ID="SqlTypeCard" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT [CardId], [CardName] FROM [TblCard]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlBank" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT [BankID], [BankName] FROM [TblBank]"></asp:SqlDataSource>
    
    <asp:SqlDataSource ID="SqlTypePay" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT [PayTypeID], [PayTypeName] FROM [TblPayType]  "></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlAppCard2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand =" Select distinct a1.* 
                        ,a2.BankName
                        ,case when a3.appID IS not null then 'False' else 'True' end as StatusEdit
                        From TblAppCard a1
                        Inner Join TblBank a2 on a1.BankID = a2.BankID
                        Left Join TblCardApprove a3 on a1.CardNo1= a3.CardNo1 and a3.appvStatus = 2 and a1.AppId = a3.AppID
                        and DATEDIFF (DAY ,a3.createDATE ,GETDATE()) &lt; 180
                        Where a1.AppID = @AppID
                        order by a1.AppCardID
                        " DeleteCommand="DELETE FROM TblAppCard WHERE (AppCardId = @AppCardId)" >
         <SelectParameters>
              <asp:QueryStringParameter DefaultValue="0" Name="AppID" 
                QueryStringField="AppID" />
         </SelectParameters>
         <DeleteParameters>
             <asp:Parameter Name="AppCardId" />
         </DeleteParameters>
        
        
     </asp:SqlDataSource>
    
     <asp:SqlDataSource ID="SqlGetApplication" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="Select top 1 * From TblApplication
                        Where AppID = @AppID 
                        order by CreateDate DESC ">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="AppID" 
                QueryStringField="AppID" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlEditPay" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>"        
       InsertCommand="INSERT INTO TblLogEditAppPay([PO],[PayID],[AppID],[PayDate],[TotalPay],[CreateID],[CreateDate],
                    [IsPaid_O],[Ispaid],[AppointDate],[UpdateID],[UpdateDate],
                    [PayBy],[Typepay],[Assignto],[Assigndate],[CallAt],[payBARCODE],
                    [commVALUE],[POtype],[chkAPPOINTDATE],[CreateIDEdit],[CreateDateEdit],IPAddress) 
                    select [PO],[PayID],[AppID],[PayDate],[TotalPay],[CreateID],[CreateDate],
                    [IsPaid_O],[Ispaid],[AppointDate],[UpdateID],[UpdateDate],
                    [PayBy],[Typepay],[Assignto],[Assigndate],[CallAt],[payBARCODE],
                    [commVALUE],[POtype],[chkAPPOINTDATE],@userid,getdate(),@IPAddress from TblAppPay where appid=@appid">
             <InsertParameters>           
                 <asp:QueryStringParameter QueryStringField = "AppID" Name="AppID" />           
                 <asp:CookieParameter CookieName = "userID" Name="userID" />
                 <asp:Parameter Name="IPAddress" />
             </InsertParameters>
     </asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlEditCard" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>"        
       InsertCommand="INSERT INTO TblLogEditAppCard([AppCardId],[Appid],[CardRun],[CardNo1],[CardNo2],[CardExp],
	[Cardname],[PayTypeId],[Cardid],[Bankid],[PayDate],[IsPayDate],
	[Createid],[Createdate],[Updatedate],[Updateid],[Tel1],[Mobile],
	[sendTYPE],[CreateidEdit],[CreatedateEdit],IPAddress) 
       select [AppCardId],[Appid],[CardRun],[CardNo1],[CardNo2],[CardExp],
	[Cardname],[PayTypeId],[Cardid],[Bankid],[PayDate],[IsPayDate],
	[Createid],[Createdate],[Updatedate],[Updateid],[Tel1],[Mobile],
	[sendTYPE],@userid,getdate(),@IPAddress from TblAppCard where AppCardId=@AppCardId">
             <InsertParameters>           
                 <asp:Parameter Name="AppCardId" />   
                  <asp:Parameter Name="IPAddress" />          
                 <asp:CookieParameter CookieName = "userID" Name="userID" />
             </InsertParameters>
     </asp:SqlDataSource>
    </form>
</body>
</html>
