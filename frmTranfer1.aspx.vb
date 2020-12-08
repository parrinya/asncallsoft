Imports System.Data
Partial Class Modules_Manager_Manage_Case_frmTranfer1
    Inherits System.Web.UI.Page
    Dim DataAccess As New DataAccess
    Protected Function checkAssign() As Boolean
        If CInt(txtAssign.Text) > CInt(ddRec.SelectedValue) Then
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(UpdatePanel), UpdatePanel1.ClientID, "alert('จำนวน Assign มากกว่าจำนวน Record');", True)

            Return False
        Else
            Return True
        End If
    End Function

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If checkAssign() = True Then
            Dim dt As New DataTable
            dt = DataAccess.DataRead(strQuery)
            If dt.Rows.Count > 0 Then
                Dim i As Integer
                For i = 0 To CInt(txtAssign.Text) - 1
                    SaveAssignTblCustomer(dt.Rows(i).Item("IdCar"), 0)
                Next
            End If
            ddSourceGroup.DataBind()
            ddRec.DataBind()
        End If
    End Sub

    Protected Function strQuery() As String
        Dim strqry As String = ""


        If ddStatus.SelectedValue = 0 Then
            strqry += "  SELECT a2.IdCar,a2.CusID,a2.CurStatus,a2.AssignTo FROM [TblSourceGroup] a1 "
            strqry += " Inner Join TblCar a2 on a1.GroupID = a2.GroupID "
            strqry += " Where  a2.CurStatus in (1,2,4,6,7,8) "
            If ddSourceGroup.SelectedValue <> 0 Then
                strqry += " and  a2.GroupID=" & ddSourceGroup.SelectedValue

            End If
            strqry += " and a2.AssignTo = " & ddTsr1.SelectedValue
        Else
            strqry += " SELECT a2.IdCar,a2.CusID,a2.CurStatus,a2.AssignTo FROM [TblSourceGroup] a1 "
            strqry += " Inner Join TblCar a2 on a1.GroupID = a2.GroupID "
            strqry += " Where   a2.CurStatus = " & ddStatus.SelectedValue
            If ddSourceGroup.SelectedValue <> 0 Then
                strqry += " and  a2.GroupID=" & ddSourceGroup.SelectedValue

            End If
            strqry += " and a2.AssignTo = " & ddTsr1.SelectedValue
        End If

   



        Return strqry

    End Function

    'Update Assign ให้ Tsr
    Protected Sub SaveAssignTblCustomer(ByVal CusID As String, TypeAssign As Integer)


        With SqlUser
            .UpdateParameters("AssignTo").DefaultValue = ddTsr2.SelectedValue
            .UpdateParameters("Type").DefaultValue = TypeAssign
            .UpdateParameters("IdCar").DefaultValue = CusID
            .Update()

        End With

    End Sub

    Protected Sub btnAssignNew_Click(sender As Object, e As System.EventArgs) Handles btnAssignNew.Click
        Try
            If checkAssign() = True Then
                Dim dt As New DataTable
                dt = DataAccess.DataRead(strQuery)
                If dt.Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To CInt(txtAssign.Text) - 1

                        With SqlUser
                            .InsertParameters("CusID").DefaultValue = dt.Rows(i).Item("CusID")
                            .InsertParameters("CarID").DefaultValue = dt.Rows(i).Item("IdCar")
                            .InsertParameters("Status_old").DefaultValue = dt.Rows(i).Item("CurStatus")
                            .InsertParameters("Status_new").DefaultValue = 0
                            .InsertParameters("comment").DefaultValue = "โอน list จาก tsr id : " & dt.Rows(i).Item("AssignTo") & " มาให้ กองกลาง"
                            .InsertParameters("userOld").DefaultValue = dt.Rows(i).Item("AssignTo")
                            .InsertParameters("userNew").DefaultValue = 0
                            .InsertParameters("HostAccess").DefaultValue = Request.ServerVariables("REMOTE_ADDR")
                            .Insert()
                        End With

                        SaveAssignTblCustomer(dt.Rows(i).Item("IdCar"), 1)
                    Next
                End If
                ddSourceGroup.DataBind()
                ddRec.DataBind()
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(UpdatePanel), UpdatePanel1.ClientID, "alert('ดำเนินการเรียบร้อย')", True)
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(UpdatePanel), UpdatePanel1.ClientID, "alert('" & ex.Message & "')", True)
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.Cookies("UserLevel").Value <> 1 Then
                btnAssignNew.Visible = False
            End If
        End If
    End Sub
End Class
