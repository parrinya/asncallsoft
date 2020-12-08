<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDetailPackage.aspx.vb" Inherits="Modules_Manager_Manage_Case_frmDetailPackage" %>
<%@ Register assembly="Infragistics35.WebUI.WebDataInput.v8.3, Version=8.3.20083.1009, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
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
        .style2
        {
            text-align: right;
        }
        .style3
        {
            text-align: left;
        }
        .style4
        {
            text-align: right;
            font-weight: bold;
            width: 156px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:FormView ID="frmPackage" runat="server" DataSourceID="SqlPackage" 
              Width="100%" BackColor="#CCCCCC"
         >
              <ItemTemplate>
                  <table style="width:100%;" cellspacing="0" class="art-article">
                  <tr><td>AppID:
                      <asp:Label ID="Label1" runat="server" Text='<%# Eval("AppID") %>'></asp:Label></td>
                      <td colspan=2>ทะเบียน:<asp:Label ID="Label2" runat="server" Text='<%# Eval("CarID") %>'></asp:Label></td></tr>
                      <tr bgcolor="#CCFFCC" style="color: #000000; font-weight: bold">
                          <td class="style12" style="text-align: center">
                              ความรับผิดชอบต่อบุคคลภายนอก</td>
                          <td class="style13" style="text-align: center">
                              รถยนต์เสียหาย สูญหาย ไฟไหม้</td>
                          <td style="text-align: center">
                              ความคุ้มครองเอกสารแนบท้าย</td>
                      </tr>
                      <tr>
                          <td class="style12" valign="top">
                              1) ความเสียหายต่อชีวิต ร่างกาย หรืออนามัย<br />
                              &nbsp;
                              
                              <asp:TextBox ID="lblLostLife1" runat="server" 
                                  Text='<%# Eval("Detail1", "{0:N0}") %>'>
                              </asp:TextBox>
                              &nbsp;บาท/คน<br />
                              &nbsp;
                               <asp:TextBox ID="lblLostLife2" runat="server" 
                                 Text='<%# Eval("Detail2", "{0:N0}") %>'></asp:TextBox>
                              &nbsp;บาท/ครั้ง<br />
                              <br />
                              2) ความเสียหายต่อทรัพทย์สิน<br />
                              &nbsp;
                              <asp:TextBox ID="lblLostProp1" runat="server" 
                                 Text='<%# Eval("Detail3", "{0:N0}") %>'></asp:TextBox>
                              &nbsp;บาท/ครั้ง<br />
                              <br />
                              &nbsp; 2.1&nbsp; ความเสียหายส่วนแรก<br />
                              &nbsp;&nbsp;&nbsp; 0&nbsp;บาท/ครั้ง</td>
                          <td class="style13" valign="top">
                              1) ความเสียหายต่อรถยนต์<br />
                              &nbsp;
                              <asp:TextBox ID="lblLostCar1" runat="server" 
                                  Text='<%# Eval("Detail7", "{0:N0}") %>'></asp:TextBox>
                              &nbsp;บาท/ครั้ง<br />
                              <br />
                              &nbsp;&nbsp; 1.1 ความเสียหายส่วนแรก<br />
                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              <asp:TextBox ID="lblLostCar2" runat="server" 
                                 Text='<%# Eval("Detail9", "{0:N0}") %>'></asp:TextBox>
                              &nbsp;บาท/ครั้ง<br />
                              <br />
                              2)รถยนต์สูญหาย/ไฟไหม้<br />
                              &nbsp;&nbsp;
                              <asp:TextBox ID="lblCarFire" runat="server" 
                                  Text='<%# Eval("Detail8", "{0:N0}") %>'></asp:TextBox>
                              &nbsp;บาท<br />
                              <br />
                              3) ความเสียหานจากภัยธรรมชาติ<br /> &nbsp;&nbsp;
                              <asp:TextBox ID="lblLostProp2" runat="server" 
                                  Text='<%# Eval("Detail13", "{0:N0}") %>'></asp:TextBox>
                              &nbsp;บาท/ครั้ง</td>
                          <td valign="top">
                              1) อุบัติเหตุส่วนบุคคล<br />
                              &nbsp; 1.1 เสียชีวิต สูญเสียอวัยวะ ทุพพลภาพถาวร<br />
                              &nbsp; ก.ผู้ขับขี่
                              <asp:TextBox ID="lblAccLost1" runat="server" Text='<%# Eval("Driver1") %>'>
                             </asp:TextBox>
                              &nbsp; คน
                              <asp:TextBox ID="lblAccLost2" runat="server" 
                                 Text='<%# Eval("Detail10", "{0:N0}") %>'></asp:TextBox>
                              &nbsp;บาท<br />
                              &nbsp; ข.ผู้โดยสาร
                              <asp:TextBox ID="lblAccLost3" runat="server" Text='<%# Eval("Driver2") %>'>
                              </asp:TextBox>
                              &nbsp; คน&nbsp;
                              <asp:TextBox ID="lblAccLost4" runat="server" 
                                  Text='<%# Eval("Detail14", "{0:N0}") %>'></asp:TextBox>
                              &nbsp;บาท/คน<br />
                              &nbsp; 1.2 ทุพพลภาพชั่วคราว<br />
                              &nbsp; ก.ผู้ขับขี่&nbsp; 0 คน&nbsp; 0 บาท/คน<br />
                              &nbsp; ข.ผู้โดยสาร&nbsp; 0 คน&nbsp; 0 บาท/คน<br />
                              <br />
                              2) ค่ารักษาพยาบาล&nbsp;
                              <asp:TextBox ID="lblMaintain" runat="server" 
                                  Text='<%# Eval("Detail5", "{0:N0}") %>'></asp:TextBox>
                              &nbsp;บาท/คน<br />
                              <br />
                              3) การประกันตัวผู้ขับขี่&nbsp;
                              <asp:TextBox ID="lblInsure" runat="server" 
                                  Text='<%# Eval("Detail6", "{0:N0}") %>'></asp:TextBox>
                              &nbsp;บาท/ครั้ง</td>
                      </tr>
                      <tr bgcolor="#CCFFFF" style="color: #000000; font-weight: bold">
                          <td class="style12" style="text-align: center">
                              เบี้ยประกัน+ภาษี+อากร<br />
                             
                             <asp:Label ID="txtProValue" runat="server" Text='<%# Eval("Preminum", "{0:N2}") %>'  >
                              </asp:Label>                              
                          </td>
                          <td class="style13" style="text-align: center">
                              พรบ.<br />
                             <asp:Label ID="lblCappet" runat="server" Text='<%# Eval("Carpet", "{0:N}") %>'></asp:Label>
                          </td>
                          <td style="text-align: center">
                              เบี้ยขาย<br />
                                <asp:Label ID="txtTotalValue" runat="server" Text='<%# Eval("Preminum2", "{0:N2}") %>'  >
                              </asp:Label>
                              <br />
                          </td>
                      </tr>
                  </table>
              </ItemTemplate>
          </asp:FormView>
        <center><asp:Button ID="btnsave" runat="server" Text="บันทึก" /></center>
    </div>
    <asp:SqlDataSource ID="SqlPackage" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="  select  
