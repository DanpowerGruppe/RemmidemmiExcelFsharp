// include Fake libs
#r "packages/FAKE/tools/FakeLib.dll"

open Fake
open System
open System.IO
open Fake.IO
open Fake.DotNet
open Fake.Core
open Fake.Core.Target
open Fake.Core.TargetOperators
open Fake.IO.Globbing.Operators

// --------------------------------------------------------------------------------------
// Project Information
// --------------------------------------------------------------------------------------

let project = "ObjRepro"
let backEndPath = "./src/" |> FullName

let mutable dotnetExePath = "dotnet"

let deployDir = "./deploy"

let buildDir = "./bin/"

// --------------------------------------------------------------------------------------
// Standard Windows Service Build Steps
// --------------------------------------------------------------------------------------

let run' timeout cmd args dir =
    if execProcess (fun info ->
        info.FileName <- cmd
        if not (String.IsNullOrWhiteSpace dir) then
            info.WorkingDirectory <- dir
        info.Arguments <- args
    ) timeout |> not then
        failwithf "Error while running '%s' with args: %s" cmd args

let run = run' System.TimeSpan.MaxValue

let runDotnet workingDir args =
    let result =
        ExecProcess (fun info ->
            info.FileName <- dotnetExePath
            info.WorkingDirectory <- workingDir
            info.Arguments <- args) TimeSpan.MaxValue
    if result <> 0 then failwithf "dotnet %s failed" args

let platformTool tool winTool =
    let tool = if Environment.isUnix then tool else winTool
    tool
    |> Process.tryFindFileOnPath
    |> function Some t -> t | _ -> failwithf "%s not found" tool

// --------------------------------------------------------------------------------------
// Clean Build Results
// --------------------------------------------------------------------------------------

create "Clean" (fun _ ->
    !!"src/**/bin"
    |> Shell.cleanDirs
    !! "src/**/obj/*.nuspec"
    |> Shell.cleanDirs

    Shell.cleanDirs [buildDir; "temp"; "docs/output"; deployDir;]
)

// --------------------------------------------------------------------------------------
// Install DotNetCore
// --------------------------------------------------------------------------------------
let installDotNet = lazy DotNet.install DotNet.Release_2_1_4

create "InstallDotNetCore" (fun _ -> ignore installDotNet.Value)


// --------------------------------------------------------------------------------------
// Build libraby
// --------------------------------------------------------------------------------------

create "BuildBackEnd" (fun _ ->
    runDotnet backEndPath "build -r win10-x64"
)

let rec runService() =
    let app =  @"src\bin\Debug\netcoreapp2.0\win10-x64\ObjRepro.exe"
    let ok = 
        execProcess (fun info -> 
            info.FileName <- app
            info.Arguments <- "") TimeSpan.MaxValue
    if not ok then tracefn "Website shut down."

create "Run" (fun _ ->
   runService()
)


create "All" DoNothing

// Build order
"Clean"
  ==> "InstallDotNetCore"
  ==> "BuildBackEnd"  
  ==> "Run"
  ==> "All"

// start build
runOrDefault "All"