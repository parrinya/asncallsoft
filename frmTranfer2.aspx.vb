
Partial Class Modules_Manager_Manage_Case_frmTranfer2
    Inherits System.Web.UI.Page

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        SqlCustomer.SelectParameters.Item("SearchTH").DefaultValue = "%" & txtSearch.Text.Trim & "%"
        GvCus.DataBind()
    End Sub

    Protected Sub CalAssign()
        Dim i As Integer
        Dim ChkSelect As CheckBox
        For i = 0 To GvCus.Rows.Count - 1
            ChkSelect = DirectCast(GvCus.Rows(i).Cells(0).FindControl("ChkUser"), CheckBox)
            If ChkSelect.Checked = True Then
                SaveAssignTblCustomer(GvCus.DataKeys(i).Item(0))
            End If
        Next
    End Sub

    'Update Assign ให้ Tsr
    Protected Sub SaveAssignTblCustomer(ByVal CusID As String)


        With SqlUser
            .UpdateParameters("AssignTo").DefaultValue = ddTsr1.SelectedValue
            .UpdateParameters("IdCar").DefaultValue = CusID
            .Update()

        End With

        With SqlCustomer
            .UpdateParameters("IdCar").DefaultValue = CusID
            .UpdateParameters("userID").DefaultValue = ddTsr1.SelectedValue
            .Update()
        End With

    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        CalAssign()
        GvCus.DataBind()
    End Sub
End Class
