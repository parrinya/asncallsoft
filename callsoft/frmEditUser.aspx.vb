Imports System.Data
Imports Infragistics.WebUI.WebDataInput
Imports AjaxControlToolkit
Imports System.Globalization

Partial Class Modules_Manager_Manage_Tsr_frmEditUser
    Inherits System.Web.UI.Page
    Dim FunAll As New FuntionAll
    Dim ChkPassword As New ChkLogin
    Dim proxy As New asnserverbk.WebService
    Dim ISODate As New ISODate
    Dim DataAccess As New DataAccess
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim mnList As Menu = CType(Master.FindControl("NavigationMenu"), Menu)
        mnList.Visible = False
        If Not IsPostBack Then
            ViewState("StartTime") = DateTime.Now
        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Response.Redirect("frmUser.aspx")
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim txtStartWork As TextBox = FunAll.ObjFindControl("txtStartWork_add", frmUser)
        Dim lblstartwork As Label = FunAll.ObjFindControl("lblstartwork", frmUser)

            Try
                Dim chkPassword As CheckBox = FunAll.ObjFindControl("chkPassword", frmUser)
                If CheckLevel() Then
                    If frmUser.DataItemCount > 0 Then
                        Dim txtUserName As TextBox = FunAll.ObjFindControl("txtUserName", frmUser)
                        If txtUserName.Text.Trim = frmUser.DataKey.Item(1) Then

                            'ตรวจสอบว่า แก้ไข password หรือไม่
                            If chkPassword.Checked Then
                                'ตรวจสอบ การตั้ง Password
                            If CheckFormat() Then
                                UpdateTblUser()
                                Response.Redirect("frmUser.aspx")
                            End If
                            Else
                                UpdateTblUser()
                                Response.Redirect("frmUser.aspx")
                            End If
                        Else
                            If chkUser() = True Then
                                'ตรวจสอบว่า แก้ไข password หรือไม่
                                If chkPassword.Checked Then
                                    'ตรวจสอบ การตั้ง Password
                                If CheckFormat() Then
                                    UpdateTblUser()
                                    Response.Redirect("frmUser.aspx")
                                End If
                                Else
                                    UpdateTblUser()
                                    Response.Redirect("frmUser.aspx")
                                End If
                            End If
                        End If
                    Else
                        If chkUser() = True Then
                        If CheckFormat() Then
                            If txtStartWork.Text <> "" Then
                                InsertTblUser()
                                Response.Redirect("frmUser.aspx")
                            Else
                                lblstartwork.Visible = True
                            End If
                        End If
                        End If

                    End If
                End If
            Catch ex As Exception
                MsgBox("ไม่สามารถบันทึกได้เนื่องจาก : " & ex.Message.ToString)
            End Try


    End Sub

    'Script MsgBox
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

    Protected Sub UpdateTblUser()

        Dim txtFNameTH As TextBox = FunAll.ObjFindControl("txtFNameTH", frmUser)
        Dim txtLNameTH As TextBox = FunAll.ObjFindControl("txtLNameTH", frmUser)
        Dim txtNName As TextBox = FunAll.ObjFindControl("txtNName", frmUser)
        Dim txtUserName As TextBox = FunAll.ObjFindControl("txtUserName", frmUser)
        Dim txtPassword As TextBox = FunAll.ObjFindControl("txtPassword", frmUser)
        Dim txtTarget As WebNumericEdit = FunAll.ObjFindControl("txtTarget", frmUser)


        Dim txtStartWork As TextBox = FunAll.ObjFindControl("txtStartWork", frmUser)
        Dim StartWork As String = ""

        If txtStartWork.Text <> "" Then
            StartWork = ISODate.SetISODate("en", txtStartWork.Text.Trim)
        End If



        Dim ddUserLevel As DropDownList = FunAll.ObjFindControl("ddUserLevel", frmUser)
        Dim ddLead As DropDownList = FunAll.ObjFindControl("ddLead", frmUser)
        Dim ddSup As DropDownList = FunAll.ObjFindControl("ddSup", frmUser)
        Dim ddGroup As DropDownList = FunAll.ObjFindControl("ddGroup", frmUser)
        Dim ddExten As DropDownList = FunAll.ObjFindControl("ddExten", frmUser)
        Dim ddtsrGRADE As DropDownList = FunAll.ObjFindControl("ddtsrGRADE", frmUser)

        Dim chkPassword As CheckBox = FunAll.ObjFindControl("chkPassword", frmUser)
        Dim chkStatus As CheckBox = FunAll.ObjFindControl("chkStatus", frmUser)
        Dim userstatus As Integer = 0
        If chkStatus.Checked Then
            userstatus = 1
        Else
            If frmUser.DataKey.Item(2) = 2 Then
                userstatus = 2
            ElseIf frmUser.DataKey.Item(2) = 1 Then
                userstatus = 1
            End If
        End If

        With SqlUser
            .UpdateParameters("UserName").DefaultValue = txtUserName.Text.Trim
            .UpdateParameters("chkPass").DefaultValue = chkPassword.Checked
            .UpdateParameters("UserPassword").DefaultValue = proxy.EncrytePassword(txtPassword.Text.Trim)
            .UpdateParameters("FName").DefaultValue = txtFNameTH.Text.Trim
            .UpdateParameters("LName").DefaultValue = txtLNameTH.Text.Trim
            .UpdateParameters("NName").DefaultValue = txtNName.Text.Trim
            .UpdateParameters("UserLevelID").DefaultValue = ddUserLevel.SelectedValue
            .UpdateParameters("LeaderID").DefaultValue = ddLead.SelectedValue
            .UpdateParameters("SupID").DefaultValue = ddSup.SelectedValue
            .UpdateParameters("UserStatus").DefaultValue = userstatus
            .UpdateParameters("UserType").DefaultValue = ddGroup.SelectedValue
            .UpdateParameters("Exten").DefaultValue = ddExten.SelectedValue
            .UpdateParameters("TargetSale").DefaultValue = txtTarget.Text.Trim
            .UpdateParameters("tsrGRADE").DefaultValue = ddtsrGRADE.SelectedValue
            .UpdateParameters("UserID").DefaultValue = frmUser.DataKey.Item(0)
            .UpdateParameters("StartWork").DefaultValue = StartWork
            .Update()
        End With

    End Sub

    Protected Sub InsertTblUser()
        Dim txtFNameTH As TextBox = FunAll.ObjFindControl("txtFNameTH", frmUser)
        Dim txtLNameTH As TextBox = FunAll.ObjFindControl("txtLNameTH", frmUser)
        Dim txtNName As TextBox = FunAll.ObjFindControl("txtNName", frmUser)
        Dim txtUserName As TextBox = FunAll.ObjFindControl("txtUserName", frmUser)
        Dim txtPassword As TextBox = FunAll.ObjFindControl("txtPassword", frmUser)
        Dim txtTarget As WebNumericEdit = FunAll.ObjFindControl("txtTarget", frmUser)

        'Dim date1 As String
        Dim txtStartWork_add As TextBox = FunAll.ObjFindControl("txtStartWork_add", frmUser)
        'Dim StartWork As String = ISODate.SetISODate("en", txtStartWork.Text.Trim)

        Dim ddUserLevel As DropDownList = FunAll.ObjFindControl("ddUserLevel", frmUser)
        Dim ddLead As DropDownList = FunAll.ObjFindControl("ddLead", frmUser)
        Dim ddSup As DropDownList = FunAll.ObjFindControl("ddSup", frmUser)
        Dim ddGroup As DropDownList = FunAll.ObjFindControl("ddGroup", frmUser)
        Dim ddExten As DropDownList = FunAll.ObjFindControl("ddExten", frmUser)
        Dim ddtsrGRADE As DropDownList = FunAll.ObjFindControl("ddtsrGRADE", frmUser)

        Dim chkPassword As CheckBox = FunAll.ObjFindControl("chkPassword", frmUser)
        Dim chkStatus As CheckBox = FunAll.ObjFindControl("chkStatus", frmUser)

        With SqlUser
            .InsertParameters("UserName").DefaultValue = txtUserName.Text.Trim
            .InsertParameters("UserPassword").DefaultValue = proxy.EncrytePassword(txtPassword.Text.Trim)
            .InsertParameters("FName").DefaultValue = txtFNameTH.Text.Trim
            .InsertParameters("LName").DefaultValue = txtLNameTH.Text.Trim
            .InsertParameters("NName").DefaultValue = txtNName.Text.Trim
            .InsertParameters("UserLevelID").DefaultValue = ddUserLevel.SelectedValue
            .InsertParameters("LeaderID").DefaultValue = ddLead.SelectedValue
            .InsertParameters("SupID").DefaultValue = ddSup.SelectedValue
            .InsertParameters("UserType").DefaultValue = ddGroup.SelectedValue
            .InsertParameters("TargetSale").DefaultValue = txtTarget.Text.Trim
            .InsertParameters("tsrGRADE").DefaultValue = ddtsrGRADE.SelectedValue
            .InsertParameters("Exten").DefaultValue = ddExten.SelectedValue
            .InsertParameters("StartWork").DefaultValue = ISODate.SetISODate("en", txtStartWork_add.Text.Trim)
            .Insert()

        End With
    End Sub

    Protected Function chkUser() As Boolean
        Dim txtUserName As TextBox = FunAll.ObjFindControl("txtUserName", frmUser)
        Dim strqry As String = "Select * from TblUser where Username = '" & txtUserName.Text.Trim & "'"
        Dim dt As New DataTable
        Dim DataAccess As New DataAccess
        dt = DataAccess.DataRead(strqry)
        If dt.Rows.Count > 0 Then
            MsgBox("Username ของคุณซ้ำกับบุคคลอื่นในระบบ")
            Return False
        Else
            Return True
        End If
    End Function
    'add by na 20150331
    Protected Function CheckFormat() As Boolean
        Dim txtPassword As TextBox = FunAll.ObjFindControl("txtPassword", frmUser)
        Dim arrerror As New ArrayList
        Dim dt As New DataTable
        Dim SreError As String = "กรุณาตรวจสอบความถูกต้อง ดังนี้ \n------------------------------------\n"
        Dim ChkBool As Boolean = True
        arrerror = ChkPassword.CheckFormat(txtPassword.Text.Trim)
        If arrerror.Count > 0 Then
            ChkBool = False
            For cellCtr = 0 To arrerror.Count - 1
                SreError += arrerror(cellCtr).ToString + "\n"

            Next
        End If

        If frmUser.DataItemCount > 0 Then
            'Hitory 4
            Dim strqry As New System.Text.StringBuilder()
            'strqry = New System.Text.StringBuilder()
            'strqry.Append(" select  top 4 TblLogPassword.UserPassword  ")
            'strqry.Append(" FROM TblLogPassword")
            'strqry.Append(" where TblLogPassword.userID = " & frmUser.DataKey.Item(0))
            'strqry.Append(" order by CreateDate desc")
            'แก้ไข ตาม Policy Company With 
            strqry.Append(" select UserPassword from tbluser  ")
            strqry.Append(" where userid = " & frmUser.DataKey.Item(0))
            dt = New DataTable
            dt = DataAccess.DataRead(strqry.ToString)
            For cellCtr = 0 To dt.Rows.Count - 1
                If txtPassword.Text.Trim = proxy.DecrytePassword(dt.Rows(cellCtr).Item("UserPassword").ToString()) Then
                    ChkBool = False
                    SreError += "- รหัสผ่านซ้ำกับรอบที่ผ่านมา" + "\n"
                    Exit For
                End If
            Next
        End If


        If ChkBool = False Then
            MsgBox(SreError.ToString)
            Return False
        Else
            Return True
        End If
    End Function
    'Protected Function CheckPassword() As Boolean
    '    Dim txtPassword As TextBox = FunAll.ObjFindControl("txtPassword", frmUser)
    '    If ChkPassword.CheckPassword(txtPassword.Text.Trim) Then
    '        Return True
    '    Else
    '        MsgBox("ความยาว Password ต้อง6-8 หลัก")
    '        Return False
    '    End If
    'End Function

    Protected Function CheckLevel() As Boolean
        Dim ddUserLevel As DropDownList = FunAll.ObjFindControl("ddUserLevel", frmUser)
        Dim ddLead As DropDownList = FunAll.ObjFindControl("ddLead", frmUser)
        Dim ddSup As DropDownList = FunAll.ObjFindControl("ddSup", frmUser)
        If ddUserLevel.SelectedValue = 5 Then
            If ddSup.SelectedValue = 0 Or ddLead.SelectedValue = 0 Then
                MsgBox("กรุณาระบุ ชื่อSUP และ ชื่อLEAD")
                Return False
            Else
                Return True
            End If
        Else
            Return True
        End If
    End Function
    'End add by na 20150331

    Protected Sub chkStatus_DataBinding(sender As Object, e As System.EventArgs)
        Dim lbltxt As Label = FunAll.ObjFindControl("lbltxt", frmUser)
        lbltxt.Visible = True
        If frmUser.DataItemCount > 0 Then
            Dim chk As Integer = frmUser.DataKey.Item(2)
            If chk = 0 Then
                lbltxt.Text = "รออนุมัติการใช้งานจากทางสารสนเทศ"
                Dim chkStatus As CheckBox = FunAll.ObjFindControl("chkStatus", frmUser)
                chkStatus.Visible = False
            ElseIf chk = 1 Then
                Dim chkStatus As CheckBox = FunAll.ObjFindControl("chkStatus", frmUser)
                chkStatus.Visible = False
                lbltxt.Text = "สถานะใช้งาน"
            ElseIf chk = 2 Then
                Dim chkStatus As CheckBox = FunAll.ObjFindControl("chkStatus", frmUser)
                chkStatus.Checked = False
                lbltxt.Text = ""
            End If
        End If
    End Sub

    'Protected Sub frmUser_Load(sender As Object, e As EventArgs) Handles frmUser.Load
    '    If Not IsPostBack Then
    '        Dim txtStartWork_add_CalendarExtender As CalendarExtender = frmUser.FindControl("txtStartWork_add_CalendarExtender")
    '        If txtStartWork_add_CalendarExtender IsNot Nothing Then

    '            txtStartWork_add_CalendarExtender.SelectedDate = Date.Now

    '        End If
    '    End If
    'End Sub

End Class
