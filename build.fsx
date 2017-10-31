// include Fake libs
#r "./packages/FAKE/tools/FakeLib.dll"

open Fake
open System
open System.IO
open System.Diagnostics

// Filesets
let solutionFile  = "Excelfsharp.sln"

let configuration = "Release"

// Targets
Target "Clean" (fun _ ->
    CleanDirs ["bin"]
)

let build () = 
    !! solutionFile
    |> MSBuildRelease "" "Build"
    |> ignore

Target "Build" (fun _ ->
    build()
)

let rec runService() =
    build()

    let app = Path.Combine("bin","Release","ExcelFsharp.exe")
    let ok = 
        execProcess (fun info -> 
            info.FileName <- app
            info.Arguments <- "") TimeSpan.MaxValue
    if not ok then tracefn "Website shut down."

Target "Run" (fun _ ->

   runService()
)

Target "Default" DoNothing


// Build order
"Clean"
  ==> "Build"
  ==> "Run"
  ==> "Default"

// start build
RunTargetOrDefault "Default"
