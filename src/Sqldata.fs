module ObjRepro.Sqldata

open System
open System.Web 
open System.IO 
open System.Data 
open FSharp.Data.Sql
open FSharp.Data.TypeProviders
open FSharp.Reflection
open System.Linq
open Db

//***************** Add Mappers ***********************//

//***************** Variante a) - entity mapper ***********************//

let mapCountries (dbRecord:Sql.dataContext.``dbo.COUNTRIESEntity``) : Domain.Country =
   { Name =  match dbRecord.CountryName with
             | Some x -> x
             | None -> "City not existing"
     Id =    dbRecord.CountryId
     RegionId = match dbRecord.RegionId with
                | Some x -> x
                | None -> 0  }


let getMasterdataCountries () = 
 query {   for countries in db.Dbo.Countries do
           select countries }
 |> Seq.map mapCountries
 |> Seq.toArray
