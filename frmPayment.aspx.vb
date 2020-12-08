Imports System.IO

Partial Class Modules_Manager_Manage_Tsr_frmPayment
    Inherits System.Web.UI.Page
    Dim ISODate As New ISODate
    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        With SqlAppPay
            .SelectParameters("date1").DefaultValue = ISODate.SetISODate("en", txtdate1.Text.Trim)
            .SelectParameters("date2").DefaultValue = ISODate.SetISODate("en", txtdate2.Text.Trim)

        End With
        GvAppPay.DataBind()
    End Sub

    Protected Sub Button2_Click(sender As Object, e As System.EventArgs) Handles Button2.Click
        If GvAppPay.Rows.Count > 0 Then
            Dim attachment As String = "attachment; filename=Payment.xls"
            Response.ClearContent()
            Response.AddHeader("content-disposition", attachment)
            Response.ContentType = "application/ms-excel"

            Dim sw As New StringWriter
            Dim htw As New HtmlTextWriter(sw)
            Dim frm As New HtmlForm
            GvAppPay.Parent.Controls.Add(frm)
            frm.Attributes("runat") = "server"
            frm.Controls.Add(GvAppPay)
            frm.RenderControl(htw)
            Response.Write(sw.ToString())
            Response.End()
        End If

    End Sub
End Class
