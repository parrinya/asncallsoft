Imports System.Data
Imports System.Data.SqlClient
Partial Class Modules_Sale_Index_frmIndex
    Inherits System.Web.UI.Page
    Dim DataAccess As New DataAccess
    Dim StrQuery As New QueryUser
    Dim dt As DataTable
    Dim Conn As New SqlConnection(ConfigurationManager.ConnectionStrings("asnbroker").ConnectionString)
    Dim com As SqlCommand
    Dim ChkLogin As New ChkLogin
    Dim proxy As New asnserverbk.WebService
    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
       
        If (Request.Cookies("userID") Is Nothing) Then
			If ChkLogin.ChkTimeLogin Then
				LoginUser()
			Else
				MsgBox("ยังไม่ถึงเวลาทำงาน 8.00 น. - 19.45 น. เวลาปัจจุบัน " & DateTime.Now.ToString("hh:mm"))
			End If		
        ElseIf Not (Request.Cookies("userID") Is Nothing) Then
            MsgBox("กรุณาออกจากระบบงานเดิมก่อน ")
        End If

    End Sub

    Protected Sub LoginUser()
        dt = New DataTable
        dt = DataAccess.DataRead(StrQuery.LoginUser(txtUsername.Text.Trim, txtPassword.Text.Trim))

        If dt.Rows.Count = 1 Then
            Dim UserStatus As Integer = dt.Rows(0).Item("UserStatus")
            If DecryptPassword() = True Then



                Response.Cookies("userID").Value = dt.Rows(0).Item("userID")
                Dim userlevel As Integer = dt.Rows(0).Item("userLevelID")

                Response.Cookies("UserLevel").Value = dt.Rows(0).Item("userLevelID")
                Response.Cookies("TypeTsr").Value = dt.Rows(0).Item("TypeTsr")
                Response.Cookies("Extension").Value = dt.Rows(0).Item("Exten")
                Response.Cookies("IpAsterisk").Value = dt.Rows(0).Item("ipAsterisk")
                Response.Cookies("SupID").Value = dt.Rows(0).Item("SupID")
				

                ' Add By Na 61-01-004->Add Condition IF  
                If IsDBNull(dt.Rows(0).Item("LastAccess")) Then
                    Response.Redirect("~/Modules/Sale/Index/frmUser.aspx")
                Else

                    ChkLogin.UpdateUserOnline(dt.Rows(0).Item("userID"), "True")
                    Dim ip As String = Request.ServerVariables("REMOTE_ADDR")
                    ChkLogin.UpdateipUser(dt.Rows(0).Item("userID"), ip)
                    Session("userID") = dt.Rows(0).Item("userID")
                    '-----สำหรับ chk การ Login ของแต่ละ User ต่อวัน-------
                    'If ChkTblLogIn(dt.Rows(0).Item("userID")) = True Then
                    InsertLogIn()
                    '    DataAccess.DataWrite(StrQuery.UpdateTblUserLogin(1, Request.Cookies("userID").Value))
                    'End If
                    '-----End-------
                    Response.Redirect("~/Modules/Sale/Index/frmHome.aspx")
                End If


            Else
                If Session("NumUser") Is Nothing Then
                    Session("NumUser") = 2
                    MsgBox("password ของคุณไม่ถูกต้อง คุณสามารถ Login ได้อีก " & Session("NumUser") & " ครั้ง")
                Else
                    If Session("NumUser") < 2 Then
                        ChkLogin.UpdateUserCancel(dt.Rows(0).Item("userID"))
                        Dim ip As String = Request.ServerVariables("REMOTE_ADDR")
                        ChkLogin.InsertTblLog_LoginFailure(dt.Rows(0).Item("userID"), ip)
                        MsgBox("username ของท่านถูกยกเลิก โปรดติดต่อ Project Lead ")
                        Session("NumUser") = 2
                    Else
                        Session("NumUser") -= 1
                        MsgBox("password ของคุณไม่ถูกต้อง คุณสามารถ Login ได้อีก " & Session("NumUser") & " ครั้ง")
                    End If
                End If
                'MsgBox("password ของคุณไม่ถูกต้อง")
            End If

        ElseIf dt.Rows.Count > 1 Then
            MsgBox("username ของคุณมีซ้ำกันในระบบ กรุณาแจ้งทาง IT")
        Else
            MsgBox("ไม่พบ username")

        End If

    End Sub

    Protected Sub MsgBox(ByVal strMassage As String)
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "script", "alert('" & strMassage & "');", True)
    End Sub


    '-----สำหรับ chk การ Login ของแต่ละ User ต่อวัน-------
    Protected Function ChkTblLogIn(ByVal userID As String) As Boolean
        dt = New DataTable
        dt = DataAccess.DataRead(StrQuery.SelectTblUserLogIn(userID))
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Sub CheckConnectionState()
        If Conn.State = ConnectionState.Open Then
            Conn.Close()
        Else
            Conn.Open()
        End If
    End Sub

    Protected Function DecryptPassword() As Boolean
        Dim str As String = proxy.DecrytePassword(dt.Rows(0).Item("UserPassword"))
        Dim strpass As String = txtPassword.Text.Trim
        If strpass = str.Trim Then

            Return True
        Else
            Return False
        End If
    End Function

    Protected Sub SaveLogIP(ByVal userID As String)
        com = New SqlCommand(StrQuery.InsertTblUserLogIP, Conn)
        With com
            .Parameters.Clear()
            .Parameters.Add("@userID", SqlDbType.VarChar).Value = userID
            .Parameters.Add("@ComIP", SqlDbType.VarChar).Value = Request.ServerVariables("REMOTE_ADDR")
            .Parameters.Add("@ComName", SqlDbType.VarChar).Value = ""
            .Parameters.Add("@ComMacAddr", SqlDbType.VarChar).Value = ""
            .ExecuteNonQuery()

        End With
    End Sub

    Protected Sub InsertLogIn()
        CheckConnectionState()
        com = New SqlCommand(StrQuery.InsertTblUserLogIn, Conn)
        With com
            .Parameters.Clear()
            .Parameters.Add("@UserID", SqlDbType.VarChar).Value = Request.Cookies("userID").Value
            .Parameters.Add("@IPLogin", SqlDbType.VarChar).Value = Request.ServerVariables("REMOTE_ADDR")
            .Parameters.Add("@TypeIO", SqlDbType.Char).Value = "I"

            .ExecuteNonQuery()
        End With

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim mnList As Menu = CType(Master.FindControl("NavigationMenu"), Menu)
        mnList.Visible = False
        If Not IsPostBack Then
            ViewState("StartTime") = DateTime.Now
        End If

    End Sub

   
End Class
