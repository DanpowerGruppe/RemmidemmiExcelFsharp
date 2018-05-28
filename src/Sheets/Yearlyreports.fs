module ObjRepro.YearlyReports

open Domain
open UtilsReport


let buildReport (exportedReport:XLSReport) =

    //Start ExcelApp
    use excelPackage = openExcelPackage ()
    printfn "added Workbook"
    YearlyReportSheets.Testsheet.testSheet excelPackage exportedReport |> ignore
    //ExportReport
    exportReport excelPackage exportedReport
 
