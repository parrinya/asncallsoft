Imports System.IO
Imports System.Data
Partial Class Modules_Manager_Manage_Case_frmCorenoteAndPayment
    Inherits System.Web.UI.Page
    Shared tmpappid As String
    Protected Sub btnFind_Click(sender As Object, e As System.EventArgs) Handles btnFind.Click
       
        SqlCustomer.SelectCommand += GetQuery()
        GvData.DataBind()

    End Sub
    Protected Function GetQuery() As String
        Dim str As String = ""
        If chkAppID.Checked Then
            str += " and a1.AppID like '%" & txtAppID.Text & "%'"
        End If
        If chkCarID.Checked Then
            str += " and a2.CarID like '%" & txtCariD.Text & "%'"
        End If
        If chkCusID.Checked Then
            str += " and a3.FNameTH + ' ' +a3.LNameTH  like  '%" & txtcusname.Text & "%'"
        End If
        '
        str += " order by a1.AppID desc "
        Return str
    End Function
    Protected Sub GetFileData(ByVal tmpappid As String)
        Dim dt As DataTable
        Dim FileName() As String
        Dim myDirInfo As DirectoryInfo
        myDirInfo = New DirectoryInfo("D:\Line\" & tmpappid)

        If myDirInfo.Exists = False Then
            Exit Sub
        End If
        FileName = Directory.GetFiles("D:\Line\" & tmpappid)
        dt = New DataTable
        dt.Columns.Add(New DataColumn("FileNames", GetType(String)))
        dt.Columns.Add(New DataColumn("APPID", GetType(String)))
        Dim dr As DataRow
        Dim i As Integer
        For i = 0 To FileName.Length - 1
            dr = dt.NewRow
            dr("FileNames") = Path.GetFileName(FileName(i))
            dr("APPID") = tmpappid
            dt.Rows.Add(dr)
        Next

        GvCase.DataSource = dt
        GvCase.DataBind()

    End Sub


    Protected Sub GvCase_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvCase.RowCommand
        Dim strFileName As String = "D:\Line\" & tmpappid & "\" & GvCase.DataKeys(e.CommandArgument).Item(0)
        ShowPdf(strFileName)


    End Sub
    Public Sub ShowPdf(ByVal strFileName As String)
        Response.ClearContent()
        Response.ClearHeaders()
        Response.AddHeader("Content-Disposition", "inline;filename=" + strFileName)
        Response.ContentType = "application/JPEG"
        Response.WriteFile(strFileName)
        Response.Flush()
        Response.Clear()
    End Sub

    Protected Sub GvData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvData.RowCommand
        ' GvData
        tmpappid = GvData.DataKeys(e.CommandArgument).Item(0)
        GetFileData(tmpappid)
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
       
    End Sub
End Class
