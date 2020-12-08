
Partial Class Modules_Manager_Manage_Tsr_frmListSendCVandPV
    Inherits System.Web.UI.Page

    Protected Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        If name.Checked And txtname.Text <> "" Then 'ค้นหาตามชื่อ   
            SqlFindData.SelectCommand += " and tblcustomer.FNameTH+' '+tblcustomer.LNameTH like   '%" + txtname.Text + "%'"
            GVShow.DataSource = SqlFindData
            GVShow.DataBind()
        ElseIf carid.Checked And txtcarid.Text <> "" Then
            SqlFindData.SelectCommand += " and tblcar.carid like   '%" + txtcarid.Text + "%'"
            GVShow.DataSource = SqlFindData
            GVShow.DataBind()
        ElseIf appid.Checked And txtappid.Text <> "" Then
            SqlFindData.SelectCommand += " and  tblapplication.AppID =   '%" + txtappid.Text + "%'"
            GVShow.DataSource = SqlFindData
            GVShow.DataBind()
        ElseIf all.Checked Then
            GVShow.DataSource = SqlFindData
            GVShow.DataBind()
        Else
            MsgBox("กรุณากรอกข้อมูลในการค้นหา")
            GVShow.DataBind()
        End If
    End Sub

    Protected Sub GVShow_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GVShow.RowCommand
        ' Insert into TblLogSendCVandPV   INSERT INTO TblLogSendCVandPV(AppID,CreateID) VALUES(@AppID,@CreateID)

        With SqlFindData
            .InsertParameters("AppID").DefaultValue = GVShow.DataKeys(e.CommandArgument).Item(0)
            .Insert()
        End With
        GVShow.DataBind()
    End Sub
End Class
