
Partial Class Modules_Sale_Phone_frmCase
    Inherits System.Web.UI.Page
    Public LinkPending As String = ""
    Public CasePhone As String = ""
    Public NewCasePhone As String = ""
    Public ExpireCasePhone As String = ""
    Public RecovingCasePhone As String = ""
    Public CaseIncomplet As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.TabContainer1.Tabs.Remove(TabPanel5)
        'add by na 09/02/2015
        If Request.Cookies("TypeTsr").Value <> 3 Then
            Me.TabContainer1.Tabs.Remove(TabPanel6)
            Me.TabContainer1.Tabs.Remove(TabPanel7)
        End If
        'end add by na 09/02/2015
        'add by na 30/06/2015
        If Request.Cookies("UserLevel").Value <> 6 Then
            Me.TabContainer1.Tabs.Remove(TabPanel8)
        ElseIf Request.Cookies("UserLevel").Value = 6 Then
            Me.TabContainer1.Tabs.Remove(TabPanel6)
            Me.TabContainer1.Tabs.Remove(TabPanel1)
            Me.TabContainer1.Tabs.Remove(TabPanel2)
            Me.TabContainer1.Tabs.Remove(TabPanel3)
            Me.TabContainer1.Tabs.Remove(TabPanel7)
        End If
        'end add by na 30/06/2015
        If Request.Cookies("UserLevel").Value = 6 Then
            RecovingCasePhone = "frmCaseCall4Recoving.aspx"
        Else
            Select Case Request.Cookies("TypeTsr").Value
                Case 3
                    Select Case Request.Cookies("SupID").Value
                        Case "5672"
                            CasePhone = "frmCaseCall4.aspx"
                        Case Else
                            CasePhone = "frmCaseCall2.aspx"
                    End Select


                    NewCasePhone = "frmNewCaseCall2.aspx"
                    ExpireCasePhone = "frmExpireCall.aspx"
                Case 6
                    CasePhone = "frmCaseCall3.aspx"
                Case Else
                    CasePhone = "frmCaseCall.aspx"
            End Select

          
        End If
        Select Case Request.Cookies("SupID").Value
            Case "5672"
                CaseIncomplet = "frmCaseIncompleteOOHOORenew.aspx"
            Case Else
                CaseIncomplet = "frmCaseIncomplete.aspx"
        End Select
        'If Request.Cookies("UserLevel").Value = 6 Then
        '    RecovingCasePhone = "frmCaseCall4Recoving.aspx"
        'Else
        '    Select Case Request.Cookies("TypeTsr").Value
        '        Case 3
        '            CasePhone = "frmCaseCall2.aspx"
        '            NewCasePhone = "frmNewCaseCall2.aspx"
        '            ExpireCasePhone = "frmExpireCall.aspx"
        '        Case 6
        '            CasePhone = "frmCaseCall3.aspx"
        '        Case Else
        '            CasePhone = "frmCaseCall.aspx"
        '    End Select
        'End If

    End Sub

    Protected Sub WebImageButton1_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles WebImageButton1.Click
        Select Case ddPending.SelectedValue
            Case 1
                LinkPending = "frmCallCenter.aspx"
            Case 2
                LinkPending = "frmPhoto.aspx"
            Case 3
                LinkPending = "frmReApp.aspx"
            Case 4
                LinkPending = "frmQc.aspx"
        End Select
    End Sub
End Class
