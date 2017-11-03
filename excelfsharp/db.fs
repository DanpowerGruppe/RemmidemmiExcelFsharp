module Excelfsharp.Db

open System
open System.IO
open FSharp.Data.Sql
open FSharp.Data.TypeProviders
open FSharp.Linq
open System.Data.SqlClient
open Domain

//***************** Connect to DB ***********************//

type Provider =
| MSSQL
| SQLite

let providerType = MSSQL

let connectionString = "Data Source=SQLDG.danpower-gruppe.lokal\DG;Initial Catalog=SageEnMS;Integrated Security=True"
//    match providerType with
//    | MSSQL ->  "Data Source=SQLDG.danpower-gruppe.lokal\DG;Initial Catalog=SageEnMS;Integrated Security=True"
//
//    | SQLite -> @"Data Source=" + __SOURCE_DIRECTORY__ + @"/northwindEF.db"


//let connectionString = "Data Source=SQLDG.danpower-gruppe.lokal\DG;Initial Catalog=SageEnMS;Integrated Security=True"
let [<Literal>] CompileTimeConnectionString = "Data Source=SQLDG.danpower-gruppe.lokal\DG;Initial Catalog=SageEnMS;Integrated Security=True"
let [<Literal>] CompileTimeConnectionStringL = @"Data Source=" + __SOURCE_DIRECTORY__ + @"/northwindEF.db;Version=3"


type Sql = SqlDataProvider<Common.DatabaseProviderTypes.MSSQLSERVER, ConnectionString = CompileTimeConnectionString, UseOptionTypes=true>
//type SqlL = SqlDataProvider<Common.DatabaseProviderTypes.SQLITE, ConnectionString = CompileTimeConnectionStringL, UseOptionTypes=true>

let triggerSQL = Common.QueryEvents.SqlQueryEvent |> Event.add (printfn "Executing SQL: %O")
// // // //let triggerLinq = Common.QueryEvents.LinqExpressionEvent |> Observable.subscribe(printfn "Excecuting SQL: %A")

let db = Sql.GetDataContext connectionString
//let dbL = SqlL.GetDataContext connectionString
