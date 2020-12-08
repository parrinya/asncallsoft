Imports System.Data

Partial Class Modules_Manager_Manage_Case_frmEditStatusCar
    Inherits System.Web.UI.Page
    Dim DataAccess As New DataAccess

     Protected Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        SqlSearch5.SelectParameters.Item("txt").DefaultValue = "%" & txtsearch.Text & "%"
        GVShow.DataBind()
    End Sub

    Protected Sub GVShow_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GVShow.RowCommand
        If e.CommandName = "Update1" Then
            Dim oItem As GridViewRow = DirectCast(DirectCast(e.CommandSource, Button).NamingContainer, GridViewRow)
            Dim RowIndex As Integer = oItem.RowIndex
            Dim idcar As Int64 = GVShow.DataKeys(RowIndex).Item(0)
            Dim statusidnew As String = TryCast(GVShow.Rows(RowIndex).FindControl("DropDownList2"), DropDownList).SelectedItem.Value
            With SqlSearch5
                .InsertParameters("idcar").DefaultValue = idcar
                .InsertParameters("statusidnew").DefaultValue = statusidnew
                .Insert()
            End With
            With SqlSearch5
                .UpdateParameters("idcar").DefaultValue = idcar
                .UpdateParameters("statusidnew").DefaultValue = statusidnew
                .Update()
            End With
            'SqlSearch5.SelectParameters.Item("txt").DefaultValue = "%" & txtsearch.Text & "%"
            GVShow.EditIndex = -1
            GVShow.DataBind()
        Else

        End If
    End Sub
End Class
