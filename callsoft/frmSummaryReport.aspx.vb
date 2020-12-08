
Partial Class Modules_Manager_Report_frmAgentCall
    Inherits System.Web.UI.Page
    Public strReport As String
    Dim ISODate As New ISODate
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        strReport = "RptAgentCall.aspx?date1=" & ISODate.SetISODate("en", txtdate1.Text.Trim)
        strReport += "&date2=" & ISODate.SetISODate("en", txtdate2.Text.Trim)
        strReport += "&TypeTsr=" & ddTsr.SelectedValue
        strReport += "&report=SummaryReport"
    End Sub
End Class
