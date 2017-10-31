module Excelfsharp.Db

open System
open System.IO
open FSharp.Data.Sql
open FSharp.Data.TypeProviders
open FSharp.Linq
open System.Data.SqlClient
open Domain

//***************** Connect to DB ***********************//

let connectionString = "Data Source=SQLDG.danpower-gruppe.lokal\DG;Initial Catalog=SageEnMS;Integrated Security=True"
let [<Literal>] CompileTimeConnectionString = "Data Source=SQLDG.danpower-gruppe.lokal\DG;Initial Catalog=SageEnMS;Integrated Security=True"

type Sql = SqlDataProvider<Common.DatabaseProviderTypes.MSSQLSERVER, ConnectionString = CompileTimeConnectionString, UseOptionTypes=true>
let triggerSQL = Common.QueryEvents.SqlQueryEvent |> Event.add (printfn "Executing SQL: %O")
// // // //let triggerLinq = Common.QueryEvents.LinqExpressionEvent |> Observable.subscribe(printfn "Excecuting SQL: %A")

let db = Sql.GetDataContext connectionString

