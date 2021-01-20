<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmPhone.aspx.vb" Inherits="Modules_Sale_Phone_frmPhone" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register assembly="Infragistics35.WebUI.WebDataInput.v8.3, Version=8.3.20083.1009, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>

<%@ Register assembly="Infragistics35.WebUI.WebDataInput.v8.3" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            text-align: right;
            font-weight: bold;
        }
        .style2
        {
            text-align: right;
        }
        .style3
        {
            text-align: left;
            font-weight: normal;
        }
        .style4
        {
            text-align: left;
        }
        .style5
        {
            text-align: left;
            font-weight: normal;
            width: 179px;
        }
        </style>

    <script language="javascript" type="text/javascript">



        function change_parent_url(url) {
            document.location = url;
        }

        function textboxMultilineMaxNumber(txt, maxLen, e) {
            try {
                if (txt.value.length > (maxLen - 1) && e.keyCode != 8) return false;
            } catch (e) {
            }
        }
   
    </script>
	<script type="text/javascript">


    function sett() {
        $("#AppID").html(document.getElementById('<%=HFAppID.ClientID%>').value);
        $("#cusname").html(document.getElementById('<%=HFCusname.ClientID%>').value);
        $("#dialog").dialog({
            title: "บทสรุปยืนยันปิดการขาย",
            buttons: {

                Confirm: function () {
                    
                    $('#dialog').dialog('close');
                }

            }, modal: true

        });
        return false;
    }
</script>
	<script type="text/javascript" src="../../../Scripts/1.7.2.jquery.min.js.htm"></script>
    <script src="../../../Scripts/jquery-ui-1.8.9/ui/jquery-ui.js" type="text/javascript"></script>
    <link href="../../../Scripts/jquery-ui-themes-1.8.9/themes/start/jquery-ui.css" rel="stylesheet" type="text/css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: center; background-color: #CCFFFF; font-weight: bold; font-size: 14px;">

    <asp:FormView ID="frmCus" runat="server" Width="100%" 
        DataSourceID="SqlCustomer" 
            
            DataKeyNames="CusID,CurStatus,CntStatus,IDCar,Exten,RefNo,IsNew,FNameTH,LNameTH">
        <ItemTemplate>
            ชื่อ-นามสกุล :
            <asp:Label ID="Label1" runat="server" Text='<%# Eval("FNameTH") %>'></asp:Label>
            <asp:Label ID="Label2" runat="server" Text='<%# Eval("LNameTH") %>'></asp:Label>
        </ItemTemplate>
    </asp:FormView>

