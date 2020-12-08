Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Class Modules_Manager_Report_frmSaleSource
    Inherits System.Web.UI.Page
    Public strReport As String
    Protected Sub btn1_Click(sender As Object, e As System.EventArgs) Handles btn1.Click
        If rdt1.Checked Then
            Dim str As String = "Source Name :"
            str += Me.ddSource.SelectedItem.ToString
            strReport = "SaleSource.aspx?rdt=1&ID=" & Me.ddSource.SelectedValue & "&name=" & str
        ElseIf rdt2.Checked Then
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                Dim str As String = "Protect Date :Start "
                Dim assSelect As Int16 = 0
                Dim assSelect2 As Int16 = 0
                Dim assSelect3 As Int16 = 0
                str += Me.txtdate1.Text + " To " + Me.txtdate2.Text
                If Request.Cookies("UserLevel").Value = 1 Then
                    assSelect = Me.ddUser.SelectedValue
                    'add dropdown sup
                    assSelect2 = Me.ddSup.SelectedValue
                    assSelect3 = 0
                ElseIf Request.Cookies("UserLevel").Value = 2 Then
                    assSelect = 0
                    assSelect2 = 0
                    assSelect3 = Me.ddSup2.SelectedValue
                Else
                    assSelect = 0
                    assSelect2 = 0
                    assSelect3 = 0
                End If
                strReport = "SaleSource.aspx?rdt=2&name=" & str & "&txtDate1=" & Me.txtdate1.Text & "&txtDate2=" & Me.txtdate2.Text & "&assig=" & assSelect & "&assig2=" & assSelect2 & "&assig3=" & assSelect3
            Else
                MsgBox("กรุณากรอกข้อมูลวันที่ให้ครบ")
            End If
        ElseIf rdt3.Checked Then
            If txtdate3.Text <> "" And txtdate4.Text <> "" Then
                Dim str As String = "Assign Date :Start "
                Dim assSelect As Int16 = 0
                Dim assSelect2 As Int16 = 0
                Dim assSelect3 As Int16 = 0
                str += Me.txtdate3.Text + " To " + Me.txtdate4.Text
                If Request.Cookies("UserLevel").Value = 1 Then
                    assSelect = Me.ddUser2.SelectedValue
                    'add dropdown sup
                    assSelect2 = Me.ddSup3.SelectedValue
                    assSelect3 = 0
                ElseIf Request.Cookies("UserLevel").Value = 2 Then
                    assSelect = 0
                    assSelect2 = 0
                    assSelect3 = Me.ddSup4.SelectedValue
                Else
                    assSelect = 0
                    assSelect2 = 0
                    assSelect3 = 0
                End If
                strReport = "SaleSource.aspx?rdt=3&name=" & str & "&txtDate3=" & Me.txtdate3.Text & "&txtDate4=" & Me.txtdate4.Text & "&assig=" & assSelect & "&assig2=" & assSelect2 & "&assig3=" & assSelect3
            Else
                MsgBox("กรุณากรอกข้อมูลวันที่ให้ครบ")
            End If
        Else
            MsgBox("กรุณาเลือกประเภทการค้นหา")
        End If

    End Sub
    Public Sub MsgBox(ByVal sMsg As String)

        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control
        sMsg = sMsg.Replace("'", "\'")
        sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
        sMsg = sMsg.Replace(vbCrLf, "\n")
        sMsg = "<script language=javascript>alert(""" & sMsg & """)</script>"

        sb = New StringBuilder()
        sb.Append(sMsg)

        For Each oFormObject In Me.Controls
            If TypeOf oFormObject Is HtmlForm Then
                Exit For
            End If
        Next

        ' Add the javascript after the form object so that the 
        ' message doesn't appear on a blank screen.
        oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        If Request.Cookies("UserLevel").Value = 3 Then
            ddSup.Visible = False
            ddUser.Visible = False
            ddSup2.Visible = False
            ddUser2.Visible = False
            ddSup3.Visible = False
            ddSup4.Visible = False
        ElseIf Request.Cookies("UserLevel").Value = 2 Then
            ddUser.Visible = False
            ddSup.Visible = False
            ddUser2.Visible = False
            ddSup3.Visible = False
        ElseIf Request.Cookies("UserLevel").Value = 1 Then
            ddSup2.Visible = False
            ddSup4.Visible = False
        End If
    End Sub

    Protected Sub rdt1_CheckedChanged(sender As Object, e As EventArgs) Handles rdt1.CheckedChanged
        rdt2.Checked = False
        rdt3.Checked = False
    End Sub

    Protected Sub rdt2_CheckedChanged(sender As Object, e As EventArgs) Handles rdt2.CheckedChanged
        rdt1.Checked = False
        rdt3.Checked = False
    End Sub

    Protected Sub rdt3_CheckedChanged(sender As Object, e As EventArgs) Handles rdt3.CheckedChanged
        rdt1.Checked = False
        rdt2.Checked = False
    End Sub
End Class
