Imports System.IO.Ports
Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography.X509Certificates
Partial Class Modules_Manager_Manage_Tsr_frmMonitor
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim Conn As New SqlConnection(ConfigurationManager.ConnectionStrings("asnbroker").ConnectionString)
    Dim com As SqlCommand
    Public strPhoneCall As String = ""
    Dim DataAccess As New DataAccess
    Public strReadPort As String = ""
    Public strRegister As String = ""
    Dim FunAll As New FuntionAll

    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            ReadPort2()
            ReadPortRegister()
            SqlCall.SelectParameters("SupID").DefaultValue = ddUser.SelectedValue
            GvCall.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ReadPortRegister()
        Dim URL As String = Replace(ConfigurationManager.AppSettings("ReadRegister"), "@IpAsserisk", Request.Cookies("IpAsterisk").Value)

        Dim request1 As HttpWebRequest = WebRequest.Create(URL)
        request1.Proxy = Nothing
        request1.Credentials = CredentialCache.DefaultCredentials
        ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidateServerCertificate)
        Dim response2 As HttpWebResponse = request1.GetResponse()
        Dim reader As StreamReader = New StreamReader(response2.GetResponseStream())
        strRegister = reader.ReadToEnd()
        reader.Close()

    End Sub

    Protected Sub ReadPort2()
        Dim URL As String = Replace(ConfigurationManager.AppSettings("ReadCall"), "@IpAsserisk", Request.Cookies("IpAsterisk").Value)
        Dim request1 As HttpWebRequest = WebRequest.Create(URL)
        request1.Proxy = Nothing
        request1.Credentials = CredentialCache.DefaultCredentials
        ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidateServerCertificate)
        Dim response2 As HttpWebResponse = request1.GetResponse()
        Dim reader As StreamReader = New StreamReader(response2.GetResponseStream())
        strReadPort = Replace(Replace(reader.ReadToEnd(), "<pre>", ""), "</pre>", "").Trim
        reader.Close()
    End Sub

    Public Shared Function ValidateServerCertificate(ByVal sender As Object, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal sslPolicyErrors As Security.SslPolicyErrors) As Boolean
        Return True
    End Function

    Protected Sub GvCall_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvCall.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblStatusCall As Label = FunAll.ObjFindControl("lblStatusCall", e.Row)
            Dim lblRegister As Label = FunAll.ObjFindControl("lblRegister", e.Row)
            Dim btnCmd1 As Button = FunAll.ObjFindControl("btnCmd1", e.Row)
            Dim btnCmd2 As Button = FunAll.ObjFindControl("btnCmd2", e.Row)
            If InStr(strReadPort, GvCall.DataKeys(e.Row.RowIndex).Item(0).ToString & " Busy") Then
                lblStatusCall.Text = "ใช้สาย"
                lblStatusCall.BackColor = Drawing.Color.Green
                lblStatusCall.ForeColor = Drawing.Color.Green
                btnCmd1.Enabled = True
                btnCmd2.Enabled = True
            Else
                lblStatusCall.Text = "สายว่าง"
                lblStatusCall.BackColor = Drawing.Color.Red
                lblStatusCall.ForeColor = Drawing.Color.Red
                btnCmd1.Enabled = False
                btnCmd2.Enabled = False
            End If

            If InStr(strRegister, GvCall.DataKeys(e.Row.RowIndex).Item(0).ToString & "/" & GvCall.DataKeys(e.Row.RowIndex).Item(0).ToString & " (Unspecified)") Then
                lblRegister.Text = "Offline"
                lblRegister.ForeColor = Drawing.Color.Red
            Else
                lblRegister.Text = "Online"
                lblRegister.ForeColor = Drawing.Color.Green
            End If
        End If

    End Sub

    Protected Sub GvCall_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvCall.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            Dim HeaderGrid As GridView = DirectCast(sender, GridView)
            Dim HeaderGridRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim HeaderCell As New TableCell()
            HeaderCell.Text = ""
            HeaderCell.ColumnSpan = 6
            HeaderGridRow.Cells.Add(HeaderCell)

            HeaderCell = New TableCell()
            HeaderCell.Text = "Talk"
            HeaderCell.HorizontalAlign = HorizontalAlign.Center
            HeaderCell.ColumnSpan = 2
            HeaderGridRow.Cells.Add(HeaderCell)

            HeaderCell = New TableCell()
            HeaderCell.Text = "Abandon"
            HeaderCell.ColumnSpan = 2
            HeaderCell.HorizontalAlign = HorizontalAlign.Center
            HeaderGridRow.Cells.Add(HeaderCell)

            HeaderCell = New TableCell()
            HeaderCell.Text = "Line Breaks"
            HeaderCell.ColumnSpan = 2
            HeaderCell.HorizontalAlign = HorizontalAlign.Center
            HeaderGridRow.Cells.Add(HeaderCell)


            HeaderCell = New TableCell()
            HeaderCell.Text = ""
            HeaderCell.ColumnSpan = 3
            HeaderCell.HorizontalAlign = HorizontalAlign.Center
            HeaderGridRow.Cells.Add(HeaderCell)


            GvCall.Controls(0).Controls.AddAt(0, HeaderGridRow)
        End If
    End Sub

    Protected Sub GvCall_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvCall.RowCommand
        strPhoneCall += Replace(ConfigurationManager.AppSettings("WebCall"), "@IpAsserisk", Request.Cookies("IpAsterisk").Value)

        If e.CommandName = "Call1" Then
            strPhoneCall += "from=" & Request.Cookies("Extension").Value & "&to=" & GvCall.DataKeys(e.CommandArgument).Item(0) & "&commd=2 "
        ElseIf e.CommandName = "Call2" Then
            strPhoneCall += "from=" & Request.Cookies("Extension").Value & "&to=" & GvCall.DataKeys(e.CommandArgument).Item(0) & "&commd=1 "
        End If
    End Sub
End Class
