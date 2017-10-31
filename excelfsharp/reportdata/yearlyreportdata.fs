module Excelfsharp.YearlyReportData

open System
open System.Web
open System.IO
open NetOffice.ExcelApi
open NetOffice.ExcelApi.Enums
open Domain
open UtilsReport

let reportNewsYearlyReport = ["TestNews"]
let yearint = 2017001
let reportdataYearlyReport:XLSReport = { 
        ReportName = "TestReport_" + matchReportIntervall Yearly
        ReportTime = yearint.ToString()
        ReportIntervall = Yearly
        ReportTyp = "vertikal"
        ReportID = 150
        ReportGoal = "TestReportGoal"
        ReportLaw = "keine"
        ReportNews = "a"
        ReportMandantlist = "b"
        ReportSageendmsGroupJoin = "c"
        ReportSageendmsGroupUserJoin = "d"
        ReportArea = "Energiezentralen;Biogasanlagen;Kraftwerke"
        ReportRecipient = "e"
        ReportDayOfDeadLine = "f"
        ReportDataTypes = "Energetische Datenpunkte"
        ReportEnergyDataField = "g"
        ReportEnergyDataFieldList = ["h"]
        ReportFinancialDataField = "i"}