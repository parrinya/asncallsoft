Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data
Imports System.IO
Imports System.Globalization
Imports System.Drawing
Partial Class Modules_Manager_Import_frmImport_Lab
    Inherits System.Web.UI.Page
    Dim drExcel As OleDbDataReader
    Dim fn As String
    Dim cdup As Integer
    Dim schemaTable As New DataTable
    Dim workRow As DataRow
    Dim StrQuery As New QueryChkLab
    Dim DataAccess As New DataAccess

    Protected Sub btnimport_Click(sender As Object, e As System.EventArgs) Handles btnimport.Click
        Label1.Text = Nothing
        Try
            If FileUpload1.HasFile Then
                If (Request.Cookies("UserLevel").Value = 1 Or Request.Cookies("UserLevel").Value = 2) Then
                    fn = "importLab.xls"
                    Dim SaveLocation As String = Server.MapPath("~/Tmp/") + fn
                    FileUpload1.SaveAs(SaveLocation)
                    Dim cnew As Integer = GetExcelToDataTable().Rows.Count

                    Label1.Text = "รายการใหม่=" + cnew.ToString + " รายการ , รายการซ้ำ=" + cdup.ToString + " รายการ"
                    MsgBox("ดำเนินการเรียบร้อย")
                Else
                    MsgBox("ไม่สามารถ Impoer ได้")
                End If
            End If


        Catch ex As Exception
            MsgBox("ไม่สามารถบันทึกได้เนื่องจาก" & ex.Message)
        End Try
    End Sub
    Protected Function GetExcelToDataTable() As DataTable

        Dim excelConnectionString As String = ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Server.MapPath("~/Tmp/") + fn) & ";Extended Properties=""Excel 8.0;"""

        ' Create Connection to Excel Workbook
        Using connection As New OleDbConnection(excelConnectionString)
            Dim sql As String = "Select *  "
            sql += " FROM [Sheet1$]"
            Dim command As New OleDbCommand(sql, connection)
            connection.Open()

            ' Create DbDataReader to Data Worksheet
            drExcel = command.ExecuteReader()


            Dim myField As DataRow
            Dim dt As New DataTable
            dt.Load(drExcel)
            drExcel.Close()
            connection.Close()
            schemaTable.Columns.Add("userid")
            schemaTable.Columns.Add("rewarddate")
            schemaTable.Columns.Add("reward")
            schemaTable.Columns.Add("typereword")
            schemaTable.Columns.Add("Createid")
            For Each myField In dt.Rows
                If myField("userid").ToString = Nothing Then

                Else
                    'ตรวจสอบว่ามีข้อมูลซ้ำ หรือไม่
                    Dim schemaTable1 As New DataTable
                    Dim time As DateTime = DateTime.Parse(myField("rewarddate"))
                    schemaTable1 = DataAccess.DataRead(StrQuery.chk_TblRewardTsr(myField("userid"), time, myField("typereword").ToString().Trim))
                    If schemaTable1.Rows.Count = 0 Then
                        workRow = schemaTable.NewRow()
                        workRow("userid") = myField("userid")
                        workRow("rewarddate") = DateTime.Parse(myField("rewarddate"))
                        workRow("reward") = myField("reward")
                        workRow("typereword") = myField("typereword")
                        workRow("Createid") = Request.Cookies("userID").Value
                        schemaTable.Rows.Add(workRow)
                    Else
                        'delect data old
                        With SqlimportLab
                            .DeleteParameters("Userid").DefaultValue = myField("userid").ToString.Trim
                            .DeleteParameters("RewardDate").DefaultValue = myField("rewarddate")
                            .DeleteParameters("Typereword").DefaultValue = myField("typereword")
                            .Delete()
                        End With
                        'insert data new
                        With SqlimportLab
                            .InsertParameters("Userid").DefaultValue = myField("userid").ToString.Trim
                            .InsertParameters("RewardDate").DefaultValue = myField("rewarddate")
                            .InsertParameters("Reward").DefaultValue = myField("reward").ToString.Trim
                            .InsertParameters("Typereword").DefaultValue = myField("typereword")
                            .Insert()
                        End With
                        cdup = cdup + 1

                    End If
                End If

            Next

            Dim bulkcopy As New SqlBulkCopy(ConfigurationManager.ConnectionStrings("asnbroker").ConnectionString)
            bulkcopy.DestinationTableName = "TblRewardTsr"
            bulkcopy.ColumnMappings.Add("userid", "Userid")
            bulkcopy.ColumnMappings.Add("rewarddate", "RewardDate")
            bulkcopy.ColumnMappings.Add("reward", "Reward")
            bulkcopy.ColumnMappings.Add("typereword", "Typereword")
            bulkcopy.ColumnMappings.Add("Createid", "Createid")
            bulkcopy.WriteToServer(schemaTable)
            Return (schemaTable)

        End Using
    End Function
End Class
