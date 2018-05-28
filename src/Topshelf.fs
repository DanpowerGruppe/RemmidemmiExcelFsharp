module ObjRepro.Program

open System
open System.Reflection
open Topshelf
open Tasks

[<assembly: AssemblyTitle("Sample Service")>]
()


[<EntryPoint>]
let main argv =
  let info : string -> unit = fun s -> Console.WriteLine(sprintf "%s logger/sample-service: %s" (DateTime.UtcNow.ToString("o")) s)

  let start hc =
      createTasks()

      info "sample service started"
      true 
    
  let stop hc =
    info "sample service stopped"
    stopTasks()
    true
  
  Service.Default
  |> display_name "ObjRepro"
  |> instance_name "ObjRepro"
  |> with_start start
  |> with_stop stop
  |> run