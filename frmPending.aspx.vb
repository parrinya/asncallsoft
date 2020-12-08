
Partial Class Modules_Manager_Manage_Tsr_frmPending
    Inherits System.Web.UI.Page
    Public LinkPending As String = ""

    Protected Sub WebImageButton1_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles WebImageButton1.Click
        Select Case ddPending.SelectedValue
            Case 1
                LinkPending = "../../Sale/Phone/frmCallCenter.aspx?SupID=" & ddUser.SelectedValue
            Case 2
                LinkPending = "../../Sale/Phone/frmPhoto.aspx?SupID=" & ddUser.SelectedValue
            Case 3
                LinkPending = "../../Sale/Phone/frmReApp.aspx?SupID=" & ddUser.SelectedValue
            Case 4
                LinkPending = "../../Sale/Phone/frmQc.aspx?SupID=" & ddUser.SelectedValue
            Case 5
                LinkPending = "../../Sale/Phone/frmPending.aspx?SupID=" & ddUser.SelectedValue
        End Select
    End Sub
End Class
