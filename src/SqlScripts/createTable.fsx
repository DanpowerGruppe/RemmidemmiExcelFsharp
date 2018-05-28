#r @"C:\Users\tforkmann\Documents\1_Programming\5_Präsentationen\RemmidemmiExample\packages\SQLProvider\lib\netstandard2.0\FSharp.Data.SqlProvider.dll"
#r @"C:\Users\tforkmann\Documents\1_Programming\5_Präsentationen\RemmidemmiExample\packages\SQLProvider\lib\netstandard2.0\System.Data.SqlClient.dll"
open System
open System.IO
open FSharp.Data.Sql
open FSharp.Linq
open System.Data.SqlClient
open Microsoft.FSharp.Reflection

let connectionString  = "Data Source=localhost;User Id=SA;Password=Passw0rd"
let generateTestTable = 
    let conn = new SqlConnection(connectionString)
    conn.Open()
    printfn "SqlConnection opened"
    let structureSql =
        "CREATE TABLE COUNTRIES (" +
        "COUNTRY_ID CHAR(2) CONSTRAINT COUNTRY_ID_NN NOT NULL, " +
        "COUNTRY_NAME VARCHAR(40), " + 
        "REGION_ID INT, " + 
        "OTHER TEXT, " +
        "CONSTRAINT COUNTRY_C_ID_PK PRIMARY KEY (COUNTRY_ID))"

    let cmd = new SqlCommand(structureSql,conn)
    cmd.ExecuteNonQuery() |> ignore
    conn.Close()
    printfn "Table from record TradeData generated"