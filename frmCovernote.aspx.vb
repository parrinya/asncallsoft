Imports System.Data.SqlClient
Imports System.Data

Partial Class Modules_Manager_Report_frmConvernote
    Inherits System.Web.UI.Page
    Dim Conn As New SqlConnection(ConfigurationManager.ConnectionStrings("asnbroker").ConnectionString)
    Dim dt As DataTable
    Dim ISODate As New ISODate
    Dim DataAccess As New DataAccess
    Dim dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9, dt20 As DataTable
    Dim FunAll As FuntionAll = New FuntionAll()
    Protected Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        If name.Checked And txtsearch.Text <> "" Then 'ค้นหาตามชื่อ/ทะเบียน           
            SqlNameCarID.SelectParameters.Item("txt").DefaultValue = "%" + txtsearch.Text + "%"
            GVShow.DataSource = SqlNameCarID
            GVShow.DataBind()
        ElseIf successdate.Checked And txtdate1.Text <> "" And txtdate2.Text <> "" Then 'ตามวันที่successdate
            SqlSuccessDate.SelectParameters.Item("date1").DefaultValue = ISODate.SetISODate("en", txtdate1.Text.Trim)
            SqlSuccessDate.SelectParameters.Item("date2").DefaultValue = ISODate.SetISODate("en", txtdate2.Text.Trim)
            GVShow.DataSource = SqlSuccessDate
            GVShow.DataBind()
        Else
            MsgBox("กรุณากรอกข้อมูลในการค้นหา")
            GVShow.DataBind()
        End If
    End Sub
    Protected Sub GVShow_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GVShow.RowCommand
        'add by na 20150316 
        Dim tmpdate1 As String
        'กด พิมพ์
        If e.CommandName = "print" Then
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
            dt.Columns.Add("DetDeviceAdd")
            dt.Columns.Add("Old_Insu")
            dt.Columns.Add("Old_PolicyNo")
            dt.Columns.Add("ASNComment")
            dt.Columns.Add("IDCARD")
            dt.Columns.Add("Status")


            Conn.Open()

            If (Format$(Date.Now, "yyyy") > 2300) Then
                tmpdate1 = Format$(Date.Now, "yyyy") - 543 & Format$(Date.Now, "MMdd")
            Else
                tmpdate1 = Format$(Date.Now, "yyyymmdd")
            End If

            str = "delete from  tmp_Covernote_app01 where UserID = '" & Request.Cookies("UserID").Value & "'"
            Command = New SqlCommand(str, Conn)
            Command.ExecuteNonQuery()
            str = "delete from tmp_Covernote_PayCredit where UserID = '" & Request.Cookies("UserID").Value & "'"
            Command = New SqlCommand(str, Conn)
            Command.ExecuteNonQuery()
            str = "delete from tmp_Covernote_app02 where UserID = '" & Request.Cookies("UserID").Value & "'"
            Command = New SqlCommand(str, Conn)
            Command.ExecuteNonQuery()

            str = "select a.* from (select a.*,b.initth from (select  a.*,ProTypeBrand, Addr + ' ' + b.Road AS a1, b.SubDist + ' ' + b.Dist + ' ' + b.Province + ' ' + b.Zip AS a2, '  ' AS a3 from (select a.*,b.fname,b.lname from (select a.*,b.cartypename  from  (SELECT a.initid,a.FNameTH, a.LNameTH, a.Address, a.SAddress, a.Villege, a.Svillege, " + _
                                  "a.Building, a.SBuilding, a.HomeFloor, a.SHomeFloor, a.HomeRoom, " + _
                                  "a.SHomeRoom, a.Moo, a.SMoo, a.Soi, a.SSoi, a.Road, a.SRoad, " + _
                                  "a.SubDist, a.SSubDist, a.Dist, a.SDist, a.Province, a.SProvince, a.Zip, " + _
                                  "a.SZip, b.AssignTo, b.CarDriverNo, b.CarDriver1 + ' ' + b.CarDriverLname1 as  CarDriver1, b.CarDriverBorn1, b.CarDriver2 + ' ' + b.CarDriverLname2 as  CarDriver2, b.CarDriverBorn2, " + _
                                 "b.DBornNO1, b.DBornDate1, b.DBornAddr1, b.DBornNO2, b.DBornDate2, b.DBornAddr2, b.CarID, " + _
                                 "b.CarBuyDate, b.CarFixIn, b.CarSize, b.CarNo, b.CarBoxNo, b.CarType, b.CarYear, b.CarBrand, " + _
                                  "b.CarSeries,c.AppID, c.AppNO, c.ProDuctID, c.ProDuctIDCarpet, c.AppStatus, " + _
                                  "c.ProtectDate, c.ProPrice, c.IsProvalue, c.ProValue,c.discounttype,  " + _
                                  "c.Typeprovalue, c.IsCarpet, c.CarPet, c.FirstPay, c.YearPay, c.Lost_Life1, " + _
                                  "c.Lost_Life2, c.Lost_Prop1, c.Lost_Prop2, c.Lost_Car1, c.Lost_Car2, " + _
                                  "c.Car_Fire, c.Acc_Lost1, c.Acc_Lost2, c.Acc_Lost3, c.Acc_Lost4, " + _
                                  "c.Maintain, c.Insure, c.Apprela, c.Pledge, c.PolicyNO, " + _
                                  "c.PolicyDate, c.SendPolicyDate, a.sname, c.expprotectdate,  " + _
                                  "c.CarPetNO , c.CarPetDate,c.successdate as createdate,c.appcomment as ASNcomt,'" & reportX & "' as typereport " & ex1 & ", c.Comments,c.Old_Insu,c.Old_PolicyNo,c.appcomment,a.IDCard,z.StatusCode " + _
                                  "FROM TblCustomer a INNER JOIN " + _
                                  "TblCar b ON a.CusID = b.CusID INNER JOIN " + _
                                  " TblApplication c ON b.IdCar = c.Idcar  " + _
                                  " LEFT JOIN TblStatus z ON b.curstatus = z.StatusID where  appid = '" & GVShow.DataKeys(e.CommandArgument).Item(0) & "'" + _
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
                    If IsDBNull(DataReader("Comments")) = False Then
                        dtr("DetDeviceAdd") = DataReader("Comments")
                    End If
                    If IsDBNull(DataReader("Old_Insu")) = False Then
                        dtr("Old_Insu") = DataReader("Old_Insu")
                    End If
                    If IsDBNull(DataReader("Old_PolicyNo")) = False Then
                        dtr("Old_PolicyNo") = DataReader("Old_PolicyNo")
                    End If
                    If IsDBNull(DataReader("appcomment")) = False Then
                        dtr("ASNComment") = DataReader("appcomment")
                    End If
                    If IsDBNull(DataReader("IDCard")) = False Then
                        dtr("IDCard") = DataReader("IDCard")
                    End If
                    If IsDBNull(DataReader("StatusCode")) = False Then
                        dtr("Status") = DataReader("StatusCode")
                    End If

                    dt.Rows.Add(dtr)
                End While
            End If
            DataReader.Close()

            Dim sName As String = dt.Rows(0).Item("sname")
            'sName = Trim(Mid(sName, InStr(1, sName, "คุณ") + 3, 255))
            ' sName = Trim(Replace(sName, "คุณ", "", 1, 255))

            str = "insert into tmp_Covernote_app01 ( " + _
            " initid, FNameTH, LNameTH, Address, SAddress, Villege, Svillege, Building, SBuilding, HomeFloor, SHomeFloor, HomeRoom, SHomeRoom, Moo, " + _
            " SMoo, Soi, SSoi, Road, SRoad, SubDist, SSubDist, Dist, SDist, Province, SProvince, Zip, SZip, AssignTo, CarDriverNo, CarDriver1,CarDriverBorn1, " + _
            " CarDriver2, CarDriverBorn2, DBornNO1, DBornDate1, DBornAddr1, DBornNO2, DBornDate2, DBornAddr2, CarID, CarBuyDate, CarFixIn, CarSize, " + _
            " CarNo, CarBoxNo, CarType, CarYear, CarBrand, CarSeries, AppID, AppNO, ProDuctID, ProDuctIDCarpet, AppStatus, ProtectDate, ProPrice, IsProvalue , " + _
            " ProValue, discounttype, Typeprovalue, IsCarpet, CarPet, FirstPay, YearPay, Lost_Life1, Lost_Life2, Lost_Prop1, Lost_Prop2, Lost_Car1, Lost_Car2 ," + _
            " Car_Fire, Acc_Lost1, Acc_Lost2, Acc_Lost3, Acc_Lost4, Maintain, Insure, Apprela, Pledge,ASNcomt, typereport,  cartypename, fname, lname, ProTypeBrand,  a1 , a2, a3, initth,expprotectdate,sname,UserID,DetDeviceAdd,Old_Insu,Old_PolicyNo,ASNComment,IDCard,CurStatus ) " + _
            " values ( " + _
              " '" & dt.Rows(0).Item("0") & "','" & dt.Rows(0).Item("1") & "','" & dt.Rows(0).Item("2") & "','" & dt.Rows(0).Item("3") & "','" & dt.Rows(0).Item("4") & "','" & dt.Rows(0).Item("5") & "','" & dt.Rows(0).Item("6") & "','" & dt.Rows(0).Item("7") & "','" & dt.Rows(0).Item("8") & "','" & dt.Rows(0).Item("9") & "' ," + _
            " '" & dt.Rows(0).Item("10") & "','" & dt.Rows(0).Item("11") & "','" & dt.Rows(0).Item("12") & "','" & dt.Rows(0).Item("13") & "','" & dt.Rows(0).Item("14") & "','" & dt.Rows(0).Item("15") & "','" & dt.Rows(0).Item("16") & "','" & dt.Rows(0).Item("17") & "','" & dt.Rows(0).Item("18") & "','" & dt.Rows(0).Item("19") & "', " + _
            " '" & dt.Rows(0).Item("20") & "','" & dt.Rows(0).Item("21") & "','" & dt.Rows(0).Item("22") & "','" & dt.Rows(0).Item("23") & "','" & dt.Rows(0).Item("24") & "','" & dt.Rows(0).Item("25") & "','" & dt.Rows(0).Item("26") & "'," & dt.Rows(0).Item("27") & ",'" & dt.Rows(0).Item("28") & "','" & dt.Rows(0).Item("29") & "'," + _
            " '" & dt.Rows(0).Item("30") & "','" & dt.Rows(0).Item("31") & "','" & (dt.Rows(0).Item("32")) & "','" & dt.Rows(0).Item("33") & "','" & (dt.Rows(0).Item("34")) & "','" & dt.Rows(0).Item("35") & "','" & dt.Rows(0).Item("36") & "','" & (dt.Rows(0).Item("37")) & "','" & dt.Rows(0).Item("38") & "','" & dt.Rows(0).Item("39") & "' ," + _
            " '" & (dt.Rows(0).Item("40")) & "','" & dt.Rows(0).Item("41") & "','" & dt.Rows(0).Item("42") & "','" & dt.Rows(0).Item("43") & "','" & dt.Rows(0).Item("44") & "','" & dt.Rows(0).Item("45") & "','" & dt.Rows(0).Item("46") & "','" & dt.Rows(0).Item("47") & "','" & dt.Rows(0).Item("48") & "','" & dt.Rows(0).Item("49") & "' ," + _
            " '" & dt.Rows(0).Item("50") & "','" & dt.Rows(0).Item("51") & "','" & dt.Rows(0).Item("52") & "','" & dt.Rows(0).Item("53") & "','" & (dt.Rows(0).Item("54")) & "','" & dt.Rows(0).Item("55") & "','" & dt.Rows(0).Item("56") & "','" & dt.Rows(0).Item("57") & "','" & dt.Rows(0).Item("58") & "','" & dt.Rows(0).Item("59") & "' ," + _
            " '" & dt.Rows(0).Item("60") & "','" & dt.Rows(0).Item("61") & "',0,'" & dt.Rows(0).Item("63") & "','" & dt.Rows(0).Item("64") & "','" & dt.Rows(0).Item("65") & "','" & dt.Rows(0).Item("66") & "','" & dt.Rows(0).Item("67") & "','" & dt.Rows(0).Item("68") & "','" & dt.Rows(0).Item("69") & "' ," + _
            " '" & dt.Rows(0).Item("70") & "','" & dt.Rows(0).Item("71") & "','" & dt.Rows(0).Item("72") & "','" & dt.Rows(0).Item("73") & "','" & dt.Rows(0).Item("74") & "','" & dt.Rows(0).Item("75") & "','" & dt.Rows(0).Item("76") & "','" & dt.Rows(0).Item("77") & "','" & dt.Rows(0).Item("78") & "','" & dt.Rows(0).Item("87") & "' ," + _
            " '" & dt.Rows(0).Item("88") & "','" & Replace(dt.Rows(0).Item("89"), "'", "") & "','" & dt.Rows(0).Item("90") & "','" & dt.Rows(0).Item("91") & "','" & dt.Rows(0).Item("92") & "','" & dt.Rows(0).Item("93") & "','" & dt.Rows(0).Item("94") & "','" & dt.Rows(0).Item("95") & "','" & dt.Rows(0).Item("96") & "','" & dt.Rows(0).Item("expprotectdate") & "','" & sName & "','" & Request.Cookies("UserID").Value & "','" & Replace(dt.Rows(0).Item("DetDeviceAdd"), "'", "") & "','" & dt.Rows(0).Item("Old_Insu") & "','" & dt.Rows(0).Item("Old_PolicyNo") & "','" & Replace(dt.Rows(0).Item("ASNComment"), "'", "") & "','" & dt.Rows(0).Item("IDCard") & "','" & dt.Rows(0).Item("Status") & "') "

            Command = New SqlCommand(str, Conn)
            Command.ExecuteNonQuery()

            '''''''''''''''''''
            'Call SetCodePrint()


            str = "select  m.*,a.*,b.*,c.*,d.*, e.*,f.*  from  (select a.*,b.initth   from (select  a.*,ProTypeBrand, Addr + ' ' + b.Road AS a1, b.SubDist + ' ' + b.Dist + ' ' + b.Province + ' ' + b.Zip AS a2, 'â·Ã : ' + b.Tel + '  ' AS a3 from (select a.*,b.fname,b.lname from (select a.*,b.cartypename  from  (SELECT      " + _
                            "c.AppID,b.CarType,b.assignto,c.ProDuctID,a.initid," + _
                            "c.CarPetNO , c.CarPetDate,c.successdate as createdate,c.appcomment as ASNcomt,'" & reportX & "' as typereport " & ex1 + _
                  "FROM TblCustomer a INNER JOIN " + _
                            "TblCar b ON a.CusID = b.CusID INNER JOIN " + _
                          " TblApplication c ON b.IdCar = c.Idcar  where appid = '" & GVShow.DataKeys(e.CommandArgument).Item(0) & "'" + _
      " ) a inner join  Tbl_Cartype b  on a.cartype =  b.cartypeid) a inner join tbluser b on a.assignto = b.userid ) a inner join Tbl_ProductType b on a.productid = b.protypeid )  a  inner join TblCustomerInit b  on a.initid = b.initid ) m left join " + _
       " (select appid appid1 ,Typepay Typepay1,convert(int,payid) payid1,AppointDate AppointDate1,totalpay totalpay1 from tblapppay where payid = 1) a on m.appid = a.appid1 left join " + _
          " (select appid appid2 ,Typepay Typepay2,convert(int,payid) payid2,AppointDate AppointDate2,totalpay totalpay2 from tblapppay where payid = 2) b on m.appid = b.appid2 left join " + _
          " (select appid appid3 ,Typepay Typepay3,convert(int,payid) payid3,AppointDate AppointDate3,totalpay totalpay3 from tblapppay where payid = 3) c on m.appid = c.appid3 left join " + _
          " (select appid appid4 ,Typepay Typepay4,convert(int,payid) payid4,AppointDate AppointDate4,totalpay totalpay4 from tblapppay where payid = 4) d on m.appid = d.appid4  left join " + _
              " (select appid appid5 ,Typepay Typepay5,convert(int,payid) payid5,AppointDate AppointDate5,totalpay totalpay5 from tblapppay where payid = 5) e on m.appid = e.appid5 left join " + _
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

            str = " insert  into  tmp_Covernote_PayCredit (" + _
             " AppID, CarType, assignto, ProDuctID, initid, CarPetNO, createdate, ASNcomt, typereport, cartypename, " + _
            " fname, lname, ProTypeBrand, a1, a2, a3, initth, appid1, Typepay1, payid1, AppointDate1, totalpay1, appid2, Typepay2, payid2, AppointDate2, totalpay2, " + _
            " appid3 , Typepay3, payid3, AppointDate3, totalpay3, appid4, Typepay4, payid4, AppointDate4, totalpay4 , appid5, Typepay5, payid5, AppointDate5, totalpay5, appid6, Typepay6, payid6, AppointDate6, totalpay6,UserID  )" + _
            " values ( " + _
              " '" & dt2.Rows(0).Item("0") & "','" & dt2.Rows(0).Item("1") & "','" & dt2.Rows(0).Item("2") & "','" & dt2.Rows(0).Item("3") & "','" & dt2.Rows(0).Item("4") & "','" & dt2.Rows(0).Item("5") & "','" & (dt2.Rows(0).Item("7")) & "','" & dt2.Rows(0).Item("8") & "','" & dt2.Rows(0).Item("9") & "' ," + _
            " '" & dt2.Rows(0).Item("10") & "','" & dt2.Rows(0).Item("11") & "','" & dt2.Rows(0).Item("12") & "','" & dt2.Rows(0).Item("13") & "','" & dt2.Rows(0).Item("14") & "','" & dt2.Rows(0).Item("15") & "','" & dt2.Rows(0).Item("16") & "','" & dt2.Rows(0).Item("17") & "','" & chkNull(dt2.Rows(0).Item("18"), 0) & "','" & chkNull(dt2.Rows(0).Item("19"), 0) & "', " + _
            " '" & chkNull(dt2.Rows(0).Item("20"), 0) & "','" & d1 & "','" & chkNull(dt2.Rows(0).Item("22"), 1) & "','" & chkNull(dt2.Rows(0).Item("23"), 0) & "','" & chkNull(dt2.Rows(0).Item("24"), 0) & "','" & chkNull(dt2.Rows(0).Item("25"), 0) & "','" & d2 & "','" & chkNull(dt2.Rows(0).Item("27"), 1) & "','" & chkNull(dt2.Rows(0).Item("28"), 0) & "','" & chkNull(dt2.Rows(0).Item("29"), 0) & "', " + _
            " '" & chkNull(dt2.Rows(0).Item("30"), 0) & "','" & d3 & "','" & chkNull(dt2.Rows(0).Item("32"), 1) & "','" & chkNull(dt2.Rows(0).Item("33"), 0) & "','" & chkNull(dt2.Rows(0).Item("34"), 0) & "','" & chkNull(dt2.Rows(0).Item("35"), 0) & "','" & d4 & "','" & chkNull(dt2.Rows(0).Item("37"), 1) & "' ,'" & chkNull(dt2.Rows(0).Item("38"), 0) & "','" & chkNull(dt2.Rows(0).Item("39"), 0) & "','" & chkNull(dt2.Rows(0).Item("40"), 0) & "','" & d5 & "','" & chkNull(dt2.Rows(0).Item("42"), 1) & "' ,'" & chkNull(dt2.Rows(0).Item("43"), 0) & "','" & chkNull(dt2.Rows(0).Item("44"), 0) & "','" & chkNull(dt2.Rows(0).Item("45"), 0) & "','" & d6 & "','" & chkNull(dt2.Rows(0).Item("47"), 1) & "','" & Request.Cookies("UserID").Value & "' ) "
            Command = New SqlCommand(str, Conn)
            Command.ExecuteNonQuery()


            Dim pd1 As String = ""
            str = " SELECT TblApplication.AppID, TblCar.IdCard1, TblCar.TypeCard1, TblCar.IdCard2, TblCar.TypeCard2, TblCustomer.AddressRemark, ProtectDateCarpet, expProtectDateCarpet, " + _
                                 " ' ' AS tmp3, ' ' AS tmp4, ' ' AS tmp5 , ' ' AS tmp6, ' ' AS tmp7, ' ' AS tmp8,iscarpet " + _
          "    FROM TblCustomer INNER JOIN " + _
                                " TblCar ON TblCustomer.CusID = TblCar.CusID INNER JOIN " + _
                                " TblApplication ON TblCar.IdCar = TblApplication.Idcar where  appid = '" & GVShow.DataKeys(e.CommandArgument).Item(0) & "'"
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
                Session("pd2") = "ระยะเวลา พรบ.:  " + dt3.Rows(0).Item("ProtectDateCarpet") + " - " + dt3.Rows(0).Item("expProtectDateCarpet")
                Session("pd1") = dt3.Rows(0).Item("ProtectDateCarpet") + " - " + dt3.Rows(0).Item("expProtectDateCarpet")
            Else
                Session("pd2") = "ระยะเวลา พรบ.: - "
                Session("pd1") = "-"
            End If

            str = "insert into tmp_Covernote_app02 ( AppID, IdCard1, TypeCard1, IdCard2, TypeCard2, AddressRemark, tmp1, tmp2, tmp3, tmp4, tmp5, tmp6, tmp7, tmp8,UserID) values ( " + _
            " '" & dt3.Rows(0).Item("0") & "','" & dt3.Rows(0).Item("1") & "','" & dt3.Rows(0).Item("2") & "','" & dt3.Rows(0).Item("3") & "','" & dt3.Rows(0).Item("4") & "','" & dt3.Rows(0).Item("5") & "','" & dt3.Rows(0).Item("6") & "','" & dt3.Rows(0).Item("7") & "','" & dt3.Rows(0).Item("8") & "' ," + _
           " '" & dt3.Rows(0).Item("9") & "','" & dt3.Rows(0).Item("10") & "','" & dt3.Rows(0).Item("11") & "','" & dt3.Rows(0).Item("12") & "','" & dt3.Rows(0).Item("13") & "','" & Request.Cookies("UserID").Value & "')"
            Command = New SqlCommand(str, Conn)
            Command.ExecuteNonQuery()

            str = "select  distinct payid from tblapppay where appid = '" & GVShow.DataKeys(e.CommandArgument).Item(0) & "'"
            Dim Count2 As Integer = 0
            Command = New SqlCommand(str, Conn)
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Count2 += 1
                End While
            End If
            DataReader.Close()

            str = "update  tmp_Covernote_PayCredit set  asncomt = '" & Count2 & "' WHERE UserID = '" & Request.Cookies("UserID").Value & "'"
            Command = New SqlCommand(str, Conn)
            Command.ExecuteNonQuery()

            str = " SELECT TblCustomer.Sname " + _
                  " FROM TblCar INNER JOIN " + _
                  " TblApplication TblApplication_1 ON TblCar.IdCar = TblApplication_1.Idcar INNER JOIN " + _
                  " TblCustomer ON TblCar.CusID = TblCustomer.CusID " + _
                  " where appid = '" & GVShow.DataKeys(e.CommandArgument).Item(0) & "'"
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

            str = "update  tmp_Covernote_PayCredit set  a1 = '" & dt4.Rows(0).Item("sName") & "' WHERE UserID = '" & Request.Cookies("UserID").Value & "'"
            Command = New SqlCommand(str, Conn)
            Command.ExecuteNonQuery()

            Conn.Close()
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "Preview", "Preview(" & GVShow.DataKeys(e.CommandArgument).Item(0) & "," & GVShow.DataKeys(e.CommandArgument).Item(1) & "," & Request.Cookies("UserID").Value & ")", True)
            

        End If
    End Sub
    
    Function chkNull(ByVal f1 As Object, ByVal flag As Byte) As String
        If IsDBNull(f1) = True Then
            chkNull = "  "
        Else
            If flag = 1 Then
                chkNull = Format(CInt(f1), "###,###")
            Else
                chkNull = f1
            End If
        End If
        Return chkNull
    End Function
End Class
