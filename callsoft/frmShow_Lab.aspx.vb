
Imports System.Data
Imports System.Data.SqlClient
Partial Class Modules_Manager_Import_frmShow_Lab
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'แสดง
        defaultMonth_Year()


    End Sub
    Protected Sub defaultMonth_Year()
        ' ddmonth.SelectedValue = DateTime.Now.Month
        Dim year As Integer = DateTime.Now.Year
        Dim cy As Int16 = 0
        While cy < 3
            ddyear.Items.Insert(cy, year)
            year = year + 1
            cy = cy + 1
        End While
    End Sub

    Protected Sub btnshow_Click(sender As Object, e As System.EventArgs) Handles btnshow.Click
 
        If Request.Cookies("UserLevel").Value = "1" Then
            With SqlshowM
                .SelectParameters("month").DefaultValue = ddmonth.SelectedValue
                .SelectParameters("year").DefaultValue = ddyear.SelectedValue
            End With
            gv_labM.DataBind()
        ElseIf Request.Cookies("UserLevel").Value = "2" Then
            With SqlshowL
                .SelectParameters("month").DefaultValue = ddmonth.SelectedValue
                .SelectParameters("year").DefaultValue = ddyear.SelectedValue
            End With
            gv_lab.DataBind()
        End If
        'With SqlshowM
        '    .SelectParameters("month").DefaultValue = ddmonth.SelectedValue
        '    .SelectParameters("year").DefaultValue = ddyear.SelectedValue
        'End With
        'gv_labM.DataBind()


    End Sub
End Class
