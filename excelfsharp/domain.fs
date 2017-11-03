module Excelfsharp.Domain

open System  

type ReportIntervall =
    | Dayly
    | Weekly
    | Monthly
    | Quarterly
    | Halfyearly
    | Yearly

type ReportStatus =
    | Productive
    | Test

let reportStatus = Test//Productive//Test//Productive

let isWeek (date:DateTime) =
    let tomorrow = date.AddDays 1.
    tomorrow.DayOfWeek = DayOfWeek.Tuesday

let isFithtenOfMonth (date:DateTime) =
    let tomorrow = date.AddDays 1.
    tomorrow.Day = 16

let isQuarterlyOfYear (date:DateTime) =
        match date.Month with
        | 1 -> isFithtenOfMonth date
        | 4 -> isFithtenOfMonth date
        | 7 -> isFithtenOfMonth date
        | _ -> false

let isHalfofYear (date:DateTime) =
        match date.Month with
        | 6 -> isFithtenOfMonth date
        | _ -> false

let isYear (date:DateTime) =
    let nextYear = date.AddMonths 1
    nextYear.Month = 1

let matchTaskControl (reportIntervall:ReportIntervall) (date:DateTime)= 
    match reportIntervall with
    | Dayly ->  DateTime.Today = date 
    | Weekly -> isWeek date 
    | Monthly -> isFithtenOfMonth date
    | Quarterly -> isQuarterlyOfYear date
    | Halfyearly -> isHalfofYear date
    | Yearly -> isYear date

let matchTimeSpan (reportStatus:ReportStatus) =
     match reportStatus with
     | Productive -> TimeSpan.FromDays 1.0
     | Test -> TimeSpan.FromMinutes 1.0


type XLSReport =   
  { ReportName : string
    ReportTime : string
    ReportIntervall : ReportIntervall
    ReportTyp : string
    ReportID : int
    ReportGoal : string
    ReportLaw : string
    ReportNews : string
    ReportMandantlist : string 
    ReportSageendmsGroupJoin : string
    ReportSageendmsGroupUserJoin : string
    ReportArea : string
    ReportRecipient : string
    ReportDayOfDeadLine : string
    ReportDataTypes : string
    ReportEnergyDataField : string
    ReportEnergyDataFieldList : string list
    ReportFinancialDataField : string}     


type Energyplant =
  { Bezeichnung : string;
    Mandantbez : string;
    Mandant : int16
    UserFibuKostenstelle : string;
    Strasse : string;
    Hausnummer : int;
    Plz : string;
    Ort : string;
    Bundesland : string ;
    Inbetrieb : int16
    Ibn : DateTime;
    Stilllegung : DateTime;
    Id : string}

type Customers =
  { City : string;
    Address : string;
    Companyname : string}