
Partial Class Modules_Sale_Phone_frmCaseCall4
    Inherits System.Web.UI.Page
    Dim FunAll As New FuntionAll
    Protected Sub GvCase_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvCase.RowCommand
        If e.CommandName = "Select" Then

        ElseIf e.CommandName = "Phone" Then

            Dim strLink As String = "?IdCar=" & GvCase.DataKeys(e.CommandArgument).Item(0)
            strLink += "&&RunNo=" & GvCase.DataKeys(e.CommandArgument).Item(5)
            strLink += "&Call=1"
            If GvCase.DataKeys(e.CommandArgument).Item(3).ToString = "0" Then
                Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "script", "<script>  javascript:parent.change_parent_url('frmPhone.aspx" & strLink & "');</script>")
                'Response.Redirect("frmPhone.aspx" & strLink)
            ElseIf GvCase.DataKeys(e.CommandArgument).Item(3).ToString = "Pending" Then
                strLink += "&&AppID=" & GvCase.DataKeys(e.CommandArgument).Item(4)
                Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "script", "<script>  javascript:parent.change_parent_url('../Pending/frmPending.aspx" & strLink & "');</script>")
                'Response.Redirect("frmPending.aspx" & strLink & "&&AppID=" & GvCase.DataKeys(e.CommandArgument).Item(4))
            ElseIf GvCase.DataKeys(e.CommandArgument).Item(3).ToString = "2" Then
                strLink += "&&AppID=" & GvCase.DataKeys(e.CommandArgument).Item(4)
                Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "script", "<script>  javascript:parent.change_parent_url('../Pending/frmPending.aspx" & strLink & "');</script>")
                'Response.Redirect("frmPending.aspx" & strLink & "&&AppID=" & GvCase.DataKeys(e.CommandArgument).Item(4))
            ElseIf GvCase.DataKeys(e.CommandArgument).Item(3).ToString = "10" Then
                strLink += "&&AppID=" & GvCase.DataKeys(e.CommandArgument).Item(4)
                Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "script", "<script>  javascript:parent.change_parent_url('../Pending/frmQc.aspx" & strLink & "');</script>")
                'Response.Redirect("../Pending/frmQc.aspx" & strLink & "&&AppID=" & GvCase.DataKeys(e.CommandArgument).Item(4))
            ElseIf GvCase.DataKeys(e.CommandArgument).Item(3).ToString = "4" Then
                strLink += "&&AppID=" & GvCase.DataKeys(e.CommandArgument).Item(4)
                Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "script", "<script>  javascript:parent.change_parent_url('../Pending/frmCallCenter.aspx" & strLink & "');</script>")
                'Response.Redirect("../Pending/frmCallCenter.aspx" & strLink & "&&AppID=" & GvCase.DataKeys(e.CommandArgument).Item(4))
            End If
        End If
    End Sub



    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If lblSec.Text > 0 And chkCall.Checked = False Then
            lblSec.Text -= 1
        Else
            If GvCase.Rows.Count > 0 And chkCall.Checked = False Then
                LinkGv()
            End If
        End If
    End Sub

    Protected Sub LinkGv()
        Dim strLink As String = "?IdCar=" & GvCase.DataKeys(0).Item(0)
        strLink += "&&RunNo=" & GvCase.DataKeys(0).Item(5)
        strLink += "&Call=1"


        If GvCase.DataKeys(0).Item(3).ToString = "0" Then
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(UpdatePanel), UpdatePanel1.ClientID, "javascript:parent.change_parent_url('frmPhone.aspx" & strLink & "');", True)
            'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "script", "<script>  </script>")
            'Response.Redirect("frmPhone.aspx" & strLink)
        ElseIf GvCase.DataKeys(0).Item(3).ToString = "Pending" Then
            strLink += "&&AppID=" & GvCase.DataKeys(0).Item(4)
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(UpdatePanel), UpdatePanel1.ClientID, "javascript:parent.change_parent_url('../Pending/frmPending.aspx" & strLink & "');", True)

            'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "script", "<script>  javascript:parent.change_parent_url('frmPending.aspx" & strLink & "');</script>")
            'Response.Redirect("frmPending.aspx" & strLink & "&&AppID=" & GvCase.DataKeys(e.CommandArgument).Item(4))
        ElseIf GvCase.DataKeys(0).Item(3).ToString = "2" Then
            strLink += "&&AppID=" & GvCase.DataKeys(0).Item(4)
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(UpdatePanel), UpdatePanel1.ClientID, "javascript:parent.change_parent_url('../Pending/frmPending.aspx" & strLink & "');", True)

            'Response.Redirect("frmPending.aspx" & strLink & "&&AppID=" & GvCase.DataKeys(e.CommandArgument).Item(4))
        ElseIf GvCase.DataKeys(0).Item(3).ToString = "10" Then
            strLink += "&&AppID=" & GvCase.DataKeys(0).Item(4)
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(UpdatePanel), UpdatePanel1.ClientID, "javascript:parent.change_parent_url('../Pending/frmQc.aspx" & strLink & "');", True)

            'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "script", "<script>  javascript:parent.change_parent_url('../Pending/frmQc.aspx" & strLink & "');</script>")
            'Response.Redirect("../Pending/frmQc.aspx" & strLink & "&&AppID=" & GvCase.DataKeys(e.CommandArgument).Item(4))
        ElseIf GvCase.DataKeys(0).Item(3).ToString = "4" Then
            strLink += "&&AppID=" & GvCase.DataKeys(0).Item(4)
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(UpdatePanel), UpdatePanel1.ClientID, "javascript:parent.change_parent_url('../Pending/frmCallCenter.aspx" & strLink & "');", True)

            'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "script", "<script>  javascript:parent.change_parent_url('../Pending/frmCallCenter.aspx" & strLink & "');</script>")
            'Response.Redirect("../Pending/frmCallCenter.aspx" & strLink & "&&AppID=" & GvCase.DataKeys(e.CommandArgument).Item(4))

        End If
    End Sub

    Protected Sub chkCall_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCall.CheckedChanged
        Session("chkCall") = chkCall.Checked
    End Sub


    Protected Sub GvCase_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvCase.DataBound


        Select Case Request.Cookies("SupID").Value
            Case "5672"
                For i As Integer = 1 To GvCase.Rows.Count - 1
                    Dim ImageButton4 As Button = FunAll.ObjFindControl("Button4", GvCase.Rows(i).Cells(0))
                    ImageButton4.Visible = True
                Next
            Case Else
                For i As Integer = 1 To GvCase.Rows.Count - 1
                    Dim ImageButton4 As Button = FunAll.ObjFindControl("Button4", GvCase.Rows(i).Cells(0))
                    ImageButton4.Visible = False
                Next
        End Select

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("chkCall") IsNot Nothing Then
                chkCall.Checked = Session("chkCall")

            Else
                If Request.Cookies("TypeTsr").Value = 3 Then
                    chkCall.Checked = True
                End If
            End If
        End If
    End Sub
End Class
