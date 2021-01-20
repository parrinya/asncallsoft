<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCaseIncompleteOOHOORenew.aspx.vb" Inherits="Modules_Sale_Phone_frmCaseIncompleteOOHOORenew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="../../../Styles/stylefont.css" type="text/css" media="screen"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
    <table>
        <tr>
            <td>วันคุ้มครอง</td>
            <td> <asp:TextBox ID="txtdate1" runat="server" Width="100"></asp:TextBox>
        <asp:MaskedEditExtender ID="txtdate1_MaskedEditExtender" runat="server" 
            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate1">
        </asp:MaskedEditExtender>
        <asp:CalendarExtender ID="txtdate1_CalendarExtender" runat="server" 
            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate1">
        </asp:CalendarExtender></td>
            <td></td>
            <td><asp:TextBox ID="txtdate2" runat="server" Width="100"></asp:TextBox>
        <asp:MaskedEditExtender ID="txtdate2_MaskedEditExtender" runat="server" 
            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate2">
        </asp:MaskedEditExtender>
        <asp:CalendarExtender ID="txtdate2_CalendarExtender" runat="server" 
            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate2">
        </asp:CalendarExtender></td>
            <td>วันชำระ</td>
            <td> <asp:TextBox ID="txtdate3" runat="server" Width="100"></asp:TextBox>
        <asp:MaskedEditExtender ID="txtdate3_MaskedEditExtender" runat="server" 
            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate3">
          </asp:MaskedEditExtender>
        <asp:CalendarExtender ID="txtdate3_CalendarExtender" runat="server" 
            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate3">
            </asp:CalendarExtender>
        </td>
        <td></td>
            <td>
             <asp:TextBox ID="txtdate4" runat="server" Width="100"></asp:TextBox>
        <asp:MaskedEditExtender ID="txtdate4_MaskedEditExtender" runat="server" 
            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate4">
            </asp:MaskedEditExtender>
        <asp:CalendarExtender ID="txtdate4_CalendarExtender" runat="server" 
            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate4">
             </asp:CalendarExtender>
            
            </td>
            <td>
                <asp:Button ID="btnFind" runat="server" Text="แสดง" /> </td>
             
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate >
              <asp:Panel ID="Panel1" runat="server" Height="400px" ScrollBars="Auto"  Width="100%">
                  <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False">
                                
                                <Columns>
                                    <asp:BoundField DataField="typee" HeaderText="สรุป" >
                                    <HeaderStyle BackColor="#99CCFF" />
                                    <ItemStyle BackColor="#99CCFF" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="countrow" HeaderText="จำนวน"  >
                                    <HeaderStyle BackColor="#99CCFF" />
                                    <ItemStyle BackColor="#99CCFF" Font-Bold="True" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="sumprovalue" HeaderText="รวมเบี้ยขาย"  
                                        DataFormatString="{0:N2}">
                                    <HeaderStyle BackColor="#99CCFF" />
                                    <ItemStyle BackColor="#99CCFF" Font-Bold="True" HorizontalAlign="Right" 
                                        ForeColor="#009933" />
                                    </asp:BoundField>
                                </Columns>
                                
                            </asp:GridView>
                        </td>
                      <%--  <td> <asp:Label ID="Label1" runat="server" Text="จำนวนทั้งหมด :"></asp:Label></td>
                        <td> <asp:Label ID="lblCase" runat="server" Text=""></asp:Label></td>--%>

                       <%-- <td> <asp:Label ID="lblCaseCMI" runat="server" Text=""></asp:Label></td>
                        <td> <asp:Label ID="lblCaseVMI" runat="server" Text=""></asp:Label></td>--%>
                    </tr>
                  </table>
                  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  CssClass="art-article">

                  <AlternatingRowStyle BackColor="#D7EBFF" />
                      <Columns>
                          <asp:BoundField DataField="appid" HeaderText="AppID" />                             
                          <asp:BoundField DataField="fnameth" HeaderText="ชื่อ" />
                          <asp:BoundField DataField="lnameth" HeaderText="สกุล" />
                          <asp:BoundField DataField="carid" HeaderText="ทะเบียน" />
                          <asp:BoundField DataField="typee" HeaderText="ประเภท" />
                          <asp:BoundField DataField="paydate" HeaderText="วันที่จ่าย" />
                          <asp:BoundField DataField="PayValue" HeaderText="จำนวนจ่าย" 
                              DataFormatString="{0:N2}" >
                          <ItemStyle HorizontalAlign="Right" />
                          </asp:BoundField>
                          <asp:BoundField DataField="protectdate" HeaderText="เริ่มคุ้มครอง" />
                          <asp:BoundField DataField="provalue" HeaderText="เบี้ยขาย" 
                              DataFormatString="{0:N2}" >
                          <ItemStyle HorizontalAlign="Right" />
                          </asp:BoundField>
                      </Columns>
                     <HeaderStyle CssClass="td-header" Height="30px" ForeColor="Black" BackColor="#99CCFF" ></HeaderStyle>

                  </asp:GridView>
              </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
   <asp:SqlDataSource ID="SqlCustomer" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="
                      select  a5.fnameth,a5.lnameth, a3.appid,a2.carid,convert(varchar,a4.paydate,103) +' '+convert(VARCHAR(8), a4.paydate, 14) as 'paydate',a4.PayValue
 ,a3.typee,convert(varchar,a3.protectdate,103) as 'protectdate',a3.provalue
 
 from tblcar a1 
 inner join tblcar a2 on a1.carboxno = a2.carboxno 
 inner join (
  select appid,idcar,'สมัครใจ' as 'typee',ProtectDate as 'ProtectDate',provalue from tblapplication where AppStatus=1 and isprovalue = 1 and CreateID=5069
 union 
 select  appid,idcar,'พรบ' as 'typee' ,CarPetDate as 'ProtectDate',CarPet as  'provalue'  from tblapplication  where AppStatus=1 and IsCarpet=1 and CreateID=5069
 
 ) a3 on a2.Idcar=a3.idcar
 inner join tblpayment  a4 on a3.appid  = a4.appid and  a4.PayNo = 1 
 inner join tblcustomer a5 on a1.cusid = a5.cusid 
 where a1.groupid in (select groupid from tblsourcegroup where dataid  = 23 ) 
 and  a2.CurStatus=3
 and  a1.AssignTo=@userID "  >
       
        
        <SelectParameters>
            <asp:CookieParameter CookieName="userID" Name="userID" />
        </SelectParameters>
    </asp:SqlDataSource>

     <asp:SqlDataSource ID="SqlCountData" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="
                      select   a3.typee,count(*) as countrow,sum(a3.provalue) as sumprovalue
 
 from tblcar a1 
 inner join tblcar a2 on a1.carboxno = a2.carboxno 
 inner join (
  select appid,idcar,'สมัครใจ' as 'typee',ProtectDate as 'ProtectDate',provalue from tblapplication where AppStatus=1 and isprovalue = 1 and CreateID=5069
 union 
 select  appid,idcar,'พรบ' as 'typee' ,CarPetDate as 'ProtectDate',CarPet as  'provalue'  from tblapplication  where AppStatus=1 and IsCarpet=1 and CreateID=5069
 
 ) a3 on a2.Idcar=a3.idcar
 inner join tblpayment  a4 on a3.appid  = a4.appid and  a4.PayNo = 1 
 inner join tblcustomer a5 on a1.cusid = a5.cusid 
 where a1.groupid in (select groupid from tblsourcegroup where dataid  = 23 ) 
 and  a2.CurStatus=3
 and  a1.AssignTo=@userID 
                                           
"  >
       
        <SelectParameters>
            <asp:CookieParameter CookieName="userID" Name="userID" />
        </SelectParameters>
       
    </asp:SqlDataSource>


    </div>
    </form>
</body>
</html>
