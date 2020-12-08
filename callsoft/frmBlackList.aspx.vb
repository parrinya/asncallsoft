
Partial Class Modules_Manager_Manage_Case_frmBlackList
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        With SqlBlackList
            .InsertParameters("FNameTH").DefaultValue = txtFNameTH.Text.Trim
            .InsertParameters("LNameTH").DefaultValue = txtLNameTH.Text.Trim
            .InsertParameters("CarID").DefaultValue = txtCarID.Text.Trim
            .Insert()
        End With
        GvBlackList.DataBind()

        txtFNameTH.Text = ""
        txtLNameTH.Text = ""
        txtCarID.Text = ""
    End Sub
End Class
