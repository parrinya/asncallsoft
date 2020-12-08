Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Imports SautinSoft
Imports System.IO
Partial Class Default2
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click

        Dim reportWord As New ReportDocument
        Dim reportname As String
        reportname = Server.MapPath("~/Modules/Sale/Report/rptQuotation.rpt")


        Dim users As String = "sa"
        'Dim pass As String = "DTS2009"
        Dim pass As String = "asn@sr1"

        'Dim rpt As New CrystalReportViewer

        reportWord.Load(reportname)
        reportWord.SetDatabaseLogon(users, pass)

        Dim faxServer As New FAXCOMLib.FaxServerClass ' = New FAXCOMLib.FaxServerClass
        'faxServer.Connect(Environment.MachineName)
        faxServer.Connect("\\ASN-002\Fax")
        'Dim faxDoc As FAXCOMLib.FaxDoc = CType(faxServer.CreateDocument(Server.MapPath("~/TestFax.doc")), FAXCOMLib.FaxDoc)
        Dim faxDoc As FAXCOMLib.FaxDoc = CType(faxServer.CreateDocument(Server.MapPath("~/TestFax.tif")), FAXCOMLib.FaxDoc)
        faxDoc.RecipientName = "GI Solutions"
        faxDoc.FaxNumber = "026192296" ' NumberFax
        faxDoc.DisplayName = "TEST FAX"
        'faxDoc.FileName = Server.MapPath("FaxPath//" & txtPath.Text)
        faxDoc.RecipientTitle = "Fax Send GI Solutions"
        faxDoc.SenderFax = "ASP.NET"
        faxDoc.SenderName = "Fax R&D from ASP.NET"
        'Dim myproccess As ProcessInfo

        Dim Response As Integer = faxDoc.Send
        faxServer.Disconnect()





    End Sub

    Protected Sub GenReport()
        'This Code is used to Convert to word document
        Dim reportWord As New ReportDocument  ' Report Name 
        Dim reportname As String
        reportname = Server.MapPath("~/Modules/Manager/Report/rptPayment.rpt")

        Dim users As String = "sa"
        'Dim pass As String = "DTS2009"
        Dim pass As String = "asn@sr1"

        'Dim rpt As New CrystalReportViewer

        reportWord.Load(reportname)
        reportWord.SetDatabaseLogon(users, pass)
        reportWord.SetParameterValue("AppID", 259313)
        reportWord.SetParameterValue("PayID", 1)


        Dim strExportFile As String = Server.MapPath("~/TestWord.doc")
        reportWord.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile
        reportWord.ExportOptions.ExportFormatType = ExportFormatType.WordForWindows
        Dim objOptions As DiskFileDestinationOptions = New DiskFileDestinationOptions()
        objOptions.DiskFileName = strExportFile
        reportWord.ExportOptions.DestinationOptions = objOptions
        'reportWord.SetDataSource(myDS)
        reportWord.Export()
        objOptions = Nothing
        reportWord = Nothing

        'Dim CrExportOptions As ExportOptions
        'Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions()
        'Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()
        'CrDiskFileDestinationOptions.DiskFileName = Server.MapPath("~/TestWord.pdf")
        'CrExportOptions = reportWord.ExportOptions
        'With CrExportOptions
        '    .ExportDestinationType = ExportDestinationType.DiskFile
        '    .ExportFormatType = ExportFormatType.PortableDocFormat
        '    .DestinationOptions = CrDiskFileDestinationOptions
        '    .FormatOptions = CrFormatTypeOptions
        'End With
        'reportWord.Export()

        'Dim f As New SautinSoft.PdfFocus
        'f.OpenPdf(Server.MapPath("~/TestWord.pdf"))
        ''If f.PageCount > 0 Then
        'f.ImageOptions.Dpi = 300
        'f.ToMultipageTiff(Server.MapPath("~/TestWord.tiff"))
        'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "script", "alert('บันทึกข้อมูลเรียบร้อย');", True)
        'End If



    End Sub

    Protected Sub Button2_Click(sender As Object, e As System.EventArgs) Handles Button2.Click
        GenReport()
    End Sub

  
    Protected Sub Button3_Click(sender As Object, e As System.EventArgs) Handles Button3.Click
        'Convert PDF files to 300-dpi TIFF files
        Dim f As New SautinSoft.PdfFocus
        f.OpenPdf(Server.MapPath("~/TestWord.pdf"))
        If f.PageCount > 0 Then
            f.ImageOptions.Dpi = 120
            f.ImageOptions.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg
            Dim image2() As Byte = f.ToImage(2)

            File.WriteAllBytes(Server.MapPath("~/TestWord.jpg"), image2)
            'f.ToImage(Server.MapPath("~/TestWord.jpg"))
        End If
        'this property is necessary only for registered version
        'f.Serial = "XXXXXXXXXXX"

        'Dim pdfFiles() As String = Directory.GetFiles("..\..\..\..\", "*.pdf")
        'Dim folderWithTiffs As String = "..\..\..\..\"

        'For Each pdffile As String In pdfFiles
        '    f.OpenPdf(pdffile)

        '    If f.PageCount > 0 Then
        '        'Set image format: TIFF, 300 dpi
        '        f.ImageOptions.Dpi = 300
        '        f.ImageOptions.ImageFormat = System.Drawing.Imaging.ImageFormat.Tiff

        '        'Save all pages to tiff files with 300 dpi
        '        f.ToImage(folderWithTiffs, Path.GetFileNameWithoutExtension(pdffile))
        '    End If
        '    f.ClosePdf()
        'Next pdffile
        ''Show folder with tiffs
        'System.Diagnostics.Process.Start(folderWithTiffs)
    End Sub
End Class