a.Lost_Life1 as Detail1,
a.Lost_Life2 as Detail2,
a.Lost_Prop1 as Detail3,
a.Lost_Car1 as Detail7,
a.Lost_Car2 as Detail9,
a.Car_Fire as Detail8,
a.Lost_Prop2 as Detail13,
a.Acc_Lost1 as Driver1,
a.Acc_Lost2 as Detail10,
a.Acc_Lost3 as Driver2,
a.Acc_Lost4 as Detail14,
a.Maintain as Detail5,
a.Insure as Detail6,
a.YearPay as Preminum,
a.CarPet as Carpet,
a.ProValue as Preminum2
,b.CarID,a.AppID
from TblApplication a 
inner join tblcar b on a.idcar=b.idcar
where a.AppID=@AppID"
UpdateCommand="Update TblApplication Set 
Lost_Life1 = @Lost_Life1,
Lost_Life2 = @Lost_Life2,
Lost_Prop1 = @Lost_Prop1,

ProPrice = @Lost_Car1,
Lost_Car1 = @Lost_Car1,
Lost_Car2 = @Lost_Car2,
Car_Fire = @Car_Fire,
Lost_Prop2 = @Lost_Prop2,

Acc_Lost1 = @Acc_Lost1,
Acc_Lost2 = @Acc_Lost2,
Acc_Lost3 = @Acc_Lost3,
Acc_Lost4 = @Acc_Lost4,
Maintain = @Maintain,
Insure = @Insure
where AppID=@AppID and Statusqc  IN (0,2,3) and AppStatus = 1 "
 InsertCommand="INSERT INTO TblLogEditPackage
        Select [AppID],[Lost_Life1],[Lost_Life2] ,[Lost_Prop1],[Lost_Prop2]
        ,[Lost_Car1],[Lost_Car2],[Car_Fire],[Acc_Lost1],[Acc_Lost2]
        ,[Acc_Lost3],[Acc_Lost4],[Maintain],[Insure],@userID,getdate()
	    from TblApplication where AppID=@AppID">
<SelectParameters>            
    <asp:QueryStringParameter DefaultValue="0" Name="AppID"  QueryStringField="AppID" />
</SelectParameters>
<InsertParameters>
     <asp:QueryStringParameter DefaultValue="0" Name="AppID"  QueryStringField="AppID" />
     <asp:CookieParameter CookieName="userID" DefaultValue="" Name="userID" />
</InsertParameters>
<UpdateParameters>
<asp:Parameter Name="Lost_Life1" />
<asp:Parameter Name="Lost_Life2" />
<asp:Parameter Name="Lost_Prop1" />
<asp:Parameter Name="Lost_Car1" />
<asp:Parameter Name="Lost_Car2" />
<asp:Parameter Name="Car_Fire" />
<asp:Parameter Name="Lost_Prop2" />
<asp:Parameter Name="Acc_Lost1" />
<asp:Parameter Name="Acc_Lost2" />
<asp:Parameter Name="Acc_Lost3" />
<asp:Parameter Name="Acc_Lost4" />
<asp:Parameter Name="Maintain" />
<asp:Parameter Name="Insure" />
<asp:QueryStringParameter DefaultValue="0" Name="AppID"  QueryStringField="AppID" />
</UpdateParameters>

</asp:SqlDataSource>
    </form>
</body>
</html>
