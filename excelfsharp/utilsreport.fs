module Excelfsharp.UtilsReport

open System
open System.IO
open NetOffice.ExcelApi
open Domain

let matchReportIntervall (intervall:ReportIntervall) =
    match intervall with
    | Dayly -> "täglich"
    | Weekly -> "wöchentlich"
    | Monthly -> "monatlich"
    | Quarterly -> "quartalsweise"
    | Halfyearly -> "halbjährlich"
    | Yearly -> "jährlich"

let matchReportDir (reportStatus:ReportStatus) =

    let sourceDir = __SOURCE_DIRECTORY__
    let projDir = Path.GetFullPath(Path.Combine(sourceDir, @"..\"));
    printfn "projDir: %s" projDir   
    if not(Directory.Exists(projDir + "reports")) then 
        Directory.CreateDirectory(projDir + "reports") |> ignore
    match reportStatus with
    | Productive -> projDir
    | Test -> projDir

let exportReport (xlapp:Application) (wb:Workbook) (exportedReport:XLSReport) (status:ReportStatus)=
    //ExportReport
    let exportDir = matchReportDir status
    let reportDir = Path.Combine(exportDir, "reports")
    printfn "ExportPath %s" (reportDir) 
    let dateTime = DateTime.Now.ToString("yyyymmdd_HHmm")
    printfn "DateTime %s" (dateTime) 
    let savedFileNameXLS =
        let fileName = reportDir + "/" + exportedReport.ReportName + "_" + dateTime + ".xlsx"
        printfn "FileName %s" (fileName) 
        wb.SaveAs (fileName, Enums.XlFileFormat.xlOpenXMLWorkbook)
        fileName
    printfn "Creating Excel report at %s" (savedFileNameXLS) 
    wb.Close ()
    xlapp.Quit ()
    printfn "Quit ExcelApp after saving %s" (savedFileNameXLS) 
    let savedFileNameXML = reportDir + "/" + exportedReport.ReportName + "_" + dateTime + ".xml"
    let reportid = exportedReport.ReportID.ToString()
    printfn "Creating Xml report at %s" (savedFileNameXML)