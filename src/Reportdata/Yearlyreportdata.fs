module ObjRepro.YearlyReportData

open Domain

let reportNewsYearlyReport = ["TestNews"]
let yearint = 2017001
let reportdataYearlyReport:XLSReport = { 
        ReportName = "TestReport_"
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
        ReportArea = "Plants"
        ReportRecipient = "e"
        ReportDayOfDeadLine = "f"
        ReportDataTypes = "Data"
        ReportEnergyDataField = "g"
        ReportEnergyDataFieldList = ["h"]
        ReportFinancialDataField = "i"}