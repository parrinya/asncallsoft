Imports System.Data.SqlClient
Imports System.Data
Imports Infragistics.Web
Imports Infragistics.WebUI.WebDataInput
Partial Class Modules_Manager_Manage_Case_frmEditCreditDetail
    Inherits System.Web.UI.Page
    Dim FunAll As New FuntionAll
    Dim StrQryProvince As New QueryProvince
    Dim com As SqlCommand
    Dim Conn As New SqlConnection(ConfigurationManager.ConnectionStrings("asnbroker").ConnectionString)
    Dim StrQueryPhone As New QueryPhone
    Dim dt As DataTable
    Dim StrQuery As New QueryApplication
    Dim ISODate As New ISODate
    Dim DataAccess As New DataAccess
    Dim QueryCredit As New QureyCredit
    Public Sub CheckConnectionState()
        If Conn.State = ConnectionState.Open Then
            Conn.Close()
        Else
            Conn.Open()
        End If
    End Sub
    Protected Sub frmCusName_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmCusName.DataBound
    End Sub

    Protected Sub frmPackage_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmPackage.DataBound

    End Sub
    Protected Sub frmAppRela_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmAppRela.DataBound
    End Sub
    Protected Sub frmOldInsure_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmOldInsure.DataBound
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
    'check การตรวจงานของ Qc ห้ามบันทึก App
    Protected Function chkAppQc() As Boolean
        Dim dvSql As DataView = DirectCast(SqlGetApplication.Select(DataSourceSelectArguments.Empty), DataView)
        If dvSql.Count > 0 And Request.Cookies("UserLevel").Value <> 8 Then
            If dvSql.Item(0).Item("StatusQc") <> 8 Or dvSql.Item(0).Item("StatusQc") = 9 Or dvSql.Item(0).Item("StatusQc") = 10 Then
                Return True
            Else
                MsgBox("App นี้กำลังตรวจสอบจาก Qc หรืออยู่ในสถานะอื่นๆที่ไม่สามารถบันทึกได้")
                Return False
            End If

        Else
            Return True
        End If
    End Function
    Protected Function CheckApp() As Boolean
        Dim lblCarPet As Label = FunAll.ObjFindControl("lblCappet", frmPackage)
        Dim lblTotalValue As Label = FunAll.ObjFindControl("txtTotalValue", frmPackage)

        Try
            If GvPay.Rows.Count > 0 And (CInt(lblCarPet.Text) + CInt(lblTotalValue.Text)) <= 0 Then
                MsgBox("ไม่สามารถบันทึก : เบี้ยขายกับงวดชำระไม่ถูกต้อง")
                Return False

            Else
                Dim TotalPay As Integer = 0
                Dim TotalValue As Integer = (CInt(lblCarPet.Text) + CInt(lblTotalValue.Text)) + 10
                For i As Integer = 0 To GvPay.Rows.Count - 1
                    TotalPay += GvPay.DataKeys(i).Item(1)
                Next
                If TotalPay > TotalValue Then
                    MsgBox("ไม่สามารถบันทึก : เบี้ยขายกับงวดชำระไม่ถูกต้อง")
                    Return False
                Else

                    Return True
                End If

            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
            Return False
        End Try

    End Function
    Protected Sub Button14_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button14.Click
        'คำนวณ
        If ChkMaxPay() = True And chkPayPL(1) = True Then
            CalPay()
            btnsavepay.Enabled = True
        End If

    End Sub
