Imports System.Drawing
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data
Imports System.IO
Partial Class Modules_Manager_Report_frmProValueDiscount
    Inherits System.Web.UI.Page
    Dim ISODate As New ISODate
    Dim dt As DataTable
    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Dim reportname As String
        Dim myReport As New ReportDocument
        reportname = Server.MapPath("rptProValueDiscount.rpt")
        Response.Buffer = False
        Response.ClearContent()
        Response.ClearHeaders()
        Dim users As String = "sa" 
        Dim pass As String = "asn@sr1"


        myReport.Load(reportname)
        myReport.SetDatabaseLogon(users, pass)
        myReport.SetParameterValue("date1", ISODate.SetISODate("en", txtdate1.Text.Trim))
        myReport.SetParameterValue("date2", txtdate1.Text.Trim)
        myReport.SetParameterValue("date3", ISODate.SetISODate("en", txtdate2.Text.Trim))
        myReport.SetParameterValue("date4", txtdate2.Text.Trim)
        'myReport.SetParameterValue("LeaderID", ddUser.SelectedValue)
        'add by na 2015/02/16
        Dim userid As String = "#"
        If ddUser.SelectedValue = 0 Then
            For value As Integer = 1 To ddUser.Items.Count - 1
                userid = userid + "," + ddUser.Items(value).Value
            Next
            userid = Replace(userid, "#,", "")
        Else
            userid = ddUser.SelectedValue
        End If
        myReport.SetParameterValue("LeaderID", userid)
        myReport.SetParameterValue("SupID", ddsup.SelectedValue)
        If Request.Cookies("TypeTsr").Value = 3 Then
            myReport.SetParameterValue("ConditionID", ddcondition.SelectedValue)
            myReport.SetParameterValue("TypeTsr", "3")
        Else
            myReport.SetParameterValue("ConditionID", "0")
            myReport.SetParameterValue("TypeTsr", "1")
        End If
        If ddExport.SelectedValue = "0" Then
            myReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ใบขออนุมัติส่วนลดเบี้ยประกันภัยรถยนต์")
        Else
            myReport.ExportToHttpResponse(ExportFormatType.Excel, Response, True, "ใบขออนุมัติส่วนลดเบี้ยประกันภัยรถยนต์")
        End If
        'If Request.Cookies("TypeTsr").Value = 3 Then
        '    If ddExport.SelectedValue = "0" Then
        '        myReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ใบขออนุมัติส่วนลดเบี้ยประกันภัยรถยนต์")
        '    Else
        '        myReport.ExportToHttpResponse(ExportFormatType.Excel, Response, True, "ใบขออนุมัติส่วนลดเบี้ยประกันภัยรถยนต์")
        '    End If
        'Else
        '    'end add by na 2015/02/16
        '    'myReport.PrintToPrinter(0, True, 0, 0)
        '    myReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ใบขออนุมัติส่วนลดเบี้ยประกันภัยรถยนต์")
        'End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'add by na 2015/02/16
        If Request.Cookies("TypeTsr").Value = 3 Then
            typetsrhide.Visible = True
            Div1.Visible = False
            DivLead.Visible = True
            ' DivSup.Visible = True
            Div2.Visible = True
        Else
            typetsrhide.Visible = False
            Div1.Visible = True
            DivLead.Visible = True
            ' DivSup.Visible = False
            Div2.Visible = True
        End If
        'end add by na 2015/02/16
    End Sub

End Class