</div>
<div>
    <table style="width: 100%;" cellspacing="0">
        <tr>
            <td valign="top" style="width :50%;">
                <table style="width: 100%;" cellspacing="0">
                    <tr>
                        <td style="text-align: center; font-weight: bold; background-color: #CCFFCC;">
                            ที่อยู่ลูกค้า</td>
                    </tr>
                    <tr>
                        <td bgcolor="#D5FFD5">
                            <asp:FormView ID="frmAddr" runat="server" Width="100%" 
                                DataSourceID="SqlCustomer" DataKeyNames="Dist,SubDist,Zip">
                                <ItemTemplate>
                                    <table style="width:100%;">
                                        <tr>
                                            <td class="style1">
                                                เลขที่</td>
                                            <td>
                                                <asp:TextBox ID="txtAddr" runat="server" Text='<%# Bind("Address") %>' 
                                                    Width="80px"></asp:TextBox>
                                                หมู่<asp:TextBox ID="txtMoo" runat="server" Text='<%# Bind("Moo") %>' 
                                                    Width="40px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                หมู่บ้าน/อาคาร/ชั้น/ห้อง</td>
                                            <td>
                                                <asp:TextBox ID="txtViilege" runat="server" Text='<%# Bind("Villege") %>' 
                                                    Width="250px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                ถนน</td>
                                            <td>
                                                <asp:TextBox ID="txtRoad" runat="server" Text='<%# Bind("Road") %>' 
                                                    Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                ซอย</td>
                                            <td>
                                                <asp:TextBox ID="txtSoi" runat="server" Text='<%# Bind("Soi") %>' Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                จังหวัด</td>
                                            <td>
                                                <asp:DropDownList ID="ddProvince" runat="server" AppendDataBoundItems="True" 
                                                    AutoPostBack="True" CssClass="jamp" DataSourceID="SqlProvince" 
                                                    DataTextField="Province" DataValueField="Province" 
                                                    onselectedindexchanged="ddProvince_SelectedIndexChanged" 
                                                    SelectedValue='<%# Bind("Province") %>'>
                                                    <asp:ListItem>[ไม่ระบุ]</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                อำเภอ/เขต</td>
                                            <td>
                                                <asp:DropDownList ID="ddDist" runat="server" AutoPostBack="True" 
                                                    CssClass="jamp" onselectedindexchanged="ddDist_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                ตำบล/แขวง</td>
                                            <td>
                                                <asp:DropDownList ID="ddSubDist" runat="server" AutoPostBack="True" 
                                                    CssClass="jamp" onselectedindexchanged="ddSubDist_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                รหัสไปรษณีย์</td>
                                            <td>
                                                <asp:DropDownList ID="ddZipcode" runat="server" CssClass="jamp">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                </td>
                                            <td>
                                                <asp:Button ID="WebImageButton1" runat="server" Text="บันทึกที่อยู่" 
                                                    onclick="WebImageButton1_Click3" />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:FormView>
                        </td>
                    </tr>
                    
                    <tr>
                        <td bgcolor="#0033CC" style="text-align: center">
                            <font color="White" style="font-weight: 700">ลงสถานะ ระบบการโทร</font></td>
                    </tr>
                    <tr>
                        <td bgcolor="#D2E9FF" style="text-align: center">
                            <asp:FormView ID="frmRecruit" runat="server" Width="100%" 
                                DataSourceID="SqlRecruit" DataKeyNames="RunID">
                                
                            <EmptyDataTemplate >
                            
                                <table style="width:100%;">
                                    <tr>
                                        <td class="style1">
                                            แหล่งที่มา</td>
                                        <td class="style4">
                                            <asp:DropDownList ID="ddStatus" runat="server" CssClass="jamp" 
                                                DataSourceID="SqlRecruitStatus" DataTextField="StatusTH" 
                                                DataValueField="StatusID" AppendDataBoundItems="True">
                                                <asp:ListItem Value="0">ลูกค้าไม่สะดวกลงความเห็น</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style1">
                                            หมายเหตุที่มา</td>
                                        <td class="style4">
                                            <asp:TextBox ID="txtComments" runat="server" Height="100px" TextMode="MultiLine" 
                                                Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            
                            </EmptyDataTemplate>
                                
                                
                            </asp:FormView>
                            <table style="width:100%;">
                                <tr>
                                    <td class="style1" valign="top">
                                        สถานะหลัก</td>
                                    <td class="style4">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate >
                                        <asp:DropDownList ID="ddStatus" runat="server" AppendDataBoundItems="True" 
                                            AutoPostBack="True" CssClass="jamp" DataSourceID="SqlStatus" 
                                            DataTextField="StatusName" DataValueField="StatusID">
                                            <asp:ListItem Value="99">[เลือกสถานะ]</asp:ListItem>
                                        </asp:DropDownList>

                                         <asp:Label ID="Label31" runat="server" Text="Percent(%)"></asp:Label>

                                            <asp:DropDownList ID="ddPercent" runat="server" CssClass="jamp">
                                                <asp:ListItem Value="0">เลือก</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                        </asp:DropDownList>
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1" valign="top">
                                        เหตุผลการโทร</td>
                                    <td class="style4">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate >
                                        <asp:DropDownList ID="ddSubStatus" runat="server" CssClass="jamp" 
                                            DataSourceID="SqlSubStatus" DataTextField="SubStatusName" 
                                            DataValueField="SubStatusID">
                                        </asp:DropDownList>

                                        <asp:Panel ID="Panel1" runat="server" style="margin-top: 7px">
                                        นัดเวลา : <asp:TextBox ID="txtAppoint" runat="server" Width="80px"></asp:TextBox>
                                
                     
                                            <asp:MaskedEditExtender ID="txtAppoint_MaskedEditExtender" runat="server" 
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtAppoint">
                                            </asp:MaskedEditExtender>
                                            <asp:CalendarExtender ID="txtAppoint_CalendarExtender" runat="server" 
                                                Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtAppoint">
                                            </asp:CalendarExtender>
                                            
                                            -<asp:TextBox ID="txtHour" runat="server" MaxLength="2" Width="30px"></asp:TextBox>
                                            :<asp:TextBox ID="txtMin" runat="server" MaxLength="2" Width="30px"></asp:TextBox>
                                
                     
                                        </asp:Panel>
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1" valign="top">
                                        หมายเหตุการโทร</td>
                                    <td class="style4">
                                        <asp:FormView ID="frmComments" runat="server" DataSourceID="SqlCustomer" 
                                            Width="90%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtComments" runat="server" Font-Names="Courier New" 
                                                    Font-Size="10pt" Height="100px" 
                                                    onkeypress="return textboxMultilineMaxNumber(this,200,event)" 
                                                    Text='<%# Bind("Comments") %>' TextMode="MultiLine" Width="100%"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:FormView>
                                    </td>
                                </tr>
                                  <tr bgcolor="#D5FFD5">
                                    <td><asp:Image ID="Image2" runat="server" ImageUrl="~/images/LINE_logo.png" >
                                    </asp:Image><asp:Label runat="server" ForeColor="GREEN"  Text="(LINE ลูกค้า)"></asp:Label></td>
                                    <td>
                                     <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate >
                                     <asp:FormView ID="formviewLine" runat="server"  DataSourceID="SqlDataSourceLineID"   >
                                    <EmptyDataTemplate>
                                        <table>
                                            <tr>   
                                                    <td align="left">
                                                     <asp:RadioButtonList ID="RBSelectConditionLINE" runat="server" Width="150px"  AutoPostBack="True"  onselectedindexchanged="RBSelectConditionLINE_SelectedIndexChanged1"  >
                                                     <asp:ListItem Value="1" >หมายเลขโทรศัพท์</asp:ListItem>
                                                     <asp:ListItem Value="2" >ID</asp:ListItem>
                                                     <asp:ListItem Value="3" >ไม่มี</asp:ListItem>
                                                     </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLINEID" runat="server"></asp:TextBox>
                                                    <asp:TextBox ID="TextBox2" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="TextBox6" runat="server" Visible="false"></asp:TextBox>
                                                 </td>
                                                 </tr>
                                                 </table>
                                            </EmptyDataTemplate>
                                        <ItemTemplate>
                                        <table>
                                            <tr>
                                                      <td align="left">
                                                     <asp:RadioButtonList ID="RBSelectConditionLINE" runat="server" Width="150px"  
                                                              SelectedValue='<%# Bind("FlagLINE") %>' 
                                                              onselectedindexchanged="RBSelectConditionLINE_SelectedIndexChanged"   AutoPostBack="True" >
                                                     <asp:ListItem Value="1" >หมายเลขโทรศัพท์</asp:ListItem>
                                                     <asp:ListItem Value="2" >ID</asp:ListItem>
                                                     <asp:ListItem Value="3" >ไม่มี</asp:ListItem>
                                                     </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLINEID" runat="server"  Text='<%# Bind("LINEID") %>'></asp:TextBox>
                                                    <asp:TextBox ID="TextBox2" runat="server" Visible="false"></asp:TextBox>
                                                    <br/><br/><br/>
                                                 </td>
                                                        </tr>
                                                 </table>
                                                 </ItemTemplate>
                                                </asp:FormView>
                                    </ContentTemplate></asp:UpdatePanel></td>
                                </tr>




                                <tr>
                                    <td style="border:solid ; border-color:coral"><b>ความสนใจโปรเจคได้เงิน</b></td>
                                    <td>
                                        <asp:RadioButtonList ID="RdPollDaiNgern" runat="server" Width="91px" >
                                              <asp:ListItem Value="1" >&nbsp;&nbsp;&nbsp;&nbsp;สนใจ</asp:ListItem>
                                              <asp:ListItem Value="2" >ไม่สนใจ</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1" valign="top">
                                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                     <ContentTemplate >
                                      <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="กดโทร" 
                                            ImageUrl="~/images/Icon/Dial.png" Width="40px" />
                                     </ContentTemplate>
                                     </asp:UpdatePanel>
                                    </td>
                                    <td class="style4" valign="top">
                                        <asp:DropDownList ID="ddCall" runat="server" CssClass="jamp">
                                            <asp:ListItem Value="1">บ้าน</asp:ListItem>
                                            <asp:ListItem Value="2">ที่ทำงาน</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="3">มือถือ</asp:ListItem>
                                            <asp:ListItem Value="4">อื่นๆ1</asp:ListItem>
                                            <asp:ListItem Value="5">อื่นๆ2</asp:ListItem>
                                        </asp:DropDownList>
                                        เลือกเบอร์สำหรับโทร
                                         <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate >
                                         <iframe src = "<%= strsrc %>" frameborder="0" height="40" width="100%"></iframe>
                                         <iframe id="Iframe3" style="display:none ;" src="<%=TelAjax %>"></iframe>   
                                        </ContentTemplate>
                                             <Triggers>
                                                 <asp:AsyncPostBackTrigger ControlID="ImageButton1" EventName="Click" />
                                                 <asp:AsyncPostBackTrigger ControlID="ddStatus" 
                                                     EventName="SelectedIndexChanged" />
                                             </Triggers>
                                        </asp:UpdatePanel>
                                        </td>
                                </tr>
                            </table>
                            <div>
                                <asp:Button ID="WebImageButton1" runat="server" Text="บันทึก" />
                                <asp:Button ID="WebImageButton2" runat="server" Text="กลับ" />
                                <asp:Button ID="WebImageButton3" runat="server" Text="Refresh" />
                                <asp:Button ID="WebImageButton4" runat="server" Text="ใบเตือน" />
                                <asp:Button ID="WebImageButton5" runat="server" Text="ข้อมูลAppเดิม" /> 
								<asp:Button ID="btnScript" runat="server" Text="Scriptปิดการขาย" />
                            
                            </div>
                        </td>
                    </tr>
                    </table>               
            </td>
            <td valign="top">
                 <table style="width: 100%;" cellspacing="0">
                    <tr>
                        <td style="text-align: center; font-weight: bold; background-color: #CCFDAC;">
                            ข้อมูลรถยนต์</td>
                    </tr>
                    <tr>
                        <td bgcolor="#E2FEA9">
                            <asp:FormView ID="frmCar" runat="server" DataSourceID="SqlCustomer" 
                                Width="100%" DataKeyNames="IdCar,CarBrand,CurStatus,ProCode,Idcar_old">
                                <ItemTemplate>
                                    <table style="width:100%;">
                                        <tr>
                                            <td class="style1">
                                                เลขทะเบียน</td>
                                            <td>
                                                <asp:Label ID="Label20" runat="server" Text='<%# Eval("CarID") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                วันหมดประกัน</td>
                                            <td>
                                                <asp:TextBox ID="txtCarBuyDate" runat="server" 
                                                    Text='<%# Bind("CarBuyDate", "{0:d}") %>' Width="80px"></asp:TextBox>
                                                <asp:MaskedEditExtender ID="txtCarBuyDate_MaskedEditExtender" runat="server" 
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtCarBuyDate">
                                                </asp:MaskedEditExtender>
                                                <asp:CalendarExtender ID="txtCarBuyDate_CalendarExtender" runat="server" 
                                                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtCarBuyDate">
                                                </asp:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                ปีรุ่นรถ</td>
                                            <td>
                                                <asp:Label ID="Label21" runat="server" Text='<%# Eval("CarYear") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                ขนาดเครื่อง</td>
                                            <td>
                                                <asp:Label ID="Label22" runat="server" Text='<%# Eval("CarSize") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                ยี้ห้อ</td>
                                            <td>
                                                <asp:Label ID="Label23" runat="server" Text='<%# Eval("CarBrand") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                รุ่น</td>
                                            <td>
                                                <asp:Label ID="Label24" runat="server" Text='<%# Eval("CarSeries") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                หมายเลขเครื่อง</td>
                                            <td>
                                                <asp:Label ID="Label25" runat="server" Text='<%# Eval("CarNo") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                หมายเลขตัวถัง</td>
                                            <td>
                                                <asp:Label ID="Label26" runat="server" Text='<%# Eval("CarBoxNo") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                </td>
                                            <td>
                                                <asp:Button ID="WebImageButton1" runat="server" Text="บันทึกวันหมดอายุ" 
                                                    onclick="WebImageButton1_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:FormView>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#99CCFF" style="text-align: center; font-weight: 700">
                            เบอร์โทรติดต่อ</td>
                    </tr>
                    <tr>
                        <td bgcolor="#CBEDFE" style="text-align: center; font-weight: 700">
                            <asp:FormView ID="frmTel" runat="server" DataSourceID="SqlCustomer" 
                                Width="100%" 
                                DataKeyNames="Tel2,OTel2,Mobile2,OthTel2,OthTel3,TelExt,OTelExt,OthTel1,OthTel1Ext,OthTel3Ext">
                                <ItemTemplate>
                                    <table style="width:100%;">
                                        <tr>
                                            <td class="style2">
                                                บ้าน</td>
                                            <td class="style3">
                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("Tel") %>'></asp:Label>
                                                ต่อ
                                                <asp:Label ID="Label17" runat="server" Text='<%# Eval("TelExt") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">
                                                ที่ทำงาน</td>
                                            <td class="style3">
                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("OTel") %>'></asp:Label>
                                                ต่อ
                                                <asp:Label ID="Label18" runat="server" Text='<%# Eval("OTelExt") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">
                                                มือถือ</td>
                                            <td class="style3">
                                                <asp:Label ID="Label14" runat="server" Text='<%# Eval("Mobile") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">
                                                อื่นๆ1</td>
                                            <td class="style3">
                                                <asp:Label ID="Label15" runat="server" Text='<%# Eval("OthTel1") %>'></asp:Label>
                                                ต่อ<asp:Label ID="Label21" runat="server" Text='<%# Eval("OthTel1Ext") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">
                                                อื่นๆ2</td>
                                            <td class="style3">
                                                <asp:Label ID="Label20" runat="server" Text='<%# Eval("OthTel3") %>'></asp:Label>
                                                ต่อ<asp:Label ID="Label22" runat="server" Text='<%# Eval("OthTel3Ext") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">
                                                Fax</td>
                                            <td class="style3">
                                                <asp:Label ID="Label16" runat="server" Text='<%# Eval("Fax") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">
                                                Email</td>
                                            <td class="style3">
                                                <asp:Label ID="Label19" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">
                                                </td>
                                            <td class="style3">
                                                <asp:Button ID="WebImageButton1" runat="server" Text="แก้ไข" ommandName="Edit" 
                                                    CommandName="Edit" />
                                                
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <EditItemTemplate >
                                <table style="width:100%;">
                                        <tr>
                                            <td class="style2">
                                                บ้าน</td>
                                            <td class="style5">
                                                <asp:TextBox ID="txtTel" runat="server" Width="100px"></asp:TextBox>
                                                <asp:MaskedEditExtender ID="txtTel_MaskedEditExtender" runat="server" 
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                    Mask="99-9999999" TargetControlID="txtTel">
                                                </asp:MaskedEditExtender>
                                                ต่อ<asp:TextBox ID="txtTelExt" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td class="style3">
                                                <asp:ImageButton ID="ImageButton2" runat="server" 
                                                    ImageUrl="~/images/Icon/Save.png" onclick="ImageButton2_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">
                                                ที่ทำงาน</td>
                                            <td class="style5">
                                                <asp:TextBox ID="txtOTel" runat="server" Width="100px"></asp:TextBox>
                                                <asp:MaskedEditExtender ID="txtOTel_MaskedEditExtender" runat="server" 
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                    Mask="99-9999999" TargetControlID="txtOTel">
                                                </asp:MaskedEditExtender>
                                                ต่อ<asp:TextBox ID="txtOTelExt" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td class="style3">
                                                <asp:ImageButton ID="ImageButton3" runat="server" 
                                                    ImageUrl="~/images/Icon/Save.png" onclick="ImageButton3_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">
                                                มือถือ</td>
                                            <td class="style5">
                                                <asp:TextBox ID="txtMobile" runat="server" Width="100px"></asp:TextBox>
                                                <asp:MaskedEditExtender ID="txtMobile_MaskedEditExtender" runat="server" 
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                    Mask="99-99999999" TargetControlID="txtMobile">
                                                </asp:MaskedEditExtender>
                                            </td>
                                            <td class="style3">
                                                <asp:ImageButton ID="ImageButton4" runat="server" 
                                                    ImageUrl="~/images/Icon/Save.png" onclick="ImageButton4_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">
                                                อื่นๆ1</td>
                                            <td class="style5">
                                                <asp:TextBox ID="txtOther" runat="server" Width="100px"></asp:TextBox>
                                                <asp:MaskedEditExtender ID="txtOther_MaskedEditExtender" runat="server" 
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                    Mask="99-99999999" TargetControlID="txtOther">
                                                </asp:MaskedEditExtender>
                                                ต่อ<asp:TextBox ID="txtOtherExt" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td class="style3">
                                                <asp:ImageButton ID="ImageButton5" runat="server" 
                                                    ImageUrl="~/images/Icon/Save.png" onclick="ImageButton5_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">
                                                อื่นๆ2</td>
                                            <td class="style5">
                                                <asp:TextBox ID="txtOther3" runat="server" Width="100px"></asp:TextBox>
                                                <asp:MaskedEditExtender ID="txtOther3_MaskedEditExtender" runat="server" 
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                    Mask="99-99999999" TargetControlID="txtOther3">
                                                </asp:MaskedEditExtender>
                                                ต่อ<asp:TextBox ID="txtOtherExt3" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td class="style3">
                                                <asp:ImageButton ID="ImageButton8" runat="server" 
                                                    ImageUrl="~/images/Icon/Save.png" onclick="ImageButton8_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">
                                                Fax</td>
                                            <td class="style5">
                                                <asp:TextBox ID="txtFax" runat="server" Width="100px"></asp:TextBox>
                                                <asp:MaskedEditExtender ID="txtFax_MaskedEditExtender" runat="server" 
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                    Mask="99-9999999" TargetControlID="txtFax">
                                                </asp:MaskedEditExtender>
                                            </td>
                                            <td class="style3">
                                                <asp:ImageButton ID="ImageButton6" runat="server" 
                                                    ImageUrl="~/images/Icon/Save.png" onclick="ImageButton6_Click" 
                                                    style="width: 24px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">
                                                Email</td>
                                            <td class="style5">
                                                <asp:TextBox ID="txtEmail" runat="server" Width="150px"></asp:TextBox>
                                            </td>
                                            <td class="style3">
                                                <asp:ImageButton ID="ImageButton7" runat="server" 
                                                    ImageUrl="~/images/Icon/Save.png" onclick="ImageButton7_Click" 
                                                    style="width: 24px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">
                                                </td>
                                            <td class="style5">
                                                <asp:Button ID="WebImageButton2" runat="server" Text="ยกเลิก"  CommandName="Cancel" />
                                            </td>
                                            <td class="style3">
                                                </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>
                            </asp:FormView>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#003366" 
                            style="text-align: center; font-weight: 700; color: #FFFFFF; font-size: 20px;">
                            เลือกซื้อประกันภัย</td>
                    </tr>
                    <tr>
                        <td >
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate >
                            <table style=" color: #FFFFFF; font-size: 14px; font-weight: bold;" 
                                cellspacing="0">
                                <tr>
                                   
                                    <td style="background-image:url('../../../images/Icon/Button-Blank-Blue-icon.png');height: 96px;width :96px;background-repeat:no-repeat; text-align: center;">
                                        <asp:LinkButton ID="LinkButton1" runat="server" Font-Underline="False" 
                                            ForeColor="White">ชั้น1</asp:LinkButton>
                                    </td>
                                      <td style="background-image:url('../../../images/Icon/Button-Blank-Blue-icon.png');height: 96px;width :96px;background-repeat:no-repeat; text-align: center;">
                                        <asp:LinkButton ID="LinkButton6" runat="server" Font-Underline="False" 
                                            ForeColor="White">ชั้น1+</asp:LinkButton>
                                    </td>
                                      
                                    
                                    <td style="background-image:url('../../../images/Icon/Button-Blank-Blue-icon.png');height: 96px;width :96px;background-repeat:no-repeat; text-align: center;">
                                        <asp:LinkButton ID="LinkButton2" runat="server" Font-Underline="False" 
                                            ForeColor="White">ชั้น2+</asp:LinkButton>
                                    </td>
                                    
                                      
                                    
                                    <td style="background-image:url('../../../images/Icon/Button-Blank-Blue-icon.png');height: 96px;width :96px;background-repeat:no-repeat; text-align: center;">
                                        <asp:LinkButton ID="LinkButton3" runat="server" Font-Underline="False" 
                                            ForeColor="White">ชั้น3+</asp:LinkButton>
                                    </td>
                                    
                                      
                                    
                                    <td style="background-image:url('../../../images/Icon/Button-Blank-Blue-icon.png');height: 96px;width :96px;background-repeat:no-repeat; text-align: center;">
                                        <asp:LinkButton ID="LinkButton4" runat="server" Font-Underline="False" 
                                            ForeColor="White">ชั้น3</asp:LinkButton>
                                    </td>
                                    
                                      
                                    
                                    <td style="background-image:url('../../../images/Icon/Button-Blank-Blue-icon.png');height: 96px;width :96px;background-repeat:no-repeat; text-align: center;">
                                        <asp:LinkButton ID="LinkButton5" runat="server" Font-Underline="False" 
                                            ForeColor="White">ใบเสนอราคา</asp:LinkButton>
                                    </td>
                                    
                                      
                                    
                                </tr>
                                </table>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                            
                                <div>
                                <asp:FormView ID="frmApp" runat="server" DataKeyNames="AppID,ProDuctID,Typeprovalue,TypeTsr,ProtectDate,CardNo1,IDCard,IDCard_Carpet,StatusProValue,StatusCarPet" 
                                    DataSourceID="SqlApp">
                                    <ItemTemplate>
                                        <table class="art-article" style="width:100%;">
                                            <tr>
                                                <td bgcolor="#99CCFF" style="font-weight: bold">
                                                    รายการ ประกันภัย</td>
                                                <td bgcolor="#99CCFF" style="font-weight: bold">
                                                    ทุนประกัน</td>
                                                <td bgcolor="#99CCFF" style="font-weight: bold">
                                                    เบี้ยประกัน</td>
                                                <td bgcolor="#99CCFF" style="font-weight: bold">
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label27" runat="server" Text='<%# Eval("ProTypeName") %>'></asp:Label>
                                                    +<asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("IsCarpet") %>' 
                                                        Text="พรบ." />
                                                    <asp:Label ID="Label30" runat="server" Text='<%# Eval("TypeName") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label28" runat="server" Text='<%# Eval("ProPrice", "{0:N}") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label29" runat="server" Text='<%# Eval("ProValue", "{0:N}") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Button ID="Button1" runat="server" Text="ตรวจสอบประกัน" 
                                                        onclick="Button1_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                 </asp:FormView>
                                </div>
                            </td>
                    </tr>
                    <tr>
                        <td >
                            <div id="TmpRenew"  runat="server" >
                                <table cellspacing="1" bgcolor="#EAF4FF"  Width="100%" >
                                     <tr>
                                        <td colspan="2" bgcolor="#FF6666"   style="text-align: center; font-weight: 700; color: #black; font-size: 20px;">ข้อมูลตามใบเตือน</td>
                                      
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="ChkCMI" runat="server" text="พร้อม พรบ."/>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnRenew" runat="server" Text="สั่งซื้อตามใบเดือน" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        
                        </td>

                    </tr>
                    </table>
            </td>
        </tr>
        </table>
