module ObjRepro.Db

open FSharp.Data.Sql
open System.Transactions
open FSharp.Data.Sql.Transactions
open System.Transactions

let [<Literal>] CompileTimeConnectionString = "Data Source=localhost;User Id=SA;Password=Passw0rd"

let runtimeConnectionString = "Data Source=localhost;User Id=SA;Password=Passw0rd"

let triggerSQL = Common.QueryEvents.SqlQueryEvent |> Event.add (printfn "Executing SQL: %O")

type Sql = SqlDataProvider<Common.DatabaseProviderTypes.MSSQLSERVER, CompileTimeConnectionString, UseOptionTypes=true>

let db = Sql.GetDataContext runtimeConnectionString
let saveContext = db.SaveContextSchema ()