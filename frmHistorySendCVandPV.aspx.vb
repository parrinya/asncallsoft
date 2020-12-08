Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Imports System.IO
Partial Class Modules_Manager_Manage_Tsr_frmHistorySendCVandPV
    Inherits System.Web.UI.Page
    Dim ISODate As New ISODate
    Dim com As SqlCommand
    Dim dt As DataTable
    Dim Connection As New SqlConnection(ConfigurationManager.ConnectionStrings("asnbroker").ConnectionString)
    Protected Sub GenExl()
        
        UltraWebGridExcelExporter1.ExportMode = Infragistics.WebUI.UltraWebGrid.ExcelExport.ExportMode.Download
        UltraWebGridExcelExporter1.DownloadName = "Payment.xls"
        UltraWebGridExcelExporter1.Export(UWGShowPayment)
        UWGShowPayment.DataBind()
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        GenExl()
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Dim strtmp As String = ""
        lblcount.Text = "0 รายการ"
        UWGShowPayment.DataSource = Nothing
        UWGShowPayment.DataBind()
        If name.Checked And txtname.Text <> "" Then 'ค้นหาตามชื่อ   
            strtmp = " and tblcustomer.FNameTH+' '+tblcustomer.LNameTH like   '%" + txtname.Text + "%'"
           
        ElseIf carid.Checked And txtcarid.Text <> "" Then
            strtmp = " and tblcar.carid like   '%" + txtcarid.Text + "%'"
           
        ElseIf appid.Checked And txtappid.Text <> "" Then
            strtmp = " and  tblapplication.AppID =   '%" + txtappid.Text + "%'"
        ElseIf all.Checked Then

            strtmp = " and CONVERT(VarChar,TblLogSendCVandPV.CreateDate,111) between '" & ISODate.SetISODate("en", txtdate1.Text.Trim) & "'and '" & ISODate.SetISODate("en", txtdate2.Text.Trim) & "' "
        Else
            Exit Sub
        End If

       

        Connection.Open()
        Dim strQuery As String
        Dim Command As SqlCommand
        Dim DataReader As SqlDataReader
        Dim tmplead As String = ""
        Dim tmpSup As String = ""
        Dim tmptsr As String = ""
        strQuery = " Select tblapplication.AppID"
        strQuery += "        ,Tbl_ProductType.ProTypeName"
        strQuery += "        ,tblcar.Carid"
        strQuery += "        ,tblcustomer.FNameTH+' '+tblcustomer.LNameTH as CusName"
        strQuery += "        ,case when tblapplication.IsProvalue=1 then   tblapplication.ProValue else 0 end as VMI"
        strQuery += "        ,case when tblapplication.IsCarpet=1   then   tblapplication.CarPet else 0 end as CMI"
        strQuery += "        ,CONVERT(VarChar,tblapplication.SuccessDate,103) as SuccessDate "
        strQuery += "        ,CONVERT(VarChar,tblapplication.QcSuccessDate,103) as QcSuccessDate "
        strQuery += "        ,tbluser.FName+' '+tbluser.LName as Tsrname"
        strQuery += "        ,tmp01.FName+' '+tmp01.LName as Sendname"
        strQuery += "        , CONVERT(VarChar,TblLogSendCVandPV.CreateDate,103) as CreateDate "
        strQuery += "        from TblLogSendCVandPV "
        strQuery += "        inner join tblapplication on TblLogSendCVandPV.AppID=tblapplication.Appid"
        strQuery += "        inner join tblcar on tblapplication.idcar=tblcar.idcar"
        strQuery += "        inner join tblcustomer on tblapplication.cusid=tblcustomer.cusid"
        strQuery += "        inner join Tbl_ProductType on tblapplication.ProDuctID=Tbl_ProductType.ProTypeID"
        strQuery += "        inner join tbluser on tblcar.AssignTo=tbluser.UserID"
        strQuery += "        inner join tbluser tmp on tmp.UserID=tbluser.SupID"
        strQuery += "        inner join tbluser tmp01 on tmp01.UserID=TblLogSendCVandPV.CreateID"


        strQuery += "        where 1=1 " & strtmp
        strQuery += "        and Case " & ddLead.SelectedValue & "  when -1 then -1 else tbluser.LeaderID end =" & ddLead.SelectedValue
        strQuery += "        and Case " & ddSup.SelectedValue & "  when -1 then -1 else tbluser.SupID end =" & ddSup.SelectedValue
        strQuery += "        and Case " & ddTsr.SelectedValue & "  when -1 then -1 else tblcar.AssignTo end =" & ddTsr.SelectedValue




        Command = New SqlCommand(strQuery, Connection)
        DataReader = Command.ExecuteReader()



        dt = New DataTable
        dt.Columns.Add("Appid")
        dt.Columns.Add("บริษัทประกัน")
        dt.Columns.Add("ชื่อ-สกุล ลูกค้า")
        dt.Columns.Add("ทะเบียน")
        dt.Columns.Add("เบี้ยประกัน")
        dt.Columns.Add("พรบ")
        dt.Columns.Add("SuccessDate")
        dt.Columns.Add("QcSuccessDate")
        dt.Columns.Add("TsrName")
        dt.Columns.Add("ผู้ทำรายการ")
        dt.Columns.Add("วันที่ส่ง")

      
        Dim count As Integer = 0
        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt.NewRow
                count += 1

                If IsDBNull(DataReader("Appid")) = False Then
                    dtr("Appid") = DataReader("Appid")
                End If
                If IsDBNull(DataReader("ProTypeName")) = False Then
                    dtr("บริษัทประกัน") = DataReader("ProTypeName")
                End If

                If IsDBNull(DataReader("Carid")) = False Then
                    dtr("ทะเบียน") = DataReader("Carid")
                End If

                If IsDBNull(DataReader("CusName")) = False Then
                    dtr("ชื่อ-สกุล ลูกค้า") = DataReader("CusName")
                End If

                If IsDBNull(DataReader("VMI")) = False Then
                    dtr("เบี้ยประกัน") = Format(DataReader("VMI"), "###,###,##0.00")
                End If
                If IsDBNull(DataReader("CMI")) = False Then
                    dtr("พรบ") = Format(DataReader("CMI"), "###,###,##0.00")
                End If
                If IsDBNull(DataReader("SuccessDate")) = False Then
                    dtr("SuccessDate") = DataReader("SuccessDate")
                End If

               
                If IsDBNull(DataReader("QcSuccessDate")) = False Then
                    dtr("QcSuccessDate") = DataReader("QcSuccessDate")
                End If
                If IsDBNull(DataReader("TsrName")) = False Then
                    dtr("TsrName") = DataReader("TsrName")
                End If
                If IsDBNull(DataReader("Sendname")) = False Then
                    dtr("ผู้ทำรายการ") = DataReader("Sendname")
                End If
                If IsDBNull(DataReader("CreateDate")) = False Then
                    dtr("วันที่ส่ง") = DataReader("CreateDate")
                End If
               


                dt.Rows.Add(dtr)
            End While
        End If
        DataReader.Close()

        If dt.Rows.Count > 0 Then
            UWGShowPayment.DataSource = dt
            UWGShowPayment.DataBind()
            UWGShowPayment.Columns(0).Width = 70
            UWGShowPayment.Columns(1).Width = 120
            UWGShowPayment.Columns(2).Width = 150
            UWGShowPayment.Columns(3).Width = 80
            UWGShowPayment.Columns(4).Width = 70
            UWGShowPayment.Columns(5).Width = 70
            UWGShowPayment.Columns(6).Width = 90
            UWGShowPayment.Columns(7).Width = 100
            UWGShowPayment.Columns(8).Width = 100
            UWGShowPayment.Columns(9).Width = 100
            UWGShowPayment.Columns(10).Width = 80
        


        End If
        Connection.Close()
        lblcount.Text = dt.Rows.Count & " รายการ"
    End Sub
End Class