</div>
<div>
    <table style="width: 100%;">
    <tr>
    <td>
   
    </td>
    <tr>
        <td>
           
            <asp:FormView ID="FormViewDetailRenew" runat="server" 
                DataSourceID="SqlDetailRenew" Width="100%" DataKeyNames="pkgid,Typeid,ProValue,ProPrice" >
                <ItemTemplate>
                    <table width="100%">
                    <tr>
                        <td>
                        <asp:Label ID="Label3" runat="server" Text="ข้อมูลปีต่อ" Font-Bold="True" 
                            Font-Size="Small" BackColor="#FF6666"></asp:Label>
                        <asp:TextBox ID="txtdetailrenew" runat="server" Text='<%# bind("detail") %>' 
                        Width="90%" TextMode="MultiLine" Height="30px"></asp:TextBox>
                        </td>
                     </tr>
                     <table>
                </ItemTemplate>
            </asp:FormView>
            
        </td>
    </tr>
	<tr>
        <td>
            <div id="dialog" style="display: none; width: 1100px;" >    
            <b>ดิฉัน/นาย .......... ใบอนุญาติเลขที่ ................... ขออนุญาตบันทึกเสียงสนทนาเพื่อแจ้งความคุ้มครองลูกค้าเข้าบริษัทประกันภัย ตามรายละเอียดคุ้มครองสรุปดังนี้ค่ะ/ครับ</b>
            <table width="100%" >
                <tr><td>1.ลูกค้ายืนยันการแจ้งต่อประกันผ่าน บริษัท เอเอสเอ็น โบรกเกอร์ จำกัด มหาชน เลขที่ รับอนุญาตอ ว000027/2548</td></tr>
                <tr><td>
                <asp:FormView ID="frmCusNameConfirm" runat="server" 
                 Width="100%" >
                     <ItemTemplate>
                        <table style="width:100%;">
                        <tr>                        
                        <td>2.ในนามผู้เอาประกันภัย                           
                            <asp:TextBox ID="txtInit" runat="server" Text='<%# Bind("InitIDTH") %>'
                                Width="500px" MaxLength="150" ForeColor="Red" BorderStyle="None"></asp:TextBox>                           
                        </td>
                        </tr>
                        </table>
                      </ItemTemplate>
            </asp:FormView>
          </td>
          </tr>
          <tr><td><asp:FormView ID="frmAddrConfirm" runat="server" Width="100%" >
                                    <ItemTemplate>
                                        <table style="width:100%;">                                            
                                            <tr>
                                                <td>
                                                  ที่อยู่ในการจัดส่ง เลขที่
                                                    <asp:TextBox ID="txtAddr" runat="server" Text='<%# Bind("Address") %>' 
                                                        Width="50px" MaxLength="150" BorderStyle="None" ForeColor="Red"></asp:TextBox>
                                                    &nbsp;หมู่&nbsp;<asp:TextBox ID="txtMoo" runat="server" Text='<%# Bind("Moo") %>' 
                                                        Width="50px" MaxLength="10" BorderStyle="None"  ForeColor="Red"></asp:TextBox>
                                                หมู่บ้าน/อาคาร/ชั้น/ห้อง
                                               <asp:TextBox ID="txtVillege" runat="server" Text='<%# Bind("Villege") %>' 
                                                  BorderStyle="None"  ForeColor="Red">
                                               </asp:TextBox>
                                                ซอย
                                               <asp:TextBox ID="txtSoi" runat="server" Text='<%# Bind("Soi") %>'
                                                        MaxLength="50" BorderStyle="None" ForeColor="Red"></asp:TextBox>
                                                ถนน
                                               <asp:TextBox ID="txtRoad" runat="server" Text='<%# Bind("Road") %>' 
                                                         MaxLength="50" BorderStyle="None" ForeColor="Red"></asp:TextBox>
                                               </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                ตำบล/แขวง  
                                                <asp:TextBox ID="ddSubDist" runat="server" Text='<%# Bind("SubDist") %>' 
                                                        MaxLength="50" BorderStyle="None" ForeColor="Red"></asp:TextBox>
                                                อำเภอ/เขต
                                                <asp:TextBox ID="ddDist" runat="server" Text='<%# Bind("Dist") %>' 
                                                        MaxLength="50" BorderStyle="None" ForeColor="Red"></asp:TextBox>
                                               จังหวัด
                                                <asp:TextBox ID="ddProvince" runat="server" Text='<%# Bind("ProvinceTH") %>' 
                                                        MaxLength="50" BorderStyle="None" ForeColor="Red"></asp:TextBox>  
                                                รหัสไปรษณีย์
                                                 <asp:TextBox ID="ddZipcode" runat="server" Text='<%# Bind("Zip") %>' 
                                                        MaxLength="50" BorderStyle="None" ForeColor="Red"></asp:TextBox>                                                  
                                                    </td>
                                              </tr>                                         
                                        </table>
                                    </ItemTemplate>
                                </asp:FormView>                 
                </td></tr>  

        <tr><td>3.เป็นรถ ยี่ห้อ<b><asp:TextBox ID="txtCarBrand" runat="server"  BorderStyle="None" ForeColor="Red"  Width="100px" ></asp:TextBox></b>
                  รุ่น<b><asp:TextBox ID="txtCarSeries" runat="server"  BorderStyle="None" ForeColor="Red"  Width="300px" ></asp:TextBox></b>
                  ขนาดเครื่องยนต์<b><asp:TextBox ID="txtCarSize" runat="server"  BorderStyle="None" ForeColor="Red"></asp:TextBox></b>
                  ทะเบียน<b><asp:TextBox ID="txtCarID" runat="server"  BorderStyle="None" ForeColor="Red"></asp:TextBox></b></td></tr>
         <tr><td>4.บริษัทประกันภัยที่ตกลงทำประกัน คือ<b><asp:TextBox ID="txtProTypeName" runat="server" BorderStyle="None" ForeColor="Red"></asp:TextBox></b>
         ประเภท<b><asp:TextBox ID="txtTypeName" runat="server"  BorderStyle="None" ForeColor="Red"></asp:TextBox></b></td></tr>
         <tr><td>5.โดยเริ่มคุ้มครองวันที่(ภาคสมัครใจ)<b><asp:TextBox ID="txtProtectDate" runat="server"  BorderStyle="None" ForeColor="Red"></asp:TextBox></b>
                 <asp:Label ID="lblProtectDateCarprt" runat="server" ></asp:Label>
                 <b><asp:TextBox ID="txtProtectDateCarprt" runat="server"  BorderStyle="None" ForeColor="Red"></asp:TextBox></b>
         </td></tr>
         <tr><td>6.ทุนประกันภัยรถยนต์ สูญหายไฟไหม้ อยู่ที่ <b><asp:TextBox ID="txtCar_Fire" runat="server"  BorderStyle="None" ForeColor="Red"></asp:TextBox></b>(เฉพาะ ชั้น 1 และ 2+)</td></tr>
         <tr><td>7.เป็นประกันภัยแบบ<b><asp:TextBox ID="txtIsFixIn" runat="server"  BorderStyle="None" ForeColor="Red"></asp:TextBox></b></td></tr>
         <tr><td>8.คุ้มครองอุปกรณ์ตกแต่งตามมาตรฐานโรงงาน ไม่เกิน 20,000 บาท</td></tr>
         <tr><td>9.
          <asp:Label ID="lblCarDriverNo" runat="server" ></asp:Label>
         <b><asp:TextBox ID="txtCarDriverNo" runat="server"  BorderStyle="None" ForeColor="Red" ></asp:TextBox></b>
          <asp:Label ID="Label13" runat="server" ></asp:Label>
        
         </td></tr>
         <tr><td>10.เบี้ยประกันภัย<b><asp:TextBox ID="txtProValue" runat="server" BorderStyle="None" ForeColor="Red" ></asp:TextBox></b>
         <asp:Label ID="lblCarpet" runat="server" ></asp:Label>
         </td></tr>
         <tr><td>11.โดยชำระเป็น
               <asp:GridView ID="DGVPay" runat="server" AutoGenerateColumns="False" 
               Width="100%">
                         <RowStyle HorizontalAlign="Center" />
                                              <Columns>
                                                  <asp:TemplateField HeaderText="งวดที่">
                                                      <ItemTemplate>
                                                          <asp:Label ID="Label2" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                      </ItemTemplate>                                                     
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="วันที่ชำระ">
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtAppoint" runat="server" ReadOnly="True" 
                                                              Text='<%# Bind("AppointDate", "{0:dd}/{0:MM}/{0:yyyy}") %>' Width="90px"></asp:TextBox>                                                          
                                                      </ItemTemplate>
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("PayDate") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="จำนวนเงิน">
                                                      <ItemTemplate>
                                                          <asp:Label ID="Label3" runat="server" ForeColor="#00CC00" 
                                                              Text='<%# Bind("ProValue","{0:N}") %>'></asp:Label>
                                                      </ItemTemplate>
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("ProValue") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="ค่าธรรมเนียม">
                                                      <ItemTemplate>
                                                          <asp:Label ID="Label4" runat="server" ForeColor="#00CC00" 
                                                              Text='<%# Bind("ProVat") %>'></asp:Label>
                                                      </ItemTemplate>
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("ProVat") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="จำนวนเงินที่ชำระ">
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTotalPay" runat="server" Font-Bold="True" ForeColor="Red" 
                                                              Text='<%# Bind("TotalPay","{0:N}") %>'></asp:Label>
                                                      </ItemTemplate>
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("TotalPay") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:BoundField DataField="TypeName" HeaderText="การชำระ" />
                                              </Columns>
                                              <HeaderStyle BackColor="#B9D0DD" Font-Size="10pt" ForeColor="Black" 
                                                  Height="30px" />
                                          </asp:GridView>
            
         งวดแรกขออนุญาติตัดบัตรเครดิต วันที่ <asp:TextBox ID="txtPayDate" runat="server" ForeColor="Red"  BorderStyle="None"></asp:TextBox>
         </td></tr>
           <tr><td>           
               <asp:GridView ID="DGVAppCard" runat="server" AutoGenerateColumns="False" 
                Width="100%"  EmptyDataText="ไม่พบข้อมูลบัตรเครดิต">
                <RowStyle HorizontalAlign="Center" />
                <Columns> 
                    <asp:TemplateField HeaderText="ลำดับที่" SortExpression="CardRun">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label></ItemTemplate><EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CardRun") %>'></asp:TextBox></EditItemTemplate></asp:TemplateField><asp:BoundField DataField="Cardname" HeaderText="ชื่อผู้ถือบัตร" 
                        SortExpression="Cardname" />
                    <asp:BoundField DataField="CardNo1" HeaderText="เลขบัตรเครดิต" 
                        SortExpression="CardNo1" />
                    <asp:BoundField DataField="BankName" HeaderText="ธนาคาร" 
                        SortExpression="BankName" />
                    <asp:BoundField DataField="CardExp" HeaderText="วันหมดอายุ" 
                        SortExpression="CardExp" />
                </Columns>
                <HeaderStyle Font-Size="10pt" ForeColor="Black" 
                    Height="30px" BackColor="#B9D0DD" />
            </asp:GridView>          
           </td></tr>
         <tr><td>12.ก่อนหมดประกันจะมี เจ้าหน้าที่นัด ถ่ายรูปรถ (เฉพาะชั้น 1)</td></tr>
         <tr><td>13.ประกันจะคุ้มครองตัวรถ ในกรณีรถชนรถ และระบุคู่กรณีได้ (เฉพาะ ชั้น 2+ และ 3+)</td></tr>
         <tr><td>14.เอกสารที่ลูกค้าต้องจัดส่งมายังบริษัท ได้แก่ …........ โดยจะมี เจ้าหน้าที่ติดต่อไปหาท่านอีกครั้งเพื่อขอเอกสาร</td></tr>
         <tr><td>15.หากมีข้อสงสัยประการใด กรุณา ติดต่อมาที่ ศูนย์บริการลูกค้า  เอเอสเอ็น โบรเกอร์  02-6191717 เวลา 8.00 – 18.00 น.ทุกวันจันทร์ ถึง ศุกร์ ยกเว้นวันนักขัตฤกษ์</td></tr></br>
         <tr><td><b>ขออนุญาติลูกค้าแจ้งกำหนดวันชำระเงินงวดแรก เพื่อยืนยันการสั่งซื้อกรมธรรม์ ชำระเงินวันที่เท่าไหร่คะ ............................. สุดท้ายขอให้ลูกค้ามีความสุข มีความปลอดภัยในการขับขี่ทุกเส้นทางค่ะ					
