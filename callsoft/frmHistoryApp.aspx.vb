Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Imports System.IO
Partial Class Modules_Manager_Manage_Tsr_frmHistoryApp
    Inherits System.Web.UI.Page
    Dim ISODate As New ISODate
    Dim com As SqlCommand
    Dim dt As DataTable
    Dim Connection As New SqlConnection(ConfigurationManager.ConnectionStrings("asnbroker").ConnectionString)
    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click

        Dim strProtectdate As String = ""
        If ChkProtectDate.Checked = True Then
            If txtProtecdateS.Text <> "" And txtProtecdateE.Text <> "" Then
                strProtectdate = " and CONVERT(VarChar,a1.ProtectDate,111) between '" & ISODate.SetISODate("en", txtProtecdateS.Text.Trim) & "' and '" & ISODate.SetISODate("en", txtProtecdateE.Text.Trim) & "'"
            End If

        End If

        UWGShowPayment.DataSource = Nothing
        UWGShowPayment.DataBind()

        Connection.Open()
        Dim strQuery As String
        Dim Command As SqlCommand
        Dim DataReader As SqlDataReader
        strQuery = "   Select "
        strQuery += "         a3.FNameTH + ' ' + a3.LNameTH + ' ' + a2.CarID   as CusName"
        strQuery += "         ,a11.GroupName "
        strQuery += "         ,a2.CarBrand "
        strQuery += "         ,a2.CarSeries "
        strQuery += "         ,CONVERT(VARCHAR(2),day(a1.SuccessDate))+'/'+CONVERT(VARCHAR(2),month(a1.SuccessDate)) +'/'+CONVERT(VARCHAR(4),year(a1.SuccessDate)+543) as SuccessDate "
        strQuery += "         ,a1.QcSuccessDate "
        strQuery += "        ,CONVERT(VARCHAR(2),day(a1.ProtectDate))+'/'+CONVERT(VARCHAR(2),month(a1.ProtectDate)) +'/'+CONVERT(VARCHAR(4),year(a1.ProtectDate)+543) as ProtectDate"
        strQuery += "         ,a6.FName + ' ' + a6.LName as TsrName"
        strQuery += "         ,a4.ProTypeName + ' ' + a5.TypeName  as ProTypeName"
        strQuery += "         ,case a7.TypePay when 2 then 'credit' else 'Payment' end as TypePay"
        strQuery += "         ,case a1.Isprovalue when 1 then a1.ProValue else 0 end  as ProValue"
        strQuery += "         ,case a1.IsCarpet when 1 then a1.CarPet else 0 end  as Carpet"
        strQuery += "         ,case a1.Isprovalue when 1 then a1.ProValue else 0 end  + case a1.IsCarpet when 1 then a1.CarPet else 0 end as TotalValue"
        strQuery += "         ,a1.submitdate "
        strQuery += "         ,a8.Astatusname "
        strQuery += "         ,case when a9.PayDate is null then '' else CONVERT(VARCHAR(2),day(a9.PayDate))+'/'+CONVERT(VARCHAR(2),month(a9.PayDate)) +'/'+CONVERT(VARCHAR(4),year(a9.PayDate)+543) end + '|' + CONVERT(VARCHAR(50),a9.PayValue) as Pay "
        strQuery += "        ,a10.StatusCode"
        strQuery += "        ,isnull(a13.PackageName +'('+ convert(varchar,a13.NetPremium)+')','-') as PackageNamePA"
        strQuery += "        ,a14.Detail "
        strQuery += "       from TblApplication a1"
        strQuery += "       Inner Join TblCar a2 on a2.idcar = a1.Idcar "
        strQuery += "       Inner Join TblCustomer a3 on a2.CusID = a3.cusid"
        strQuery += "       Inner Join Tbl_ProductType a4 on a1.ProDuctID = a4.ProTypeID "
        strQuery += "       Inner Join Tbl_Type a5 on a1.Typeprovalue = a5.Typeid "
        strQuery += "       Inner Join TblUser a6 on a2.AssignTo = a6.UserID "
        strQuery += "       Inner Join TblAppPay a7 on a1.appid = a7.AppID and PayID = 1"
        strQuery += "       Left Join Tbl_AStatus a8 on a1.Statusqc = a8.Astatusid "
        strQuery += "       Left Join Tblpayment a9 on a1.AppID = a9.AppID and a9.PayNo = 1"
        strQuery += "       Inner Join TblStatus a10 on a2.CurStatus  = a10.StatusID"
        strQuery += "       Inner Join TblSourceGroup a11 on a2.GroupID  = a11.GroupID"
        strQuery += "       Left Join TblApplicationPA a12 on a1.AppID=a12.AppID "
        strQuery += "       Left Join TblPackagePA a13 on a13.PackageID=a12.PackageID  "
        strQuery += "       left join TblAppSubmit a14 on a14.AppsubmitId=a1.Pkgid "


        strQuery += "       Where CONVERT(VarChar,a1.successdate,111) between '" & ISODate.SetISODate("en", txtdate1.Text.Trim) & "' and '" & ISODate.SetISODate("en", txtdate2.Text.Trim) & "' "
        strQuery += "       and case " & ddTsr.SelectedValue & "  when 0 then 0 else a6.UserID end = " & ddTsr.SelectedValue & "  and a6.SupID = " & ddSup.SelectedValue & "  and a2.CurStatus in(3,4) "

        strQuery += "       and case " & ddCompanyIns.SelectedValue & "  when 0 then 0 else a1.ProDuctID end = " & ddCompanyIns.SelectedValue & " "
        strQuery += " " & strProtectdate
        strQuery += " and a1.appstatus=1 "
        
        If ddpay.SelectedValue = "1" Then
            'ชำระแล้ว
            strQuery += " and a9.PayDate is not null "
        ElseIf ddpay.SelectedValue = "2" Then
            'ยังชำระแล้ว
            strQuery += " and a9.PayDate is  null "
        End If

            strQuery += " order by a1.SuccessDate"
            Command = New SqlCommand(strQuery, Connection)
            DataReader = Command.ExecuteReader()



            dt = New DataTable
        dt.Columns.Add("SuccessDate") '0
        dt.Columns.Add("Name Source List") '1
        dt.Columns.Add("ชื่อ-สกุล ลูกค้า") '2
        dt.Columns.Add("ยี่ห้อ") '3
        dt.Columns.Add("รุ่นรถ") '4
        dt.Columns.Add("บริษัทประกัน") '5   
        dt.Columns.Add("PK") '5  
        dt.Columns.Add("วันคุ้มครอง") '6
        dt.Columns.Add("เบี้ยประกัน") '7
        dt.Columns.Add("พรบ") '8
        dt.Columns.Add("เบี้ยรวม") '9
        dt.Columns.Add("การชำระ") '10
        dt.Columns.Add("submitdate") '11
        dt.Columns.Add("QcSuccess") '12
        dt.Columns.Add("TsrName") '13
        dt.Columns.Add("สถานะApp") '14
        dt.Columns.Add("วันที่ชำระ|จำนวนเงิน") '15
        dt.Columns.Add("StatusCode") '16
        dt.Columns.Add("PackageNamePA") '17

            Dim TotalProValue As Decimal = 0.0
            Dim TotalCarpet As Decimal = 0.0
            Dim Total As Decimal = 0.0

            Dim count As Integer = 0
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim dtr As DataRow = dt.NewRow
                    count += 1
                    If IsDBNull(DataReader("SuccessDate")) = False Then
                        dtr("SuccessDate") = DataReader("SuccessDate")
                    End If
                    If IsDBNull(DataReader("GroupName")) = False Then
                        dtr("Name Source List") = DataReader("GroupName")
                    End If
                    If IsDBNull(DataReader("CusName")) = False Then
                        dtr("ชื่อ-สกุล ลูกค้า") = DataReader("CusName")
                    End If
                    If IsDBNull(DataReader("CarBrand")) = False Then
                        dtr("ยี่ห้อ") = DataReader("CarBrand")
                    End If
                    If IsDBNull(DataReader("CarSeries")) = False Then
                        dtr("รุ่นรถ") = DataReader("CarSeries")
                End If

                    If IsDBNull(DataReader("ProTypeName")) = False Then
                        dtr("บริษัทประกัน") = DataReader("ProTypeName")
                End If
               

                    If IsDBNull(DataReader("ProtectDate")) = False Then
                        dtr("วันคุ้มครอง") = DataReader("ProtectDate")
                    End If
                    If IsDBNull(DataReader("ProValue")) = False Then
                        TotalProValue += DataReader("ProValue")
                        dtr("เบี้ยประกัน") = Format(DataReader("ProValue"), "###,###,##0.00")
                    End If
                    If IsDBNull(DataReader("Carpet")) = False Then
                        TotalCarpet += DataReader("Carpet")
                        dtr("พรบ") = Format(DataReader("Carpet"), "###,###,##0.00")
                    End If
                    If IsDBNull(DataReader("TotalValue")) = False Then
                        Total += DataReader("TotalValue")
                        dtr("เบี้ยรวม") = Format(DataReader("TotalValue"), "###,###,##0.00")
                    End If

                    If IsDBNull(DataReader("TypePay")) = False Then
                        dtr("การชำระ") = DataReader("TypePay")
                    End If
                    If IsDBNull(DataReader("submitdate")) = False Then
                        dtr("submitdate") = DataReader("submitdate")
                    End If
                    If IsDBNull(DataReader("QcSuccessDate")) = False Then
                        dtr("QcSuccess") = DataReader("QcSuccessDate")
                    End If
                    If IsDBNull(DataReader("TsrName")) = False Then
                        dtr("TsrName") = DataReader("TsrName")
                    End If
                    If IsDBNull(DataReader("Astatusname")) = False Then
                        dtr("สถานะApp") = DataReader("Astatusname")
                    End If
                    If IsDBNull(DataReader("Pay")) = False Then
                        dtr("วันที่ชำระ|จำนวนเงิน") = DataReader("Pay")
                    Else
                        dtr("วันที่ชำระ|จำนวนเงิน") = "|"
                    End If
                    If IsDBNull(DataReader("StatusCode")) = False Then
                        dtr("StatusCode") = DataReader("StatusCode")
                End If

                    If IsDBNull(DataReader("PackageNamePA")) = False Then
                        dtr("PackageNamePA") = DataReader("PackageNamePA")
                End If
                If IsDBNull(DataReader("Detail")) = False Then
                    dtr("PK") = DataReader("Detail")
                End If
              
                    dt.Rows.Add(dtr)
                End While
            End If
            DataReader.Close()

        If dt.Rows.Count > 0 Then

            UWGShowPayment.DataSource = dt
            UWGShowPayment.DataBind()
            UWGShowPayment.Columns(0).Width = 80 'SuccessDate
            UWGShowPayment.Columns(1).Width = 80 'NameSourceList
            UWGShowPayment.Columns(2).Width = 100 'ชื่อ-สกุล ลูกค้า
            UWGShowPayment.Columns(3).Width = 100  'ยี่ห้อ
            UWGShowPayment.Columns(4).Width = 100  'รุ่นรถ
            UWGShowPayment.Columns(5).Width = 150 'บริษัทประกัน
            UWGShowPayment.Columns(6).Width = 150 'pk
            UWGShowPayment.Columns(7).Width = 90 'วันคุ้มครอง
            UWGShowPayment.Columns(8).Width = 100 'เบี้ยประกัน
            UWGShowPayment.Columns(9).Width = 100 'พรบ
            UWGShowPayment.Columns(10).Width = 100 'เบี้ยรวม
            UWGShowPayment.Columns(11).Width = 90 'การชำระ
            UWGShowPayment.Columns(12).Width = 100 'submitdate
            UWGShowPayment.Columns(13).Width = 100 'QcSuccess
            UWGShowPayment.Columns(14).Width = 90 'TsrName
            UWGShowPayment.Columns(15).Width = 90 'สถานะApp
            UWGShowPayment.Columns(16).Width = 120 'วันที่ชำระ|จำนวนเงิน
            UWGShowPayment.Columns(17).Width = 90 'StatusCode
            UWGShowPayment.Columns(18).Width = 120 'PackageNamePA


            UWGShowPayment.Columns(7).Footer.Caption = "Total"
            UWGShowPayment.Columns(8).Footer.Caption = TotalProValue.ToString("n2")
            UWGShowPayment.Columns(9).Footer.Caption = TotalCarpet.ToString("n2")
            UWGShowPayment.Columns(10).Footer.Caption = Total.ToString("n2")

        End If
            Connection.Close()
            lblCase.Text = dt.Rows.Count

    End Sub
    'Protected Sub SqlHistoryApp_Selected(sender As Object, e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlHistoryApp.Selected
    '      lblCase.Text = e.AffectedRows
    '  End Sub

    Protected Sub Button2_Click(sender As Object, e As System.EventArgs) Handles Button2.Click
        GenExl()
        'Response.Redirect("~/Tmp/HisortyApp.xls")
    End Sub
    Protected Sub GenReport()
        'This Code is used to Convert to word document
        Dim reportWord As New ReportDocument  ' Report Name 
        Dim reportname As String
        reportname = Server.MapPath("~/Modules/Manager/Report/rptHistoryApp.rpt")

        Dim users As String = "sa"
        'Dim pass As String = "DTS2009"
        Dim pass As String = "asn@sr1"

        'Dim rpt As New CrystalReportViewer

        reportWord.Load(reportname)
        reportWord.SetDatabaseLogon(users, pass)
        reportWord.SetParameterValue("date1", ISODate.SetISODate("en", txtdate1.Text.Trim))
        reportWord.SetParameterValue("date2", ISODate.SetISODate("en", txtdate2.Text.Trim))
        reportWord.SetParameterValue("userID", ddTsr.SelectedValue)

        Dim strExportFile As String = Server.MapPath("~/Tmp/HisortyAppdd.xls")
        reportWord.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile
        reportWord.ExportOptions.ExportFormatType = ExportFormatType.ExcelRecord
        Dim objOptions As DiskFileDestinationOptions = New DiskFileDestinationOptions()
        objOptions.DiskFileName = strExportFile
        reportWord.ExportOptions.DestinationOptions = objOptions
        'reportWord.SetDataSource(myDS)
        reportWord.Export()
        objOptions = Nothing
        reportWord = Nothing

        'Dim CrExportOptions As ExportOptions
        'Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions()
        'Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()
        'CrDiskFileDestinationOptions.DiskFileName = Server.MapPath("~/TestWord.pdf")
        'CrExportOptions = reportWord.ExportOptions
        'With CrExportOptions
        '    .ExportDestinationType = ExportDestinationType.DiskFile
        '    .ExportFormatType = ExportFormatType.PortableDocFormat
        '    .DestinationOptions = CrDiskFileDestinationOptions
        '    .FormatOptions = CrFormatTypeOptions
        'End With
        'reportWord.Export()

        'Dim f As New SautinSoft.PdfFocus
        'f.OpenPdf(Server.MapPath("~/TestWord.pdf"))
        ''If f.PageCount > 0 Then
        'f.ImageOptions.Dpi = 300
        'f.ToMultipageTiff(Server.MapPath("~/TestWord.tiff"))
        'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "script", "alert('บันทึกข้อมูลเรียบร้อย');", True)
        'End If



    End Sub

    Protected Sub GenExl()
        'comment code เดิม
        'If GvHistoryApp.Rows.Count > 0 Then
        '    Dim attachment As String = "attachment; filename=Payment.xls"
        '    Response.ClearContent()
        '    Response.AddHeader("content-disposition", attachment)
        '    Response.ContentType = "application/vnd.ms-excel"

        '    Dim sw As New StringWriter
        '    Dim htw As New HtmlTextWriter(sw)
        '    Dim frm As New HtmlForm
        '    GvHistoryApp.Parent.Controls.Add(frm)
        '    frm.Attributes("runat") = "server"
        '    frm.Controls.Add(GvHistoryApp)
        '    frm.RenderControl(htw)
        '    Response.Write(sw.ToString())
        '    Response.End()

        'End If   
        'Add by na 26062015
        UltraWebGridExcelExporter1.ExportMode = Infragistics.WebUI.UltraWebGrid.ExcelExport.ExportMode.Download
        UltraWebGridExcelExporter1.DownloadName = "Payment.xls"
        UltraWebGridExcelExporter1.Export(UWGShowPayment)
        UWGShowPayment.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.Cookies("UserLevel").Value = 5 Then
                Button2.Visible = False
            End If
        End If
    End Sub
    Protected Sub UltraWebGridExcelExporter1_CellExported(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.ExcelExport.CellExportedEventArgs) Handles UltraWebGridExcelExporter1.CellExported

    End Sub

End Class
