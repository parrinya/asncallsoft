Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Class Modules_Manager_Report_Retention
    Inherits System.Web.UI.Page
    Dim Connection As New SqlConnection(ConfigurationManager.ConnectionStrings("asnbroker").ConnectionString)
    Dim ISODate As New ISODate
    Dim DataAccess As New DataAccess
    Dim dt, dt1 As New DataTable
    Dim Command As SqlCommand
    Dim txtdate1 As String
    Dim txtdate2 As String
    Dim CompID As Integer
    Dim CompName As String
    Dim SupID As Integer
    Dim DispDate As String
    Dim myReport As New ReportDocument
    Dim users As String = "sa"
    Dim pass As String = "asn@sr1"
  
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        CrystalReportViewer1.ReportSource = Session("MonthReport")
        CrystalReportViewer1.DataBind()
    End Sub
    Protected Sub MyReportLoad0()
        Dim strQuery As String
        Dim Command As SqlCommand
        Connection.Open()
        strQuery = "Select Count(*) From tblreportadminretentsr"
        Command = New SqlCommand(strQuery, Connection)
        Dim Check As Integer = Command.ExecuteScalar()
        Connection.Close()
        If Check <= 0 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('ไม่พบข้อมูลที่ต้องการออกรายงาน !!');", True)
            Exit Sub
        End If

        Dim reportname As String = Server.MapPath("rptrenewadmin4.rpt")
        myReport.Load(reportname)
        myReport.SetDatabaseLogon(users, pass)
        myReport.SetParameterValue("f1", CompName)
        myReport.SetParameterValue("f2", DispDate)
        Session("MonthReport") = myReport
        CrystalReportViewer1.ReportSource = Session("MonthReport")
        CrystalReportViewer1.DataBind()


    End Sub
    Protected Sub MyReportLoad()
        Dim strQuery As String
        Dim Command As SqlCommand
        Connection.Open()
        strQuery = "Select Count(*) From tblreportadminretentsr"
        Command = New SqlCommand(strQuery, Connection)
        Dim Check As Integer = Command.ExecuteScalar()
        Connection.Close()
        If Check <= 0 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('ไม่พบข้อมูลที่ต้องการออกรายงาน !!');", True)
            Exit Sub
        End If
        'CrystalReportViewer1.RefreshReport()
        Dim reportname As String = Server.MapPath("rptrenewadmin3.rpt")
        myReport.Load(reportname)
        myReport.SetDatabaseLogon(users, pass)
        myReport.SetParameterValue("f1", CompName)
        myReport.SetParameterValue("f2", DispDate)
        Session("MonthReport") = myReport
        CrystalReportViewer1.ReportSource = Session("MonthReport")
        CrystalReportViewer1.DataBind()

    End Sub
    Protected Sub Process()
        Dim sql As String = ""
        Dim tmp1 As String = ""
        Dim tmp2 As String = ""
        Dim Tx2, sx1, sx2, sx3, sx4, approveall, approveallp, newP, NC, NCP As Decimal
        Dim px1, approveallpersent As Decimal
        '1.วันที่(ที่ค้นหา)
        Dim dd111 As String
        Dim dd222 As String
        Dim dd11 As String
        Dim dd22 As String
        If (Format(CDate(txtdate1), "yyyy") > 2300) Then
            dd11 = Format(CDate(txtdate1), "yyyy") - 543 & Format(CDate(txtdate1), "MMdd")
            dd22 = Format(CDate(txtdate2), "yyyy") - 543 & Format(CDate(txtdate2), "MMdd") + " 23:59"
            dd111 = Format(CDate(txtdate1), "yyyy") - 544 & Format(CDate(txtdate1), "MMdd")
            dd222 = Format(CDate(txtdate2), "yyyy") - 544 & Format(CDate(txtdate2), "MMdd") + " 23:59"
        Else
            dd11 = Format(CDate(txtdate1), "yyyymmdd")
            dd22 = Format(CDate(txtdate2), "yyyymmdd") + " 23:59"
        End If
        'MyReportLoad()
        Try
            '2.ลบ tblreportadminretentsr
            sql = " delete from tblreportadminretentsr "
            Connection.Open()
            Command = New SqlCommand(sql, Connection)
            Command.ExecuteNonQuery()
            Connection.Close()
            '3.บริษัท
            If CompID = 0 Then tmp1 = ""
            If CompID <> 0 Then
                tmp2 = " and  tblapplicationu.ProDuctID = '" & CompID & "'"
                tmp1 = " and  tblapplication.ProDuctID = '" & CompID & "'"
            End If
            '4.reten
            If SupID <> 0 Then
                sql = " select  userid,FName+ ' ' + LName as tsr,SupID " + _
                " from TblUser where UserLevelID = 5 and UserStatus = 1  and TypeTsr = 3  and userid not in (195,1073) and SupID='" & SupID & "' " + _
                " order by SupID,fname "
            Else
                sql = " select  userid,FName+ ' ' + LName as tsr,SupID " + _
             " from TblUser where UserLevelID = 5 and UserStatus = 1  and TypeTsr = 3  and userid not in (195,1073)" + _
             " order by SupID,fname "
            End If

            dt = DataAccess.DataRead(sql)
            If dt.Rows.Count > 0 Then
                'For i = 0 To 0
                For i = 0 To dt.Rows.Count - 1
                    'getTX2
                    sql = " SELECT     TblApplicationU.AppID " + _
                      " FROM         TblImpCaseReNew INNER JOIN " + _
                      " TblApplicationU ON TblImpCaseReNew.AppID = TblApplicationU.AppID INNER JOIN " + _
                      " TblCar ON TblApplicationU.Idcar = TblCar.IdCar INNER JOIN " + _
                      " TblCustomer ON TblCar.CusID = TblCustomer.CusID " + _
                      " WHERE     (TblApplicationU.AppStatus = 1) AND (TblApplicationU.Statusqc = 7) AND (TblApplicationU.IsProvalue = 1) AND (TblImpCaseReNew.AppID IN " + _
                      " (SELECT DISTINCT AppID FROM          TblPayment)) AND (TblCar.DataID NOT IN (206, 6)) and  tblapplicationu.protectdate  between '" & dd111 & "' and '" & dd222 & "'  and tblcar.assignto  = '" & dt.Rows(i).Item("userid") & "' " + tmp2
                    dt1 = New DataTable
                    dt1 = DataAccess.DataRead(sql)
                    Tx2 = dt1.Rows.Count

                    ' ?? reten success+wait ????????? ??????????
                    sql = " SELECT     TblApplication.AppID " + _
                  " FROM         TblImpCaseReNew INNER JOIN " + _
                                        " TblApplication ON TblImpCaseReNew.idcar = TblApplication.idcar INNER JOIN " + _
                                        " TblCar ON TblApplication.Idcar = TblCar.IdCar INNER JOIN " + _
                                        " TblCustomer ON TblCar.CusID = TblCustomer.CusID " + _
                  " WHERE     (TblApplication.AppStatus = 1) AND (TblApplication.IsProvalue = 1) AND (TblApplication.AppID IN " + _
                  " (SELECT DISTINCT AppID FROM          TblPayment)) AND (TblCar.DataID NOT IN (206, 6)) and   tblapplication.protectdate between '" & dd11 & "' and '" & dd22 & "'   and tblcar.assignto  = '" & dt.Rows(i).Item("userid") & "' and  TblImpCaseReNew.proid = '" & CompID & "'    " + tmp1
                    dt1 = New DataTable
                    dt1 = DataAccess.DataRead(sql)
                    sx1 = dt1.Rows.Count

                    If CompID <> 0 Then
                        ' ?? reten success+wait ????????? ????????????????
                        sql = " SELECT     TblApplication.AppID " + _
                      " FROM         TblImpCaseReNew INNER JOIN " + _
                                            " TblApplication ON TblImpCaseReNew.idcar = TblApplication.idcar INNER JOIN " + _
                                            " TblCar ON TblApplication.Idcar = TblCar.IdCar INNER JOIN " + _
                                            " TblCustomer ON TblCar.CusID = TblCustomer.CusID " + _
                      " WHERE     (TblApplication.AppStatus = 1) AND (TblApplication.IsProvalue = 1) AND (TblApplication.AppID IN " + _
                      " (SELECT DISTINCT AppID FROM          TblPayment)) AND (TblCar.DataID NOT IN (206, 6)) and tblapplication.protectdate between '" & dd11 & "' and '" & dd22 & "'   " + _
                      " and tblcar.assignto  = '" & dt.Rows(i).Item("userid") & "'   and  TblImpCaseReNew.proid = '" & CompID & "'  and  tblapplication.ProDuctID <> '" & CompID & "'"
                        dt1 = New DataTable
                        dt1 = DataAccess.DataRead(sql)
                        sx2 = dt1.Rows.Count

                    Else
                        sx2 = 0
                    End If
                    '????????????  ?????????? ?????? ????????????????
                    If CompID <> 0 Then
                        sql = " SELECT     TblApplication.AppID " + _
                    " FROM         TblImpCaseReNew INNER JOIN " + _
                                          " TblApplication ON TblImpCaseReNew.idcar = TblApplication.idcar INNER JOIN " + _
                                          " TblCar ON TblApplication.Idcar = TblCar.IdCar INNER JOIN " + _
                                          " TblCustomer ON TblCar.CusID = TblCustomer.CusID " + _
                    " WHERE     (TblApplication.AppStatus = 1) AND (TblApplication.IsProvalue = 1) AND (TblApplication.AppID  not IN " + _
                    " (SELECT DISTINCT AppID FROM          TblPayment)) AND (TblCar.DataID NOT IN (206, 6)) and  tblapplication.protectdate between '" & dd11 & "' and '" & dd22 & "'   and tblcar.assignto  = '" & dt.Rows(i).Item("userid") & "'   " + tmp1 + _
                    " union      " + _
                     " SELECT     TblApplication.AppID " + _
                    " FROM         TblImpCaseReNew INNER JOIN " + _
                                          " TblApplication ON TblImpCaseReNew.idcar = TblApplication.idcar INNER JOIN " + _
                                          " TblCar ON TblApplication.Idcar = TblCar.IdCar INNER JOIN " + _
                                          " TblCustomer ON TblCar.CusID = TblCustomer.CusID " + _
                    " WHERE     (TblApplication.AppStatus = 1) AND (TblApplication.IsProvalue = 1) AND (TblApplication.AppID not IN " + _
                    " (SELECT DISTINCT AppID FROM          TblPayment)) AND (TblCar.DataID NOT IN (206, 6)) and  tblapplication.protectdate between '" & dd11 & "' and '" & dd22 & "'   " + _
                    " and tblcar.assignto  = '" & dt.Rows(i).Item("userid") & "'   and  TblImpCaseReNew.proid = '" & CompID & "'  and  tblapplication.ProDuctID <> '" & CompID & "'"
                        dt1 = New DataTable
                        dt1 = DataAccess.DataRead(sql)
                        sx3 = dt1.Rows.Count

                    Else
                        sx3 = 0
                    End If
                    'new case new1*************************************************************
                    sql = " SELECT     TblApplication.AppID " + _
                    " FROM         TblApplication INNER JOIN " + _
                                           " TblCar ON TblApplication.Idcar = TblCar.IdCar INNER JOIN " + _
                                          " TblCustomer ON TblCar.CusID = TblCustomer.CusID " + _
                    " Where tblcar.groupid = 99 And (tblapplication.AppStatus = 1) And (tblapplication.IsProvalue = 1) " + _
                     " AND (TblApplication.AppID IN " + _
                     " (SELECT DISTINCT AppID FROM          TblPayment)) " + _
                      " AND (TblCar.DataID NOT IN (206, 6)) " + _
                       " and tblapplication.protectdate between '" & dd11 & "' and '" & dd22 & "'   " + _
                       " and tblcar.assignto = '" & dt.Rows(i).Item("userid") & "' " + tmp1
                    dt1 = DataAccess.DataRead(sql)
                    sx4 = dt1.Rows.Count


                    '****************************************************************************
                    '% reten
                    If Tx2 <> 0 Then
                        px1 = ((sx1 + sx2) * 100) / Tx2
                    Else
                        px1 = 0
                    End If

                    '**********************************************
                    ' ?? approve all ?????
                    approveall = 0
                    If CompID = 0 Then
                        sql = " SELECT     TblApplication.AppID " + _
                      " FROM         TblImpCaseReNew INNER JOIN " + _
                                            " TblApplication ON TblImpCaseReNew.idcar = TblApplication.idcar INNER JOIN " + _
                                            " TblCar ON TblApplication.Idcar = TblCar.IdCar INNER JOIN " + _
                                            " TblCustomer ON TblCar.CusID = TblCustomer.CusID " + _
                      " WHERE     (TblApplication.AppStatus = 1) AND (TblApplication.IsProvalue = 1) AND (TblApplication.AppID IN " + _
                      " (SELECT DISTINCT AppID FROM          TblPayment)) AND (TblCar.DataID NOT IN (206, 6)) and  tblapplication.protectdate between '" & dd11 & "' and '" & dd22 & "'   and tblcar.assignto  = '" & dt.Rows(i).Item("userid") & "' and  TblCar.GroupID <>103 "
                        dt1 = DataAccess.DataRead(sql)
                        approveall = dt1.Rows.Count

                        approveallp = 0
                        sql = " select isnull(sum(paidCARPET+paidINSUR),0) as appP from (SELECT    'paidCARPET' = case iscarpet when 1 then carpet else 0 end ,  'paidINSUR' = case isprovalue when 1 then provalue else 0 end  " + _
                      " FROM         TblImpCaseReNew INNER JOIN " + _
                                            " TblApplication ON TblImpCaseReNew.idcar = TblApplication.idcar INNER JOIN " + _
                                            " TblCar ON TblApplication.Idcar = TblCar.IdCar INNER JOIN " + _
                                            " TblCustomer ON TblCar.CusID = TblCustomer.CusID " + _
                      " WHERE     (TblApplication.AppStatus = 1) AND (TblApplication.IsProvalue = 1) AND (TblApplication.AppID IN " + _
                      " (SELECT DISTINCT AppID FROM          TblPayment)) AND (TblCar.DataID NOT IN (206, 6)) and  tblapplication.protectdate between '" & dd11 & "' and '" & dd22 & "'   and tblcar.assignto  = '" & dt.Rows(i).Item("userid") & "' and  TblCar.GroupID <>103 ) a1 "
                        dt1 = DataAccess.DataRead(sql)
                        approveallp = dt1.Rows(0).Item("appP")


                        'new ??????????? + ???
                        sql = " SELECT     TblApplication.AppID " + _
                        " FROM         TblApplication INNER JOIN " + _
                                               " TblCar ON TblApplication.Idcar = TblCar.IdCar INNER JOIN " + _
                                              " TblCustomer ON TblCar.CusID = TblCustomer.CusID " + _
                        " Where tblcar.groupid = 99 And (tblapplication.AppStatus = 1) And (tblapplication.IsProvalue = 1) " + _
                         " AND (TblApplication.AppID IN " + _
                         " (SELECT DISTINCT AppID FROM          TblPayment)) " + _
                          " AND (TblCar.DataID NOT IN (206, 6)) " + _
                           " and  tblapplication.protectdate between '" & dd11 & "' and '" & dd22 & "'   " + _
                           " and tblcar.assignto = '" & dt.Rows(i).Item("userid") & "' "
                        dt1 = DataAccess.DataRead(sql)
                        sx4 = dt1.Rows.Count

                        sql = " select isnull(sum(paidCARPET+paidINSUR),0) as appP from ( SELECT         'paidCARPET' = case iscarpet when 1 then carpet else 0 end ,  'paidINSUR' = case isprovalue when 1 then provalue else 0 end " + _
                   " FROM         TblApplication INNER JOIN " + _
                                          " TblCar ON TblApplication.Idcar = TblCar.IdCar INNER JOIN " + _
                                         " TblCustomer ON TblCar.CusID = TblCustomer.CusID " + _
                   " Where tblcar.groupid = 99 And (tblapplication.AppStatus = 1) And (tblapplication.IsProvalue = 1) " + _
                    " AND (TblApplication.AppID IN " + _
                    " (SELECT DISTINCT AppID FROM          TblPayment)) " + _
                     " AND (TblCar.DataID NOT IN (206, 6)) " + _
                      " and  tblapplication.protectdate between '" & dd11 & "' and '" & dd22 & "'   " + _
                      " and tblcar.assignto = '" & dt.Rows(i).Item("userid") & "') a1 "
                        dt1 = DataAccess.DataRead(sql)
                        newP = dt1.Rows(0).Item("appP")

                    End If

                    If Tx2 <> 0 Then
                        approveallpersent = ((approveall) * 100) / Tx2
                    Else
                        approveallpersent = 0
                    End If
                    'Nc
                    sql = " SELECT   isnull(count(TblApplication.AppID),0) as nC " + _
                          " FROM         TblApplication INNER JOIN " + _
                          " TblCar ON TblApplication.Idcar = TblCar.IdCar INNER JOIN " + _
                          " TblCustomer ON TblCar.CusID = TblCustomer.CusID " + _
                          " Where tblcar.groupid = 103 And (tblapplication.AppStatus = 1) And (tblapplication.IsProvalue = 1) " + _
                          " AND (TblApplication.AppID IN " + _
                          " (SELECT DISTINCT AppID FROM          TblPayment)) " + _
                          " AND (TblCar.DataID NOT IN (206, 6)) " + _
                          " and tblapplication.protectdate between '" & dd11 & "' and '" & dd22 & "'   " + _
                          " and tblcar.assignto = '" & dt.Rows(i).Item("userid") & "' " + tmp1

                    dt1 = DataAccess.DataRead(sql)
                    NC = dt1.Rows(0).Item("nC")
                    'NCP
                    sql = " select isnull(sum(paidCARPET+paidINSUR),0) as appP from ( SELECT         'paidCARPET' = case iscarpet when 1 then carpet else 0 end ,  'paidINSUR' = case isprovalue when 1 then provalue else 0 end " + _
                          " FROM         TblApplication INNER JOIN " + _
                          " TblCar ON TblApplication.Idcar = TblCar.IdCar INNER JOIN " + _
                          " TblCustomer ON TblCar.CusID = TblCustomer.CusID " + _
                          " Where tblcar.groupid = 103 And (tblapplication.AppStatus = 1) And (tblapplication.IsProvalue = 1) " + _
                          " AND (TblApplication.AppID IN " + _
                          " (SELECT DISTINCT AppID FROM          TblPayment)) " + _
                          " AND (TblCar.DataID NOT IN (206, 6)) " + _
                          " and tblapplication.protectdate between '" & dd11 & "' and '" & dd22 & "'   " + _
                          " and tblcar.assignto = '" & dt.Rows(i).Item("userid") & "') a1 "
                   
                    dt1 = DataAccess.DataRead(sql)
                    NCP = dt1.Rows(0).Item("appP")

                    '****************************************************
                    sql = " insert into tblreportadminretentsr (tsr, total1, total2, success1,success2,success3, persent1,new1,approveall,approveallp,ApproveAllper,new1p,Supid,nC,nCP) values ('" & dt.Rows(i).Item("tsr") & "',0,'" & Tx2 & "','" & sx1 & "','" & sx2 & "','" & sx3 & "','" & px1 & "','" & sx4 & "','" & approveall & "','" & approveallp & "','" & approveallpersent & "','" & newP & "'," & dt.Rows(i).Item("SupID") & ",'" & NC & "','" & NCP & "')"
                    Connection.Open()
                    Command = New SqlCommand(sql, Connection)
                    Command.ExecuteNonQuery()
                    Connection.Close()

                Next
                If CompID = 0 Then
                    MyReportLoad0()
                Else
                    MyReportLoad()
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        txtdate1 = Request.QueryString("date1")
        txtdate2 = Request.QueryString("date2")
        CompID = Request.QueryString("CompID")
        CompName = Request.QueryString("CompName")
        SupID = Request.QueryString("SupID")
        DispDate = Format(CDate(txtdate1), "dd-MM-yyyy").ToString() + "-" + Format(CDate(txtdate2), "dd-MM-yyyy").ToString()

        If Not IsPostBack Then
            Process()
        End If
    End Sub
    Protected Sub CrystalReportViewer1_ReportRefresh(ByVal source As Object, ByVal e As CrystalDecisions.Web.ViewerEventArgs) Handles CrystalReportViewer1.ReportRefresh
        CrystalReportViewer1.RefreshReport()
    End Sub
End Class