#Region "AppPay"
    'งวดชำระเพิ่มสำหรับ PL,PM
    Protected Function chkPayPL(Typebtn As Integer) As Boolean
        Dim dateCreate As Date
        If frmCar.DataKey.Item(4) = 3 Or frmCar.DataKey.Item(4) = 4 Then
            If frmAppRela.DataItemCount > 0 Then
                dateCreate = frmAppRela.DataKey.Item(1)
                dateCreate = ISODate.SetISODate("th", dateCreate.ToString("dd/MM/yyyy"))
            Else
                dateCreate = DateTime.Today
            End If
        Else
            dateCreate = DateTime.Today
        End If

        Dim PayID As Integer
        If Typebtn = 1 Then
            PayID = ddPay.SelectedValue
            ViewState("btnPL") = True 'chk ว่า PL ได้กดคำนวนงวดหรือไม่่
        Else
            PayID = GvPay.Rows.Count

        End If

        Dim txtProtectDate As TextBox = FunAll.ObjFindControl("txtProtectDate", frmAppRela)
        Dim dateProtect As DateTime = ISODate.SetISODate("th", txtProtectDate.Text.Trim)
        Dim TypeID As Integer = frmPackage.DataKey.Item(17)
        Dim dateMax As Integer = DateDiff(DateInterval.Day, dateCreate, dateProtect)

        If Request.Cookies("UserLevel").Value = 1 Or Request.Cookies("UserLevel").Value = 2 And ViewState("btnPL") IsNot Nothing Then
            If PayID = 3 And dateMax < 30 Then 'เลือก 3 งวด วันคุ้มครองน้อย 30
                Return True
            ElseIf PayID = 4 And dateMax >= 30 And dateMax < 60 Then 'เลือก 4 งวด วันคุ้มครองน้อย 30- 59
                Return True
            ElseIf PayID = 5 And dateMax >= 60 And dateMax < 90 Then 'เลือก 5 งวด วันคุ้มครองน้อย 60-89
                Return True
            ElseIf PayID = 6 And dateMax >= 90 Then 'เลือก 5 งวด วันคุ้มครองน้อย 90 ขึ้นไป
                Return True
            Else
                ScriptManager.RegisterClientScriptBlock(upAppPay, GetType(UpdatePanel), upAppPay.ClientID, "alert('งวดที่ชำระไม่ตรงตามเงื่อนไข วันคุ้มครอง " & dateMax & " วัน')", True)
                Return False
            End If
        Else
            Return True
        End If
    End Function

    'คำนวนงวดชำระ
    Protected Function ChkMaxPay() As Boolean
        Dim txtProtectDate As TextBox = FunAll.ObjFindControl("txtProtectDate", frmAppRela)
        Dim ProTypeID As Integer = frmPackage.DataKey.Item(16)
        Dim TypeID As Integer = frmPackage.DataKey.Item(17)
        Dim dateCreate As DateTime
        If frmCar.DataKey.Item(4) = 3 Or frmCar.DataKey.Item(4) = 4 Then
            If frmAppRela.DataItemCount > 0 Then
                dateCreate = frmAppRela.DataKey.Item(1)
                dateCreate = DateTime.Today
            End If
        Else
            dateCreate = DateTime.Today
        End If
        Dim dateProtect As DateTime = ISODate.SetISODate("th", txtProtectDate.Text.Trim)
        Dim MaxPay As Integer = 4
        If Request.Cookies("TypeTsr").Value = 3 Or Request.Cookies("TypeTsr").Value = 11 Then
            If TypeID = 1 Then
                MaxPay = 4
            Else
                MaxPay = 3
            End If
        ElseIf Request.Cookies("TypeTsr").Value = 6 Then
            If ddTypePay.SelectedValue = 2 Then
                MaxPay = 6
            Else : ddTypePay.SelectedValue = 1
                MaxPay = 4
            End If
        ElseIf Request.Cookies("UserLevel").Value = 2 Or Request.Cookies("UserLevel").Value = 1 Then
            MaxPay = 6
        Else
            If TypeID = 3 Or TypeID = 4 And ProTypeID <> 15 Then
                MaxPay = 3
            ElseIf TypeID = 4 And ProTypeID = 15 Then

                If DateDiff(DateInterval.Day, dateCreate.Date, dateProtect) < 45 Then
                    MaxPay = 3
                Else
                    MaxPay = 3
                End If
            ElseIf TypeID = 1 Then
                If DateDiff(DateInterval.Day, dateCreate.Date, dateProtect) <= 30 Then
                    Dim itest As Integer = DateDiff(DateInterval.Day, dateCreate.Date, dateProtect)
                    MaxPay = 2
                ElseIf DateDiff(DateInterval.Day, dateCreate.Date, dateProtect) > 30 And DateDiff(DateInterval.Day, dateCreate.Date, dateProtect) <= 60 Then
                    MaxPay = 3
                ElseIf DateDiff(DateInterval.Day, dateCreate.Date, dateProtect) > 60 Then
                    MaxPay = 4
                End If
            End If

        End If

        Dim dateMax As Integer = DateDiff(DateInterval.Month, dateProtect, dateCreate.AddMonths(ddPay.SelectedValue - 1))

        If ddPay.SelectedValue > MaxPay Then
            ScriptManager.RegisterClientScriptBlock(upAppPay, GetType(UpdatePanel), upAppPay.ClientID, "alert('งวดการชำเงินไม่ตรงตามเงื่อนไข: การชำระสูงสุดคือ " & MaxPay & " งวด')", True)

            Return False
        Else

            If ddPay.SelectedValue < 4 And chkPay.Checked = True Then
                ScriptManager.RegisterClientScriptBlock(upAppPay, GetType(UpdatePanel), upAppPay.ClientID, "alert('งวดการชำระต่ำกว่า 4 งวด:ไม่สามารถกำหนดงวดแรก 3000 ได้')", True)
                Return False
            ElseIf (Request.Cookies("UserLevel").Value = 1 Or Request.Cookies("UserLevel").Value = 2) And chkPay.Checked = True Then
                ScriptManager.RegisterClientScriptBlock(upAppPay, GetType(UpdatePanel), upAppPay.ClientID, "alert('ไม่มีสิทธิ์กำหนดงวดแรก 3000')", True)

                Return False
            ElseIf ddPay.SelectedValue > 4 And ddTypePay.SelectedValue = 1 Then
                ScriptManager.RegisterClientScriptBlock(upAppPay, GetType(UpdatePanel), upAppPay.ClientID, "alert('งวดแรกจะต้องเป็น credit Card')", True)
                Return False
            ElseIf (chkCredit.Checked = False Or ddTypePay.SelectedValue = 1) And (Request.Cookies("UserLevel").Value = 1 Or Request.Cookies("UserLevel").Value = 2) Then
                ScriptManager.RegisterClientScriptBlock(upAppPay, GetType(UpdatePanel), upAppPay.ClientID, "alert('เพิ่มงวดจะต้องเป็น credit ทุกงวด')", True)
                Return False

            Else
                Return True
            End If
        End If
    End Function

    Protected Function CreateDtPay() As DataTable
        Dim dtPay As New DataTable
        Dim dtPayCl As New DataColumn
        dtPay.Columns.Add("PayNum", Type.GetType("System.Int32"))
        dtPay.Columns.Add("AppointDate", Type.GetType("System.DateTime"))
        dtPay.Columns.Add("ProValue", Type.GetType("System.Int32"))
        dtPay.Columns.Add("ProVat", Type.GetType("System.Int32"))
        dtPay.Columns.Add("TotalPay", Type.GetType("System.Int32"))
        dtPay.Columns.Add("Typepay")
        dtPay.Columns.Add("TypeName")
        Return dtPay
    End Function
    Protected Function CalProValu(ByVal i As Integer) As Integer
        Dim lblCarPet As Label = FunAll.ObjFindControl("lblCappet", frmPackage)
        Dim lblTotalValue As Label = FunAll.ObjFindControl("txtTotalValue", frmPackage)
        Dim ProValue As Integer = CInt(lblCarPet.Text) + CInt(lblTotalValue.Text)
        Dim ProPay As Integer = CInt(lblCarPet.Text) + CInt(lblTotalValue.Text)
        If i = 0 Then

            If chkPay.Checked = True Then
                If ddPay.SelectedValue <> 1 Then
                    Return 3000
                End If

            Else
                ProValue = ProValue / ddPay.SelectedValue
            End If
        Else
            If chkPay.Checked = True Then
                ProValue = (ProValue - 3000) / (ddPay.SelectedValue - 1)
            Else
                ProValue = ProValue / ddPay.SelectedValue
            End If
        End If


        If i = 0 Then
            If ProValue * ddPay.SelectedValue < ProPay Then
                ProPay = ProPay - (ProValue * ddPay.SelectedValue)
                ProValue = ProValue + ProPay
            End If

        End If
        Return ProValue
    End Function

    Protected Sub BindGvPay()
        Dim TextBox6 As TextBox = FunAll.ObjFindControl("TextBox6", frmOldInsure)
        dt = New DataTable
        dt = DataAccess.DataRead(Replace(SqlAppPay.SelectCommand, "@AppID", TextBox6.Text.Trim))

        GvPay.DataSource = dt
        GvPay.DataBind()

        If Not IsPostBack Then
            ViewState("PayNo") = dt.Rows.Count
        End If
    End Sub

    Protected Sub SaveTblAppPay()
        Dim TextBox6 As TextBox = FunAll.ObjFindControl("TextBox6", frmOldInsure)
        dt = New DataTable
        dt = DataAccess.DataRead("select * from Tblpayment Where AppID=" & TextBox6.Text.Trim)
        If dt.Rows.Count = 0 Then
            DeleteAppPay()
            InsertTblAppPay()
        End If

    End Sub

    Protected Sub DeleteAppPay()
        Dim TextBox6 As TextBox = FunAll.ObjFindControl("TextBox6", frmOldInsure)
        With SqlAppPay
            .DeleteParameters("AppID").DefaultValue = TextBox6.Text.Trim
            .Delete()
        End With
    End Sub

    Protected Sub InsertTblAppPay()
        Dim i As Integer
        For i = 0 To GvPay.Rows.Count - 1
            Dim txtAppoint As TextBox = FunAll.ObjFindControl("txtAppoint", GvPay.Rows(i))
            Dim TextBox6 As TextBox = FunAll.ObjFindControl("TextBox6", frmOldInsure)
            With SqlAppPay
                .InsertParameters("PayID").DefaultValue = i + 1
                .InsertParameters("AppID").DefaultValue = TextBox6.Text.Trim
                .InsertParameters("TotalPay").DefaultValue = CInt(DirectCast(GvPay.Rows(i).Cells(4).FindControl("lblTotalPay"), Label).Text.Trim)
                .InsertParameters("AppointDate").DefaultValue = ISODate.SetISODate("en", txtAppoint.Text.Trim)
                .InsertParameters("Typepay").DefaultValue = GvPay.DataKeys(i).Item(0)
                .Insert()
            End With
        Next
    End Sub

    Protected Function GetPayType(ByVal i As Integer) As ArrayList
        Dim ar As New ArrayList
        If Request.Cookies("TypeTsr").Value = 3 Or Request.Cookies("TypeTsr").Value = 6 Or Request.Cookies("UserLevel").Value = 1 Or Request.Cookies("UserLevel").Value = 2 Or Request.Cookies("TypeTsr").Value = 11 Then
            If ddTypePay.SelectedValue = 2 And i = 0 Then
                ar.Add(2)
                ar.Add("Credit")
            ElseIf ddTypePay.SelectedValue = 2 And chkCredit.Checked = True Then
                ar.Add(2)
                ar.Add("Credit")
            Else
                ar.Add(1)
                ar.Add("Payment")
            End If
        Else
            If ddTypePay.SelectedValue = 2 And i = 0 Then
                ar.Add(2)
                ar.Add("Credit")
            Else
                ar.Add(1)
                ar.Add("Payment")
            End If
        End If
        Return ar
    End Function

    'LogAppPay
    Protected Sub InsertTblLogAppPay()
        Dim TextBox6 As TextBox = FunAll.ObjFindControl("TextBox6", frmOldInsure)
        With SqlAppCard
            .InsertParameters("AppID").DefaultValue = TextBox6.Text.Trim
            .InsertParameters("PayNo1").DefaultValue = ViewState("PayNo")
            .InsertParameters("PayNo2").DefaultValue = GvPay.Rows.Count
            .Insert()
        End With
    End Sub
