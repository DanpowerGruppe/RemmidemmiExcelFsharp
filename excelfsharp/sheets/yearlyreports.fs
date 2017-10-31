module Excelfsharp.YearlyReports

open System
open System.Web
open System.IO
open NetOffice.ExcelApi
open NetOffice.ExcelApi.Enums
open Domain
open UtilsReport
open YearlyReportData

let buildReport (exportedReport:XLSReport) (status:ReportStatus) =

    //Start ExcelApp
    use xlapp = new NetOffice.ExcelApi.Application ()
    xlapp.DisplayAlerts <- true
    xlapp.Visible <- true
    xlapp.ScreenUpdating <- true
    let wb = xlapp.Workbooks.Add ()
    
    //Build ExcelSheet
    let testSheet = YearlyReportSheets.Testsheet.testSheet wb exportedReport

    //ExportReport
    exportReport xlapp wb exportedReport status


