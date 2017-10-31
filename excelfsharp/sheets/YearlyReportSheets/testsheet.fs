module Excelfsharp.YearlyReportSheets.Testsheet

open NetOffice.ExcelApi
open Excelfsharp.Domain
open Excelfsharp.UtilsReport
open Excelfsharp.Sqldata


// Get Data from Testdb

let testSheet (wb:Workbook) (exportedReport:XLSReport) =

    // Report HEADER - static
    let rshezdp = wb.Sheets.["Tabelle1"] :?> Worksheet
    
    rshezdp.Name <- "Energyplants"
    rshezdp.Range(rshezdp.Cells.[1,1].Address).Value2  <- "Energyplants"

    let masterData = getMasterdata () //getEnergyplants //Doesn't work
    printfn "finishedFirst Query"

    let startRow = 2
    let rows = masterData.Length

    rshezdp.Cells.[8, 1].Value2 <- "Bezeichnung Energiezentrale"
    let range = rshezdp.Range(rshezdp.Cells.[startRow, 1], rshezdp.Cells.[startRow+rows-1, 1])
    let arrayName = Array2D.init rows 1 (fun a b -> masterData.[a].Bezeichnung)
    range.Value2 <- arrayName
    rshezdp.Cells.[8, 2].Value2 <- "MandantBezeichnung"
    let range = rshezdp.Range(rshezdp.Cells.[startRow, 2], rshezdp.Cells.[startRow+rows-1, 2])
    let arrayName = Array2D.init rows 1 (fun a b -> masterData.[a].Mandantbez)
    range.Value2 <- arrayName
    rshezdp.Cells.[8, 3].Value2 <- "USER_FIBUKostenstelle"
    let range = rshezdp.Range(rshezdp.Cells.[startRow, 3], rshezdp.Cells.[startRow+rows-1, 3])
    let arrayName = Array2D.init rows 1 (fun a b -> masterData.[a].UserFibuKostenstelle)
    range.Value2 <- arrayName
    rshezdp.Cells.[8, 4].Value2 <- "USER_Straße"
    let range = rshezdp.Range(rshezdp.Cells.[startRow, 4], rshezdp.Cells.[startRow+rows-1, 4])
    let arrayName = Array2D.init rows 1 (fun a b -> masterData.[a].Strasse)
    range.Value2 <- arrayName
    rshezdp.Cells.[8, 5].Value2 <- "USER_Hausnummer"
    let range = rshezdp.Range(rshezdp.Cells.[startRow, 5], rshezdp.Cells.[startRow+rows-1, 5])
    let arrayName = Array2D.init rows 1 (fun a b -> masterData.[a].Hausnummer)
    range.Value2 <- arrayName
    rshezdp.Cells.[8, 6].Value2 <- "USER_PLZ"
    let range = rshezdp.Range(rshezdp.Cells.[startRow, 6], rshezdp.Cells.[startRow+rows-1, 6])
    let arrayName = Array2D.init rows 1 (fun a b -> masterData.[a].Plz)
    range.Value2 <- arrayName
    rshezdp.Cells.[8, 7].Value2 <- "USER_Ort"
    let range = rshezdp.Range(rshezdp.Cells.[startRow, 7], rshezdp.Cells.[startRow+rows-1, 7])
    let arrayName = Array2D.init rows 1 (fun a b -> masterData.[a].Ort)
    range.Value2 <- arrayName
    rshezdp.Cells.[8, 8].Value2 <- "USER_Bundesland"
    let range = rshezdp.Range(rshezdp.Cells.[startRow, 8], rshezdp.Cells.[startRow+rows-1, 8])
    let arrayName = Array2D.init rows 1 (fun a b -> masterData.[a].Bundesland)
    range.Value2 <- arrayName
    rshezdp.Cells.[8, 9].Value2 <- "InBetriebEZ"
    let range = rshezdp.Range(rshezdp.Cells.[startRow, 9], rshezdp.Cells.[startRow+rows-1, 9])
    let arrayName = Array2D.init rows 1 (fun a b -> masterData.[a].Inbetrieb)
    range.Value2 <- arrayName
    rshezdp.Cells.[8, 10].Value2 <- "USER_IBN"
    let range = rshezdp.Range(rshezdp.Cells.[startRow, 10], rshezdp.Cells.[startRow+rows-1, 10])
    let arrayName = Array2D.init rows 1 (fun a b -> masterData.[a].Ibn)
    range.Value2 <- arrayName
    range.NumberFormat <- "yyyy-mm-dd"
    rshezdp.Cells.[8, 11].Value2 <- "USER_Stilllegung"
    let range = rshezdp.Range(rshezdp.Cells.[startRow, 11], rshezdp.Cells.[startRow+rows-1, 11])
    let arrayName = Array2D.init rows 1 (fun a b -> masterData.[a].Stilllegung)
    range.Value2 <- arrayName
    range.NumberFormat <- "yyyy-mm-dd"

    let autofit = rshezdp.Columns.AutoFit ()
    printfn "Generated Test Sheet %s" (matchReportIntervall exportedReport.ReportIntervall)

