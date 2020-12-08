Imports System.Data
Partial Class Modules_Manager_Manage_Case_frmAssginReNew
    Inherits System.Web.UI.Page

    Dim DataAccess As New DataAccess
    'นับจำนวน User ทั้งหมด
    Protected Sub ChkSelectUser()
        Dim RecTsr As Integer = 0
        Dim i As Integer
        Dim ChkSelect As CheckBox
        For i = 0 To GvUser.Rows.Count - 1
            ChkSelect = DirectCast(GvUser.Rows(i).Cells(0).FindControl("ChkUser"), CheckBox)
            If ChkSelect.Checked = True Then
                RecTsr += 1
            End If
        Next

        'lblRecord.Text = RecTsr
    End Sub

    Protected Sub ChkUser_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ChkSelectUser()
    End Sub

    'นับจำนวน User ทั้งหมด เฉพาะคน
    Protected Sub ChkAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim ChkSelect As CheckBox
        Dim ChkAll As CheckBox = DirectCast(GvUser.HeaderRow.Cells(0).FindControl("ChkAll"), CheckBox)
        For i = 0 To GvUser.Rows.Count - 1
            ChkSelect = DirectCast(GvUser.Rows(i).Cells(0).FindControl("ChkUser"), CheckBox)
            If ChkAll.Checked = True Then
                ChkSelect.Checked = True
                'lblRecord.Text = GvUser.Rows.Count
            Else
                ChkSelect.Checked = False
                'lblRecord.Text = 0
            End If
        Next
    End Sub


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If checkAssign() = True Then
                Dim dt As New DataTable
                Dim i As Integer = 0
                Dim j As Integer = 0
                dt = DataAccess.DataRead(strQuery)
                If dt.Rows.Count > 0 Then

                    '*************ดึง UserID ลง ArUser***************
                    Dim ChkSelect As CheckBox
                    Dim ArUser As New ArrayList
                    For i = 0 To GvUser.Rows.Count - 1
                        ChkSelect = DirectCast(GvUser.Rows(i).Cells(0).FindControl("ChkUser"), CheckBox)
                        If ChkSelect.Checked = True Then
                            ArUser.Add(GvUser.DataKeys(i).Item(0))
                        End If
                    Next
                    '**********************************************

                    '***********ส่ง User เพื่อทำการ Assign**************

                    For i = 0 To CInt(txtAssign.Text.Trim) - 1
                        If j = ArUser.Count Then
                            j = 0
                        End If
                        UpdateCustomerAssign(ArUser(j), dt.Rows(i).Item("IdCar").ToString)
                        j = j + 1
                    Next
                    '*********************************************
                End If
                ddSourceGroup.DataBind()
                ddRec.DataBind()
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(UpdatePanel), UpdatePanel1.ClientID, "alert('ดำเนินการเสร็จแล้วค่ะ');", True)
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(UpdatePanel), UpdatePanel1.ClientID, "alert('" & ex.Message & "');", True)

        End Try

    End Sub

    Protected Function strQuery() As String
        Dim str As String = ""
        Dim str1 As String = ""
        Dim strqry As String = ""

       

            strqry += " SELECT a2.IdCar FROM [TblSourceGroup] a1 "
            strqry += " Inner Join TblCar a2 on a1.GroupID = a2.GroupID "

            If ddType.SelectedValue <> 0 Or ddtypeRepair.SelectedValue <> 0 Or rbClam.SelectedValue <> 0 Or ddCompany.SelectedValue <> -1 Then
                strqry += " Inner Join TblImpCaseReNew a3 on a2.idcar = a3.idcar "
            'strqry += " Inner Join TblApplicationu a4 on a3.appid = a4.appid "
            'strqry += " Inner Join TblAppSubmit a5 on a4.Pkgid = a5.AppsubmitId "
            End If
            strqry += " Where a2.GroupID=" & ddSourceGroup.SelectedValue
            strqry += "  and a2.CurStatus in (0,1)"
            If ddType.SelectedValue <> 0 Then
            strqry += " and  a3.typeProvalue = " + ddType.SelectedValue
            End If
            If ddtypeRepair.SelectedValue <> 0 Then
                If ddtypeRepair.SelectedValue = 1 Then
                    str = "อู่"
                Else
                    str = "ห้าง"
                End If
                strqry += " And a3.CarFixIN = '" + str + "'"

            End If
            If ddCompany.SelectedValue <> -1 And ddCompany.SelectedValue <> 0 Then
            strqry += " and a3.proid = '" + ddCompany.SelectedValue + "'"

            ElseIf ddCompany.SelectedValue = 0 Then 'ทุก บ.
                strqry += "  "
            End If
            If rbClam.SelectedValue <> 0 Then
                If rbClam.SelectedValue = 1 Then
                    strqry += " and a3.claim = 0"
                ElseIf rbClam.SelectedValue = 2 Then
                    strqry += " and a3.claim > 0"
                End If
            End If
            If Request.Cookies("UserLevel").Value = 1 Then
                strqry += "  and a2.AssignTo = 0"
            Else
                strqry += "  and a2.AssignTo = " & Request.Cookies("userID").Value
            End If

        Return strqry

    End Function

    'Save Assign
    Protected Sub UpdateCustomerAssign(ByVal strUser As String, ByVal strCusID As String)


        With SqlUser
            .UpdateParameters("AssignTo").DefaultValue = strUser
            .UpdateParameters("IdCar").DefaultValue = strCusID
            .Update()

        End With


    End Sub


    Protected Function checkAssign() As Boolean
        If CInt(txtAssign.Text) > CInt(ddRec.SelectedValue) Then
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(UpdatePanel), UpdatePanel1.ClientID, "alert('จำนวน Assign มากกว่าจำนวน Record');", True)

            Return False
        Else
            Return True
        End If
    End Function

    Protected Sub ddType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddType.SelectedIndexChanged
        showrec()
    End Sub
    Protected Sub showrec()
        Dim str As String = ""
                With SqlRec
                    .SelectCommand = "  SELECT Count(a2.IdCar) as RecCar FROM [TblSourceGroup] a1 "
                    .SelectCommand += " Inner Join TblCar a2 on a1.GroupID = a2.GroupID "
                    If ddType.SelectedValue <> 0 Or ddtypeRepair.SelectedValue <> 0 Or rbClam.SelectedValue <> 0 Or ddCompany.SelectedValue <> -1 Then
                        .SelectCommand += " Inner Join TblImpCaseReNew a3 on a2.idcar = a3.idcar "
              
                    End If
                    .SelectCommand += " Where a2.GroupID=" + ddSourceGroup.SelectedValue
            If ddType.SelectedValue <> 0 Then
                .SelectCommand += "  and  a3.typeProvalue=" + ddType.SelectedValue
            End If
                    If ddtypeRepair.SelectedValue <> 0 Then
                        If ddtypeRepair.SelectedValue = 1 Then
                    str = "อู่"

                        Else
                            str = "ห้าง"
                        End If
                        .SelectCommand += "  And a3.CarFixIN = '" + str + "'"

                    End If
                    If ddCompany.SelectedValue <> -1 And ddCompany.SelectedValue <> 0 Then
                .SelectCommand += "  and a3.proid = '" + ddCompany.SelectedValue + "'"
                    ElseIf ddCompany.SelectedValue = 0 Then 'ทุก บ.
                        .SelectCommand += "  "
                    End If
                    If rbClam.SelectedValue <> 0 Then
                        If rbClam.SelectedValue = 1 Then
                            .SelectCommand += " and a3.claim = 0"
                        ElseIf rbClam.SelectedValue = 2 Then
                            .SelectCommand += " and a3.claim > 0"
                        End If
                    End If

                    If Request.Cookies("UserLevel").Value = 1 Then
                        .SelectCommand += " and a2.CurStatus in (0,1) and a2.AssignTo = 0"
                    Else
                        .SelectCommand += " and a2.CurStatus in (0,1) and a2.AssignTo = " & Request.Cookies("userID").Value
                    End If
                End With


            ddRec.DataBind()
    End Sub

    Protected Sub rbClam_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rbClam.SelectedIndexChanged
        showrec()
    End Sub

    Protected Sub ddtypeRepair_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddtypeRepair.SelectedIndexChanged
        showrec()
    End Sub

    Protected Sub ddCompany_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddCompany.SelectedIndexChanged
        showrec()
    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub
End Class


