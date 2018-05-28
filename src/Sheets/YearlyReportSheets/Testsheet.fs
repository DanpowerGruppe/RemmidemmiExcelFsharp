module ObjRepro.YearlyReportSheets.Testsheet

open OfficeOpenXml
open ObjRepro.Domain
open ObjRepro.UtilsReport
open ObjRepro.Sqldata


// Get Data from Testdb

let testSheet (package:ExcelPackage) (exportedReport:XLSReport) =
    let wks = package.Workbook.Worksheets.[1]
    wks.Cells.[1,1].Value  <- "Berichtsname:"
    wks.Cells.[2,1].Value  <- "Berichtsintervall:"

    printfn "Generated Test Sheet %s" (matchReportIntervall exportedReport.ReportIntervall)

