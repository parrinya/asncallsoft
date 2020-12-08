Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Partial Class Modules_Manager_Report_frmRetention
    Inherits System.Web.UI.Page
    Public strReport As String
    
    Protected Sub btn1_Click(sender As Object, e As System.EventArgs) Handles btn1.Click
        strReport = "Retention.aspx?date1=" & txtdate1.Text
        strReport += "&date2=" & txtdate2.Text
        strReport += "&CompID=" & ddcomp.SelectedValue
        strReport += "&CompName=" & ddcomp.SelectedItem.ToString
        strReport += "&SupID=" & ddsup.SelectedValue
    End Sub
End Class
