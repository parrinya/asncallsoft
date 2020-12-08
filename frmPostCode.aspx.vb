
Partial Class Modules_Manager_Manage_Case_frmPostCode
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        InsertProVince()
        GvProvince.DataBind()
    End Sub

    Protected Sub InsertProVince()
        With SqlZipCode2
            .InsertParameters("ZipCode").DefaultValue = txtZipCode.Text.Trim
            .InsertParameters("ProvinceID").DefaultValue = ddProvince2.SelectedValue
            .InsertParameters("Province").DefaultValue = ddProvince2.SelectedItem.ToString
            .InsertParameters("Dist").DefaultValue = txtDist.Text.Trim
            .InsertParameters("SubDist").DefaultValue = txtSubDist.Text.Trim
            .Insert()
        End With
    End Sub
End Class
