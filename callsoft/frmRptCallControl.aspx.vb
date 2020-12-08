Imports System.IO

Partial Class Modules_Manager_Report_frmRptCallControl
    Inherits System.Web.UI.Page
    Dim ISODate As New ISODate
    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        SqlCallControl.SelectParameters("date1").DefaultValue = ISODate.SetISODate("en", txtdate1.Text.Trim)
        GvCallControl.DataBind()
    End Sub

    Protected Sub Button2_Click(sender As Object, e As System.EventArgs) Handles Button2.Click
        If GvCallControl.Rows.Count > 0 Then
            Dim attachment As String = "attachment; filename=CallControl.xls"
            Response.ClearContent()
            Response.AddHeader("content-disposition", attachment)
            Response.ContentType = "application/ms-excel"

            Dim sw As New StringWriter
            Dim htw As New HtmlTextWriter(sw)
            Dim frm As New HtmlForm
            GvCallControl.Parent.Controls.Add(frm)
            frm.Attributes("runat") = "server"
            frm.Controls.Add(GvCallControl)
            frm.RenderControl(htw)
            Response.Write(sw.ToString())
            Response.End()
        End If
    End Sub
End Class
