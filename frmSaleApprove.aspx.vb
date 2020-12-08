Imports System.IO
Imports System.Linq
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient

Partial Class Modules_Manager_Report_frmSaleApprove
    Inherits System.Web.UI.Page
    Dim ISODate As New ISODate
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'add by na 2015/02/19
        If Request.Cookies("TypeTsr").Value = 3 Then
            ddTypedate.Visible = True
        Else
            ddTypedate.Visible = False
        End If
        'End add by na 2015/02/19

    End Sub

    Protected Sub Button2_Click(sender As Object, e As System.EventArgs) Handles Button2.Click
        If GvSaleApprove.Rows.Count > 0 Then
            Dim attachment As String = "attachment; filename=ReportSaleApprove.xls"
            Response.ClearContent()
            Response.AddHeader("content-disposition", attachment)
            Response.ContentType = "application/ms-excel"
            Response.ContentEncoding = System.Text.Encoding.Unicode
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble())

            Dim sw As New StringWriter

            Dim htw As New HtmlTextWriter(sw)
            Dim frm As New HtmlForm
            GvSaleApprove.Parent.Controls.Add(frm)
            frm.Attributes("runat") = "server"
            frm.Controls.Add(GvSaleApprove)
            frm.RenderControl(htw)

            Response.Write(sw.ToString())
            Response.End()


        End If
    End Sub

   
    Protected Sub btn_showList_Click(sender As Object, e As System.EventArgs) Handles btn_showList.Click
        If txtdate1.Text.Trim <> "" And txtdate2.Text.Trim <> "" Then
            'add by na 2015/02/19

            If Request.Cookies("TypeTsr").Value = 3 Then
                If ddTypedate.SelectedValue = 1 Then
                    SqlSaleApprove2.SelectParameters.Item("date1").DefaultValue = ISODate.SetISODate("en", txtdate1.Text.Trim)
                    SqlSaleApprove2.SelectParameters.Item("date2").DefaultValue = ISODate.SetISODate("en", txtdate2.Text.Trim)
                    SqlSaleApprove2.SelectParameters.Item("ComIns").DefaultValue = ddCompanyIns.SelectedValue
                    GvSaleApprove.DataSource = SqlSaleApprove2
                    GvSaleApprove.DataBind()


                Else
                    SqlSaleApprove1.SelectParameters.Item("date1").DefaultValue = ISODate.SetISODate("en", txtdate1.Text.Trim)
                    SqlSaleApprove1.SelectParameters.Item("date2").DefaultValue = ISODate.SetISODate("en", txtdate2.Text.Trim)
                    SqlSaleApprove1.SelectParameters.Item("ComIns").DefaultValue = ddCompanyIns.SelectedValue
                    GvSaleApprove.DataSource = SqlSaleApprove1
                    GvSaleApprove.DataBind()


                End If
                'End add by na 2015/02/19
            Else
                With SqlSaleApprove
                    .SelectParameters("date1").DefaultValue = ISODate.SetISODate("en", txtdate1.Text.Trim)
                    .SelectParameters("date2").DefaultValue = ISODate.SetISODate("en", txtdate2.Text.Trim)
                    .SelectParameters("ComIns").DefaultValue = ddCompanyIns.SelectedValue
                End With
                GvSaleApprove.DataSource = SqlSaleApprove
                GvSaleApprove.DataBind()

               
            End If
            Dim Volume1 As Double = 0
            Dim Volume2 As Double = 0
            Dim Volume3 As Double = 0

            For Each r As GridViewRow In GvSaleApprove.Rows
                If r.RowType = DataControlRowType.DataRow Then
                    Volume1 = Volume1 + CDbl(r.Cells(11).Text) 'เบี้ยเต็ม...
                    Volume2 = Volume2 + CDbl(r.Cells(12).Text) 'เบี้ยขาย...
                    Volume3 = Volume3 + CDbl(r.Cells(13).Text) 'เบี้ยพรบ...
                End If
            Next

            If GvSaleApprove.Rows.Count > 0 Then
                GvSaleApprove.FooterRow.Cells(10).Text = "Total"
                GvSaleApprove.FooterRow.Cells(10).HorizontalAlign = HorizontalAlign.Right
                GvSaleApprove.FooterRow.Cells(11).Text = Math.Round(Volume1, 2).ToString("N2")
                GvSaleApprove.FooterRow.Cells(12).Text = Math.Round(Volume2, 2).ToString("N2")
                GvSaleApprove.FooterRow.Cells(13).Text = Math.Round(Volume3, 2).ToString("N2")
                lbRecordNoFound.Visible = False
            Else
                lbRecordNoFound.Visible = True
            End If
            
        End If
    End Sub
End Class