(กรณีลูกค้าชำระ 6 งวด ระบุวันชำระเงินไม่เกิน 7 วัน เริ่มนับตั้งแต่วันบันทึกสนทนา)</b></td></tr>
<tr><td><b><center></center></b></td></tr>
<tr><td><b><center>Script Confirm PA TNI</center></b></td></tr>
<tr><td><b>ดิฉัน/ผม ขอนุญาตบันทึกเสียงสนทนาเพื่อคอนเฟิร์ม ข้อมูลสำหรับการทำประกัน PA สุขใจ ของธนชาต ประกันภัย  ดังนี้นะ คะ/ครับ</b></td></tr>
<tr><td>
<asp:FormView ID="FormView1" runat="server" Width="100%" >
                                    <ItemTemplate>
                                        <table style="width:100%;">                                            
                                            <tr><td>ผู้เอาประกันภัย <asp:TextBox ID="txtInit" runat="server" Text='<%# Bind("name") %>'
                                             ForeColor="Red" BorderStyle="None"></asp:TextBox> </td></tr>
                                            <tr><td>เลขประจำตัวประชาชน  <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("IDCard") %>'
                                             ForeColor="Red" BorderStyle="None"></asp:TextBox> </td></tr>
                                            <tr><td>เกิดวันที่ <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("BirthDate") %>'
                                             ForeColor="Red" BorderStyle="None"></asp:TextBox>ปัจจุบันอายุ <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("Age") %>'
                                             ForeColor="Red" BorderStyle="None"></asp:TextBox>ปี</td></tr>
                                            <tr><td>ตกลงทำประกันภัย PA สุขใจ ของธนาชาตประกันภัย เลือกความคุ้มครอง แผน<asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("PackageName") %>' ForeColor="Red" BorderStyle="None"></asp:TextBox>ทุนประกัน<asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("GeneralCasualty_PhysicalAssault") %>' ForeColor="Red" BorderStyle="None"></asp:TextBox>บาท </td></tr>
                                             <tr><td>เบี้ยประกันภัย <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("NetPremium") %>' ForeColor="Red" BorderStyle="None"></asp:TextBox> บาท</td></tr>
                                            <tr><td>กรมธรรม์จะมีผลคุ้มครองพร้อมกันกับประกันภัยรถยนต์  เริ่มวันที่ <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("ProtectDate") %>' ForeColor="Red" BorderStyle="None"></asp:TextBox>ค่ะ/ครับ</td></tr>
                                            <tr><td><b>รบกวน ขอทราบข้อมูลส่วนตัวที่เกี่ยวกับการทำประกันด้วยค่ะ/ครับ</b></td></tr>
                                            <tr><td>ปัจจุบันประกอบอาชีพ<asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("OccNAME") %>' ForeColor="Red" BorderStyle="None"></asp:TextBox></td></tr>
                                                                                   
                                        </table>
                                    </ItemTemplate>
					 <EmptyDataTemplate>
                                     <table style="width:100%;">                                            
                                            <tr><td>ผู้เอาประกันภัย…........555.................</td></tr>
                                            <tr><td>เลขประจำตัวประชาชน….........................</td></tr>
                                            <tr><td>เกิดวันที่ ….........................ปี….........................</td></tr>
                                            <tr><td>ตกลงทำประกันภัย PA สุขใจ ของธนาชาตประกันภัย เลือกความคุ้มครอง แผน….........................ทุนประกัน….........................บาท </td></tr>
                                             <tr><td>เบี้ยประกันภัย …......................... บาท</td></tr>
                                            <tr><td>กรมธรรม์จะมีผลคุ้มครองพร้อมกันกับประกันภัยรถยนต์  เริ่มวันที่ ….........................ค่ะ/ครับ</td></tr>
                                            <tr><td><b>รบกวน ขอทราบข้อมูลส่วนตัวที่เกี่ยวกับการทำประกันด้วยค่ะ/ครับ</b></td></tr>
                                            <tr><td>ปัจจุบันประกอบอาชีพ….........................</td></tr>
                                        </table>
                                    </EmptyDataTemplate>

                                </asp:FormView>  </td></tr>
