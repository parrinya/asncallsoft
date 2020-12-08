Imports System.Drawing
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data
Partial Class Modules_Manager_Report_frmAgentCall
    Inherits System.Web.UI.Page
    Dim myReport As New ReportDocument
    Dim ISODate As New ISODate

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Request.QueryString("report") IsNot Nothing Then
            Select Case Request.QueryString("report").ToString
                Case "KPI"
                    ReportKPI()
                Case "SaleReport"
                    ReportSale()
                Case "SummaryReport"
                    ReportSummary()
            End Select

        Else
            MyReportLoad()
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        myReport.Dispose()
        myReport.Close()
    End Sub

    Protected Sub MyReportLoad()

        Dim reportname As String
        reportname = Server.MapPath("rptAgentCall.rpt")

        Dim users As String = "sa"
        'Dim pass As String = "DTS2009"
        Dim pass As String = "asn@sr1"


        myReport.Load(reportname)
        myReport.SetDatabaseLogon(users, pass)
        CrystalReportViewer1.ReportSource = myReport
        myReport.SetParameterValue("date1", Request.QueryString("date1").ToString)
        myReport.SetParameterValue("date2", Request.QueryString("date2").ToString)
        myReport.SetParameterValue("typetsr", Request.Cookies("TypeTsr").Value)
        If Request.Cookies("UserLevel").Value = 3 Then
            CrystalReportViewer1.SelectionFormula = "{tblcall.SupID}=" & Request.Cookies("userID").Value
        ElseIf Request.Cookies("UserLevel").Value = 2 Then
            CrystalReportViewer1.SelectionFormula = "{tblcall.LeaderID}=" & Request.Cookies("userID").Value
        End If
        CrystalReportViewer1.DataBind()


    End Sub

    Protected Sub ReportKPI()

        Dim reportname As String
        reportname = Server.MapPath("rptKpiReport.rpt")

        Dim users As String = "sa"
        'Dim pass As String = "DTS2009"
        Dim pass As String = "asn@sr1"


        myReport.Load(reportname)
        myReport.SetDatabaseLogon(users, pass)
        CrystalReportViewer1.ReportSource = myReport
        myReport.SetParameterValue("date1", Request.QueryString("date1").ToString)
        myReport.SetParameterValue("date2", Request.QueryString("date2").ToString)
        'myReport.SetParameterValue("typetsr", Request.Cookies("TypeTsr").Value)



        If Request.QueryString("LeadID").ToString <> 0 And Request.QueryString("SupID").ToString <> 0 Then
            CrystalReportViewer1.SelectionFormula = "{tblkpi.SupID}=" & Request.QueryString("SupID").ToString
            CrystalReportViewer1.SelectionFormula += " and {tblkpi.LeaderID}=" & Request.QueryString("LeadID").ToString
        ElseIf Request.QueryString("LeadID").ToString <> 0 And Request.QueryString("SupID").ToString = 0 Then
            CrystalReportViewer1.SelectionFormula = "{tblkpi.LeaderID}=" & Request.QueryString("LeadID").ToString
        End If
        CrystalReportViewer1.DataBind()
    End Sub

    Protected Sub ReportSale()

        Dim reportname As String
        reportname = Server.MapPath("rptSaleReport.rpt")

        Dim users As String = "sa"
        'Dim pass As String = "DTS2009"
        Dim pass As String = "asn@sr1"


        myReport.Load(reportname)
        myReport.SetDatabaseLogon(users, pass)
        CrystalReportViewer1.ReportSource = myReport
        myReport.SetParameterValue("date1", Request.QueryString("date1").ToString)
        myReport.SetParameterValue("date2", Request.QueryString("date2").ToString)
        myReport.SetParameterValue("SupID", Request.QueryString("SupID").ToString)
        myReport.SetParameterValue("LeadID", Request.QueryString("LeadID").ToString)
        CrystalReportViewer1.DataBind()
    End Sub

    Protected Sub ReportSummary()

        Dim reportname As String
        reportname = Server.MapPath("rptSummaryReport.rpt")

        Dim users As String = "sa"
        'Dim pass As String = "DTS2009"
        Dim pass As String = "asn@sr1"


        myReport.Load(reportname)
        myReport.SetDatabaseLogon(users, pass)
        CrystalReportViewer1.ReportSource = myReport
        myReport.SetParameterValue("date1", Request.QueryString("date1").ToString)
        myReport.SetParameterValue("date2", Request.QueryString("date2").ToString)
        myReport.SetParameterValue("TypeTsr", Request.QueryString("TypeTsr").ToString)
        CrystalReportViewer1.DataBind()
    End Sub
End Class
