Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Partial Class Modules_Manager_Report_frmWebASN
    Inherits System.Web.UI.Page
    Dim ISODate As New ISODate

    Protected Sub btn1_Click(sender As Object, e As System.EventArgs) Handles btn1.Click
        'กด แสดง 
        SqlSalewebasn.Delete()
        With SqlSalewebasn
            .InsertParameters("date1").DefaultValue = ISODate.SetISODate("en", txtdate1.Text.Trim)
            .InsertParameters("date2").DefaultValue = ISODate.SetISODate("en", txtdate2.Text.Trim)
            .Insert()
        End With
        GvSaleApprove.DataBind()
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        'กด export
        Dim reportname As String
        Dim myReport As New ReportDocument
        Dim users As String = "sa"
        Dim pass As String = "asn@sr1"
        Dim DispDate As String = "ระหว่าง " + Format(CDate(txtdate1.Text), "dd-MM-yyyy").ToString() + " ถึง " + Format(CDate(txtdate2.Text), "dd-MM-yyyy").ToString()
        reportname = Server.MapPath("rptSaleWebAgent.rpt")
        Response.Buffer = False
        Response.ClearContent()
        Response.ClearHeaders()
        myReport.Load(reportname)
        myReport.SetDatabaseLogon(users, pass)
        myReport.SetParameterValue("datestr", DispDate)
        If ddExport.SelectedValue = "0" Then
            myReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "WebAgentReport")
        Else
            myReport.ExportToHttpResponse(ExportFormatType.Excel, Response, True, "WebAgentReport")
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SqlDataSource1.Delete()
        End If

    End Sub
End Class