#End Region
#Region "AppCard"
    Protected Sub Button8_Click1(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim txtCardName As TextBox = FunAll.ObjFindControl("txtCardName", frmAppCard)
        Dim txtFNameTH As TextBox = FunAll.ObjFindControl("txtFNameTH", frmCusName)
        Dim txtLNameTH As TextBox = FunAll.ObjFindControl("txtLNameTH", frmCusName)
        txtCardName.Text = txtFNameTH.Text.Trim & " " & txtLNameTH.Text.Trim
    End Sub

    Protected Sub btnSaveCard_Click1(ByVal sender As Object, ByVal e As System.EventArgs)
        'กด บันทึก
        Try
            'ตรวจสอบแก้งาน qc
            If CheckApp() = True And chkCreditcard() = True Then

                If frmOldInsure.DataItemCount > 0 Then
                    If ChkMaxPay() = True Then
                        CalPay()
                    End If
                    CheckConnectionState()

                    SqlEditCard.InsertParameters("IPAddress").DefaultValue = Request.ServerVariables("REMOTE_ADDR")
                    SqlEditCard.InsertParameters("AppCardId").DefaultValue = frmAppCard.DataKey.Item(0)
                    SqlEditCard.Insert()

                    InsertTblAppCard2(QueryCredit.UpdateAppCard, frmAppCard.DataKey.Item(0))
                    With SqlAppCard
                        .SelectParameters("AppCardId").DefaultValue = 0
                    End With
                    GvAppCard.DataBind()
                End If

            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(UpdatePanel7, GetType(UpdatePanel), UpdatePanel7.ClientID, "alert('ไม่สามารถบันทึกได้')", True)

        End Try
    End Sub

    Protected Function chkCreditcard() As Boolean
        Dim txtCardID As TextBox = FunAll.ObjFindControl("txtCardID", frmAppCard)
        If txtCardID.Text.Trim = "" Then
            ScriptManager.RegisterClientScriptBlock(UpdatePanel7, GetType(UpdatePanel), UpdatePanel7.ClientID, "alert('กรุณากรอกหมายเลขบัตร')", True)
            Return False
        ElseIf txtCardID.Text.Trim = "0000000000000000" Then
            ScriptManager.RegisterClientScriptBlock(UpdatePanel7, GetType(UpdatePanel), UpdatePanel7.ClientID, "alert('เลขบัตรไม่ถูกต้อง')", True)
            Return False
        Else
            Return True
        End If

    End Function

    Protected Sub EditAppCardTMP()

        Dim ddTypeCard As DropDownList = FunAll.ObjFindControl("ddTypeCard", frmAppCard)
        Dim ddBank As DropDownList = FunAll.ObjFindControl("ddBank", frmAppCard)
        Dim ddNum As DropDownList = FunAll.ObjFindControl("ddNum", frmAppCard)

        Dim txtCardID As TextBox = FunAll.ObjFindControl("txtCardID", frmAppCard)
        Dim txtCardID3 As TextBox = FunAll.ObjFindControl("txtCardID3", frmAppCard)
        Dim txtExpCard As TextBox = FunAll.ObjFindControl("txtExpCard", frmAppCard)
        Dim txtExpCard2 As TextBox = FunAll.ObjFindControl("txtExpCard2", frmAppCard)
        Dim txtTel As TextBox = FunAll.ObjFindControl("txtTel", frmAppCard)
        Dim txtMobile As TextBox = FunAll.ObjFindControl("txtMobile", frmAppCard)
        Dim txtFristPay As TextBox = FunAll.ObjFindControl("txtFristPay", frmAppCard)
        Dim txtCommentCard As TextBox = FunAll.ObjFindControl("txtCommentCard", frmAppCard)
        Dim txtCardName As TextBox = FunAll.ObjFindControl("txtCardName", frmAppCard)

        dt = New DataTable
        dt = ViewState("AppCard")
        dt.Rows(frmAppCard.PageIndex).Item("Cardname") = txtCardName.Text.Trim
        dt.Rows(frmAppCard.PageIndex).Item("Cardno1") = txtCardID.Text.Trim
        dt.Rows(frmAppCard.PageIndex).Item("PayTypeId") = 1
        dt.Rows(frmAppCard.PageIndex).Item("CardNo2") = txtCardID3.Text.Trim
        dt.Rows(frmAppCard.PageIndex).Item("CardExp") = txtExpCard.Text.Trim
        dt.Rows(frmAppCard.PageIndex).Item("Cardid") = ddTypeCard.SelectedValue
        dt.Rows(frmAppCard.PageIndex).Item("Bankid") = ddBank.SelectedValue
        dt.Rows(frmAppCard.PageIndex).Item("Tel1") = txtTel.Text.Trim
        dt.Rows(frmAppCard.PageIndex).Item("Mobile") = txtMobile.Text.Trim
        dt.Rows(frmAppCard.PageIndex).Item("IsPayDate") = GetFirstPay()
        dt.Rows(frmAppCard.PageIndex).Item("PayDate") = ISODate.SetISODate("th", txtFristPay.Text.Trim)
        dt.Rows(frmAppCard.PageIndex).Item("BankName") = ddBank.SelectedItem.Text

        ViewState("AppCard") = dt
        GvAppCard.DataSource = dt
        GvAppCard.DataBind()
    End Sub
    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If frmOldInsure.DataItemCount > 0 Then
            With SqlAppCard
                .SelectParameters("AppCardId").DefaultValue = 0
            End With
            frmAppCard.DataBind()
        Else
            frmAppCard.DataSource = SqlAppCard
            frmAppCard.DataBind()
        End If

    End Sub

    Protected Sub btnSaveCard_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'กด เพิ่ม ข้อมูล บัตรเครดิต
        Try
            If chkCreditcard() = True Then
                If frmOldInsure.DataItemCount > 0 Then
                    'กรณีมี App
                    CheckConnectionState()
                    InsertTblAppCard2(QueryCredit.InsertAppCard, "")
                    GvAppCard.DataBind()

                    If ChkMaxPay() = True Then
                        CalPay()
                    End If
                End If

            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCall", "alert('ไม่สามารถบันทึกได้ : " & ex.Message & "')", True)
        End Try

    End Sub

    Protected Sub CreateDtAppCard()
        dt = New DataTable
        dt.Columns.Add("AppCardID")
        dt.Columns.Add("Cardname")
        dt.Columns.Add("Cardno1")
        dt.Columns.Add("PayTypeId")
        dt.Columns.Add("CardNo2")
        dt.Columns.Add("CardExp")
        dt.Columns.Add("Cardid")
        dt.Columns.Add("Bankid")
        dt.Columns.Add("Tel1")
        dt.Columns.Add("Mobile")
        dt.Columns.Add("IsPayDate")
        dt.Columns.Add("PayDate", Type.GetType("System.DateTime"))
        dt.Columns.Add("BankName")
        dt.Columns.Add("StatusEdit")

    End Sub

    Protected Sub InsertTmpAppCard()
        Dim dr As DataRow
        dr = dt.NewRow
        Dim ddTypeCard As DropDownList = FunAll.ObjFindControl("ddTypeCard", frmAppCard)
        Dim ddBank As DropDownList = FunAll.ObjFindControl("ddBank", frmAppCard)
        Dim ddNum As DropDownList = FunAll.ObjFindControl("ddNum", frmAppCard)

        Dim txtCardID As TextBox = FunAll.ObjFindControl("txtCardID", frmAppCard)
        Dim txtCardID3 As TextBox = FunAll.ObjFindControl("txtCardID3", frmAppCard)
        Dim txtExpCard As TextBox = FunAll.ObjFindControl("txtExpCard", frmAppCard)

        Dim txtTel As TextBox = FunAll.ObjFindControl("txtTel", frmAppCard)
        Dim txtMobile As TextBox = FunAll.ObjFindControl("txtMobile", frmAppCard)
        Dim txtFristPay As TextBox = FunAll.ObjFindControl("txtFristPay", frmAppCard)
        Dim txtCommentCard As TextBox = FunAll.ObjFindControl("txtCommentCard", frmAppCard)
        Dim txtCardName As TextBox = FunAll.ObjFindControl("txtCardName", frmAppCard)

        Dim paydate As Date = ISODate.SetISODate("th", txtFristPay.Text.Trim)

        dr("AppCardID") = 0
        dr("Cardname") = txtCardName.Text.Trim
        dr("Cardno1") = txtCardID.Text.Trim
        dr("PayTypeId") = 1
        dr("CardNo2") = txtCardID3.Text.Trim
        dr("CardExp") = txtExpCard.Text.Trim
        dr("Cardid") = ddTypeCard.SelectedValue
        dr("Bankid") = ddBank.SelectedValue
        dr("Tel1") = txtTel.Text.Trim
        dr("Mobile") = txtMobile.Text.Trim
        dr("IsPayDate") = GetFirstPay()
        dr("PayDate") = paydate
        dr("BankName") = ddBank.SelectedItem.Text
        dr("StatusEdit") = "True"
        dt.Rows.Add(dr)

    End Sub

    Protected Function GetFirstPay() As Integer
        Dim chdFristPay As CheckBox = FunAll.ObjFindControl("chdFristPay", frmAppCard)
        If chdFristPay.Checked = True Then
            Return 1
        Else
            Return 0
        End If
    End Function
    Protected Sub GvAppPayDataBind()
    End Sub

    'สำหรับกรณี ยังไม่มี App
    Protected Sub InsertTblAppCard()
        For i As Integer = 0 To GvAppCard.Rows.Count - 1
            Dim Paydate As DateTime = GvAppCard.DataKeys(i).Item(7)
            Dim TextBox6 As TextBox = FunAll.ObjFindControl("TextBox6", frmOldInsure)
            com = New SqlCommand(QueryCredit.InsertAppCard, Conn)
            With com
                .Parameters.Clear()
                .Parameters.Add("@Appid", SqlDbType.VarChar).Value = TextBox6.Text.Trim
                .Parameters.Add("@CardRun", SqlDbType.VarChar).Value = i + 1
                .Parameters.Add("@CardNo1", SqlDbType.VarChar).Value = GvAppCard.DataKeys(i).Item(0)
                .Parameters.Add("@CardNo2", SqlDbType.VarChar).Value = GvAppCard.DataKeys(i).Item(1)
                .Parameters.Add("@CardExp", SqlDbType.VarChar).Value = GvAppCard.DataKeys(i).Item(2)
                .Parameters.Add("@Cardname", SqlDbType.VarChar).Value = GvAppCard.DataKeys(i).Item(3)
                .Parameters.Add("@PayTypeId", SqlDbType.VarChar).Value = GvAppCard.DataKeys(i).Item(4)
                .Parameters.Add("@Cardid", SqlDbType.VarChar).Value = GvAppCard.DataKeys(i).Item(5)
                .Parameters.Add("@Bankid", SqlDbType.VarChar).Value = GvAppCard.DataKeys(i).Item(6)
                .Parameters.Add("@PayDate", SqlDbType.VarChar).Value = ISODate.SetISODate("en", Paydate.ToString("dd/MM/yyyy"))
                .Parameters.Add("@IsPayDate", SqlDbType.VarChar).Value = GvAppCard.DataKeys(i).Item(8)
                .Parameters.Add("@Createid", SqlDbType.VarChar).Value = Request.Cookies("userID").Value
                .Parameters.Add("@Updateid", SqlDbType.VarChar).Value = Request.Cookies("userID").Value
                .Parameters.Add("@Tel1", SqlDbType.VarChar).Value = GvAppCard.DataKeys(i).Item(9)
                .Parameters.Add("@Mobile", SqlDbType.VarChar).Value = GvAppCard.DataKeys(i).Item(10)
                .ExecuteNonQuery()

            End With
        Next
    End Sub

    'สำหรับกรณี  มี App
    Protected Sub InsertTblAppCard2(ByVal strqry As String, ByVal AppCardID As String)
        Dim ddTypeCard As DropDownList = FunAll.ObjFindControl("ddTypeCard", frmAppCard)
        Dim ddBank As DropDownList = FunAll.ObjFindControl("ddBank", frmAppCard)
        Dim ddNum As DropDownList = FunAll.ObjFindControl("ddNum", frmAppCard)

        Dim txtCardID As TextBox = FunAll.ObjFindControl("txtCardID", frmAppCard)
        Dim txtCardID3 As TextBox = FunAll.ObjFindControl("txtCardID3", frmAppCard)
        Dim txtExpCard As TextBox = FunAll.ObjFindControl("txtExpCard", frmAppCard)
        Dim txtExpCard2 As TextBox = FunAll.ObjFindControl("txtExpCard2", frmAppCard)
        Dim txtTel As TextBox = FunAll.ObjFindControl("txtTel", frmAppCard)
        Dim txtMobile As TextBox = FunAll.ObjFindControl("txtMobile", frmAppCard)
        Dim txtFristPay As TextBox = FunAll.ObjFindControl("txtFristPay", frmAppCard)
        Dim txtCommentCard As TextBox = FunAll.ObjFindControl("txtCommentCard", frmAppCard)
        Dim txtCardName As TextBox = FunAll.ObjFindControl("txtCardName", frmAppCard)
        Dim TextBox6 As TextBox = FunAll.ObjFindControl("TextBox6", frmOldInsure)
        com = New SqlCommand(strqry, Conn)
        With com
            .Parameters.Clear()
            .Parameters.Add("@Appid", SqlDbType.VarChar).Value = TextBox6.Text.Trim
            .Parameters.Add("@CardRun", SqlDbType.VarChar).Value = GvAppCard.Rows.Count + 1
            .Parameters.Add("@CardNo1", SqlDbType.VarChar).Value = txtCardID.Text.Trim
            .Parameters.Add("@CardNo2", SqlDbType.VarChar).Value = txtCardID3.Text.Trim
            .Parameters.Add("@CardExp", SqlDbType.VarChar).Value = txtExpCard.Text.Trim
            .Parameters.Add("@Cardname", SqlDbType.VarChar).Value = txtCardName.Text.Trim
            .Parameters.Add("@Cardid", SqlDbType.VarChar).Value = ddTypeCard.SelectedValue
            .Parameters.Add("@Bankid", SqlDbType.VarChar).Value = ddBank.SelectedValue
            .Parameters.Add("@PayDate", SqlDbType.VarChar).Value = ISODate.SetISODate("en", txtFristPay.Text.Trim)
            .Parameters.Add("@IsPayDate", SqlDbType.VarChar).Value = GetFirstPay()
            .Parameters.Add("@Createid", SqlDbType.VarChar).Value = Request.Cookies("userID").Value
            .Parameters.Add("@Updateid", SqlDbType.VarChar).Value = Request.Cookies("userID").Value
            .Parameters.Add("@Tel1", SqlDbType.VarChar).Value = txtTel.Text.Trim
            .Parameters.Add("@Mobile", SqlDbType.VarChar).Value = txtMobile.Text.Trim
            .Parameters.Add("@AppCardID", SqlDbType.VarChar).Value = AppCardID
            .ExecuteNonQuery()

        End With
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        T1.Visible = False
        'แสดงข้อมูลเบื้องต้น
        'Dim Label46 As Label = FunAll.ObjFindControl("Label46", frmProType)
        'Dim Label43 As Label = FunAll.ObjFindControl("Label43", frmProType)
        'Dim Label45 As Label = FunAll.ObjFindControl("Label45", frmProType)
        'Dim lbltype As Label = FunAll.ObjFindControl("lbltype", frmCusName)
        'lbltype.Text = Label46.Text.Trim & " " & Label43.Text.Trim & " " & Label43.Text.Trim

        'Dim lblCarPet As Label = FunAll.ObjFindControl("lblCappet", frmPackage)
        'Dim lblTotalValue As Label = FunAll.ObjFindControl("txtTotalValue", frmPackage)

        If Not IsPostBack Then
            BindGvPay()
            btnsavepay.Enabled = False
        Else

        End If
    End Sub

    Protected Sub GvPay_DataBound(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub GvPay_DataBound1(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvPay.DataBound
        If Request.Cookies("UserLevel").Value = 2 Or Request.Cookies("UserLevel").Value = 1 Then
            For i As Integer = 0 To GvPay.Rows.Count - 1
                Dim txtAppoint As TextBox = FunAll.ObjFindControl("txtAppoint", GvPay.Rows(i))
                txtAppoint.ReadOnly = True
            Next

        End If
    End Sub

    Protected Sub SqlAppPay_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlAppPay.Selected

    End Sub

    Protected Sub GvAppCard_Load(sender As Object, e As System.EventArgs) Handles GvAppCard.Load
        'If Request.QueryString("Edit").ToString = 0 Then
        'GvAppCard.Columns(1).Visible = False
        'GvAppCard.Columns(0).Visible = False
        'End If
        GvAppPayDataBind()
    End Sub
    'คำนวนการแบ่งจ่ายเงิน
    Protected Sub CalPay()
        dt = New DataTable
        dt = CreateDtPay()

        Dim dateMonthAdd As DateTime
        Dim ProTypeID As Integer = frmPackage.DataKey.Item(16)
        Dim TypeID As Integer = frmPackage.DataKey.Item(17)
        Dim dateCreate As DateTime
        Dim txtProtectDate As TextBox = FunAll.ObjFindControl("txtProtectDate", frmAppRela)
        If frmCar.DataKey.Item(4) = 3 Or frmCar.DataKey.Item(4) = 4 Then
            If frmOldInsure.DataItemCount > 0 Then
                dateCreate = frmAppRela.DataKey.Item(2)

            Else
                dateCreate = DateTime.Today
            End If


        Else
            dateCreate = DateTime.Today
        End If

        Dim dateProtect As DateTime = ISODate.SetISODate("th", txtProtectDate.Text.Trim)

        Dim dr As DataRow
        Dim DateAppoint As DateTime = DateTime.Today.AddDays(7)
        Dim i As Integer
        For i = 0 To ddPay.SelectedValue - 1
            dr = dt.NewRow
            dr("PayNum") = i + 1

            If DateDiff(DateInterval.Day, dateCreate, dateProtect) >= 61 And TypeID = 1 Then 'วันสมัครห่างจากวันคุ้มครอง มากกว่า 60 วันและเป็นชั้น1
                'งวด 1

                If i = 0 Then
                    If ddTypePay.SelectedValue = 1 Then
                        dr("AppointDate") = dateCreate.AddDays(7).ToString("dd/MM/yyyy")
                        If ddPay.SelectedValue > 4 Then
                            DateAppoint = dateCreate.AddDays(7).ToString("dd/MM/yyyy")
                        Else
                            DateAppoint = dateProtect.AddMonths(-2)
                        End If

                    Else
                        If Request.Cookies("TypeTsr").Value = 3 Or Request.Cookies("TypeTsr").Value = 6 Or Request.Cookies("TypeTsr").Value = 11 Then
                            Dim chdFristPay As CheckBox = FunAll.ObjFindControl("chdFristPay", frmAppCard)
                            Dim txtFristPay As TextBox = FunAll.ObjFindControl("txtFristPay", frmAppCard)
                            If chdFristPay.Checked = True And txtFristPay.Text.Trim <> "" Then
                                dr("AppointDate") = ISODate.SetISODate("th", txtFristPay.Text.Trim)
                                DateAppoint = ISODate.SetISODate("th", txtFristPay.Text.Trim)
                            Else
                                dr("AppointDate") = dateCreate.ToString("dd/MM/yyyy")
                                DateAppoint = dateProtect.AddMonths(-2)
                            End If

                        Else
                            dr("AppointDate") = dateCreate.ToString("dd/MM/yyyy")
                            If ddPay.SelectedValue > 4 Then
                                DateAppoint = dateCreate
                            ElseIf ddPay.SelectedValue <= 4 And (Request.Cookies("UserLevel").Value = 2 Or Request.Cookies("UserLevel").Value = 1) Then
                                DateAppoint = dateCreate
                            Else
                                DateAppoint = dateProtect.AddMonths(-2)
                            End If

                        End If

                    End If

                Else 'งวดต่อไป

                    dr("AppointDate") = DateAppoint.AddMonths(i).ToString("dd/MM/yyyy")

                End If



            Else
                If i = 0 Then 'งวดแรก
                    If DateDiff(DateInterval.Day, dateCreate, dateProtect) < 7 Then ' วันสมัครห่างจากวันคุ้มครอง < 7 วัน
                        dr("AppointDate") = dateCreate.ToString("dd/MM/yyyy")
                        dateMonthAdd = dateCreate
                        Dim chdFristPay As CheckBox = FunAll.ObjFindControl("chdFristPay", frmAppCard)
                        Dim txtFristPay As TextBox = FunAll.ObjFindControl("txtFristPay", frmAppCard)
                        If chdFristPay.Checked = True And txtFristPay.Text.Trim <> "" Then
                            dr("AppointDate") = ISODate.SetISODate("th", txtFristPay.Text.Trim)
                            dateMonthAdd = ISODate.SetISODate("th", txtFristPay.Text.Trim)
                        End If

                    Else
                        If ddTypePay.SelectedValue = 1 Then
                            dr("AppointDate") = dateCreate.AddDays(7).ToString("dd/MM/yyyy")
                            dateMonthAdd = dateCreate.AddDays(7)
                        Else
                            If Request.Cookies("TypeTsr").Value = 3 Or Request.Cookies("TypeTsr").Value = 6 Or Request.Cookies("TypeTsr").Value = 11 Then
                                Dim chdFristPay As CheckBox = FunAll.ObjFindControl("chdFristPay", frmAppCard)
                                Dim txtFristPay As TextBox = FunAll.ObjFindControl("txtFristPay", frmAppCard)
                                If chdFristPay.Checked = True And txtFristPay.Text.Trim <> "" Then
                                    dr("AppointDate") = ISODate.SetISODate("th", txtFristPay.Text.Trim)
                                    dateMonthAdd = ISODate.SetISODate("th", txtFristPay.Text.Trim)
                                Else
                                    dr("AppointDate") = dateCreate.ToString("dd/MM/yyyy")
                                    dateMonthAdd = dateCreate
                                End If
                            Else
                                dr("AppointDate") = dateCreate.ToString("dd/MM/yyyy")
                                dateMonthAdd = dateCreate
                            End If

                        End If

                    End If
                Else
                    If i = 2 Then
                        'If ProTypeID = 15 Then
                        '    dr("AppointDate") = dateProtect.AddMonths(-7).ToString("dd/MM/yyyy") 'งวด 3  น้องกว่า วันคุ้มครอง 7 วัน
                        '    dateMonthAdd = dateProtect.AddMonths(-7)
                        'Else
                        dr("AppointDate") = dateMonthAdd.AddMonths(i).ToString("dd/MM/yyyy")
                        'End If


                    Else
                        dr("AppointDate") = dateMonthAdd.AddMonths(i).ToString("dd/MM/yyyy")
                    End If
                End If



            End If

            dr("ProValue") = CalProValu(i)
            If ddTypePay.SelectedValue = 2 And i = 0 Then
                dr("ProVat") = 0
                dr("TotalPay") = CalProValu(i)

            Else
                If Request.Cookies("TypeTsr").Value = 3 And chkCredit.Checked = True And ddTypePay.SelectedValue = 2 Then
                    dr("ProVat") = 0
                    dr("TotalPay") = CalProValu(i)
                ElseIf Request.Cookies("TypeTsr").Value = 11 And chkCredit.Checked = True And ddTypePay.SelectedValue = 2 Then
                    dr("ProVat") = 0
                    dr("TotalPay") = CalProValu(i)
                ElseIf Request.Cookies("TypeTsr").Value = 6 And chkCredit.Checked = True And ddTypePay.SelectedValue = 2 Then
                    dr("ProVat") = 0
                    dr("TotalPay") = CalProValu(i)
                ElseIf Request.Cookies("UserLevel").Value = 2 And chkCredit.Checked = True And ddTypePay.SelectedValue = 2 Then
                    dr("ProVat") = 0
                    dr("TotalPay") = CalProValu(i)
                ElseIf Request.Cookies("UserLevel").Value = 1 And chkCredit.Checked = True And ddTypePay.SelectedValue = 2 Then
                    dr("ProVat") = 0
                    dr("TotalPay") = CalProValu(i)
                Else
                    dr("ProVat") = 10
                    dr("TotalPay") = CalProValu(i) + 10
                End If

            End If

            dr("Typepay") = GetPayType(i).Item(0)
            dr("TypeName") = GetPayType(i).Item(1)
            dt.Rows.Add(dr)

            GvPay.DataSource = dt
            GvPay.DataBind()

        Next
        Dim aa As Int16 = GvPay.Rows.Count
    End Sub
    Protected Sub btnsavepay_Click(sender As Object, e As System.EventArgs) Handles btnsavepay.Click
        'บันทึกข้อมูลใหม่
        'ส่วนที่ 1 การคำนวณแต่ละงวด

        If CheckApp() = True And chkPayPL(2) = True And chkAppQc() = True Then
            If GvPay.Rows.Count > 0 Then
                '1.1.เก็บ(Log) ->จาก TblAppPay ->TblLogEditAppPay
                SqlEditPay.InsertParameters("IPAddress").DefaultValue = Request.ServerVariables("REMOTE_ADDR")
                SqlEditPay.Insert()
                SaveTblAppPay()
                If Request.Cookies("UserLevel").Value = 2 Or Request.Cookies("UserLevel").Value = 1 Then
                    InsertTblLogAppPay()
                End If
            End If
        End If
        BindGvPay()
        btnsavepay.Enabled = False
    End Sub


    Protected Sub GvAppCard_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvAppCard.RowCommand

        If e.CommandName = "Remove" Then
            If frmOldInsure.DataItemCount > 0 Then
                SqlEditCard.InsertParameters("IPAddress").DefaultValue = Request.ServerVariables("REMOTE_ADDR")
                SqlEditCard.InsertParameters("AppCardId").DefaultValue = GvAppCard.DataKeys(e.CommandArgument).Item(11)
                SqlEditCard.Insert()
                SqlAppCard2.DeleteParameters("AppCardId").DefaultValue = GvAppCard.DataKeys(e.CommandArgument).Item(11)
                SqlAppCard2.Delete()
                GvAppPayDataBind()
            End If

        ElseIf e.CommandName = "Select" Then
            If frmOldInsure.DataItemCount > 0 Then
                With SqlAppCard
                    .SelectParameters("AppCardId").DefaultValue = GvAppCard.DataKeys(e.CommandArgument).Item(11)
                End With
                frmAppCard.DataBind()
            End If
        End If
    End Sub
   
End Class

