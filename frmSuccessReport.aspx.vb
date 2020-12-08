Imports System.IO

Partial Class Modules_Manager_Report_frmSuccessReport
    Inherits System.Web.UI.Page
    Dim ISODate As New ISODate
    Dim Value1, Value2, Value3, Value4, Value5, Value6, Value7, Value8, Value9, Value10 As Double
    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        SqlCase.SelectParameters("date1").DefaultValue = ISODate.SetISODate("en", txtdate1.Text.Trim)
        SqlCase.SelectParameters("date2").DefaultValue = ISODate.SetISODate("en", txtdate2.Text.Trim)
        GvCase.DataBind()
    End Sub

    Protected Sub GvCase_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvCase.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            Dim HeaderGrid As GridView = DirectCast(sender, GridView)
            Dim HeaderGridRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim HeaderCell As New TableCell()
            HeaderCell.Text = ""
            HeaderCell.ColumnSpan = 1
            HeaderGridRow.Cells.Add(HeaderCell)

            HeaderCell = New TableCell()
            HeaderCell.Text = "Submit"
            HeaderCell.HorizontalAlign = HorizontalAlign.Center
            HeaderCell.ColumnSpan = 6
            HeaderGridRow.Cells.Add(HeaderCell)

            HeaderCell = New TableCell()
            HeaderCell.Text = "Approve"
            HeaderCell.ColumnSpan = 2
            HeaderCell.HorizontalAlign = HorizontalAlign.Center
            HeaderGridRow.Cells.Add(HeaderCell)

            HeaderCell = New TableCell()
            HeaderCell.Text = "Cancel"
            HeaderCell.HorizontalAlign = HorizontalAlign.Center
            HeaderCell.ColumnSpan = 2
            HeaderGridRow.Cells.Add(HeaderCell)


            GvCase.Controls(0).Controls.AddAt(0, HeaderGridRow)
        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As System.EventArgs) Handles Button2.Click
        If GvCase.Rows.Count > 0 Then
            Dim attachment As String = "attachment; filename=SuccessReport.xls"
            Response.ClearContent()
            Response.AddHeader("content-disposition", attachment)
            Response.ContentType = "application/ms-excel"

            Dim sw As New StringWriter
            Dim htw As New HtmlTextWriter(sw)
            Dim frm As New HtmlForm
            GvCase.Parent.Controls.Add(frm)
            frm.Attributes("runat") = "server"
            frm.Controls.Add(GvCase)
            frm.RenderControl(htw)
            Response.Write(sw.ToString())
            Response.End()
        End If
    End Sub

    Protected Sub GvCase_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvCase.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Value1 = Value1 + CDbl(e.Row.Cells(1).Text)
            Value2 = Value2 + CDbl(e.Row.Cells(2).Text)
            Value3 = Value3 + CDbl(e.Row.Cells(3).Text)
            Value4 = Value4 + CDbl(e.Row.Cells(4).Text)
            Value5 = Value5 + CDbl(e.Row.Cells(5).Text)
            Value6 = Value6 + CDbl(e.Row.Cells(6).Text)
            Value7 = Value7 + CDbl(e.Row.Cells(7).Text)
            Value8 = Value8 + CDbl(e.Row.Cells(8).Text)
            Value9 = Value9 + CDbl(e.Row.Cells(9).Text)
            Value10 = Value10 + CDbl(e.Row.Cells(10).Text)

        ElseIf e.Row.RowType = DataControlRowType.Footer Then

            e.Row.Cells(0).Text = "Total"
            e.Row.Cells(1).Text = Value1.ToString
            e.Row.Cells(2).Text = Value2.ToString
            e.Row.Cells(3).Text = Value3.ToString
            e.Row.Cells(4).Text = Value4.ToString
            e.Row.Cells(5).Text = Value5.ToString
            e.Row.Cells(6).Text = Value6.ToString
            e.Row.Cells(7).Text = Value7.ToString
            e.Row.Cells(8).Text = Value8.ToString
            e.Row.Cells(9).Text = Value9.ToString
            e.Row.Cells(10).Text = Value10.ToString

        End If
    End Sub
End Class
