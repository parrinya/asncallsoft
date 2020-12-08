Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Class Modules_Manager_Report_ReportSourcebyList
    Inherits System.Web.UI.Page
    Dim Connection As New SqlConnection(ConfigurationManager.ConnectionStrings("asnbroker").ConnectionString)
    Dim ISODate As New ISODate
    Dim DataAccess As New DataAccess
    Dim dt, dt1 As New DataTable
    Dim Command As SqlCommand
    Dim txtdate1 As String
    Dim txtdate2 As String
    Dim DispDate As String
    Dim SupID As Integer
    Dim LeadID As Integer
    Dim users As String = "sa"
    Dim pass As String = "asn@sr1"
    Protected Sub MyReportLoad()
        Dim strQuery As String
        Dim Command As SqlCommand
        'Dim Supname As String = ""
        'Dim Leadname As String = ""
        Connection.Open()
        strQuery = "Select Count(*) From tmpReportSourcebyList"
        Command = New SqlCommand(strQuery, Connection)
        Dim Check As Integer = Command.ExecuteScalar()
        'If SupID = 0 Then
        '    Supname = "All"
        'Else
        '    strQuery = "Select [FName]+' '+[LName] as nameSup From tblUser where Userid=" & SupID
        '    Command = New SqlCommand(strQuery, Connection)
        '    Supname = Command.ExecuteScalar()
        'End If
        'If LeadID = 0 Then
        '    Leadname = "All"
        'Else
        '    strQuery = "Select [FName]+' '+[LName] as nameLead From tblUser where Userid=" & LeadID
        '    Command = New SqlCommand(strQuery, Connection)
        '    Leadname = Command.ExecuteScalar()
        'End If

        Connection.Close()
        If Check <= 0 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('ไม่พบข้อมูลที่ต้องการออกรายงาน !!');", True)
            Exit Sub
        End If

        Dim reportname As String = Server.MapPath("rptReportSourcebyList.rpt")
        Dim myReport As New ReportDocument
        myReport.Load(reportname)
        myReport.SetDatabaseLogon(users, pass)
        myReport.SetParameterValue("strDate", DispDate)
        'myReport.SetParameterValue("Supname", Supname)
        'myReport.SetParameterValue("Leadname", Leadname)
        Session("ReportSourcebyList") = myReport
        CrystalReportViewer1.ReportSource = Session("ReportSourcebyList")
        CrystalReportViewer1.DataBind()


    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        txtdate1 = Request.QueryString("date1")
        txtdate2 = Request.QueryString("date2")
        SupID = Request.QueryString("SupID")
        LeadID = Request.QueryString("LeadID")
        DispDate = "ตามวันที่Submit ระหว่างวันที่ " & Format(CDate(txtdate1), "dd-MM-yyyy").ToString() + " ถึง " + Format(CDate(txtdate2), "dd-MM-yyyy").ToString()
        If Not IsPostBack Then
            Process()
        End If
    End Sub
    Protected Sub CrystalReportViewer1_ReportRefresh(ByVal source As Object, ByVal e As CrystalDecisions.Web.ViewerEventArgs) Handles CrystalReportViewer1.ReportRefresh
        CrystalReportViewer1.RefreshReport()
    End Sub
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        CrystalReportViewer1.ReportSource = Session("ReportSourcebyList")
        CrystalReportViewer1.DataBind()
    End Sub
    Protected Sub Process()
        Dim strQuery As String
        Dim Command As SqlCommand

        If (Format(CDate(txtdate1), "yyyy") > 2300) Then
            txtdate1 = Format(CDate(txtdate1), "yyyy") - 543 & Format(CDate(txtdate1), "MMdd")
            txtdate2 = Format(CDate(txtdate2), "yyyy") - 543 & Format(CDate(txtdate2), "MMdd") + " 23:59"
        Else
            txtdate1 = Format(CDate(txtdate1), "yyyymmdd")
            txtdate2 = Format(CDate(txtdate2), "yyyymmdd") + " 23:59"
        End If
     
        Dim Check As Integer


        strQuery = " DROP TABLE tmpReportSourcebyList"
        Connection.Open()
        Command = New SqlCommand(strQuery, Connection)
        Command.ExecuteNonQuery()

        strQuery = " select a1.CusID,"
        strQuery += " a1.FNameTH+' ' +a1.LNameTH as CusName,"
        strQuery += " a2.CarID,a3.Appid,"
        strQuery += " Convert(VarChar,a3.Submitdate,103) as Submitdate,"
        strQuery += " a5.ProTypeName,a4.TypeName,"
        strQuery += " a3.ProValue   as ProValue,"
        strQuery += " a3.CarPet  as CarPet,"
        strQuery += " (a3.YearPay-a3.ProValue) as distCOUNT,"
        strQuery += " 'AppStatus'=case a3.appstatus when 1 then 'A' else 'C' end,a6.GroupName,"
        strQuery += " a7.FName + ' ' + a7.LName as tsrNAME ,a7.supid,a7.UserID"
        strQuery += " ,(select [FName]+' '+[LName]  from tbluser where userid=a7.supid) as nameSup"
        strQuery += " ,(select [FName]+' '+[LName]  from tbluser where userid=a7.LeaderID) as nameLead"
        strQuery += " into tmpReportSourcebyList"
        strQuery += " from tblcustomer a1 inner join tblcar a2  on a1.CusID = a2.CusID "
        strQuery += " inner join TblApplicationU a3  on a2.IdCar = a3.Idcar "
        strQuery += " inner join Tbl_Type a4  on a3.Typeprovalue = a4.Typeid "
        strQuery += " inner join Tbl_ProductType a5  on a3.ProDuctID = a5.ProTypeID "
        strQuery += " inner join TblSourceGroup a6  on a2.GroupID = a6.GroupID "
        strQuery += " inner join TblUser a7  on a3.CreateID = a7.UserID "
        strQuery += " where a3.Statusqc = 7  "
        strQuery += " and a3.submitdate between '" & txtdate1 & "' and '" & txtdate2 & "'   "
        If SupID <> 0 Then
            strQuery += " and a7.SupID =" & SupID
        End If
        If LeadID <> 0 Then
            strQuery += " and a7.LeaderID =" & LeadID
        End If
        strQuery += " order by a3.submitdate"
        Command = New SqlCommand(strQuery, Connection)
        Command.ExecuteNonQuery()

        strQuery = "Select Count(*) From tmpReportSourcebyList"
        Command = New SqlCommand(strQuery, Connection)
        Check = Command.ExecuteScalar()
        Connection.Close()
        If Check > 0 Then
            MyReportLoad()
        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Alert", "alert('ไม่พบข้อมูลที่ต้องการออกรายงาน !!');", True)
            Exit Sub
        End If

       

    End Sub
End Class
