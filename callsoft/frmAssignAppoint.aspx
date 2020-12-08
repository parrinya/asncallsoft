<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmAssignAppoint.aspx.vb" Inherits="Modules_Manager_Manage_Case_frmAssignAppoint" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .style1
    {
        width: 300px;
        text-align: right;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
<table style="width: 100%;">
        <tr>
            <td class="style1">
                Team Sup</td>
            <td>
                <asp:DropDownList ID="ddSup" runat="server" AutoPostBack="True" CssClass="jamp" 
                    DataSourceID="SqlSup" DataTextField="SupName" DataValueField="userID">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Tsr</td>
            <td>
                <asp:DropDownList ID="ddTsr" runat="server" AutoPostBack="True" 
                    CssClass="jamp" DataSourceID="SqlTsr" DataTextField="SupName" 
                    DataValueField="userID">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style1">
                สถานะ&nbsp;
            </td>
            <td>
                <asp:DropDownList ID="ddStatus" runat="server" AutoPostBack="True" 
                    CssClass="jamp" DataSourceID="SqlStatus" DataTextField="StatusName" 
                    DataValueField="StatusID">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style1">
                จำนวน Case</td>
            <td>
                <asp:DropDownList ID="ddCase" runat="server" CssClass="jamp" 
                    DataSourceID="SqlCase" DataTextField="RecIdCar" DataValueField="RecIdCar">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Assign" />
                <asp:ConfirmButtonExtender ID="Button1_ConfirmButtonExtender" runat="server" 
                    ConfirmText="ต้องการเปลี่ยนวันนัดเป็นวันปัจจุบันหรือไม่" 
                    TargetControlID="Button1">
                </asp:ConfirmButtonExtender>
            </td>
        </tr>
    </table>
</div>
<div style="font-weight: 700">
* ระบบจะดำเนินการเปลี่ยนวันนัดให้เป็นวันปัจจุบัน ตามเงื่อนไขที่ท่านได้เลือกข้างต้น
</div>

    <asp:SqlDataSource ID="SqlSup" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT a1.userID
                        ,a1.FName + ' ' + a1.LName as SupName
                         FROM [TblUser] a1
                         where UserLevelID in(3) and a1.userstatus = 1
                        "></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlTsr" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT a1.userID
                        ,a1.FName + ' ' + a1.LName as SupName
                         FROM [TblUser] a1
                         where UserLevelID in(5) and a1.userstatus = 1 and a1.SupID = @SupID
                        ">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddSup" Name="SupID" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlStatus" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT * from TblStatus Where StatusID in(6,7,8)
                        "></asp:SqlDataSource>

     <asp:SqlDataSource ID="SqlCase" runat="server" 
        ConnectionString="<%$ ConnectionStrings:asnbroker %>" 
        SelectCommand="SELECT Count(a1.IdCar) as RecIdCar
                        from TblCar  a1
                        inner join tbluser a2 on a1.Assignto = a2.userid
                        where a1.AssignTo = @TsrID and a1.CurStatus = @CurStatus  and a2.supid = @supID
                        " 
        
        UpdateCommand="UPDATE TblCar SET AppointDate = GETDATE() WHERE (AssignTo = @AssignTo) AND (CurStatus = @curStatus)">
         <SelectParameters>
             <asp:ControlParameter ControlID="ddTsr" Name="TsrID" 
                 PropertyName="SelectedValue" />
             <asp:ControlParameter ControlID="ddStatus" Name="CurStatus" 
                 PropertyName="SelectedValue" />
             <asp:ControlParameter ControlID="ddSup" Name="supID" 
                 PropertyName="SelectedValue" />
         </SelectParameters>
         <UpdateParameters>
              <asp:ControlParameter ControlID="ddTsr" Name="AssignTo" 
                 PropertyName="SelectedValue" />
             <asp:ControlParameter ControlID="ddStatus" Name="CurStatus" 
                 PropertyName="SelectedValue" />
         </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>

