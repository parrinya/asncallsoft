
Partial Class Modules_Manager_Manage_Tsr_frmSuccess
    Inherits System.Web.UI.Page

    Protected Sub GvSuccess_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvSuccess.RowCommand
        If e.CommandName = "Select" Then
            Dim strLink As String = "?IdCar=" & GvSuccess.DataKeys(e.CommandArgument).Item(1)
            strLink += "&&RunNo=0"
            strLink += "&Call=1"
            strLink += "&AppID=" & GvSuccess.DataKeys(e.CommandArgument).Item(0)
            Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "script", "<script> window.open('../../Sale/Pending/frmPending.aspx" & strLink & "');</script>")
        End If
    End Sub

    Protected Sub SqlSuccess_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlSuccess.Selected
        lblCase.Text = e.AffectedRows
    End Sub
End Class
