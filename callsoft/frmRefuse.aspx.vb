
Partial Class Modules_Manager_Manage_Tsr_frmRefuse
    Inherits System.Web.UI.Page
    Dim ISODate As New ISODate
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        With SqlRefuse
            .SelectParameters("date1").DefaultValue = ISODate.SetISODate("en", txtdate1.Text.Trim)
            .SelectParameters("date2").DefaultValue = ISODate.SetISODate("en", txtdate2.Text.Trim)

        End With
        GvRefuse.DataBind()
    End Sub

    Protected Sub GvRefuse_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvRefuse.RowCommand
        If e.CommandName = "Select" Then
            Dim strLink As String = "RunNo=0&Call=2"
            strLink += "&IdCar=" & e.CommandArgument

            Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "script", "<script>window.open('../../Sale/Phone/frmPhone.aspx?" & strLink & "','Application');</script>")
        End If
    End Sub

    Protected Sub SqlRefuse_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlRefuse.Selected
        lblCase.Text = e.AffectedRows
    End Sub

    Protected Sub GvFollow_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvFollow.RowCommand
        If e.CommandName = "Select" Then
            Dim strLink As String = "RunNo=0&Call=2"
            strLink += "&IdCar=" & e.CommandArgument

            Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "script", "<script>window.open('../../Sale/Phone/frmPhone.aspx?" & strLink & "','Application');</script>")
        End If
    End Sub

    Protected Sub SqlFollow_Selected(sender As Object, e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlFollow.Selected
        lblFollow.Text = e.AffectedRows
    End Sub
End Class
