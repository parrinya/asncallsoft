Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net

Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
'Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq
Imports System.Text
Imports System.Data.OleDb

Imports System.Drawing
Imports System.Diagnostics
Imports System.ComponentModel
Imports GenCode128

Partial Class QCSoundRecord
    Inherits System.Web.UI.Page
    Dim Conn As New SqlConnection(ConfigurationManager.ConnectionStrings("asnbroker").ConnectionString)
    Dim com As SqlCommand
    Dim Tran As SqlTransaction
    Dim dt, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9, dt20, dt21 As DataTable
    Dim ConvertDate As ISODate = New ISODate()
    Dim FunAll As FuntionAll = New FuntionAll()
    Dim date1 As String
    Dim date2 As String
    Public TelAjax As String = ""
    Public Search As String = ""
    Dim myReport As New ReportDocument
    Dim DataAccess As New DataAccess


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            'WDTFrom.Value = Date.Now
            'WDTTo.Value = Date.Now
            If Request.Cookies("userID") IsNot Nothing Then
                If Request.Cookies("UserLevel").Value = "8" And Request.Cookies("TypeTsr").Value = "0" Then '8 type 0 คือ Qcพิเศษ จะเห็นเมนูมากกว่า Qc ปกติ  Then
                    wddQC.SelectedValue = 0
                Else
                    wddQC.SelectedValue = Request.Cookies("UserID").Value
                End If

                ShowData(0)
            Else
                Response.Redirect("~/Default.aspx")
            End If

            'If Request.Cookies("userID") IsNot Nothing Then
            '    Search = "http://asquarenetwork/webupload/default2.aspx?UserID=" & Request.Cookies("UserID").Value
            'Else
            '    Response.Redirect("~/Default.aspx")
            'End If
        End If

        If Request.Cookies("userID") IsNot Nothing Then
            Search = "http://asquarenetwork/webupload/default2.aspx?UserID=" & Request.Cookies("UserID").Value
            If Request.Cookies("UserLevel").Value = "8" And Request.Cookies("TypeTsr").Value = "0" Then '8 type 0 คือ Qcพิเศษ จะเห็นเมนูมากกว่า Qc ปกติ  Then
                lblAssignTo.Visible = True
                wddAssign.Visible = True
                Panel1.Visible = True
            End If
        Else
            Response.Redirect("~/Default.aspx")
        End If
    End Sub

    Protected Sub ClearData()
        'Reminder by na 20/11/2014
        Me.txtwcommentqc.Text = Nothing
        btnAddReminder.Enabled = False
        GvReminder.DataSource = Nothing
        GvReminder.DataBind()
        'End 
        btnHistory.Enabled = False
        btnEditApp.Enabled = False
        BtnOpenSound.Enabled = False
        BtnOpenFile.Enabled = False
        btnAddStatus.Enabled = False
        btnPass.Enabled = False
        btnNotPass.Enabled = False
        btnHoldOn.Enabled = False
        btnHangUp.Enabled = False
        wddCusStatus.Enabled = False
        wddTelType.Enabled = False
        wddMStatus.Enabled = False
        wddSStatus.Enabled = False
        btnUpdate.Enabled = False
        btnOldApp.Enabled = False
        btnPreview.Enabled = False
        btnCalculate.Enabled = False
        uwgComment.DataSource = Nothing
        uwgComment.DataBind()
        UltraWebGrid1.DataSource = Nothing
        UltraWebGrid1.DataBind()
        lblApp.Text = ""
        lblCarid.Text = ""
        lblCusID.Text = ""
        Panel2.Visible = False
        txtPhoneNo.Text = ""
        wddTelType.SelectedValue = 0
        txtQCcomment.Text = ""
        wddMStatus.SelectedValue = 0
        wddSStatus.CurrentValue = "- กรุณาเลือก -"
        UltraWebGrid2.DataSource = Nothing
        UltraWebGrid2.DataBind()
        UltraWebGrid2.Visible = False
        chkPhoto.Checked = False
    End Sub

    Private Sub ShowData(ByVal numchk As Integer)
        Dim tmp2 As String
        Dim str1 As String
        Dim Str2 As String
        Dim tmp3 As String
        Dim tmp4 As String
        Dim tmp5 As String

        'Call setdate()

        ClearData()

        Dim sql As String = "SELECT StatusQC FROM Tblapplication  "

        If wddAppStatus.SelectedItemIndex <= 0 Then tmp2 = " (TblApplication.Statusqc in (0,9))  "
        If wddAppStatus.SelectedItemIndex = 1 Then tmp2 = " (TblApplication.Statusqc in (3,10))  "
        'If wddAppStatus.SelectedItemIndex = 2 Then tmp2 = " (TblApplication.Statusqc in (9))  "

        If wddTSR.SelectedItemIndex > 0 Then tmp3 = "and assignto = " + wddTSR.SelectedValue

        If numchk = 0 Then
            If Request.Cookies("userID") IsNot Nothing Then
                If Request.Cookies("UserLevel").Value = "8" And Request.Cookies("TypeTsr").Value = "0" Then '8 type 0 คือ Qcพิเศษ จะเห็นเมนูมากกว่า Qc ปกติ  Then
                    tmp5 = ""
                Else
                    tmp5 = " and useridqc = '" & Request.Cookies("UserID").Value & "'"
                End If
            Else
                Response.Redirect("~/Default.aspx")
            End If

        ElseIf numchk = 1 Then
            If wddQC.SelectedItemIndex <= 0 Then tmp5 = ""
            If wddQC.SelectedItemIndex > 0 Then tmp5 = " and useridqc = '" & wddQC.SelectedValue & "'"
        End If


        tmp4 = ""

        Dim str As String = "  SELECT   a.* FROM (SELECT TblCustomer.FNameTH,TblCustomer.LNameTH, TblCar.IdCar, TblCar.CusID, TblCar.curstatus,TblCar.AssignTo, TblCar.CarID, TblApplication.AppID,TblApplication.Statusqc, " + _
                            " TblApplication.ProtectDate,TblApplication.ProtectDate as p1,TblApplication.successdate,TblApplication.successdate as p2,TblApplication.useridqc,tblcar.dataid,TblApplication.doc1,TblApplication.doc2,TblApplication.doc3,tblapplication.appip,tblapplication.productid  ,tblapplication.typeprovalue,tblapplication.createid,'takephoto' = case TblApplication.takephoto when 1 then 'ASN ตรวจสภาพ' when 0 then ' ' end ,fname + ' ' +lname as tsr,pkgid, 1 as flag " + _
                            "FROM TblCustomer INNER JOIN " + _
                            "TblCar ON TblCustomer.CusID = TblCar.CusID INNER JOIN " + _
                            "TblApplication ON TblCar.IdCar = TblApplication.Idcar  inner join tbluser  on tblcar.assignto = tbluser.userid  " + _
                            "WHERE  " + tmp2 + tmp4 + tmp5 + _
                            " and curstatus in(3,4,19) and appstatus = 1 and year(successdate) > = 2011 and DATEDIFF(d, getdate(),ProtectDate) < 8  "
        str = str + tmp3
        str = str + "UNION "
        str = str + "  SELECT distinct TblCustomer.FNameTH, TblCustomer.LNameTH, TblCar.IdCar, TblCar.CusID, TblCar.curstatus,TblCar.AssignTo, TblCar.CarID, TblApplication.AppID,TblApplication.Statusqc, " + _
                            " TblApplication.ProtectDate,TblApplication.successdate as p1,TblApplication.successdate,TblApplication.ProtectDate as p2,TblApplication.useridqc,tblcar.dataid,TblApplication.doc1,TblApplication.doc2,TblApplication.doc3,tblapplication.appip,tblapplication.productid  ,tblapplication.typeprovalue,tblapplication.createid,'takephoto' = case TblApplication.takephoto when 1 then 'ASN ตรวจสภาพ' when 0 then ' ' end ,fname + ' ' +lname as tsr,pkgid, 2 as flag " + _
                            "FROM TblCustomer INNER JOIN " + _
                            "TblCar ON TblCustomer.CusID = TblCar.CusID INNER JOIN " + _
                            "TblApplication ON TblCar.IdCar = TblApplication.Idcar  inner join tbluser  on tblcar.assignto = tbluser.userid  " + _
                            "WHERE  " + tmp2 + tmp4 + tmp5 + _
                            " and curstatus in(3,4,19) and appstatus = 1 and year(successdate) > = 2011 and DATEDIFF(d, getdate(),ProtectDate) > 7 "
        str = str + tmp3

        str = str + ") a order by a.flag,a.p1,a.p2"

        Conn.Open()
        Dim Command As SqlCommand = New SqlCommand(str, Conn)
        Dim DataReader As SqlDataReader
        DataReader = Command.ExecuteReader()
        Dim fName, LName, fullName As String
        Dim protectdate, SuccessDate As Date
        dt = New DataTable

        dt.Columns.Add("No.")
        dt.Columns.Add("ชื่อ-นามสกุล")
        dt.Columns.Add("SuccessDate")
        dt.Columns.Add("สถานะ/ประเภทการชำระ/สถานะตรวจรถ")
        dt.Columns.Add("ยอดการชำระ")
        dt.Columns.Add("วันคุ้มครอง")
        dt.Columns.Add("หน้าตาราง")
        dt.Columns.Add("ใบขับขี่ 1")
        dt.Columns.Add("ใบขับขี่ 2")
        dt.Columns.Add("CusID")
        dt.Columns.Add("IP")
        dt.Columns.Add("TSR")
        dt.Columns.Add("บริษัทประกัน")
        dt.Columns.Add("Package")
        dt.Columns.Add("Extension")
        dt.Columns.Add("สถานะถ่ายรูป")
        dt.Columns.Add("createid")
        dt.Columns.Add("useridqc")
        dt.Columns.Add("idcar")
        dt.Columns.Add("curstatus")
        dt.Columns.Add("appid")
        dt.Columns.Add("takephoto")
        dt.Columns.Add("productid")
        dt.Columns.Add("typeprovalue")
        dt.Columns.Add("pkgid")
        dt.Columns.Add("statusqc")

        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt.NewRow
                If IsDBNull(DataReader("assignto")) = False Then
                    dtr("createid") = DataReader("assignto")
                End If
                If IsDBNull(DataReader("useridqc")) = False Then
                    dtr("useridqc") = DataReader("useridqc")
                End If
                If IsDBNull(DataReader("idcar")) = False Then
                    dtr("idcar") = DataReader("idcar")
                End If
                If IsDBNull(DataReader("fnameth")) = False Then
                    fName = DataReader("fnameth")
                End If
                If IsDBNull(DataReader("lnameth")) = False Then
                    LName = DataReader("lnameth")
                    If LName = "NULL" Then
                        LName = ""
                    End If
                End If
                dtr("ชื่อ-นามสกุล") = fName + " " + LName
                If IsDBNull(DataReader("successdate")) = False Then
                    dtr("SuccessDate") = Format(DataReader("successdate"), "dd/MM/yyyy HH:mm:ss")
                    SuccessDate = DataReader("successdate")
                End If
                If IsDBNull(DataReader("curstatus")) = False Then
                    dtr("curstatus") = DataReader("curstatus")
                End If
                If IsDBNull(DataReader("appid")) = False Then
                    dtr("appid") = DataReader("appid")
                End If
                If IsDBNull(DataReader("takephoto")) = False Then
                    dtr("takephoto") = DataReader("takephoto")
                End If
                If IsDBNull(DataReader("protectdate")) = False And IsDBNull(DataReader("successdate")) = False Then
                    protectdate = DataReader("protectdate")
                    dtr("วันคุ้มครอง") = "<FONT Color = 'navy'>" + Format(protectdate, "dd/MM/yyyy") + "</FONT> >> " + DateDiff(DateInterval.Day, CDate(SuccessDate).Date, CDate(protectdate).Date).ToString + " วัน"
                End If

                If IsDBNull(DataReader("doc1")) = False Then
                    If DataReader("doc1") = 1 Then dtr("หน้าตาราง") = "มี"
                End If
                If IsDBNull(DataReader("doc2")) = False Then
                    If DataReader("doc2") = 1 Then dtr("ใบขับขี่ 1") = "มี"
                End If
                If IsDBNull(DataReader("doc3")) = False Then
                    If DataReader("doc3") = 1 Then dtr("ใบขับขี่ 2") = "มี"
                End If
                If IsDBNull(DataReader("cusid")) = False Then
                    dtr("CusID") = DataReader("cusid")
                End If
                If IsDBNull(DataReader("dataid")) = False Then
                    If DataReader("dataid") <> 6 Then dtr("IP") = DataReader("appip")
                    If DataReader("dataid") = 6 Then dtr("IP") = "Web Agent"
                    If DataReader("dataid") = 8 Then dtr("IP") = "ASN WEB"
                End If
                If IsDBNull(DataReader("tsr")) = False Then
                    dtr("TSR") = DataReader("tsr")
                End If
                If IsDBNull(DataReader("productid")) = False Then
                    dtr("productid") = DataReader("productid")
                    dtr("typeprovalue") = DataReader("typeprovalue")
                End If
                If IsDBNull(DataReader("pkgid")) = False Then
                    dtr("pkgid") = DataReader("pkgid")
                Else
                    dtr("pkgid") = "0"
                    'Else
                    '    dtr("pkgid") = "0"
                End If
                If IsDBNull(DataReader("takephoto")) = False Then
                    dtr("สถานะถ่ายรูป") = DataReader("takephoto")
                End If
                If IsDBNull(DataReader("statusqc")) = False Then
                    dtr("statusqc") = DataReader("statusqc")
                End If

                dt.Rows.Add(dtr)
            End While
        End If

        DataReader.Close()
        Conn.Close()
        dt2 = New DataTable

        dt2.Columns.Add("No./AppID") '0
        dt2.Columns.Add("ชื่อ-นามสกุล") '1
        dt2.Columns.Add("SuccessDate") '2
        dt2.Columns.Add("สถานะ/ประเภทการชำระ") '3
        dt2.Columns.Add("ยอดการชำระ") '4
        dt2.Columns.Add("วันคุ้มครอง") '5
        dt2.Columns.Add("หน้าตาราง") '6
        dt2.Columns.Add("ใบขับขี่ 1") '7
        dt2.Columns.Add("ใบขับขี่ 2") '8
        dt2.Columns.Add("IP") '9
        dt2.Columns.Add("บ.ประกัน") '10
        dt2.Columns.Add("TSR") '11

        dt2.Columns.Add("Package") '12
        'dt2.Columns.Add("Extension")
        dt2.Columns.Add("สถานะถ่ายรูป") '13
        dt2.Columns.Add("CusID") '14
        dt2.Columns.Add("AppID") '15
        dt2.Columns.Add("IDCar") '16
        dt2.Columns.Add("TypeTSR") '17

        For Count = 0 To dt.Rows.Count - 1
            Dim dtr As DataRow = dt2.NewRow
            str1 = FunAll.DispTypeTsr(dt.Rows(Count).Item("createid")) + "|"
            If dt.Rows(Count).Item("useridqc") <> 0 Or dt.Rows(Count).Item("useridqc") <> "" Then
                If dt.Rows(Count).Item("statusqc") = "9" Or dt.Rows(Count).Item("statusqc") = "10" Then
                    Str2 = "<FONT Color = 'Blue'>" & FunAll.qcname(dt.Rows(Count).Item("useridqc")) & "</FONT>"
                Else
                    Str2 = FunAll.qcname(dt.Rows(Count).Item("useridqc"))
                End If

            Else
                Str2 = ""
            End If

            Dim strapp As String = ""
            strapp = FunAll.StrApp1(dt.Rows(Count).Item("idcar"))
            If strapp.ToLower = "Old".ToLower Then
                strapp = "<font color='Navy'><b>" + CStr(Count + 1) + "</b></font> | <font color='red'>" + strapp + "||" + dt.Rows(Count).Item("appid") + "</font>"
            Else
                strapp = "<font color='Navy'><b>" + CStr(Count + 1) + "</b></font> | <font color='black'>" + strapp + "||" + dt.Rows(Count).Item("appid") + "</font>"
            End If
            dtr("No./AppID") = strapp
            If str1 = "Web Agent ไม่มีบัตรวินาศภัย|" Then
                dtr("ชื่อ-นามสกุล") = "<font color='Purple'>" + dt.Rows(Count).Item("ชื่อ-นามสกุล") + "|" + str1 + "</font>" + Str2
            Else
                dtr("ชื่อ-นามสกุล") = dt.Rows(Count).Item("ชื่อ-นามสกุล") + "|" + str1 + Str2
            End If


            dtr("SuccessDate") = dt.Rows(Count).Item("SuccessDate")


            dtr("สถานะ/ประเภทการชำระ") = FunAll.statusname(dt.Rows(Count).Item("curstatus")) + FunAll.apppay(dt.Rows(Count).Item("appid"))
            dtr("ยอดการชำระ") = FunAll.chkPayment(dt.Rows(Count).Item("appid"))
            dtr("วันคุ้มครอง") = dt.Rows(Count).Item("วันคุ้มครอง")
            dtr("หน้าตาราง") = dt.Rows(Count).Item("หน้าตาราง")
            dtr("ใบขับขี่ 1") = dt.Rows(Count).Item("ใบขับขี่ 1")
            dtr("ใบขับขี่ 2") = dt.Rows(Count).Item("ใบขับขี่ 2")
            dtr("CusID") = dt.Rows(Count).Item("CusID")
            dtr("IP") = dt.Rows(Count).Item("IP")
            dtr("TSR") = dt.Rows(Count).Item("TSR")
            If dt.Rows(Count).Item("productid") = 13 Then dtr("บ.ประกัน") = FunAll.pronamedesc(dt.Rows(Count).Item("productid"))
            If dt.Rows(Count).Item("productid") = 10 Then dtr("บ.ประกัน") = FunAll.pronamedesc(dt.Rows(Count).Item("productid"))
            If dt.Rows(Count).Item("productid") = 10 And (dt.Rows(Count).Item("typeprovalue") = 2 Or dt.Rows(Count).Item("typeprovalue") = 3 Or dt.Rows(Count).Item("typeprovalue") = 4) Then dtr("บ.ประกัน") = FunAll.pronamedesc(dt.Rows(Count).Item("productid")) + " (ชั้น 3,5)"
            If dt.Rows(Count).Item("pkgid") <> 456 And dt.Rows(Count).Item("pkgid") <> 457 Then
                dtr("Package") = FunAll.SetPackage(dt.Rows(Count).Item("appid"), dt.Rows(Count).Item("productid"), 1)
            Else
                dtr("Package") = FunAll.SingleRate(dt.Rows(Count).Item("appid"))
            End If
            dtr("สถานะถ่ายรูป") = dt.Rows(Count).Item("สถานะถ่ายรูป")
            dtr("AppID") = dt.Rows(Count).Item("appid")
            dtr("IDCar") = dt.Rows(Count).Item("IDCar")
            dtr("TypeTSR") = FunAll.DispTypeTsrID(dt.Rows(Count).Item("createid"))
            dt2.Rows.Add(dtr)
        Next

        Conn.Close()

        If dt2.Rows.Count > 0 Then
            UWGShowData.DataSource = dt2
            UWGShowData.DataBind()

            UWGShowData.Columns(0).Width = 120
            UWGShowData.Columns(1).Width = 370
            UWGShowData.Columns(1).CellStyle.HorizontalAlign = HorizontalAlign.Left
            UWGShowData.Columns(2).Width = 150
            UWGShowData.Columns(2).CellStyle.HorizontalAlign = HorizontalAlign.Center
            UWGShowData.Columns(3).Width = 160
            UWGShowData.Columns(3).CellStyle.HorizontalAlign = HorizontalAlign.Left
            UWGShowData.Columns(4).Width = 100
            UWGShowData.Columns(4).CellStyle.HorizontalAlign = HorizontalAlign.Right
            UWGShowData.Columns(5).Width = 135
            UWGShowData.Columns(5).CellStyle.HorizontalAlign = HorizontalAlign.Left
            UWGShowData.Columns(6).Width = 80
            UWGShowData.Columns(6).CellStyle.HorizontalAlign = HorizontalAlign.Center
            UWGShowData.Columns(7).Width = 80
            UWGShowData.Columns(7).CellStyle.HorizontalAlign = HorizontalAlign.Center
            UWGShowData.Columns(8).Width = 80
            UWGShowData.Columns(8).CellStyle.HorizontalAlign = HorizontalAlign.Center
            UWGShowData.Columns(9).Header.Style.Width = 0
            UWGShowData.Columns(9).Width = 0
            UWGShowData.Columns(10).Width = 85
            UWGShowData.Columns(10).CellStyle.HorizontalAlign = HorizontalAlign.Left
            UWGShowData.Columns(11).Width = 180
            UWGShowData.Columns(11).CellStyle.HorizontalAlign = HorizontalAlign.Left
            UWGShowData.Columns(12).Width = 80
            UWGShowData.Columns(12).CellStyle.HorizontalAlign = HorizontalAlign.Center
            UWGShowData.Columns(13).Header.Style.Width = 0
            UWGShowData.Columns(13).Width = 0
            UWGShowData.Columns(14).Header.Style.Width = 0
            UWGShowData.Columns(14).Width = 0
            UWGShowData.Columns(15).Header.Style.Width = 0
            UWGShowData.Columns(15).Width = 0
            UWGShowData.Columns(16).Header.Style.Width = 0
            UWGShowData.Columns(16).Width = 0
            UWGShowData.Columns(17).Header.Style.Width = 0
            UWGShowData.Columns(17).Width = 0

            Label3.Text = "Display Sound Recoder and App. : <font color='Green'>มีจำนวน Case ทั้งสิ้น " & UWGShowData.Rows.Count().ToString & " Case</font>"
        Else
            UWGShowData.DataSource = Nothing
            UWGShowData.DataBind()
            Label3.Text = "Display Sound Recoder and App. : <font color='red'>ไม่พบ Case ในการตรวจ</font>"
            UWGShowData.DisplayLayout.NoDataMessage = "<Font Size=4  Color='Red'>ไม่พบ Case ในการตรวจ</FONT>"
        End If
    End Sub

    Protected Sub wddAppStatus_SelectionChanged(ByVal sender As Object, ByVal e As Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs) Handles wddAppStatus.SelectionChanged
        ShowData(1)
    End Sub

    Protected Sub wddQC_SelectionChanged(ByVal sender As Object, ByVal e As Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs) Handles wddQC.SelectionChanged
        ShowData(1)
    End Sub

    Protected Sub wddTSR_SelectionChanged(ByVal sender As Object, ByVal e As Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs) Handles wddTSR.SelectionChanged
        ShowData(1)
    End Sub

    Protected Sub BtnRefresh_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles BtnRefresh.Click
        ShowData(1)
    End Sub

    Private Sub showDataapp(ByVal carx As String)

        Dim chkCOMP As Boolean
        Dim sPaid As Long
        Dim sDay, sDay1 As String
        Dim x As Integer
        Dim chkVIEWapp As Integer = 0

        Dim str As String = "select a.*,b.protypename as namecarpet from (SELECT   TblApplication.qcsuccessdate, TblApplication.successdate,TblApplication.submitdate,TblApplication.IDcar, TblApplication.AppID, Tbl_ProductType.ProTypeName, TblApplication.Typeprovalue, Tbl_Type.TypeName, TblApplication.ProValue, " + _
                              " TblApplication.ProPrice , TblApplication.IsCarpet, TblApplication.CarPet, TblApplication.ProDuctIDCarpet ,TblApplication.isprovalue ,TblApplication.appstatus,TblApplication.statusqc,TblApplication.appno,TblApplication.protectdate,tblapplication.comments + ' ' + TblApplication.Appcomment as Comment,TblApplication.PolicyNO , TblApplication.CarPetNO,tblapplication.useridqc,TblApplication.takephoto " + _
        "FROM TblApplication INNER JOIN " + _
                              "Tbl_ProductType ON TblApplication.ProDuctID = Tbl_ProductType.ProTypeID INNER JOIN " + _
                              " Tbl_Type ON TblApplication.Typeprovalue = Tbl_Type.Typeid) a inner join  Tbl_ProductType b on a.ProDuctIDCarpet = b.ProTypeID " + _
        " WHERE a.idcar = '" & carx & "' "

        Conn.Open()
        Dim Command As SqlCommand = New SqlCommand(str, Conn)
        Dim DataReader As SqlDataReader
        DataReader = Command.ExecuteReader()

        dt = New DataTable

        dt.Columns.Add("AppID")
        dt.Columns.Add("บ.ประกัน")
        dt.Columns.Add("ประเภท")
        dt.Columns.Add("ราคาประกัน")
        dt.Columns.Add("ทุน")
        dt.Columns.Add("สถานะประกัน")
        dt.Columns.Add("เลขรับแจ้ง")
        dt.Columns.Add("บ.ประกัน พรบ.")
        dt.Columns.Add("ราคา พรบ.")
        dt.Columns.Add("สถานะ พรบ.")
        dt.Columns.Add("วันแจ้งงาน")
        dt.Columns.Add("SubmitDate")
        dt.Columns.Add("วันที่จ่ายเงิน(งวดแรก)")
        dt.Columns.Add("สถานะตรวจสอบ")
        dt.Columns.Add("หมายเหตุ")
        dt.Columns.Add("QC ที่ตรวจ App")
        dt.Columns.Add("ตรวจสภาพรถ")
        dt.Columns.Add("หมายเหตุตรวจสภาพรถ")
        dt.Columns.Add("tmpAppID")

        Dim xx As String
        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt.NewRow
                If DataReader("appstatus") = 1 Then dtr("AppID") = "<font color='Green'>(ใช้งาน) " + CStr(DataReader("appid")) + "</font>"
                If DataReader("appstatus") = 0 Then dtr("AppID") = "<font color='Red'>(ยกเลิก) " + CStr(DataReader("appid")) + "</font>"
                dtr("tmpAppID") = DataReader("appid")
                dtr("บ.ประกัน") = DataReader("protypename")
                dtr("ประเภท") = DataReader("typename")
                dtr("ราคาประกัน") = Format(DataReader("provalue"), "###,###,##0.#0")
                dtr("ทุน") = Format(DataReader("proprice"), "###,###,##0.#0")
                dtr("สถานะประกัน") = "มีประกัน"
                If DataReader("isprovalue") = 0 Then dtr("สถานะประกัน") = "ไม่มีประกัน"
                dtr("เลขรับแจ้ง") = DataReader("appno") + " || " + DataReader("PolicyNO")
                dtr("บ.ประกัน พรบ.") = DataReader("namecarpet") + " || " + DataReader("CarPetNO")
                dtr("ราคา พรบ.") = Format(DataReader("carpet"), "###,###,##0.#0")
                dtr("สถานะ พรบ.") = "มีพรบ."
                If DataReader("iscarpet") = 0 Then dtr("สถานะ พรบ.") = "ไม่มีพรบ."

                Dim qcdate As String
                If IsDBNull(DataReader("qcsuccessdate")) = True Then
                    qcdate = ""
                Else
                    qcdate = CStr(Format(DataReader("qcsuccessdate"), "dd/MM/yyyy HH:mm:ss"))
                End If
                dtr("วันแจ้งงาน") = CStr(Format(DataReader("successdate"), "dd/MM/yyyy HH:mm:ss")) + " || <FONT Color='Navy'><b>" + CStr(Format(DataReader("protectdate"), "dd/MM/yyyy")) + "</b></FONT> || " + qcdate
                If IsDBNull(DataReader("submitdate")) = False Then dtr("submitdate") = DataReader("submitdate")
                'dtr("วันที่จ่ายเงิน(งวดแรก)")
                dtr("สถานะตรวจสอบ") = DataReader("statusqc")
                dtr("หมายเหตุ") = DataReader("comment")
                dtr("QC ที่ตรวจ App") = DataReader("useridqc")
                If DataReader("takephoto") = 1 Then dtr("ตรวจสภาพรถ") = "ASN ตรวจสภาพ"
                ' dtr("หมายเหตุตรวจสภาพรถ") = FunAll.DTakephoto(DataReader("appid"))
                dt.Rows.Add(dtr)
            End While
        End If
        DataReader.Close()

        dt2 = New DataTable

        dt2.Columns.Add("AppID") '0
        dt2.Columns.Add("บ.ประกัน") '1
        dt2.Columns.Add("ประเภท") '2
        dt2.Columns.Add("ราคาประกัน") '3
        dt2.Columns.Add("ทุน") '4
        dt2.Columns.Add("สถานะประกัน") '5
        dt2.Columns.Add("เลขรับแจ้ง || เลขกรมธรรม์") '6
        dt2.Columns.Add("บ.ประกัน พ.ร.บ. | เลข พ.ร.บ.") '7
        dt2.Columns.Add("ราคา พ.ร.บ.") '8
        dt2.Columns.Add("สถานะ พ.ร.บ.") '9
        dt2.Columns.Add("วันแจ้งงาน || วันคุ้มครอง || QC SuccessDate") '10
        dt2.Columns.Add("SubmitDate") '11
        dt2.Columns.Add("วันที่จ่ายเงิน(งวดแรก)") '12
        dt2.Columns.Add("สถานะตรวจสอบ") '13
        dt2.Columns.Add("หมายเหตุ") '14
        dt2.Columns.Add("QC ที่ตรวจ App") '15
        dt2.Columns.Add("ตรวจสภาพรถ") '16
        dt2.Columns.Add("หมายเหตุตรวจสภาพรถ") '17



        For Count = 0 To dt.Rows.Count - 1
            chkCOMP = False : sPaid = 0
            sDay = ""
            sDay1 = ""
            Dim dtr As DataRow = dt2.NewRow
            str = "select payvalue,paydate,iscomplete,createdate from tblpayment where appid = '" & dt.Rows(Count).Item("tmpAppID") & "' order by payno"
            Dim Command2 As SqlCommand = New SqlCommand(str, Conn)
            DataReader = Command2.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read

                    If DataReader("iscomplete") = 0 Then dtr("วันที่จ่ายเงิน(งวดแรก)") = Format(DataReader("paydate"), "dd/MM/yyyy") + " >> " + Format(DataReader("payvalue"), "###,###,##0.#0") + "||วันที่ Key เข้าระบบ > " + Format(DataReader("createdate"), "dd/MM/yyyy")
                    If DataReader("iscomplete") = 1 Then dtr("วันที่จ่ายเงิน(งวดแรก)") = Format(DataReader("paydate"), "dd/MM/yyyy") + " >> " + Format(DataReader("payvalue"), "###,###,##0.#0") + " (ชำระครบ)" + "|| วันที่ Key เข้าระบบ > " + Format(DataReader("createdate"), "dd/MM/yyyy")

                End While
            Else

                While DataReader.Read
                    x = x + 1
                    If IsDBNull(DataReader("PayValue")) = False Then sPaid = sPaid + DataReader("PayValue")
                    sDay = Format(DataReader("paydate"), "dd/MM/yyyy")
                    sDay1 = "||วันที่ Key เข้าระบบ > " + Format(DataReader("createdate"), "dd/MM/yyyy")
                    If DataReader("isComplete") = 1 Then chkCOMP = True
                End While

                If chkCOMP = True Then
                    If sDay <> "" Then
                        sDay = Format(sDay, "dd/MM/yyyy")
                    End If
                    dtr("วันที่จ่ายเงิน(งวดแรก)") = "วันที่ชำระล่าสุด : " & sDay & "  ชำระรวม : " & Format(sPaid, "###,###,##0.#0") & " (ชำระครบ)" + sDay1
                Else
                    If sDay <> "" Then
                        sDay = Format(sDay, "dd/MM/yyyy")
                    End If
                    dtr("วันที่จ่ายเงิน(งวดแรก)") = "วันที่ชำระล่าสุด : " & sDay & "  ชำระรวม : " & Format(sPaid, "###,###,##0.#0") & " ชำระเข้ามา: " & x & " งวด" + sDay1
                End If
            End If
            dtr("AppID") = dt.Rows(Count).Item("AppID")
            dtr("บ.ประกัน") = dt.Rows(Count).Item("บ.ประกัน")
            dtr("ประเภท") = dt.Rows(Count).Item("ประเภท")
            dtr("ราคาประกัน") = dt.Rows(Count).Item("ราคาประกัน")
            dtr("ทุน") = dt.Rows(Count).Item("ทุน")
            dtr("สถานะประกัน") = dt.Rows(Count).Item("สถานะประกัน")
            dtr("เลขรับแจ้ง || เลขกรมธรรม์") = dt.Rows(Count).Item("เลขรับแจ้ง")
            dtr("บ.ประกัน พ.ร.บ. | เลข พ.ร.บ.") = dt.Rows(Count).Item("บ.ประกัน พรบ.")
            dtr("ราคา พ.ร.บ.") = dt.Rows(Count).Item("ราคา พรบ.")
            dtr("สถานะ พ.ร.บ.") = dt.Rows(Count).Item("สถานะ พรบ.")
            dtr("วันแจ้งงาน || วันคุ้มครอง || QC SuccessDate") = dt.Rows(Count).Item("วันแจ้งงาน")
            dtr("SubmitDate") = dt.Rows(Count).Item("SubmitDate")
            dtr("สถานะตรวจสอบ") = FunAll.statusqc(dt.Rows(Count).Item("สถานะตรวจสอบ"))
            dtr("หมายเหตุ") = dt.Rows(Count).Item("หมายเหตุ")
            dtr("QC ที่ตรวจ App") = FunAll.qcname(dt.Rows(Count).Item("QC ที่ตรวจ App"))
            dtr("ตรวจสภาพรถ") = dt.Rows(Count).Item("ตรวจสภาพรถ")
            dtr("หมายเหตุตรวจสภาพรถ") = FunAll.DTakephoto(dt.Rows(Count).Item("tmpAppID"))

            dt2.Rows.Add(dtr)
            DataReader.Close()
        Next
        Conn.Close()

        If dt.Rows.Count > 0 Then
            UltraWebGrid1.DataSource = dt2
            UltraWebGrid1.DataBind()

            UltraWebGrid1.Columns(0).Width = 100
            UltraWebGrid1.Columns(0).CellStyle.HorizontalAlign = HorizontalAlign.Center
            UltraWebGrid1.Columns(1).Width = 220
            UltraWebGrid1.Columns(1).CellStyle.HorizontalAlign = HorizontalAlign.Left
            UltraWebGrid1.Columns(2).Width = 60
            UltraWebGrid1.Columns(2).CellStyle.HorizontalAlign = HorizontalAlign.Center
            UltraWebGrid1.Columns(3).Width = 90
            UltraWebGrid1.Columns(3).CellStyle.HorizontalAlign = HorizontalAlign.Right
            UltraWebGrid1.Columns(4).Width = 90
            UltraWebGrid1.Columns(4).CellStyle.HorizontalAlign = HorizontalAlign.Right
            UltraWebGrid1.Columns(5).Width = 100
            UltraWebGrid1.Columns(5).CellStyle.HorizontalAlign = HorizontalAlign.Center
            UltraWebGrid1.Columns(6).Width = 200
            UltraWebGrid1.Columns(6).CellStyle.HorizontalAlign = HorizontalAlign.Center
            UltraWebGrid1.Columns(7).Width = 220
            UltraWebGrid1.Columns(7).CellStyle.HorizontalAlign = HorizontalAlign.Center
            UltraWebGrid1.Columns(8).Width = 80
            UltraWebGrid1.Columns(8).CellStyle.HorizontalAlign = HorizontalAlign.Right
            UltraWebGrid1.Columns(9).Width = 100
            UltraWebGrid1.Columns(9).CellStyle.HorizontalAlign = HorizontalAlign.Center
            UltraWebGrid1.Columns(10).Width = 330
            UltraWebGrid1.Columns(11).Width = 120
            UltraWebGrid1.Columns(11).CellStyle.HorizontalAlign = HorizontalAlign.Center
            UltraWebGrid1.Columns(12).Width = 400
            UltraWebGrid1.Columns(13).Width = 150
            UltraWebGrid1.Columns(14).Width = 500
            UltraWebGrid1.Columns(15).Width = 150
            UltraWebGrid1.Columns(16).Width = 0
            UltraWebGrid1.Columns(16).Header.Style.Width = 0
            UltraWebGrid1.Columns(17).Header.Style.Width = 0
            UltraWebGrid1.Columns(17).Width = 0
        Else
            UltraWebGrid1.DataSource = Nothing
            UltraWebGrid1.DataBind()

        End If




    End Sub

    Protected Sub DisplayControl()
        'Reminder by na 20/11/2014
        btnAddReminder.Enabled = True
        'End
        btnHistory.Enabled = True
        btnEditApp.Enabled = True
        BtnOpenSound.Enabled = True
        BtnOpenFile.Enabled = True
        btnAddStatus.Enabled = True
        btnPass.Enabled = False
        btnNotPass.Enabled = True
        btnHoldOn.Enabled = True
        btnHangUp.Enabled = True
        wddCusStatus.Enabled = True
        btnUpdate.Enabled = True
        wddTelType.Enabled = True
        wddMStatus.Enabled = True
        wddSStatus.Enabled = True
        Panel2.Visible = True
        btnCalculate.Enabled = True
        btnOldApp.Enabled = True
        btnPreview.Enabled = True
        txtPhoneNo.Text = ""
        wddTelType.SelectedValue = 0
        txtQCcomment.Text = ""
        wddMStatus.SelectedValue = 0
        wddSStatus.CurrentValue = "- กรุณาเลือก -"
        UltraWebGrid2.DataSource = Nothing
        UltraWebGrid2.DataBind()
        UltraWebGrid2.Visible = False
    End Sub

    Protected Sub UWGShowData_SelectedRowsChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.SelectedRowsEventArgs) Handles UWGShowData.SelectedRowsChange


        If e.SelectedRows.Count = 1 Then

            'exit commect 21/08/2014
            'If Request.Cookies("UserID").Value = "2896" Then
            '    If wddAppStatus.SelectedItemIndex <= 0 Then
            '        If UWGShowData.DisplayLayout.ActiveRow.Index > 0 Then
            '            ClearData()
            '            showDataapp(e.SelectedRows.Item(0).Cells(16).Text)
            '            UltraWebGrid1.Visible = True
            '            Exit Sub
            '        End If
            '    End If
            'End If
            'Reminder Byna 21/11/2014
            lblappidreminder.Text = e.SelectedRows.Item(0).Cells(15).Text
            showReminder(e.SelectedRows.Item(0).Cells(15).Text)
            'End
            showDataapp(e.SelectedRows.Item(0).Cells(16).Text)
            showcomment(e.SelectedRows.Item(0).Cells(15).Text)
            lblCarid.Text = e.SelectedRows.Item(0).Cells(16).Text
            lblApp.Text = e.SelectedRows.Item(0).Cells(15).Text
            lblCusID.Text = e.SelectedRows.Item(0).Cells(14).Text
            lblTypeTSR.Text = e.SelectedRows.Item(0).Cells(17).Text
            lblExten.Text = FunAll.Exten(e.SelectedRows.Item(0).Cells(15).Text)
            HDAppID.Value = e.SelectedRows.Item(0).Cells(15).Text
            HDCusName.Value = FunAll.ShowCusName(e.SelectedRows.Item(0).Cells(14).Text)
            HDTsr.Value = e.SelectedRows.Item(0).Cells(11).Text
            DisplayControl()
            chkPhoto.Checked = chkAreaPhoto(lblApp.Text)
            Dim Sql As String = "SELECT CurStatus FROM TblCar where IdCar = '" & e.SelectedRows.Item(0).Cells(16).Text & "'"
            Conn.Open()
            com = New SqlCommand(Sql, Conn)
            Dim dr As SqlDataReader = com.ExecuteReader()
            If dr.HasRows Then
                While dr.Read
                    If IsDBNull(dr("CurStatus")) = False Then
                        wddCusStatus.SelectedValue = dr("CurStatus")
                    End If
                End While
            End If
            dr.Close()

            Sql = "SELECT Mobile,'-' As TelExt FROM TblCustomer WHERE CusID = '" & e.SelectedRows.Item(0).Cells(14).Text & "'"
            com = New SqlCommand(Sql, Conn)
            dr = com.ExecuteReader()
            If dr.HasRows Then
                While dr.Read
                    If IsDBNull(dr("Mobile")) = False Then
                        txtPhoneNo.Text = Trim(dr("Mobile"))
                    Else
                        txtPhoneNo.Text = ""
                    End If
                    If IsDBNull(dr("TelExt")) = False Then
                        lblExt.Text = dr("TelExt")
                    End If
                End While
            End If
            dr.Close()


            Sql = "SELECT TblStatus.StatusName as StatusName  FROM TblCar INNER Join TblStatus ON TblCar.CurStatus = TblStatus.StatusID WHERE IdCar = '" & e.SelectedRows.Item(0).Cells(16).Text & "'"
            com = New SqlCommand(Sql, Conn)
            dr = com.ExecuteReader()
            If dr.HasRows Then
                While dr.Read
                    If IsDBNull(dr("StatusName")) = False Then
                        HDStatus.Value = Trim(dr("StatusName"))
                    Else
                        HDStatus.Value = "ไม่พบสถานะ"
                    End If

                End While
            End If
            dr.Close()

            Conn.Close()

        End If



    End Sub

    Protected Sub btnAssign_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnAssign.Click
        AssignCase(lblApp.Text)
    End Sub

    Protected Sub AssignCase(ByVal appidx As String)
        If wddAssign.SelectedItemIndex <= 0 Then

            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('กรุณาเลือกรายชื่อ QC !!');", True)
            Exit Sub
        End If
        If appidx = "" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('กรุณาเลือก App จากตารางด้านล่าง !!');", True)
            Exit Sub
        End If
        Dim str As String = "update tblapplication set useridqc = '" & wddAssign.SelectedValue & "' where appid = '" & appidx & "'"
        Conn.Open()
        Dim Command As SqlCommand = New SqlCommand(str, Conn)
        Dim chk As Integer = Command.ExecuteNonQuery()
        If chk > 0 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('Assign งานให้กับ " & wddAssign.SelectedItem.ToString & " แล้ว !!');", True)
        End If
        Conn.Close()
        ShowData(1)
        appidx = ""
    End Sub

    Protected Sub showcomment(ByVal appidx As String)

        Dim str As String = "select * from tblqc where appid = '" & appidx & "' order by  createdate desc"
        Conn.Open()
        Dim Command As SqlCommand = New SqlCommand(str, Conn)
        Dim DataReader As SqlDataReader
        DataReader = Command.ExecuteReader()

        dt = New DataTable

        'dt.Columns.Add("No.1")
        dt.Columns.Add("หมายเหตุ")
        dt.Columns.Add("วันที่ QC > TSR")
        dt.Columns.Add("วันที่ QC < TSR")
        Dim Count As Integer = 0
        If DataReader.HasRows Then
            While DataReader.Read()
                Dim dtr As DataRow = dt.NewRow
                'dtr("No.1") = Count + 1
                If IsDBNull(DataReader("comment1")) = False Then
                    dtr("หมายเหตุ") = DataReader("comment1")
                End If
                If IsDBNull(DataReader("createdate")) = False Then
                    dtr("วันที่ QC > TSR") = Format(DataReader("createdate"), "dd/MM/yyyy HH:mm:ss")
                End If
                If IsDBNull(DataReader("AnswerDate")) = False Then
                    dtr("วันที่ QC < TSR") = Format(DataReader("AnswerDate"), "dd/MM/yyyy HH:mm:ss")
                End If
                dt.Rows.Add(dtr)
            End While
        End If
        DataReader.Close()
        Conn.Close()

        If dt.Rows.Count > 0 Then
            uwgComment.DataSource = dt
            uwgComment.DataBind()

            uwgComment.Columns(0).Width = 650
            uwgComment.Columns(1).Width = 130
            uwgComment.Columns(2).Width = 130

            uwgComment.Visible = True
        Else
            uwgComment.DataSource = Nothing
            uwgComment.DataBind()
            uwgComment.Visible = False
        End If
    End Sub

    Protected Sub btnHistory_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnHistory.Click

        ScriptManager.RegisterStartupScript(Page, Page.GetType, "ViewHistory", "PopUpHistory(" & lblApp.Text & "," & lblCarid.Text & ")", True)
    End Sub

    Protected Sub btnViewJob_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnViewJob.Click

        ScriptManager.RegisterStartupScript(Page, Page.GetType, "ViewJob", "PopUpView()", True)

    End Sub

    Protected Sub wddMStatus_SelectionChanged(ByVal sender As Object, ByVal e As Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs) Handles wddMStatus.SelectionChanged
        SqlSubStatus.SelectParameters("MQcid").DefaultValue = wddMStatus.SelectedValue
        wddSStatus.DataBind()
    End Sub

    Protected Sub wddMStatus_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles wddMStatus.DataBound

    End Sub

    Protected Sub btnAddStatus_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnAddStatus.Click
        If wddMStatus.SelectedItemIndex <= 0 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('กรุณาเลือกสถานะหลัก !!');", True)
            Exit Sub
        End If

        If wddSStatus.SelectedItemIndex <= 0 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('กรุณาเลือกสถานะรอง !!');", True)
            Exit Sub
        End If

        If ViewState("Status") IsNot Nothing Then
            dt = New DataTable
            dt = ViewState("Status")
            AddStatus()
            UltraWebGrid2.DataSource = dt
            UltraWebGrid2.DataBind()
        Else
            CreateStatus()
            AddStatus()
            UltraWebGrid2.DataSource = dt
            UltraWebGrid2.DataBind()
        End If
        If UltraWebGrid2.Rows.Count > 0 Then
            UltraWebGrid2.Visible = True
        Else
            UltraWebGrid2.Visible = False
        End If
    End Sub

    Protected Sub CreateStatus()
        dt = New DataTable
        dt.Columns.Add("StatusID")
        dt.Columns.Add("หัวข้อการแก้ไข")
    End Sub

    Protected Sub AddStatus()
        Dim dtr As DataRow = dt.NewRow
        dtr("หัวข้อการแก้ไข") = wddSStatus.SelectedItem.ToString
        dtr("StatusID") = wddSStatus.SelectedValue
        dt.Rows.Add(dtr)
        ViewState("Status") = dt
    End Sub

    Protected Sub btnHangUp_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnHangUp.Click
        Dim CallUrl As String = ""

        CallUrl += Replace(ConfigurationManager.AppSettings("PhoneEndIn"), "@IPAsterisk", Request.Cookies("ipAsterisk").Value) & "exten=" & Request.Cookies("Extension").Value
        TelAjax = CallUrl
        Label8.Text = "โทรหาลูกค้า : "
        'txtPhoneNo.Text = ""
        'btnHoldOn.Enabled = True
    End Sub

    Private Sub AlertMsg()
        If lblApp.Text = "" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('ไม่พบเลข App กรุณาเลือกข้อมูลจากตางรางด้านบน !!');", True)
            Exit Sub
        End If
        If lblCusID.Text = "" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('ไม่พบเลข App กรุณาเลือกข้อมูลจากตางรางด้านบน !!');", True)
            Exit Sub
        End If
        If lblCarid.Text = "" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('ไม่พบเลข App กรุณาเลือกข้อมูลจากตางรางด้านบน !!');", True)
            Exit Sub
        End If
    End Sub

    Private Sub SaveStatus()
        If wddCusStatus.SelectedItemIndex <= 0 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('กรุณาเลือกสถานะลูกค้า !!');", True)
            Exit Sub
        End If

        AlertMsg()

        Dim tmp As String = Request.ServerVariables("REMOTE_ADDR") '+ " : " + System.Net.Dns.GetHostEntry(Request.UserHostAddress).HostName
        Dim NewStatus As String = wddCusStatus.SelectedValue
        Conn.Open()
        Dim str, TmpStatus As String
        Dim Command As SqlCommand
        Dim Dr As SqlDataReader

        str = "Select Curstatus FROM TblCar WHERE IdCar = '" & lblCarid.Text & "'" 'Query OldStatus
        Command = New SqlCommand(str, Conn)
        Dr = Command.ExecuteReader()
        If Dr.HasRows Then
            While Dr.Read
                If IsDBNull(Dr("CurStatus")) = False Then
                    TmpStatus = Dr("CurStatus")
                End If
            End While
        End If
        Dr.Close()

        Tran = Conn.BeginTransaction()

        Try
            str = "Update TblCar Set Curstatus = '" & NewStatus & "' where idcar = " & lblCarid.Text 'Update NewStatus
            Command = New SqlCommand(str, Conn)
            Command.Transaction = Tran
            Dim chk As Integer = Command.ExecuteNonQuery()
            Dim chk3 As Integer
            If chk > 0 Then 'Insert yo Table-Restatus
                str = "Insert into TblRestatus(cusid,carid,status_new,createid,createdate,hostaccess,restatusid,status_old) values('" & lblCusID.Text & "','" & lblCarid.Text & "','" & NewStatus & "'," & Request.Cookies("UserID").Value & ",getdate(),'" & tmp & "',2,'" & TmpStatus & "')"
                Command = New SqlCommand(str, Conn)
                Command.Transaction = Tran
                Dim chk2 As Integer = Command.ExecuteNonQuery()
                If chk2 > 0 Then

                    If NewStatus = 3 Then ' Case Succes Flagwait = NULL
                        str = "update tblapplication set flagwait  = null where appid = '" & lblApp.Text & "'"
                        Command = New SqlCommand(str, Conn)
                        Command.Transaction = Tran
                        chk3 = Command.ExecuteNonQuery()
                    End If

                    If NewStatus = 4 Then 'Case Wait FlagWait = 1(true)
                        str = "update tblapplication set flagwait  = 1 where appid = '" & lblApp.Text & "'"
                        Command = New SqlCommand(str, Conn)
                        Command.Transaction = Tran
                        chk3 = Command.ExecuteNonQuery()
                    End If

                End If
            End If
            Tran.Commit()

            Conn.Close()

            If chk3 > 0 Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('เปลี่ยนสถานะลูกค้าเรียบร้อย !!');", True)
            End If

            ShowData(1)


        Catch ex As Exception
            Tran.Rollback()
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('พบปัญหา " & ex.Message & " กรุณาติดต่อ IT (โทร 811) !!');", True)

            Conn.Close()

        End Try


    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnUpdate.Click
        SaveStatus()
    End Sub

    Protected Sub UltraWebGrid2_SelectedRowsChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.SelectedRowsEventArgs) Handles UltraWebGrid2.SelectedRowsChange
        dt = New DataTable
        dt = ViewState("Status")
        dt.Rows(e.SelectedRows.Item(0).Index).Delete()
        ViewState("Status") = dt
        UltraWebGrid2.DataSource = dt
        UltraWebGrid2.DataBind()
        If UltraWebGrid2.Rows.Count > 0 Then
            UltraWebGrid2.Visible = True
        Else
            UltraWebGrid2.Visible = False
        End If
    End Sub

    Protected Sub btnPass_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnPass.Click
        If HDConfirm.Value = "1" Then
            QCPASS()
        End If
        HDConfirm.Value = ""

    End Sub
    Private Sub sendSms(ByVal appid As String)
        'ส่ง sms
        '1.
        Dim Command As SqlCommand
        Dim DataReader As SqlDataReader
        Dim Payvalue As Decimal
        Dim Refno1 As String = ""
        Dim Refno2 As String = ""
        Dim phoneto As String = ""
        Conn.Open()
        Dim Str As String = ""

        Str = " SELECT    distinct    a1.totalpay,assignto, TblCustomer.FNameTH, TblCustomer.LNameTH,tblcar.carid,mobile as tel,CarBrand,tblcar.RefNo,tblapplication.appid"
        Str += " FROM TblApplication "
        Str += " INNER JOIN   TblCar ON TblApplication.Idcar = TblCar.IdCar "
        Str += " INNER JOIN   TblCustomer ON TblCar.CusID = TblCustomer.CusID "
        Str += " inner join   (select  appid,totalpay  from tblapppay where payid  = 1) a1"
        Str += "       on tblapplication.appid = a1.appid"
        Str += "      where assignto in (select   userid   from tbluser where userlevelid = 5 "
        Str += " and userstatus = 1  and LeaderID in (1885,4698)) "
        Str += " and curstatus = 3 "
        Str += " and CarBrand like '%ISUZU%'"
        Str += " and tblapplication.appid in (select  appid from tblapppay  where payid = 1 and Typepay = 1)"
        Str += " and tblapplication.appid not in (select appid from tblpayment )"
        Str += " and appstatus = 1 and tblapplication.appid= '" & appid & "' "

        Command = New SqlCommand(Str, Conn)
        DataReader = Command.ExecuteReader



        If DataReader.HasRows Then
            While DataReader.Read

                If IsDBNull(DataReader("totalpay")) = False Then
                    Payvalue = DataReader("totalpay")
                End If
                If IsDBNull(DataReader("RefNo")) = False Then
                    Refno1 = DataReader("RefNo")
                End If
                If IsDBNull(DataReader("appid")) = False Then
                    Refno2 = DataReader("appid")
                End If
                If IsDBNull(DataReader("tel")) = False Then
                    phoneto = DataReader("tel")
                End If

            End While
            DataReader.Close()
            Conn.Close()

            Dim strDetail As String = ""
            Dim strDetail1 As String = ""


            If (phoneto <> "") Then
                If (phoneto <> ("66" + phoneto.Substring((phoneto.Length - 9)))) Then
                    phoneto = ("66" + phoneto.Substring((phoneto.Length - 9)))
                End If
            End If

            Dim mess As String = strDetail
            strDetail1 = StringUnicode(mess)
            Dim ToGroup() As String = phoneto.Split(Microsoft.VisualBasic.ChrW(44))
            Dim to1 As String
            For Each to1 In ToGroup
                OnPostInfoClick(to1, Payvalue, Refno1, Refno2)
                SaveLogSmSForWEB(to1, Payvalue, Refno1, Refno2)
            Next
        Else
            DataReader.Close()
            Conn.Close()
        End If

    End Sub

    Private Sub SaveLogSmSForWEB(ByVal ToNo As String, ByVal Payvalue As Decimal, ByVal Refno1 As String, ByVal Refno2 As String)

        Conn.Open()

        Dim mess As String = "ขอขอบคุณลูกค้าเลือกประกันภัยรถยนต์กับบ.เอเอสเอ็นโบรกเกอร์ จำกัด(มหาชน)ชำระที่7-11เบี้ย" & Payvalue.ToString("##,###") & "บ.รหัส3031815285 R1# " & Refno1 & " R2# " & Refno2 & " สอบถามโทร.026191717"
        Dim str As String
        str = "INSERT INTO TblLogQCSendSMS (Appid, mobile, Msg, PayV,Createid) VALUES ('" & Refno2 & "', '" & ToNo & "', '" & mess & "', '" & Payvalue & "','" & Request.Cookies("UserID").Value & "')"

        Dim Command As SqlCommand = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()
        Conn.Close()
    End Sub

    Private Sub OnPostInfoClick(ByVal ToNo As String, ByVal Payvalue As Decimal, ByVal Refno1 As String, ByVal Refno2 As String)

        Dim encoding As ASCIIEncoding = New ASCIIEncoding
        Dim mess As String = "ขอขอบคุณลูกค้าเลือกประกันภัยรถยนต์กับบ.เอเอสเอ็นโบรกเกอร์ จำกัด(มหาชน)ชำระที่7-11เบี้ย" & Payvalue.ToString("##,###") & "บ.รหัส3031815285 R1# " & Refno1 & " R2# " & Refno2 & " สอบถามโทร.026191717"
        Dim postData As String = "CMD=SENDMSG"
        postData = postData + "&TRANSID=BULK"
        postData = postData + "&FROM=ASNBroker"
        postData = postData + "&TO=" + ToNo
        postData = postData + "&CHARGE=Y"
        postData = postData + "&REPORT=Y"
        postData = postData + "&CODE=Asquarenetwork_BulkSMS"
        postData = postData + "&CTYPE=UNICODE"
        postData = postData + "&CONTENT=" + StringUnicode(mess)

        Dim data() As Byte = encoding.GetBytes(postData)
        ' Prepare web request...
        Dim myRequest As HttpWebRequest = CType(WebRequest.Create("http://203.170.228.190:3419/"), HttpWebRequest)
        myRequest.Method = "POST"
        myRequest.ContentType = "application/x-www-form-urlencoded"
        myRequest.ContentLength = data.Length
        Dim newStream As Stream = myRequest.GetRequestStream
        ' Send the data.
        newStream.Write(data, 0, data.Length)
        newStream.Close()
    End Sub
    Private Function StringUnicode(ByVal messageString As String) As String
        Dim str_unicode As String = String.Empty
        Try
            Dim obj_unicode() As Byte = Encoding.Unicode.GetBytes(messageString)
            Dim i As Integer = 0
            Do While (i < obj_unicode.Length)
                str_unicode = ((str_unicode + ("%" _
                            + (obj_unicode((i + 1)).ToString("X").PadLeft(2, Microsoft.VisualBasic.ChrW(48)) + "%"))) _
                            + obj_unicode(i).ToString("X").PadLeft(2, Microsoft.VisualBasic.ChrW(48)))
                i = (i + 2)
            Loop
        Catch ex As Exception
            Return messageString
        End Try
        Return str_unicode
    End Function
    Private Sub AutoSignSuccessDoc()
        Dim str As String
        Dim Command As SqlCommand
        Dim DataReader As SqlDataReader
        Dim CountApp As Integer = 0
        Dim User As Integer
        Conn.Open()

        str = "SELECT a.AppID,a.ProductID " + _
              " from TblCustomer c INNER JOIN " + _
              " TblCar b ON c.CusID = b.CusID INNER JOIN " + _
              " TblApplication a ON b.IdCar = a.Idcar " + _
              " where  a.statusqc = 7 and b.curstatus = 3 and a.appid = '" & lblApp.Text & "' and a.appid not in (SELECT appid from TblQCCheckDoc) " + _
              " and (((a.doc1+a.doc2+a.doc3+a.doc4+a.doc5+a.doc6) <> (a.isdoc1+a.isdoc2+a.isdoc3+a.isdoc4+a.isdoc5+a.isdoc6)) or ((RTRIM(LTrim(c.IDcard)) = '') or (LEN(c.IDcard) < 13)))"
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader()

        dt = New DataTable
        dt.Columns.Add("AppID")
        dt.Columns.Add("ProductID")

        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt.NewRow
                If IsDBNull(DataReader("AppID")) = False Then
                    dtr("AppID") = DataReader("AppID")
                End If
                If IsDBNull(DataReader("ProductID")) = False Then
                    dtr("ProductID") = DataReader("ProductID")
                End If
                dt.Rows.Add(dtr)
            End While
        End If
        DataReader.Close()

        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1
                str = "SELECT Top(1) AssignTo FROM TblQCCheckDoc ORDER BY CreateDate DESC"
                Command = New SqlCommand(str, Conn)
                DataReader = Command.ExecuteReader()
                If DataReader.HasRows Then
                    While DataReader.Read
                        User = CInt(DataReader("AssignTo"))
                    End While
                End If
                DataReader.Close()

                If User = 3297 Then
                    str = "INSERT INTO TblQCCheckDoc (AppID, AssignTo, CreateDate, CreateID, DocStatus, ProductID) VALUES ('" & dt.Rows(i).Item("AppID") & "', 3347, GETDATE(),'" & Request.Cookies("UserID").Value & "',0,'" & dt.Rows(i).Item("ProductID") & "')"
                ElseIf User = 3347 Then
                    str = "INSERT INTO TblQCCheckDoc (AppID, AssignTo, CreateDate, CreateID, DocStatus, ProductID) VALUES ('" & dt.Rows(i).Item("AppID") & "', 3297, GETDATE(),'" & Request.Cookies("UserID").Value & "',0,'" & dt.Rows(i).Item("ProductID") & "')"
                Else
                    str = "INSERT INTO TblQCCheckDoc (AppID, AssignTo, CreateDate, CreateID, DocStatus, ProductID) VALUES ('" & dt.Rows(i).Item("AppID") & "', 3297, GETDATE(),'" & Request.Cookies("UserID").Value & "',0,'" & dt.Rows(i).Item("ProductID") & "')"
                End If

                Command = New SqlCommand(str, Conn)
                Command.ExecuteNonQuery()
            Next
        End If

        Conn.Close()

    End Sub

    Protected Sub btnNotPass_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnNotPass.Click

        'LINEGenPayment()

        If HDConfirm2.Value = "1" Then
            If wddMStatus.SelectedItemIndex <= 0 Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('กรุณาเลือกสถานะหลัก !!');", True)
                Exit Sub
            End If
            If wddSStatus.SelectedItemIndex <= 0 Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('กรุณาเลือกสถานะรอง !!');", True)
                Exit Sub
            End If
            If UltraWebGrid2.Rows.Count <= 0 Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('กรุณาเพื่มข้อมูลสถานะก่อน !!');", True)
                Exit Sub
            End If

            QCNOTPASS()
        End If
        HDConfirm2.Value = ""

        txtQCcomment.Text = ""
        wddMStatus.SelectedValue = 0
        wddSStatus.CurrentValue = "- กรุณาเลือก -"
        UltraWebGrid2.DataSource = Nothing
        UltraWebGrid2.DataBind()
        UltraWebGrid2.Visible = False
        ViewState("Status") = ""
        ViewState("Status") = Nothing
    End Sub
    Private Sub QCNOTPASS()


        Dim str As String
        Dim Command As SqlCommand
        Dim Datareader As SqlDataReader

        Conn.Open()

        dt = New DataTable

        dt.Columns.Add("Tmp")
        dt.Columns.Add("typetsr")
        str = "select useridqc,typetsr from tblapplication where appid = '" & lblApp.Text & "'"
        Command = New SqlCommand(str, Conn)
        Datareader = Command.ExecuteReader
        If Datareader.HasRows Then
            While Datareader.Read
                Dim dtr As DataRow = dt.NewRow
                If IsDBNull(Datareader("useridqc")) = False Then
                    dtr("Tmp") = Datareader("useridqc")
                End If
                If IsDBNull(Datareader("typetsr")) = False Then
                    dtr("typetsr") = Datareader("typetsr")
                End If
                dt.Rows.Add(dtr)
            End While
        End If
        Datareader.Close()

        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("Tmp") = 0 Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('ไม่สามารถส่ง App ได้เนื่องจากยังไม่ผ่านการตรวจจาก QC !!');", True)
                'str = "delete  from TblPrintCheckErrorQC"
                'Command = New SqlCommand(str, Conn)
                'Command.ExecuteNonQuery()
                Exit Sub
            End If
        End If

        If dt.Rows.Count > 0 Then
            If Request.Cookies("userLevel").Value = "8" And Request.Cookies("TypeTsr").Value <> "0" Then '8 type 0 คือ Qcพิเศษ จะเห็นเมนูมากกว่า Qc ปกติ 
                'If Request.Cookies("UserID").Value <> "684" And Request.Cookies("UserID").Value <> "3293" And Request.Cookies("UserID").Value = "141" Then 'ยกเว้นวัต
                If dt.Rows(0).Item("Tmp") <> Request.Cookies("UserID").Value Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('ไม่สามารถส่ง App ได้เนื่องจากเป็น App ของ QC คนอื่น !!');", True)
                    'str = "delete  from TblPrintCheckErrorQC"
                    'Command = New SqlCommand(str, Conn)
                    'Command.ExecuteNonQuery()
                    Exit Sub
                End If
            End If
        End If
        Conn.Close()



        If lblApp.Text = "" Then Exit Sub


        ' ส่ง case เข้า table เพื่อเป็น case แรก TSR
        'If wddMStatus.SelectedItemIndex = 3 Then
        '    SendCasePending(lblApp.Text.Trim, txtQCcomment.Text.Trim)
        'End If

        Conn.Open()

        Tran = Conn.BeginTransaction()

        Try
            str = "update tblapplication set statusqc = 2 where appid = '" & lblApp.Text & "'"
            Command = New SqlCommand(str, Conn)
            Command.Transaction = Tran
            Command.ExecuteNonQuery()

            str = "insert into tblqc(appid,statusqc,comment1,useridqc)values('" & lblApp.Text & "','2','" & txtQCcomment.Text & "','" & Request.Cookies("UserID").Value & "')"
            Command = New SqlCommand(str, Conn)
            Command.Transaction = Tran
            Command.ExecuteNonQuery()

            str = "insert into  tblcalllisttsr(idcar,appid,statusid,divisionid,createdate,createid,flag_list) values ((select idcar from tblapplication where appid = '" & lblApp.Text & "'),'" & lblApp.Text & "',(select curstatus from tblcar where idcar =(select idcar from tblapplication where appid = '" & lblApp.Text & "')),10,getdate(),'" & Request.Cookies("UserID").Value & "',0)"
            Command = New SqlCommand(str, Conn)
            Command.Transaction = Tran
            Command.ExecuteNonQuery()

            For i = 0 To UltraWebGrid2.Rows.Count - 1
                str = "insert into  tblQcDetail(appid,sqcid,createid,createdate) values ('" & lblApp.Text & "','" & UltraWebGrid2.Rows(i).Cells(1).Text & "','" & Request.Cookies("UserID").Value & "',getdate())"
                Command = New SqlCommand(str, Conn)
                Command.Transaction = Tran
                Command.ExecuteNonQuery()
            Next
            'แกไข วันที่นัด 
            If dt.Rows(0).Item("typetsr") = 11 Or dt.Rows(0).Item("typetsr") = 12 Or dt.Rows(0).Item("typetsr") = 15 Then
                str = "update tblcar set AppointDate = '" & SortDateProtect() & "' where idcar in (select idcar from tblapplication where appid = '" & lblApp.Text & "')"
                Command = New SqlCommand(str, Conn)
                Command.Transaction = Tran
                Command.ExecuteNonQuery()
            End If
            Tran.Commit()

            Conn.Close()

            ShowData(1)

            ClearData()
            UltraWebGrid2.DataSource = Nothing
            UltraWebGrid2.DataBind()
            UltraWebGrid2.Visible = False

            txtQCcomment.Text = ""

            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Message", "alert('ปล่อยเอกสารไม่ผ่านเรียบร้อย !!');", True)

        Catch ex As Exception
            Tran.Rollback()
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('พบปัญหา " & ex.Message & " กรุณาติดต่อ IT (โทร 811) !!');", True)

            Conn.Close()
        End Try



    End Sub
    Protected Function SortDateProtect() As String
        Dim AppointDateCV As String = ""
        AppointDateCV = ConvertDate.SetISODate("en", DateTime.Today.ToString("dd/MM/yyyy")) & " " & "09:" & "00"
        Return AppointDateCV
    End Function
    Private Sub SendCasePending(ByVal appx As String, ByVal comm As String)
        Dim str As String
        Dim Command As SqlCommand
        Dim Datareader As SqlDataReader
        Conn.Open()

        dt = New DataTable

        dt.Columns.Add("cusid")
        dt.Columns.Add("idcar")
        dt.Columns.Add("appid")
        dt.Columns.Add("createid")


        str = " SELECT TblCustomer.CusID, TblCar.IdCar, TblApplication.AppID, TblApplication.CreateID " + _
        " FROM TblApplication INNER JOIN " + _
                              " TblCar ON TblApplication.Idcar = TblCar.IdCar INNER JOIN " + _
                              "  TblCustomer ON TblCar.CusID = TblCustomer.CusID " + _
        " Where (TblApplication.AppID = '" & appx & "') "
        Command = New SqlCommand(str, Conn)
        Datareader = Command.ExecuteReader()
        If Datareader.HasRows Then
            While Datareader.Read
                Dim dtr As DataRow = dt.NewRow
                If IsDBNull(Datareader("cusid")) = False Then
                    dtr("cusid") = Datareader("cusid")
                End If
                If IsDBNull(Datareader("idcar")) = False Then
                    dtr("idcar") = Datareader("idcar")
                End If
                If IsDBNull(Datareader("appid")) = False Then
                    dtr("appid") = Datareader("appid")
                End If
                If IsDBNull(Datareader("createid")) = False Then
                    dtr("createid") = Datareader("createid")
                End If
                dt.Rows.Add(dtr)

            End While
        End If
        Datareader.Close()

        str = "insert into TblPendingCaseTSR (cusid, idcar, Appid, Comments, Divisionid, Userid, FlagPending, Cratedate, Createid) values " + _
        " ('" & dt.Rows(0).Item("cusid") & "','" & dt.Rows(0).Item("idcar") & "','" & dt.Rows(0).Item("appid") & "','" & comm & "',10,'" & dt.Rows(0).Item("createid") & "',0,getdate(),'" & Request.Cookies("UserID").Value & "')"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        Conn.Close()
    End Sub

    Private Sub QCPASS()
        AlertMsg()

        Dim tmpdate As String
        Dim str As String
        Dim Command As SqlCommand
        Dim DataReader As SqlDataReader

        dt = New DataTable

        dt.Columns.Add("Tmp")


        Conn.Open()

        str = " SELECT TblApplication.AppID  FROM  TblCar INNER JOIN TblApplication ON TblCar.IdCar = TblApplication.Idcar " + _
              " WHERE (TblApplication.Typeprovalue IN (2, 3, 4)) AND (TblCar.CurStatus = 3) and  appid = '" & lblApp.Text & "'"
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader
        If DataReader.HasRows Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('ชั้น 2+,3+,3 ปล่อยผ่านจาก Success ไม่ได้ !!');", True)
            Conn.Close()
            Exit Sub
        End If
        DataReader.Close()


        ' edit by na 23/12/2015 ถ้า ซื้อ พรบ อย่างเดียว ให้ เปลี่ยน เป็น wait ถ้าเป็น success
        Dim strQ As New System.Text.StringBuilder()
        strQ = New System.Text.StringBuilder()
        strQ.Append(" select tblapplication.appid,tblcar.CurStatus")
        strQ.Append(" from tblapplication ")
        strQ.Append(" inner join tblcar on tblcar.IdCar=tblapplication.idcar")
        strQ.Append(" where tblapplication.IsCarpet=1 and tblapplication.IsProvalue=0 and tblcar.CurStatus =3 and tblapplication.Appid =" & lblApp.Text)

        dt21 = New DataTable
        dt21 = DataAccess.DataRead(strQ.ToString)
        If dt21.Rows.Count > 0 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('กรณีที่ ซื้อพรบ.อย่างเดียว ปล่อยผ่านจาก Success ไม่ได้ ต้องเปลี่ยนสถานะเป็น Wait !!');", True)
            Conn.Close()
            Exit Sub
        End If
        'จบการแก้ไข  edit by na 

        tmpdate = Format$(Date.Now, "yyyy") - 543 & Format$(Date.Now, "/MM/dd")
        tmpdate = tmpdate + " " + Format(Date.Now, "HH:mm:ss")

        str = "select useridqc from tblapplication where appid = '" & lblApp.Text & "'"
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader
        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt.NewRow
                If IsDBNull(DataReader("useridqc")) = False Then
                    dtr("Tmp") = DataReader("useridqc")
                End If
                dt.Rows.Add(dtr)
            End While
        End If
        DataReader.Close()

        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("Tmp") = 0 Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('ไม่สามารถส่ง App ได้เนื่องจากยังไม่ผ่านการตรวจจาก QC !!');", True)
                Conn.Close()
                Exit Sub
            End If
        End If

        If dt.Rows.Count > 0 Then
            If Request.Cookies("userLevel").Value = "8" And Request.Cookies("TypeTsr").Value <> "0" Then '8 type 0 คือ Qcพิเศษ จะเห็นเมนูมากกว่า Qc ปกติ 
                'If Request.Cookies("UserID").Value <> "684" And Request.Cookies("UserID").Value <> "3293" And Request.Cookies("UserID").Value <> "141" Then 'ยกเว้นวัต
                If dt.Rows(0).Item("Tmp") <> Request.Cookies("UserID").Value Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('ไม่สามารถส่ง App ได้เนื่องจากเป็น App ของ QC คนอื่น !!');", True)
                    Conn.Close()
                    Exit Sub
                End If
            End If
        End If

        str = "select  dataid,curstatus,tblapplication.productid,DATEDIFF(d, getdate(),tblapplication.ProtectDate)  as d2,tblapplication.protectdate,typeprovalue  from tblapplication inner join tblcar on tblapplication.idcar = tblcar.idcar  where appid = '" & lblApp.Text & "'"
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader

        dt20 = New DataTable

        dt20.Columns.Add("curstatus")
        dt20.Columns.Add("typeprovalue")
        dt20.Columns.Add("productid")
        dt20.Columns.Add("protectdate")
        dt20.Columns.Add("d2")
        dt20.Columns.Add("dataid")
        'dt2.Columns.Add("isqcsuccessdate")

        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt20.NewRow
                If IsDBNull(DataReader("curstatus")) = False Then
                    dtr("curstatus") = DataReader("curstatus")
                End If
                If IsDBNull(DataReader("typeprovalue")) = False Then
                    dtr("typeprovalue") = DataReader("typeprovalue")
                End If
                If IsDBNull(DataReader("productid")) = False Then
                    dtr("productid") = DataReader("productid")
                End If
                If IsDBNull(DataReader("protectdate")) = False Then
                    dtr("protectdate") = Format(DataReader("protectdate"), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader("d2")) = False Then
                    dtr("d2") = DataReader("d2")
                End If
                If IsDBNull(DataReader("dataid")) = False Then
                    dtr("dataid") = DataReader("dataid")
                End If
                'If IsDBNull(DataReader("isqcsuccessdate")) = False Then
                '    dtr("isqcsuccessdate") = DataReader("isqcsuccessdate")
                'End If
                dt20.Rows.Add(dtr)
            End While
        End If
        DataReader.Close()
        Conn.Close()

        If dt20.Rows(0).Item("d2") < 0 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('ไม่สามารถส่ง App ได้ เนื่องจากวันคุ้มครองผิด !!');", True)
            Exit Sub
        End If

        If dt20.Rows(0).Item("curstatus") = 3 Then            '' status : success           
            If dt20.Rows(0).Item("typeprovalue") = 1 Then       ' ตรวจสอบว่าเป็น ประกันชั้น 1
                If dt20.Rows(0).Item("d2") >= 0 Then 'วันสมัครห่างจากวัรคุ้มครอง ต้องมีค่าอย่างน้อย 0 วัน ทุกบริษัท
                    ' บริษัทอื่นๆนอกจาก วิริยะ เทเวศน์ เอเชีย ไม่สนใจวันคุ้มครอง

                    'msig >45 ให้แจ้งเตือน
                    If ((dt20.Rows(0).Item("productid") = 54)) Then
                        If dt20.Rows(0).Item("d2") > 45 Then
                            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('เอ็ม.เอส.ไอ.จีประกันภัย วันคุ้มครองเกิน 45 วัน ต้องปล่อยเป็น WAIT เท่านั้น !!');", True)
                            Exit Sub
                        End If
                    End If


                    If dt20.Rows(0).Item("d2") > 30 Then 'วิริยะ (9) วีนนสมัครห่างจากวัรคุ้มครอง เกิน 30 วัน (VI)
                        If dt20.Rows(0).Item("productid") = 9 Then
                            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('วิริยะประกันภัย วันคุ้มครองเกิน 30 วัน ต้องปล่อยเป็น WAIT เท่านั้น !!');", True)
                            Exit Sub
                        End If

                        'If dt20.Rows(0).Item("productid") = 8 Then 'เทเวศน์ เกิน 30 วัน (DEV)
                        '    ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('เทเวศประกันภัย วันคุ้มครองเกิน 30 วัน ต้องปล่อยเป็น WAIT เท่านั้น !!');", True)
                        '    Exit Sub
                        'End If
                    End If
                    'แก้ไข
                    If dt20.Rows(0).Item("d2") > 60 And Request.Cookies("TypeTsr").Value <> 3 Then 'เทเวศน์ เกิน 60 วัน (DEV)                       

                        If dt20.Rows(0).Item("productid") = 8 Then 'เทเวศน์ เกิน 60 วัน (DEV)
                            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('เทเวศประกันภัย วันคุ้มครองเกิน 60 วัน ต้องปล่อยเป็น WAIT เท่านั้น !!');", True)
                            Exit Sub
                        End If
                    End If

                    If dt20.Rows(0).Item("d2") > 45 And Request.Cookies("TypeTsr").Value = 3 Then 'เทเวศน์ เกิน 45 วัน (DEV)                       

                        If dt20.Rows(0).Item("productid") = 8 Then 'เทเวศน์ เกิน 45 วัน (DEV)
                            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('เทเวศประกันภัย วันคุ้มครองเกิน 45 วัน ต้องปล่อยเป็น WAIT เท่านั้น !!');", True)
                            Exit Sub
                        End If
                    End If
                    'จบแก้ไข

                    If dt20.Rows(0).Item("d2") > 60 Then 'เอเชีย วันสมัครห่างจากวันคุ้มครอง เกิน 60 วัน (AI)(id 15) 
                        If dt20.Rows(0).Item("productid") = 15 Then
                            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('เอเชียประกันภัย วันคุ้มครองเกิน 60 วัน ต้องปล่อยเป็น WAIT เท่านั้น !!');", True)
                            Exit Sub
                        End If

                        If dt20.Rows(0).Item("productid") = 24 Then
                            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('ประกันคุ้มภัย วันคุ้มครองเกิน 60 วัน ต้องปล่อยเป็น WAIT เท่านั้น !!');", True)
                            Exit Sub
                        End If
                    End If
                End If
            End If
        End If



        'Update_Payment(lblCarid.Text, lblCusID.Text) ' โอนเงิน

        Conn.Open()

        ''''''''''''''''''''''' ส่งไปตัดบัตร
        str = "  SELECT TblAppPay.AppID, TblAppCard.Bankid, TblAppCard.CardNo1, TblAppCard.CardNo2, TblAppCard.CardExp, TblAppPay.TotalPay, TblAppPay.PayID,tblapplication.idcar " + _
                  " FROM TblAppPay INNER JOIN TblAppCard ON TblAppPay.AppID = TblAppCard.Appid   inner join tblapplication on tblapppay.appid = tblapplication.appid" + _
                  " Where (TblAppPay.Typepay = 2) And (tblapppay.ispaid = 0) and (TblAppPay.PayID = 1) and len(tblappcard.cardno1) = 16 and len(tblappcard.cardexp) = 4  and tblappcard.cardrun = 1 and TblAppPay.appid = '" & lblApp.Text & "' "
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader

        dt = New DataTable

        dt.Columns.Add("idcar")
        dt.Columns.Add("bankid")
        dt.Columns.Add("CardNo1")
        dt.Columns.Add("CardNo2")
        dt.Columns.Add("CardExp")
        dt.Columns.Add("TotalPay")
        dt.Columns.Add("PayID")

        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt.NewRow
                If IsDBNull(DataReader("idcar")) = False Then
                    dtr("idcar") = DataReader("idcar")
                End If
                If IsDBNull(DataReader("bankid")) = False Then
                    dtr("bankid") = DataReader("bankid")
                End If
                If IsDBNull(DataReader("CardNo1")) = False Then
                    dtr("CardNo1") = DataReader("CardNo1")
                End If
                If IsDBNull(DataReader("CardNo2")) = False Then
                    dtr("CardNo2") = DataReader("CardNo2")
                End If
                If IsDBNull(DataReader("CardExp")) = False Then
                    dtr("CardExp") = DataReader("CardExp")
                End If
                If IsDBNull(DataReader("TotalPay")) = False Then
                    dtr("TotalPay") = DataReader("TotalPay")
                End If
                If IsDBNull(DataReader("PayID")) = False Then
                    dtr("PayID") = DataReader("PayID")
                End If
                dt.Rows.Add(dtr)
            End While
        End If
        DataReader.Close()


        ''''''''''''''''''

        str = "select  dataid,curstatus,tblapplication.productid,DATEDIFF(d, getdate(),tblapplication.ProtectDate) as d2,tblapplication.protectdate,typeprovalue  from tblapplication inner join tblcar on tblapplication.idcar = tblcar.idcar  where appid = '" & lblApp.Text & "'"
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader

        dt2 = New DataTable

        dt2.Columns.Add("curstatus")
        dt2.Columns.Add("typeprovalue")
        dt2.Columns.Add("productid")
        dt2.Columns.Add("protectdate")
        dt2.Columns.Add("d2")
        dt2.Columns.Add("dataid")
        'dt2.Columns.Add("isqcsuccessdate")

        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt2.NewRow
                If IsDBNull(DataReader("curstatus")) = False Then
                    dtr("curstatus") = DataReader("curstatus")
                End If
                If IsDBNull(DataReader("typeprovalue")) = False Then
                    dtr("typeprovalue") = DataReader("typeprovalue")
                End If
                If IsDBNull(DataReader("productid")) = False Then
                    dtr("productid") = DataReader("productid")
                End If
                If IsDBNull(DataReader("protectdate")) = False Then
                    dtr("protectdate") = Format(DataReader("protectdate"), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader("d2")) = False Then
                    dtr("d2") = DataReader("d2")
                End If
                If IsDBNull(DataReader("dataid")) = False Then
                    dtr("dataid") = DataReader("dataid")
                End If
                'If IsDBNull(DataReader("isqcsuccessdate")) = False Then
                '    dtr("isqcsuccessdate") = DataReader("isqcsuccessdate")
                'End If
                dt2.Rows.Add(dtr)
            End While
        End If
        DataReader.Close()

        'Update AppNo กรณีเป็นบริษัท TPB เพื่อลงเลขรับแจ้ง
        If dt2.Rows(0).Item("productid") = 71 Then
            str = "update tblapplication set  AppNo = '" & lblApp.Text & "'where appid = '" & lblApp.Text & "'"
            Command = New SqlCommand(str, Conn)
            Command.ExecuteNonQuery()
        End If


        '' check cust status : success / wait
        If dt2.Rows(0).Item("curstatus") = 3 Then            '' status : success
            'ถ่ายรูปรถ 20080702
            'Call TakePhoto(carIDx, cusIDx)
            '--------------------------------------------
            If dt2.Rows(0).Item("typeprovalue") = 1 Then       ' ตรวจสอบว่าเป็น ประกันชั้น 1

                If dt2.Rows(0).Item("d2") >= 0 Then 'วันสมัครห่างจากวัรคุ้มครอง ต้องมีค่าอย่างน้อย 0 วัน ทุกบริษัท
                    ' บริษัทอื่นๆนอกจาก วิริยะ เทเวศน์ เอเชีย ไม่สนใจวันคุ้มครอง
                    If dt2.Rows(0).Item("productid") <> 9 And dt2.Rows(0).Item("productid") <> 8 And dt2.Rows(0).Item("productid") <> 15 And dt2.Rows(0).Item("productid") <> 54 Then
                        'If test <> 9 And test <> 8 And test <> 15 Then
                        str = "update tblapplication set lockapp = 1,isqcsuccessdate = 1,qcsuccessdate  = getdate() ,isqcsenddate = 1,qcsenddate  = getdate(),submitdate = getdate(),updatedate= getdate(),updateid= '" & Request.Cookies("UserID").Value & "', statusqc = 7 where appid = '" & lblApp.Text & "'"
                        Command = New SqlCommand(str, Conn)
                        Command.ExecuteNonQuery()

                        'loopSave = 21
                        str = "insert into TblLogQcSubmit (Appid,Createdate,createid,protectdate,diffdate,productid,submitby,qcstatus,loopsave) values( '" & lblApp.Text & "',getdate(),'" & Request.Cookies("UserID").Value & "','" & ConvertDate.SetISODate("en", dt2.Rows(0).Item("protectdate")) & "','" & dt2.Rows(0).Item("d2") & "','" & dt2.Rows(0).Item("productid") & "',2,7,21)"
                        Command = New SqlCommand(str, Conn)
                        Command.ExecuteNonQuery()

                        GoTo LineQc1
                    End If

                    If dt2.Rows(0).Item("d2") <= 30 Then 'วิริยะ วันสมัครห่างจากวัรคุ้มครอง ไม่เกิน 30 วัน (VI,DE)

                        If (dt2.Rows(0).Item("productid") = 9) Then
                            str = "update tblapplication set lockapp = 1,isqcsuccessdate = 1,qcsuccessdate  = getdate() ,isqcsenddate = 1,qcsenddate  = getdate(),submitdate = getdate(),updatedate= getdate(),updateid= '" & Request.Cookies("UserID").Value & "', statusqc = 7 where appid = '" & lblApp.Text & "'"
                            Command = New SqlCommand(str, Conn)
                            Command.ExecuteNonQuery()

                            'loopsave = 22
                            str = "insert into TblLogQcSubmit (Appid,Createdate,createid,protectdate,diffdate,productid,submitby,qcstatus,loopsave) values( '" & lblApp.Text & "',getdate(),'" & Request.Cookies("UserID").Value & "','" & ConvertDate.SetISODate("en", dt2.Rows(0).Item("protectdate")) & "','" & dt2.Rows(0).Item("d2") & "','" & dt2.Rows(0).Item("productid") & "',2,7,22)"
                            Command = New SqlCommand(str, Conn)
                            Command.ExecuteNonQuery()

                            GoTo LineQc1
                        End If
                    End If

                    'แก้ไข
                    If dt2.Rows(0).Item("d2") <= 60 And Request.Cookies("TypeTsr").Value <> 3 Then 'เทเวศน์ วันสมัครห่างจากวัรคุ้มครอง ไม่เกิน 60 วัน (VI,DE)

                        If (dt2.Rows(0).Item("productid") = 8) Then
                            str = "update tblapplication set lockapp = 1,isqcsuccessdate = 1,qcsuccessdate  = getdate() ,isqcsenddate = 1,qcsenddate  = getdate(),submitdate = getdate(),updatedate= getdate(),updateid= '" & Request.Cookies("UserID").Value & "', statusqc = 7 where appid = '" & lblApp.Text & "'"
                            Command = New SqlCommand(str, Conn)
                            Command.ExecuteNonQuery()

                            'loopsave = 22
                            str = "insert into TblLogQcSubmit (Appid,Createdate,createid,protectdate,diffdate,productid,submitby,qcstatus,loopsave) values( '" & lblApp.Text & "',getdate(),'" & Request.Cookies("UserID").Value & "','" & ConvertDate.SetISODate("en", dt2.Rows(0).Item("protectdate")) & "','" & dt2.Rows(0).Item("d2") & "','" & dt2.Rows(0).Item("productid") & "',2,7,22)"
                            Command = New SqlCommand(str, Conn)
                            Command.ExecuteNonQuery()

                            GoTo LineQc1
                        End If
                    End If
                    If dt2.Rows(0).Item("d2") <= 45 And Request.Cookies("TypeTsr").Value = 3 Then 'เทเวศน์ วันสมัครห่างจากวัรคุ้มครอง ไม่เกิน 45 วัน (VI,DE)

                        If (dt2.Rows(0).Item("productid") = 8) Then
                            str = "update tblapplication set lockapp = 1,isqcsuccessdate = 1,qcsuccessdate  = getdate() ,isqcsenddate = 1,qcsenddate  = getdate(),submitdate = getdate(),updatedate= getdate(),updateid= '" & Request.Cookies("UserID").Value & "', statusqc = 7 where appid = '" & lblApp.Text & "'"
                            Command = New SqlCommand(str, Conn)
                            Command.ExecuteNonQuery()

                            'loopsave = 22
                            str = "insert into TblLogQcSubmit (Appid,Createdate,createid,protectdate,diffdate,productid,submitby,qcstatus,loopsave) values( '" & lblApp.Text & "',getdate(),'" & Request.Cookies("UserID").Value & "','" & ConvertDate.SetISODate("en", dt2.Rows(0).Item("protectdate")) & "','" & dt2.Rows(0).Item("d2") & "','" & dt2.Rows(0).Item("productid") & "',2,7,22)"
                            Command = New SqlCommand(str, Conn)
                            Command.ExecuteNonQuery()

                            GoTo LineQc1
                        End If
                    End If
                    'จบแก้ไข

                    'msig <= 45 ให้ AutoSubmit
                    If ((dt2.Rows(0).Item("productid") = 54)) Then
                        If dt2.Rows(0).Item("d2") > 45 Then
                            str = "update tblapplication set statusqc =  4,lockapp = 1,isqcsuccessdate = 1,qcsuccessdate  = getdate() ,isqcsenddate = 1,qcsenddate  = getdate()  where appid = '" & lblApp.Text & "'"
                            Command = New SqlCommand(str, Conn)
                            Command.ExecuteNonQuery()

                        Else
                            str = "update tblapplication set  lockapp = 1,isqcsuccessdate = 1,qcsuccessdate  = getdate() ,isqcsenddate = 1,qcsenddate  = getdate(),submitdate = getdate(),updatedate= getdate(),updateid= '" & Request.Cookies("UserID").Value & "', statusqc = 7 where appid = '" & lblApp.Text & "'"
                            Command = New SqlCommand(str, Conn)
                            Command.ExecuteNonQuery()

                            'loopsave = 23
                            str = "insert into TblLogQcSubmit (Appid,Createdate,createid,protectdate,diffdate,productid,submitby,qcstatus,loopsave) values( '" & lblApp.Text & "',getdate(),'" & Request.Cookies("UserID").Value & "','" & ConvertDate.SetISODate("en", dt2.Rows(0).Item("protectdate")) & "','" & dt2.Rows(0).Item("d2") & "','" & dt2.Rows(0).Item("productid") & "',2,7,23)"
                            Command = New SqlCommand(str, Conn)
                            Command.ExecuteNonQuery()

                        End If
                        GoTo LineQc1
                    End If


                    If dt2.Rows(0).Item("d2") <= 60 Then 'เอเชียประกันภัย วันสมัครห่างจากวัรคุ้มครอง ไม่เกิน 60 วัน (AI)
                        If (dt2.Rows(0).Item("productid") = 15) Then
                            str = "update tblapplication set lockapp = 1,isqcsuccessdate = 1,qcsuccessdate  = getdate() ,isqcsenddate = 1,qcsenddate  = getdate(),submitdate = getdate(),updatedate= getdate(),updateid= '" & Request.Cookies("UserID").Value & "', statusqc = 7 where appid = '" & lblApp.Text & "'"
                            Command = New SqlCommand(str, Conn)
                            Command.ExecuteNonQuery()

                            'loopSave = 24
                            str = "insert into TblLogQcSubmit (Appid,Createdate,createid,protectdate,diffdate,productid,submitby,qcstatus,loopsave) values( '" & lblApp.Text & "',getdate(),'" & Request.Cookies("UserID").Value & "','" & ConvertDate.SetISODate("en", dt2.Rows(0).Item("protectdate")) & "','" & dt2.Rows(0).Item("d2") & "','" & dt2.Rows(0).Item("productid") & "',2,7,24)"
                            Command = New SqlCommand(str, Conn)
                            Command.ExecuteNonQuery()

                            GoTo LineQc1
                        End If

                        If (dt2.Rows(0).Item("productid") = 24) Then 'ประกันคุ้มภัย วันสมัครห่างจากวัรคุ้มครอง ไม่เกิน 60 วัน edit::26/03/2556
                            str = "update tblapplication set lockapp = 1,isqcsuccessdate = 1,qcsuccessdate  = getdate() ,isqcsenddate = 1,qcsenddate  = getdate(),submitdate = getdate(),updatedate= getdate(),updateid= '" & Request.Cookies("UserID").Value & "', statusqc = 7 where appid = '" & lblApp.Text & "'"
                            Command = New SqlCommand(str, Conn)
                            Command.ExecuteNonQuery()

                            'loopSave = 241
                            str = "insert into TblLogQcSubmit (Appid,Createdate,createid,protectdate,diffdate,productid,submitby,qcstatus,loopsave) values( '" & lblApp.Text & "',getdate(),'" & Request.Cookies("UserID").Value & "','" & ConvertDate.SetISODate("en", dt2.Rows(0).Item("protectdate")) & "','" & dt2.Rows(0).Item("d2") & "','" & dt2.Rows(0).Item("productid") & "',2,7,241)"
                            Command = New SqlCommand(str, Conn)
                            Command.ExecuteNonQuery()

                            GoTo LineQc1
                        End If
                    End If

                    If dt2.Rows(0).Item("d2") > 30 Then 'วิริยะ (9)วันสมัครห่างจากวัรคุ้มครอง เกิน 30 วัน (VI,DE)
                        If dt2.Rows(0).Item("productid") = 9 Then
                            str = "update tblapplication set lockapp = 1,isqcsuccessdate = 1,qcsuccessdate  = getdate() ,isqcsenddate = 1,qcsenddate  = getdate(),updatedate= getdate(),updateid= '" & Request.Cookies("UserID").Value & "', statusqc = 4 where appid = '" & lblApp.Text & "'"
                            Command = New SqlCommand(str, Conn)
                            Command.ExecuteNonQuery()
                            GoTo LineQc1
                        End If
                    End If
                    'แก้ไข
                    If dt2.Rows(0).Item("d2") > 60 And Request.Cookies("TypeTsr").Value <> 3 Then 'เทเวศน์ (8) วันสมัครห่างจากวัรคุ้มครอง เกิน 60 วัน (VI,DE)
                        If dt2.Rows(0).Item("productid") = 8 Then
                            str = "update tblapplication set lockapp = 1,isqcsuccessdate = 1,qcsuccessdate  = getdate() ,isqcsenddate = 1,qcsenddate  = getdate(),updatedate= getdate(),updateid= '" & Request.Cookies("UserID").Value & "', statusqc = 4 where appid = '" & lblApp.Text & "'"
                            Command = New SqlCommand(str, Conn)
                            Command.ExecuteNonQuery()
                            GoTo LineQc1
                        End If
                    End If
                    If dt2.Rows(0).Item("d2") > 45 And Request.Cookies("TypeTsr").Value = 3 Then 'เทเวศน์ (8) วันสมัครห่างจากวัรคุ้มครอง เกิน 45 วัน (VI,DE)
                        If dt2.Rows(0).Item("productid") = 8 Then
                            str = "update tblapplication set lockapp = 1,isqcsuccessdate = 1,qcsuccessdate  = getdate() ,isqcsenddate = 1,qcsenddate  = getdate(),updatedate= getdate(),updateid= '" & Request.Cookies("UserID").Value & "', statusqc = 4 where appid = '" & lblApp.Text & "'"
                            Command = New SqlCommand(str, Conn)
                            Command.ExecuteNonQuery()
                            GoTo LineQc1
                        End If
                    End If
                    'จบ


                    If dt2.Rows(0).Item("d2") > 60 Then 'เอเชีย วันสมัครห่างจากวันคุ้มครอง เกิน 60 วัน (AI)(id 15) 

                        If dt2.Rows(0).Item("productid") = 15 Then
                            str = "update tblapplication set lockapp = 1,isqcsuccessdate = 1,qcsuccessdate  = getdate() ,isqcsenddate = 1,qcsenddate  = getdate(),updatedate= getdate(),updateid= '" & Request.Cookies("UserID").Value & "', statusqc = 4 where appid = '" & lblApp.Text & "'"
                            Command = New SqlCommand(str, Conn)
                            Command.ExecuteNonQuery()
                            GoTo LineQc1
                        End If
                    End If
                End If



            End If

            If dt2.Rows(0).Item("typeprovalue") <> 1 Then       'ชั้น 3/5
                str = "update tblapplication set  lockapp = 1,isqcsuccessdate = 1,qcsuccessdate  = getdate() ,isqcsenddate = 1,qcsenddate  = getdate(),submitdate = getdate(),updatedate= getdate(),updateid= '" & Request.Cookies("UserID").Value & "', statusqc = 7 where appid = '" & lblApp.Text & "'"
                Command = New SqlCommand(str, Conn)
                Command.ExecuteNonQuery()

                'loopSave = 25
                str = "insert into TblLogQcSubmit (Appid,Createdate,createid,protectdate,diffdate,productid,submitby,qcstatus,loopsave) values( '" & lblApp.Text & "',getdate(),'" & Request.Cookies("UserID").Value & "','" & ConvertDate.SetISODate("en", dt2.Rows(0).Item("protectdate")) & "','" & dt2.Rows(0).Item("d2") & "','" & dt2.Rows(0).Item("productid") & "',2,7,25)"
                Command = New SqlCommand(str, Conn)
                Command.ExecuteNonQuery()
            End If



LineQc1:
            If Trim(txtQCcomment.Text) = "" Then
                str = "insert into tblqc(appid,statusqc,comment1,useridqc)values('" & lblApp.Text & "','1','Qc ผ่าน','" & Request.Cookies("UserID").Value & "')"
            End If
            If Trim(txtQCcomment.Text) <> "" Then
                str = "insert into tblqc(appid,statusqc,comment1,useridqc)values('" & lblApp.Text & "','1','" & txtQCcomment.Text & "','" & Request.Cookies("UserID").Value & "')"
            End If
            Command = New SqlCommand(str, Conn)
            Command.ExecuteNonQuery()

        ElseIf dt2.Rows(0).Item("curstatus") = 4 Then 'status : wait


            str = "update tblapplication set statusqc =  1,lockapp = 1,isqcsuccessdate = 1,qcsuccessdate  = getdate(),isqcsenddate = 1,qcsenddate  = getdate() ,updatedate= getdate(),updateid= '" & Request.Cookies("UserID").Value & "' where appid = '" & lblApp.Text & "'"
            Command = New SqlCommand(str, Conn)
            Command.ExecuteNonQuery()

            '   cn.Execute "update tblcar set lockapp = 1 where idcar = '" & lcaridx & "' "


            If Trim(txtQCcomment.Text) = "" Then
                str = "insert into tblqc(appid,statusqc,comment1,useridqc)values('" & lblApp.Text & "','1','Qc ผ่าน','" & Request.Cookies("UserID").Value & "')"
            End If
            If Trim(txtQCcomment.Text) <> "" Then
                str = "insert into tblqc(appid,statusqc,comment1,useridqc)values('" & lblApp.Text & "','1','" & txtQCcomment.Text & "','" & Request.Cookies("UserID").Value & "')"
            End If
            Command = New SqlCommand(str, Conn)
            Command.ExecuteNonQuery()


        End If

        If dt2.Rows(0).Item("dataid") = 6 Then   ' point  insure ถ่ายรูป
            TakePhoto(lblApp.Text)
        End If
        If Conn.State = ConnectionState.Closed Then
            Conn.Open()
        End If
        str = " SELECT TblApplication.AppID, TblApplication.statusqc,TblUser.TypeTsr,TblApplication.flagsend FROM TblApplication INNER JOIN TblUser ON TblApplication.CreateID = TblUser.UserID Where (TblApplication.appid = '" & lblApp.Text & "') "
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader

        dt3 = New DataTable

        dt3.Columns.Add("typetsr")
        dt3.Columns.Add("statusqc")
        dt3.Columns.Add("flagsend")

        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt3.NewRow
                If IsDBNull(DataReader("typetsr")) = False Then
                    dtr("typetsr") = DataReader("typetsr")
                End If
                If IsDBNull(DataReader("statusqc")) = False Then
                    dtr("statusqc") = DataReader("statusqc")
                End If
                If IsDBNull(DataReader("flagsend")) = False Then
                    dtr("flagsend") = DataReader("flagsend")
                End If

                dt3.Rows.Add(dtr)
            End While
        End If
        DataReader.Close()

        str = "select count(*) from tblcardapprove where appid = '" & lblApp.Text & "' and comment = 'QC' and appvSTATUS = 2"
        Command = New SqlCommand(str, Conn)
        Dim CheckSum As Integer = Command.ExecuteScalar()

        If dt.Rows.Count <> 0 And CheckSum <= 0 And dt3.Rows(0).Item("statusqc") <> 0 And dt3.Rows(0).Item("statusqc") <> 9 And dt3.Rows(0).Item("statusqc") <> 2 And dt3.Rows(0).Item("statusqc") <> 3 And dt3.Rows(0).Item("statusqc") <> 10 Then
            str = "insert into tblcardapprove (CusID, idCAR, appID, bankID, cardNO1, cardNO2, cardEXP, payVALUE, payNO, appvSTATUS, appvCODE,createID, createDATE,comment) " + _
            " values ('" & lblCusID.Text & "','" & dt.Rows(0).Item("idcar") & "','" & lblApp.Text & "','" & dt.Rows(0).Item("bankid") & "','" & dt.Rows(0).Item("CardNo1") & "','" & dt.Rows(0).Item("CardNo2") & "', " + _
            "'" & dt.Rows(0).Item("CardExp") & "'," & dt.Rows(0).Item("TotalPay") & ",'" & dt.Rows(0).Item("PayID") & "',1,'WA','" & Request.Cookies("UserID").Value & "',getdate(),'QC')"
            Command = New SqlCommand(str, Conn)
            Command.ExecuteNonQuery()
        End If
        'ส่งถ่ายรูป

        If chkPhoto.Checked Then

            str = "   select tblapplication.AppID,tblapplication.cusid,tblapplication.Idcar from tblapplication inner join tblcar on tblapplication.idcar=tblcar.idcar where tblapplication.AppID='" & lblApp.Text & "' "
            Command = New SqlCommand(str, Conn)
            DataReader = Command.ExecuteReader

            dt = New DataTable

            dt.Columns.Add("AppID")
            dt.Columns.Add("cusid")
            dt.Columns.Add("Idcar")


            If DataReader.HasRows Then
                While DataReader.Read
                    Dim dtr As DataRow = dt.NewRow
                    If IsDBNull(DataReader("AppID")) = False Then
                        dtr("AppID") = DataReader("AppID")
                    End If
                    If IsDBNull(DataReader("cusid")) = False Then
                        dtr("cusid") = DataReader("cusid")
                    End If
                    If IsDBNull(DataReader("Idcar")) = False Then
                        dtr("Idcar") = DataReader("Idcar")
                    End If
                    dt.Rows.Add(dtr)

                End While
            End If
            DataReader.Close()
            'str = "insert into tblPhotoCase (CusID, idCAR, appID, createID) " + _
            '                  " values ('" & lblCusID.Text & "','" & dt.Rows(0).Item("idcar") & "','" & lblApp.Text & "','" & Request.Cookies("UserID").Value & "')"
            'Command = New SqlCommand(str, Conn)
            'Command.ExecuteNonQuery()

            'str = "update tblapplication set takephoto = 1 where  appid = '" & lblApp.Text & "' "
            'Command = New SqlCommand(str, Conn)
            'Command.ExecuteNonQuery()

        End If

        'Add By Na เรื่อง การคิดค่าคอม เนื่องจาก มีการย้ายยอดเงิน

        Dim CommandstrQ1 As SqlCommand
        Dim strQ1 As String = ""
        Dim dtstrQ1 As DataTable

        strQ1 += " select Appid,Idcar,CarPet from tblapplication "
        strQ1 += " where IsCarpet = 1 And appstatus = 1 And IsProvalue = 0 "
        strQ1 += " and appid in(select appid from tblpayment )"
        strQ1 += " and idcar= (select idcar from tblapplication where appid='" & lblApp.Text & "')"
        strQ1 += " and appid<>'" & lblApp.Text & "' "

        CommandstrQ1 = New SqlCommand(strQ1, Conn)
        DataReader = CommandstrQ1.ExecuteReader

        dtstrQ1 = New DataTable
        dtstrQ1.Columns.Add("AppID")
        dtstrQ1.Columns.Add("Idcar")
        dtstrQ1.Columns.Add("CarPet")

        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dtstrQ1.NewRow
                If IsDBNull(DataReader("AppID")) = False Then
                    dtr("AppID") = DataReader("AppID")
                End If
                If IsDBNull(DataReader("Idcar")) = False Then
                    dtr("Idcar") = DataReader("Idcar")
                End If
                If IsDBNull(DataReader("CarPet")) = False Then
                    dtr("CarPet") = DataReader("CarPet")
                End If
                dtstrQ1.Rows.Add(dtr)
            End While
        End If
        DataReader.Close()
        If dtstrQ1.Rows.Count > 0 Then
            'ADD IN TBL
            str = "INSERT INTO TblChangeAppPay(AppID,PayNo,PayOld,PayNew)  VALUES('" & dtstrQ1.Rows(0).Item("AppID") & "',1,(select TotalPay from tblapppay where appid=" & dtstrQ1.Rows(0).Item("AppID") & " and Payid=1)," & dtstrQ1.Rows(0).Item("CarPet") & ") "
            Command = New SqlCommand(str, Conn)
            Command.ExecuteNonQuery()
            'Update TblAppPay
            'str = "update tblapppay set TotalPay = " & dtstrQ1.Rows(0).Item("CarPet") & " where appid='" & dtstrQ1.Rows(0).Item("AppID") & "'  and Payid=1"

            'Command = New SqlCommand(str, Conn)
            'Command.ExecuteNonQuery()
        End If
        'End




        Conn.Close()
        sendSms(lblApp.Text)
        AutoSignSuccessDoc()


        If dt3.Rows(0).Item("statusqc") <> 0 And dt3.Rows(0).Item("statusqc") <> 9 And dt3.Rows(0).Item("statusqc") <> 2 And dt3.Rows(0).Item("statusqc") <> 3 And dt3.Rows(0).Item("statusqc") <> 10 Then
            If dt3.Rows(0).Item("typetsr") = 100 Or dt3.Rows(0).Item("typetsr") = 200 Or dt3.Rows(0).Item("typetsr") = 201 Then

            Else
                '0=ไม่ส่งเอกสาร ,1=ส่งเอกสารปกติ
                If dt3.Rows(0).Item("flagsend") = 1 Then
                    Print_CV_Payment()
                End If


                Auto_File()
                ShowData(1)

                ClearData()
            End If
        End If


        'cn.Execute "delete  from TblPrintCheckErrorQC "
        'clear

    End Sub


    'Private Sub Update_Payment(ByVal Tcarid As String, ByVal Tcusid As String) ' âÍ¹à§Ô¹

    '    Dim AppID1, Appid2, PID As String
    '    Dim payvalueX, PaybyX, PfromX As Integer
    '    ''''''''''''''
    '    '''''''''''''''
    '    Dim str As String
    '    Dim Command As SqlCommand
    '    Dim DataReader As SqlDataReader

    '    Dim Count As Integer = 0
    '    Dim Count2 As Integer = 0

    '    Conn.Open()

    '    dt = New DataTable
    '    dt.Columns.Add("AppID")

    '    'หา 2 app
    '    str = "select  * from tblapplication where appstatus = 1  and AppComplete=0 and idcar = '" & Tcarid & "' "
    '    Command = New SqlCommand(str, Conn)
    '    DataReader = Command.ExecuteReader
    '    If DataReader.HasRows Then
    '        While DataReader.Read
    '            Count += 1
    '        End While
    '    End If
    '    DataReader.Close()

    '    If Count = 1 Then    '*************************Case 1 ยกเลิกทั้งหมดทำ APP ใหม่*****************************
    '        str = "select  top 2 * from tblapplication where  idcar = '" & Tcarid & "' order by appid desc"
    '        Command = New SqlCommand(str, Conn)
    '        DataReader = Command.ExecuteReader
    '        If DataReader.HasRows Then
    '            While DataReader.Read
    '                Dim dtr As DataRow = dt.NewRow
    '                If IsDBNull(DataReader("appid")) = False Then
    '                    dtr("AppID") = DataReader("appid")
    '                End If
    '                dt.Rows.Add(dtr)
    '                Count2 += 1
    '            End While
    '        End If
    '        DataReader.Close()

    '        'หา 2 app ล่าสุดที่ทำ
    '        dt2 = New DataTable
    '        dt2.Columns.Add("successdate")
    '        dt2.Columns.Add("appid")
    '        If Count2 = 2 Then 'มี 2 app
    '            'หาเลข App ก่อนหน้า (App เก่า) ด้านล่าง (index = 1)
    '            str = "select appid,successdate from tblapplication where  appid = '" & dt.Rows(1).Item("AppID") & "'  and appid in(select distinct appid from tblpayment)  "
    '            Command = New SqlCommand(str, Conn)
    '            DataReader = Command.ExecuteReader
    '            If DataReader.HasRows Then
    '                While DataReader.Read
    '                    Dim dtr As DataRow = dt2.NewRow
    '                    If IsDBNull(DataReader("successdate")) = False Then
    '                        dtr("successdate") = Format(DataReader("successdate"), "dd/MM/yyyy")
    '                    End If
    '                    If IsDBNull(DataReader("appid")) = False Then
    '                        dtr("appid") = DataReader("appid")
    '                    End If
    '                    dt2.Rows.Add(dtr)
    '                End While
    '            Else
    '                DataReader.Close()
    '                Conn.Close()
    '                Exit Sub
    '            End If
    '            DataReader.Close()

    '            'กลับไปหา App แรก (App ใหม่) ด้านบน (index = 0)
    '            str = "select * from tblapplication where appid = '" & dt2.Rows(0).Item("AppID") & "' and appstatus = 1 and " + _
    '            " datediff(day,'" & ConvertDate.SetISODate("en", dt2.Rows(0).Item("successdate")) & "',successdate) < = 30 "
    '            Command = New SqlCommand(str, Conn)
    '            DataReader = Command.ExecuteReader
    '            dt3 = New DataTable
    '            dt3.Columns.Add("appid")
    '            If DataReader.HasRows Then
    '	While DataReader.Read
    '		Dim dtr As DataRow = dt3.NewRow
    '		If IsDBNull(DataReader("appid")) = False Then
    '			dtr("appid") = DataReader("appid")
    '		End If
    '		dt3.Rows.Add(dtr)
    '	End While
    '            End If
    '            DataReader.Close()

    '            'app เก่ายกเลิก Appใหม่ถูก Success ภายใน 30 วัน เทียบกับวัน Success ของ App เก่า
    '            If dt3.Rows.Count = 1 Then
    '                str = "update tblpayment set appid = '" & dt3.Rows(0).Item("appid") & "', comment = 'โอนเงินมาจาก App ' + '" & dt2.Rows(0).Item("appid") & "'  where appid = '" & dt2.Rows(0).Item("appid") & "' and  payno in(1,2,3,4)"
    '                Command = New SqlCommand(str, Conn)
    '                Command.ExecuteNonQuery()

    '                str = "insert into TblLogPayment(Cusid, IdCar, Appid1,appid2, Payvalue,typelog) values('" & Trim(Tcusid) & "','" & Trim(Tcarid) & "','" & dt2.Rows(0).Item("appid") & "','" & dt3.Rows(0).Item("appid") & "',0,2)"
    '                Command = New SqlCommand(str, Conn)
    '                Command.ExecuteNonQuery()

    '                str = "update tblapppay set ispaid = 1,paydate = getdate() where appid = '" & dt3.Rows(0).Item("appid") & "' and payid = 1"
    '                Command = New SqlCommand(str, Conn)
    '                Command.ExecuteNonQuery()

    '	Conn.Close()

    '                UpdatePayno(dt3.Rows(0).Item("appid"))  'เปลี่ยนจำนวนเงินชำระใจแต่ละงวด
    '                UpdatePaynoNew(dt3.Rows(0).Item("appid"))
    '                'ส่งเคสให้ทวงหนี้เช็คแต่ละงวด
    '                AssignToCallection2(dt3.Rows(0).Item("appid"), Tcusid, Tcarid)
    '                AssignToCallection3(dt3.Rows(0).Item("appid"), Tcusid, Tcarid)
    '                AssignToCallection4(dt3.Rows(0).Item("appid"), Tcusid, Tcarid)
    '                AssignToCallection5(dt3.Rows(0).Item("appid"), Tcusid, Tcarid)
    '                AssignToCallection6(dt3.Rows(0).Item("appid"), Tcusid, Tcarid)



    '            End If
    '        End If
    '    End If



    '    If Count = 2 Then '******************************Case 2 ***********************************
    '        'chk มีประกัน และมีพรบ คนละ App กัน
    '        str = "select * from tblapplication where  idcar = '" & Tcarid & "' and isprovalue = 0 and iscarpet = 1 and appstatus = 1"
    '        Command = New SqlCommand(str, Conn)
    '        DataReader = Command.ExecuteReader
    '        If DataReader.HasRows Then
    '            While DataReader.Read
    '                If IsDBNull(DataReader("appid")) = False Then
    '                    AppID1 = DataReader("appid")
    '                Else
    '                    AppID1 = ""
    '                End If
    '            End While
    '        Else
    '            AppID1 = ""
    '        End If
    '        DataReader.Close()

    '        str = "select * from tblapplication where  idcar = '" & Tcarid & "' and isprovalue = 1 and iscarpet = 0 and appstatus = 1"
    '        Command = New SqlCommand(str, Conn)
    '        DataReader = Command.ExecuteReader
    '        If DataReader.HasRows Then
    '            While DataReader.Read
    '                If IsDBNull(DataReader("appid")) = False Then
    '                    Appid2 = DataReader("appid")
    '                Else
    '                    Appid2 = ""
    '                End If
    '            End While
    '        Else
    '            Appid2 = ""
    '        End If
    '        DataReader.Close()



    '        '-------------------------'หาประเภทรถ
    '        dt4 = New DataTable
    '        dt4.Columns.Add("cartype")

    '        str = "select cartype from tblcar where idcar = '" & Tcarid & "'"
    '        Command = New SqlCommand(str, Conn)
    '        DataReader = Command.ExecuteReader
    '        If DataReader.HasRows Then
    '            While DataReader.Read
    '                Dim dtr As DataRow = dt4.NewRow
    '                If IsDBNull(DataReader("cartype")) = False Then
    '                    dtr("cartype") = DataReader("cartype")
    '                End If
    '                dt4.Rows.Add(dtr)
    '            End While

    '        End If
    '        DataReader.Close()


    '        If AppID1 <> "" And Appid2 <> "" Then 'chk มีประกัน และมี พรบ คนละ App กัน

    '            ' gen paid no
    '            str = "Select PayID from TblPayment order by payid desc"
    '            Command = New SqlCommand(str, Conn)
    '            DataReader = Command.ExecuteReader
    '            If DataReader.HasRows Then
    '                While DataReader.Read
    '                    If IsDBNull(DataReader("PayID")) = False Then
    '                        If CStr(DataReader("PayID")) <> "" Then
    '                            PID = Format(DataReader("PayID") + 1, "0000#")
    '                        Else
    '                            PID = "00001"
    '                        End If
    '                    Else
    '                        PID = "00001"
    '                    End If
    '                End While
    '            Else
    '                PID = "00001"
    '            End If
    '            DataReader.Close()

    '            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '            'edit ปิดการโอนเงิน สร้าง App ประกัน ก่อน App พรบ. วันที่  20090728
    '            If Val(AppID1) > Val(Appid2) Then '***************สร้าง App ประกัน ก่อน App พรบ******************************************************

    '                dt5 = New DataTable
    '                dt5.Columns.Add("payvalue")
    '                dt5.Columns.Add("Payby")
    '                dt5.Columns.Add("payform")
    '                dt5.Columns.Add("paydate")

    '                If dt4.Rows(0).Item("cartype") = 1 Then ' เก๋ง 645
    '                    str = "select payvalue,payby,payform,paydate from tblpayment where appid = '" & Appid2 & "' and payno = 1"
    '                    Command = New SqlCommand(str, Conn)
    '                    DataReader = Command.ExecuteReader

    '                    If DataReader.HasRows Then

    '                        While DataReader.Read
    '                            Dim dtr As DataRow = dt5.NewRow
    '                            If IsDBNull(DataReader("payvalue")) = False Then
    '                                dtr("payvalue") = DataReader("payvalue")
    '                            End If
    '                            If IsDBNull(DataReader("Payby")) = False Then
    '                                dtr("Payby") = DataReader("Payby")
    '                            End If
    '                            If IsDBNull(DataReader("payform")) = False Then
    '                                dtr("payform") = DataReader("payform")
    '                            End If
    '                            If IsDBNull(DataReader("paydate")) = False Then
    '                                dtr("paydate") = Format(DataReader("paydate"), "dd/MM/yyyy")
    '                            End If
    '                            dt5.Rows.Add(dtr)
    '                        End While
    '                    End If
    '                    DataReader.Close()

    '                    If dt5.Rows.Count = 0 Then
    '                        If Conn.State = ConnectionState.Open Then
    '                            Conn.Close()
    '                        End If
    '                        Exit Sub
    '                    End If

    '                    If dt5.Rows.Count = 1 And dt5.Rows(0).Item("payvalue") > 645 Then  'มี record ใน payment และมี payvlaue มากกว่า 645
    '                        payvalueX = dt5.Rows(0).Item("payvalue") - 645
    '                        PaybyX = dt5.Rows(0).Item("Payby")
    '                        PfromX = dt5.Rows(0).Item("payform")

    '                        str = "update tblpayment set payvalue = " & payvalueX & ",updateid = 134,updatedate = getdate(),comment = 'โอนเงินไป App ' + '" & AppID1 & "' where appid = '" & Appid2 & "' and  payno = 1"
    '                        Command = New SqlCommand(str, Conn)
    '                        Command.ExecuteNonQuery()

    '                        str = "update tblapplication set  appcomplete = 1 where appid = '" & AppID1 & "'"
    '                        Command = New SqlCommand(str, Conn)
    '                        Command.ExecuteNonQuery()


    '                        str = "Insert into TblPayment(paydate,cusid,idcar,appid,payno,payvalue,payby,payform,comment,iscomplete,createid) " + _
    '                        " values('" & ConvertDate.SetISODate("en", dt5.Rows(0).Item("paydate")) & "','" & Trim(Tcusid) & "','" & Trim(Tcarid) & "','" & Trim(AppID1) & "',1,645,'" & PaybyX & "','" & PfromX & "','โอนเงินมาจาก App ' + '" & Appid2 & "',1,134) "
    '                        Command = New SqlCommand(str, Conn)
    '                        Command.ExecuteNonQuery()

    '                        str = "update tblapppay set ispaid = 1,paydate = getdate(),payby = '" & PaybyX & "' where appid = '" & AppID1 & "' and payid = 1"
    '                        Command = New SqlCommand(str, Conn)
    '                        Command.ExecuteNonQuery()

    '                        str = "insert into TblLogPayment(Cusid, IdCar, Appid1,appid2, Payvalue) values('" & Trim(Tcusid) & "','" & Trim(Tcarid) & "','" & Trim(Appid2) & "','" & Trim(AppID1) & "'," & dt5.Rows(0).Item("payvalue") & " )"
    '                        Command = New SqlCommand(str, Conn)
    '                        Command.ExecuteNonQuery()

    '                        AssignToCallection2(Appid2, Tcusid, Tcarid)
    '                        AssignToCallection3(Appid2, Tcusid, Tcarid)
    '                        AssignToCallection4(Appid2, Tcusid, Tcarid)
    '                        AssignToCallection5(Appid2, Tcusid, Tcarid)
    '                        AssignToCallection6(Appid2, Tcusid, Tcarid)
    '                        UpdatePBvalueTblapppay(Appid2, 1)  'เพิ่มค่า พรบ.

    '                    End If
    '                End If

    '                If dt4.Rows(0).Item("cartype") <> 1 Then 'กะบะ และอื่นๆ 967
    '                    str = "select payvalue,payby,payform,paydate from tblpayment where appid = '" & Appid2 & "' and payno = 1"
    '                    Command = New SqlCommand(str, Conn)
    '                    DataReader = Command.ExecuteReader

    '                    If DataReader.HasRows Then
    '                        While DataReader.Read
    '                            Dim dtr As DataRow = dt5.NewRow
    '                            If IsDBNull(DataReader("payvalue")) = False Then
    '                                dtr("payvalue") = DataReader("payvalue")
    '                            End If
    '                            If IsDBNull(DataReader("Payby")) = False Then
    '                                dtr("Payby") = DataReader("Payby")
    '                            End If
    '                            If IsDBNull(DataReader("payform")) = False Then
    '                                dtr("payform") = DataReader("payform")
    '                                If IsDBNull(DataReader("paydate")) = False Then
    '                                    dtr("paydate") = Format(DataReader("paydate"), "dd/MM/yyyy")
    '                                End If
    '                            End If
    '                            dt5.Rows.Add(dtr)
    '                        End While
    '                    End If
    '                    DataReader.Close()

    '                    'กรณีไม่มียอดเงิน ปล่อยผ่านเลยไม่ต้องโอน 9/05/55
    '                    If dt5.Rows.Count = 0 Then
    '                        If Conn.State = ConnectionState.Open Then
    '                            Conn.Close()
    '                        End If
    '                        Exit Sub
    '                    End If

    '                    If dt5.Rows.Count = 1 And dt5.Rows(0).Item("payvalue") > 967 Then  'มี record ใน payment และ payvlaue เกิน 967
    '                        payvalueX = dt5.Rows(0).Item("payvalue") - 967
    '                        PaybyX = dt5.Rows(0).Item("Payby")
    '                        PfromX = dt5.Rows(0).Item("payform")

    '                        str = "update tblpayment set payvalue = " & payvalueX & ",updateid = 134,updatedate = getdate(),comment = 'โอนเงินไป App ' + '" & AppID1 & "' where appid = '" & Appid2 & "' and payno = 1"
    '                        Command = New SqlCommand(str, Conn)
    '                        Command.ExecuteNonQuery()

    '                        str = "update tblapplication set  appcomplete = 1 where appid = '" & AppID1 & "'"
    '                        Command = New SqlCommand(str, Conn)
    '                        Command.ExecuteNonQuery()

    '                        str = "Insert into TblPayment(paydate,cusid,idcar,appid,payno,payvalue,payby,payform,comment,iscomplete,createid) " + _
    '                         " values('" & ConvertDate.SetISODate("en", dt5.Rows(0).Item("paydate")) & "','" & Trim(Tcusid) & "','" & Trim(Tcarid) & "','" & Trim(AppID1) & "',1,967,'" & PaybyX & "','" & PfromX & "','โอนเงินมาจาก App ' + '" & Appid2 & "',1,134) "
    '                        Command = New SqlCommand(str, Conn)
    '                        Command.ExecuteNonQuery()

    '                        str = "update tblapppay set ispaid = 1,paydate = getdate(),payby = '" & PaybyX & "' where appid = '" & AppID1 & "' and payid = 1"
    '                        Command = New SqlCommand(str, Conn)
    '                        Command.ExecuteNonQuery()

    '                        str = "insert into TblLogPayment(Cusid, IdCar, Appid1,appid2, Payvalue) values('" & Trim(Tcusid) & "','" & Trim(Tcarid) & "','" & Trim(Appid2) & "','" & Trim(AppID1) & "'," & dt5.Rows(0).Item("payvalue") & " )"
    '                        Command = New SqlCommand(str, Conn)
    '                        Command.ExecuteNonQuery()

    '                        AssignToCallection2(Appid2, Tcusid, Tcarid)
    '                        AssignToCallection3(Appid2, Tcusid, Tcarid)
    '                        AssignToCallection4(Appid2, Tcusid, Tcarid)
    '                        AssignToCallection5(Appid2, Tcusid, Tcarid)
    '                        AssignToCallection6(Appid2, Tcusid, Tcarid)
    '                        UpdatePBvalueTblapppay(Appid2, 2)  'เพิ่มค่า พรบ.

    '                    End If
    '                End If
    '            End If '**********************************************************************************************

    '            '-----------------------------------------------------------------------
    '            '-----------------------------------------------------------------------
    '            '-----------------------------------------------------------------------
    '            '-----------------------------------------------------------------------
    '            '-----------------------------------------------------------------------

    '            If Val(AppID1) < Val(Appid2) Then '***************สร้าง App พรบ ก่อน App ประกัน******************************************************
    '                dt5 = New DataTable
    '                dt5.Columns.Add("payvalue")
    '                dt5.Columns.Add("Payby")
    '                dt5.Columns.Add("payform")
    '                dt5.Columns.Add("paydate")

    '                If dt4.Rows(0).Item("cartype") = 1 Then ' เก๋ง 645
    '                    str = "select payvalue,payby,payform,paydate from tblpayment where appid = '" & AppID1 & "' and payno = 1"
    '                    Command = New SqlCommand(str, Conn)
    '                    DataReader = Command.ExecuteReader

    '                    If DataReader.HasRows Then

    '                        While DataReader.Read
    '                            Dim dtr As DataRow = dt5.NewRow
    '                            If IsDBNull(DataReader("payvalue")) = False Then
    '                                dtr("payvalue") = DataReader("payvalue")
    '                            End If
    '                            If IsDBNull(DataReader("Payby")) = False Then
    '                                dtr("Payby") = DataReader("Payby")
    '                            End If
    '                            If IsDBNull(DataReader("payform")) = False Then
    '                                dtr("payform") = DataReader("payform")
    '                            End If
    '                            If IsDBNull(DataReader("paydate")) = False Then
    '                                dtr("paydate") = Format(DataReader("paydate"), "dd/MM/yyyy")
    '                            End If
    '                            dt5.Rows.Add(dtr)
    '                        End While
    '                    End If
    '                    DataReader.Close()

    '                    If dt5.Rows.Count = 0 Then
    '                        If Conn.State = ConnectionState.Open Then
    '                            Conn.Close()
    '                        End If
    '                        Exit Sub
    '                    End If

    '                    If dt5.Rows.Count = 1 And dt5.Rows(0).Item("payvalue") > 645 Then     'ÁÕ record ã¹ payment áÅÐ payvlaue à¡Ô¹ 645
    '                        payvalueX = dt5.Rows(0).Item("payvalue") - 645
    '                        PaybyX = dt5.Rows(0).Item("Payby")
    '                        PfromX = dt5.Rows(0).Item("payform")


    '                        str = "update tblpayment set payvalue = 645,iscomplete = 1,updateid = 134,updatedate = getdate(),comment = 'โอนเงินไป App  ' + '" & Appid2 & "'  where appid = '" & AppID1 & "' and payno = 1"
    '                        Command = New SqlCommand(str, Conn)
    '                        Command.ExecuteNonQuery()

    '                        str = "update tblapplication set  appcomplete = 1 where appid = '" & AppID1 & "'"
    '                        Command = New SqlCommand(str, Conn)
    '                        Command.ExecuteNonQuery()

    '                        str = "Insert into TblPayment(paydate,cusid,idcar,appid,payno,payvalue,payby,payform,comment,iscomplete,createid) " + _
    '                        " values('" & ConvertDate.SetISODate("en", dt5.Rows(0).Item("paydate")) & "','" & Trim(Tcusid) & "','" & Trim(Tcarid) & "','" & Trim(Appid2) & "',1," & payvalueX & ",'" & PaybyX & "','" & PfromX & "','โอนเงินมาจาก App ' + '" & AppID1 & "',0,134) "
    '                        Command = New SqlCommand(str, Conn)
    '                        Command.ExecuteNonQuery()


    '                        str = "update tblapppay set ispaid = 1 ,paydate = getdate(),payby = '" & PaybyX & "' where appid = '" & Appid2 & "' and payid = 1"
    '                        Command = New SqlCommand(str, Conn)
    '                        Command.ExecuteNonQuery()

    '                        str = "update tblpayment set appid = '" & Appid2 & "', comment = 'โอนเงินมาจาก App ' + '" & AppID1 & "'  where appid = '" & AppID1 & "' and  payno in(2,3,4)"
    '                        Command = New SqlCommand(str, Conn)
    '                        Command.ExecuteNonQuery()

    '                        str = "insert into TblLogPayment(Cusid, IdCar, Appid1,appid2, Payvalue) values('" & Trim(Tcusid) & "','" & Trim(Tcarid) & "','" & Trim(AppID1) & "','" & Trim(Appid2) & "'," & dt5.Rows(0).Item("payvalue") & " )"
    '                        Command = New SqlCommand(str, Conn)
    '                        Command.ExecuteNonQuery()

    '                        Conn.Close()

    '                        UpdatePayno(Appid2)  'เปลี่ยนจำนวนเงินชำระในแต่ละงวด
    '                        UpdatePaynoNew(Appid2)
    '                        AssignToCallection2(Appid2, Tcusid, Tcarid)
    '                        AssignToCallection3(Appid2, Tcusid, Tcarid)
    '                        AssignToCallection4(Appid2, Tcusid, Tcarid)
    '                        AssignToCallection5(Appid2, Tcusid, Tcarid)
    '                        AssignToCallection6(Appid2, Tcusid, Tcarid)


    '                    End If
    '                End If

    '                If dt4.Rows(0).Item("cartype") <> 1 Then 'กระบะ และอื่นๆ ราคา 967
    '                    str = "select payvalue,payby,payform,paydate from tblpayment where appid = '" & AppID1 & "' and payno = 1"
    '                    Command = New SqlCommand(str, Conn)
    '                    DataReader = Command.ExecuteReader

    '                    If DataReader.HasRows Then
    '                        While DataReader.Read
    '                            Dim dtr As DataRow = dt5.NewRow
    '                            If IsDBNull(DataReader("payvalue")) = False Then
    '                                dtr("payvalue") = DataReader("payvalue")
    '                            End If
    '                            If IsDBNull(DataReader("Payby")) = False Then
    '                                dtr("Payby") = DataReader("Payby")
    '                            End If
    '                            If IsDBNull(DataReader("payform")) = False Then
    '                                dtr("payform") = DataReader("payform")
    '                            End If
    '                            If IsDBNull(DataReader("paydate")) = False Then
    '                                dtr("paydate") = Format(DataReader("paydate"), "dd/MM/yyyy")
    '                            End If
    '                            dt5.Rows.Add(dtr)
    '                        End While
    '                    End If
    '                    DataReader.Close()

    '                    If dt5.Rows.Count = 0 Then
    '                        If Conn.State = ConnectionState.Open Then
    '                            Conn.Close()
    '                        End If
    '                        Exit Sub
    '                    End If

    '                    If dt5.Rows.Count = 1 And dt5.Rows(0).Item("payvalue") > 967 Then  'มี Record ใน Payment แอะ PayValue เกิน 967
    '                        payvalueX = dt5.Rows(0).Item("payvalue") - 967
    '                        PaybyX = dt5.Rows(0).Item("Payby")
    '                        PfromX = dt5.Rows(0).Item("payform")

    '                        str = "update tblpayment set payvalue = 967,iscomplete = 1,updateid = 134,updatedate = getdate(),comment = 'โอนเงินไป App ' + '" & Appid2 & "'  where appid = '" & AppID1 & "' and payno = 1"
    '                        Command = New SqlCommand(str, Conn)
    '                        Command.ExecuteNonQuery()



    '                        str = "update tblapplication set  appcomplete = 1 where appid = '" & AppID1 & "'"
    '                        Command = New SqlCommand(str, Conn)
    '                        Command.ExecuteNonQuery()

    '                        str = "Insert into TblPayment(paydate,cusid,idcar,appid,payno,payvalue,payby,payform,comment,iscomplete,createid) " + _
    '                                     " values('" & ConvertDate.SetISODate("en", dt5.Rows(0).Item("paydate")) & "','" & Trim(Tcusid) & "','" & Trim(Tcarid) & "','" & Trim(Appid2) & "',1," & payvalueX & ",'" & PaybyX & "','" & PfromX & "','โอนเงินมาจาก App '+ '" & AppID1 & "',0,134) "
    '                        Command = New SqlCommand(str, Conn)
    '                        Command.ExecuteNonQuery()

    '                        str = "update tblapppay set ispaid = 1 ,paydate = getdate(),payby = '" & PaybyX & "' where appid = '" & Appid2 & "' and payid = 1"
    '                        Command = New SqlCommand(str, Conn)
    '                        Command.ExecuteNonQuery()

    '                        str = "update tblpayment set appid = '" & Appid2 & "', comment = 'โอนเงินมาจาก App ' + '" & AppID1 & "'  where appid = '" & AppID1 & "' and  payno in(2,3,4)"
    '                        Command = New SqlCommand(str, Conn)
    '                        Command.ExecuteNonQuery()

    '                        str = "insert into TblLogPayment(Cusid, IdCar, Appid1,appid2, Payvalue) values('" & Trim(Tcusid) & "','" & Trim(Tcarid) & "','" & Trim(AppID1) & "','" & Trim(Appid2) & "'," & dt5.Rows(0).Item("payvalue") & " )"
    '                        Command = New SqlCommand(str, Conn)
    '                        Command.ExecuteNonQuery()

    '                        Conn.Close()

    '                        UpdatePayno(Appid2)  'เปลี่ยนจำนวนเงินชำระในแต่ละงวด
    '                        UpdatePaynoNew(Appid2)
    '                        AssignToCallection2(Appid2, Tcusid, Tcarid)
    '                        AssignToCallection3(Appid2, Tcusid, Tcarid)
    '                        AssignToCallection4(Appid2, Tcusid, Tcarid)
    '                        AssignToCallection5(Appid2, Tcusid, Tcarid)
    '                        AssignToCallection6(Appid2, Tcusid, Tcarid)

    '                    End If
    '                End If
    '            End If '**********************************************************************************************

    '        End If

    '    End If
    '    If Conn.State = ConnectionState.Open Then
    '        Conn.Close()
    '    End If
    'End Sub

    Public Sub CheckData()
        Dim str As String
        Dim Command As SqlCommand
        Conn.Open()

        str = "update tblapplication  set isprovalue = 0 Where appstatus = 0 And isprovalue = 1 and year(createdate) = 2009 "
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        str = "update tblapplication  set iscarpet = 0 Where appstatus = 0 And iscarpet = 1 and year(createdate) = 2009 "
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()


        str = "drop table arenew "
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        str = "SELECT TblApplication.AppID, TblApplication.Idcar, TblCar.productID Into Arenew " + _
              " FROM TblApplication INNER JOIN TblCar ON TblApplication.Idcar = TblCar.IdCar LEFT OUTER JOIN " + _
              " TmpApp_CustRenew ON TblApplication.Idcar = TmpApp_CustRenew.Idcar Where (TmpApp_CustRenew.idcar Is Null) And (TblCar.productID <> 0) "
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        str = "update tblcar set tblcar.productid = 0 FROM Arenew INNER JOIN TblCar ON Arenew.idcar = TblCar.IdCar "
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()


        str = " drop table arenew "
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        str = " SELECT   TmpApp_CustRenew.successdate,TblApplication.idcar,  TblApplication.AppID, TblApplication.ProDuctID, TmpApp_CustRenew.ProDuctID AS ProDuctIDT, tblcar.productid as ProCar, TmpApp_CustRenew.AppID AS AppIDT " + _
        " Into Arenew  FROM TblApplication inner JOIN " + _
        " (select b.* from (select idcar,max(appid)as appid  From TmpApp_CustRenew where year(successdate)in (2008,2009) and appstatus = 1 " + _
        " group by idcar) a inner join tmpapp_custrenew b   on a.appid = b.appid) tmpapp_custrenew ON TblApplication.Idcar = TmpApp_CustRenew.Idcar inner join tblcar on tblapplication.idcar = tblcar.idcar " + _
        " Where TblApplication.idcar <> 0 and   TmpApp_CustRenew.ProDuctID <> tblcar.productid "
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        str = " update tblcar set tblcar.productid = Arenew.ProDuctIDT FROM Arenew INNER JOIN TblCar ON Arenew.idcar = TblCar.IdCar "
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        Conn.Close()

    End Sub

    Private Sub UpdatePayno(ByVal appx As String)
        Conn.Open()

        Dim pay1 As Integer
        Dim payvalueX As Integer
        Dim paycou As Byte
        Dim payvalueNew As Integer
        Dim insu, pb As Integer
        Dim compTAX As String
        Dim compSUFFIX, barC As String
        Dim refNOS As String
        compTAX = "3031815285"            '' company tax id.
        compSUFFIX = "00"

        Dim str As String
        Dim Command As SqlCommand
        Dim DataReader As SqlDataReader

        str = "select * from tblpayment where appid = '" & appx & "' and payno = 1"
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader()
        If DataReader.HasRows Then
            While DataReader.Read
                If IsDBNull(DataReader("payvalue")) = False Then
                    pay1 = DataReader("payvalue") ' ยอดที่จ่ายมาทั้งหมดในงวดแรก
                End If
            End While
        End If
        DataReader.Close()

        '***************************************************************************************************************
        str = "select provalue as p1 from tblapplication where appid = '" & appx & "' and isprovalue = 1 "
        insu = 0
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader()
        If DataReader.HasRows Then
            While DataReader.Read
                If IsDBNull(DataReader("p1")) = False Then
                    insu = DataReader("p1") ''เบี้ยประกัน
                End If
            End While
        End If
        DataReader.Close()

        pb = 0
        str = "select carpet as p1 from tblapplication where appid = '" & appx & "' and iscarpet = 1 "
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader()
        If DataReader.HasRows Then
            While DataReader.Read
                If IsDBNull(DataReader("p1")) = False Then
                    pb = DataReader("p1") ''เบี้ยพ.ร.บ.
                End If
            End While
        End If
        DataReader.Close()

        payvalueX = (insu + pb) - pay1  'เบี้ยประกัน + พรบ หัก ยอดที่จ่ายเข้ามาในงวดแรกใน TblPayment



        '***************************************************************************************************************

        str = "select count(*) as payno from tblapppay where appid = '" & appx & "' " 'หาจำนวนงวด
        Command = New SqlCommand(str, Conn)
        paycou = Command.ExecuteScalar

        'If paycou <> 0 Then
        '    paycou = paycou 'จำนวนงวดที่เหลือ
        'End If


        '***************************************************************************************************************
        str = "select tblcar.refno from tblapplication inner join tblcar on tblapplication.idcar = tblcar.idcar where appid = '" & appx & "' "
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader
        If DataReader.HasRows Then
            While DataReader.Read
                If IsDBNull(DataReader("refno")) = False Then
                    refNOS = DataReader("refno")
                End If
            End While
        End If
        DataReader.Close()
        '***************************************************************************************************************

        If paycou <> 0 Then

            If paycou <> 1 Then   ' แก้ไขงวด
                payvalueNew = (payvalueX / (paycou - 1))
                For i = 1 To paycou
                    If i = 1 Then  ' แก้ไขงวด 1 เท่ากับ จำนวนที่จ่ายมาทั้งหมดในงวดแรก ใน TblPayment
                        barC = "|" & compTAX.Trim & compSUFFIX.Trim & Chr(13) & refNOS.Trim & Chr(13) & Chr(13) & CStr(pay1).Trim & "00"
                        str = "update tblapppay set totalpay = '" & pay1 & "',updatedate = getdate(),updateid = '" & Request.Cookies("UserID").Value & "' ,payBARCODE = '" & barC & "' where appid = '" & appx & "'  and payid = '" & i & "' "
                    End If
                    If i <> 1 Then
                        barC = "|" & compTAX.Trim & compSUFFIX.Trim & Chr(13) & refNOS.Trim & Chr(13) & Chr(13) & CStr(payvalueX).Trim & "00"
                        str = "update tblapppay set totalpay = '" & payvalueNew & "',updatedate = getdate(),updateid = '" & Request.Cookies("UserID").Value & "' ,payBARCODE = '" & barC & "'  where appid = '" & appx & "'  and payid = '" & i & "' "
                    End If
                    Command = New SqlCommand(str, Conn)
                    Command.ExecuteNonQuery()
                Next
            End If

            If paycou = 1 Then  'เพิ่มงวด
                barC = "|" & compTAX.Trim & compSUFFIX.Trim & Chr(13) & refNOS.Trim & Chr(13) & Chr(13) & CStr(pay1).Trim & "00"
                str = "update tblapppay set totalpay = '" & pay1 & "',updatedate = getdate(),updateid = '" & Request.Cookies("UserID").Value & "',payBARCODE = '" & barC & "' where appid = '" & appx & "'  and payid = '" & 1 & "' "
                Command = New SqlCommand(str, Conn) 'แก้ไขงวด 1 เท่ากับ จำนวนที่จ่ายมาทั้งหมดในงวดแรก ใน TblPayment
                Command.ExecuteNonQuery()

                barC = "|" & compTAX.Trim & compSUFFIX.Trim & Chr(13) & refNOS.Trim & Chr(13) & Chr(13) & CStr(payvalueX).Trim & "00"
                str = "insert into TblAppPay (PayID,AppID,AppointDate,TotalPay,typepay,payBARCODE,CreateID,potype) values (2,'" & appx & "',getdate()," & payvalueX & ",1,'" & barC & "','" & Request.Cookies("UserID").Value & "',1)"
                Command = New SqlCommand(str, Conn)
                Command.ExecuteNonQuery()
            End If

            For i = 2 To paycou
                If i = 2 Then
                    str = "update tblapppay set AppointDate =  '" & ConvertDate.SetISODate("en", Format(Date.Now, "dd/MM/yyyy")) & "' where appid = '" & appx & "'  and payid = 2 "
                End If
                If i = 3 Then
                    str = "update tblapppay set AppointDate =  '" & ConvertDate.SetISODate("en", Format(Date.Now.AddMonths(1), "dd/MM/yyyy")) & "' where appid = '" & appx & "'  and payid = 3 "
                End If
                If i = 4 Then
                    str = "update tblapppay set AppointDate =  '" & ConvertDate.SetISODate("en", Format(Date.Now.AddMonths(2), "dd/MM/yyyy")) & "' where appid = '" & appx & "'  and payid = 4 "
                End If
                If i = 5 Then
                    str = "update tblapppay set AppointDate =  '" & ConvertDate.SetISODate("en", Format(Date.Now.AddMonths(3), "dd/MM/yyyy")) & "' where appid = '" & appx & "'  and payid = 5 "
                End If
                If i = 6 Then
                    str = "update tblapppay set AppointDate =  '" & ConvertDate.SetISODate("en", Format(Date.Now.AddMonths(4), "dd/MM/yyyy")) & "' where appid = '" & appx & "'  and payid = 6 "
                End If
                Command = New SqlCommand(str, Conn)
                Command.ExecuteNonQuery()
            Next

            str = "insert into  TblLogChangeApppay (appid,createdate,createid) values ('" & appx & "',getdate(),'" & Request.Cookies("UserID").Value & "')"
            Command = New SqlCommand(str, Conn)
            Command.ExecuteNonQuery()
        End If

        Conn.Close()
    End Sub

    Private Sub UpdatePBvalueTblapppay(ByVal apx As String, ByVal flag As Byte) ' flag ประเภทรถ
        Conn.Open()

        Dim str As String
        Dim Command As SqlCommand
        Dim DataReader As SqlDataReader
        dt = New DataTable
        dt.Columns.Add("appid")
        dt.Columns.Add("po")

        str = " SELECT TOP (1) PO, PayID, AppID From TblAppPay  Where (AppID = '" & apx & "') And (Ispaid = 0)  ORDER BY PayID "
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader

        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt.NewRow
                If IsDBNull(DataReader("appid")) = False Then
                    dtr("appid") = DataReader("appid")
                End If
                If IsDBNull(DataReader("po")) = False Then
                    dtr("po") = DataReader("po")
                End If
                dt.Rows.Add(dtr)
            End While
        End If
        DataReader.Close()

        If dt.Rows.Count = 1 Then

            If flag = 1 Then   ' เก๋ง
                str = "update tblapppay set TotalPay = TotalPay + 645,updatedate = getdate(), updateid = 134  where po = '" & dt.Rows(0).Item("po") & "'   "
                Command = New SqlCommand(str, Conn)
                Command.ExecuteNonQuery()

                str = "update tblapplication set Comments = Comments + ' เพิ่มค่าพรบ.อีก 645 บาท ' where appid  = '" & dt.Rows(0).Item("appid") & "' "
                Command = New SqlCommand(str, Conn)
                Command.ExecuteNonQuery()
            End If
            If flag = 2 Then   ' กระบะ
                str = "update tblapppay set TotalPay = TotalPay + 967,updatedate = getdate(), updateid = 134  where po = '" & dt.Rows(0).Item("po") & "'   "
                Command = New SqlCommand(str, Conn)
                Command.ExecuteNonQuery()

                str = "update tblapplication set Comments = Comments + ' เพิ่มค่าพรบ.อีก 967 บาท ' where appid  = '" & dt.Rows(0).Item("appid") & "' "
                Command = New SqlCommand(str, Conn)
                Command.ExecuteNonQuery()
            End If

        End If

        Conn.Close()
    End Sub

    Private Sub AssignToCallection2(ByVal appidx As String, ByVal tcus As String, ByVal tcar As String) '28/01/2009
        If Conn.State = ConnectionState.Open Then
            Conn.Close()
        End If

        Conn.Open()

        Dim str As String
        Dim Command As SqlCommand
        Dim DataReader As SqlDataReader

        Dim value1 As Integer
        Dim d1 As String
        Dim Tpay As Integer

        ' หาส่วนต่าง tblapppay กับ tblpayment งวด 2
        str = " SELECT TblAppPay.TotalPay, Tblpayment.PayValue, TblAppPay.TotalPay - Tblpayment.PayValue AS value1 ,appointdate" + _
              " FROM TblAppPay INNER JOIN  Tblpayment ON TblAppPay.AppID = Tblpayment.AppID AND TblAppPay.PayID = Tblpayment.PayNo " + _
              " Where (Tblpayment.PayNo = 2) And (TblAppPay.appid = '" & appidx & "') "

        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader
        If DataReader.HasRows Then
            While DataReader.Read
                If IsDBNull(DataReader("value1")) = False Then
                    value1 = DataReader("value1")
                End If
                If IsDBNull(DataReader("totalpay")) = False Then
                    Tpay = DataReader("totalpay")
                End If
                If IsDBNull(DataReader("appointdate")) = False Then
                    d1 = Format(DataReader("appointdate"), "dd/MM/yyyy")
                End If
            End While
        End If
        DataReader.Close()

        If value1 < 50 Then
            Conn.Close()
            Exit Sub
        End If



        'หา TSR ที่ Case น้อยที่สุดเพื่อโอนให้
        Dim assignto As String
        str = "SELECT TOP 1 Assignto, COUNT(*) AS Expr1 From TblAssignCallection Where (statuscase = 0) and assignto in (select userid from tbluser where userlevelid = 11 and userstatus = 1 ) GROUP BY Assignto ORDER BY COUNT(*) "

        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader

        If DataReader.HasRows Then
            While DataReader.Read
                If IsDBNull(DataReader("Assignto")) = False Then
                    assignto = DataReader("Assignto")
                End If
            End While
        End If
        DataReader.Close()


        'ส่ง Case ให้ทวงหนี้
        str = "insert into tblassigncallection(Appid,idcar,cusid, Payno, Appointdate, Assignto, CallAt,totalpay,caseTYPE) " + _
               " values('" & appidx & "','" & tcar & "','" & tcus & "',2,'" & ConvertDate.SetISODate("en", d1) & "','" & assignto & "',1," & value1 & ",1)"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        'เก็บ log
        str = "insert into TblLogass_PaidDIFF( cusID, idCAR, appID, payID, totalPAY, paidIN, createID, createDATE, addBY) " + _
        "values ('" & tcus & "','" & tcar & "','" & appidx & "',2," & Tpay & "," & value1 & ",'" & Request.Cookies("UserID").Value & "', getdate(),'โอนเงิน Auto')"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        Conn.Close()
    End Sub

    Private Sub AssignToCallection3(ByVal appidx As String, ByVal tcus As String, ByVal tcar As String) '28/01/2009
        If Conn.State = ConnectionState.Open Then
            Conn.Close()
        End If

        Conn.Open()

        Dim str As String
        Dim Command As SqlCommand
        Dim DataReader As SqlDataReader

        Dim value1 As Integer
        Dim d1 As String
        Dim Tpay As Integer

        ' หาส่วนต่าง tblapppay กับ tblpayment งวด 3
        str = " SELECT TblAppPay.TotalPay, Tblpayment.PayValue, TblAppPay.TotalPay - Tblpayment.PayValue AS value1 ,appointdate" + _
              " FROM TblAppPay INNER JOIN  Tblpayment ON TblAppPay.AppID = Tblpayment.AppID AND TblAppPay.PayID = Tblpayment.PayNo " + _
              " Where (Tblpayment.PayNo = 3) And (TblAppPay.appid = '" & appidx & "') "

        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader
        If DataReader.HasRows Then
            While DataReader.Read
                If IsDBNull(DataReader("value1")) = False Then
                    value1 = DataReader("value1")
                End If
                If IsDBNull(DataReader("totalpay")) = False Then
                    Tpay = DataReader("totalpay")
                End If
                If IsDBNull(DataReader("appointdate")) = False Then
                    d1 = Format(DataReader("appointdate"), "dd/MM/yyyy")
                End If
            End While
        End If
        DataReader.Close()

        If value1 < 50 Then
            Conn.Close()
            Exit Sub
        End If



        'หา TSR ที่ Case น้อยที่สุดเพื่อโอนให้
        Dim assignto As String
        str = "SELECT TOP 1 Assignto, COUNT(*) AS Expr1 From TblAssignCallection Where (statuscase = 0) and assignto in (select userid from tbluser where userlevelid = 11 and userstatus = 1 ) GROUP BY Assignto ORDER BY COUNT(*) "

        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader

        If DataReader.HasRows Then
            While DataReader.Read
                If IsDBNull(DataReader("Assignto")) = False Then
                    assignto = DataReader("Assignto")
                End If
            End While
        End If
        DataReader.Close()


        'ส่ง Case ให้ทวงหนี้
        str = "insert into tblassigncallection(Appid,idcar,cusid, Payno, Appointdate, Assignto, CallAt,totalpay,caseTYPE) " + _
               " values('" & appidx & "','" & tcar & "','" & tcus & "',3,'" & ConvertDate.SetISODate("en", d1) & "','" & assignto & "',1," & value1 & ",1)"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        'เก็บ log
        str = "insert into TblLogass_PaidDIFF( cusID, idCAR, appID, payID, totalPAY, paidIN, createID, createDATE, addBY) " + _
        "values ('" & tcus & "','" & tcar & "','" & appidx & "',3," & Tpay & "," & value1 & ",'" & Request.Cookies("UserID").Value & "', getdate(),'โอนเงิน Auto')"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        Conn.Close()
    End Sub

    Private Sub AssignToCallection4(ByVal appidx As String, ByVal tcus As String, ByVal tcar As String) '28/01/2009
        If Conn.State = ConnectionState.Open Then
            Conn.Close()
        End If

        Conn.Open()

        Dim str As String
        Dim Command As SqlCommand
        Dim DataReader As SqlDataReader

        Dim value1 As Integer
        Dim d1 As String
        Dim Tpay As Integer

        ' หาส่วนต่าง tblapppay กับ tblpayment งวด 4
        str = " SELECT TblAppPay.TotalPay, Tblpayment.PayValue, TblAppPay.TotalPay - Tblpayment.PayValue AS value1 ,appointdate" + _
              " FROM TblAppPay INNER JOIN  Tblpayment ON TblAppPay.AppID = Tblpayment.AppID AND TblAppPay.PayID = Tblpayment.PayNo " + _
              " Where (Tblpayment.PayNo = 4) And (TblAppPay.appid = '" & appidx & "') "

        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader
        If DataReader.HasRows Then
            While DataReader.Read
                If IsDBNull(DataReader("value1")) = False Then
                    value1 = DataReader("value1")
                End If
                If IsDBNull(DataReader("totalpay")) = False Then
                    Tpay = DataReader("totalpay")
                End If
                If IsDBNull(DataReader("appointdate")) = False Then
                    d1 = Format(DataReader("appointdate"), "dd/MM/yyyy")
                End If
            End While
        End If
        DataReader.Close()

        If value1 < 50 Then
            Conn.Close()
            Exit Sub
        End If



        'หา TSR ที่ Case น้อยที่สุดเพื่อโอนให้
        Dim assignto As String
        str = "SELECT TOP 1 Assignto, COUNT(*) AS Expr1 From TblAssignCallection Where (statuscase = 0) and assignto in (select userid from tbluser where userlevelid = 11 and userstatus = 1 ) GROUP BY Assignto ORDER BY COUNT(*) "

        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader

        If DataReader.HasRows Then
            While DataReader.Read
                If IsDBNull(DataReader("Assignto")) = False Then
                    assignto = DataReader("Assignto")
                End If
            End While
        End If
        DataReader.Close()


        'ส่ง Case ให้ทวงหนี้
        str = "insert into tblassigncallection(Appid,idcar,cusid, Payno, Appointdate, Assignto, CallAt,totalpay,caseTYPE) " + _
               " values('" & appidx & "','" & tcar & "','" & tcus & "',4,'" & ConvertDate.SetISODate("en", d1) & "','" & assignto & "',1," & value1 & ",1)"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        'เก็บ log
        str = "insert into TblLogass_PaidDIFF( cusID, idCAR, appID, payID, totalPAY, paidIN, createID, createDATE, addBY) " + _
        "values ('" & tcus & "','" & tcar & "','" & appidx & "',4," & Tpay & "," & value1 & ",'" & Request.Cookies("UserID").Value & "', getdate(),'โอนเงิน Auto')"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        Conn.Close()
    End Sub

    Private Sub AssignToCallection5(ByVal appidx As String, ByVal tcus As String, ByVal tcar As String) '28/01/2009
        If Conn.State = ConnectionState.Open Then
            Conn.Close()
        End If

        Conn.Open()

        Dim str As String
        Dim Command As SqlCommand
        Dim DataReader As SqlDataReader

        Dim value1 As Integer
        Dim d1 As String
        Dim Tpay As Integer

        ' หาส่วนต่าง tblapppay กับ tblpayment งวด 2
        str = " SELECT TblAppPay.TotalPay, Tblpayment.PayValue, TblAppPay.TotalPay - Tblpayment.PayValue AS value1 ,appointdate" + _
              " FROM TblAppPay INNER JOIN  Tblpayment ON TblAppPay.AppID = Tblpayment.AppID AND TblAppPay.PayID = Tblpayment.PayNo " + _
              " Where (Tblpayment.PayNo = 5) And (TblAppPay.appid = '" & appidx & "') "

        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader
        If DataReader.HasRows Then
            While DataReader.Read
                If IsDBNull(DataReader("value1")) = False Then
                    value1 = DataReader("value1")
                End If
                If IsDBNull(DataReader("totalpay")) = False Then
                    Tpay = DataReader("totalpay")
                End If
                If IsDBNull(DataReader("appointdate")) = False Then
                    d1 = Format(DataReader("appointdate"), "dd/MM/yyyy")
                End If
            End While
        End If
        DataReader.Close()

        If value1 < 50 Then
            Conn.Close()
            Exit Sub
        End If



        'หา TSR ที่ Case น้อยที่สุดเพื่อโอนให้
        Dim assignto As String
        str = "SELECT TOP 1 Assignto, COUNT(*) AS Expr1 From TblAssignCallection Where (statuscase = 0) and assignto in (select userid from tbluser where userlevelid = 11 and userstatus = 1 ) GROUP BY Assignto ORDER BY COUNT(*) "

        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader

        If DataReader.HasRows Then
            While DataReader.Read
                If IsDBNull(DataReader("Assignto")) = False Then
                    assignto = DataReader("Assignto")
                End If
            End While
        End If
        DataReader.Close()


        'ส่ง Case ให้ทวงหนี้
        str = "insert into tblassigncallection(Appid,idcar,cusid, Payno, Appointdate, Assignto, CallAt,totalpay,caseTYPE) " + _
               " values('" & appidx & "','" & tcar & "','" & tcus & "',5,'" & ConvertDate.SetISODate("en", d1) & "','" & assignto & "',1," & value1 & ",1)"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        'เก็บ log
        str = "insert into TblLogass_PaidDIFF( cusID, idCAR, appID, payID, totalPAY, paidIN, createID, createDATE, addBY) " + _
        "values ('" & tcus & "','" & tcar & "','" & appidx & "',5," & Tpay & "," & value1 & ",'" & Request.Cookies("UserID").Value & "', getdate(),'โอนเงิน Auto')"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        Conn.Close()
    End Sub

    Private Sub AssignToCallection6(ByVal appidx As String, ByVal tcus As String, ByVal tcar As String) '28/01/2009
        If Conn.State = ConnectionState.Open Then
            Conn.Close()
        End If

        Conn.Open()

        Dim str As String
        Dim Command As SqlCommand
        Dim DataReader As SqlDataReader

        Dim value1 As Integer
        Dim d1 As String
        Dim Tpay As Integer

        ' หาส่วนต่าง tblapppay กับ tblpayment งวด 2
        str = " SELECT TblAppPay.TotalPay, Tblpayment.PayValue, TblAppPay.TotalPay - Tblpayment.PayValue AS value1 ,appointdate" + _
              " FROM TblAppPay INNER JOIN  Tblpayment ON TblAppPay.AppID = Tblpayment.AppID AND TblAppPay.PayID = Tblpayment.PayNo " + _
              " Where (Tblpayment.PayNo = 6) And (TblAppPay.appid = '" & appidx & "') "

        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader
        If DataReader.HasRows Then
            While DataReader.Read
                If IsDBNull(DataReader("value1")) = False Then
                    value1 = DataReader("value1")
                End If
                If IsDBNull(DataReader("totalpay")) = False Then
                    Tpay = DataReader("totalpay")
                End If
                If IsDBNull(DataReader("appointdate")) = False Then
                    d1 = Format(DataReader("appointdate"), "dd/MM/yyyy")
                End If
            End While
        End If
        DataReader.Close()

        If value1 < 50 Then
            Conn.Close()
            Exit Sub
        End If



        'หา TSR ที่ Case น้อยที่สุดเพื่อโอนให้
        Dim assignto As String
        str = "SELECT TOP 1 Assignto, COUNT(*) AS Expr1 From TblAssignCallection Where (statuscase = 0) and assignto in (select userid from tbluser where userlevelid = 11 and userstatus = 1 ) GROUP BY Assignto ORDER BY COUNT(*) "

        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader

        If DataReader.HasRows Then
            While DataReader.Read
                If IsDBNull(DataReader("Assignto")) = False Then
                    assignto = DataReader("Assignto")
                End If
            End While
        End If
        DataReader.Close()


        'ส่ง Case ให้ทวงหนี้
        str = "insert into tblassigncallection(Appid,idcar,cusid, Payno, Appointdate, Assignto, CallAt,totalpay,caseTYPE) " + _
               " values('" & appidx & "','" & tcar & "','" & tcus & "',6,'" & ConvertDate.SetISODate("en", d1) & "','" & assignto & "',1," & value1 & ",1)"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        'เก็บ log
        str = "insert into TblLogass_PaidDIFF( cusID, idCAR, appID, payID, totalPAY, paidIN, createID, createDATE, addBY) " + _
        "values ('" & tcus & "','" & tcar & "','" & appidx & "',6," & Tpay & "," & value1 & ",'" & Request.Cookies("UserID").Value & "', getdate(),'โอนเงิน Auto')"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        Conn.Close()
    End Sub

    Private Sub TakePhoto(ByVal TakeApp As String)
        Dim Phototime As String = ""
        Dim str As String
        Dim Command As SqlCommand
        Dim DataReader As SqlDataReader

        dt = New DataTable

        dt.Columns.Add("cusid")
        dt.Columns.Add("idcar")
        dt.Columns.Add("tid")
        dt.Columns.Add("name")
        dt.Columns.Add("mobile")
        dt.Columns.Add("dist")
        dt.Columns.Add("subdist")
        dt.Columns.Add("province")

        If Conn.State = ConnectionState.Open Then
            Conn.Close()
        End If

        Conn.Open()

        str = " SELECT TblCustomer.CusID, TblCar.IdCar, TblCar.DataID, TblCar.productID, TblApplication.ProDuctID AS Proapp, TblRiderArea.tID, TblRiderArea.AreaStatus, " + _
                              " TblCustomer.province , TblCustomer.Dist, TblCustomer.SubDist ,fnameth + ' ' + lnameth as name ,mobile" + _
        " FROM TblCustomer INNER JOIN " + _
                              " TblCar ON TblCustomer.CusID = TblCar.CusID INNER JOIN " + _
                              " TblApplication ON TblCar.IdCar = TblApplication.Idcar INNER JOIN " + _
                               " TblRiderArea ON TblCustomer.Province = TblRiderArea.Province AND TblCustomer.SubDist = TblRiderArea.SubDist AND " + _
                              " TblCustomer.Dist = TblRiderArea.Dist " + _
        " WHERE (TblApplication.ProDuctID IN (10, 20, 13, 18, 15)) AND (TblApplication.Typeprovalue = 1) AND " + _
                              " (TblRiderArea.AreaStatus = 1)  and appid = '" & TakeApp & "'"
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader()

        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt.NewRow
                If IsDBNull(DataReader("cusid")) = False Then
                    dtr("cusid") = DataReader("cusid")
                End If
                If IsDBNull(DataReader("idcar")) = False Then
                    dtr("idcar") = DataReader("idcar")
                End If
                If IsDBNull(DataReader("tid")) = False Then
                    dtr("tid") = DataReader("tid")
                End If
                If IsDBNull(DataReader("name")) = False Then
                    dtr("name") = DataReader("name")
                End If
                If IsDBNull(DataReader("mobile")) = False Then
                    dtr("mobile") = DataReader("mobile")
                End If
                If IsDBNull(DataReader("dist")) = False Then
                    dtr("dist") = DataReader("dist")
                End If
                If IsDBNull(DataReader("subdist")) = False Then
                    dtr("subdist") = DataReader("subdist")
                End If
                If IsDBNull(DataReader("province")) = False Then
                    dtr("province") = DataReader("province")
                End If
                dt.Rows.Add(dtr)
            End While
        End If
        DataReader.Close()

        If dt.Rows.Count > 0 Then

            'str = "insert into tbltakephoto (CusID, idCAR, appID, phID, tID, appointDATE, appointCOMMENT, mainDEST, Destination, createID, createDATE,provinceDEST) values " + _
            '" ( '" & dt.Rows(0).Item("cusid") & "','" & dt.Rows(0).Item("idcar") & "','" & TakeApp & "',2,'" & dt.Rows(0).Item("tid") & "',getdate(),'" & dt.Rows(0).Item("name") + " " + dt.Rows(0).Item("mobile") + " " + Phototime & "' , '" & dt.Rows(0).Item("dist") & "','" & dt.Rows(0).Item("subdist") & "','" & Request.Cookies("UserID").Value & "',getdate(),'" & dt.Rows(0).Item("province") & "')  "
            'Command = New SqlCommand(str, Conn)
            'Command.ExecuteNonQuery()

            'str = "update tblapplication set takephoto = 1 where  appid = '" & TakeApp & "' "
            'Command = New SqlCommand(str, Conn)
            'Command.ExecuteNonQuery()

        End If

        Conn.Close()
    End Sub

    Private Sub Print_CV_Payment()

        Dim repNAME As String
        Dim Appbar As String
        Dim str As String
        Dim Command As SqlCommand
        Dim DataReader As SqlDataReader

        Dim users As String = "sa"
        Dim pass As String = "asn@sr1"

        'print CV

        SetappAcc()

        Conn.Open()

        'Gen QR_Codr and Barcode use on CV version 2020
        Dim dttmprefno As New DataTable
        Dim query = New System.Text.StringBuilder()
        query.Append(" select tblapplication.appid,tblcar.RefNo")
        query.Append(" from tblapplication ")
        query.Append(" inner join tblcar on tblapplication.idcar=tblcar.idcar")
        query.Append(" where tblapplication.appid='" & lblApp.Text & "'")



        dttmprefno = DataAccess.DataRead(query.ToString)
        If dttmprefno.Rows.Count = 1 Then
            Dim refno As String = dttmprefno.Rows(0)("RefNo").ToString()
            Dim appid As String = dttmprefno.Rows(0)("appid").ToString()

            Dim barcodestr As String = "|010755800027000" & Chr(13) & refno & Chr(13) & appid & Chr(13) & "0"
            Dim myimgQRCode As Image = Code128Rendering.MakeBarcodeImage(barcodestr, 2, True)
            Dim QRCode As MessagingToolkit.QRCode.Codec.QRCodeEncoder = New MessagingToolkit.QRCode.Codec.QRCodeEncoder
            Dim myimgbarcode As Image = QRCode.Encode(barcodestr)
            Dim CreateFolder1 As String = "D:\LINE\" + lblApp.Text
            CreateFolder(CreateFolder1)

            'save barcode
            Dim destPathbarcode As String = "D:\\LINE\\" + lblApp.Text + "\barcode.bmp"
            Dim bmbarcode As New Bitmap(myimgQRCode)
            bmbarcode.Save(destPathbarcode)
            bmbarcode.Dispose()

            'save barCode
            Dim destPathqrCode As String = "D:\\LINE\\" + lblApp.Text + "\qrcode.bmp"
            Dim bmqrCode As New Bitmap(myimgbarcode)
            bmqrCode.Save(destPathqrCode)
            bmqrCode.Dispose()
        End If



        str = "select  ProDuctID,dateadd(year,542,protectdate) as expProtectDate,createid ,senddoc from tblapplication where appid = '" & lblApp.Text & "'"
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader()
        dt = New DataTable

        dt.Columns.Add("createid")
        dt.Columns.Add("senddoc")

        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt.NewRow

                If IsDBNull(DataReader("createid")) = False Then
                    dtr("createid") = DataReader("createid")
                End If
                If IsDBNull(DataReader("senddoc")) = False Then
                    dtr("senddoc") = DataReader("senddoc")
                Else
                    dtr("senddoc") = 0
                End If
                dt.Rows.Add(dtr)
            End While
        End If
        DataReader.Close()

        Dim rptx1 As String = ""
        Appbar = lblApp.Text


        rptx1 = "*" & Trim(Appbar) & "*"
        Dim tsrid As String = "TSR NO. : " + dt.Rows(0).Item("createid")

        str = "INSERT INTO TblAutoPrint (appid,[type],flag,payno,createid) VALUES('" & lblApp.Text & "','1','0','0'," & Request.Cookies("UserID").Value & ")"

        Command = New SqlCommand(str, Conn)
        Dim chk As Integer = Command.ExecuteNonQuery()

        Dim reportname As String = Server.MapPath("acv_lm2.rpt")
        myReport.Load(reportname)
        myReport.SetDatabaseLogon(users, pass)

        myReport.SetParameterValue("bcode", rptx1)
        myReport.SetParameterValue("tsrid", tsrid)
        myReport.SetParameterValue("pd1", Session("pd1"))

        myReport.SetParameterValue("UserID", Request.Cookies("UserID").Value)
        

        myReport.PrintOptions.PrinterName = "cvRPT" '"cvRPT"
        myReport.PrintToPrinter(1, False, 1, 1)

        

        Conn.Close()

        ScriptManager.RegisterStartupScript(Page, Page.GetType, "Message", "alert('ปล่อยเอกสารผ่านเรียบร้อย !!');", True)

        'ShowData(1)

        'ClearData()



    End Sub

    Private Sub SetappAcc()

        Dim tmpdate1 As String
        Dim Count As Integer = 0
        Dim str As String
        Dim Command As SqlCommand
        Dim DataReader As SqlDataReader
        Dim reportX, ex1 As String
        reportX = "1"
        dt = New DataTable

        dt.Columns.Add("0")
        dt.Columns.Add("1")
        dt.Columns.Add("2")
        dt.Columns.Add("3")
        dt.Columns.Add("4")
        dt.Columns.Add("5")
        dt.Columns.Add("6")
        dt.Columns.Add("7")
        dt.Columns.Add("8")
        dt.Columns.Add("9")
        dt.Columns.Add("10")
        dt.Columns.Add("11")
        dt.Columns.Add("12")
        dt.Columns.Add("13")
        dt.Columns.Add("14")
        dt.Columns.Add("15")
        dt.Columns.Add("16")
        dt.Columns.Add("17")
        dt.Columns.Add("18")
        dt.Columns.Add("19")
        dt.Columns.Add("20")
        dt.Columns.Add("21")
        dt.Columns.Add("22")
        dt.Columns.Add("23")
        dt.Columns.Add("24")
        dt.Columns.Add("25")
        dt.Columns.Add("26")
        dt.Columns.Add("27")
        dt.Columns.Add("28")
        dt.Columns.Add("29")
        dt.Columns.Add("30")
        dt.Columns.Add("31")
        dt.Columns.Add("32")
        dt.Columns.Add("33")
        dt.Columns.Add("34")
        dt.Columns.Add("35")
        dt.Columns.Add("36")
        dt.Columns.Add("37")
        dt.Columns.Add("38")
        dt.Columns.Add("39")
        dt.Columns.Add("40")
        dt.Columns.Add("41")
        dt.Columns.Add("42")
        dt.Columns.Add("43")
        dt.Columns.Add("44")
        dt.Columns.Add("45")
        dt.Columns.Add("46")
        dt.Columns.Add("47")
        dt.Columns.Add("48")
        dt.Columns.Add("49")
        dt.Columns.Add("50")
        dt.Columns.Add("51")
        dt.Columns.Add("52")
        dt.Columns.Add("53")
        dt.Columns.Add("54")
        dt.Columns.Add("55")
        dt.Columns.Add("56")
        dt.Columns.Add("57")
        dt.Columns.Add("58")
        dt.Columns.Add("59")
        dt.Columns.Add("60")
        dt.Columns.Add("61")
        dt.Columns.Add("63")
        dt.Columns.Add("64")
        dt.Columns.Add("65")
        dt.Columns.Add("66")
        dt.Columns.Add("67")
        dt.Columns.Add("68")
        dt.Columns.Add("69")
        dt.Columns.Add("70")
        dt.Columns.Add("71")
        dt.Columns.Add("72")
        dt.Columns.Add("73")
        dt.Columns.Add("74")
        dt.Columns.Add("75")
        dt.Columns.Add("76")
        dt.Columns.Add("77")
        dt.Columns.Add("78")
        dt.Columns.Add("87")
        dt.Columns.Add("88")
        dt.Columns.Add("89")
        dt.Columns.Add("90")
        dt.Columns.Add("91")
        dt.Columns.Add("92")
        dt.Columns.Add("93")
        dt.Columns.Add("94")
        dt.Columns.Add("95")
        dt.Columns.Add("96")
        dt.Columns.Add("expprotectdate")
        dt.Columns.Add("sname")
        dt.Columns.Add("DetDeviceAdd")
        dt.Columns.Add("Old_Insu")
        dt.Columns.Add("Old_PolicyNo")
        dt.Columns.Add("ASNComment")
        dt.Columns.Add("IDCARD")
        dt.Columns.Add("Status")


        Conn.Open()

        If (Format$(Date.Now, "yyyy") > 2300) Then
            tmpdate1 = Format$(Date.Now, "yyyy") - 543 & Format$(Date.Now, "MMdd")
        Else
            tmpdate1 = Format$(Date.Now, "yyyymmdd")
        End If

        str = "delete from  tmp_QC_app01 where UserID = '" & Request.Cookies("UserID").Value & "'"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()
        str = "delete from tmp_QC_PayCredit where UserID = '" & Request.Cookies("UserID").Value & "'"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()
        str = "delete from tmp_QC_app02 where UserID = '" & Request.Cookies("UserID").Value & "'"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        str = "select a.* from (select a.*,b.initth from (select  a.*,ProTypeBrand, Addr + ' ' + b.Road AS a1, b.SubDist + ' ' + b.Dist + ' ' + b.Province + ' ' + b.Zip AS a2, '  ' AS a3 from (select a.*,b.fname,b.lname from (select a.*,b.cartypename  from  (SELECT a.initid,a.FNameTH, a.LNameTH, a.Address, a.SAddress, a.Villege, a.Svillege, " + _
                              "a.Building, a.SBuilding, a.HomeFloor, a.SHomeFloor, a.HomeRoom, " + _
                              "a.SHomeRoom, a.Moo, a.SMoo, a.Soi, a.SSoi, a.Road, a.SRoad, " + _
                              "a.SubDist, a.SSubDist, a.Dist, a.SDist, a.Province, a.SProvince, a.Zip, " + _
                              "a.SZip, b.AssignTo, b.CarDriverNo, b.CarDriver1 + ' ' + b.CarDriverLname1 as  CarDriver1, b.CarDriverBorn1, b.CarDriver2 + ' ' + b.CarDriverLname2 as  CarDriver2, b.CarDriverBorn2, " + _
                             "b.DBornNO1, b.DBornDate1, b.DBornAddr1, b.DBornNO2, b.DBornDate2, b.DBornAddr2, b.CarID, " + _
                             "b.CarBuyDate, b.CarFixIn, b.CarSize, b.CarNo, b.CarBoxNo, b.CarType, b.CarYear, b.CarBrand, " + _
                              "b.CarSeries,c.AppID, c.AppNO, c.ProDuctID, c.ProDuctIDCarpet, c.AppStatus, " + _
                              "c.ProtectDate, c.ProPrice, c.IsProvalue, c.ProValue,c.discounttype,  " + _
                              "c.Typeprovalue, c.IsCarpet, c.CarPet, c.FirstPay, c.YearPay, c.Lost_Life1, " + _
                              "c.Lost_Life2, c.Lost_Prop1, c.Lost_Prop2, c.Lost_Car1, c.Lost_Car2, " + _
                              "c.Car_Fire, c.Acc_Lost1, c.Acc_Lost2, c.Acc_Lost3, c.Acc_Lost4, " + _
                              "c.Maintain, c.Insure, c.Apprela, c.Pledge, c.PolicyNO, " + _
                              "c.PolicyDate, c.SendPolicyDate, a.sname, c.expprotectdate,  " + _
                              "c.CarPetNO , c.CarPetDate,c.successdate as createdate,c.appcomment as ASNcomt,'" & reportX & "' as typereport " & ex1 & ", c.Comments,c.Old_Insu,c.Old_PolicyNo,c.appcomment,a.IDCard,z.StatusCode " + _
                              "FROM TblCustomer a INNER JOIN " + _
                              "TblCar b ON a.CusID = b.CusID INNER JOIN " + _
                              " TblApplication c ON b.IdCar = c.Idcar  " + _
                              " LEFT JOIN TblStatus z ON b.curstatus = z.StatusID where  appid = '" & lblApp.Text & "'" + _
                              " ) a inner join  Tbl_Cartype b  on a.cartype =  b.cartypeid) a inner join tbluser b on a.assignto = b.userid ) a inner join Tbl_ProductType b on a.productid = b.protypeid )  a  inner join TblCustomerInit b  on a.initid = b.initid) a where a.appid not in (SELECT distinct appid  From TblAppDoc Where  Docid = 99 ) order by fnameth,lnameth "

        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader()
        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt.NewRow
                If IsDBNull(DataReader(0)) = False Then
                    dtr("0") = DataReader(0)
                End If
                If IsDBNull(DataReader(1)) = False Then
                    dtr("1") = DataReader(1)
                End If
                If IsDBNull(DataReader(2)) = False Then
                    dtr("2") = DataReader(2)
                End If
                If IsDBNull(DataReader(3)) = False Then
                    dtr("3") = DataReader(3)
                End If
                If IsDBNull(DataReader(4)) = False Then
                    dtr("4") = DataReader(4)
                End If
                If IsDBNull(DataReader(5)) = False Then
                    dtr("5") = DataReader(5)
                End If
                If IsDBNull(DataReader(6)) = False Then
                    dtr("6") = DataReader(6)
                End If
                If IsDBNull(DataReader(7)) = False Then
                    dtr("7") = DataReader(7)
                End If
                If IsDBNull(DataReader(8)) = False Then
                    dtr("8") = DataReader(8)
                End If
                If IsDBNull(DataReader(9)) = False Then
                    dtr("9") = DataReader(9)
                End If
                If IsDBNull(DataReader(10)) = False Then
                    dtr("10") = DataReader(10)
                End If
                If IsDBNull(DataReader(11)) = False Then
                    dtr("11") = DataReader(11)
                End If
                If IsDBNull(DataReader(12)) = False Then
                    dtr("12") = DataReader(12)
                End If
                If IsDBNull(DataReader(13)) = False Then
                    dtr("13") = DataReader(13)
                End If
                If IsDBNull(DataReader(14)) = False Then
                    dtr("14") = DataReader(14)
                End If
                If IsDBNull(DataReader(15)) = False Then
                    dtr("15") = DataReader(15)
                End If
                If IsDBNull(DataReader(16)) = False Then
                    dtr("16") = DataReader(16)
                End If
                If IsDBNull(DataReader(17)) = False Then
                    dtr("17") = DataReader(17)
                End If
                If IsDBNull(DataReader(18)) = False Then
                    dtr("18") = DataReader(18)
                End If
                If IsDBNull(DataReader(19)) = False Then
                    dtr("19") = DataReader(19)
                End If
                If IsDBNull(DataReader(20)) = False Then
                    dtr("20") = DataReader(20)
                End If
                If IsDBNull(DataReader(21)) = False Then
                    dtr("21") = DataReader(21)
                End If
                If IsDBNull(DataReader(22)) = False Then
                    dtr("22") = DataReader(22)
                End If
                If IsDBNull(DataReader(23)) = False Then
                    dtr("23") = DataReader(23)
                End If
                If IsDBNull(DataReader(24)) = False Then
                    dtr("24") = DataReader(24)
                End If
                If IsDBNull(DataReader(25)) = False Then
                    dtr("25") = DataReader(25)
                End If
                If IsDBNull(DataReader(26)) = False Then
                    dtr("26") = DataReader(26)
                End If
                If IsDBNull(DataReader(27)) = False Then
                    dtr("27") = DataReader(27)
                End If
                If IsDBNull(DataReader(28)) = False Then
                    dtr("28") = DataReader(28)
                End If
                If IsDBNull(DataReader(29)) = False Then
                    dtr("29") = DataReader(29)
                End If
                If IsDBNull(DataReader(30)) = False Then
                    dtr("30") = Format(DataReader(30), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader(31)) = False Then
                    dtr("31") = DataReader(31)
                End If
                If IsDBNull(DataReader(32)) = False Then
                    dtr("32") = Format(DataReader(32), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader(33)) = False Then
                    dtr("33") = DataReader(33)
                End If
                If IsDBNull(DataReader(34)) = False Then
                    dtr("34") = Format(DataReader(34), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader(35)) = False Then
                    dtr("35") = DataReader(35)
                End If
                If IsDBNull(DataReader(36)) = False Then
                    dtr("36") = DataReader(36)
                End If
                If IsDBNull(DataReader(37)) = False Then
                    dtr("37") = Format(DataReader(37), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader(38)) = False Then
                    dtr("38") = DataReader(38)
                End If
                If IsDBNull(DataReader(39)) = False Then
                    dtr("39") = DataReader(39)
                End If
                If IsDBNull(DataReader(40)) = False Then
                    dtr("40") = Format(DataReader(40), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader(41)) = False Then
                    dtr("41") = DataReader(41)
                End If
                If IsDBNull(DataReader(42)) = False Then
                    dtr("42") = DataReader(42)
                End If
                If IsDBNull(DataReader(43)) = False Then
                    dtr("43") = DataReader(43)
                End If
                If IsDBNull(DataReader(44)) = False Then
                    dtr("44") = DataReader(44)
                End If
                If IsDBNull(DataReader(45)) = False Then
                    dtr("45") = DataReader(45)
                End If
                If IsDBNull(DataReader(46)) = False Then
                    dtr("46") = DataReader(46)
                End If
                If IsDBNull(DataReader(47)) = False Then
                    dtr("47") = DataReader(47)
                End If
                If IsDBNull(DataReader(48)) = False Then
                    dtr("48") = DataReader(48)
                End If
                If IsDBNull(DataReader(49)) = False Then
                    dtr("49") = DataReader(49)
                End If
                If IsDBNull(DataReader(50)) = False Then
                    dtr("50") = DataReader(50)
                End If
                If IsDBNull(DataReader(51)) = False Then
                    dtr("51") = DataReader(51)
                End If
                If IsDBNull(DataReader(52)) = False Then
                    dtr("52") = DataReader(52)
                End If
                If IsDBNull(DataReader(53)) = False Then
                    dtr("53") = DataReader(53)
                End If
                If IsDBNull(DataReader(54)) = False Then
                    dtr("54") = Format(DataReader(54), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader(55)) = False Then
                    dtr("55") = Format(DataReader(55), "###,###,##0.#0") 'proprice
                End If
                If IsDBNull(DataReader(56)) = False Then 'IsProvalue
                    dtr("56") = DataReader(56)
                End If
                If IsDBNull(DataReader(57)) = False Then
                    dtr("57") = Format(DataReader(57), "###,###,##0.#0") 'เบี้ยประกันภัย (provalue)
                End If
                If IsDBNull(DataReader(58)) = False Then 'discounttype
                    dtr("58") = DataReader(58)
                End If
                If IsDBNull(DataReader(59)) = False Then 'typeProvalue
                    dtr("59") = DataReader(59)
                End If
                If IsDBNull(DataReader(60)) = False Then
                    dtr("60") = DataReader(60) 'IsCarpet
                End If
                If IsDBNull(DataReader(61)) = False Then
                    dtr("61") = Format(DataReader(61), "###,###,##0.#0") 'พรบ.(carpet)
                End If
                If IsDBNull(DataReader(63)) = False Then
                    dtr("63") = Format(DataReader(63), "###,###,##0.#0") ' จ่ายรวม(totalx)
                End If
                If IsDBNull(DataReader(64)) = False Then
                    dtr("64") = Format(DataReader(64), "###,###,##0.#0") 'ความเสียหายต่อชีวิต / คน (Lost_life1)
                End If
                If IsDBNull(DataReader(65)) = False Then
                    dtr("65") = Format(DataReader(65), "###,###,##0.#0") 'ความเสียหายต่อชีวิต / ครั้ง (Lost_Life2)
                End If
                If IsDBNull(DataReader(66)) = False Then
                    dtr("66") = Format(DataReader(66), "###,###,##0.#0") 'ความเสียหายต่อทรัพย์สิน/ครั้ง (Lost_Prop)1)
                End If
                If IsDBNull(DataReader(67)) = False Then
                    dtr("67") = Format(DataReader(67), "###,###,##0.#0") 'Lost_prop2
                End If
                If IsDBNull(DataReader(68)) = False Then
                    dtr("68") = Format(DataReader(68), "###,###,##0.#0") ' Lost_Car1
                End If
                If IsDBNull(DataReader(69)) = False Then
                    dtr("69") = Format(DataReader(69), "###,###,##0.#0") 'Lost_car2
                End If
                If IsDBNull(DataReader(70)) = False Then
                    dtr("70") = Format(DataReader(70), "###,###,##0.#0") 'car_fire
                End If
                If IsDBNull(DataReader(71)) = False Then
                    dtr("71") = DataReader(71) 'acc_lost
                End If
                If IsDBNull(DataReader(72)) = False Then
                    dtr("72") = Format(DataReader(72), "###,###,##0.#0")
                End If
                If IsDBNull(DataReader(73)) = False Then
                    dtr("73") = DataReader(73)
                End If
                If IsDBNull(DataReader(74)) = False Then
                    dtr("74") = Format(DataReader(74), "###,###,##0.#0")
                End If
                If IsDBNull(DataReader(75)) = False Then
                    dtr("75") = Format(DataReader(75), "###,###,##0.#0")
                End If
                If IsDBNull(DataReader(76)) = False Then
                    dtr("76") = Format(DataReader(76), "###,###,##0.#0")
                End If
                If IsDBNull(DataReader(77)) = False Then
                    dtr("77") = DataReader(77)
                End If
                If IsDBNull(DataReader(78)) = False Then
                    dtr("78") = DataReader(78)
                End If
                If IsDBNull(DataReader(87)) = False Then
                    dtr("87") = DataReader(87)
                End If
                If IsDBNull(DataReader(88)) = False Then
                    dtr("88") = DataReader(88)
                End If
                If IsDBNull(DataReader(89)) = False Then
                    dtr("89") = DataReader(89)
                End If
                If IsDBNull(DataReader(90)) = False Then
                    dtr("90") = DataReader(90)
                End If
                If IsDBNull(DataReader(91)) = False Then
                    dtr("91") = DataReader(91)
                End If
                If IsDBNull(DataReader(92)) = False Then
                    dtr("92") = DataReader(92)
                End If
                If IsDBNull(DataReader(93)) = False Then
                    dtr("93") = DataReader(93)
                End If
                If IsDBNull(DataReader(94)) = False Then
                    dtr("94") = DataReader(94)
                End If
                If IsDBNull(DataReader(95)) = False Then
                    dtr("95") = DataReader(95)
                End If
                If IsDBNull(DataReader(96)) = False Then
                    dtr("96") = DataReader(96)
                End If
                If IsDBNull(DataReader("expprotectdate")) = False Then
                    dtr("expprotectdate") = Format(DataReader("expprotectdate"), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader("sname")) = False Then
                    dtr("sname") = DataReader("sname")
                End If
                If IsDBNull(DataReader("Comments")) = False Then
                    dtr("DetDeviceAdd") = DataReader("Comments")
                End If
                If IsDBNull(DataReader("Old_Insu")) = False Then
                    dtr("Old_Insu") = DataReader("Old_Insu")
                End If
                If IsDBNull(DataReader("Old_PolicyNo")) = False Then
                    dtr("Old_PolicyNo") = DataReader("Old_PolicyNo")
                End If
                If IsDBNull(DataReader("appcomment")) = False Then
                    dtr("ASNComment") = DataReader("appcomment")
                End If
                If IsDBNull(DataReader("IDCard")) = False Then
                    dtr("IDCard") = DataReader("IDCard")
                End If
                If IsDBNull(DataReader("StatusCode")) = False Then
                    dtr("Status") = DataReader("StatusCode")
                End If

                dt.Rows.Add(dtr)
            End While
        End If
        DataReader.Close()

        Dim sName As String = dt.Rows(0).Item("sname")
        'sName = Trim(Mid(sName, InStr(1, sName, "คุณ") + 3, 255))
        ' sName = Trim(Replace(sName, "คุณ", "", 1, 255))

        str = "insert into tmp_QC_app01 ( " +
        " initid, FNameTH, LNameTH, Address, SAddress, Villege, Svillege, Building, SBuilding, HomeFloor, SHomeFloor, HomeRoom, SHomeRoom, Moo, " +
        " SMoo, Soi, SSoi, Road, SRoad, SubDist, SSubDist, Dist, SDist, Province, SProvince, Zip, SZip, AssignTo, CarDriverNo, CarDriver1,CarDriverBorn1, " +
        " CarDriver2, CarDriverBorn2, DBornNO1, DBornDate1, DBornAddr1, DBornNO2, DBornDate2, DBornAddr2, CarID, CarBuyDate, CarFixIn, CarSize, " +
        " CarNo, CarBoxNo, CarType, CarYear, CarBrand, CarSeries, AppID, AppNO, ProDuctID, ProDuctIDCarpet, AppStatus, ProtectDate, ProPrice, IsProvalue , " +
        " ProValue, discounttype, Typeprovalue, IsCarpet, CarPet, FirstPay, YearPay, Lost_Life1, Lost_Life2, Lost_Prop1, Lost_Prop2, Lost_Car1, Lost_Car2 ," +
        " Car_Fire, Acc_Lost1, Acc_Lost2, Acc_Lost3, Acc_Lost4, Maintain, Insure, Apprela, Pledge,ASNcomt, typereport,  cartypename, fname, lname, ProTypeBrand,  a1 , a2, a3, initth,expprotectdate,sname,UserID,DetDeviceAdd,Old_Insu,Old_PolicyNo,ASNComment,IDCard,CurStatus ) " +
        " values ( " +
          " '" & dt.Rows(0).Item("0") & "','" & dt.Rows(0).Item("1") & "','" & dt.Rows(0).Item("2") & "','" & dt.Rows(0).Item("3") & "','" & dt.Rows(0).Item("4") & "','" & dt.Rows(0).Item("5") & "','" & dt.Rows(0).Item("6") & "','" & dt.Rows(0).Item("7") & "','" & dt.Rows(0).Item("8") & "','" & dt.Rows(0).Item("9") & "' ," +
        " '" & dt.Rows(0).Item("10") & "','" & dt.Rows(0).Item("11") & "','" & dt.Rows(0).Item("12") & "','" & dt.Rows(0).Item("13") & "','" & dt.Rows(0).Item("14") & "','" & dt.Rows(0).Item("15") & "','" & dt.Rows(0).Item("16") & "','" & dt.Rows(0).Item("17") & "','" & dt.Rows(0).Item("18") & "','" & dt.Rows(0).Item("19") & "', " +
        " '" & dt.Rows(0).Item("20") & "','" & dt.Rows(0).Item("21") & "','" & dt.Rows(0).Item("22") & "','" & dt.Rows(0).Item("23") & "','" & dt.Rows(0).Item("24") & "','" & dt.Rows(0).Item("25") & "','" & dt.Rows(0).Item("26") & "'," & dt.Rows(0).Item("27") & ",'" & dt.Rows(0).Item("28") & "','" & dt.Rows(0).Item("29") & "'," +
        " '" & dt.Rows(0).Item("30") & "','" & dt.Rows(0).Item("31") & "','" & (dt.Rows(0).Item("32")) & "','" & dt.Rows(0).Item("33") & "','" & (dt.Rows(0).Item("34")) & "','" & dt.Rows(0).Item("35") & "','" & dt.Rows(0).Item("36") & "','" & (dt.Rows(0).Item("37")) & "','" & dt.Rows(0).Item("38") & "','" & dt.Rows(0).Item("39") & "' ," +
        " '" & (dt.Rows(0).Item("40")) & "','" & dt.Rows(0).Item("41") & "','" & dt.Rows(0).Item("42") & "','" & dt.Rows(0).Item("43") & "','" & dt.Rows(0).Item("44") & "','" & dt.Rows(0).Item("45") & "','" & dt.Rows(0).Item("46") & "','" & dt.Rows(0).Item("47") & "','" & dt.Rows(0).Item("48") & "','" & dt.Rows(0).Item("49") & "' ," +
        " '" & dt.Rows(0).Item("50") & "','" & dt.Rows(0).Item("51") & "','" & dt.Rows(0).Item("52") & "','" & dt.Rows(0).Item("53") & "','" & (dt.Rows(0).Item("54")) & "','" & dt.Rows(0).Item("55") & "','" & dt.Rows(0).Item("56") & "','" & dt.Rows(0).Item("57") & "','" & dt.Rows(0).Item("58") & "','" & dt.Rows(0).Item("59") & "' ," +
        " '" & dt.Rows(0).Item("60") & "','" & dt.Rows(0).Item("61") & "',0,'" & dt.Rows(0).Item("63") & "','" & dt.Rows(0).Item("64") & "','" & dt.Rows(0).Item("65") & "','" & dt.Rows(0).Item("66") & "','" & dt.Rows(0).Item("67") & "','" & dt.Rows(0).Item("68") & "','" & dt.Rows(0).Item("69") & "' ," +
        " '" & dt.Rows(0).Item("70") & "','" & dt.Rows(0).Item("71") & "','" & dt.Rows(0).Item("72") & "','" & dt.Rows(0).Item("73") & "','" & dt.Rows(0).Item("74") & "','" & dt.Rows(0).Item("75") & "','" & dt.Rows(0).Item("76") & "','" & dt.Rows(0).Item("77") & "','" & dt.Rows(0).Item("78") & "','" & Replace(dt.Rows(0).Item("87"), "'", "") & "' ," +
        " '" & dt.Rows(0).Item("88") & "','" & Replace(dt.Rows(0).Item("89"), "'", "") & "','" & dt.Rows(0).Item("90") & "','" & dt.Rows(0).Item("91") & "','" & Replace(dt.Rows(0).Item("92"), "'", "") & "','" & dt.Rows(0).Item("93") & "','" & dt.Rows(0).Item("94") & "','" & dt.Rows(0).Item("95") & "','" & dt.Rows(0).Item("96") & "'," +
        "'" & dt.Rows(0).Item("expprotectdate") & "','" & sName & "','" & Request.Cookies("UserID").Value & "','" & Replace(dt.Rows(0).Item("DetDeviceAdd"), "'", "") & "','" & dt.Rows(0).Item("Old_Insu") & "','" & dt.Rows(0).Item("Old_PolicyNo") & "','" & Replace(dt.Rows(0).Item("ASNComment"), "'", "") & "','" & dt.Rows(0).Item("IDCard") & "','" & dt.Rows(0).Item("Status") & "') "

        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        '''''''''''''''''''
        'Call SetCodePrint()


        str = "select  m.*,a.*,b.*,c.*,d.*, e.*,f.*  from  (select a.*,b.initth   from (select  a.*,ProTypeBrand, Addr + ' ' + b.Road AS a1, b.SubDist + ' ' + b.Dist + ' ' + b.Province + ' ' + b.Zip AS a2, 'â·Ã : ' + b.Tel + '  ' AS a3 from (select a.*,b.fname,b.lname from (select a.*,b.cartypename  from  (SELECT      " + _
                        "c.AppID,b.CarType,b.assignto,c.ProDuctID,a.initid," + _
                        "c.CarPetNO , c.CarPetDate,c.successdate as createdate,c.appcomment as ASNcomt,'" & reportX & "' as typereport " & ex1 + _
              "FROM TblCustomer a INNER JOIN " + _
                        "TblCar b ON a.CusID = b.CusID INNER JOIN " + _
                      " TblApplication c ON b.IdCar = c.Idcar  where appid = '" & lblApp.Text & "'" + _
  " ) a inner join  Tbl_Cartype b  on a.cartype =  b.cartypeid) a inner join tbluser b on a.assignto = b.userid ) a inner join Tbl_ProductType b on a.productid = b.protypeid )  a  inner join TblCustomerInit b  on a.initid = b.initid ) m left join " + _
   " (select appid appid1 ,Typepay Typepay1,convert(int,payid) payid1,AppointDate AppointDate1,totalpay totalpay1 from tblapppay where payid = 1) a on m.appid = a.appid1 left join " + _
      " (select appid appid2 ,Typepay Typepay2,convert(int,payid) payid2,AppointDate AppointDate2,totalpay totalpay2 from tblapppay where payid = 2) b on m.appid = b.appid2 left join " + _
      " (select appid appid3 ,Typepay Typepay3,convert(int,payid) payid3,AppointDate AppointDate3,totalpay totalpay3 from tblapppay where payid = 3) c on m.appid = c.appid3 left join " + _
      " (select appid appid4 ,Typepay Typepay4,convert(int,payid) payid4,AppointDate AppointDate4,totalpay totalpay4 from tblapppay where payid = 4) d on m.appid = d.appid4  left join " + _
          " (select appid appid5 ,Typepay Typepay5,convert(int,payid) payid5,AppointDate AppointDate5,totalpay totalpay5 from tblapppay where payid = 5) e on m.appid = e.appid5 left join " + _
      " (select appid appid6 ,Typepay Typepay6,convert(int,payid) payid6,AppointDate AppointDate6,totalpay totalpay6 from tblapppay where payid = 6) f on m.appid = f.appid6 "
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader()
        dt2 = New DataTable

        dt2.Columns.Add("0")
        dt2.Columns.Add("1")
        dt2.Columns.Add("2")
        dt2.Columns.Add("3")
        dt2.Columns.Add("4")
        dt2.Columns.Add("5")
        dt2.Columns.Add("7")
        dt2.Columns.Add("8")
        dt2.Columns.Add("9")
        dt2.Columns.Add("10")
        dt2.Columns.Add("11")
        dt2.Columns.Add("12")
        dt2.Columns.Add("13")
        dt2.Columns.Add("14")
        dt2.Columns.Add("15")
        dt2.Columns.Add("16")
        dt2.Columns.Add("17")
        dt2.Columns.Add("18")
        dt2.Columns.Add("19")
        dt2.Columns.Add("20")
        dt2.Columns.Add("22")
        dt2.Columns.Add("23")
        dt2.Columns.Add("24")
        dt2.Columns.Add("25")
        dt2.Columns.Add("27")
        dt2.Columns.Add("28")
        dt2.Columns.Add("29")
        dt2.Columns.Add("30")
        dt2.Columns.Add("32")
        dt2.Columns.Add("33")
        dt2.Columns.Add("34")
        dt2.Columns.Add("35")
        dt2.Columns.Add("37")
        dt2.Columns.Add("38")
        dt2.Columns.Add("39")
        dt2.Columns.Add("40")
        dt2.Columns.Add("42")
        dt2.Columns.Add("43")
        dt2.Columns.Add("44")
        dt2.Columns.Add("45")
        dt2.Columns.Add("47")
        dt2.Columns.Add("AppointDate1")
        dt2.Columns.Add("AppointDate2")
        dt2.Columns.Add("AppointDate3")
        dt2.Columns.Add("AppointDate4")
        dt2.Columns.Add("AppointDate5")
        dt2.Columns.Add("AppointDate6")

        Dim d1, d2, d3, d4, d5, d6 As String

        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt2.NewRow
                If IsDBNull(DataReader(0)) = False Then
                    dtr("0") = DataReader(0)
                End If
                If IsDBNull(DataReader(1)) = False Then
                    dtr("1") = DataReader(1)
                End If
                If IsDBNull(DataReader(2)) = False Then
                    dtr("2") = DataReader(2)
                End If
                If IsDBNull(DataReader(3)) = False Then
                    dtr("3") = DataReader(3)
                End If
                If IsDBNull(DataReader(4)) = False Then
                    dtr("4") = DataReader(4)
                End If
                If IsDBNull(DataReader(5)) = False Then
                    dtr("5") = DataReader(5)
                End If

                If IsDBNull(DataReader(7)) = False Then
                    dtr("7") = Format(DataReader(7), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader(8)) = False Then
                    dtr("8") = DataReader(8)
                End If
                If IsDBNull(DataReader(9)) = False Then
                    dtr("9") = DataReader(9)
                End If
                If IsDBNull(DataReader(10)) = False Then
                    dtr("10") = DataReader(10)
                End If
                If IsDBNull(DataReader(11)) = False Then
                    dtr("11") = DataReader(11)
                End If
                If IsDBNull(DataReader(12)) = False Then
                    dtr("12") = DataReader(12)
                End If
                If IsDBNull(DataReader(13)) = False Then
                    dtr("13") = DataReader(13)
                End If
                If IsDBNull(DataReader(14)) = False Then
                    dtr("14") = DataReader(14)
                End If
                If IsDBNull(DataReader(15)) = False Then
                    dtr("15") = DataReader(15)
                End If
                If IsDBNull(DataReader(16)) = False Then
                    dtr("16") = DataReader(16)
                End If
                If IsDBNull(DataReader(17)) = False Then
                    dtr("17") = DataReader(17)
                End If
                If IsDBNull(DataReader(18)) = False Then
                    dtr("18") = DataReader(18)
                End If
                If IsDBNull(DataReader(19)) = False Then
                    dtr("19") = DataReader(19)
                End If
                If IsDBNull(DataReader(20)) = False Then
                    dtr("20") = DataReader(20)
                End If

                If IsDBNull(DataReader(22)) = False Then
                    dtr("22") = DataReader(22)
                End If
                If IsDBNull(DataReader(23)) = False Then
                    dtr("23") = DataReader(23)
                End If
                If IsDBNull(DataReader(24)) = False Then
                    dtr("24") = DataReader(24)
                End If
                If IsDBNull(DataReader(25)) = False Then
                    dtr("25") = DataReader(25)
                End If

                If IsDBNull(DataReader(27)) = False Then
                    dtr("27") = DataReader(27)
                End If
                If IsDBNull(DataReader(28)) = False Then
                    dtr("28") = DataReader(28)
                End If
                If IsDBNull(DataReader(29)) = False Then
                    dtr("29") = DataReader(29)
                End If
                If IsDBNull(DataReader(30)) = False Then
                    dtr("30") = DataReader(30)
                End If

                If IsDBNull(DataReader(32)) = False Then
                    dtr("32") = DataReader(32)
                End If
                If IsDBNull(DataReader(33)) = False Then
                    dtr("33") = DataReader(33)
                End If
                If IsDBNull(DataReader(34)) = False Then
                    dtr("34") = DataReader(34)
                End If
                If IsDBNull(DataReader(35)) = False Then
                    dtr("35") = DataReader(35)
                End If

                If IsDBNull(DataReader(37)) = False Then
                    dtr("37") = DataReader(37)
                End If
                If IsDBNull(DataReader(38)) = False Then
                    dtr("38") = DataReader(38)
                End If
                If IsDBNull(DataReader(39)) = False Then
                    dtr("39") = DataReader(39)
                End If
                If IsDBNull(DataReader(40)) = False Then
                    dtr("40") = DataReader(40)
                End If

                If IsDBNull(DataReader(42)) = False Then
                    dtr("42") = DataReader(42)
                End If
                If IsDBNull(DataReader(43)) = False Then
                    dtr("43") = DataReader(43)
                End If
                If IsDBNull(DataReader(44)) = False Then
                    dtr("44") = DataReader(44)
                End If
                If IsDBNull(DataReader(45)) = False Then
                    dtr("45") = DataReader(45)
                End If

                If IsDBNull(DataReader(47)) = False Then
                    dtr("47") = DataReader(47)
                End If
                dt2.Rows.Add(dtr)
                '-----------------------------------------------------------------------------------------
                '--------------------------------------------------------------------------------
                If IsDBNull(DataReader("AppointDate1")) = True Then
                    d1 = "  "
                Else
                    d1 = Format(DataReader("AppointDate1"), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader("AppointDate2")) = True Then
                    d2 = "  "
                Else
                    d2 = Format(DataReader("AppointDate2"), "dd/MM/yyyy")
                End If

                If IsDBNull(DataReader("AppointDate3")) = True Then
                    d3 = "  "
                Else
                    d3 = Format(DataReader("AppointDate3"), "dd/MM/yyyy")
                End If

                If IsDBNull(DataReader("AppointDate4")) = True Then
                    d4 = "  "
                Else
                    d4 = Format(DataReader("AppointDate4"), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader("AppointDate5")) = True Then
                    d5 = "  "
                Else
                    d5 = Format(DataReader("AppointDate5"), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader("AppointDate6")) = True Then
                    d6 = "  "
                Else
                    d6 = Format(DataReader("AppointDate6"), "dd/MM/yyyy")
                End If

            End While
        End If
        DataReader.Close()

        str = " insert  into  tmp_QC_PayCredit (" +
         " AppID, CarType, assignto, ProDuctID, initid, CarPetNO, createdate, ASNcomt, typereport, cartypename, " +
        " fname, lname, ProTypeBrand, a1, a2, a3, initth, appid1, Typepay1, payid1, AppointDate1, totalpay1, appid2, Typepay2, payid2, AppointDate2, totalpay2, " +
        " appid3 , Typepay3, payid3, AppointDate3, totalpay3, appid4, Typepay4, payid4, AppointDate4, totalpay4 , appid5, Typepay5, payid5, AppointDate5, totalpay5, appid6, Typepay6, payid6, AppointDate6, totalpay6,UserID  )" +
        " values ( " +
          " '" & dt2.Rows(0).Item("0") & "','" & dt2.Rows(0).Item("1") & "','" & dt2.Rows(0).Item("2") & "','" & dt2.Rows(0).Item("3") & "','" & dt2.Rows(0).Item("4") & "','" & dt2.Rows(0).Item("5") & "','" & (dt2.Rows(0).Item("7")) & "','" & Replace(dt2.Rows(0).Item("8"), "'", "") & "','" & dt2.Rows(0).Item("9") & "' ," +
        " '" & dt2.Rows(0).Item("10") & "','" & dt2.Rows(0).Item("11") & "','" & dt2.Rows(0).Item("12") & "','" & dt2.Rows(0).Item("13") & "','" & dt2.Rows(0).Item("14") & "','" & dt2.Rows(0).Item("15") & "','" & dt2.Rows(0).Item("16") & "','" & dt2.Rows(0).Item("17") & "','" & FunAll.chkNull(dt2.Rows(0).Item("18"), 0) & "','" & FunAll.chkNull(dt2.Rows(0).Item("19"), 0) & "', " +
        " '" & FunAll.chkNull(dt2.Rows(0).Item("20"), 0) & "','" & d1 & "','" & FunAll.chkNull(dt2.Rows(0).Item("22"), 1) & "','" & FunAll.chkNull(dt2.Rows(0).Item("23"), 0) & "','" & FunAll.chkNull(dt2.Rows(0).Item("24"), 0) & "','" & FunAll.chkNull(dt2.Rows(0).Item("25"), 0) & "','" & d2 & "','" & FunAll.chkNull(dt2.Rows(0).Item("27"), 1) & "','" & FunAll.chkNull(dt2.Rows(0).Item("28"), 0) & "','" & FunAll.chkNull(dt2.Rows(0).Item("29"), 0) & "', " +
        " '" & FunAll.chkNull(dt2.Rows(0).Item("30"), 0) & "','" & d3 & "','" & FunAll.chkNull(dt2.Rows(0).Item("32"), 1) & "','" & FunAll.chkNull(dt2.Rows(0).Item("33"), 0) & "','" & FunAll.chkNull(dt2.Rows(0).Item("34"), 0) & "','" & FunAll.chkNull(dt2.Rows(0).Item("35"), 0) & "','" & d4 & "','" & FunAll.chkNull(dt2.Rows(0).Item("37"), 1) & "' ,'" & FunAll.chkNull(dt2.Rows(0).Item("38"), 0) & "','" & FunAll.chkNull(dt2.Rows(0).Item("39"), 0) & "'," +
        " '" & FunAll.chkNull(dt2.Rows(0).Item("40"), 0) & "','" & d5 & "','" & FunAll.chkNull(dt2.Rows(0).Item("42"), 1) & "' ,'" & FunAll.chkNull(dt2.Rows(0).Item("43"), 0) & "','" & FunAll.chkNull(dt2.Rows(0).Item("44"), 0) & "','" & FunAll.chkNull(dt2.Rows(0).Item("45"), 0) & "','" & d6 & "','" & FunAll.chkNull(dt2.Rows(0).Item("47"), 1) & "','" & Request.Cookies("UserID").Value & "' ) "
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()


        Dim pd1 As String = ""
        str = " SELECT TblApplication.AppID, TblCar.IdCard1, TblCar.TypeCard1, TblCar.IdCard2, TblCar.TypeCard2, TblCustomer.AddressRemark, ProtectDateCarpet, expProtectDateCarpet, " + _
                             " ' ' AS tmp3, ' ' AS tmp4, ' ' AS tmp5 , ' ' AS tmp6, ' ' AS tmp7, ' ' AS tmp8,iscarpet " + _
      "    FROM TblCustomer INNER JOIN " + _
                            " TblCar ON TblCustomer.CusID = TblCar.CusID INNER JOIN " + _
                            " TblApplication ON TblCar.IdCar = TblApplication.Idcar where  appid = '" & lblApp.Text & "'"
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader()
        dt3 = New DataTable

        dt3.Columns.Add("0")
        dt3.Columns.Add("1")
        dt3.Columns.Add("2")
        dt3.Columns.Add("3")
        dt3.Columns.Add("4")
        dt3.Columns.Add("5")
        dt3.Columns.Add("6")
        dt3.Columns.Add("7")
        dt3.Columns.Add("8")
        dt3.Columns.Add("9")
        dt3.Columns.Add("10")
        dt3.Columns.Add("11")
        dt3.Columns.Add("12")
        dt3.Columns.Add("13")
        dt3.Columns.Add("iscarpet")
        dt3.Columns.Add("ProtectDateCarpet")
        dt3.Columns.Add("expProtectDateCarpet")

        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt3.NewRow
                If IsDBNull(DataReader(0)) = False Then
                    dtr("0") = DataReader(0)
                End If
                If IsDBNull(DataReader(1)) = False Then
                    dtr("1") = DataReader(1)
                End If
                If IsDBNull(DataReader(2)) = False Then
                    dtr("2") = DataReader(2)
                End If
                If IsDBNull(DataReader(3)) = False Then
                    dtr("3") = DataReader(3)
                End If
                If IsDBNull(DataReader(4)) = False Then
                    dtr("4") = DataReader(4)
                End If
                If IsDBNull(DataReader(5)) = False Then
                    dtr("5") = DataReader(5)
                End If
                If IsDBNull(DataReader(6)) = False Then
                    dtr("6") = Format(DataReader(6), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader(7)) = False Then
                    dtr("7") = Format(DataReader(7), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader(8)) = False Then
                    dtr("8") = DataReader(8)
                End If
                If IsDBNull(DataReader(9)) = False Then
                    dtr("9") = DataReader(9)
                End If
                If IsDBNull(DataReader(10)) = False Then
                    dtr("10") = DataReader(10)
                End If
                If IsDBNull(DataReader(11)) = False Then
                    dtr("11") = DataReader(11)
                End If
                If IsDBNull(DataReader(12)) = False Then
                    dtr("12") = DataReader(12)
                End If
                If IsDBNull(DataReader(13)) = False Then
                    dtr("13") = DataReader(13)
                End If
                If IsDBNull(DataReader("iscarpet")) = False Then
                    dtr("iscarpet") = DataReader("iscarpet")
                End If
                If IsDBNull(DataReader("ProtectDateCarpet")) = False Then
                    dtr("ProtectDateCarpet") = Format(DataReader("ProtectDateCarpet"), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader("expProtectDateCarpet")) = False Then
                    dtr("expProtectDateCarpet") = Format(DataReader("expProtectDateCarpet"), "dd/MM/yyyy")
                End If
                dt3.Rows.Add(dtr)
            End While
        End If
        DataReader.Close()

        If dt3.Rows(0).Item("iscarpet") = 1 Then
            Session("pd2") = "ระยะเวลา พรบ.:  " + dt3.Rows(0).Item("ProtectDateCarpet") + " - " + dt3.Rows(0).Item("expProtectDateCarpet")
            Session("pd1") = dt3.Rows(0).Item("ProtectDateCarpet") + " - " + dt3.Rows(0).Item("expProtectDateCarpet")
        Else
            Session("pd2") = "ระยะเวลา พรบ.: - "
            Session("pd1") = "-"
        End If

        str = "insert into tmp_QC_app02 ( AppID, IdCard1, TypeCard1, IdCard2, TypeCard2, AddressRemark, tmp1, tmp2, tmp3, tmp4, tmp5, tmp6, tmp7, tmp8,UserID) values ( " + _
        " '" & dt3.Rows(0).Item("0") & "','" & dt3.Rows(0).Item("1") & "','" & dt3.Rows(0).Item("2") & "','" & dt3.Rows(0).Item("3") & "','" & dt3.Rows(0).Item("4") & "','" & dt3.Rows(0).Item("5") & "','" & dt3.Rows(0).Item("6") & "','" & dt3.Rows(0).Item("7") & "','" & dt3.Rows(0).Item("8") & "' ," + _
       " '" & dt3.Rows(0).Item("9") & "','" & dt3.Rows(0).Item("10") & "','" & dt3.Rows(0).Item("11") & "','" & dt3.Rows(0).Item("12") & "','" & dt3.Rows(0).Item("13") & "','" & Request.Cookies("UserID").Value & "')"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        str = "select  distinct payid from tblapppay where appid = '" & lblApp.Text & "'"
        Dim Count2 As Integer = 0
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader()
        If DataReader.HasRows Then
            While DataReader.Read
                Count2 += 1
            End While
        End If
        DataReader.Close()

        str = "update  tmp_QC_PayCredit set  asncomt = '" & Count2 & "' WHERE UserID = '" & Request.Cookies("UserID").Value & "'"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        str = " SELECT TblCustomer.Sname " + _
              " FROM TblCar INNER JOIN " + _
              " TblApplication TblApplication_1 ON TblCar.IdCar = TblApplication_1.Idcar INNER JOIN " + _
              " TblCustomer ON TblCar.CusID = TblCustomer.CusID " + _
              " where appid = '" & lblApp.Text & "'"
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader()
        dt4 = New DataTable
        dt4.Columns.Add("sName")

        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt4.NewRow
                If IsDBNull(DataReader("sName")) = False Then
                    dtr("sName") = DataReader("sName")
                End If
                dt4.Rows.Add(dtr)
            End While
        End If
        DataReader.Close()

        str = "update  tmp_QC_PayCredit set  a1 = '" & dt4.Rows(0).Item("sName") & "' WHERE UserID = '" & Request.Cookies("UserID").Value & "'"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        Conn.Close()
    End Sub

    Private Sub SetCodePrint()

        Dim tmpdate1, tmpdate2 As String
        Dim Codex As String

        Dim Count As Integer = 0
        Dim str As String
        Dim Command As SqlCommand
        Dim DataReader As SqlDataReader




        Conn.Open()

        If (Format$(Date.Now, "yyyy") > 2300) Then
            tmpdate1 = Format$(Date.Now, "yyyy") - 543 & Format$(Date.Now, "MMdd")
        Else
            tmpdate1 = Format$(Date.Now, "yyyymmdd")
        End If
        '''''gencode

        '''''''''''

        str = "select distinct appid from tmp_QC_app01 WHERE UserID = '" & Request.Cookies("UserID").Value & "'"
        Command = New SqlCommand(str, Conn)

        dt = New DataTable
        dt.Columns.Add("appid")

        DataReader = Command.ExecuteReader()
        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt.NewRow
                If IsDBNull(DataReader("appid")) = False Then
                    dtr("appid") = DataReader("appid")
                End If
                dt.Rows.Add(dtr)
            End While
        End If
        DataReader.Close()

        For i = 0 To dt.Rows.Count - 1
            Count = 0

            str = "select * from tblprint where appid = '" & dt.Rows(i).Item("appid") & "' and createdate = '" & tmpdate1 & "'  "
            Command = New SqlCommand(str, Conn)
            DataReader = Command.ExecuteReader()

            If DataReader.HasRows Then
                While DataReader.Read
                    Count += 1
                End While
            End If
            DataReader.Close()

            If Count = 0 Then
                str = "select top 1 * from tblprint  where createdate = '" & tmpdate1 & "'  order by runno desc "
                Command = New SqlCommand(str, Conn)
                DataReader = Command.ExecuteReader()

                dt2 = New DataTable
                dt2.Columns.Add("codeprint")

                If DataReader.HasRows Then
                    While DataReader.Read
                        Dim dtr As DataRow = dt2.NewRow
                        If IsDBNull(DataReader("codeprint")) = False Then
                            dtr("codeprint") = DataReader("codeprint")
                        End If
                        dt2.Rows.Add(dtr)
                    End While
                End If
                DataReader.Close()

                If dt2.Rows.Count = 0 Then
                    Codex = "0001"
                Else
                    Codex = Format(Val(dt2.Rows(0).Item("codeprint")) + 1, "0000")
                End If


                str = "insert into tblprint ( CodePrint, Appid,createdate,createid) values('" & Codex & "','" & dt.Rows(i).Item("appid") & "','" & tmpdate1 & "','" & Request.Cookies("UserID").Value & "')"
                Command = New SqlCommand(str, Conn)
                Command.ExecuteNonQuery()
            End If

        Next

        str = "drop table tmpprintcode"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()
        str = "select * into TmpPrintCode  from tblprint where createdate = '" & tmpdate1 & "'"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()
    End Sub

    Protected Sub BtnOpenSound_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles BtnOpenSound.Click
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "ListenSound", "PopUpSoundRec(" & lblExten.Text & "," & lblCarid.Text & "," & lblCusID.Text & ")", True)
    End Sub

    Protected Sub BtnOpenFile_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles BtnOpenFile.Click
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "UploadFile", "FileUpload(" & lblApp.Text & "," & lblCarid.Text & "," & Request.Cookies("UserID").Value & ")", True)
    End Sub

    'Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnSearch.Click
    '    lblRowCount.Text = ""
    '    UltraWebGrid3.DataSource = Nothing
    '    UltraWebGrid3.DataBind()

    '    Dim str As String
    '    Dim Command As SqlCommand
    '    Dim DataReader As SqlDataReader
    '    Dim str1 As String
    '    If wddTypeSearch.SelectedItemIndex <= 0 Then
    '        ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('กรุณาเลือกประเภทการค้นหา !!');", True)
    '        Exit Sub
    '    End If
    '    If txtSearch.Text = "" Then
    '        ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('กรุณาระบุคำค้นหา !!');", True)
    '        Exit Sub
    '    End If

    '    If wddTypeSearch.SelectedValue = "1" Then
    '        str1 = " where (LTRIM(RTRIM(TblCustomer.fnameth)) + LTRIM(RTRIM(TblCustomer.lnameth)) like '%" & Trim(txtSearch.Text.Replace(" ", "")) & "%')   "
    '    End If
    '    If wddTypeSearch.SelectedValue = "2" Then
    '        str1 = " where (LTRIM(RTRIM(tblcar.carid)) like '%" & Trim(txtSearch.Text) & "%' )  "
    '    End If
    '    If wddTypeSearch.SelectedValue = "3" Then
    '        str1 = " where (LTRIM(RTRIM(TblApplication.AppID)) = '" & Trim(txtSearch.Text) & "')"
    '    End If
    '    If wddTypeSearch.SelectedValue = "4" Then
    '        str1 = " where (LTRIM(RTRIM(tblcar.refno))  = '" & Trim(txtSearch.Text) & "' )   "
    '    End If

    '    Conn.Open()

    '    str = " SELECT TblApplication.AppID, TblCustomer.FNameTH + ' ' + TblCustomer.LNameTH AS [ชื่อ-สกุล ลูกค้า], TblCar.CarID AS [ทะเบียนรถ], TblApplication.SuccessDate, TblApplication.ProtectDate AS [วันคุ้มครอง] " + _
    '          " FROM TblCustomer INNER JOIN " + _
    '          " TblCar ON TblCustomer.CusID = TblCar.CusID INNER JOIN " + _
    '          " TblApplication ON TblCar.IdCar = TblApplication.Idcar " + str1

    '    Command = New SqlCommand(str, Conn)
    '    DataReader = Command.ExecuteReader()

    '    dt = New DataTable

    '    dt.Columns.Add("AppID")
    '    dt.Columns.Add("ชื่อ-สกุล ลูกค้า")
    '    dt.Columns.Add("ทะเบียนรถ")
    '    dt.Columns.Add("SuccessDate")
    '    dt.Columns.Add("วันคุ้มครอง")

    '    If DataReader.HasRows Then
    '        While DataReader.Read
    '            Dim dtr As DataRow = dt.NewRow
    '            If IsDBNull(DataReader("AppID")) = False Then
    '                dtr("AppID") = DataReader("AppID")
    '            End If
    '            If IsDBNull(DataReader("ชื่อ-สกุล ลูกค้า")) = False Then
    '                dtr("ชื่อ-สกุล ลูกค้า") = DataReader("ชื่อ-สกุล ลูกค้า")
    '            End If
    '            If IsDBNull(DataReader("ทะเบียนรถ")) = False Then
    '                dtr("ทะเบียนรถ") = DataReader("ทะเบียนรถ")
    '            End If
    '            If IsDBNull(DataReader("SuccessDate")) = False Then
    '                dtr("SuccessDate") = DataReader("SuccessDate")
    '            End If
    '            If IsDBNull(DataReader("วันคุ้มครอง")) = False Then
    '                dtr("วันคุ้มครอง") = DataReader("วันคุ้มครอง")
    '            End If
    '            dt.Rows.Add(dtr)
    '        End While
    '    End If
    '    If dt.Rows.Count > 0 Then
    '        UltraWebGrid3.DataSource = dt
    '        UltraWebGrid3.DataBind()
    '        UltraWebGrid3.Visible = True
    '    End If

    '    dt2 = New DataTable

    '    dt2.Columns.Add("ชื่อไฟล์")
    '    dt2.Columns.Add("ขนาดไฟล์")
    '    dt2.Columns.Add("วันที่สร้าง")
    '    dt2.Columns.Add("ดาวน์โหลด")

    '    Dim myDirInfo As DirectoryInfo = New DirectoryInfo("O:\" & UltraWebGrid3.Rows(0).Cells(0).Text)
    '    Dim arrFileInfo As Array
    '    Dim myFileInfo As FileInfo

    '    arrFileInfo = myDirInfo.GetFiles("*.*")
    '    For Each myFileInfo In arrFileInfo
    '        Dim dtr As DataRow = dt2.NewRow
    '        dtr("ชื่อไฟล์") = myFileInfo.Name
    '        dtr("ขนาดไฟล์") = Math.Round(myFileInfo.Length / 1024, 0) & " KB"
    '        dtr("วันที่สร้าง") = myFileInfo.CreationTime
    '        dtr("ดาวน์โหลด") = "http://asquarenetwork/WebUpload/FileDownload/" & UltraWebGrid3.Rows(0).Cells(0).Text & "/" & myFileInfo.Name
    '        dt2.Rows.Add(dtr)
    '    Next
    '    Label16.Visible = True
    '    UltraWebGrid4.DataSource = dt2
    '    UltraWebGrid4.DataBind()
    '    UltraWebGrid4.Visible = True
    '    Conn.Close()

    '    lblRowCount.Text = "พบข้อมูลการค้นหาทั้งสิ้น " & UltraWebGrid3.Rows.Count & " แถว"
    '    lblRowCount0.Text = "พบข้อมูลไฟล์ทั้งสิ้น " & UltraWebGrid4.Rows.Count & " แถว"
    'End Sub



    Protected Sub btnEditApp_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnEditApp.Click
        Dim Command As SqlCommand
        Dim DataReader As SqlDataReader
        dt = New DataTable

        dt.Columns.Add("Tmp")
        Conn.Open()
        Dim Str As String = "select useridqc from tblapplication where appid = '" & lblApp.Text & "'"
        Command = New SqlCommand(Str, Conn)
        DataReader = Command.ExecuteReader
        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt.NewRow
                If IsDBNull(DataReader("useridqc")) = False Then
                    dtr("Tmp") = DataReader("useridqc")
                End If
                dt.Rows.Add(dtr)
            End While
        End If
        DataReader.Close()


        If dt.Rows.Count > 0 Then
            If Request.Cookies("userLevel").Value = "8" And Request.Cookies("TypeTsr").Value <> "0" Then '8 type 0 คือ Qcพิเศษ จะเห็นเมนูมากกว่า Qc ปกติ 
                'If Request.Cookies("UserID").Value <> "684" And Request.Cookies("UserID").Value <> "3293" And Request.Cookies("UserID").Value <> "141" Then 'ยกเว้นวัต
                If dt.Rows(0).Item("Tmp") <> Request.Cookies("UserID").Value Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('ไม่สามารถ Edit App ได้เนื่องจากเป็น App ของ QC คนอื่น !!');", True)
                    Exit Sub
                End If
            End If
        End If
        Conn.Close()

        UpDateStatusInProcess(lblApp.Text)
        AddNeedIDCardandKYC(lblApp.Text)

        Response.Cookies("TypeTsr").Value = lblTypeTSR.Text

        ScriptManager.RegisterStartupScript(Page, Page.GetType, "Application", "EditApp(" & lblApp.Text & "," & lblCarid.Text & "," & Request.Cookies("UserID").Value & ")", True)


    End Sub

    Private Sub ViewOldApp()
        Dim Command As SqlCommand
        Dim DataReader As SqlDataReader
        dt = New DataTable

        dt.Columns.Add("AppID")
        Conn.Open()
        Dim Str As String = "SELECT TOP 1 AppID FROM TmpApp_CustRenew WHERE Idcar = '" & lblCarid.Text & "' ORDER BY AppID DESC"
        Command = New SqlCommand(Str, Conn)
        DataReader = Command.ExecuteReader
        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt.NewRow
                If IsDBNull(DataReader("AppID")) = False Then
                    dtr("AppID") = DataReader("AppID")
                End If
                dt.Rows.Add(dtr)
            End While
        End If
        Conn.Close()
        If dt.Rows.Count > 0 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "OldApps", "ViewOldApp(" & dt.Rows(0).Item("AppID") & "," & lblCarid.Text & "," & Request.Cookies("UserID").Value & ")", True)
        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "AppNone", "OldAppNone()", True)
        End If
    End Sub

    Protected Sub btnCalculate_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnCalculate.Click
        Response.Cookies("TypeTsr").Value = lblTypeTSR.Text
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "Calculate", "PopUpCalculate(" & lblApp.Text & "," & lblCarid.Text & ")", True)
    End Sub

    'Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
    '    txtSearch.Attributes.Add("onkeypress", "return clickButton(event,'" + btnSearch.ClientID + "')")
    'End Sub

    Protected Sub btnHoldOn_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnHoldOn.Click
        GetSoftPhone()
        Label8.Text = "โทรหาลูกค้า : <FONT Color='red'>กำลังโทรหาลูกค้า...</FONT>"
        'btnHoldOn.Enabled = False
    End Sub

    Protected Sub GetSoftPhone()
        Dim CallUrl As String = ""
        CallUrl += Replace(ConfigurationManager.AppSettings("WebCall"), "@IPAsterisk", Request.Cookies("ipAsterisk").Value) & "to=" & txtPhoneNo.Text & "&from=" & Request.Cookies("Extension").Value & "&refer1=" & lblCarid.Text
        TelAjax = CallUrl
    End Sub

    Protected Sub wddTelType_SelectionChanged(ByVal sender As Object, ByVal e As Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs) Handles wddTelType.SelectionChanged
        Dim Command As SqlCommand
        Dim DataReader As SqlDataReader
        Dim Str As String

        If wddTelType.SelectedItemIndex <= 0 Then
            Str = "SELECT Mobile,'-' As TelExt FROM TblCustomer WHERE CusID = '" & lblCusID.Text & "'"
        ElseIf wddTelType.SelectedItemIndex = 1 Then
            Str = "SELECT Tel, TelExt FROM TblCustomer WHERE CusID = '" & lblCusID.Text & "'"
        ElseIf wddTelType.SelectedItemIndex = 2 Then
            Str = "SELECT OTel, OTelExt FROM TblCustomer WHERE CusID = '" & lblCusID.Text & "'"
        ElseIf wddTelType.SelectedItemIndex = 3 Then
            Str = "SELECT OthTel1, OthTel1Ext FROM TblCustomer WHERE CusID = '" & lblCusID.Text & "'"
        ElseIf wddTelType.SelectedItemIndex = 4 Then
            Str = "SELECT OthTel2, OthTel2Ext FROM TblCustomer WHERE CusID = '" & lblCusID.Text & "'"
        End If
        Conn.Open()

        Command = New SqlCommand(Str, Conn)
        DataReader = Command.ExecuteReader
        If DataReader.HasRows Then
            While DataReader.Read
                If IsDBNull(DataReader(0)) = False Then
                    txtPhoneNo.Text = Trim(DataReader(0))
                End If

                If IsDBNull(DataReader(1)) = False Then
                    If Trim(DataReader(1)) = "" Then
                        lblExt.Text = "-"
                    Else
                        lblExt.Text = DataReader(1)
                    End If

                End If
            End While
        End If
        Conn.Close()
    End Sub

    Protected Sub btnOldApp_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnOldApp.Click
        Response.Cookies("TypeTsr").Value = lblTypeTSR.Text
        If lblTypeTSR.Text = "3" Then
            ViewOldApp()
        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "AppNone", "OldAppNone()", True)
        End If
    End Sub

    Protected Sub UltraWebGrid4_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.LayoutEventArgs)

    End Sub

    'Protected Sub UltraWebGrid4_InitializeRow(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles UltraWebGrid4.InitializeRow
    '    e.Row.Cells(0).Value = "<FONT FACE='angsana new' SIZE='2' color='Blue'><u><a href= '" + e.Row.Cells(3).Value.ToString() + "'>" + e.Row.Cells(0).Value.ToString() + "</a></u></FONT>"

    'End Sub

    Private Sub UpDateStatusInProcess(ByVal AppID As String)
        Dim Command As SqlCommand
        Dim Str, Chk As String
        Dim DataReader As SqlDataReader
        Str = "SELECT Statusqc FROM TblApplication WHERE AppID = '" & AppID & "'"
        Conn.Open()

        Command = New SqlCommand(Str, Conn)
        DataReader = Command.ExecuteReader
        If DataReader.HasRows Then
            While DataReader.Read
                If IsDBNull(DataReader("Statusqc")) = False Then
                    Chk = DataReader("Statusqc")
                End If
            End While
        End If

        DataReader.Close()

        If Chk = 0 Then
            Str = "UPDATE TblApplication SET Statusqc = 9 ,UpdateDate = GETDATE() ,UpdateID = '" & Request.Cookies("UserID").Value & "' WHERE AppID = '" & AppID & "'"
        ElseIf Chk = 3 Then
            Str = "UPDATE TblApplication SET Statusqc = 10 ,UpdateDate = GETDATE() ,UpdateID = '" & Request.Cookies("UserID").Value & "' WHERE AppID = '" & AppID & "'"
        Else
            Conn.Close()
            Exit Sub
        End If

        Tran = Conn.BeginTransaction()

        Try
            Command = New SqlCommand(Str, Conn)
            Command.Transaction = Tran
            Command.ExecuteNonQuery()

            Tran.Commit()

            Conn.Close()
        Catch ex As Exception
            Tran.Rollback()
            Conn.Close()
        End Try


    End Sub

    Private Sub AddNeedIDCardandKYC(ByVal AppID As String)
        Dim Command As SqlCommand
        Dim sqlCmd As String
        Dim DataReader As SqlDataReader
        Dim ProPrice, productid As Integer
        Conn.Open()

        sqlCmd = "SELECT ProPrice,ProductID FROM TblApplication WHERE AppID = '" & AppID & "' and IsProvalue = 1 and ProductID in (9,10,14,18,69,71)"
        Command = New SqlCommand(sqlCmd, Conn)
        DataReader = Command.ExecuteReader
        If DataReader.HasRows Then
            While DataReader.Read
                If IsDBNull(DataReader("ProPrice")) = False Then
                    ProPrice = CInt(DataReader("ProPrice"))
                Else
                    ProPrice = 0
                End If

                If IsDBNull(DataReader("ProductID")) = False Then
                    productid = CInt(DataReader("ProductID"))
                End If

            End While
        Else
            ProPrice = 0
        End If

        DataReader.Close()

        If ProPrice >= 700000 And productid = 71 Then
            sqlCmd = "UPDATE TblApplication SET doc4 = 1, doc6 = 1 WHERE AppID = '" & AppID & "'"
        ElseIf ProPrice >= 700000 And productid <> 71 Then
            sqlCmd = "UPDATE TblApplication SET doc4 = 1, doc6 = 0 WHERE AppID = '" & AppID & "'"
        ElseIf ProPrice < 700000 Then
            sqlCmd = "UPDATE TblApplication SET doc4 = 0, doc6 = 0 WHERE AppID = '" & AppID & "'"
        End If

        Tran = Conn.BeginTransaction()

        Try
            Command = New SqlCommand(sqlCmd, Conn)
            Command.Transaction = Tran
            Command.ExecuteNonQuery()

            Tran.Commit()

            Conn.Close()
        Catch ex As Exception
            Tran.Rollback()
            Conn.Close()
        End Try

    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        myReport.Dispose()
        myReport.Close()
    End Sub

    Protected Sub btnPreview_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnPreview.Click
        SetappAcc()
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "Preview", "Preview(" & lblApp.Text & "," & lblCarid.Text & "," & Request.Cookies("UserID").Value & ")", True)
        btnPass.Enabled = True
    End Sub
    Protected Sub showReminder(ByVal appidx As String)
        Conn.Open()
        Dim str As String = "select a.*,b.FName+' '+b.LName  as NameQc from TblQcReminder a"
        str += " inner join tbluser b on a.CreateID=b.UserID where a.AppID = '" & appidx & "' order by  a.createdate desc"

        Dim Command As SqlCommand = New SqlCommand(str, Conn)
        Dim DataReader As SqlDataReader
        DataReader = Command.ExecuteReader()
        dt = New DataTable
        dt.Columns.Add("ลำดับ")
        dt.Columns.Add("รายละเอียด")
        dt.Columns.Add("Qc")
        dt.Columns.Add("วันที่")

        Dim Count As Integer = 0
        If DataReader.HasRows Then
            While DataReader.Read()
                Dim dtr As DataRow = dt.NewRow
                Count += 1
                dtr("ลำดับ") = Count
                If IsDBNull(DataReader("ReminderDetail")) = False Then
                    dtr("รายละเอียด") = DataReader("ReminderDetail")
                End If
                If IsDBNull(DataReader("NameQc")) = False Then
                    dtr("Qc") = DataReader("NameQc")
                End If
                If IsDBNull(DataReader("CreateDate")) = False Then
                    dtr("วันที่") = Format(DataReader("CreateDate"), "dd/MM/yyyy HH:mm:ss")
                End If
                dt.Rows.Add(dtr)
            End While
        End If
        DataReader.Close()
        Conn.Close()

        If dt.Rows.Count > 0 Then
            GvReminder.DataSource = dt
            GvReminder.DataBind()
            GvReminder.Visible = True

        Else
            GvReminder.DataSource = Nothing
            GvReminder.DataBind()
            GvReminder.Visible = False
        End If
    End Sub
    Protected Sub btnAddReminder_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnAddReminder.Click
        Conn.Open()
        If Me.txtwcommentqc.Text <> "" AndAlso lblappidreminder.Text <> "" Then

            Dim Command As SqlCommand
            Dim sqlCmd As String
            sqlCmd = "INSERT INTO TblQcReminder(AppID,ReminderDetail,CreateID) VALUES ('" & lblappidreminder.Text & "','" & txtwcommentqc.Text & "','" & Request.Cookies("UserID").Value & "')"
            Command = New SqlCommand(sqlCmd, Conn)
            Command.Transaction = Tran
            Command.ExecuteNonQuery()
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('บันทึกแล้วค่ะ');", True)

        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('กรุณาตรวจสอบอีกครั้งค่ะ');", True)
        End If
        Conn.Close()
        If lblappidreminder.Text <> "" Then
            showReminder(lblappidreminder.Text)
        End If
        Me.txtwcommentqc.Text = ""
    End Sub
    Private Sub UpdatePaynoNew(ByVal appxnew As String)
        Conn.Open()
        '1.หาเงินที่ชำระเงินมาจาก Appเก่า 
        Dim str As String
        Dim Command As SqlCommand
        Dim DataReader As SqlDataReader
        Dim pay1 As Integer = 0
        Dim pay2 As Integer = 0
        Dim pay3 As Integer = 0
        Dim pay4 As Integer = 0
        '1.ยอดที่จ่ายมาทั้งหมด
        str = "select payvalue from tblpayment where appid = '" & appxnew & "'"
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader()
        If DataReader.HasRows Then
            While DataReader.Read
                If IsDBNull(DataReader("payvalue")) = False Then
                    pay1 += DataReader("payvalue")
                End If
            End While
        End If
        DataReader.Close()
        '2.หาเงินที่ยังไม่ชำระ
        str = "select TotalPay from tblapppay where  Ispaid=0 and appid='" & appxnew & "'"
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader()
        If DataReader.HasRows Then
            While DataReader.Read
                If IsDBNull(DataReader("TotalPay")) = False Then
                    pay2 += DataReader("TotalPay") ' 
                End If
            End While
        End If
        DataReader.Close()
        '3.จำนวนเงินของ
        str = "select TotalPay from tblapppay where  appid='" & appxnew & "'"
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader()
        If DataReader.HasRows Then
            While DataReader.Read
                If IsDBNull(DataReader("TotalPay")) = False Then
                    pay3 += DataReader("TotalPay")
                End If
            End While
        End If
        DataReader.Close()
        pay4 = pay3 - pay2 - pay1
        Dim MaxP As Integer
        If pay4 > 100 Then
            'Max งวดการชำระ
            str = "select max(PayID) as MaxP from tblapppay where  appid='" & appxnew & "'"
            Command = New SqlCommand(str, Conn)
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    If IsDBNull(DataReader("MaxP")) = False Then
                        MaxP = DataReader("MaxP")
                    End If
                End While
            End If
            DataReader.Close()
            str = "insert into TblAppPay (PayID,AppID,AppointDate,TotalPay,typepay,CreateID,potype) values ('" & MaxP + 1 & "','" & appxnew & "',getdate()," & pay4 & ",1,'" & Request.Cookies("UserID").Value & "',1)"
            Command = New SqlCommand(str, Conn)
            Command.ExecuteNonQuery()
        End If
        Conn.Close()
    End Sub
    Protected Function chkAreaPhoto(ByVal appid As String) As Boolean
        Dim Str As String = ""
        Str += " select distinct a3.appid,a1.cusid,a2.idcar,getdate(),138,a4.userid,1"
        Str += " from tblcustomer a1 "
        Str += " inner join tblcar a2 on a1.cusid = a2.cusid "
        Str += " inner join (select * from tblapplication where appstatus=1 and isprovalue=1 and statusqc in(0,3,9,10)"
        Str += " and productid in(18,52,83) ) as a3 on a2.idcar = a3.idcar "
        str += " Inner join TblAppSubmit a5 on a3.Pkgid=a5.AppsubmitId "
        Str += " inner join (select * from TblPhotoArea where userid is not null and userid <> '0') as a4 "
        Str += " on a1.subdist = a4.subdist "
        Str += " and a1.dist = a4.dist "
        Str += " and a1.province = a4.province"
        Str += " where a5.Typeid=1 and a3.appid not in(select distinct appid from tblphotocase)"
        Str += " and (select dbo.[chkAppRenewT](a3.appid))='N'  and a3.appid = " + appid

        dt21 = DataAccess.DataRead(Str)
        If dt21.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    'Add By NA 20170928 ตาม Req.พี่อ้า เรื่อง LINE สร้าง Folder ใบคำขอ+Payment 
    Private Sub Auto_File()
        '1.AppID
        '2.Idcar
        Conn.Open()
        Dim dttmpLine As New DataTable
        Dim query = New System.Text.StringBuilder()

        query.Append(" SELECT   tmp_QC_PayCredit.UserID as gg,   tmp_QC_PayCredit.*, tmp_QC_app02.*, tmp_QC_app01.* ,tblcar.refno,tmp_QC_app01.AppID as appid01,isnull(Tbl_ProductType.hotLINE,'') as 'hotLINE01' ")
        query.Append(" FROM     tmp_QC_PayCredit ")
        query.Append(" INNER JOIN	tmp_QC_app02 ON tmp_QC_PayCredit.AppID = tmp_QC_app02.AppID ")
        query.Append(" INNER JOIN	tmp_QC_app01 ON tmp_QC_app02.AppID = tmp_QC_app01.AppID")
        query.Append(" inner join tblapplication on tmp_QC_app01.AppID=tblapplication.appid")
        query.Append(" inner join tblcar on tblapplication.idcar=tblcar.idcar")
        query.Append(" inner join Tbl_ProductType on Tbl_ProductType.ProTypeID=tblapplication.ProDuctID ")


        query.Append(" WHERE tmp_QC_PayCredit.UserID = " & Request.Cookies("userID").Value)
        query.Append(" AND tmp_QC_app02.UserID = " & Request.Cookies("userID").Value)
        query.Append(" AND tmp_QC_app01.UserID = " & Request.Cookies("userID").Value)
        query.Append(" order by tmp_QC_PayCredit.UserID")


        dttmpLine = DataAccess.DataRead(query.ToString)
        If dttmpLine.Rows.Count = 1 Then
            '1.File background 
            Dim P_Server As String = "~/images/LINE/covernote.jpg"
            Dim folder As String = Server.MapPath(P_Server)

            '2.Process 

            Dim bm As New Bitmap(folder)
            Dim FontName As String = "Angsana New"
            Dim gra As Graphics = Graphics.FromImage(bm)

            Dim B1 As String = ""
            Dim B2 As String = ""
            LINE_Address(dttmpLine, B1, B2)
            'New Data 
            Dim A1 As String = dttmpLine.Rows(0)("sname").ToString()
            Dim refno As String = dttmpLine.Rows(0)("refno").ToString()
            Dim appid01 As String = dttmpLine.Rows(0)("appid01").ToString()


            A1 = "เรียน " & A1 & Environment.NewLine & B1
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(200, 450)) '12

            Dim barcodestr As String = "|010755800027000" & Chr(13) & refno & Chr(13) & appid01 & Chr(13) & "0"
            Dim myimg As Image = Code128Rendering.MakeBarcodeImage(barcodestr, 2, True)
            gra.DrawImage(myimg, 750, 3380, myimg.Width, 75)

            gra.DrawString("สำหรับชำระค่าเบี้ยประกันภัย", New Font(FontName, 36), Brushes.Black, New PointF(2040, 3435)) '10
            Dim qe As MessagingToolkit.QRCode.Codec.QRCodeEncoder = New MessagingToolkit.QRCode.Codec.QRCodeEncoder
            Dim myimg1 As Image = qe.Encode(barcodestr)
            gra.DrawImage(myimg1, 2140, 3260, 170, 170)
            gra.DrawString(barcodestr, New Font(FontName, 36), Brushes.Black, New PointF(920, 3435)) '12
            'barcodestr


            Dim myimgappid As Image = Code128Rendering.MakeBarcodeImage(appid01, 2, True)
            'myimgappid.RotateFlip(RotateFlipType.Rotate270FlipX)
            gra.DrawImage(myimgappid, 1100, 630, myimgappid.Width, 50) 'myimgappid.Height


            gra.DrawString(appid01, New Font(FontName, 30), Brushes.Black, New PointF(1180, 670)) '10


            A1 = ""
            A1 = If((dttmpLine.Rows(0)("initth").ToString() <> "[ไม่ทราบ]"), dttmpLine.Rows(0)("initth").ToString(), "") & dttmpLine.Rows(0)("FNameTH").ToString() & " " & dttmpLine.Rows(0)("LNameTH").ToString()
            A1 = A1 & Environment.NewLine & B2
            gra.DrawString(A1, New Font(FontName, 30), Brushes.Black, New PointF(1490, 520)) '10


            A1 = dttmpLine.Rows(0)("ProTypeBrand").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(350, 740))


            'LINE_Phone(dttmpLine.Rows(0)("ProductID").ToString(), A1)
            A1 = dttmpLine.Rows(0)("hotLINE01").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1265, 740))


            If dttmpLine.Rows(0)("CarDriver1").ToString() <> " " Then
                B1 = "X"
                B2 = ""
            End If
            If dttmpLine.Rows(0)("CarDriver1").ToString() = " " Then
                B1 = ""
                B2 = "X"
            End If

            gra.DrawString(B1, New Font(FontName, 36), Brushes.Black, New PointF(1582, 740))
            gra.DrawString(B2, New Font(FontName, 36), Brushes.Black, New PointF(1985, 740))

            A1 = dttmpLine.Rows(0)("CarDriver1").ToString()

            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(270, 825))
            A1 = ""
            If dttmpLine.Rows(0)("CarDriver1").ToString() <> " " Then
                A1 = dttmpLine.Rows(0)("CarDriverBorn1").ToString()
            End If

            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(995, 825))

            A1 = ""
            If dttmpLine.Rows(0)("CarDriver1").ToString() <> " " Then
                A1 = dttmpLine.Rows(0)("DBornNO1").ToString()
            End If

            gra.DrawString(A1, New Font(FontName, 30), Brushes.Black, New PointF(1350, 830))

            A1 = ""

            If dttmpLine.Rows(0)("CarDriver1").ToString() <> " " Then
                A1 = dttmpLine.Rows(0)("DBornDate1").ToString()
            End If

            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1660, 825))

            A1 = ""
            'D5
            If dttmpLine.Rows(0)("CarDriver1").ToString() <> " " Then
                A1 = dttmpLine.Rows(0)("DBornAddr1").ToString()
            End If
            'A1 = "กรุงเทพมหานคร"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(2070, 825))

            A1 = ""
            'E1
            A1 = dttmpLine.Rows(0)("CarDriver2").ToString()

            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(270, 900))

            A1 = ""
            If dttmpLine.Rows(0)("CarDriver2").ToString() <> " " Then
                A1 = dttmpLine.Rows(0)("CarDriverBorn2").ToString()
            End If
            'A1 = "06/09/2523"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(995, 900))

            A1 = ""
            If dttmpLine.Rows(0)("CarDriver2").ToString() <> " " Then
                A1 = dttmpLine.Rows(0)("DBornNO2").ToString()
            End If
            'A1 = "XXXXXXXX"
            gra.DrawString(A1, New Font(FontName, 30), Brushes.Black, New PointF(1350, 900))

            A1 = ""
            If dttmpLine.Rows(0)("CarDriver2").ToString() <> " " Then
                A1 = dttmpLine.Rows(0)("DBornDate2").ToString()
            End If
            'A1 = "11/03/2548"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1660, 900))


            A1 = ""
            If dttmpLine.Rows(0)("CarDriver2").ToString() <> " " Then
                A1 = dttmpLine.Rows(0)("DBornAddr2").ToString()
            End If
            ' A1 = "กรุงเทพมหานคร"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(2070, 900))

            A1 = ""
            If dttmpLine.Rows(0)("CarDriver1").ToString() <> " " Then
                A1 = dttmpLine.Rows(0)("IdCard1").ToString()
            End If

            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(490, 975))

            A1 = ""

            'Dim F2 As String = ""
            If dttmpLine.Rows(0)("CarDriver2").ToString() <> " " Then
                A1 = dttmpLine.Rows(0)("IdCard2").ToString()
            End If

            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1400, 975))
            A1 = ""

            If dttmpLine.Rows(0)("ProDuctID").ToString() <> "15" Then
                A1 = dttmpLine.Rows(0)("AppNO").ToString()
            Else
                A1 = dttmpLine.Rows(0)("PolicyNO").ToString()
            End If

            'gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(2070, 1050))

            A1 = ""
            If dttmpLine.Rows(0)("IsProvalue").ToString() = "1" Then
                A1 = dttmpLine.Rows(0)("ProtectDate").ToString() & "  -  " & dttmpLine.Rows(0)("expprotectdate").ToString()
            End If
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(450, 1050)) '1130
            A1 = ""

            '    'If dttmpLine.Rows(0)("IsProvalue").ToString() = "1" Then
            '    '    A1 = dttmpLine.Rows(0)("ProtectDate").ToString() & "-" & dttmpLine.Rows(0)("expprotectdate").ToString()
            'End If
            A1 = Session("pd1")
            'A1 = "21/09/2560 - 21/09/2561"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1330, 1050))
            gra.DrawString("1", New Font(FontName, 36), Brushes.Black, New PointF(140, 1280)) '1360

            'I2
            A1 = ""
            If dttmpLine.Rows(0)("discounttype").ToString() > "0" Then
                A1 = dttmpLine.Rows(0)("discounttype").ToString()
            End If
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(250, 1280))

            A1 = ""

            If dttmpLine.Rows(0)("CarBrand").ToString() <> "" And dttmpLine.Rows(0)("CarSeries").ToString() <> "" Then
                A1 = dttmpLine.Rows(0)("CarBrand").ToString() & "/" & Chr(10) & Chr(13) & dttmpLine.Rows(0)("CarSeries").ToString()
            ElseIf dttmpLine.Rows(0)("CarBrand").ToString() <> "" And dttmpLine.Rows(0)("CarSeries").ToString() = "" Then
                A1 = dttmpLine.Rows(0)("CarBrand").ToString() & "/" & Chr(10) & Chr(13) & "-"
            ElseIf dttmpLine.Rows(0)("CarBrand").ToString() = "" And dttmpLine.Rows(0)("CarSeries").ToString() <> "" Then
                A1 = "-" & "/" & Chr(10) & Chr(13) & dttmpLine.Rows(0)("CarSeries").ToString()
            End If
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(375, 1280))
            'I4
            A1 = ""
            A1 = dttmpLine.Rows(0)("CarID").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(840, 1280))

            'I5
            A1 = ""
            If dttmpLine.Rows(0)("CarNo").ToString() <> "" And dttmpLine.Rows(0)("CarBoxNo").ToString() <> "" Then
                A1 = dttmpLine.Rows(0)("CarBoxNo").ToString() & "/" & Chr(10) & Chr(13) & dttmpLine.Rows(0)("CarNo").ToString()

            ElseIf dttmpLine.Rows(0)("CarNo").ToString() <> "" And dttmpLine.Rows(0)("CarBoxNo").ToString() = "" Then
                A1 = "-" & "/" & Chr(10) & Chr(13) & dttmpLine.Rows(0)("CarNo").ToString()
            ElseIf dttmpLine.Rows(0)("CarNo").ToString() = "" And dttmpLine.Rows(0)("CarBoxNo").ToString() <> "" Then
                A1 = dttmpLine.Rows(0)("CarBoxNo").ToString() & "/" & "-"
            End If
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1120, 1280))

            'I6
            A1 = ""
            A1 = dttmpLine.Rows(0)("CarYear").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1570, 1280))

            'I7
            A1 = ""
            A1 = dttmpLine.Rows(0)("cartypename").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1720, 1280))

            'I8
            A1 = ""
            A1 = "- / " & dttmpLine.Rows(0)("CarSize").ToString() & " / -"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1970, 1280))


            'J1
            A1 = ""
            A1 = dttmpLine.Rows(0)("Lost_Car1").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1068, 1570))
            'J2
            A1 = ""
            A1 = dttmpLine.Rows(0)("Acc_Lost1").ToString()
            ' A1 = "999999"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1820, 1629))

            'J3
            A1 = ""
            A1 = dttmpLine.Rows(0)("Acc_Lost4").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1970, 1629))

            'K1
            A1 = ""
            A1 = dttmpLine.Rows(0)("Lost_Life1").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(375, 1634)) '1706


            'K4
            A1 = ""
            If dttmpLine.Rows(0)("IsProvalue").ToString() = "1" Then
                A1 = dttmpLine.Rows(0)("Lost_Life2").ToString()
            End If
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(375, 1699)) '1771
            'K2
            A1 = ""
            A1 = dttmpLine.Rows(0)("Lost_Car2").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1068, 1699))

            'K3
            A1 = ""
            A1 = dttmpLine.Rows(0)("Acc_Lost3").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1850, 1699))

            'k4
            A1 = ""
            A1 = dttmpLine.Rows(0)("Acc_Lost4").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1970, 1699))


            'L1
            A1 = "L1"
            A1 = dttmpLine.Rows(0)("Lost_Prop1").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(375, 1849)) '1921

            'L2
            A1 = ""
            A1 = dttmpLine.Rows(0)("Car_Fire").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1068, 1849))


            'L3
            A1 = "0"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1850, 1825)) '1895
            'L4
            A1 = "0.00"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1970, 1825))

            'M1
            A1 = "0"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1850, 1890)) '1965
            'M2
            A1 = "0.00"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1970, 1890))
            ' N1()
            A1 = "0.00"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(375, 1970)) '2050
            'N2()
            A1 = ""
            A1 = dttmpLine.Rows(0)("Lost_Prop2").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1068, 2020)) '2100

            'N3
            A1 = ""
            A1 = dttmpLine.Rows(0)("Maintain").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1970, 1966)) '2040

            'O1
            A1 = ""
            A1 = dttmpLine.Rows(0)("Insure").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1970, 2050)) '2130


            'P1
            A1 = ""
            A1 = If(dttmpLine.Rows(0)("IsProvalue").ToString() = "1", dttmpLine.Rows(0)("ProValue").ToString(), "0.00")
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(350, 2240)) '2300
            'P2
            A1 = ""
            A1 = If(dttmpLine.Rows(0)("IsCarpet").ToString() = "1", dttmpLine.Rows(0)("CarPet").ToString(), "0.00")
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1068, 2240))
            'P3
            A1 = ""

            Dim AA As Decimal = dttmpLine.Rows(0)("ProValue")
            Dim AB As Decimal = dttmpLine.Rows(0)("CarPet")
            AA = Math.Round(AA + AB, 2)
            gra.DrawString(AA.ToString("N2"), New Font(FontName, 36), Brushes.Black, New PointF(1850, 2240))
            'Q1
            A1 = ""

            Dim TmpQ1 As String = ""

            If dttmpLine.Rows(0)("Typeprovalue").ToString() = "1" Then
                TmpQ1 = "ชั้น1"
            ElseIf dttmpLine.Rows(0)("Typeprovalue").ToString() = "2" Then
                TmpQ1 = "ชั้น3"
            ElseIf dttmpLine.Rows(0)("Typeprovalue").ToString() = "3" Then
                TmpQ1 = "ชั้น3+"
            ElseIf dttmpLine.Rows(0)("Typeprovalue").ToString() = "4" Then
                TmpQ1 = "ชั้น2+"
            End If

            If dttmpLine.Rows(0)("IsProvalue").ToString() = "1" And dttmpLine.Rows(0)("IsCarpet").ToString() = "1" Then
                A1 = "ประกันภัย " & TmpQ1 & " รวม พ.ร.บ."
            ElseIf dttmpLine.Rows(0)("IsProvalue").ToString() = "1" And dttmpLine.Rows(0)("IsCarpet").ToString() = "0" Then
                A1 = "ประกันภัย " & TmpQ1 & " ไม่รวม พ.ร.บ."
            ElseIf dttmpLine.Rows(0)("IsProvalue").ToString() = "0" And dttmpLine.Rows(0)("IsCarpet").ToString() = "1" Then
                A1 = "พ.ร.บ."
            End If
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(500, 2300)) '2380

            A1 = ""
            AA = dttmpLine.Rows(0)("YearPay")
            AB = dttmpLine.Rows(0)("ProValue")
            If AA - AB > 0 Then

                A1 = "* ส่วนลด    " & (AA - AB).ToString("N2") & "   บาท"
            End If

            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1100, 2300))
            Dim Q3 As String = If(dttmpLine.Rows(0)("CarFixIn").ToString() = "1", "X", "")
            Dim Q4 As String = If(dttmpLine.Rows(0)("CarFixIn").ToString() = "0", "X", "")

            gra.DrawString(Q3, New Font(FontName, 36), Brushes.Black, New PointF(1530, 2300))
            gra.DrawString(Q4, New Font(FontName, 36), Brushes.Black, New PointF(1990, 2300))

            'R1
            A1 = ""

            'Dim R1 As String = ""
            Dim TmpR1 As String = Replace(dttmpLine.Rows(0)("discounttype").ToString(), " ", "")

            If dttmpLine.Rows(0)("IsProvalue").ToString() = "1" And TmpR1 = "320" Then
                A1 = "ใช้เพื่อการพาณิชย์ ไม่ใช้เพื่อการบรรทุก และขนส่งสินค้าที่มี ความเสี่ยงภัยสูง เช่น เชื้อเพลิง"
            ElseIf dttmpLine.Rows(0)("IsProvalue").ToString() = "1" And TmpR1 = "220" Then
                A1 = "ใช้เพื่อการพาณิชย์ ไม่ใช้รับจ้างสาธารณะ"
            ElseIf dttmpLine.Rows(0)("IsProvalue") = "1" And TmpR1 = "110" Then
                A1 = "ใช้ส่วนบุคคล ไม่ใช้รับจ้าง หรือให้เช่า"
            ElseIf dttmpLine.Rows(0)("IsProvalue").ToString() = "1" And TmpR1 = "210" Then
                A1 = "ใช้ส่วนบุคคล ไม่ใช้รับจ้าง หรือให้เช่า"
            ElseIf dttmpLine.Rows(0)("IsProvalue").ToString() = "0" Then
                A1 = "ความคุ้มครองตามเงื่อนไข กรมธรรม์ประกันภัยคุ้มครองผู้ประสบภัยจากรถยนต์"
            End If

            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(400, 2387)) '2460

            'R2
            A1 = ""
            A1 = dttmpLine.Rows(0)("createdate").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1850, 2387))
            'Dim R2 As String = dttmpLine.Rows(0)("createdate").ToString()
            ''PayNo1
            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("payid1").ToString(), dttmpLine.Rows(0)("payid1").ToString())
            'A1 = "1"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(150, 2570))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("AppointDate1").ToString(), dttmpLine.Rows(0)("AppointDate1").ToString())
            'A1 = "1"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(350, 2570))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("totalpay1").ToString() <> "  ", dttmpLine.Rows(0)("totalpay1").ToString() & ".00", "")
            'A1 = "1"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(680, 2570))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", "เงินสด", If(dttmpLine.Rows(0)("Typepay1").ToString() = "2", "บัตรเครดิต", ""))
            'A1 = "1"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(980, 2570))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("payid4").ToString(), dttmpLine.Rows(0)("payid4").ToString())
            'A1 = "4"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1310, 2570))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("AppointDate4").ToString(), dttmpLine.Rows(0)("AppointDate4").ToString())
            ' A1 = "4"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1510, 2570))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("totalpay4").ToString() <> "  ", dttmpLine.Rows(0)("totalpay4").ToString() & ".00", "")
            'A1 = "4"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1840, 2570))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay4").ToString() = "1", "เงินสด", If(dttmpLine.Rows(0)("Typepay4").ToString() = "2", "บัตรเครดิต", ""))
            ' A1 = "4"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(2140, 2570))
            'pAYID2
            'Dim t1 As String = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("payid2").ToString(), dttmpLine.Rows(0)("payid2").ToString())
            'Dim t2 As String = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("AppointDate2").ToString(), dttmpLine.Rows(0)("AppointDate2").ToString())
            'Dim t3 As String = If(dttmpLine.Rows(0)("totalpay2").ToString() <> "", dttmpLine.Rows(0)("totalpay2").ToString() & ".00", "")
            'Dim t4 As String = If(dttmpLine.Rows(0)("Typepay2").ToString() = "1", "เงินสด", If(dttmpLine.Rows(0)("Typepay2").ToString() = "2", "บัตรเครดิต", ""))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("payid2").ToString(), dttmpLine.Rows(0)("payid2").ToString())
            'A1 = "2"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(150, 2650))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("AppointDate2").ToString(), dttmpLine.Rows(0)("AppointDate2").ToString())
            'A1 = "2"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(350, 2650))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("totalpay2").ToString() <> "  ", dttmpLine.Rows(0)("totalpay2").ToString() & ".00", "")
            'A1 = "2"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(680, 2650))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay2").ToString() = "1", "เงินสด", If(dttmpLine.Rows(0)("Typepay2").ToString() = "2", "บัตรเครดิต", ""))
            'A1 = "2"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(980, 2650))
            ''payid3
            'Dim U1 As String = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("payid3").ToString(), dttmpLine.Rows(0)("payid3").ToString())
            'Dim U2 As String = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("AppointDate3").ToString(), dttmpLine.Rows(0)("AppointDate3").ToString())
            'Dim U3 As String = If(dttmpLine.Rows(0)("totalpay3").ToString() <> "", dttmpLine.Rows(0)("totalpay3").ToString() & ".00", "")
            'Dim U4 As String = If(dttmpLine.Rows(0)("Typepay3").ToString() = "1", "เงินสด", If(dttmpLine.Rows(0)("Typepay3").ToString() = "2", "บัตรเครดิต", ""))
            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("payid3").ToString(), dttmpLine.Rows(0)("payid3").ToString())
            'A1 = "3"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(150, 2730))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("AppointDate3").ToString(), dttmpLine.Rows(0)("AppointDate3").ToString())
            'A1 = "3"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(350, 2730))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("totalpay3").ToString() <> "  ", dttmpLine.Rows(0)("totalpay3").ToString() & ".00", "")
            'A1 = "3"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(680, 2730))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay3").ToString() = "1", "เงินสด", If(dttmpLine.Rows(0)("Typepay3").ToString() = "2", "บัตรเครดิต", ""))
            'A1 = "3"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(980, 2730))

            ''PayNo4
            'Dim S5 As String = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("payid4").ToString(), dttmpLine.Rows(0)("payid4").ToString())
            'Dim S6 As String = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("AppointDate4").ToString(), dttmpLine.Rows(0)("AppointDate4").ToString())
            'Dim S7 As String = If(dttmpLine.Rows(0)("totalpay4").ToString() <> "", dttmpLine.Rows(0)("totalpay4").ToString() & ".00", "")
            'Dim S8 As String = If(dttmpLine.Rows(0)("Typepay4").ToString() = "1", "เงินสด", If(dttmpLine.Rows(0)("Typepay4").ToString() = "2", "บัตรเครดิต", ""))
            ''payid5
            'Dim t5 As String = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("payid5").ToString(), dttmpLine.Rows(0)("payid5").ToString())
            'Dim t6 As String = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("AppointDate5").ToString(), dttmpLine.Rows(0)("AppointDate5").ToString())
            'Dim t7 As String = If(dttmpLine.Rows(0)("totalpay5").ToString() <> "", dttmpLine.Rows(0)("totalpay5").ToString() & ".00", "")
            'Dim t8 As String = If(dttmpLine.Rows(0)("Typepay5").ToString() = "1", "เงินสด", If(dttmpLine.Rows(0)("Typepay5").ToString() = "2", "บัตรเครดิต", ""))
            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("payid5").ToString(), dttmpLine.Rows(0)("payid5").ToString())
            'A1 = "5"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1310, 2650))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("AppointDate5").ToString(), dttmpLine.Rows(0)("AppointDate5").ToString())
            'A1 = "5"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1510, 2650))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("totalpay5").ToString() <> "  ", dttmpLine.Rows(0)("totalpay5").ToString() & ".00", "")
            'A1 = "5"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1840, 2650))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay5").ToString() = "1", "เงินสด", If(dttmpLine.Rows(0)("Typepay5").ToString() = "2", "บัตรเครดิต", ""))
            'A1 = "5"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(2140, 2650))

            ''payid6
            'Dim U5 As String = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("payid6").ToString(), dttmpLine.Rows(0)("payid6").ToString())
            'Dim U6 As String = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("AppointDate6").ToString(), dttmpLine.Rows(0)("AppointDate6").ToString())
            'Dim U7 As String = If(dttmpLine.Rows(0)("totalpay6").ToString() <> "", dttmpLine.Rows(0)("totalpay6").ToString() & ".00", "")
            'Dim U8 As String = If(dttmpLine.Rows(0)("Typepay6").ToString() = "1", "เงินสด", If(dttmpLine.Rows(0)("Typepay6").ToString() = "2", "บัตรเครดิต", ""))
            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("payid6").ToString(), dttmpLine.Rows(0)("payid6").ToString())

            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1310, 2730))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("AppointDate6").ToString(), dttmpLine.Rows(0)("AppointDate6").ToString())
            ' A1 = "6"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1510, 2730))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("totalpay6").ToString() <> "  ", dttmpLine.Rows(0)("totalpay6").ToString() & ".00", "")
            ' A1 = "6"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1840, 2730))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay6").ToString() = "1", "เงินสด", If(dttmpLine.Rows(0)("Typepay6").ToString() = "2", "บัตรเครดิต", ""))
            'A1 = "6"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(2140, 2730))

            A1 = "*** บริษัทฯ ขอสงวนสิทธิ์ในการคิดค่าธรรมเนียมยกเลิกทั่วไป 350 บาท, ขายรถ 500 บาท"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(680, 2810))



            '3.save To Server 
            '3.1 สร้าง Folder

            Dim CreateFolder1 As String = "D:\LINE\" + lblApp.Text
            CreateFolder(CreateFolder1)
            'Dim destPath2 As String = Server.MapPath("~/LINE/") + lblApp.Text + "\" + lblApp.Text + "_ใบคำขอเอาประกัน.jpg"
            CreateFolder1 = CreateFolder1 + "\" + lblApp.Text + "_ใบคำขอเอาประกัน.jpg"
            bm.Save(CreateFolder1)
            gra.Dispose()
            bm.Dispose()



            Dim queryRefNO = New System.Text.StringBuilder()
            ''ถ้าชำระเป็นเงินสด Gen Payment ยกเลิก ให้ทำทุกเคส

            'queryRefNO.Append(" select typePay from tblapppay ")
            'queryRefNO.Append(" where payid=1 and typePay=1 and AppID=" & lblApp.Text)
            'dttmpLine = DataAccess.DataRead(query.ToString)
            'If dttmpLine.Rows.Count > 0 Then
            '    LINEGenPayment()
            'End If
            LINEGenPayment()

            ' Insert To Table
            ' TblLogLineAppStore
            'Dim Command As SqlCommand
            'queryRefNO.Append("insert into TblLogLineAppStore (AppID) values (" & lblApp.Text & ") ")
            'Command = New SqlCommand(queryRefNO.ToString, Conn)
            'Command.ExecuteNonQuery()
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "ViewHistory", "GenPYDetail(" & lblApp.Text & ",1)", True)
        End If
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "script", "<script>window.open('GenPaymentDatail.aspx?AppID=" & lblApp.Text & "','Application');</script>")
        Conn.Close()
    End Sub


    'Private Sub LINE_Address(ByVal dttmp As DataTable, ByRef B1 As String, ByRef B2 As String)
    '    Dim str_Adress As String = ""


    '    If dttmp.Rows(0)("Address").ToString() <> "" Then
    '        str_Adress += "เลขที่ " & dttmp.Rows(0)("Address").ToString() & " "
    '    End If
    '    If dttmp.Rows(0)("Moo").ToString() <> "" Then
    '        str_Adress += "ม." & dttmp.Rows(0)("Moo").ToString() & " "
    '    End If
    '    If dttmp.Rows(0)("Villege").ToString() <> "" Then
    '        str_Adress += dttmp.Rows(0)("Villege").ToString() & " "
    '    End If
    '    If dttmp.Rows(0)("Building").ToString() <> "" Then
    '        str_Adress += dttmp.Rows(0)("Building").ToString() & " "
    '    End If
    '    If dttmp.Rows(0)("HomeFloor").ToString() <> "" Then
    '        str_Adress += dttmp.Rows(0)("HomeFloor").ToString() & " "
    '    End If
    '    If dttmp.Rows(0)("HomeRoom").ToString() <> "" Then
    '        str_Adress += dttmp.Rows(0)("HomeRoom").ToString() & " "
    '    End If
    '    'crlf = chr(13) ; 
    '    If dttmp.Rows(0)("Moo").ToString() <> "" Or dttmp.Rows(0)("Address").ToString() <> "" Or dttmp.Rows(0)("Villege").ToString() <> "" Or dttmp.Rows(0)("Building").ToString() <> "" Or dttmp.Rows(0)("HomeFloor").ToString() <> "" Or dttmp.Rows(0)("HomeRoom").ToString() <> "" Then
    '        str_Adress += Chr(13)
    '    End If
    '    If dttmp.Rows(0)("Soi").ToString() <> "" Then
    '        str_Adress += "ซ." & dttmp.Rows(0)("Soi").ToString() & " "
    '    End If
    '    If dttmp.Rows(0)("Road").ToString() <> "" Then
    '        str_Adress += "ถ." & dttmp.Rows(0)("Road").ToString() & " "
    '    End If
    '    If dttmp.Rows(0)("Soi").ToString() <> "" Or dttmp.Rows(0)("Road").ToString() <> "" Then
    '        str_Adress += Chr(13)
    '    End If
    '    If dttmp.Rows(0)("Province").ToString() <> "กรุงเทพมหานคร" Then
    '        If dttmp.Rows(0)("SubDist").ToString() <> "" Then
    '            str_Adress += Environment.NewLine & "ต." & dttmp.Rows(0)("SubDist").ToString() & " "
    '        End If
    '    End If
    '    If dttmp.Rows(0)("Province").ToString() = "กรุงเทพมหานคร" Then
    '        If dttmp.Rows(0)("SubDist").ToString() <> "" Then
    '            str_Adress += Environment.NewLine & "แขวง" & dttmp.Rows(0)("SubDist").ToString() & " "
    '        End If
    '    End If

    '    If dttmp.Rows(0)("Province").ToString() <> "กรุงเทพมหานคร" Then
    '        If dttmp.Rows(0)("Dist").ToString() <> "" Then
    '            str_Adress += "อ." & dttmp.Rows(0)("Dist").ToString() & " "
    '        End If
    '    End If
    '    If dttmp.Rows(0)("Province").ToString() = "กรุงเทพมหานคร" Then
    '        If dttmp.Rows(0)("Dist").ToString() <> "" Then
    '            str_Adress += "เขต" & dttmp.Rows(0)("Dist").ToString() & " "
    '        End If
    '    End If
    '    str_Adress += Chr(13)
    '    If dttmp.Rows(0)("Province").ToString() <> "กรุงเทพมหานคร" Then
    '        If dttmp.Rows(0)("Province").ToString() <> "" Then
    '            str_Adress += Environment.NewLine & "จ." & dttmp.Rows(0)("Province").ToString() & " "
    '        End If
    '    End If
    '    If dttmp.Rows(0)("Province").ToString() = "กรุงเทพมหานคร" Then
    '        If dttmp.Rows(0)("Province").ToString() <> "" Then
    '            str_Adress += Environment.NewLine & dttmp.Rows(0)("Province").ToString() & " "
    '        End If
    '    End If

    '    If dttmp.Rows(0)("Zip").ToString() <> "" Then
    '        str_Adress += dttmp.Rows(0)("Zip").ToString() & " "
    '    End If

    '    B1 = str_Adress
    '    B2 = str_Adress
    'End Sub

    Private Sub LINE_Address(ByVal dttmp As DataTable, ByRef B1 As String, ByRef B2 As String)
        Dim str_Adress As String = ""
        Dim str_Adress2 As String = ""

        If dttmp.Rows(0)("Address").ToString() <> "" Then
            str_Adress += "เลขที่ " & dttmp.Rows(0)("Address").ToString() & " "
        End If

        If dttmp.Rows(0)("Moo").ToString() <> "" Then
            str_Adress += "ม." & dttmp.Rows(0)("Moo").ToString() & " "
        End If

        If dttmp.Rows(0)("Villege").ToString() <> "" Then
            str_Adress += dttmp.Rows(0)("Villege").ToString() & " "
        End If

        If dttmp.Rows(0)("Building").ToString() <> "" Then
            str_Adress += dttmp.Rows(0)("Building").ToString() & " "
        End If

        If dttmp.Rows(0)("HomeFloor").ToString() <> "" Then
            str_Adress += dttmp.Rows(0)("HomeFloor").ToString() & " "
        End If

        If dttmp.Rows(0)("HomeRoom").ToString() <> "" Then
            str_Adress += dttmp.Rows(0)("HomeRoom").ToString() & " "
        End If

        'crlf = chr(13) ; 
        If dttmp.Rows(0)("Moo").ToString() <> "" Or dttmp.Rows(0)("Address").ToString() <> "" Or dttmp.Rows(0)("Villege").ToString() <> "" Or dttmp.Rows(0)("Building").ToString() <> "" Or dttmp.Rows(0)("HomeFloor").ToString() <> "" Or dttmp.Rows(0)("HomeRoom").ToString() <> "" Then
            str_Adress += Chr(13)
        End If
        If dttmp.Rows(0)("Soi").ToString() <> "" Then
            str_Adress += "ซ." & dttmp.Rows(0)("Soi").ToString() & " "
        End If
        If dttmp.Rows(0)("Road").ToString() <> "" Then
            str_Adress += "ถ." & dttmp.Rows(0)("Road").ToString() & " "
        End If
        If dttmp.Rows(0)("Soi").ToString() <> "" Or dttmp.Rows(0)("Road").ToString() <> "" Then
            str_Adress += Chr(13)
        End If
        If dttmp.Rows(0)("Province").ToString() <> "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("SubDist").ToString() <> "" Then
                str_Adress += Environment.NewLine & "ต." & dttmp.Rows(0)("SubDist").ToString() & " "
            End If
        End If
        If dttmp.Rows(0)("Province").ToString() = "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("SubDist").ToString() <> "" Then
                str_Adress += Environment.NewLine & "แขวง" & dttmp.Rows(0)("SubDist").ToString() & " "
            End If
        End If

        If dttmp.Rows(0)("Province").ToString() <> "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("Dist").ToString() <> "" Then
                str_Adress += "อ." & dttmp.Rows(0)("Dist").ToString() & " "
            End If
        End If
        If dttmp.Rows(0)("Province").ToString() = "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("Dist").ToString() <> "" Then
                str_Adress += "เขต" & dttmp.Rows(0)("Dist").ToString() & " "
            End If
        End If
        str_Adress += Chr(13)
        If dttmp.Rows(0)("Province").ToString() <> "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("Province").ToString() <> "" Then
                str_Adress += Environment.NewLine & "จ." & dttmp.Rows(0)("Province").ToString() & " "
            End If
        End If
        If dttmp.Rows(0)("Province").ToString() = "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("Province").ToString() <> "" Then
                str_Adress += Environment.NewLine & dttmp.Rows(0)("Province").ToString() & " "
            End If
        End If

        If dttmp.Rows(0)("Zip").ToString() <> "" Then
            str_Adress += dttmp.Rows(0)("Zip").ToString() & " "
        End If

        '------
        If dttmp.Rows(0)("SAddress").ToString() <> "" Then
            str_Adress2 += "เลขที่ " & dttmp.Rows(0)("SAddress").ToString() & " "
        End If

        If dttmp.Rows(0)("SMoo").ToString() <> "" Then
            str_Adress2 += "ม." & dttmp.Rows(0)("SMoo").ToString() & " "
        End If

        If dttmp.Rows(0)("SVillege").ToString() <> "" Then
            str_Adress2 += dttmp.Rows(0)("SVillege").ToString() & " "
        End If

        If dttmp.Rows(0)("SBuilding").ToString() <> "" Then
            str_Adress2 += dttmp.Rows(0)("SBuilding").ToString() & " "
        End If

        If dttmp.Rows(0)("SHomeFloor").ToString() <> "" Then
            str_Adress2 += dttmp.Rows(0)("SHomeFloor").ToString() & " "
        End If

        If dttmp.Rows(0)("SHomeRoom").ToString() <> "" Then
            str_Adress2 += dttmp.Rows(0)("SHomeRoom").ToString() & " "
        End If

        'crlf = chr(13) ; 
        If dttmp.Rows(0)("SMoo").ToString() <> "" Or dttmp.Rows(0)("SAddress").ToString() <> "" Or dttmp.Rows(0)("SVillege").ToString() <> "" Or dttmp.Rows(0)("SBuilding").ToString() <> "" Or dttmp.Rows(0)("SHomeFloor").ToString() <> "" Or dttmp.Rows(0)("SHomeRoom").ToString() <> "" Then
            str_Adress2 += Chr(13)
        End If
        If dttmp.Rows(0)("SSoi").ToString() <> "" Then
            str_Adress2 += "ซ." & dttmp.Rows(0)("SSoi").ToString() & " "
        End If
        If dttmp.Rows(0)("SRoad").ToString() <> "" Then
            str_Adress2 += "ถ." & dttmp.Rows(0)("SRoad").ToString() & " "
        End If
        If dttmp.Rows(0)("SSoi").ToString() <> "" Or dttmp.Rows(0)("SRoad").ToString() <> "" Then
            str_Adress2 += Chr(13)
        End If
        If dttmp.Rows(0)("SProvince").ToString() <> "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("SSubDist").ToString() <> "" Then
                str_Adress2 += Environment.NewLine & "ต." & dttmp.Rows(0)("SSubDist").ToString() & " "
            End If
        End If
        If dttmp.Rows(0)("SProvince").ToString() = "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("SSubDist").ToString() <> "" Then
                str_Adress2 += Environment.NewLine & "แขวง" & dttmp.Rows(0)("SSubDist").ToString() & " "
            End If
        End If

        If dttmp.Rows(0)("SProvince").ToString() <> "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("SDist").ToString() <> "" Then
                str_Adress2 += "อ." & dttmp.Rows(0)("SDist").ToString() & " "
            End If
        End If
        If dttmp.Rows(0)("SProvince").ToString() = "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("SDist").ToString() <> "" Then
                str_Adress2 += "เขต" & dttmp.Rows(0)("SDist").ToString() & " "
            End If
        End If
        str_Adress2 += Chr(13)
        If dttmp.Rows(0)("SProvince").ToString() <> "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("SProvince").ToString() <> "" Then
                str_Adress2 += Environment.NewLine & "จ." & dttmp.Rows(0)("SProvince").ToString() & " "
            End If
        End If
        If dttmp.Rows(0)("SProvince").ToString() = "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("SProvince").ToString() <> "" Then
                str_Adress2 += Environment.NewLine & dttmp.Rows(0)("SProvince").ToString() & " "
            End If
        End If

        If dttmp.Rows(0)("SZip").ToString() <> "" Then
            str_Adress2 += dttmp.Rows(0)("SZip").ToString() & " "
        End If

        B1 = str_Adress
        B2 = str_Adress2
    End Sub

    Private Sub LINE_Phone(ByVal productid As String, ByRef C2 As String)

        If productid = "14" Then
            C2 = "0-2285-8000"
        ElseIf productid = "8" Then
            C2 = "0-2670-4444"
        ElseIf productid = "20" Then
            C2 = "0-2640-4500"
        ElseIf productid = "84" Then
            C2 = "0-2695-0700"
        ElseIf productid = "83" Then
            C2 = "1748"
        ElseIf productid = "10" Then
            C2 = "02-022-1100"
        ElseIf productid = "18" Then
            C2 = "0-2792-5500"
        ElseIf productid = "9" Then
            C2 = "1557"
        ElseIf productid = "13" Then
            C2 = "1790"
        ElseIf productid = "15" Then
            C2 = "0-2869-3333"
        ElseIf productid = "63" Then
            C2 = "02-624-1111"
        ElseIf productid = "24" Then
            C2 = "02-257-8080"
        ElseIf productid = "71" Then
            C2 = "02-209-3299"
        ElseIf productid = "52" Then
            C2 = "1726"
        End If
    End Sub

    Public Sub CreateFolder(ByVal DirInfo_1 As String)

        Dim DirInfo = New DirectoryInfo(DirInfo_1)
        If Not DirInfo.Exists Then
            DirInfo.Create()
        End If
    End Sub
    Private Sub LINEGenPayment()

        Dim dttmpRefNO As New DataTable
        Dim queryRefNO = New System.Text.StringBuilder()

        queryRefNO.Append(" Select  tblcar.refno As 'refno1' ")
        queryRefNO.Append(", tblapplication.AppID as 'refno2'")
        queryRefNO.Append(" , isnull(Tbl_ProductType.ProTypeName,'') as 'Desc'")
        queryRefNO.Append(" , Tbl_Type.TypeName as 'Desc1'")
        queryRefNO.Append(" , TblCustomerInit.InitTH+' '+ isnull(tblcustomer.FNameTH,'')+' '+isnull(tblcustomer.LNameTH,'') as 'customername'")
        queryRefNO.Append(" , tblcar.Carid")
        queryRefNO.Append(", (select count(*) from tblapppay where appid=tblapplication.AppID) As CountR  ")

        queryRefNO.Append(" , cast(p1.TotalPay As Decimal(10,2)) As 'Pay1'")
        queryRefNO.Append(" , SUBSTRING(convert(varchar,p1.AppointDate,103), 1, 2) + '/'")
        queryRefNO.Append(" + SUBSTRING(convert(varchar,p1.AppointDate,103), 4, 2)")
        queryRefNO.Append("   + '/'+convert(varchar, (SUBSTRING(convert(varchar,p1.AppointDate,103), 7, 4)+543)) as 'AppointDate1' ")


        queryRefNO.Append(" , cast(p2.TotalPay As Decimal(10,2)) As 'Pay2'")
        queryRefNO.Append(" , SUBSTRING(convert(varchar,p2.AppointDate,103), 1, 2) + '/'")
        queryRefNO.Append(" + SUBSTRING(convert(varchar,p2.AppointDate,103), 4, 2)")
        queryRefNO.Append("   + '/'+convert(varchar, (SUBSTRING(convert(varchar,p2.AppointDate,103), 7, 4)+543)) as 'AppointDate2' ")

        queryRefNO.Append(" , cast(p3.TotalPay As Decimal(10,2)) As 'Pay3'")
        queryRefNO.Append(" , SUBSTRING(convert(varchar,p3.AppointDate,103), 1, 2) + '/'")
        queryRefNO.Append(" + SUBSTRING(convert(varchar,p3.AppointDate,103), 4, 2)")
        queryRefNO.Append("   + '/'+convert(varchar, (SUBSTRING(convert(varchar,p3.AppointDate,103), 7, 4)+543)) as 'AppointDate3' ")

        queryRefNO.Append(" , cast(p4.TotalPay As Decimal(10,2)) As 'Pay4'")
        queryRefNO.Append(" , SUBSTRING(convert(varchar,p4.AppointDate,103), 1, 2) + '/'")
        queryRefNO.Append(" + SUBSTRING(convert(varchar,p4.AppointDate,103), 4, 2)")
        queryRefNO.Append("   + '/'+convert(varchar, (SUBSTRING(convert(varchar,p4.AppointDate,103), 7, 4)+543)) as 'AppointDate4' ")
        queryRefNO.Append(" , cast(p5.TotalPay As Decimal(10,2)) As 'Pay5'")
        queryRefNO.Append(" , SUBSTRING(convert(varchar,p5.AppointDate,103), 1, 2) + '/'")
        queryRefNO.Append(" + SUBSTRING(convert(varchar,p5.AppointDate,103), 4, 2)")
        queryRefNO.Append("   + '/'+convert(varchar, (SUBSTRING(convert(varchar,p5.AppointDate,103), 7, 4)+543)) as 'AppointDate5' ")

        queryRefNO.Append(" , cast(p6.TotalPay As Decimal(10,2)) As 'Pay6'")
        queryRefNO.Append(" , SUBSTRING(convert(varchar,p6.AppointDate,103), 1, 2) + '/'")
        queryRefNO.Append(" + SUBSTRING(convert(varchar,p6.AppointDate,103), 4, 2)")
        queryRefNO.Append("   + '/'+convert(varchar, (SUBSTRING(convert(varchar,p6.AppointDate,103), 7, 4)+543)) as 'AppointDate6' ")

        queryRefNO.Append(" ,case when tblapplication.isprovalue=1 then ")
        queryRefNO.Append(" 	  SUBSTRING(Convert(varchar, tblapplication.protectdate, 103), 1, 2) + '/' ")
        queryRefNO.Append("       + SUBSTRING(convert(varchar,tblapplication.protectdate,103), 4, 2)")
        queryRefNO.Append("       + '/'+convert(varchar, (SUBSTRING(convert(varchar,tblapplication.protectdate,103), 7, 4)+543))")
        queryRefNO.Append("      when tblapplication.IsCarpet=1 then ")
        queryRefNO.Append(" 	 SUBSTRING(Convert(varchar, tblapplication.ProtectDateCarpet, 103), 1, 2) + '/'")
        queryRefNO.Append("      + SUBSTRING(convert(varchar,tblapplication.ProtectDateCarpet,103), 4, 2)")
        queryRefNO.Append("      + '/'+convert(varchar, (SUBSTRING(convert(varchar,tblapplication.ProtectDateCarpet,103), 7, 4)+543))")
        queryRefNO.Append(" 	Else")
        queryRefNO.Append("            '' end as 'protectdate'")

        queryRefNO.Append(",case when tblapplication.isprovalue=1 then tblapplication.ProValue else '0.00' end as 'provalue'")
        queryRefNO.Append(",case when tblapplication.IsCarpet=1 then tblapplication.CarPet else '0.00' end as 'CarPet',isnull(p1.PayID,0) as 'p1',   isnull(p2.PayID,0) as 'p2', isnull(p3.PayID,0) as 'p3', isnull(p4.PayID,0) as 'p4', isnull(p5.PayID,0) as 'p5', isnull(p6.PayID,0) as 'p6'")
        queryRefNO.Append("         From tblapplication  ")
        queryRefNO.Append(" inner Join Tbl_ProductType on tblapplication.ProDuctID=Tbl_ProductType.ProTypeID ")
        queryRefNO.Append(" inner Join Tbl_Type on Tbl_Type.Typeid=tblapplication.Typeprovalue")
        queryRefNO.Append(" inner Join tblcar on tblapplication.idcar=tblcar.idcar ")
        queryRefNO.Append(" inner Join tblcustomer on tblapplication.cusid=tblcustomer.cusid ")
        queryRefNO.Append(" inner Join TblCustomerInit on tblcustomer.InitID=TblCustomerInit.InitID ")
        queryRefNO.Append(" inner Join TblApppay p1 on tblapplication.appid=p1.AppID And p1.PayID=1 ")
        queryRefNO.Append(" Left  Join TblApppay p2 on tblapplication.appid=p2.AppID And p2.PayID=2 ")
        queryRefNO.Append(" Left  Join TblApppay p3 on tblapplication.appid=p3.AppID And p3.PayID=3 ")
        queryRefNO.Append(" Left  Join TblApppay p4 on tblapplication.appid=p4.AppID And p4.PayID=4 ")
        queryRefNO.Append(" Left  Join TblApppay p5 on tblapplication.appid=p5.AppID And p5.PayID=5 ")
        queryRefNO.Append(" Left  Join TblApppay p6 on tblapplication.appid=p6.AppID And p6.PayID=6 ")
        queryRefNO.Append(" where  tblapplication.AppID=" & lblApp.Text)


        dttmpRefNO = DataAccess.DataRead(queryRefNO.ToString)
        If dttmpRefNO.Rows.Count = 1 Then

            Dim refno As String = dttmpRefNO.Rows(0)("refno1").ToString()
            Dim refno2 As String = dttmpRefNO.Rows(0)("refno2").ToString()
            Dim CountR As String = dttmpRefNO.Rows(0)("CountR").ToString()
            Dim ProTypeName As String = dttmpRefNO.Rows(0)("Desc").ToString()
            Dim Desc1 As String = dttmpRefNO.Rows(0)("Desc1").ToString()
            Dim customername As String = dttmpRefNO.Rows(0)("customername").ToString()
            Dim Carid As String = dttmpRefNO.Rows(0)("Carid").ToString()
            Dim protectdate As String = dttmpRefNO.Rows(0)("protectdate").ToString()
            Dim provalue As Decimal = dttmpRefNO.Rows(0)("provalue")
            Dim CarPet As Decimal = dttmpRefNO.Rows(0)("CarPet")
            Dim SumTotal As Decimal = provalue + CarPet

            Dim Pay1 As Decimal = dttmpRefNO.Rows(0)("Pay1")
            Dim AppointDate1 As String = dttmpRefNO.Rows(0)("AppointDate1").ToString()

            Dim strMoney As String = "0"
            Dim P_Server As String = ""
            If CountR = "1" Then
                P_Server = "~/images/LINE/pay01.jpg"
            ElseIf dttmpRefNO.Rows(0)("CountR") < 4 Then
                P_Server = "~/images/LINE/pay03.jpg"
            Else
                P_Server = "~/images/LINE/pay04.jpg"
            End If
            strMoney = Replace(strMoney, ".", "")
            strMoney = Replace(strMoney, ",", "")

            Dim barcodestr As String = "|010755800027000" & Chr(13) & refno & Chr(13) & lblApp.Text & Chr(13) & strMoney
            Dim myimg As Image = Code128Rendering.MakeBarcodeImage(barcodestr, 1, True)
            Dim barcodesre1 As String = "|010755800027000" & refno & lblApp.Text & strMoney

            Dim folder As String = Server.MapPath(P_Server)

            Dim bm As New Bitmap(folder)
            Dim FontName As String = "Prompt Medium"
            Dim gra As Graphics = Graphics.FromImage(bm)

            gra.DrawString(customername, New Font(FontName, 22), Brushes.Navy, New PointF(75, 204))
            gra.DrawString(Carid, New Font(FontName, 22), Brushes.Navy, New PointF(210, 255))
            gra.DrawString(protectdate, New Font(FontName, 22), Brushes.Navy, New PointF(190, 305))
            gra.DrawString(ProTypeName, New Font(FontName, 18), Brushes.Black, New PointF(55, 400))

            If provalue = 0 Then
                gra.DrawString(provalue.ToString("N2"), New Font(FontName, 18), Brushes.Black, New PointF(560, 440))
            ElseIf provalue > 9999 Then
                gra.DrawString(provalue.ToString("N2"), New Font(FontName, 18), Brushes.Black, New PointF(500, 440))
            Else
                gra.DrawString(provalue.ToString("N2"), New Font(FontName, 18), Brushes.Black, New PointF(520, 440))
            End If

            gra.DrawString(" บาท", New Font(FontName, 18), Brushes.Black, New PointF(620, 440))
            gra.DrawString("ประกันภาคสมัครใจ " + Desc1, New Font(FontName, 18), Brushes.Black, New PointF(55, 440))
            gra.DrawString("ประกันภาคบังคับ (พ.ร.บ)", New Font(FontName, 18), Brushes.Black, New PointF(55, 480))

            If CarPet = 0 Then
                gra.DrawString(CarPet.ToString("N2"), New Font(FontName, 18), Brushes.Black, New PointF(560, 480))
            ElseIf CarPet > 999 Then
                gra.DrawString(CarPet.ToString("N2"), New Font(FontName, 18), Brushes.Black, New PointF(500, 480))
            Else
                gra.DrawString(CarPet.ToString("N2"), New Font(FontName, 18), Brushes.Black, New PointF(535, 480))
            End If

            gra.DrawString(" บาท", New Font(FontName, 18), Brushes.Black, New PointF(620, 480))
            gra.DrawString("ยอดเงินรวมที่ต้องชำระ", New Font(FontName, 18), Brushes.Black, New PointF(55, 530))
            gra.DrawString("(ยอดเงินรวมที่ต้องชำระ)", New Font(FontName, 14), Brushes.Black, New PointF(55, 560))

            If CountR = "1" Then
                If SumTotal > 9999 Then
                    gra.DrawString(SumTotal.ToString("N2"), New Font(FontName, 24), Brushes.Black, New PointF(460, 520))
                Else
                    gra.DrawString(SumTotal.ToString("N2"), New Font(FontName, 24), Brushes.Black, New PointF(480, 520))
                End If
            Else
                If SumTotal > 9999 Then
                    gra.DrawString(SumTotal.ToString("N2"), New Font(FontName, 24), Brushes.Black, New PointF(460, 520))
                Else
                    gra.DrawString(SumTotal.ToString("N2"), New Font(FontName, 24), Brushes.Black, New PointF(490, 520))
                End If
            End If


            gra.DrawString(" บาท", New Font(FontName, 18), Brushes.Black, New PointF(620, 530))

            Dim qe As MessagingToolkit.QRCode.Codec.QRCodeEncoder = New MessagingToolkit.QRCode.Codec.QRCodeEncoder
            Dim L1_T1 As String = "|010755800027000" & Chr(13) & refno & Chr(13) & refno2 & Chr(13) & strMoney
            Dim ALL As String = L1_T1
            Dim myimg1 As Image = qe.Encode(ALL)

            If CountR = "1" Then

                gra.DrawString(refno, New Font(FontName, 18), Brushes.Black, New PointF(200, 990))
                gra.DrawString(refno2, New Font(FontName, 18), Brushes.Black, New PointF(550, 990))

                gra.DrawImage(myimg1, 450, 650, 150, 150)
                gra.DrawImage(myimg, 120, 880, myimg.Width, myimg.Height)

            Else
                gra.DrawString("ผ่อนชำระ", New Font(FontName, 18), Brushes.Navy, New PointF(55, 580))
                gra.DrawString(CountR, New Font(FontName, 18), Brushes.Navy, New PointF(600, 580))
                gra.DrawString(" งวด", New Font(FontName, 18), Brushes.Navy, New PointF(620, 580))

                gra.DrawString("งวดแรกที่ต้องชำระ", New Font(FontName, 18), Brushes.Navy, New PointF(55, 620))



                If Pay1 > 9999 Then
                    gra.DrawString(Pay1.ToString("N2"), New Font(FontName, 18), Brushes.Navy, New PointF(500, 620))
                Else
                    gra.DrawString(Pay1.ToString("N2"), New Font(FontName, 18), Brushes.Navy, New PointF(520, 620))
                End If
                gra.DrawString(" บาท", New Font(FontName, 18), Brushes.Navy, New PointF(620, 620))

                gra.DrawString("ผ่อน 0 % " & CountR & " งวด", New Font(FontName, 20, FontStyle.Bold), Brushes.Black, New PointF(280, 700))


                If dttmpRefNO.Rows(0)("p1").ToString() <> "0" Then
                    gra.DrawString("งวดที่ 1", New Font(FontName, 18), Brushes.Black, New PointF(140, 750))
                    gra.DrawString(AppointDate1, New Font(FontName, 18), Brushes.Black, New PointF(280, 750))
                    gra.DrawString(Pay1.ToString("N2") + " บาท", New Font(FontName, 18), Brushes.Black, New PointF(450, 750))
                End If
                If dttmpRefNO.Rows(0)("p2").ToString() <> "0" Then
                    Dim Pay2 As Decimal = dttmpRefNO.Rows(0)("Pay2")
                    Dim AppointDate2 As String = dttmpRefNO.Rows(0)("AppointDate2").ToString()

                    gra.DrawString("งวดที่ 2", New Font(FontName, 18), Brushes.Black, New PointF(140, 790))
                    gra.DrawString(AppointDate2, New Font(FontName, 18), Brushes.Black, New PointF(280, 790))
                    gra.DrawString(Pay2.ToString("N2") + " บาท", New Font(FontName, 18), Brushes.Black, New PointF(450, 790))
                End If
                If dttmpRefNO.Rows(0)("p3").ToString() <> "0" Then
                    Dim Pay3 As Decimal = dttmpRefNO.Rows(0)("Pay3")
                    Dim AppointDate3 As String = dttmpRefNO.Rows(0)("AppointDate3").ToString()

                    gra.DrawString("งวดที่ 3", New Font(FontName, 18), Brushes.Black, New PointF(140, 830))
                    gra.DrawString(AppointDate3, New Font(FontName, 18), Brushes.Black, New PointF(280, 830))
                    gra.DrawString(Pay3.ToString("N2") + " บาท", New Font(FontName, 18), Brushes.Black, New PointF(450, 830))
                End If



                If dttmpRefNO.Rows(0)("p4").ToString() <> "0" Then
                    Dim Pay4 As Decimal = dttmpRefNO.Rows(0)("Pay4")
                    Dim AppointDate4 As String = dttmpRefNO.Rows(0)("AppointDate4").ToString()

                    gra.DrawString("งวดที่ 4", New Font(FontName, 18), Brushes.Black, New PointF(140, 870))
                    gra.DrawString(AppointDate4, New Font(FontName, 18), Brushes.Black, New PointF(280, 870))
                    gra.DrawString(Pay4.ToString("N2") + " บาท", New Font(FontName, 18), Brushes.Black, New PointF(450, 870))
                End If
                If dttmpRefNO.Rows(0)("p5").ToString() <> "0" Then
                    Dim Pay5 As Decimal = dttmpRefNO.Rows(0)("Pay5")
                    Dim AppointDate5 As String = dttmpRefNO.Rows(0)("AppointDate5").ToString()

                    gra.DrawString("งวดที่ 5", New Font(FontName, 18), Brushes.Black, New PointF(140, 910))
                    gra.DrawString(AppointDate5, New Font(FontName, 18), Brushes.Black, New PointF(280, 910))
                    gra.DrawString(Pay5.ToString("N2") + " บาท", New Font(FontName, 18), Brushes.Black, New PointF(450, 910))
                End If
                If dttmpRefNO.Rows(0)("p6").ToString() <> "0" Then
                    Dim Pay6 As Decimal = dttmpRefNO.Rows(0)("Pay6")
                    Dim AppointDate6 As String = dttmpRefNO.Rows(0)("AppointDate6").ToString()
                    gra.DrawString("งวดที่ 6", New Font(FontName, 18), Brushes.Black, New PointF(140, 950))
                    gra.DrawString(AppointDate6, New Font(FontName, 18), Brushes.Black, New PointF(280, 950))
                    gra.DrawString(Pay6.ToString("N2") + " บาท", New Font(FontName, 18), Brushes.Black, New PointF(450, 950))
                End If

                If dttmpRefNO.Rows(0)("CountR") < 4 Then
                    gra.DrawString(refno, New Font(FontName, 18), Brushes.Black, New PointF(200, 1300))
                    gra.DrawString(refno2, New Font(FontName, 18), Brushes.Black, New PointF(550, 1300))

                    gra.DrawImage(myimg1, 450, 950, 150, 150)
                    gra.DrawImage(myimg, 120, 1180, myimg.Width, myimg.Height)
                Else
                    gra.DrawString(refno, New Font(FontName, 18), Brushes.Black, New PointF(200, 1440))
                    gra.DrawString(refno2, New Font(FontName, 18), Brushes.Black, New PointF(550, 1440))

                    gra.DrawImage(myimg1, 450, 1100, 150, 150)
                    gra.DrawImage(myimg, 120, 1310, myimg.Width, myimg.Height)
                End If


            End If




            Dim destPath As String = "D:\\LINE\\" + lblApp.Text + "\" + lblApp.Text + "_Payment.jpg"

            bm.Save(destPath)
            gra.Dispose()
            bm.Dispose()

            'Ref.IT Service XXXXX GEN Give PBIG Use in Payment Form
            Dim destPathQR As String = "D:\\LINE\\" + lblApp.Text + "\qr.bmp"
            Dim bm1 As New Bitmap(myimg1)
            bm1.Save(destPathQR)
            bm1.Dispose()

        End If
    End Sub

End Class
