module ObjRepro.UtilsReport

open System
open System.IO
open OfficeOpenXml
open Domain

let intervall  = "yearly"

let reportDir =

    let sourceDir = __SOURCE_DIRECTORY__
    let projDir = Path.GetFullPath(Path.Combine(sourceDir, @"..\"));
    printfn "projDir: %s" projDir   
    if not(Directory.Exists(projDir + "reports")) then 
        Directory.CreateDirectory(projDir + "reports") |> ignore
    projDir

///Function to start an ExcelApplication
let openExcelPackage () =
    printfn "opening ExcelWorkbook"
    let memoryStream = new MemoryStream ()
    let xlspackage = new ExcelPackage (memoryStream)
    xlspackage

let exportReport (wb:ExcelPackage) (exportedReport:XLSReport)=
    //ExportReport
    let exportDir = reportDir
    let reportDir = Path.Combine(exportDir, "Reports")
    printfn "ExportPath %s" (reportDir) 
    let dateTime = DateTime.Now.ToString("yyyyMMdd_HHmm")
    printfn "DateTime %s" (dateTime) 
    let reportFolderPath  = Path.Combine(reportDir, exportedReport.ReportName + "_" + dateTime + ".xlsx")
    let data = wb.GetAsByteArray()
    File.WriteAllBytes(reportFolderPath,data)
    printfn "Saved Excel report at %A" reportFolderPath    