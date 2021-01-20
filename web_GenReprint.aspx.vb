Imports System.Drawing
Imports System.Diagnostics
Imports System.ComponentModel
Imports GenCode128
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO


Partial Class web_GenReprint
    Inherits System.Web.UI.Page
    Dim Conn As New SqlConnection(ConfigurationManager.ConnectionStrings("asnbroker").ConnectionString)
    Dim com As SqlCommand
    Dim DataAccess As New DataAccess
    Dim ConvertDate As ISODate = New ISODate()
    Dim FunAll As FuntionAll = New FuntionAll()
    Dim dt, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9 As DataTable

    Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

 
        SqlCustomer.SelectCommand += GetQuery()
        gvDataFind.DataBind()
    End Sub

    Protected Function GetQuery() As String
        Dim str As String = ""
        If ddCondition.SelectedValue = "1" Then 'เลขที่ AppID
            str += " and a1.AppID like '%" & txtFind.Text & "%'"
        ElseIf ddCondition.SelectedValue = "2" Then 'ทะเบียน
            str += " and a2.CarID like '%" & txtFind.Text & "%'"
        ElseIf ddCondition.SelectedValue = "3" Then 'ชื่อ - สกุล
            str += " and a3.FNameTH + ' ' +a3.LNameTH  like  '%" & txtFind.Text & "%'"
        End If

        '
        str += " order by a1.AppID desc "
        Return str
    End Function
    Private Sub SetappAcc()

        Dim tmpdate1 As String
        Dim Count As Integer = 0
        Dim str As String
        Dim Command As SqlCommand
        Dim DataReader As SqlDataReader
        Dim reportX, ex1 As String
        reportX = "1"
        dt = New DataTable

        dt.Columns.Add("0")
        dt.Columns.Add("1")
        dt.Columns.Add("2")
        dt.Columns.Add("3")
        dt.Columns.Add("4")
        dt.Columns.Add("5")
        dt.Columns.Add("6")
        dt.Columns.Add("7")
        dt.Columns.Add("8")
        dt.Columns.Add("9")
        dt.Columns.Add("10")
        dt.Columns.Add("11")
        dt.Columns.Add("12")
        dt.Columns.Add("13")
        dt.Columns.Add("14")
        dt.Columns.Add("15")
        dt.Columns.Add("16")
        dt.Columns.Add("17")
        dt.Columns.Add("18")
        dt.Columns.Add("19")
        dt.Columns.Add("20")
        dt.Columns.Add("21")
        dt.Columns.Add("22")
        dt.Columns.Add("23")
        dt.Columns.Add("24")
        dt.Columns.Add("25")
        dt.Columns.Add("26")
        dt.Columns.Add("27")
        dt.Columns.Add("28")
        dt.Columns.Add("29")
        dt.Columns.Add("30")
        dt.Columns.Add("31")
        dt.Columns.Add("32")
        dt.Columns.Add("33")
        dt.Columns.Add("34")
        dt.Columns.Add("35")
        dt.Columns.Add("36")
        dt.Columns.Add("37")
        dt.Columns.Add("38")
        dt.Columns.Add("39")
        dt.Columns.Add("40")
        dt.Columns.Add("41")
        dt.Columns.Add("42")
        dt.Columns.Add("43")
        dt.Columns.Add("44")
        dt.Columns.Add("45")
        dt.Columns.Add("46")
        dt.Columns.Add("47")
        dt.Columns.Add("48")
        dt.Columns.Add("49")
        dt.Columns.Add("50")
        dt.Columns.Add("51")
        dt.Columns.Add("52")
        dt.Columns.Add("53")
        dt.Columns.Add("54")
        dt.Columns.Add("55")
        dt.Columns.Add("56")
        dt.Columns.Add("57")
        dt.Columns.Add("58")
        dt.Columns.Add("59")
        dt.Columns.Add("60")
        dt.Columns.Add("61")
        dt.Columns.Add("63")
        dt.Columns.Add("64")
        dt.Columns.Add("65")
        dt.Columns.Add("66")
        dt.Columns.Add("67")
        dt.Columns.Add("68")
        dt.Columns.Add("69")
        dt.Columns.Add("70")
        dt.Columns.Add("71")
        dt.Columns.Add("72")
        dt.Columns.Add("73")
        dt.Columns.Add("74")
        dt.Columns.Add("75")
        dt.Columns.Add("76")
        dt.Columns.Add("77")
        dt.Columns.Add("78")
        dt.Columns.Add("87")
        dt.Columns.Add("88")
        dt.Columns.Add("89")
        dt.Columns.Add("90")
        dt.Columns.Add("91")
        dt.Columns.Add("92")
        dt.Columns.Add("93")
        dt.Columns.Add("94")
        dt.Columns.Add("95")
        dt.Columns.Add("96")
        dt.Columns.Add("expprotectdate")
        dt.Columns.Add("sname")

        Conn.Open()

        If (Format$(Date.Now, "yyyy") > 2300) Then
            tmpdate1 = Format$(Date.Now, "yyyy") - 543 & Format$(Date.Now, "MMdd")
        Else
            tmpdate1 = Format$(Date.Now, "yyyymmdd")
        End If

        str = "delete from  tmp_QC_app01 where UserID = '" & Request.Cookies("userIDGen").Value & "'"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()
        str = "delete from tmp_QC_PayCredit where UserID = '" & Request.Cookies("userIDGen").Value & "'"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()
        str = "delete from tmp_QC_app02 where UserID = '" & Request.Cookies("userIDGen").Value & "'"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        str = "select a.* from (select a.*,b.initth from (select  a.*,ProTypeBrand, Addr + ' ' + b.Road AS a1, b.SubDist + ' ' + b.Dist + ' ' + b.Province + ' ' + b.Zip AS a2, '  ' AS a3 from (select a.*,b.fname,b.lname from (select a.*,b.cartypename  from  (SELECT a.initid,a.FNameTH, a.LNameTH, a.Address, a.SAddress, a.Villege, a.Svillege, " +
                              "a.Building, a.SBuilding, a.HomeFloor, a.SHomeFloor, a.HomeRoom, " +
                              "a.SHomeRoom, a.Moo, a.SMoo, a.Soi, a.SSoi, a.Road, a.SRoad, " +
                              "a.SubDist, a.SSubDist, a.Dist, a.SDist, a.Province, a.SProvince, a.Zip, " +
                              "a.SZip, b.AssignTo, b.CarDriverNo, b.CarDriver1 + ' ' + b.CarDriverLname1 as  CarDriver1, b.CarDriverBorn1, b.CarDriver2 + ' ' + b.CarDriverLname2 as  CarDriver2, b.CarDriverBorn2, " +
                             "b.DBornNO1, b.DBornDate1, b.DBornAddr1, b.DBornNO2, b.DBornDate2, b.DBornAddr2, b.CarID, " +
                             "b.CarBuyDate, b.CarFixIn, b.CarSize, b.CarNo, b.CarBoxNo, b.CarType, b.CarYear, b.CarBrand, " +
                              "b.CarSeries,c.AppID, c.AppNO, c.ProDuctID, c.ProDuctIDCarpet, c.AppStatus, " +
                              "c.ProtectDate, c.ProPrice, c.IsProvalue, c.ProValue,c.discounttype,  " +
                              "c.Typeprovalue, c.IsCarpet, c.CarPet, c.FirstPay, c.YearPay, c.Lost_Life1, " +
                              "c.Lost_Life2, c.Lost_Prop1, c.Lost_Prop2, c.Lost_Car1, c.Lost_Car2, " +
                              "c.Car_Fire, c.Acc_Lost1, c.Acc_Lost2, c.Acc_Lost3, c.Acc_Lost4, " +
                              "c.Maintain, c.Insure, c.Apprela, c.Pledge, c.PolicyNO, " +
                              "c.PolicyDate, c.SendPolicyDate, a.sname, c.expprotectdate,  " +
                              "c.CarPetNO , c.CarPetDate,c.successdate as createdate,c.appcomment as ASNcomt,'" & reportX & "' as typereport " & ex1 +
                              "FROM TblCustomer a INNER JOIN " +
                              "TblCar b ON a.CusID = b.CusID INNER JOIN " +
                              " TblApplication c ON b.IdCar = c.Idcar  where  appid = '" & lblApp.Text & "'" +
                              " ) a inner join  Tbl_Cartype b  on a.cartype =  b.cartypeid) a inner join tbluser b on a.assignto = b.userid ) a inner join Tbl_ProductType b on a.productid = b.protypeid )  a  inner join TblCustomerInit b  on a.initid = b.initid) a where a.appid not in (SELECT distinct appid  From TblAppDoc Where  Docid = 99 ) order by fnameth,lnameth "

        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader()
        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt.NewRow
                If IsDBNull(DataReader(0)) = False Then
                    dtr("0") = DataReader(0)
                End If
                If IsDBNull(DataReader(1)) = False Then
                    dtr("1") = DataReader(1)
                End If
                If IsDBNull(DataReader(2)) = False Then
                    dtr("2") = DataReader(2)
                End If
                If IsDBNull(DataReader(3)) = False Then
                    dtr("3") = DataReader(3)
                End If
                If IsDBNull(DataReader(4)) = False Then
                    dtr("4") = DataReader(4)
                End If
                If IsDBNull(DataReader(5)) = False Then
                    dtr("5") = DataReader(5)
                End If
                If IsDBNull(DataReader(6)) = False Then
                    dtr("6") = DataReader(6)
                End If
                If IsDBNull(DataReader(7)) = False Then
                    dtr("7") = DataReader(7)
                End If
                If IsDBNull(DataReader(8)) = False Then
                    dtr("8") = DataReader(8)
                End If
                If IsDBNull(DataReader(9)) = False Then
                    dtr("9") = DataReader(9)
                End If
                If IsDBNull(DataReader(10)) = False Then
                    dtr("10") = DataReader(10)
                End If
                If IsDBNull(DataReader(11)) = False Then
                    dtr("11") = DataReader(11)
                End If
                If IsDBNull(DataReader(12)) = False Then
                    dtr("12") = DataReader(12)
                End If
                If IsDBNull(DataReader(13)) = False Then
                    dtr("13") = DataReader(13)
                End If
                If IsDBNull(DataReader(14)) = False Then
                    dtr("14") = DataReader(14)
                End If
                If IsDBNull(DataReader(15)) = False Then
                    dtr("15") = DataReader(15)
                End If
                If IsDBNull(DataReader(16)) = False Then
                    dtr("16") = DataReader(16)
                End If
                If IsDBNull(DataReader(17)) = False Then
                    dtr("17") = DataReader(17)
                End If
                If IsDBNull(DataReader(18)) = False Then
                    dtr("18") = DataReader(18)
                End If
                If IsDBNull(DataReader(19)) = False Then
                    dtr("19") = DataReader(19)
                End If
                If IsDBNull(DataReader(20)) = False Then
                    dtr("20") = DataReader(20)
                End If
                If IsDBNull(DataReader(21)) = False Then
                    dtr("21") = DataReader(21)
                End If
                If IsDBNull(DataReader(22)) = False Then
                    dtr("22") = DataReader(22)
                End If
                If IsDBNull(DataReader(23)) = False Then
                    dtr("23") = DataReader(23)
                End If
                If IsDBNull(DataReader(24)) = False Then
                    dtr("24") = DataReader(24)
                End If
                If IsDBNull(DataReader(25)) = False Then
                    dtr("25") = DataReader(25)
                End If
                If IsDBNull(DataReader(26)) = False Then
                    dtr("26") = DataReader(26)
                End If
                If IsDBNull(DataReader(27)) = False Then
                    dtr("27") = DataReader(27)
                End If
                If IsDBNull(DataReader(28)) = False Then
                    dtr("28") = DataReader(28)
                End If
                If IsDBNull(DataReader(29)) = False Then
                    dtr("29") = DataReader(29)
                End If
                If IsDBNull(DataReader(30)) = False Then
                    dtr("30") = Format(DataReader(30), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader(31)) = False Then
                    dtr("31") = DataReader(31)
                End If
                If IsDBNull(DataReader(32)) = False Then
                    dtr("32") = Format(DataReader(32), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader(33)) = False Then
                    dtr("33") = DataReader(33)
                End If
                If IsDBNull(DataReader(34)) = False Then
                    dtr("34") = Format(DataReader(34), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader(35)) = False Then
                    dtr("35") = DataReader(35)
                End If
                If IsDBNull(DataReader(36)) = False Then
                    dtr("36") = DataReader(36)
                End If
                If IsDBNull(DataReader(37)) = False Then
                    dtr("37") = Format(DataReader(37), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader(38)) = False Then
                    dtr("38") = DataReader(38)
                End If
                If IsDBNull(DataReader(39)) = False Then
                    dtr("39") = DataReader(39)
                End If
                If IsDBNull(DataReader(40)) = False Then
                    dtr("40") = Format(DataReader(40), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader(41)) = False Then
                    dtr("41") = DataReader(41)
                End If
                If IsDBNull(DataReader(42)) = False Then
                    dtr("42") = DataReader(42)
                End If
                If IsDBNull(DataReader(43)) = False Then
                    dtr("43") = DataReader(43)
                End If
                If IsDBNull(DataReader(44)) = False Then
                    dtr("44") = DataReader(44)
                End If
                If IsDBNull(DataReader(45)) = False Then
                    dtr("45") = DataReader(45)
                End If
                If IsDBNull(DataReader(46)) = False Then
                    dtr("46") = DataReader(46)
                End If
                If IsDBNull(DataReader(47)) = False Then
                    dtr("47") = DataReader(47)
                End If
                If IsDBNull(DataReader(48)) = False Then
                    dtr("48") = DataReader(48)
                End If
                If IsDBNull(DataReader(49)) = False Then
                    dtr("49") = DataReader(49)
                End If
                If IsDBNull(DataReader(50)) = False Then
                    dtr("50") = DataReader(50)
                End If
                If IsDBNull(DataReader(51)) = False Then
                    dtr("51") = DataReader(51)
                End If
                If IsDBNull(DataReader(52)) = False Then
                    dtr("52") = DataReader(52)
                End If
                If IsDBNull(DataReader(53)) = False Then
                    dtr("53") = DataReader(53)
                End If
                If IsDBNull(DataReader(54)) = False Then
                    dtr("54") = Format(DataReader(54), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader(55)) = False Then
                    dtr("55") = Format(DataReader(55), "###,###,##0.#0") 'proprice
                End If
                If IsDBNull(DataReader(56)) = False Then 'IsProvalue
                    dtr("56") = DataReader(56)
                End If
                If IsDBNull(DataReader(57)) = False Then
                    dtr("57") = Format(DataReader(57), "###,###,##0.#0") 'เบี้ยประกันภัย (provalue)
                End If
                If IsDBNull(DataReader(58)) = False Then 'discounttype
                    dtr("58") = DataReader(58)
                End If
                If IsDBNull(DataReader(59)) = False Then 'typeProvalue
                    dtr("59") = DataReader(59)
                End If
                If IsDBNull(DataReader(60)) = False Then
                    dtr("60") = DataReader(60) 'IsCarpet
                End If
                If IsDBNull(DataReader(61)) = False Then
                    dtr("61") = Format(DataReader(61), "###,###,##0.#0") 'พรบ.(carpet)
                End If
                If IsDBNull(DataReader(63)) = False Then
                    dtr("63") = Format(DataReader(63), "###,###,##0.#0") ' จ่ายรวม(totalx)
                End If
                If IsDBNull(DataReader(64)) = False Then
                    dtr("64") = Format(DataReader(64), "###,###,##0.#0") 'ความเสียหายต่อชีวิต / คน (Lost_life1)
                End If
                If IsDBNull(DataReader(65)) = False Then
                    dtr("65") = Format(DataReader(65), "###,###,##0.#0") 'ความเสียหายต่อชีวิต / ครั้ง (Lost_Life2)
                End If
                If IsDBNull(DataReader(66)) = False Then
                    dtr("66") = Format(DataReader(66), "###,###,##0.#0") 'ความเสียหายต่อทรัพย์สิน/ครั้ง (Lost_Prop)1)
                End If
                If IsDBNull(DataReader(67)) = False Then
                    dtr("67") = Format(DataReader(67), "###,###,##0.#0") 'Lost_prop2
                End If
                If IsDBNull(DataReader(68)) = False Then
                    dtr("68") = Format(DataReader(68), "###,###,##0.#0") ' Lost_Car1
                End If
                If IsDBNull(DataReader(69)) = False Then
                    dtr("69") = Format(DataReader(69), "###,###,##0.#0") 'Lost_car2
                End If
                If IsDBNull(DataReader(70)) = False Then
                    dtr("70") = Format(DataReader(70), "###,###,##0.#0") 'car_fire
                End If
                If IsDBNull(DataReader(71)) = False Then
                    dtr("71") = DataReader(71) 'acc_lost
                End If
                If IsDBNull(DataReader(72)) = False Then
                    dtr("72") = Format(DataReader(72), "###,###,##0.#0")
                End If
                If IsDBNull(DataReader(73)) = False Then
                    dtr("73") = DataReader(73)
                End If
                If IsDBNull(DataReader(74)) = False Then
                    dtr("74") = Format(DataReader(74), "###,###,##0.#0")
                End If
                If IsDBNull(DataReader(75)) = False Then
                    dtr("75") = Format(DataReader(75), "###,###,##0.#0")
                End If
                If IsDBNull(DataReader(76)) = False Then
                    dtr("76") = Format(DataReader(76), "###,###,##0.#0")
                End If
                If IsDBNull(DataReader(77)) = False Then
                    dtr("77") = DataReader(77)
                End If
                If IsDBNull(DataReader(78)) = False Then
                    dtr("78") = DataReader(78)
                End If
                If IsDBNull(DataReader(87)) = False Then
                    dtr("87") = DataReader(87)
                End If
                If IsDBNull(DataReader(88)) = False Then
                    dtr("88") = DataReader(88)
                End If
                If IsDBNull(DataReader(89)) = False Then
                    dtr("89") = DataReader(89)
                End If
                If IsDBNull(DataReader(90)) = False Then
                    dtr("90") = DataReader(90)
                End If
                If IsDBNull(DataReader(91)) = False Then
                    dtr("91") = DataReader(91)
                End If
                If IsDBNull(DataReader(92)) = False Then
                    dtr("92") = DataReader(92)
                End If
                If IsDBNull(DataReader(93)) = False Then
                    dtr("93") = DataReader(93)
                End If
                If IsDBNull(DataReader(94)) = False Then
                    dtr("94") = DataReader(94)
                End If
                If IsDBNull(DataReader(95)) = False Then
                    dtr("95") = DataReader(95)
                End If
                If IsDBNull(DataReader(96)) = False Then
                    dtr("96") = DataReader(96)
                End If
                If IsDBNull(DataReader("expprotectdate")) = False Then
                    dtr("expprotectdate") = Format(DataReader("expprotectdate"), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader("sname")) = False Then
                    dtr("sname") = DataReader("sname")
                End If
                dt.Rows.Add(dtr)
            End While
        End If
        DataReader.Close()

        Dim sName As String = dt.Rows(0).Item("sname")
        'sName = Trim(Mid(sName, InStr(1, sName, "คุณ") + 3, 255))
        'sName = Trim(Replace(sName, "คุณ", "", 1, 255))

        str = "insert into tmp_QC_app01 ( " +
        " initid, FNameTH, LNameTH, Address, SAddress, Villege, Svillege, Building, SBuilding, HomeFloor, SHomeFloor, HomeRoom, SHomeRoom, Moo, " +
        " SMoo, Soi, SSoi, Road, SRoad, SubDist, SSubDist, Dist, SDist, Province, SProvince, Zip, SZip, AssignTo, CarDriverNo, CarDriver1,CarDriverBorn1, " +
        " CarDriver2, CarDriverBorn2, DBornNO1, DBornDate1, DBornAddr1, DBornNO2, DBornDate2, DBornAddr2, CarID, CarBuyDate, CarFixIn, CarSize, " +
        " CarNo, CarBoxNo, CarType, CarYear, CarBrand, CarSeries, AppID, AppNO, ProDuctID, ProDuctIDCarpet, AppStatus, ProtectDate, ProPrice, IsProvalue , " +
        " ProValue, discounttype, Typeprovalue, IsCarpet, CarPet, FirstPay, YearPay, Lost_Life1, Lost_Life2, Lost_Prop1, Lost_Prop2, Lost_Car1, Lost_Car2 ," +
        " Car_Fire, Acc_Lost1, Acc_Lost2, Acc_Lost3, Acc_Lost4, Maintain, Insure, Apprela, Pledge,ASNcomt, typereport,  cartypename, fname, lname, ProTypeBrand,  a1 , a2, a3, initth,expprotectdate,sname,UserID ) " +
        " values ( " +
          " '" & dt.Rows(0).Item("0") & "','" & dt.Rows(0).Item("1") & "','" & dt.Rows(0).Item("2") & "','" & dt.Rows(0).Item("3") & "','" & dt.Rows(0).Item("4") & "','" & dt.Rows(0).Item("5") & "','" & dt.Rows(0).Item("6") & "','" & dt.Rows(0).Item("7") & "','" & dt.Rows(0).Item("8") & "','" & dt.Rows(0).Item("9") & "' ," +
        " '" & dt.Rows(0).Item("10") & "','" & dt.Rows(0).Item("11") & "','" & dt.Rows(0).Item("12") & "','" & dt.Rows(0).Item("13") & "','" & dt.Rows(0).Item("14") & "','" & dt.Rows(0).Item("15") & "','" & dt.Rows(0).Item("16") & "','" & dt.Rows(0).Item("17") & "','" & dt.Rows(0).Item("18") & "','" & dt.Rows(0).Item("19") & "', " +
        " '" & dt.Rows(0).Item("20") & "','" & dt.Rows(0).Item("21") & "','" & dt.Rows(0).Item("22") & "','" & dt.Rows(0).Item("23") & "','" & dt.Rows(0).Item("24") & "','" & dt.Rows(0).Item("25") & "','" & dt.Rows(0).Item("26") & "'," & dt.Rows(0).Item("27") & ",'" & dt.Rows(0).Item("28") & "','" & dt.Rows(0).Item("29") & "'," +
        " '" & dt.Rows(0).Item("30") & "','" & dt.Rows(0).Item("31") & "','" & (dt.Rows(0).Item("32")) & "','" & dt.Rows(0).Item("33") & "','" & (dt.Rows(0).Item("34")) & "','" & dt.Rows(0).Item("35") & "','" & dt.Rows(0).Item("36") & "','" & (dt.Rows(0).Item("37")) & "','" & dt.Rows(0).Item("38") & "','" & dt.Rows(0).Item("39") & "' ," +
        " '" & (dt.Rows(0).Item("40")) & "','" & dt.Rows(0).Item("41") & "','" & dt.Rows(0).Item("42") & "','" & dt.Rows(0).Item("43") & "','" & dt.Rows(0).Item("44") & "','" & dt.Rows(0).Item("45") & "','" & dt.Rows(0).Item("46") & "','" & dt.Rows(0).Item("47") & "','" & dt.Rows(0).Item("48") & "','" & dt.Rows(0).Item("49") & "' ," +
        " '" & dt.Rows(0).Item("50") & "','" & dt.Rows(0).Item("51") & "','" & dt.Rows(0).Item("52") & "','" & dt.Rows(0).Item("53") & "','" & (dt.Rows(0).Item("54")) & "','" & dt.Rows(0).Item("55") & "','" & dt.Rows(0).Item("56") & "','" & dt.Rows(0).Item("57") & "','" & dt.Rows(0).Item("58") & "','" & dt.Rows(0).Item("59") & "' ," +
        " '" & dt.Rows(0).Item("60") & "','" & dt.Rows(0).Item("61") & "',0,'" & dt.Rows(0).Item("63") & "','" & dt.Rows(0).Item("64") & "','" & dt.Rows(0).Item("65") & "','" & dt.Rows(0).Item("66") & "','" & dt.Rows(0).Item("67") & "','" & dt.Rows(0).Item("68") & "','" & dt.Rows(0).Item("69") & "' ," +
        " '" & dt.Rows(0).Item("70") & "','" & dt.Rows(0).Item("71") & "','" & dt.Rows(0).Item("72") & "','" & dt.Rows(0).Item("73") & "','" & dt.Rows(0).Item("74") & "','" & dt.Rows(0).Item("75") & "','" & dt.Rows(0).Item("76") & "','" & dt.Rows(0).Item("77") & "','" & dt.Rows(0).Item("78") & "','" & dt.Rows(0).Item("87") & "' ," +
        " '" & dt.Rows(0).Item("88") & "','" & dt.Rows(0).Item("89") & "','" & dt.Rows(0).Item("90") & "','" & dt.Rows(0).Item("91") & "','" & dt.Rows(0).Item("92") & "','" & dt.Rows(0).Item("93") & "','" & dt.Rows(0).Item("94") & "','" & dt.Rows(0).Item("95") & "','" & dt.Rows(0).Item("96") & "','" & dt.Rows(0).Item("expprotectdate") & "','" & sName & "','" & Request.Cookies("userIDGen").Value & "') "

        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        '''''''''''''''''''
        'Call SetCodePrint()


        str = "select  m.*,a.*,b.*,c.*,d.*, e.*,f.*  from  (select a.*,b.initth   from (select  a.*,ProTypeBrand, Addr + ' ' + b.Road AS a1, b.SubDist + ' ' + b.Dist + ' ' + b.Province + ' ' + b.Zip AS a2, 'â·Ã : ' + b.Tel + '  ' AS a3 from (select a.*,b.fname,b.lname from (select a.*,b.cartypename  from  (SELECT      " +
                        "c.AppID,b.CarType,b.assignto,c.ProDuctID,a.initid," +
                        "c.CarPetNO , c.CarPetDate,c.successdate as createdate,c.appcomment as ASNcomt,'" & reportX & "' as typereport " & ex1 +
              "FROM TblCustomer a INNER JOIN " +
                        "TblCar b ON a.CusID = b.CusID INNER JOIN " +
                      " TblApplication c ON b.IdCar = c.Idcar  where appid = '" & lblApp.Text & "'" +
  " ) a inner join  Tbl_Cartype b  on a.cartype =  b.cartypeid) a inner join tbluser b on a.assignto = b.userid ) a inner join Tbl_ProductType b on a.productid = b.protypeid )  a  inner join TblCustomerInit b  on a.initid = b.initid ) m left join " +
   " (select appid appid1 ,Typepay Typepay1,convert(int,payid) payid1,AppointDate AppointDate1,totalpay totalpay1 from tblapppay where payid = 1) a on m.appid = a.appid1 left join " +
      " (select appid appid2 ,Typepay Typepay2,convert(int,payid) payid2,AppointDate AppointDate2,totalpay totalpay2 from tblapppay where payid = 2) b on m.appid = b.appid2 left join " +
      " (select appid appid3 ,Typepay Typepay3,convert(int,payid) payid3,AppointDate AppointDate3,totalpay totalpay3 from tblapppay where payid = 3) c on m.appid = c.appid3 left join " +
      " (select appid appid4 ,Typepay Typepay4,convert(int,payid) payid4,AppointDate AppointDate4,totalpay totalpay4 from tblapppay where payid = 4) d on m.appid = d.appid4  left join " +
          " (select appid appid5 ,Typepay Typepay5,convert(int,payid) payid5,AppointDate AppointDate5,totalpay totalpay5 from tblapppay where payid = 5) e on m.appid = e.appid5 left join " +
      " (select appid appid6 ,Typepay Typepay6,convert(int,payid) payid6,AppointDate AppointDate6,totalpay totalpay6 from tblapppay where payid = 6) f on m.appid = f.appid6 "
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader()
        dt2 = New DataTable

        dt2.Columns.Add("0")
        dt2.Columns.Add("1")
        dt2.Columns.Add("2")
        dt2.Columns.Add("3")
        dt2.Columns.Add("4")
        dt2.Columns.Add("5")
        dt2.Columns.Add("7")
        dt2.Columns.Add("8")
        dt2.Columns.Add("9")
        dt2.Columns.Add("10")
        dt2.Columns.Add("11")
        dt2.Columns.Add("12")
        dt2.Columns.Add("13")
        dt2.Columns.Add("14")
        dt2.Columns.Add("15")
        dt2.Columns.Add("16")
        dt2.Columns.Add("17")
        dt2.Columns.Add("18")
        dt2.Columns.Add("19")
        dt2.Columns.Add("20")
        dt2.Columns.Add("22")
        dt2.Columns.Add("23")
        dt2.Columns.Add("24")
        dt2.Columns.Add("25")
        dt2.Columns.Add("27")
        dt2.Columns.Add("28")
        dt2.Columns.Add("29")
        dt2.Columns.Add("30")
        dt2.Columns.Add("32")
        dt2.Columns.Add("33")
        dt2.Columns.Add("34")
        dt2.Columns.Add("35")
        dt2.Columns.Add("37")
        dt2.Columns.Add("38")
        dt2.Columns.Add("39")
        dt2.Columns.Add("40")
        dt2.Columns.Add("42")
        dt2.Columns.Add("43")
        dt2.Columns.Add("44")
        dt2.Columns.Add("45")
        dt2.Columns.Add("47")
        dt2.Columns.Add("AppointDate1")
        dt2.Columns.Add("AppointDate2")
        dt2.Columns.Add("AppointDate3")
        dt2.Columns.Add("AppointDate4")
        dt2.Columns.Add("AppointDate5")
        dt2.Columns.Add("AppointDate6")

        Dim d1, d2, d3, d4, d5, d6 As String

        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt2.NewRow
                If IsDBNull(DataReader(0)) = False Then
                    dtr("0") = DataReader(0)
                End If
                If IsDBNull(DataReader(1)) = False Then
                    dtr("1") = DataReader(1)
                End If
                If IsDBNull(DataReader(2)) = False Then
                    dtr("2") = DataReader(2)
                End If
                If IsDBNull(DataReader(3)) = False Then
                    dtr("3") = DataReader(3)
                End If
                If IsDBNull(DataReader(4)) = False Then
                    dtr("4") = DataReader(4)
                End If
                If IsDBNull(DataReader(5)) = False Then
                    dtr("5") = DataReader(5)
                End If

                If IsDBNull(DataReader(7)) = False Then
                    dtr("7") = Format(DataReader(7), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader(8)) = False Then
                    dtr("8") = DataReader(8)
                End If
                If IsDBNull(DataReader(9)) = False Then
                    dtr("9") = DataReader(9)
                End If
                If IsDBNull(DataReader(10)) = False Then
                    dtr("10") = DataReader(10)
                End If
                If IsDBNull(DataReader(11)) = False Then
                    dtr("11") = DataReader(11)
                End If
                If IsDBNull(DataReader(12)) = False Then
                    dtr("12") = DataReader(12)
                End If
                If IsDBNull(DataReader(13)) = False Then
                    dtr("13") = DataReader(13)
                End If
                If IsDBNull(DataReader(14)) = False Then
                    dtr("14") = DataReader(14)
                End If
                If IsDBNull(DataReader(15)) = False Then
                    dtr("15") = DataReader(15)
                End If
                If IsDBNull(DataReader(16)) = False Then
                    dtr("16") = DataReader(16)
                End If
                If IsDBNull(DataReader(17)) = False Then
                    dtr("17") = DataReader(17)
                End If
                If IsDBNull(DataReader(18)) = False Then
                    dtr("18") = DataReader(18)
                End If
                If IsDBNull(DataReader(19)) = False Then
                    dtr("19") = DataReader(19)
                End If
                If IsDBNull(DataReader(20)) = False Then
                    dtr("20") = DataReader(20)
                End If

                If IsDBNull(DataReader(22)) = False Then
                    dtr("22") = DataReader(22)
                End If
                If IsDBNull(DataReader(23)) = False Then
                    dtr("23") = DataReader(23)
                End If
                If IsDBNull(DataReader(24)) = False Then
                    dtr("24") = DataReader(24)
                End If
                If IsDBNull(DataReader(25)) = False Then
                    dtr("25") = DataReader(25)
                End If

                If IsDBNull(DataReader(27)) = False Then
                    dtr("27") = DataReader(27)
                End If
                If IsDBNull(DataReader(28)) = False Then
                    dtr("28") = DataReader(28)
                End If
                If IsDBNull(DataReader(29)) = False Then
                    dtr("29") = DataReader(29)
                End If
                If IsDBNull(DataReader(30)) = False Then
                    dtr("30") = DataReader(30)
                End If

                If IsDBNull(DataReader(32)) = False Then
                    dtr("32") = DataReader(32)
                End If
                If IsDBNull(DataReader(33)) = False Then
                    dtr("33") = DataReader(33)
                End If
                If IsDBNull(DataReader(34)) = False Then
                    dtr("34") = DataReader(34)
                End If
                If IsDBNull(DataReader(35)) = False Then
                    dtr("35") = DataReader(35)
                End If

                If IsDBNull(DataReader(37)) = False Then
                    dtr("37") = DataReader(37)
                End If
                If IsDBNull(DataReader(38)) = False Then
                    dtr("38") = DataReader(38)
                End If
                If IsDBNull(DataReader(39)) = False Then
                    dtr("39") = DataReader(39)
                End If
                If IsDBNull(DataReader(40)) = False Then
                    dtr("40") = DataReader(40)
                End If

                If IsDBNull(DataReader(42)) = False Then
                    dtr("42") = DataReader(42)
                End If
                If IsDBNull(DataReader(43)) = False Then
                    dtr("43") = DataReader(43)
                End If
                If IsDBNull(DataReader(44)) = False Then
                    dtr("44") = DataReader(44)
                End If
                If IsDBNull(DataReader(45)) = False Then
                    dtr("45") = DataReader(45)
                End If

                If IsDBNull(DataReader(47)) = False Then
                    dtr("47") = DataReader(47)
                End If
                dt2.Rows.Add(dtr)
                '-----------------------------------------------------------------------------------------
                '--------------------------------------------------------------------------------
                If IsDBNull(DataReader("AppointDate1")) = True Then
                    d1 = "  "
                Else
                    d1 = Format(DataReader("AppointDate1"), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader("AppointDate2")) = True Then
                    d2 = "  "
                Else
                    d2 = Format(DataReader("AppointDate2"), "dd/MM/yyyy")
                End If

                If IsDBNull(DataReader("AppointDate3")) = True Then
                    d3 = "  "
                Else
                    d3 = Format(DataReader("AppointDate3"), "dd/MM/yyyy")
                End If

                If IsDBNull(DataReader("AppointDate4")) = True Then
                    d4 = "  "
                Else
                    d4 = Format(DataReader("AppointDate4"), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader("AppointDate5")) = True Then
                    d5 = "  "
                Else
                    d5 = Format(DataReader("AppointDate5"), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader("AppointDate6")) = True Then
                    d6 = "  "
                Else
                    d6 = Format(DataReader("AppointDate6"), "dd/MM/yyyy")
                End If

            End While
        End If
        DataReader.Close()

        str = " insert  into  tmp_QC_PayCredit (" +
         " AppID, CarType, assignto, ProDuctID, initid, CarPetNO, createdate, ASNcomt, typereport, cartypename, " +
        " fname, lname, ProTypeBrand, a1, a2, a3, initth, appid1, Typepay1, payid1, AppointDate1, totalpay1, appid2, Typepay2, payid2, AppointDate2, totalpay2, " +
        " appid3 , Typepay3, payid3, AppointDate3, totalpay3, appid4, Typepay4, payid4, AppointDate4, totalpay4 , appid5, Typepay5, payid5, AppointDate5, totalpay5, appid6, Typepay6, payid6, AppointDate6, totalpay6,UserID  )" +
        " values ( " +
          " '" & dt2.Rows(0).Item("0") & "','" & dt2.Rows(0).Item("1") & "','" & dt2.Rows(0).Item("2") & "','" & dt2.Rows(0).Item("3") & "','" & dt2.Rows(0).Item("4") & "','" & dt2.Rows(0).Item("5") & "','" & (dt2.Rows(0).Item("7")) & "','" & dt2.Rows(0).Item("8") & "','" & dt2.Rows(0).Item("9") & "' ," +
        " '" & dt2.Rows(0).Item("10") & "','" & dt2.Rows(0).Item("11") & "','" & dt2.Rows(0).Item("12") & "','" & dt2.Rows(0).Item("13") & "','" & dt2.Rows(0).Item("14") & "','" & dt2.Rows(0).Item("15") & "','" & dt2.Rows(0).Item("16") & "','" & dt2.Rows(0).Item("17") & "','" & FunAll.chkNull(dt2.Rows(0).Item("18"), 0) & "','" & FunAll.chkNull(dt2.Rows(0).Item("19"), 0) & "', " +
        " '" & FunAll.chkNull(dt2.Rows(0).Item("20"), 0) & "','" & d1 & "','" & FunAll.chkNull(dt2.Rows(0).Item("22"), 1) & "','" & FunAll.chkNull(dt2.Rows(0).Item("23"), 0) & "','" & FunAll.chkNull(dt2.Rows(0).Item("24"), 0) & "','" & FunAll.chkNull(dt2.Rows(0).Item("25"), 0) & "','" & d2 & "','" & FunAll.chkNull(dt2.Rows(0).Item("27"), 1) & "','" & FunAll.chkNull(dt2.Rows(0).Item("28"), 0) & "','" & FunAll.chkNull(dt2.Rows(0).Item("29"), 0) & "', " +
        " '" & FunAll.chkNull(dt2.Rows(0).Item("30"), 0) & "','" & d3 & "','" & FunAll.chkNull(dt2.Rows(0).Item("32"), 1) & "','" & FunAll.chkNull(dt2.Rows(0).Item("33"), 0) & "','" & FunAll.chkNull(dt2.Rows(0).Item("34"), 0) & "','" & FunAll.chkNull(dt2.Rows(0).Item("35"), 0) & "','" & d4 & "','" & FunAll.chkNull(dt2.Rows(0).Item("37"), 1) & "' ,'" & FunAll.chkNull(dt2.Rows(0).Item("38"), 0) & "','" & FunAll.chkNull(dt2.Rows(0).Item("39"), 0) & "','" & FunAll.chkNull(dt2.Rows(0).Item("40"), 0) & "','" & d5 & "','" & FunAll.chkNull(dt2.Rows(0).Item("42"), 1) & "' ,'" & FunAll.chkNull(dt2.Rows(0).Item("43"), 0) & "','" & FunAll.chkNull(dt2.Rows(0).Item("44"), 0) & "','" & FunAll.chkNull(dt2.Rows(0).Item("45"), 0) & "','" & d6 & "','" & FunAll.chkNull(dt2.Rows(0).Item("47"), 1) & "','" & Request.Cookies("userIDGen").Value & "' ) "
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()


        Dim pd1 As String = ""
        str = " SELECT TblApplication.AppID, TblCar.IdCard1, TblCar.TypeCard1, TblCar.IdCard2, TblCar.TypeCard2, TblCustomer.AddressRemark, ProtectDateCarpet, expProtectDateCarpet, " +
                             " ' ' AS tmp3, ' ' AS tmp4, ' ' AS tmp5 , ' ' AS tmp6, ' ' AS tmp7, ' ' AS tmp8,iscarpet " +
      "    FROM TblCustomer INNER JOIN " +
                            " TblCar ON TblCustomer.CusID = TblCar.CusID INNER JOIN " +
                            " TblApplication ON TblCar.IdCar = TblApplication.Idcar where  appid = '" & lblApp.Text & "'"
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader()
        dt3 = New DataTable

        dt3.Columns.Add("0")
        dt3.Columns.Add("1")
        dt3.Columns.Add("2")
        dt3.Columns.Add("3")
        dt3.Columns.Add("4")
        dt3.Columns.Add("5")
        dt3.Columns.Add("6")
        dt3.Columns.Add("7")
        dt3.Columns.Add("8")
        dt3.Columns.Add("9")
        dt3.Columns.Add("10")
        dt3.Columns.Add("11")
        dt3.Columns.Add("12")
        dt3.Columns.Add("13")
        dt3.Columns.Add("iscarpet")
        dt3.Columns.Add("ProtectDateCarpet")
        dt3.Columns.Add("expProtectDateCarpet")

        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt3.NewRow
                If IsDBNull(DataReader(0)) = False Then
                    dtr("0") = DataReader(0)
                End If
                If IsDBNull(DataReader(1)) = False Then
                    dtr("1") = DataReader(1)
                End If
                If IsDBNull(DataReader(2)) = False Then
                    dtr("2") = DataReader(2)
                End If
                If IsDBNull(DataReader(3)) = False Then
                    dtr("3") = DataReader(3)
                End If
                If IsDBNull(DataReader(4)) = False Then
                    dtr("4") = DataReader(4)
                End If
                If IsDBNull(DataReader(5)) = False Then
                    dtr("5") = DataReader(5)
                End If
                If IsDBNull(DataReader(6)) = False Then
                    dtr("6") = Format(DataReader(6), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader(7)) = False Then
                    dtr("7") = Format(DataReader(7), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader(8)) = False Then
                    dtr("8") = DataReader(8)
                End If
                If IsDBNull(DataReader(9)) = False Then
                    dtr("9") = DataReader(9)
                End If
                If IsDBNull(DataReader(10)) = False Then
                    dtr("10") = DataReader(10)
                End If
                If IsDBNull(DataReader(11)) = False Then
                    dtr("11") = DataReader(11)
                End If
                If IsDBNull(DataReader(12)) = False Then
                    dtr("12") = DataReader(12)
                End If
                If IsDBNull(DataReader(13)) = False Then
                    dtr("13") = DataReader(13)
                End If
                If IsDBNull(DataReader("iscarpet")) = False Then
                    dtr("iscarpet") = DataReader("iscarpet")
                End If
                If IsDBNull(DataReader("ProtectDateCarpet")) = False Then
                    dtr("ProtectDateCarpet") = Format(DataReader("ProtectDateCarpet"), "dd/MM/yyyy")
                End If
                If IsDBNull(DataReader("expProtectDateCarpet")) = False Then
                    dtr("expProtectDateCarpet") = Format(DataReader("expProtectDateCarpet"), "dd/MM/yyyy")
                End If
                dt3.Rows.Add(dtr)
            End While
        End If
        DataReader.Close()

        If dt3.Rows(0).Item("iscarpet") = 1 Then
            'Session("pd1") = "ระยะเวลา พ.ร.บ.: " & dt3.Rows(0).Item("ProtectDateCarpet") + " - " + dt3.Rows(0).Item("expProtectDateCarpet")
            Session("pd1") = dt3.Rows(0).Item("ProtectDateCarpet") + "  -  " + dt3.Rows(0).Item("expProtectDateCarpet")
        Else
            'Session("pd1") = "ระยะเวลา พ.ร.บ.: - "
            Session("pd1") = "-"
        End If

        str = "insert into tmp_QC_app02 ( AppID, IdCard1, TypeCard1, IdCard2, TypeCard2, AddressRemark, tmp1, tmp2, tmp3, tmp4, tmp5, tmp6, tmp7, tmp8,UserID) values ( " +
        " '" & dt3.Rows(0).Item("0") & "','" & dt3.Rows(0).Item("1") & "','" & dt3.Rows(0).Item("2") & "','" & dt3.Rows(0).Item("3") & "','" & dt3.Rows(0).Item("4") & "','" & dt3.Rows(0).Item("5") & "','" & dt3.Rows(0).Item("6") & "','" & dt3.Rows(0).Item("7") & "','" & dt3.Rows(0).Item("8") & "' ," +
       " '" & dt3.Rows(0).Item("9") & "','" & dt3.Rows(0).Item("10") & "','" & dt3.Rows(0).Item("11") & "','" & dt3.Rows(0).Item("12") & "','" & dt3.Rows(0).Item("13") & "','" & Request.Cookies("userIDGen").Value & "')"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        str = "select  distinct payid from tblapppay where appid = '" & lblApp.Text & "'"
        Dim Count2 As Integer = 0
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader()
        If DataReader.HasRows Then
            While DataReader.Read
                Count2 += 1
            End While
        End If
        DataReader.Close()

        str = "update  tmp_QC_PayCredit set  asncomt = '" & Count2 & "' WHERE UserID = '" & Request.Cookies("userIDGen").Value & "'"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        str = " SELECT TblCustomer.Sname " +
              " FROM TblCar INNER JOIN " +
              " TblApplication TblApplication_1 ON TblCar.IdCar = TblApplication_1.Idcar INNER JOIN " +
              " TblCustomer ON TblCar.CusID = TblCustomer.CusID " +
              " where appid = '" & lblApp.Text & "'"
        Command = New SqlCommand(str, Conn)
        DataReader = Command.ExecuteReader()
        dt4 = New DataTable
        dt4.Columns.Add("sName")

        If DataReader.HasRows Then
            While DataReader.Read
                Dim dtr As DataRow = dt4.NewRow
                If IsDBNull(DataReader("sName")) = False Then
                    dtr("sName") = DataReader("sName")
                End If
                dt4.Rows.Add(dtr)
            End While
        End If
        DataReader.Close()

        str = "update  tmp_QC_PayCredit set  a1 = '" & dt4.Rows(0).Item("sName") & "' WHERE UserID = '" & Request.Cookies("userIDGen").Value & "'"
        Command = New SqlCommand(str, Conn)
        Command.ExecuteNonQuery()

        Conn.Close()



    End Sub
    'Add By NA 20170928 ตาม Req.พี่อ้า เรื่อง LINE สร้าง Folder ใบคำขอ+Payment 
    Private Sub Auto_File()
        '1.AppID
        '2.Idcar
        Conn.Open()
        Dim dttmpLine As New DataTable
        Dim query = New System.Text.StringBuilder()

        query.Append(" SELECT   tmp_QC_PayCredit.UserID as gg,   tmp_QC_PayCredit.*, tmp_QC_app02.*, tmp_QC_app01.* ,tblcar.refno,tmp_QC_app01.AppID as appid01,isnull(Tbl_ProductType.hotLINE,'') as 'hotLINE01' ")
        query.Append(" FROM     tmp_QC_PayCredit ")
        query.Append(" INNER JOIN	tmp_QC_app02 ON tmp_QC_PayCredit.AppID = tmp_QC_app02.AppID ")
        query.Append(" INNER JOIN	tmp_QC_app01 ON tmp_QC_app02.AppID = tmp_QC_app01.AppID")
        query.Append(" inner join tblapplication on tmp_QC_app01.AppID=tblapplication.appid")
        query.Append(" inner join tblcar on tblapplication.idcar=tblcar.idcar")
        query.Append(" inner join Tbl_ProductType on Tbl_ProductType.ProTypeID=tblapplication.ProDuctID ")

        query.Append(" WHERE tmp_QC_PayCredit.UserID = " & Request.Cookies("userIDGen").Value)
        query.Append(" AND tmp_QC_app02.UserID = " & Request.Cookies("userIDGen").Value)
        query.Append(" AND tmp_QC_app01.UserID = " & Request.Cookies("userIDGen").Value)
        query.Append(" order by tmp_QC_PayCredit.UserID")

        dttmpLine = DataAccess.DataRead(query.ToString)
        If dttmpLine.Rows.Count = 1 Then
            '1.File background 
            Dim P_Server As String = "~/images/LINE/covernote.jpg"
            Dim folder As String = Server.MapPath(P_Server)

            '2.Process 

            Dim bm As New Bitmap(folder)
            Dim FontName As String = "Angsana New"
            Dim gra As Graphics = Graphics.FromImage(bm)

            Dim B1 As String = ""
            Dim B2 As String = ""
            LINE_Address(dttmpLine, B1, B2)
            'New Data 
            Dim A1 As String = dttmpLine.Rows(0)("sname").ToString()
            Dim refno As String = dttmpLine.Rows(0)("refno").ToString()
            Dim appid01 As String = dttmpLine.Rows(0)("appid01").ToString()


            A1 = "เรียน " & A1 & Environment.NewLine & B1
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(200, 450)) '12

            Dim barcodestr As String = "|010755800027000" & Chr(13) & refno & Chr(13) & appid01 & Chr(13) & "0"
            Dim myimg As Image = Code128Rendering.MakeBarcodeImage(barcodestr, 2, True)
            gra.DrawImage(myimg, 750, 3380, myimg.Width, 75)

            gra.DrawString("สำหรับชำระค่าเบี้ยประกันภัย", New Font(FontName, 36), Brushes.Black, New PointF(2040, 3435)) '10
            Dim qe As MessagingToolkit.QRCode.Codec.QRCodeEncoder = New MessagingToolkit.QRCode.Codec.QRCodeEncoder
            Dim myimg1 As Image = qe.Encode(barcodestr)
            gra.DrawImage(myimg1, 2140, 3260, 170, 170)
            gra.DrawString(barcodestr, New Font(FontName, 36), Brushes.Black, New PointF(920, 3435)) '12



            Dim myimgappid As Image = Code128Rendering.MakeBarcodeImage(appid01, 2, True)
            'myimgappid.RotateFlip(RotateFlipType.Rotate270FlipX)
            gra.DrawImage(myimgappid, 1100, 630, myimgappid.Width, 50) 'myimgappid.Height


            gra.DrawString(appid01, New Font(FontName, 30), Brushes.Black, New PointF(1180, 670)) '10
            '
            A1 = ""
            A1 = If((dttmpLine.Rows(0)("initth").ToString() <> "[ไม่ทราบ]"), dttmpLine.Rows(0)("initth").ToString(), "") & dttmpLine.Rows(0)("FNameTH").ToString() & " " & dttmpLine.Rows(0)("LNameTH").ToString()
            A1 = A1 & Environment.NewLine & B2
            gra.DrawString(A1, New Font(FontName, 30), Brushes.Black, New PointF(1490, 520)) '10


            A1 = dttmpLine.Rows(0)("ProTypeBrand").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(350, 740))


            'LINE_Phone(dttmpLine.Rows(0)("ProductID").ToString(), A1)
            A1 = dttmpLine.Rows(0)("hotLINE01").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1265, 740))


            If dttmpLine.Rows(0)("CarDriver1").ToString() <> " " Then
                B1 = "X"
                B2 = ""
            End If
            If dttmpLine.Rows(0)("CarDriver1").ToString() = " " Then
                B1 = ""
                B2 = "X"
            End If

            gra.DrawString(B1, New Font(FontName, 36), Brushes.Black, New PointF(1582, 740))
            gra.DrawString(B2, New Font(FontName, 36), Brushes.Black, New PointF(1985, 740))

            A1 = dttmpLine.Rows(0)("CarDriver1").ToString()

            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(270, 825))
            A1 = ""
            If dttmpLine.Rows(0)("CarDriver1").ToString() <> " " Then
                A1 = dttmpLine.Rows(0)("CarDriverBorn1").ToString()
            End If

            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(995, 825))

            A1 = ""
            If dttmpLine.Rows(0)("CarDriver1").ToString() <> " " Then
                A1 = dttmpLine.Rows(0)("DBornNO1").ToString()
            End If

            gra.DrawString(A1, New Font(FontName, 30), Brushes.Black, New PointF(1350, 830))

            A1 = ""

            If dttmpLine.Rows(0)("CarDriver1").ToString() <> " " Then
                A1 = dttmpLine.Rows(0)("DBornDate1").ToString()
            End If

            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1660, 825))

            A1 = ""
            'D5
            If dttmpLine.Rows(0)("CarDriver1").ToString() <> " " Then
                A1 = dttmpLine.Rows(0)("DBornAddr1").ToString()
            End If
            'A1 = "กรุงเทพมหานคร"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(2070, 825))

            A1 = ""
            'E1
            A1 = dttmpLine.Rows(0)("CarDriver2").ToString()

            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(270, 900))

            A1 = ""
            If dttmpLine.Rows(0)("CarDriver2").ToString() <> " " Then
                A1 = dttmpLine.Rows(0)("CarDriverBorn2").ToString()
            End If
            'A1 = "06/09/2523"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(995, 900))

            A1 = ""
            If dttmpLine.Rows(0)("CarDriver2").ToString() <> " " Then
                A1 = dttmpLine.Rows(0)("DBornNO2").ToString()
            End If
            'A1 = "XXXXXXXX"
            gra.DrawString(A1, New Font(FontName, 30), Brushes.Black, New PointF(1350, 900))

            A1 = ""
            If dttmpLine.Rows(0)("CarDriver2").ToString() <> " " Then
                A1 = dttmpLine.Rows(0)("DBornDate2").ToString()
            End If
            'A1 = "11/03/2548"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1660, 900))


            A1 = ""
            If dttmpLine.Rows(0)("CarDriver2").ToString() <> " " Then
                A1 = dttmpLine.Rows(0)("DBornAddr2").ToString()
            End If
            ' A1 = "กรุงเทพมหานคร"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(2070, 900))

            A1 = ""
            If dttmpLine.Rows(0)("CarDriver1").ToString() <> " " Then
                A1 = dttmpLine.Rows(0)("IdCard1").ToString()
            End If

            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(490, 975))

            A1 = ""

            'Dim F2 As String = ""
            If dttmpLine.Rows(0)("CarDriver2").ToString() <> " " Then
                A1 = dttmpLine.Rows(0)("IdCard2").ToString()
            End If

            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1400, 975))
            A1 = ""

            If dttmpLine.Rows(0)("ProDuctID").ToString() <> "15" Then
                A1 = dttmpLine.Rows(0)("AppNO").ToString()
            Else
                A1 = dttmpLine.Rows(0)("PolicyNO").ToString()
            End If

            'gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(2070, 1050))
           
            A1 = ""
            If dttmpLine.Rows(0)("IsProvalue").ToString() = "1" Then
                A1 = dttmpLine.Rows(0)("ProtectDate").ToString() & "  -  " & dttmpLine.Rows(0)("expprotectdate").ToString()
            End If
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(450, 1050)) '1130
            A1 = ""

            A1 = Session("pd1")
            'A1 = "21/09/2560 - 21/09/2561"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1330, 1050))
            gra.DrawString("1", New Font(FontName, 36), Brushes.Black, New PointF(140, 1280)) '1360

            'I2
            A1 = ""
            If dttmpLine.Rows(0)("discounttype").ToString() > "0" Then
                A1 = dttmpLine.Rows(0)("discounttype").ToString()
            End If
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(250, 1280))

            A1 = ""

            If dttmpLine.Rows(0)("CarBrand").ToString() <> "" And dttmpLine.Rows(0)("CarSeries").ToString() <> "" Then
                A1 = dttmpLine.Rows(0)("CarBrand").ToString() & "/" & Chr(10) & Chr(13) & dttmpLine.Rows(0)("CarSeries").ToString()
            ElseIf dttmpLine.Rows(0)("CarBrand").ToString() <> "" And dttmpLine.Rows(0)("CarSeries").ToString() = "" Then
                A1 = dttmpLine.Rows(0)("CarBrand").ToString() & "/" & Chr(10) & Chr(13) & "-"
            ElseIf dttmpLine.Rows(0)("CarBrand").ToString() = "" And dttmpLine.Rows(0)("CarSeries").ToString() <> "" Then
                A1 = "-" & "/" & Chr(10) & Chr(13) & dttmpLine.Rows(0)("CarSeries").ToString()
            End If
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(375, 1280))
            'I4
            A1 = ""
            A1 = dttmpLine.Rows(0)("CarID").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(840, 1280))

            'I5
            A1 = ""
            If dttmpLine.Rows(0)("CarNo").ToString() <> "" And dttmpLine.Rows(0)("CarBoxNo").ToString() <> "" Then
                A1 = dttmpLine.Rows(0)("CarBoxNo").ToString() & "/" & Chr(10) & Chr(13) & dttmpLine.Rows(0)("CarNo").ToString()

            ElseIf dttmpLine.Rows(0)("CarNo").ToString() <> "" And dttmpLine.Rows(0)("CarBoxNo").ToString() = "" Then
                A1 = "-" & "/" & Chr(10) & Chr(13) & dttmpLine.Rows(0)("CarNo").ToString()
            ElseIf dttmpLine.Rows(0)("CarNo").ToString() = "" And dttmpLine.Rows(0)("CarBoxNo").ToString() <> "" Then
                A1 = dttmpLine.Rows(0)("CarBoxNo").ToString() & "/" & "-"
            End If
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1120, 1280))

            'I6
            A1 = ""
            A1 = dttmpLine.Rows(0)("CarYear").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1570, 1280))

            'I7
            A1 = ""
            A1 = dttmpLine.Rows(0)("cartypename").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1720, 1280))

            'I8
            A1 = ""
            A1 = "- / " & dttmpLine.Rows(0)("CarSize").ToString() & " / -"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1970, 1280))


            'J1
            A1 = ""
            A1 = dttmpLine.Rows(0)("Lost_Car1").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1068, 1570))
            'J2
            A1 = ""
            A1 = dttmpLine.Rows(0)("Acc_Lost1").ToString()
            ' A1 = "999999"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1820, 1629))

            'J3
            A1 = ""
            A1 = dttmpLine.Rows(0)("Acc_Lost4").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1970, 1629))

            'K1
            A1 = ""
            A1 = dttmpLine.Rows(0)("Lost_Life1").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(375, 1634)) '1706


            'K4
            A1 = ""
            If dttmpLine.Rows(0)("IsProvalue").ToString() = "1" Then
                A1 = dttmpLine.Rows(0)("Lost_Life2").ToString()
            End If
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(375, 1699)) '1771
            'K2
            A1 = ""
            A1 = dttmpLine.Rows(0)("Lost_Car2").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1068, 1699))

            'K3
            A1 = ""
            A1 = dttmpLine.Rows(0)("Acc_Lost3").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1850, 1699))

            'k4
            A1 = ""
            A1 = dttmpLine.Rows(0)("Acc_Lost4").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1970, 1699))


            'L1
            A1 = "L1"
            A1 = dttmpLine.Rows(0)("Lost_Prop1").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(375, 1849)) '1921

            'L2
            A1 = ""
            A1 = dttmpLine.Rows(0)("Car_Fire").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1068, 1849))


            'L3
            A1 = "0"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1850, 1825)) '1895
            'L4
            A1 = "0.00"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1970, 1825))

            'M1
            A1 = "0"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1850, 1890)) '1965
            'M2
            A1 = "0.00"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1970, 1890))
            ' N1()
            A1 = "0.00"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(375, 1970)) '2050
            'N2()
            A1 = ""
            A1 = dttmpLine.Rows(0)("Lost_Prop2").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1068, 2020)) '2100

            'N3
            A1 = ""
            A1 = dttmpLine.Rows(0)("Maintain").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1970, 1966)) '2040

            'O1
            A1 = ""
            A1 = dttmpLine.Rows(0)("Insure").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1970, 2050)) '2130


            'P1
            A1 = ""
            A1 = If(dttmpLine.Rows(0)("IsProvalue").ToString() = "1", dttmpLine.Rows(0)("ProValue").ToString(), "0.00")
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(350, 2240)) '2300
            'P2
            A1 = ""
            A1 = If(dttmpLine.Rows(0)("IsCarpet").ToString() = "1", dttmpLine.Rows(0)("CarPet").ToString(), "0.00")
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1068, 2240))
            'P3
            A1 = ""

            Dim AA As Decimal = dttmpLine.Rows(0)("ProValue")
            Dim AB As Decimal = dttmpLine.Rows(0)("CarPet")
            AA = Math.Round(AA + AB, 2)
            gra.DrawString(AA.ToString("N2"), New Font(FontName, 36), Brushes.Black, New PointF(1850, 2240))
            'Q1
            A1 = ""

            Dim TmpQ1 As String = ""

            If dttmpLine.Rows(0)("Typeprovalue").ToString() = "1" Then
                TmpQ1 = "ชั้น1"
            ElseIf dttmpLine.Rows(0)("Typeprovalue").ToString() = "2" Then
                TmpQ1 = "ชั้น3"
            ElseIf dttmpLine.Rows(0)("Typeprovalue").ToString() = "3" Then
                TmpQ1 = "ชั้น3+"
            ElseIf dttmpLine.Rows(0)("Typeprovalue").ToString() = "4" Then
                TmpQ1 = "ชั้น2+"
            End If

            If dttmpLine.Rows(0)("IsProvalue").ToString() = "1" And dttmpLine.Rows(0)("IsCarpet").ToString() = "1" Then
                A1 = "ประกันภัย " & TmpQ1 & " รวม พ.ร.บ."
            ElseIf dttmpLine.Rows(0)("IsProvalue").ToString() = "1" And dttmpLine.Rows(0)("IsCarpet").ToString() = "0" Then
                A1 = "ประกันภัย " & TmpQ1 & " ไม่รวม พ.ร.บ."
            ElseIf dttmpLine.Rows(0)("IsProvalue").ToString() = "0" And dttmpLine.Rows(0)("IsCarpet").ToString() = "1" Then
                A1 = "พ.ร.บ."
            End If
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(500, 2300)) '2380

            A1 = ""
            AA = dttmpLine.Rows(0)("YearPay")
            AB = dttmpLine.Rows(0)("ProValue")
            If AA - AB > 0 Then

                A1 = "* ส่วนลด    " & (AA - AB).ToString("N2") & "   บาท"
            End If

            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1100, 2300))
            Dim Q3 As String = If(dttmpLine.Rows(0)("CarFixIn").ToString() = "1", "X", "")
            Dim Q4 As String = If(dttmpLine.Rows(0)("CarFixIn").ToString() = "0", "X", "")

            gra.DrawString(Q3, New Font(FontName, 36), Brushes.Black, New PointF(1530, 2300))
            gra.DrawString(Q4, New Font(FontName, 36), Brushes.Black, New PointF(1990, 2300))

            'R1
            A1 = ""

            'Dim R1 As String = ""
            Dim TmpR1 As String = Replace(dttmpLine.Rows(0)("discounttype").ToString(), " ", "")

            If dttmpLine.Rows(0)("IsProvalue").ToString() = "1" And TmpR1 = "320" Then
                A1 = "ใช้เพื่อการพาณิชย์ ไม่ใช้เพื่อการบรรทุก และขนส่งสินค้าที่มี ความเสี่ยงภัยสูง เช่น เชื้อเพลิง"
            ElseIf dttmpLine.Rows(0)("IsProvalue").ToString() = "1" And TmpR1 = "220" Then
                A1 = "ใช้เพื่อการพาณิชย์ ไม่ใช้รับจ้างสาธารณะ"
            ElseIf dttmpLine.Rows(0)("IsProvalue") = "1" And TmpR1 = "110" Then
                A1 = "ใช้ส่วนบุคคล ไม่ใช้รับจ้าง หรือให้เช่า"
            ElseIf dttmpLine.Rows(0)("IsProvalue").ToString() = "1" And TmpR1 = "210" Then
                A1 = "ใช้ส่วนบุคคล ไม่ใช้รับจ้าง หรือให้เช่า"
            ElseIf dttmpLine.Rows(0)("IsProvalue").ToString() = "0" Then
                A1 = "ความคุ้มครองตามเงื่อนไข กรมธรรม์ประกันภัยคุ้มครองผู้ประสบภัยจากรถยนต์"
            End If

            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(400, 2387)) '2460

            'R2
            A1 = ""
            A1 = dttmpLine.Rows(0)("createdate").ToString()
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1850, 2387))
            'Dim R2 As String = dttmpLine.Rows(0)("createdate").ToString()
            ''PayNo1
            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("payid1").ToString(), dttmpLine.Rows(0)("payid1").ToString())
            'A1 = "1"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(150, 2570))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("AppointDate1").ToString(), dttmpLine.Rows(0)("AppointDate1").ToString())
            'A1 = "1"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(350, 2570))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("totalpay1").ToString() <> "  ", dttmpLine.Rows(0)("totalpay1").ToString() & ".00", "")
            'A1 = "1"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(680, 2570))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", "เงินสด", If(dttmpLine.Rows(0)("Typepay1").ToString() = "2", "บัตรเครดิต", ""))
            'A1 = "1"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(980, 2570))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("payid4").ToString(), dttmpLine.Rows(0)("payid4").ToString())
            'A1 = "4"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1310, 2570))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("AppointDate4").ToString(), dttmpLine.Rows(0)("AppointDate4").ToString())
            ' A1 = "4"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1510, 2570))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("totalpay4").ToString() <> "  ", dttmpLine.Rows(0)("totalpay4").ToString() & ".00", "")
            'A1 = "4"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1840, 2570))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay4").ToString() = "1", "เงินสด", If(dttmpLine.Rows(0)("Typepay4").ToString() = "2", "บัตรเครดิต", ""))
            ' A1 = "4"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(2140, 2570))
            'pAYID2
            'Dim t1 As String = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("payid2").ToString(), dttmpLine.Rows(0)("payid2").ToString())
            'Dim t2 As String = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("AppointDate2").ToString(), dttmpLine.Rows(0)("AppointDate2").ToString())
            'Dim t3 As String = If(dttmpLine.Rows(0)("totalpay2").ToString() <> "", dttmpLine.Rows(0)("totalpay2").ToString() & ".00", "")
            'Dim t4 As String = If(dttmpLine.Rows(0)("Typepay2").ToString() = "1", "เงินสด", If(dttmpLine.Rows(0)("Typepay2").ToString() = "2", "บัตรเครดิต", ""))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("payid2").ToString(), dttmpLine.Rows(0)("payid2").ToString())
            'A1 = "2"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(150, 2650))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("AppointDate2").ToString(), dttmpLine.Rows(0)("AppointDate2").ToString())
            'A1 = "2"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(350, 2650))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("totalpay2").ToString() <> "  ", dttmpLine.Rows(0)("totalpay2").ToString() & ".00", "")
            'A1 = "2"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(680, 2650))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay2").ToString() = "1", "เงินสด", If(dttmpLine.Rows(0)("Typepay2").ToString() = "2", "บัตรเครดิต", ""))
            'A1 = "2"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(980, 2650))
            ''payid3
            'Dim U1 As String = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("payid3").ToString(), dttmpLine.Rows(0)("payid3").ToString())
            'Dim U2 As String = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("AppointDate3").ToString(), dttmpLine.Rows(0)("AppointDate3").ToString())
            'Dim U3 As String = If(dttmpLine.Rows(0)("totalpay3").ToString() <> "", dttmpLine.Rows(0)("totalpay3").ToString() & ".00", "")
            'Dim U4 As String = If(dttmpLine.Rows(0)("Typepay3").ToString() = "1", "เงินสด", If(dttmpLine.Rows(0)("Typepay3").ToString() = "2", "บัตรเครดิต", ""))
            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("payid3").ToString(), dttmpLine.Rows(0)("payid3").ToString())
            'A1 = "3"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(150, 2730))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("AppointDate3").ToString(), dttmpLine.Rows(0)("AppointDate3").ToString())
            'A1 = "3"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(350, 2730))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("totalpay3").ToString() <> "  ", dttmpLine.Rows(0)("totalpay3").ToString() & ".00", "")
            'A1 = "3"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(680, 2730))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay3").ToString() = "1", "เงินสด", If(dttmpLine.Rows(0)("Typepay3").ToString() = "2", "บัตรเครดิต", ""))
            'A1 = "3"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(980, 2730))

            ''PayNo4
            'Dim S5 As String = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("payid4").ToString(), dttmpLine.Rows(0)("payid4").ToString())
            'Dim S6 As String = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("AppointDate4").ToString(), dttmpLine.Rows(0)("AppointDate4").ToString())
            'Dim S7 As String = If(dttmpLine.Rows(0)("totalpay4").ToString() <> "", dttmpLine.Rows(0)("totalpay4").ToString() & ".00", "")
            'Dim S8 As String = If(dttmpLine.Rows(0)("Typepay4").ToString() = "1", "เงินสด", If(dttmpLine.Rows(0)("Typepay4").ToString() = "2", "บัตรเครดิต", ""))
            ''payid5
            'Dim t5 As String = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("payid5").ToString(), dttmpLine.Rows(0)("payid5").ToString())
            'Dim t6 As String = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("AppointDate5").ToString(), dttmpLine.Rows(0)("AppointDate5").ToString())
            'Dim t7 As String = If(dttmpLine.Rows(0)("totalpay5").ToString() <> "", dttmpLine.Rows(0)("totalpay5").ToString() & ".00", "")
            'Dim t8 As String = If(dttmpLine.Rows(0)("Typepay5").ToString() = "1", "เงินสด", If(dttmpLine.Rows(0)("Typepay5").ToString() = "2", "บัตรเครดิต", ""))
            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("payid5").ToString(), dttmpLine.Rows(0)("payid5").ToString())
            'A1 = "5"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1310, 2650))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("AppointDate5").ToString(), dttmpLine.Rows(0)("AppointDate5").ToString())
            'A1 = "5"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1510, 2650))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("totalpay5").ToString() <> "  ", dttmpLine.Rows(0)("totalpay5").ToString() & ".00", "")
            'A1 = "5"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1840, 2650))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay5").ToString() = "1", "เงินสด", If(dttmpLine.Rows(0)("Typepay5").ToString() = "2", "บัตรเครดิต", ""))
            'A1 = "5"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(2140, 2650))

            ''payid6
            'Dim U5 As String = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("payid6").ToString(), dttmpLine.Rows(0)("payid6").ToString())
            'Dim U6 As String = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("AppointDate6").ToString(), dttmpLine.Rows(0)("AppointDate6").ToString())
            'Dim U7 As String = If(dttmpLine.Rows(0)("totalpay6").ToString() <> "", dttmpLine.Rows(0)("totalpay6").ToString() & ".00", "")
            'Dim U8 As String = If(dttmpLine.Rows(0)("Typepay6").ToString() = "1", "เงินสด", If(dttmpLine.Rows(0)("Typepay6").ToString() = "2", "บัตรเครดิต", ""))
            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("payid6").ToString(), dttmpLine.Rows(0)("payid6").ToString())

            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1310, 2730))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay1").ToString() = "1", dttmpLine.Rows(0)("AppointDate6").ToString(), dttmpLine.Rows(0)("AppointDate6").ToString())
            ' A1 = "6"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1510, 2730))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("totalpay6").ToString() <> "  ", dttmpLine.Rows(0)("totalpay6").ToString() & ".00", "")
            ' A1 = "6"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(1840, 2730))

            A1 = ""
            A1 = If(dttmpLine.Rows(0)("Typepay6").ToString() = "1", "เงินสด", If(dttmpLine.Rows(0)("Typepay6").ToString() = "2", "บัตรเครดิต", ""))
            'A1 = "6"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(2140, 2730))

            A1 = "*** บริษัทฯ ขอสงวนสิทธิ์ในการคิดค่าธรรมเนียมยกเลิกทั่วไป 350 บาท, ขายรถ 500 บาท"
            gra.DrawString(A1, New Font(FontName, 36), Brushes.Black, New PointF(680, 2810))



            '3.save To Server 
            '3.1 สร้าง Folder

            Dim CreateFolder1 As String = "D:\LINE\" + lblApp.Text
            CreateFolder(CreateFolder1)

            CreateFolder1 = CreateFolder1 + "\" + lblApp.Text + "_ใบคำขอเอาประกัน_Reprint.jpg"
            bm.Save(CreateFolder1)
            gra.Dispose()
            bm.Dispose()



            Dim queryRefNO = New System.Text.StringBuilder()
            ''ถ้าชำระเป็นเงินสด Gen Payment ยกเลิก ให้ทำทุกเคส

            'queryRefNO.Append(" select typePay from tblapppay ")
            'queryRefNO.Append(" where payid=1 and typePay=1 and AppID=" & lblApp.Text)
            'dttmpLine = DataAccess.DataRead(query.ToString)
            'If dttmpLine.Rows.Count > 0 Then
            '    LINEGenPayment()
            'End If
            LINEGenPayment()

            ' Insert To Table
            ' TblLogLineAppStore
            'Dim Command As SqlCommand
            'queryRefNO.Append("insert into TblLogLineAppStore (AppID) values (" & lblApp.Text & ") ")
            'Command = New SqlCommand(queryRefNO.ToString, Conn)
            'Command.ExecuteNonQuery()
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "ViewHistory", "GenPYDetail(" & lblApp.Text & ",1)", True)
        End If
        'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "script", "<script>window.open('GenPaymentDatail.aspx?AppID=" & lblApp.Text & "','Application');</script>")
        Conn.Close()
    End Sub

    Private Sub LINE_Address(ByVal dttmp As DataTable, ByRef B1 As String, ByRef B2 As String)
        Dim str_Adress As String = ""
        Dim str_Adress2 As String = ""

        If dttmp.Rows(0)("Address").ToString() <> "" Then
            str_Adress += "เลขที่ " & dttmp.Rows(0)("Address").ToString() & " "
        End If

        If dttmp.Rows(0)("Moo").ToString() <> "" Then
            str_Adress += "ม." & dttmp.Rows(0)("Moo").ToString() & " "
        End If

        If dttmp.Rows(0)("Villege").ToString() <> "" Then
            str_Adress += dttmp.Rows(0)("Villege").ToString() & " "
        End If

        If dttmp.Rows(0)("Building").ToString() <> "" Then
            str_Adress += dttmp.Rows(0)("Building").ToString() & " "
        End If

        If dttmp.Rows(0)("HomeFloor").ToString() <> "" Then
            str_Adress += dttmp.Rows(0)("HomeFloor").ToString() & " "
        End If

        If dttmp.Rows(0)("HomeRoom").ToString() <> "" Then
            str_Adress += dttmp.Rows(0)("HomeRoom").ToString() & " "
        End If

        'crlf = chr(13) ; 
        If dttmp.Rows(0)("Moo").ToString() <> "" Or dttmp.Rows(0)("Address").ToString() <> "" Or dttmp.Rows(0)("Villege").ToString() <> "" Or dttmp.Rows(0)("Building").ToString() <> "" Or dttmp.Rows(0)("HomeFloor").ToString() <> "" Or dttmp.Rows(0)("HomeRoom").ToString() <> "" Then
            str_Adress += Chr(13)
        End If
        If dttmp.Rows(0)("Soi").ToString() <> "" Then
            str_Adress += "ซ." & dttmp.Rows(0)("Soi").ToString() & " "
        End If
        If dttmp.Rows(0)("Road").ToString() <> "" Then
            str_Adress += "ถ." & dttmp.Rows(0)("Road").ToString() & " "
        End If
        If dttmp.Rows(0)("Soi").ToString() <> "" Or dttmp.Rows(0)("Road").ToString() <> "" Then
            str_Adress += Chr(13)
        End If
        If dttmp.Rows(0)("Province").ToString() <> "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("SubDist").ToString() <> "" Then
                str_Adress += Environment.NewLine & "ต." & dttmp.Rows(0)("SubDist").ToString() & " "
            End If
        End If
        If dttmp.Rows(0)("Province").ToString() = "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("SubDist").ToString() <> "" Then
                str_Adress += Environment.NewLine & "แขวง" & dttmp.Rows(0)("SubDist").ToString() & " "
            End If
        End If

        If dttmp.Rows(0)("Province").ToString() <> "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("Dist").ToString() <> "" Then
                str_Adress += "อ." & dttmp.Rows(0)("Dist").ToString() & " "
            End If
        End If
        If dttmp.Rows(0)("Province").ToString() = "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("Dist").ToString() <> "" Then
                str_Adress += "เขต" & dttmp.Rows(0)("Dist").ToString() & " "
            End If
        End If
        str_Adress += Chr(13)
        If dttmp.Rows(0)("Province").ToString() <> "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("Province").ToString() <> "" Then
                str_Adress += Environment.NewLine & "จ." & dttmp.Rows(0)("Province").ToString() & " "
            End If
        End If
        If dttmp.Rows(0)("Province").ToString() = "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("Province").ToString() <> "" Then
                str_Adress += Environment.NewLine & dttmp.Rows(0)("Province").ToString() & " "
            End If
        End If

        If dttmp.Rows(0)("Zip").ToString() <> "" Then
            str_Adress += dttmp.Rows(0)("Zip").ToString() & " "
        End If

        '------
        If dttmp.Rows(0)("SAddress").ToString() <> "" Then
            str_Adress2 += "เลขที่ " & dttmp.Rows(0)("SAddress").ToString() & " "
        End If

        If dttmp.Rows(0)("SMoo").ToString() <> "" Then
            str_Adress2 += "ม." & dttmp.Rows(0)("SMoo").ToString() & " "
        End If

        If dttmp.Rows(0)("SVillege").ToString() <> "" Then
            str_Adress2 += dttmp.Rows(0)("SVillege").ToString() & " "
        End If

        If dttmp.Rows(0)("SBuilding").ToString() <> "" Then
            str_Adress2 += dttmp.Rows(0)("SBuilding").ToString() & " "
        End If

        If dttmp.Rows(0)("SHomeFloor").ToString() <> "" Then
            str_Adress2 += dttmp.Rows(0)("SHomeFloor").ToString() & " "
        End If

        If dttmp.Rows(0)("SHomeRoom").ToString() <> "" Then
            str_Adress2 += dttmp.Rows(0)("SHomeRoom").ToString() & " "
        End If

        'crlf = chr(13) ; 
        If dttmp.Rows(0)("SMoo").ToString() <> "" Or dttmp.Rows(0)("SAddress").ToString() <> "" Or dttmp.Rows(0)("SVillege").ToString() <> "" Or dttmp.Rows(0)("SBuilding").ToString() <> "" Or dttmp.Rows(0)("SHomeFloor").ToString() <> "" Or dttmp.Rows(0)("SHomeRoom").ToString() <> "" Then
            str_Adress2 += Chr(13)
        End If
        If dttmp.Rows(0)("SSoi").ToString() <> "" Then
            str_Adress2 += "ซ." & dttmp.Rows(0)("SSoi").ToString() & " "
        End If
        If dttmp.Rows(0)("SRoad").ToString() <> "" Then
            str_Adress2 += "ถ." & dttmp.Rows(0)("SRoad").ToString() & " "
        End If
        If dttmp.Rows(0)("SSoi").ToString() <> "" Or dttmp.Rows(0)("SRoad").ToString() <> "" Then
            str_Adress2 += Chr(13)
        End If
        If dttmp.Rows(0)("SProvince").ToString() <> "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("SSubDist").ToString() <> "" Then
                str_Adress2 += Environment.NewLine & "ต." & dttmp.Rows(0)("SSubDist").ToString() & " "
            End If
        End If
        If dttmp.Rows(0)("SProvince").ToString() = "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("SSubDist").ToString() <> "" Then
                str_Adress2 += Environment.NewLine & "แขวง" & dttmp.Rows(0)("SSubDist").ToString() & " "
            End If
        End If

        If dttmp.Rows(0)("SProvince").ToString() <> "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("SDist").ToString() <> "" Then
                str_Adress2 += "อ." & dttmp.Rows(0)("SDist").ToString() & " "
            End If
        End If
        If dttmp.Rows(0)("SProvince").ToString() = "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("SDist").ToString() <> "" Then
                str_Adress2 += "เขต" & dttmp.Rows(0)("SDist").ToString() & " "
            End If
        End If
        str_Adress2 += Chr(13)
        If dttmp.Rows(0)("SProvince").ToString() <> "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("SProvince").ToString() <> "" Then
                str_Adress2 += Environment.NewLine & "จ." & dttmp.Rows(0)("SProvince").ToString() & " "
            End If
        End If
        If dttmp.Rows(0)("SProvince").ToString() = "กรุงเทพมหานคร" Then
            If dttmp.Rows(0)("SProvince").ToString() <> "" Then
                str_Adress2 += Environment.NewLine & dttmp.Rows(0)("SProvince").ToString() & " "
            End If
        End If

        If dttmp.Rows(0)("SZip").ToString() <> "" Then
            str_Adress2 += dttmp.Rows(0)("SZip").ToString() & " "
        End If

        B1 = str_Adress
        B2 = str_Adress2
    End Sub
    Private Sub LINE_Phone(ByVal productid As String, ByRef C2 As String)

        If productid = "14" Then
            C2 = "0-2285-8000"
        ElseIf productid = "8" Then
            C2 = "0-2670-4444"
        ElseIf productid = "20" Then
            C2 = "0-2640-4500"
        ElseIf productid = "84" Then
            C2 = "0-2695-0700"
        ElseIf productid = "83" Then
            C2 = "1748"
        ElseIf productid = "10" Then
            C2 = "02-022-1100"
        ElseIf productid = "18" Then
            C2 = "0-2792-5500"
        ElseIf productid = "9" Then
            C2 = "1557"
        ElseIf productid = "13" Then
            C2 = "1790"
        ElseIf productid = "15" Then
            C2 = "0-2869-3333"
        ElseIf productid = "63" Then
            C2 = "02-624-1111"
        ElseIf productid = "24" Then
            C2 = "02-257-8080"
        ElseIf productid = "71" Then
            C2 = "02-209-3299"
        ElseIf productid = "52" Then
            C2 = "1726"
        Else
            C2 = ""
        End If
    End Sub

    Public Sub CreateFolder(ByVal DirInfo_1 As String)

        Dim DirInfo = New DirectoryInfo(DirInfo_1)
        If Not DirInfo.Exists Then
            DirInfo.Create()
        End If
    End Sub
    Private Sub LINEGenPayment()

        Dim dttmpRefNO As New DataTable
        Dim queryRefNO = New System.Text.StringBuilder()

        queryRefNO.Append(" Select  tblcar.refno As 'refno1' ")
        queryRefNO.Append(", tblapplication.AppID as 'refno2'")
        queryRefNO.Append(" , isnull(Tbl_ProductType.ProTypeName,'') as 'Desc'")
        queryRefNO.Append(" , Tbl_Type.TypeName as 'Desc1'")
        queryRefNO.Append(" , TblCustomerInit.InitTH+' '+ isnull(tblcustomer.FNameTH,'')+' '+isnull(tblcustomer.LNameTH,'') as 'customername'")
        queryRefNO.Append(" , tblcar.Carid")
        queryRefNO.Append(", (select count(*) from tblapppay where appid=tblapplication.AppID) As CountR  ")

        queryRefNO.Append(" , cast(p1.TotalPay As Decimal(10,2)) As 'Pay1'")
        queryRefNO.Append(" , SUBSTRING(convert(varchar,p1.AppointDate,103), 1, 2) + '/'")
        queryRefNO.Append(" + SUBSTRING(convert(varchar,p1.AppointDate,103), 4, 2)")
        queryRefNO.Append("   + '/'+convert(varchar, (SUBSTRING(convert(varchar,p1.AppointDate,103), 7, 4)+543)) as 'AppointDate1' ")


        queryRefNO.Append(" , cast(p2.TotalPay As Decimal(10,2)) As 'Pay2'")
        queryRefNO.Append(" , SUBSTRING(convert(varchar,p2.AppointDate,103), 1, 2) + '/'")
        queryRefNO.Append(" + SUBSTRING(convert(varchar,p2.AppointDate,103), 4, 2)")
        queryRefNO.Append("   + '/'+convert(varchar, (SUBSTRING(convert(varchar,p2.AppointDate,103), 7, 4)+543)) as 'AppointDate2' ")

        queryRefNO.Append(" , cast(p3.TotalPay As Decimal(10,2)) As 'Pay3'")
        queryRefNO.Append(" , SUBSTRING(convert(varchar,p3.AppointDate,103), 1, 2) + '/'")
        queryRefNO.Append(" + SUBSTRING(convert(varchar,p3.AppointDate,103), 4, 2)")
        queryRefNO.Append("   + '/'+convert(varchar, (SUBSTRING(convert(varchar,p3.AppointDate,103), 7, 4)+543)) as 'AppointDate3' ")

        queryRefNO.Append(" , cast(p4.TotalPay As Decimal(10,2)) As 'Pay4'")
        queryRefNO.Append(" , SUBSTRING(convert(varchar,p4.AppointDate,103), 1, 2) + '/'")
        queryRefNO.Append(" + SUBSTRING(convert(varchar,p4.AppointDate,103), 4, 2)")
        queryRefNO.Append("   + '/'+convert(varchar, (SUBSTRING(convert(varchar,p4.AppointDate,103), 7, 4)+543)) as 'AppointDate4' ")
        queryRefNO.Append(" , cast(p5.TotalPay As Decimal(10,2)) As 'Pay5'")
        queryRefNO.Append(" , SUBSTRING(convert(varchar,p5.AppointDate,103), 1, 2) + '/'")
        queryRefNO.Append(" + SUBSTRING(convert(varchar,p5.AppointDate,103), 4, 2)")
        queryRefNO.Append("   + '/'+convert(varchar, (SUBSTRING(convert(varchar,p5.AppointDate,103), 7, 4)+543)) as 'AppointDate5' ")

        queryRefNO.Append(" , cast(p6.TotalPay As Decimal(10,2)) As 'Pay6'")
        queryRefNO.Append(" , SUBSTRING(convert(varchar,p6.AppointDate,103), 1, 2) + '/'")
        queryRefNO.Append(" + SUBSTRING(convert(varchar,p6.AppointDate,103), 4, 2)")
        queryRefNO.Append("   + '/'+convert(varchar, (SUBSTRING(convert(varchar,p6.AppointDate,103), 7, 4)+543)) as 'AppointDate6' ")

        queryRefNO.Append(" ,case when tblapplication.isprovalue=1 then ")
        queryRefNO.Append(" 	  SUBSTRING(Convert(varchar, tblapplication.protectdate, 103), 1, 2) + '/' ")
        queryRefNO.Append("       + SUBSTRING(convert(varchar,tblapplication.protectdate,103), 4, 2)")
        queryRefNO.Append("       + '/'+convert(varchar, (SUBSTRING(convert(varchar,tblapplication.protectdate,103), 7, 4)+543))")
        queryRefNO.Append("      when tblapplication.IsCarpet=1 then ")
        queryRefNO.Append(" 	 SUBSTRING(Convert(varchar, tblapplication.ProtectDateCarpet, 103), 1, 2) + '/'")
        queryRefNO.Append("      + SUBSTRING(convert(varchar,tblapplication.ProtectDateCarpet,103), 4, 2)")
        queryRefNO.Append("      + '/'+convert(varchar, (SUBSTRING(convert(varchar,tblapplication.ProtectDateCarpet,103), 7, 4)+543))")
        queryRefNO.Append(" 	Else")
        queryRefNO.Append("            '' end as 'protectdate'")

        queryRefNO.Append(",case when tblapplication.isprovalue=1 then tblapplication.ProValue else '0.00' end as 'provalue'")
        queryRefNO.Append(",case when tblapplication.IsCarpet=1 then tblapplication.CarPet else '0.00' end as 'CarPet',isnull(p1.PayID,0) as 'p1',   isnull(p2.PayID,0) as 'p2', isnull(p3.PayID,0) as 'p3', isnull(p4.PayID,0) as 'p4', isnull(p5.PayID,0) as 'p5', isnull(p6.PayID,0) as 'p6'")
        queryRefNO.Append("         From tblapplication  ")
        queryRefNO.Append(" inner Join Tbl_ProductType on tblapplication.ProDuctID=Tbl_ProductType.ProTypeID ")
        queryRefNO.Append(" inner Join Tbl_Type on Tbl_Type.Typeid=tblapplication.Typeprovalue")
        queryRefNO.Append(" inner Join tblcar on tblapplication.idcar=tblcar.idcar ")
        queryRefNO.Append(" inner Join tblcustomer on tblapplication.cusid=tblcustomer.cusid ")
        queryRefNO.Append(" inner Join TblCustomerInit on tblcustomer.InitID=TblCustomerInit.InitID ")
        queryRefNO.Append(" inner Join TblApppay p1 on tblapplication.appid=p1.AppID And p1.PayID=1 ")
        queryRefNO.Append(" Left  Join TblApppay p2 on tblapplication.appid=p2.AppID And p2.PayID=2 ")
        queryRefNO.Append(" Left  Join TblApppay p3 on tblapplication.appid=p3.AppID And p3.PayID=3 ")
        queryRefNO.Append(" Left  Join TblApppay p4 on tblapplication.appid=p4.AppID And p4.PayID=4 ")
        queryRefNO.Append(" Left  Join TblApppay p5 on tblapplication.appid=p5.AppID And p5.PayID=5 ")
        queryRefNO.Append(" Left  Join TblApppay p6 on tblapplication.appid=p6.AppID And p6.PayID=6 ")
        queryRefNO.Append(" where  tblapplication.AppID=" & lblApp.Text)


        dttmpRefNO = DataAccess.DataRead(queryRefNO.ToString)
        If dttmpRefNO.Rows.Count = 1 Then

            Dim refno As String = dttmpRefNO.Rows(0)("refno1").ToString()
            Dim refno2 As String = dttmpRefNO.Rows(0)("refno2").ToString()
            Dim CountR As String = dttmpRefNO.Rows(0)("CountR").ToString()
            Dim ProTypeName As String = dttmpRefNO.Rows(0)("Desc").ToString()
            Dim Desc1 As String = dttmpRefNO.Rows(0)("Desc1").ToString()

            Dim customername As String = dttmpRefNO.Rows(0)("customername").ToString()
            Dim Carid As String = dttmpRefNO.Rows(0)("Carid").ToString()
            Dim protectdate As String = dttmpRefNO.Rows(0)("protectdate").ToString()
            Dim provalue As Decimal = dttmpRefNO.Rows(0)("provalue")
            Dim CarPet As Decimal = dttmpRefNO.Rows(0)("CarPet")
            Dim SumTotal As Decimal = provalue + CarPet

            Dim Pay1 As Decimal = dttmpRefNO.Rows(0)("Pay1")
            Dim AppointDate1 As String = dttmpRefNO.Rows(0)("AppointDate1").ToString()

            Dim strMoney As String = "0"
            Dim P_Server As String = ""
            If CountR = "1" Then
                P_Server = "~/images/LINE/pay01.jpg"
            ElseIf dttmpRefNO.Rows(0)("CountR") < 4 Then
                P_Server = "~/images/LINE/pay03.jpg"
            Else
                P_Server = "~/images/LINE/pay04.jpg"
            End If
            strMoney = Replace(strMoney, ".", "")
            strMoney = Replace(strMoney, ",", "")

            Dim barcodestr As String = "|010755800027000" & Chr(13) & refno & Chr(13) & lblApp.Text & Chr(13) & strMoney
            Dim myimg As Image = Code128Rendering.MakeBarcodeImage(barcodestr, 1, True)
            Dim barcodesre1 As String = "|010755800027000" & refno & lblApp.Text & strMoney

            Dim folder As String = Server.MapPath(P_Server)

            Dim bm As New Bitmap(folder)
            Dim FontName As String = "Prompt Medium"
            Dim gra As Graphics = Graphics.FromImage(bm)

            gra.DrawString(customername, New Font(FontName, 22), Brushes.Navy, New PointF(75, 204))
            gra.DrawString(Carid, New Font(FontName, 22), Brushes.Navy, New PointF(210, 255))
            gra.DrawString(protectdate, New Font(FontName, 22), Brushes.Navy, New PointF(190, 305))
            gra.DrawString(ProTypeName, New Font(FontName, 18), Brushes.Black, New PointF(55, 400))

            If provalue = 0 Then
                gra.DrawString(provalue.ToString("N2"), New Font(FontName, 18), Brushes.Black, New PointF(560, 440))
            ElseIf provalue > 9999 Then
                gra.DrawString(provalue.ToString("N2"), New Font(FontName, 18), Brushes.Black, New PointF(500, 440))
            Else
                gra.DrawString(provalue.ToString("N2"), New Font(FontName, 18), Brushes.Black, New PointF(520, 440))
            End If

            gra.DrawString(" บาท", New Font(FontName, 18), Brushes.Black, New PointF(620, 440))
            gra.DrawString("ประกันภาคสมัครใจ " + Desc1, New Font(FontName, 18), Brushes.Black, New PointF(55, 440))
            gra.DrawString("ประกันภาคบังคับ (พ.ร.บ)", New Font(FontName, 18), Brushes.Black, New PointF(55, 480))

            If CarPet = 0 Then
                gra.DrawString(CarPet.ToString("N2"), New Font(FontName, 18), Brushes.Black, New PointF(560, 480))
            ElseIf CarPet > 999 Then
                gra.DrawString(CarPet.ToString("N2"), New Font(FontName, 18), Brushes.Black, New PointF(500, 480))
            Else
                gra.DrawString(CarPet.ToString("N2"), New Font(FontName, 18), Brushes.Black, New PointF(535, 480))
            End If

            gra.DrawString(" บาท", New Font(FontName, 18), Brushes.Black, New PointF(620, 480))
            gra.DrawString("ยอดเงินรวมที่ต้องชำระ", New Font(FontName, 18), Brushes.Black, New PointF(55, 530))
            gra.DrawString("(ยอดเงินรวมที่ต้องชำระ)", New Font(FontName, 14), Brushes.Black, New PointF(55, 560))

            If CountR = "1" Then
                If SumTotal > 9999 Then
                    gra.DrawString(SumTotal.ToString("N2"), New Font(FontName, 24), Brushes.Black, New PointF(460, 520))
                Else
                    gra.DrawString(SumTotal.ToString("N2"), New Font(FontName, 24), Brushes.Black, New PointF(480, 520))
                End If
            Else
                If SumTotal > 9999 Then
                    gra.DrawString(SumTotal.ToString("N2"), New Font(FontName, 24), Brushes.Black, New PointF(460, 520))
                Else
                    gra.DrawString(SumTotal.ToString("N2"), New Font(FontName, 24), Brushes.Black, New PointF(490, 520))
                End If
            End If


            gra.DrawString(" บาท", New Font(FontName, 18), Brushes.Black, New PointF(620, 530))

            Dim qe As MessagingToolkit.QRCode.Codec.QRCodeEncoder = New MessagingToolkit.QRCode.Codec.QRCodeEncoder
            Dim L1_T1 As String = "|010755800027000" & Chr(13) & refno & Chr(13) & refno2 & Chr(13) & strMoney
            Dim ALL As String = L1_T1
            Dim myimg1 As Image = qe.Encode(ALL)

            If CountR = "1" Then

                gra.DrawString(refno, New Font(FontName, 18), Brushes.Black, New PointF(200, 990))
                gra.DrawString(refno2, New Font(FontName, 18), Brushes.Black, New PointF(550, 990))

                gra.DrawImage(myimg1, 450, 650, 150, 150)
                gra.DrawImage(myimg, 120, 880, myimg.Width, myimg.Height)

            Else
                gra.DrawString("ผ่อนชำระ", New Font(FontName, 18), Brushes.Navy, New PointF(55, 590))
                gra.DrawString(CountR, New Font(FontName, 18), Brushes.Navy, New PointF(600, 590))
                gra.DrawString(" งวด", New Font(FontName, 18), Brushes.Navy, New PointF(620, 590))

                gra.DrawString("งวดแรกที่ต้องชำระ", New Font(FontName, 18), Brushes.Navy, New PointF(55, 630))



                If Pay1 > 9999 Then
                    gra.DrawString(Pay1.ToString("N2"), New Font(FontName, 18), Brushes.Navy, New PointF(500, 630))
                Else
                    gra.DrawString(Pay1.ToString("N2"), New Font(FontName, 18), Brushes.Navy, New PointF(520, 630))
                End If
                gra.DrawString(" บาท", New Font(FontName, 18), Brushes.Navy, New PointF(620, 630))

                gra.DrawString("ผ่อน 0 % " & CountR & " งวด", New Font(FontName, 20, FontStyle.Bold), Brushes.Black, New PointF(280, 700))


                If dttmpRefNO.Rows(0)("p1").ToString() <> "0" Then
                    gra.DrawString("งวดที่ 1", New Font(FontName, 18), Brushes.Black, New PointF(140, 750))
                    gra.DrawString(AppointDate1, New Font(FontName, 18), Brushes.Black, New PointF(280, 750))
                    gra.DrawString(Pay1.ToString("N2") + " บาท", New Font(FontName, 18), Brushes.Black, New PointF(450, 750))
                End If
                If dttmpRefNO.Rows(0)("p2").ToString() <> "0" Then
                    Dim Pay2 As Decimal = dttmpRefNO.Rows(0)("Pay2")
                    Dim AppointDate2 As String = dttmpRefNO.Rows(0)("AppointDate2").ToString()

                    gra.DrawString("งวดที่ 2", New Font(FontName, 18), Brushes.Black, New PointF(140, 790))
                    gra.DrawString(AppointDate2, New Font(FontName, 18), Brushes.Black, New PointF(280, 790))
                    gra.DrawString(Pay2.ToString("N2") + " บาท", New Font(FontName, 18), Brushes.Black, New PointF(450, 790))
                End If
                If dttmpRefNO.Rows(0)("p3").ToString() <> "0" Then
                    Dim Pay3 As Decimal = dttmpRefNO.Rows(0)("Pay3")
                    Dim AppointDate3 As String = dttmpRefNO.Rows(0)("AppointDate3").ToString()

                    gra.DrawString("งวดที่ 3", New Font(FontName, 18), Brushes.Black, New PointF(140, 830))
                    gra.DrawString(AppointDate3, New Font(FontName, 18), Brushes.Black, New PointF(280, 830))
                    gra.DrawString(Pay3.ToString("N2") + " บาท", New Font(FontName, 18), Brushes.Black, New PointF(450, 830))
                End If



                If dttmpRefNO.Rows(0)("p4").ToString() <> "0" Then
                    Dim Pay4 As Decimal = dttmpRefNO.Rows(0)("Pay4")
                    Dim AppointDate4 As String = dttmpRefNO.Rows(0)("AppointDate4").ToString()

                    gra.DrawString("งวดที่ 4", New Font(FontName, 18), Brushes.Black, New PointF(140, 870))
                    gra.DrawString(AppointDate4, New Font(FontName, 18), Brushes.Black, New PointF(280, 870))
                    gra.DrawString(Pay4.ToString("N2") + " บาท", New Font(FontName, 18), Brushes.Black, New PointF(450, 870))
                End If
                If dttmpRefNO.Rows(0)("p5").ToString() <> "0" Then
                    Dim Pay5 As Decimal = dttmpRefNO.Rows(0)("Pay5")
                    Dim AppointDate5 As String = dttmpRefNO.Rows(0)("AppointDate5").ToString()

                    gra.DrawString("งวดที่ 5", New Font(FontName, 18), Brushes.Black, New PointF(140, 910))
                    gra.DrawString(AppointDate5, New Font(FontName, 18), Brushes.Black, New PointF(280, 910))
                    gra.DrawString(Pay5.ToString("N2") + " บาท", New Font(FontName, 18), Brushes.Black, New PointF(450, 910))
                End If
                If dttmpRefNO.Rows(0)("p6").ToString() <> "0" Then
                    Dim Pay6 As Decimal = dttmpRefNO.Rows(0)("Pay6")
                    Dim AppointDate6 As String = dttmpRefNO.Rows(0)("AppointDate6").ToString()
                    gra.DrawString("งวดที่ 6", New Font(FontName, 18), Brushes.Black, New PointF(140, 950))
                    gra.DrawString(AppointDate6, New Font(FontName, 18), Brushes.Black, New PointF(280, 950))
                    gra.DrawString(Pay6.ToString("N2") + " บาท", New Font(FontName, 18), Brushes.Black, New PointF(450, 950))
                End If

                If dttmpRefNO.Rows(0)("CountR") < 4 Then
                    gra.DrawString(refno, New Font(FontName, 18), Brushes.Black, New PointF(200, 1300))
                    gra.DrawString(refno2, New Font(FontName, 18), Brushes.Black, New PointF(550, 1300))

                    gra.DrawImage(myimg1, 450, 950, 150, 150)
                    gra.DrawImage(myimg, 120, 1180, myimg.Width, myimg.Height)
                Else
                    gra.DrawString(refno, New Font(FontName, 18), Brushes.Black, New PointF(200, 1440))
                    gra.DrawString(refno2, New Font(FontName, 18), Brushes.Black, New PointF(550, 1440))

                    gra.DrawImage(myimg1, 450, 1100, 150, 150)
                    gra.DrawImage(myimg, 120, 1310, myimg.Width, myimg.Height)
                End If


            End If




            Dim destPath As String = "D:\\LINE\\" + lblApp.Text + "\" + lblApp.Text + "_Payment_Reprint.jpg"

                bm.Save(destPath)
                gra.Dispose()
                bm.Dispose()

                'Ref.IT Service XXXXX GEN Give PBIG Use in Payment Form
                Dim destPathQR As String = "D:\\LINE\\" + lblApp.Text + "\qr.bmp"
                Dim bm1 As New Bitmap(myimg1)
                bm1.Save(destPathQR)
                bm1.Dispose()

            End If

        'Conn.Close()
    End Sub

    Protected Sub gvDataFind_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvDataFind.RowCommand
        lblApp.Text = gvDataFind.DataKeys(e.CommandArgument).Item(0)
        If lblApp.Text <> "" Then
            SetappAcc()
            Auto_File()
        End If
    End Sub

End Class
