module ObjRepro.Tasks

open System

let refreshRate = TimeSpan.FromMilliseconds 500.

type TaskMessage = 
    | CreateYearlyReports

let yearlyreports () = YearlyReports.buildReport YearlyReportData.reportdataYearlyReport

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


let createTasks () = 
    Trigger.SingleTask(DateTime.Now, CreateYearlyReports)
    |> trigger.Post

let stopTasks () = trigger.Post Trigger.Stop