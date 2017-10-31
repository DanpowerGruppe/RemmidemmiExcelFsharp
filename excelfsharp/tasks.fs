module Excelfsharp.Tasks

open System
open System.IO
open Domain

let refreshRate = TimeSpan.FromMilliseconds 500.

type TaskMessage = 
    | CreateYearlyReports

let yearlyreports () = YearlyReports.buildReport YearlyReportData.reportdataYearlyReport reportStatus

let taskAgent = 
    Trigger.Agent.Start(fun agent ->
        let rec loop() = 
            async { 
                let! msg = agent.Receive()
                match msg with
                | CreateYearlyReports -> yearlyreports ()
                return! loop ()
            }
        loop())

let trigger = Trigger.createTrigger refreshRate taskAgent

// let month = DateTime.Now.AddMonths(-1).ToString("MMMM")

let createTasks () = 
    // Trigger.RecurringTask(matchTimeSpan reportStatus, CreateYearlyReports)
    // |> trigger.Post
    Trigger.SingleTask(DateTime.Now, CreateYearlyReports)
    |> trigger.Post

let stopTasks () = trigger.Post Trigger.Stop