Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Class Modules_Manager_Manage_Tsr_frmHistoryCall
    Inherits System.Web.UI.Page
    Dim ISODate As New ISODate

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        With SqlHistory
            .SelectParameters("userID").DefaultValue = ddTsr.SelectedValue
            .SelectParameters("SupID").DefaultValue = ddSup.SelectedValue
            .SelectParameters("date1").DefaultValue = ISODate.SetISODate("en", txtdate1.Text.Trim)
            .SelectParameters("date2").DefaultValue = ISODate.SetISODate("en", txtdate2.Text.Trim)

        End With

        With SqlTotalTime
            .SelectParameters("userID").DefaultValue = ddTsr.SelectedValue
            .SelectParameters("SupID").DefaultValue = ddSup.SelectedValue
            .SelectParameters("date1").DefaultValue = ISODate.SetISODate("en", txtdate1.Text.Trim)
            .SelectParameters("date2").DefaultValue = ISODate.SetISODate("en", txtdate2.Text.Trim)

        End With

        GvCaseCall.DataBind()
        FormView1.DataBind()
    End Sub

    Protected Sub Button2_Click(sender As Object, e As System.EventArgs) Handles btnBilling.Click
        Dim reportWord As New ReportDocument  ' Report Name 
        Dim reportname As String
        reportname = Server.MapPath("~/Modules/Manager/Report/rptBilling.rpt")

        Dim users As String = "sa"
        'Dim pass As String = "DTS2009"
        Dim pass As String = "asn@sr1"

        'Dim rpt As New CrystalReportViewer

        reportWord.Load(reportname)
        reportWord.SetDatabaseLogon(users, pass)
        reportWord.SetParameterValue("date1", ISODate.SetISODate("en", txtdate1.Text.Trim))
        reportWord.SetParameterValue("date2", ISODate.SetISODate("en", txtdate2.Text.Trim))
        Dim CrExportOptions As ExportOptions
        Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions()
        Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()
        CrDiskFileDestinationOptions.DiskFileName = Server.MapPath("~/Modules/Manager/Report/tmp/rptBilling.pdf")
        CrExportOptions = reportWord.ExportOptions
        With CrExportOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
            .DestinationOptions = CrDiskFileDestinationOptions
            .FormatOptions = CrFormatTypeOptions
        End With
        reportWord.Export()
        reportWord.Close()
        reportWord.Dispose()
        Response.Redirect("~/Modules/Manager/Report/tmp/rptBilling.pdf")
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Request.Cookies("UserLevel").Value <> 1 Then
            btnBilling.Visible = False
        End If
    End Sub
End Class
