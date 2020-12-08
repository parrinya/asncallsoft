Imports Infragistics.WebUI.Shared

Partial Class Modules_Manager_Report_frmReportSourcebyList
    Inherits System.Web.UI.Page
    Public strReport As String
    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        If txtdate1.Text = "" Or txtdate2.Text = "" Then

            'MessageBox.Show("กรุณากรอกวัน SubmitDate ค่ะ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            Dim sb As New System.Text.StringBuilder()
            sb.Append("<script type = 'text/javascript'>")
            sb.Append("window.onload=function(){")
            sb.Append("alert('")
            sb.Append("กรุณากรอกวัน SubmitDate ค่ะ")
            sb.Append("')};")
            sb.Append("</script>")
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", sb.ToString())


        ElseIf txtdate1.Text <> "" And txtdate2.Text <> "" And (Format(CDate(txtdate1.Text), "dd-MM-yyyy") <= Format(CDate(txtdate2.Text), "dd-MM-yyyy")) Then
            strReport = "ReportSourcebyList.aspx?date1=" & txtdate1.Text
            strReport += "&date2=" & txtdate2.Text
            strReport += "&LeadID=" & ddLead.SelectedValue
            strReport += "&SupID=" & ddSup.SelectedValue

        End If
    End Sub
End Class
