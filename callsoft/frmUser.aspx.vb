Imports System.IO

Partial Class Modules_Manager_Manage_Tsr_frmUser
    Inherits System.Web.UI.Page

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click, Button3.Click, Button4.Click
        Response.Redirect("frmEditUser.aspx")
    End Sub

    Protected Sub GvUser_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvUser.RowCommand
        If e.CommandName = "Select" Then
            Dim strLink As String = "?TsrID=" & GvUser.DataKeys(e.CommandArgument).Item(0) & "&Exten=" & GvUser.DataKeys(e.CommandArgument).Item(1)
            Response.Redirect("frmEditUser.aspx" & strLink)
        End If
    End Sub

    Protected Sub GvSup_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvSup.RowCommand
        If e.CommandName = "Select" Then
            Dim strLink As String = "?TsrID=" & GvSup.DataKeys(e.CommandArgument).Item(0) & "&Exten=" & GvSup.DataKeys(e.CommandArgument).Item(1)
            Response.Redirect("frmEditUser.aspx" & strLink)
        End If
    End Sub

    Protected Sub GvLead_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvLead.RowCommand
        If e.CommandName = "Select" Then
            Dim strLink As String = "?TsrID=" & GvLead.DataKeys(e.CommandArgument).Item(0) & "&Exten=" & GvLead.DataKeys(e.CommandArgument).Item(1)
            Response.Redirect("frmEditUser.aspx" & strLink)
        End If
    End Sub

    Protected Sub Button6_Click(sender As Object, e As System.EventArgs) Handles Button6.Click
        If GvUserReport.Rows.Count > 0 Then
            Dim attachment As String = "attachment; filename=UserList.xls"
            Response.ClearContent()
            Response.AddHeader("content-disposition", attachment)
            Response.ContentType = "application/ms-excel"

            Dim sw As New StringWriter
            Dim htw As New HtmlTextWriter(sw)
            Dim frm As New HtmlForm
            GvUserReport.Parent.Controls.Add(frm)
            frm.Attributes("runat") = "server"
            frm.Controls.Add(GvUserReport)
            frm.RenderControl(htw)
            Response.Write(sw.ToString())
            Response.End()
        End If
    End Sub


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.Cookies("UserLevel").Value = 1 Then
                TabContainer1.Tabs.Item(3).Visible = True
            Else
                TabContainer1.Tabs.Item(3).Visible = False
            End If
        End If
    End Sub
End Class
