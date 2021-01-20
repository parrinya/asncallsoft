
Partial Class Modules_Sale_Phone_frmCaseIncompleteOOHOORenew
    Inherits System.Web.UI.Page
    Dim ISODate As New ISODate

    Protected Sub btnFind_Click(sender As Object, e As System.EventArgs) Handles btnFind.Click
        If txtdate1.Text <> "" And txtdate2.Text <> "" And txtdate3.Text <> "" And txtdate4.Text <> "" Then

            SqlCustomer.SelectCommand += "and convert(varchar,a3.protectdate,111) between'" + ISODate.SetISODate("en", txtdate1.Text.Trim) + "' and '" + ISODate.SetISODate("en", txtdate2.Text.Trim) + "'"
            SqlCustomer.SelectCommand += "and convert(varchar,a4.PayDate,111) between'" + ISODate.SetISODate("en", txtdate3.Text.Trim) + "' and '" + ISODate.SetISODate("en", txtdate4.Text.Trim) + "'"
            SqlCustomer.SelectCommand += " order by a4.PayDate "
            GridView1.DataSource = SqlCustomer
            GridView1.DataBind()

            SqlCountData.SelectCommand += "and convert(varchar,a3.protectdate,111) between'" + ISODate.SetISODate("en", txtdate1.Text.Trim) + "' and '" + ISODate.SetISODate("en", txtdate2.Text.Trim) + "'"
            SqlCountData.SelectCommand += "and convert(varchar,a4.PayDate,111) between'" + ISODate.SetISODate("en", txtdate3.Text.Trim) + "' and '" + ISODate.SetISODate("en", txtdate4.Text.Trim) + "'"
            SqlCountData.SelectCommand += " group by a3.typee "
            GridView2.DataSource = SqlCountData
            GridView2.DataBind()

        End If

       
       
    End Sub

    Protected Sub SqlCustomer_Selected(sender As Object, e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlCustomer.Selected
        '  lblCase.Text = e.AffectedRows
    End Sub
End Class
