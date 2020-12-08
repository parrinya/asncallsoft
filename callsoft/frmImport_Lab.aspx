<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/Page/Index/MasterPage.master" AutoEventWireup="false" CodeFile="frmImport_Lab.aspx.vb" Inherits="Modules_Manager_Import_frmImport_Lab" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <ContentTemplate >
        <div>
            <table style="width: 100%;">
                <tr>
                    <td >
                        เลือก file</td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                         <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    
                </tr>
                
                <tr>
                    <td class="style1">
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnimport" runat="server" Text="Import" />
                    </td>
                    <td>
                       
                    </td>
                </tr>
            </table>
        </div>
        <asp:SqlDataSource ID="SqlimportLab" runat="server" 
    ConnectionString="<%$ ConnectionStrings:asnbroker %>"
      InsertCommand="INSERT INTO TblRewardTsr(Userid, RewardDate, Reward, Typereword, Createid) 
                        VALUES (@Userid, @RewardDate, @Reward, @Typereword, @Createid )"
       DeleteCommand= "DELETE FROM TblRewardTsr WHERE Userid = @Userid and Typereword=@Typereword and 
                        month(RewardDate)=month(convert(datetime, @RewardDate, 103)) and 
                        year(RewardDate)+543=year(convert(datetime, @RewardDate, 103))  ">
       <DeleteParameters>
            <asp:Parameter Name="Userid" />
            <asp:Parameter Name="RewardDate" />            
            <asp:Parameter Name="Typereword" />
       </DeleteParameters>
       <InsertParameters>  
            <asp:Parameter Name="Userid" />
            <asp:Parameter Name="RewardDate" type="DateTime"/>
            <asp:Parameter Name="Reward" />
            <asp:Parameter Name="Typereword" />   
            <asp:CookieParameter CookieName="userID"  Name="Createid" />        
                
       </InsertParameters>
        
</asp:SqlDataSource>   
        </ContentTemplate>

  

</asp:Content>

