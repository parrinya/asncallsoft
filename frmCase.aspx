<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmCase.aspx.vb" Inherits="Modules_Sale_Phone_frmCase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register assembly="Infragistics35.WebUI.WebDataInput.v8.3, Version=8.3.20083.1009, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>

<%@ Register assembly="Infragistics35.WebUI.WebDataInput.v8.3" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/javascript">
   

    function change_parent_url(url) {
        document.location = url;
    }		
   
    </script>
    <style type="text/css">
        .style1
        {
            width: 139px;
        }
        .style2
        {
            width: 390px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin-top: 25px">
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
        Width="100%">
         <asp:TabPanel runat="server" HeaderText="New Case" ID="TabPanel6">
        <ContentTemplate >
            <iframe   id ="Iframe3" src = "<%=NewCasePhone %>" frameborder="0" height="500" scrolling="auto" 
        width="100%"></iframe>
             </ContentTemplate>
             
        </asp:TabPanel>

        <asp:TabPanel runat="server" HeaderText="Case" ID="TabPanel1">
        <ContentTemplate >
            <iframe   id ="ifame2" src = "<%=CasePhone %>" frameborder="0" height="500" scrolling="auto" 
        width="100%"></iframe>
            </ContentTemplate>
            
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Pending">
        <ContentTemplate >
            <table style="width: 100%;">
                <tr>
                    <td class="style2" style="text-align: right">
                        <p style="color: #0066FF; font-weight: bold">
                            เลือก ประเภทงาน :
                        </p>
                    </td>
                    <td class="style1">
                        <asp:DropDownList ID="ddPending" runat="server" CssClass="jamp">
                    <asp:ListItem Value="1">CallCenter</asp:ListItem>
                    <asp:ListItem Value="2">ถ่ายรูปรถ+ทวงหนี้</asp:ListItem>
                    <asp:ListItem Value="3">งานตีกลับ</asp:ListItem>
                    <asp:ListItem Value="4">งานQc</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <igtxt:WebImageButton ID="WebImageButton1" runat="server" Text="แสดง">
                    <appearance><image url="../../../images/Icon/View.png" /></appearance></igtxt:WebImageButton>
                    </td>
                </tr>
            </table>
 <div><iframe  src = "<%= LinkPending %>" frameborder="0" height="400" scrolling="auto" 
        width="100%"></iframe></div>
            </ContentTemplate>
            
        </asp:TabPanel>
         <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Incomplete">
         <ContentTemplate >
             <iframe id ="ifame1"  src = "<%= CaseIncomplet %>"  frameborder="0" height="500" scrolling="no" 
        width="100%"></iframe>
             </ContentTemplate>
         
             
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="บริษัทประกัน">
        <ContentTemplate >
            <iframe id ="Iframe1"  src = "frmCaseInsurance.aspx" frameborder="0" height="500" scrolling="auto" 
        width="100%"></iframe>
            </ContentTemplate>
            
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="วันคุ้มครอง">
        <ContentTemplate >
            <iframe id ="Iframe2"  src = "frmCaseCarbuydate.aspx?userID=<%= Request.Cookies("userID").Value %>" frameborder="0" height="500" scrolling="auto" 
        width="100%"></iframe>
            </ContentTemplate>
            
        </asp:TabPanel>
        <asp:TabPanel runat="server" HeaderText="Expire Case" ID="TabPanel7">
        <ContentTemplate >
            <iframe   id ="Iframe4" src = "<%=ExpireCasePhone %>" frameborder="0" height="500" scrolling="auto" 
        width="100%"></iframe>
            </ContentTemplate>
            
        </asp:TabPanel>
          <asp:TabPanel runat="server" HeaderText="Recoving Case(Renew)" ID="TabPanel8">
        <ContentTemplate >
            <iframe   id ="Iframe5" src = "<%=RecovingCasePhone %>" frameborder="0" height="500" scrolling="auto" 
        width="100%"></iframe>
              </ContentTemplate>
              
        </asp:TabPanel>
    </asp:TabContainer>
</div>
<div style ="width :100%; margin-top: 11px; margin-bottom: 0px;">
  <font style="color: #000000; font-size: 16px; font-weight: bold">Sumary</font><font style="color: #00CCFF; font-size: 16px; font-weight: bold"> 
      Today 
      </font></div>

<div style="border-top: 3px solid #3399FF; margin-top: 5px;"  >
      <asp:DataList ID="DataList2" runat="server" DataSourceID="SqlCaseToday" 
          RepeatDirection="Horizontal" Width="100%" DataKeyField="CCus">
      <ItemTemplate >
      <table cellspacing="0" class="art-article" width="150">
                  <tr>
                      <td class="style1" bgcolor="#99CCFF" style="color: #000000">
                          <asp:Label ID="Label3" runat="server" Text='<%# Eval("StatusName") %>'></asp:Label>
                      </td>
                  </tr>
                  <tr>
                      <td class="style1">
                          <asp:Label ID="lblCCus" runat="server" Text='<%# Eval("CCus") %>'></asp:Label>
                      </td>
                  </tr>
              </table>
      </ItemTemplate>
      </asp:DataList>
      </div>
<div style ="width :100%; margin-top: 11px; margin-bottom: 0px;">
  <font style="color: #000000; font-size: 16px; font-weight: bold">Sumary</font><font style="color: #00CCFF; font-size: 16px; font-weight: bold"> 
      Case 
      </font></div>
     <div style="border-top: 3px solid #3399FF; margin-top: 5px;" class="fontGay2">
     <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlCaseSum" 
          RepeatDirection="Horizontal" Width="100%" DataKeyField="CCus">
      <ItemTemplate >
      <table style="width:150px;" cellspacing="0" class="art-article">
                  <tr>
                      <td class="style2" bgcolor="#CCFF99" style="color: #000000">
                          <asp:Label ID="Label3" runat="server" Text='<%# Eval("StatusName") %>'></asp:Label>
                      </td>
                  </tr>
                  <tr>
                      <td class="style2">
                          <asp:Label ID="Label4" runat="server" Text='<%# Eval("CCus") %>'></asp:Label>
                      </td>
                  </tr>
              </table>
      </ItemTemplate>
      </asp:DataList>
     </div>
      <div style="border-top: 3px solid #3399FF; margin-top: 5px;" class="fontGay2">
     <iframe src="../Report/frmGraph.aspx" frameborder ="0" width = "100%" 
            scrolling ="auto" style="height: 413px" ></iframe>
     </div>
          <asp:SqlDataSource ID="SqlCaseSum" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand=" Select a2.StatusName,Count(a1.IdCar) as CCus
                        ,a2.SortID
                        from TblCar a1
                        Inner Join TblStatus a2 on a1.CurStatus = a2.StatusID
                        Inner Join TblCustomer a3 on a1.cusID = a3.CusID
                        where a1.AssignTo = @userID
                        and a2.StatusID in(1,4,6,7,8)
                        group by  a2.StatusName,a2.SortID
                        order by a2.SortID

">
        <SelectParameters>
            <asp:CookieParameter CookieName="userID" Name="userID" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="SqlCaseToday" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand=" Select a2.StatusName,Count(a1.IdCar) as CCus
                        ,a2.SortID
                        from TblCar a1
                        Inner Join TblStatus a2 on a1.CurStatus = a2.StatusID
                        Inner Join TblCustomer a3 on a1.cusID = a3.CusID
                        where a1.AssignTo = @userID
                        and a2.StatusID in(1,4,6,7,8)
                         and (a1.AppointDate &lt; GetDate() or Convert(VarChar,a1.AppointDate,111) =  Convert(VarChar,GetDate(),111))
                        group by  a2.StatusName,a2.SortID
                        order by a2.SortID

">
        <SelectParameters>
            <asp:CookieParameter CookieName="userID" Name="userID" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

