Imports System.Data
Partial Class Modules_Manager_Manage_Case_frmAssgin
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
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(UpdatePanel), UpdatePanel1.ClientID, "alert('" & ex.Message & "');", True)

        End Try

    End Sub

    Protected Function strQuery() As String
        Dim strqry As String = ""
        strqry += " SELECT a2.IdCar FROM [TblSourceGroup] a1 "
        strqry += " Inner Join TblCar a2 on a1.GroupID = a2.GroupID "
        strqry += " Where a2.GroupID=" & ddSourceGroup.SelectedValue
        strqry += "  and a2.CurStatus in (0,1)"
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
End Class