<tr><td>ในช่วง 2 ปีที่ผ่านมา เคยได้รับบาดเจ็บจากอุบัติเหตุถึงขั้นเข้ารักษาตัวในโรงพยาบาลบ้างไหม คะ/ครับ</td></tr>
<tr><td>ไม่เคย/เคย ถ้าเคย (ถ้าเคยโปรดให้ลูกค้าแจ้ง) ….........................</td></tr>
<tr><td>วัน/เดือน/ปี ที่บาดเจ็บ.............................ลักษณะการบาดเจ็บ..........................................................................................</td></tr>
<tr><td>ผลการรักษา.....................................................แพทย์/ รพ.ที่รักษา.....................................................................................</td></tr>
<tr><td>เป็นหรือเคยได้รับการรักษาโรคต่อไปนี้บ้างหรือเปล่า คะ/ครับ (อ่านให้ลูกค้าฟังให้ครบทั้ง 7 ชนิด)</td></tr>
<tr><td>โรคลมชัก, โรคหัวใจ, ความดันโลหิตสูง, โรคเบาหวาน, โรคกระดูกและ/หรือกล้ามเนื้อ, โรคมะเร็ง, โรคเอดส์</td></tr>
<tr><td>ไม่เป็น/ไม่เคย              ถ้าเป็น หรือเคยเป็น (โปรดให้ลูกค้าแจ้งถึงโรคที่เป็นหรือเคยเป็น)</td></tr>
<tr><td>ไม่ทราบว่า มีความผิดปกติของสายตา หรือ ประสาทหูบ้างหรือไม่ คะ/ครับ</td></tr>
<tr><td>ไม่มี/มี (ถ้ามีโปรดให้ลูกค้าระบุ...............................)</td></tr>
<tr><td>ไม่ทราบว่า มีอวัยวะส่วนใดที่พิการบ้างหรือไม่ คะ/ครับ</td></tr>
<tr><td>ไม่มี/มี (ถ้ามีโปรดให้ลูกค้าแจ้งรายละเอียด) ..................................................................................)</td></tr>
<tr><td>ขอบคุณสำหรับรายละเอียดข้อมูลเกี่ยวกับการประกันที่แจ้งให้ทราบ ค่ะ/ครับ สำหรับผลประโยชน์ มอบให้กับ ทายาทตามกฎหมาย นะครับ/ค่ะ</td></tr>

    </table>
		<asp:HiddenField ID="HFAppID" runat="server" />
		<asp:HiddenField ID="HFCusname" runat="server" />
        <asp:HiddenField ID="HChkIsshow" runat="server" Value="0" />
