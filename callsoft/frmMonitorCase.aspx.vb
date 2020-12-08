
Imports Infragistics.WebUI.WebDataInput

Partial Class Modules_Manager_Manage_Tsr_frmMonitorCase
    Inherits System.Web.UI.Page
    Dim ISODate As New ISODate
    Dim FunAll As New FuntionAll
    Public linkCarBuy As String
    Public IdCar As String = ""
 
 
 
    Protected Sub GvFollow_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvFollow.RowCommand
        If e.CommandName = "Select" Then
            SqlCustomer.SelectParameters("IdCar").DefaultValue = e.CommandArgument
            frmCustomer.DataBind()
            IdCar = e.CommandArgument
        End If

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim txtAppoint As TextBox = FunAll.ObjFindControl("txtAppoint", frmCustomer)
        Dim txtHour As WebNumericEdit = FunAll.ObjFindControl("txtHour", frmCustomer)
        Dim txtMin As WebNumericEdit = FunAll.ObjFindControl("txtMin", frmCustomer)
        Dim ddStatus As DropDownList = FunAll.ObjFindControl("ddStatus", frmCustomer)

        With SqlCustomer
            .InsertParameters("StatusOld").DefaultValue = frmCustomer.DataKey.Item(1)
            .InsertParameters("StatusNew").DefaultValue = ddStatus.SelectedValue
            .InsertParameters("IpLog").DefaultValue = Request.ServerVariables("REMOTE_ADDR")
            .InsertParameters("IdCar").DefaultValue = frmCustomer.DataKey.Item(0)
            .Insert()
        End With

        SqlCustomer.UpdateParameters("AppointDate").DefaultValue = ISODate.SetISODate("en", txtAppoint.Text.Trim) & " " & txtHour.Text.Trim & ":" & txtMin.Text.Trim
        SqlCustomer.UpdateParameters("IdCar").DefaultValue = frmCustomer.DataKey.Item(0)
        SqlCustomer.UpdateParameters("CurStatus").DefaultValue = ddStatus.SelectedValue
        SqlCustomer.Update()

        GvFollow.DataBind()
        GvCallBack.DataBind()
        GvNoContact.DataBind()
        GvWait.DataBind()

        SqlCustomer.SelectParameters("IdCar").DefaultValue = 0
        frmCustomer.DataBind()
    End Sub

    Protected Sub GvCallBack_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvCallBack.RowCommand
        If e.CommandName = "Select" Then
            SqlCustomer.SelectParameters("IdCar").DefaultValue = e.CommandArgument
            frmCustomer.DataBind()
            IdCar = e.CommandArgument
        End If
    End Sub

    Protected Sub GvNoContact_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvNoContact.RowCommand
        If e.CommandName = "Select" Then
            SqlCustomer.SelectParameters("IdCar").DefaultValue = e.CommandArgument
            frmCustomer.DataBind()
            IdCar = e.CommandArgument
        End If
    End Sub

    Protected Sub GvWait_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvWait.RowCommand
        If e.CommandName = "Select" Then
            SqlCustomer.SelectParameters("IdCar").DefaultValue = e.CommandArgument
            frmCustomer.DataBind()
            IdCar = e.CommandArgument
        End If
    End Sub

    Protected Sub GvRefuse_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvRefuse.RowCommand
        If e.CommandName = "Select" Then
            SqlCustomer.SelectParameters("IdCar").DefaultValue = e.CommandArgument
            frmCustomer.DataBind()
            IdCar = e.CommandArgument
        End If
    End Sub

    Protected Sub GvNotUpdate_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvNotUpdate.RowCommand
        If e.CommandName = "Select" Then
            SqlCustomer.SelectParameters("IdCar").DefaultValue = e.CommandArgument
            frmCustomer.DataBind()
            IdCar = e.CommandArgument
        End If
    End Sub

    Protected Sub SqlNewCase_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlNewCase.Selected
        lblNewCase.text = e.AffectedRows
    End Sub

    Protected Sub SqlFollow_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlFollow.Selected
        lblFollow.Text = e.AffectedRows
    End Sub

    Protected Sub SqlCallBack_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlCallBack.Selected
        lblCallBack.Text = e.AffectedRows
    End Sub

    Protected Sub SqlNoContact_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlNoContact.Selected
        lblNoContact.Text = e.AffectedRows
    End Sub

    Protected Sub SqlWait_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlWait.Selected
        lblWait.Text = e.AffectedRows
    End Sub

    Protected Sub SqlRefuse_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlRefuse.Selected
        lblRefuse.Text = e.AffectedRows
    End Sub

    Protected Sub SqlNotUpdate_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlNotUpdate.Selected
        lblNotUpdate.Text = e.AffectedRows
    End Sub

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        BindGv(1, txtSearch.Text.Trim)
    End Sub


    Protected Sub BindGv(ByVal TypeSearch As String, ByVal strSearch As String)
        With SqlNewCase
            .SelectParameters("TypeSearch").DefaultValue = TypeSearch
            .SelectParameters("Search").DefaultValue = "%" & strSearch & "%"

        End With
        With SqlFollow
            .SelectParameters("TypeSearch").DefaultValue = TypeSearch
            .SelectParameters("Search").DefaultValue = "%" & strSearch & "%"

        End With
        With SqlWait
            .SelectParameters("TypeSearch").DefaultValue = TypeSearch
            .SelectParameters("Search").DefaultValue = "%" & strSearch & "%"

        End With
        With SqlNoContact
            .SelectParameters("TypeSearch").DefaultValue = TypeSearch
            .SelectParameters("Search").DefaultValue = "%" & strSearch & "%"

        End With
        With SqlCallBack
            .SelectParameters("TypeSearch").DefaultValue = TypeSearch
            .SelectParameters("Search").DefaultValue = "%" & strSearch & "%"

        End With
        With SqlNotUpdate
            .SelectParameters("TypeSearch").DefaultValue = TypeSearch
            .SelectParameters("Search").DefaultValue = "%" & strSearch & "%"

        End With
        With SqlRefuse
            .SelectParameters("TypeSearch").DefaultValue = TypeSearch
            .SelectParameters("Search").DefaultValue = "%" & strSearch & "%"

        End With
    
        GvNewCase.DataBind()
        GvCallBack.DataBind()
        GvFollow.DataBind()
        GvNoContact.DataBind()
        GvRefuse.DataBind()
        GvWait.DataBind()
        GvNotUpdate.DataBind()
    End Sub

    Protected Sub ddTsr1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddTsr1.SelectedIndexChanged
        BindGv(0, 0)
        linkCarBuy = "../../Sale/Phone/frmCaseCarbuydate.aspx?userID=" & ddTsr1.SelectedValue
    End Sub


 
End Class
