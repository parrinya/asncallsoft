Imports System.Drawing
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data
Imports System.Data.SqlClient

Partial Class Modules_Manager_Report_Convernote
    Inherits System.Web.UI.Page
    Dim myReport As New ReportDocument
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        MyReportLoad()
    End Sub
    Protected Sub MyReportLoad()
        Dim users As String = "sa"
        Dim pass As String = "asn@sr1"

        Dim reportname As String = Server.MapPath("acv_lm.rpt")
        myReport.Load(reportname)
        myReport.SetDatabaseLogon(users, pass)

        myReport.SetParameterValue("bcode", "")
        myReport.SetParameterValue("tsrid", "")
        myReport.SetParameterValue("pd1", Session("pd2"))

        myReport.SetParameterValue("UserID", Request.Cookies("UserID").Value)
        
        Response.Buffer = False
        Response.ClearContent()
        Response.ClearHeaders()

        myReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, False, "PrintConvernote")

        
    End Sub
End Class