</div>


        </td>
    </tr>
    </tr>
        <tr>
            <td bgcolor="#006699" 
                style="text-align: center; font-size: 20px; font-weight: bold; color: #FFFFFF;">
                รายการประกันภัย<asp:Image ID="Image1" runat="server" 
        ImageUrl="~/images/Icon/bullet_arrow_bottom.png" Width="25px" />
            </td>
        </tr>
        <tr>
            <td>
    <asp:Panel
        ID="Panel2" runat="server">
<asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
<ContentTemplate >
<iframe src = "<%=strProtype %>" frameborder="0" height="850" width="100%" >
        </iframe>
</ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="LinkButton2" EventName="Command" />
            <asp:AsyncPostBackTrigger ControlID="LinkButton3" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="LinkButton4" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="LinkButton5" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="LinkButton6" EventName="Click" />
    </Triggers>
        </asp:UpdatePanel>

        
        

                </asp:Panel>
    <asp:CollapsiblePanelExtender ID="Panel2_CollapsiblePanelExtender" 
        runat="server" CollapseControlID="Image1" Enabled="True" 
        ExpandControlID="Image1" TargetControlID="Panel2" 
                    CollapsedImage="~/images/Icon/bullet_arrow_bottom.png" 
                    ExpandedImage="~/images/Icon/bullet_arrow_top.png" ImageControlID="Image1">
    </asp:CollapsiblePanelExtender>
            </td>
        </tr>
       
        <tr>
            <td>
               <iframe src = "frmHistory.aspx?IdCar=<%= Request.QueryString("IdCar") %>" 
                    frameborder="0" height="300" width="100%" ></iframe></td>
        </tr>
       <tr>
            <td>
               <iframe src = "<%=strRecoving %>"
                    frameborder="0" height="300" width="100%" ></iframe></td>
        </tr>

            <tr>
            <td>
               <iframe  src = "frmHistoryApplication.aspx?IdCar=<%= Request.QueryString("IdCar") %>"
                    frameborder="0" height="300" width="100%" ></iframe></td>
        </tr></table>

</div>


