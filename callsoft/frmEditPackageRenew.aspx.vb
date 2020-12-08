
Partial Class Modules_Manager_Manage_Case_frmEditPackageRenew
    Inherits System.Web.UI.Page
    Dim ISODate As New ISODate
    Public strLink As String

    Protected Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        If name.Checked And txtsearch.Text <> "" Then 'ค้นหาตามชื่อ/ทะเบียน           
            SqlNameCarID.SelectParameters.Item("txt").DefaultValue = "%" + txtsearch.Text + "%"
            GVShow.DataSource = SqlNameCarID
            GVShow.DataBind()
        ElseIf successdate.Checked And txtdate1.Text <> "" And txtdate2.Text <> "" Then 'ตามวันที่successdate
            SqlSuccessDate.SelectParameters.Item("date1").DefaultValue = ISODate.SetISODate("en", txtdate1.Text.Trim)
            SqlSuccessDate.SelectParameters.Item("date2").DefaultValue = ISODate.SetISODate("en", txtdate2.Text.Trim)
            GVShow.DataSource = SqlSuccessDate
            GVShow.DataBind()
        Else
            MsgBox("กรุณากรอกข้อมูลในการค้นหา")
            GVShow.DataBind()
        End If
    End Sub

    Protected Sub GVShow_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GVShow.RowCommand
        If e.CommandName = "select" Then
            strLink = "frmDetailPackage.aspx?AppId=" & GVShow.DataKeys(e.CommandArgument).Item(0)
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
     
    End Sub

    Protected Sub ddSup_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddSup.SelectedIndexChanged
        GVShow.DataBind()
    End Sub

    Protected Sub ddTsr_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddTsr.SelectedIndexChanged
        GVShow.DataBind()
    End Sub
End Class
