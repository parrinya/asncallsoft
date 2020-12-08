Imports System.Data.SqlClient

Partial Class Modules_Manager_Manage_Case_frmDetailPackage
    Inherits System.Web.UI.Page
    Dim FunAll As New FuntionAll
    Dim Conn As New SqlConnection(ConfigurationManager.ConnectionStrings("asnbroker").ConnectionString)
    Protected Sub btnsave_Click(sender As Object, e As System.EventArgs) Handles btnsave.Click
        Conn.Open()
        Dim command As SqlCommand = Conn.CreateCommand()
        Dim transaction As SqlTransaction
        transaction = Conn.BeginTransaction("Transactionxml")
        command.Connection = Conn
        command.Transaction = transaction
        Try
            'แก้ไข
            Dim lblLostLife1 As TextBox = FunAll.ObjFindControl("lblLostLife1", frmPackage)
            Dim lblLostLife2 As TextBox = FunAll.ObjFindControl("lblLostLife2", frmPackage)
            Dim lblLostProp1 As TextBox = FunAll.ObjFindControl("lblLostProp1", frmPackage)
            Dim lblLostProp2 As TextBox = FunAll.ObjFindControl("lblLostProp2", frmPackage)

            Dim lblLostCar1 As TextBox = FunAll.ObjFindControl("lblLostCar1", frmPackage)
            Dim lblLostCar2 As TextBox = FunAll.ObjFindControl("lblLostCar2", frmPackage)
            Dim lblCarFire As TextBox = FunAll.ObjFindControl("lblCarFire", frmPackage)

            Dim lblAccLost1 As TextBox = FunAll.ObjFindControl("lblAccLost1", frmPackage)
            Dim lblAccLost2 As TextBox = FunAll.ObjFindControl("lblAccLost2", frmPackage)
            Dim lblAccLost3 As TextBox = FunAll.ObjFindControl("lblAccLost3", frmPackage)
            Dim lblAccLost4 As TextBox = FunAll.ObjFindControl("lblAccLost4", frmPackage)
            Dim lblMaintain As TextBox = FunAll.ObjFindControl("lblMaintain", frmPackage)
            Dim lblInsure As TextBox = FunAll.ObjFindControl("lblInsure", frmPackage)
            'เก็บ Log ข้อมูลเดิม
            With SqlPackage
                .Insert()
            End With
            '
            With SqlPackage
                .UpdateParameters("Lost_Life1").DefaultValue = lblLostLife1.Text
                .UpdateParameters("Lost_Life2").DefaultValue = lblLostLife2.Text
                .UpdateParameters("Lost_Prop1").DefaultValue = lblLostProp1.Text
                .UpdateParameters("Lost_Prop2").DefaultValue = lblLostProp2.Text
                .UpdateParameters("Lost_Car1").DefaultValue = lblLostCar1.Text
                .UpdateParameters("Lost_Car2").DefaultValue = lblLostCar2.Text
                .UpdateParameters("Car_Fire").DefaultValue = lblCarFire.Text
                .UpdateParameters("Acc_Lost1").DefaultValue = CInt(lblAccLost1.Text)
                .UpdateParameters("Acc_Lost2").DefaultValue = lblAccLost2.Text
                .UpdateParameters("Acc_Lost3").DefaultValue = CInt(lblAccLost3.Text)
                .UpdateParameters("Acc_Lost4").DefaultValue = lblAccLost4.Text
                .UpdateParameters("Maintain").DefaultValue = lblMaintain.Text
                .UpdateParameters("Insure").DefaultValue = lblInsure.Text
                .Update()
            End With
            transaction.Commit()
            MsgBox("ดำเนินการเสร็จสิ้น")
        Catch ex As Exception
            transaction.Rollback()
            MsgBox("ขออภัยมีข้อผิดพลาด")
        End Try
        Conn.Close()
    End Sub
    Public Sub MsgBox(ByVal sMsg As String)

        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control
        sMsg = sMsg.Replace("'", "\'")
        sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
        sMsg = sMsg.Replace(vbCrLf, "\n")
        sMsg = "<script language=javascript>alert(""" & sMsg & """)</script>"

        sb = New StringBuilder()
        sb.Append(sMsg)

        For Each oFormObject In Me.Controls
            If TypeOf oFormObject Is HtmlForm Then
                Exit For
            End If
        Next

        ' Add the javascript after the form object so that the 
        ' message doesn't appear on a blank screen.
        oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
    End Sub
End Class