<asp:SqlDataSource ID="SqlCustomer" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        
        SelectCommand="SELECT distinct a1.FNameTH
                             ,a1.LNameTH
                             ,a1.CusID
                             ,a2.IDCar
                             ,a1.Tel
                             ,a1.TelExt
                             ,a1.OTel
                             ,a1.OTelExt
                             ,a1.Mobile
                             ,a1.OthTel1
                             ,a1.OthTel1Ext
                             ,a1.OthTel2 as OthTel3
                             ,a1.OthTel2Ext as OthTel3Ext
                             ,a1.Fax
                             ,a1.Tel as Tel2
                             ,a1.OTel as OTel2
                             ,a1.Mobile as Mobile2
                             ,a1.OthTel1 as OthTel2
                             ,a2.Comments
                             ,a2.CarID
                             ,a2.CarNo
                             ,a2.CarBoxNo
                             ,a2.CarSize
                             ,a2.CarBrand
                             ,a2.CarSeries
                             ,a2.CarYear
                             ,a2.CarBuyDate
                             
                             ,a2.CurStatus
                             ,a2.CntStatus
                             ,a2.CarBuyDate
                             ,a2.AppointDate
                             ,a3.Extension
                             ,case when a4.Province is null then '[ไม่ระบุ]' else a4.Province end as Province
                             ,a1.Dist
                             ,a1.SubDist
                             ,a1.Address
                             ,a1.Moo
                             ,a1.Villege
                             ,a1.Zip
                             ,a1.Road
                             ,a1.Soi
                             ,case when a2.RefNo is null then '0' else a2.refNo end as RefNo
                             ,a3.Exten
                             ,case a2.CurStatus when 1 then 1 else 0 end IsNew 
                             ,a1.Email
                             ,case when a2.ProCode  is null  then 0  
							   when a2.ProCode = 0 then 0
							  else a5.productid end  as ProCode
                              ,isnull(a9.Idcar_old,0) as 'Idcar_old'
                       FROM [TblCustomer] a1
                              Inner Join TblCar a2 on a1.CusID = a2.CusID
                              Inner Join TblUser a3 on a2.AssignTo = a3.UserID
                              Left Join TblZipcode a4 on a1.Province = a4.Province
                              Left Join TblProductCar a5 on a2.ProCode=a5.runno
                              left join TblLogMoveAutoCC a9 on a2.IdCar=a9.idcar_new
                              Where   a2.IdCar = @IdCar" 
        UpdateCommand="UPDATE TblCustomer 
                    SET Address = @Address
                    , Villege = @Villege
                    , Moo = @Moo
                    , Soi = @Soi
                    , Road = @Road
                    , Dist = @Dist
                    , SubDist = @SubDist
                    , Province = @Province
                    , Zip = @Zip 
                    WHERE (CusID = @CusID)">
        <SelectParameters>
            <asp:QueryStringParameter Name="IdCar" QueryStringField="IdCar" />
         
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="Address" />
            <asp:Parameter Name="Villege" />
            <asp:Parameter Name="Moo" />
            <asp:Parameter Name="Soi" />
            <asp:Parameter Name="Road" />
            <asp:Parameter Name="Dist" />
            <asp:Parameter Name="Province" />
             <asp:Parameter Name="SubDist" />
            <asp:Parameter Name="Zip" />
            <asp:Parameter Name="CusID" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlStatus" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        
        SelectCommand="
        if @SupID = '5672' 
            begin
                SELECT [StatusID], [StatusName] FROM [TblStatus]  where statusID in (5,6,7,8,9)  order by StatusName
            end
        else
             begin
            if @StatusID in(6,8)
            SELECT [StatusID], [StatusName] FROM [TblStatus]  where statusID in (3,4,5,6,8,9,11,12,26,25,27) order by StatusName
            else 
            SELECT [StatusID], [StatusName] FROM [TblStatus] where statusID in (3,4,5,6,7,8,9,11,12,26,25,27) order by StatusName
            end
        " 
        UpdateCommand="UPDATE TblPendingInCom SET FlagPending = 1 WHERE (IdCar = @IdCar)">
        <SelectParameters>
            <asp:Parameter Name="StatusID" />
            <asp:CookieParameter CookieName="SupID" Name="SupID" DefaultValue="0"/>
        </SelectParameters>
        <UpdateParameters>
            <asp:QueryStringParameter Name="IdCar" QueryStringField = "IdCar" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlProvince" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT Distinct [Province] FROM [TblZipcode] Order by Province "></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlSubStatus" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="Select a1.SubStatusID,a1.SubStatusName
                         from TblSubStatus a1 
                         Where a1.ValidSubStatus = 1 And a1.StatusID = @StatusID">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddStatus" DefaultValue="0" Name="StatusID" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlApp" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand=" Select Top 1  a1.*,a2.TypeTsr 
                        ,a3.ProTypeName
                        ,a4.TypeName
                        ,case a1.IsProValue when 1 then 'True' else 'False' end as StatusProValue
                        ,case a1.IsCarpet when 1 then 'True' else 'False' end as StatusCarPet
                        ,'btnApplication(' + Convert(NVarChar,a1.CreateID) + ','+ Convert(NVarChar,a1.TypeProValue) + ',' + Convert(NVarChar,a5.IdCar) + ',' + Convert(NVarChar,a6.CusID) + ','+ '2'+')' as btnAppClick
                        , a7.CardNo1
                        ,a6.IDCard
                        ,a6.IDCard_Carpet
                        from TblApplication a1
                        Inner Join TblUser a2 on a1.createID = a2.userID
                        Inner Join Tbl_ProductType a3 on a1.ProductID = a3.ProTypeID
                        Inner Join Tbl_Type a4 on a1.TypeProvalue = a4.TypeID
                        Inner Join TblCar a5 on a1.IdCar = a5.IdCar
                        Inner Join TblCustomer a6 on a5.CusID = a6.CusID
                        Left Join TblAppCard a7 on a1.AppID = a7.AppID and a7.CardRun = 1
                        where a1.AppStatus = 1 and a1.idCar =@IdCar and FlagNewApp = 0
                        Order by a1.CreateDate Desc
        " 
        UpdateCommand="UPDATE TblCar SET CarBuyDate = @CarBuyDate WHERE (IdCar = @IdCar)">
        <SelectParameters>
            <asp:QueryStringParameter Name="IdCar" QueryStringField="IdCar" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="CarBuyDate" />
             <asp:QueryStringParameter Name="IdCar" QueryStringField="IdCar" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlCall" runat="server" 
            ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand="SELECT     a1.CallID
                ,a1.CarID
                , a1.IsOutbound 
                , a1.CallOrder
                , a1.SubStatusID
                , a1.CntSubStatus
                , a1.CallDetail
                , a1.StartTime
                , a1.EndTime
                , a1.EndTime - a1.StartTime as TimeCall
                , a1.CreateID
                , a1.CreateDate
                , a1.UpdateID
                , a1.UpdateDate
                , a3.StatusCode +  '(' + CONVERT(VARCHAR(8), a1.CntSubStatus, 112) + ')' as StatusCode
                , a1.IsNew
                , a1.CallTime
                 FROM         TblCall a1 INNER JOIN
                 TblSubStatus a2 ON a1.SubStatusID = a2.SubStatusID
                 Inner Join TblStatus a3 on a2.StatusID = a3.StatusID
                 Where a1.CarID = @CarID  and a1.StatusID <> 5
                 Order by  a1.UpdateDate">
            <SelectParameters>
                <asp:QueryStringParameter Name="CarID" QueryStringField="IdCar" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlRecruit" runat="server" 
            ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand="SELECT * FROM TblRecruit WHERE (IdCar = @IdCar)" 
        InsertCommand="INSERT INTO TblRecruit(IdCar, StatusID, ReDesc, CreateID) 
                    VALUES (@IdCar, @StatusID, @ReDesc, @CreateID)">
            <InsertParameters>
                <asp:QueryStringParameter QueryStringField = "IdCar" Name="IdCar" />
                <asp:Parameter Name="StatusID" />
                <asp:Parameter Name="ReDesc" />
                <asp:CookieParameter CookieName = "userID" Name="CreateID" />
            </InsertParameters>
            <SelectParameters>
                <asp:QueryStringParameter Name="IdCar" QueryStringField="IdCar" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlRecruitStatus" runat="server" 
            ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
            SelectCommand="SELECT * FROM TblRecruitStatus " 
        DeleteCommand="DELETE FROM TblTakePhoto WHERE (appID = @appID)">
            <DeleteParameters>
                <asp:Parameter Name="appID" />
            </DeleteParameters>
           
        </asp:SqlDataSource>

        

    <asp:SqlDataSource ID="SqlAppPay" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" SelectCommand="Select * from tblapppay
where appid = @appid">
        <SelectParameters>
            <asp:Parameter Name="appid" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDetailRenew" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="select ' ทุนประกัน= '+ case when len([newProPrice])> 0 then CONVERT(varchar,CONVERT(Decimal(10,2),[newProPrice]))  else 'ไม่มีข้อมูล' end
                        +',ซ่อม= '+ case when len([CarFixIN])> 0 then CONVERT(varchar,[CarFixIN])  else 'ไม่มีข้อมูล' end
                        +',เบี้ยสุทธิ= '+ case when len([newYearPay])> 0 then  CONVERT(varchar,CONVERT(Decimal(10,2),[newYearPay]))  else 'ไม่มีข้อมูล' end
                        +',เบี้ยรวม= '+ case when len([newYearPayVat])> 0 then  CONVERT(varchar,CONVERT(Decimal(10,2),[newYearPayVat]))  else 'ไม่มีข้อมูล' end
                        +',เคลมผิด= '+ case when len([claim])> 0 then CONVERT(varchar,[claim])  else 'ไม่มีข้อมูล' end
                        +',ยอดเคลม= '+ case when len([claim_value])> 0 then  CONVERT(varchar,CONVERT(Decimal(10,2),[claim_value]))  else 'ไม่มีข้อมูล' end
                        +',หมายเหตุ= '+ case when len([claim_comment])> 0 then CONVERT(varchar,[claim_comment])  else 'ไม่มีข้อมูล' end
                        +',เลขที่กธ.= ' + (select PolicyNO from [TblApplicationU]   where appid in  (select appid from [TblImpCaseReNew] where idcar=@IdCar))
                       +case when len(value2)>1 then
						+',  ' + 
                       +case when value2/1000000 >0  then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value2/1000000.0))) +' ล้าน' 
			            when value2/100000  >0 then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value2/100000.0)))  + ' แสน' 
					    when value2/10000  >0 then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value2/10000.0)))  + ' หมื่น'
					    when value2/1000  >0 then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value2/1000.0)))  + ' พัน' 
		                else ''
