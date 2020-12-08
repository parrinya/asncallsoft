Imports System.Data
Imports System.Data.SqlClient
Partial Class Modules_Manager_Manage_Case_frmAddNew
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim StrQuery As New QuerySupAssign
    Dim DataAccess As New DataAccess
    Dim FunAll As New FuntionAll
    Dim StrQryProvince As New QueryProvince
    Dim ISODate As New ISODate
    Dim Connection As New SqlConnection(ConfigurationManager.ConnectionStrings("asnbroker").ConnectionString)
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        With SqlCheckDup
            .SelectParameters("FNameTH").DefaultValue = txtFNameTH.Text.Trim
            .SelectParameters("LNameTH").DefaultValue = txtLNameTH.Text.Trim

        End With
        ViewState("display") = True
        GvCustomer.DataBind()

    End Sub

    Protected Sub GvCustomer_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvCustomer.RowCommand
        If e.CommandName = "Select" Then
            SqlCar.SelectParameters("CusID").DefaultValue = e.CommandArgument
            frmCustomer.DataBind()
            Panel1.Visible = True
        End If
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        If chkCarID() = True Then
            If frmCustomer.DataItemCount > 0 Then
                UpdateTblCustomer()
                InsertTblCar(frmCustomer.DataKey.Item(0))
            Else
                InsertTblCustomer()
                InsertTblCar(chkCusID)


            End If
            MsgBox("ดำเนินการเรียบร้อย")
            Panel1.Visible = False
        End If

    End Sub

    Protected Sub InsertTblCustomer()
        Dim txtFNameTH As TextBox = FunAll.ObjFindControl("txtFNameTH", frmCustomer)
        Dim txtLNameTH As TextBox = FunAll.ObjFindControl("txtLNameTH", frmCustomer)
        Dim txtTel As TextBox = FunAll.ObjFindControl("txtTel", frmCustomer)
        Dim txtOTel As TextBox = FunAll.ObjFindControl("txtOTel", frmCustomer)
        Dim txtMobile As TextBox = FunAll.ObjFindControl("txtMobile", frmCustomer)


        Dim cmd As SqlCommand
        Dim CusID As System.Nullable(Of Integer)
        Try
            Dim strSQL As New System.Text.StringBuilder()

             Connection.Open()

            strSQL.AppendLine("INSERT INTO TblCustomer(FNameTH, LNameTH, Tel, OTel, Mobile, CreateID) ")
            strSQL.AppendLine("  ")
            strSQL.AppendLine(" VALUES ")
            strSQL.AppendLine(" (")
            strSQL.AppendLine(" '" & txtFNameTH.Text.Trim & "'")
            strSQL.AppendLine(", '" & txtLNameTH.Text.Trim & "'")
            strSQL.AppendLine(", '" & txtTel.Text.Trim & "'")
            strSQL.AppendLine(", '" & txtOTel.Text.Trim & "'")
            strSQL.AppendLine(", '" & txtMobile.Text.Trim & "'")
            strSQL.AppendLine(", " & Request.Cookies("userID").Value & "")
            strSQL.AppendLine(" ); ")
            strSQL.AppendLine(" SELECT Scope_Identity() ")
            cmd = New SqlCommand(strSQL.ToString(), Connection)
            'เลขที่ CusID

            Dim obj As Object = cmd.ExecuteScalar()
            Dim ChkInsert As Boolean = False
            If obj IsNot Nothing AndAlso obj IsNot DBNull.Value Then
                CusID = Convert.ToInt32(obj)
                ChkInsert = True


            End If

            If ChkInsert Then
                Dim ddListStation As DropDownList = FunAll.ObjFindControl("ddListStation", frmCustomer)
                Dim txtFNameTHSource As TextBox = FunAll.ObjFindControl("txtFNameTHSource", frmCustomer)
                Dim txtLNameTHSource As TextBox = FunAll.ObjFindControl("txtLNameTHSource", frmCustomer)

                strSQL = New System.Text.StringBuilder()
                strSQL.AppendLine(" INSERT INTO TblAddNewSourceData (CusId,SourceDataID ,Fname,Lname) ")
                strSQL.AppendLine(" VALUES ")
                strSQL.AppendLine(" (")
                strSQL.AppendLine(" " & CusID & "")
                strSQL.AppendLine(", '" & ddListStation.SelectedValue & "'")
                strSQL.AppendLine(", '" & txtFNameTHSource.Text.Trim & "'")
                strSQL.AppendLine(", '" & txtLNameTHSource.Text.Trim & "'")
                strSQL.AppendLine(" ); ")
                strSQL.AppendLine(" SELECT Scope_Identity() ")
                cmd = New SqlCommand(strSQL.ToString(), Connection)
                cmd.ExecuteNonQuery()
            End If
        Catch ex As Exception
           
        Finally

            If Connection Is Nothing Then
                Connection = New SqlConnection
            End If

            If Connection.State = ConnectionState.Open Then
                Connection.Close()
            Else
                Connection.Open()
            End If

        End Try

    End Sub



    Protected Sub UpdateTblCustomer()
        Dim txtTel As TextBox = FunAll.ObjFindControl("txtTel", frmCustomer)
        Dim txtOTel As TextBox = FunAll.ObjFindControl("txtOTel", frmCustomer)
        Dim txtMobile As TextBox = FunAll.ObjFindControl("txtMobile", frmCustomer)
        With SqlCustomer
            .UpdateParameters("Tel").DefaultValue = txtTel.Text.Trim
            .UpdateParameters("OTel").DefaultValue = txtOTel.Text.Trim
            .UpdateParameters("Mobile").DefaultValue = txtMobile.Text.Trim
            .UpdateParameters("cusID").DefaultValue = frmCustomer.DataKey.Item(0)
            .Update()
        End With
    End Sub

    Protected Sub InsertTblCar(ByVal CusID As String)
        Dim ddUser As DropDownList = FunAll.ObjFindControl("ddUser", frmCustomer)
        Dim ddCarID As DropDownList = FunAll.ObjFindControl("ddCarID", frmCustomer)
        Dim txtCarID As TextBox = FunAll.ObjFindControl("txtCarID", frmCustomer)
        Dim ddBrand As DropDownList = DirectCast(frmCustomer.FindControl("ddBrand"), DropDownList)
        Dim ddSeries As DropDownList = DirectCast(frmCustomer.FindControl("ddSeries"), DropDownList)
        Dim txtCarBuyDate As TextBox = FunAll.ObjFindControl("txtCarBuyDate", frmCustomer)

        'ตรวจสอบข้อมูล
        Dim strqry As String = "select top 1 CarID,CarSize,CarNo,CarBoxNo,CarType,CarYear,CarBrand,CarSeries,CarBuyDate from tblcar  "
        strqry += " where idcar in (select carid from  [TblNotupdate14] ) and "
        strqry += " carid = '" & txtCarID.Text.Trim & ddCarID.SelectedValue & "'   order by UpdateDate desc "
        dt = New DataTable
        dt = DataAccess.DataRead(strqry)
        If dt.Rows.Count > 0 Then
            '
            With SqlCar_1
                .InsertParameters("CusID").DefaultValue = CusID
                .InsertParameters("AssignTo").DefaultValue = ddUser.SelectedValue
                .InsertParameters("CarID").DefaultValue = txtCarID.Text.Trim & ddCarID.SelectedValue
                .InsertParameters("CarSize").DefaultValue = dt.Rows(0).Item("CarSize")
                .InsertParameters("CarNo").DefaultValue = dt.Rows(0).Item("CarNo")
                .InsertParameters("CarBoxNo").DefaultValue = dt.Rows(0).Item("CarBoxNo")
                .InsertParameters("CarType").DefaultValue = dt.Rows(0).Item("CarType")
                .InsertParameters("CarYear").DefaultValue = dt.Rows(0).Item("CarYear")
                .InsertParameters("CarBrand").DefaultValue = dt.Rows(0).Item("CarBrand")
                .InsertParameters("CarSeries").DefaultValue = dt.Rows(0).Item("CarSeries")
                .InsertParameters("CarSeries").DefaultValue = dt.Rows(0).Item("CarSeries")
                .InsertParameters("CarBuyDate").DefaultValue = dt.Rows(0).Item("CarBuyDate")
                .Insert()

            End With

        Else
            With SqlCar
                .InsertParameters("CusID").DefaultValue = CusID
                .InsertParameters("AssignTo").DefaultValue = ddUser.SelectedValue
                .InsertParameters("CarID").DefaultValue = txtCarID.Text.Trim & ddCarID.SelectedValue
                .InsertParameters("CarBrand").DefaultValue = ddBrand.SelectedValue
                .InsertParameters("CarSeries").DefaultValue = ddSeries.SelectedValue
                .InsertParameters("AssignTo").DefaultValue = ddUser.SelectedValue
                .InsertParameters("CarBuyDate").DefaultValue = ISODate.SetISODate("en", txtCarBuyDate.Text.Trim)
                .Insert()

            End With

        End If        '
       
    End Sub

    Protected Function chkCarID() As Boolean
        Dim ddCarID As DropDownList = FunAll.ObjFindControl("ddCarID", frmCustomer)
        Dim txtCarID As TextBox = FunAll.ObjFindControl("txtCarID", frmCustomer)
        Dim strqry As String = "Select * from TblCar Where CarID = '" & txtCarID.Text.Trim & ddCarID.SelectedValue & "' "
        strqry += " and (CurStatus not in(12)) and idcar not in (select carid from  [TblNotupdate14] ) "
        dt = New DataTable
        dt = DataAccess.DataRead(strqry)
        If dt.Rows.Count > 0 Then
            MsgBox("เลขทะเบียนซ้ำในระบบ")
            Return False
        Else
            Return True

        End If
    End Function

    Protected Function chkCusID() As String
        Dim strqry As String = "SELECT Top 1 * from TblCustomer Where CreateID = " & Request.Cookies("userID").Value & " Order by CreateDate DESC "
        dt = New DataTable
        dt = DataAccess.DataRead(strqry)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item("CusID").ToString
        Else
            Return 0
        End If
    End Function

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


    Protected Sub GvCustomer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvCustomer.Load

    End Sub

    Protected Sub GvCustomer_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvCustomer.DataBound
        If ViewState("display") IsNot Nothing And GvCustomer.Rows.Count = 0 Then
            Panel1.Visible = True
            SqlCar.SelectParameters("CusID").DefaultValue = "0"
            frmCustomer.DataBind()
        End If
    End Sub

    Protected Sub ddBrand_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        BindCarSeries()

    End Sub
    Protected Sub BindCarSeries()
        Dim ddBrand As DropDownList = DirectCast(frmCustomer.FindControl("ddBrand"), DropDownList)
        Dim ddSeries As DropDownList = DirectCast(frmCustomer.FindControl("ddSeries"), DropDownList)
       
        FunAll.ListDropDown(ddSeries, StrQryProvince.SelectCarBrand(ddBrand.SelectedValue), "CarSeries", "CarSeries")

    End Sub
End Class