end +'/'+
		
		                case when value3/1000000 >0  then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value3/1000000.0))) +' ล้าน' 
			        when value3/100000  >0 then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value3/100000.0)))  + ' แสน' 
					when value3/10000  >0 then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value3/10000.0)))  + ' หมื่น' 
					when value3/1000  >0 then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value3/1000.0)))  + ' พัน' 
					else ''
		end  +'/'+
		case when value1/1000000 >0  then CONVERT(varchar,CONVERT(Decimal(10,2),CONVERT(varchar,value1/1000000.0))) +' ล้าน' 
			        when value1/100000  >0 then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value1/100000.0)))  + ' แสน' 
					when value1/10000  >0 then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value1/10000.0)))  + ' หมื่น' 
					when value1/1000  >0 then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value1/1000.0)))  + ' พัน'
					else ''
		end  +'/'+
		'1+'+CONVERT(varchar,value6) +' '+
		case when value4/1000000 >0  then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value4/1000000.0))) +' ล้าน' 
			        when value4/100000  >0 then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value4/100000.0)))  + ' แสน' 
					when value4/10000  >0 then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value4/10000.0)))  + ' หมื่น' 
					when value4/1000  >0 then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value4/1000.0)))  + ' พัน' 
					else ''
		end  +'/'+
		case when value7/1000000 >0  then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value7/1000000.0))) +' ล้าน' 
			        when value7/100000  >0 then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value7/100000.0)))  + ' แสน' 
					when value7/10000  >0 then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value7/10000.0)))  + ' หมื่น' 
					when value7/1000  >0 then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value7/1000.0)))  + ' พัน' 
					else ''
		end  +'/'+
		case when value9/1000000 >0  then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value9/1000000.0))) +' ล้าน' 
			        when value9/100000  >0 then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value9/100000.0)))  + ' แสน' 
					when value9/10000  >0 then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value9/10000.0)))  + ' หมื่น' 
					when value9/1000  >0 then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value9/1000.0)))  + ' พัน' 
					else ''
		end  +'/DD= '+  
		case when value10/1000000 >0  then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value10/1000000.0))) +' ล้าน' 
			        when value10/100000  >0 then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value10/100000.0)))  + ' แสน' 
					when value10/10000  >0 then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value10/10000.0)))  + ' หมื่น' 
					when value10/1000  >0 then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value10/1000.0)))  + ' พัน' 
					when value10/100  >0 then CONVERT(varchar,CONVERT(Decimal(10,1),CONVERT(varchar,value10/100.0)))  + ' ร้อย'
					when value10<=0 then 'ไม่มีข้อมูล'
					
					else ''

		end
		else '' end
		 as detail
                        ,isnull(pkgid,0) as pkgid
                        ,isnull(newYearPayVat,0) as ProValue
                        ,isnull(newProPrice,0) as ProPrice
                        ,isnull((select Typeid from tblappsubmit  where AppsubmitId=TblImpCaseReNew.pkgid),0) as Typeid
                        from  TblImpCaseReNew where idcar=@IdCar">
        <SelectParameters>
            <asp:QueryStringParameter Name="IdCar" QueryStringField="IdCar" />
        </SelectParameters>
    </asp:SqlDataSource>
	<asp:SqlDataSource ID="SqlAppConfirm" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand=" Select Top 1  a1.AppID                        
                        from TblApplication a1
                        Inner Join TblUser a2 on a1.createID = a2.userID
                        Inner Join Tbl_ProductType a3 on a1.ProductID = a3.ProTypeID
                        Inner Join Tbl_Type a4 on a1.TypeProvalue = a4.TypeID
                        Inner Join TblCar a5 on a1.IdCar = a5.IdCar
                        Inner Join TblCustomer a6 on a5.CusID = a6.CusID
                        Left Join TblAppCard a7 on a1.AppID = a7.AppID and a7.CardRun = 1
                        where a1.AppStatus = 1 and a1.idCar =@IdCar and FlagNewApp = 0
                        Order by a1.CreateDate Desc" >
        <SelectParameters>
            <asp:QueryStringParameter Name="IdCar" QueryStringField="IdCar" />
        </SelectParameters>
</asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlAppPayDGVPay" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand ="Select AppointDate
								,PayDate
								,case TypePay when '2' then TotalPay else TotalPay - 10 end as ProValue
								,case TypePay when '2' then 0 else 10 end as ProVat
								,case TypePay when 2 then 'credit' else 'Payment' end as TypeName
                                ,TotalPay
                        From TblAppPay
                        Where AppID = @AppID
                        order by PayId
                        ">
         <SelectParameters>
            <asp:Parameter Name="Appid" DefaultValue="0" />
         </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqltblapplicationPa" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand ="  select TblPackagePA.PackageName,convert(varchar,TblPackagePA.GeneralCasualty_PhysicalAssault ) as GeneralCasualty_PhysicalAssault,convert(varchar,TblPackagePA.NetPremium ) as NetPremium
  ,convert(varchar,tblapplication.ProtectDate,103) as ProtectDate
  ,isnull(tblcustomer.Age,0) as Age
  ,isnull(convert(varchar,tblcustomer.BirthDate,103),'-') as BirthDate
  ,TblCustomerInit.InitTH+ isnull(tblcustomer.FNameTH,0)+' ' +isnull(tblcustomer.LNameTH,0) as name 
  ,tblcustomer.IDCard
  ,TblOccupation.OccNAME
  from tblapplicationPa
  inner join TblPackagePA on tblapplicationPa.PackageID=TblPackagePA.PackageID
  inner join tblapplication on tblapplicationPa.AppiD=tblapplication.Appid
  inner join tblcustomer on tblapplication.Cusid=tblcustomer.Cusid
  inner join TblCustomerInit on tblcustomer.InitID=TblCustomerInit.InitID
  left join TblOccupation on TblOccupation.occID=tblcustomer.OccID
  where tblapplicationPa.AppID=@AppID">
         <SelectParameters>
            <asp:Parameter Name="AppID" DefaultValue="0" />
         </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlCustomerConfirm" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT a1.CusID,a1.IDCard,a5.CurStatus,a5.CntStatus,a5.IDCar,a5.Comments,
                        a6.Exten,a5.RefNo,a5.IsNew,a1.FNameTH,a1.LNameTH,a2.InitTH,a6.Exten
                        ,a1.Address,a1.Moo,a1.Villege,a1.Soi,a1.Road,a1.Dist,a1.SubDist
						,a1.Province as Provinceth,a1.Zip ,a2.InitTH +' '+a1.FNameTH+' '+a1.LNameTH as InitIDTH
                        FROM TblCustomer a1
                        Left Join TblCustomerInit a2 on a1.InitID = a2.InitID                     
                        Inner Join TblCar a5 on a1.CusID = a5.CusID
                        Inner Join TblUser a6 on a5.AssignTo = a6.UserID
                        where a5.IdCar = @IdCar">
		<SelectParameters> 
            <asp:QueryStringParameter Name="IdCar" QueryStringField="IdCar" DefaultValue="0"/>
        </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlAppCard2Confirm" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand =" Select distinct a1.* 
                        ,a2.BankName
                        ,case when a3.appID IS not null then 'False' else 'True' end as StatusEdit
                        From TblAppCard a1
                        Inner Join TblBank a2 on a1.BankID = a2.BankID
                        Left Join TblCardApprove a3 on a1.CardNo1= a3.CardNo1 and a3.appvStatus = 2 and a1.AppId = a3.AppID
                        and DATEDIFF (DAY ,a3.createDATE ,GETDATE()) < 180 Where a1.AppID = @AppID order by a1.AppCardID" >
         <SelectParameters>
                <asp:Parameter Name="Appid" DefaultValue="0" />
         </SelectParameters>        
</asp:SqlDataSource>

    <asp:SqlDataSource ID="pollDaiNgern" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        UpdateCommand ="Update tblDaiNgern_poll set IsDaiNgern = @IsDaiNgern where IdCar = @IdCar"
        InsertCommand ="INSERT INTO tblDaiNgern_poll 
                               (IdCar,IsDaiNgern,CreateDate)
                        VALUES (@IdCar,@IsDaiNgern,Getdate()) "
             >
        <UpdateParameters >
            <asp:QueryStringParameter Name="IdCar" QueryStringField="IdCar" />
            <asp:Parameter Name="IsDaiNgern" />
        </UpdateParameters>

        <InsertParameters>
             <asp:QueryStringParameter Name="IdCar" QueryStringField="IdCar" />
             <asp:Parameter Name="IsDaiNgern" />
         </InsertParameters>

    </asp:SqlDataSource>
	 <asp:SqlDataSource ID="SqlDataSourceLineID" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
          
        UpdateCommand = "UPDATE TblCustomer  Set FlagLINE=@FlagLINE,LINEID=@LINEID  WHERE Cusid = @CusID" 
        SelectCommand="  select isnull(FlagLINE,'3') as FlagLINE ,isnull(LINEID,'') as LINEID
  from tblcustomer 
  inner join tblcar on tblcustomer.cusid=tblcar.CusID
  where tblcar.IdCar=@IdCar">
         <SelectParameters>
             <asp:QueryStringParameter Name="IdCar" QueryStringField="IdCar" />
         </SelectParameters>
        <UpdateParameters>
           <asp:Parameter Name="FlagLINE" />
           <asp:Parameter Name="LINEID" />
           <asp:Parameter Name="CusID" />
        </UpdateParameters>
    </asp:SqlDataSource>
	<asp:SqlDataSource ID="SqlCaseAppointFirst" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
         UpdateCommand = "Update TblCaseAppointFirst set StatudCase=1 where idcar = @IdCar and StatudCase=0" >
           <UpdateParameters>           
           <asp:Parameter Name="IdCar" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>

