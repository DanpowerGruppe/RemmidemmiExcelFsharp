module Excelfsharp.Sqldata

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

// MSSQL

let mapEnergyPlant (dbRecord:Sql.dataContext.``dbo.EnmsSicht_KstEnergiezentraleEntity``) : Domain.Energyplant =
    { Bezeichnung = dbRecord.Bezeichnung
      Mandantbez = 
        match  dbRecord.MandantBezeichnung with
          | Some x -> x
          | None -> "Mandantenbezeichnung nicht gepflegt"
      Mandant = dbRecord.Mandant 
      UserFibuKostenstelle =
        match  dbRecord.UserFibuKostenstelle with
          | Some x -> x
          | None -> "FibuKostenstelle nicht gepflegt"
      Strasse = 
        match  dbRecord.UserStrasse with
          | Some x -> x
          | None -> "Strasse nicht gepflegt"
      Hausnummer = 
        match dbRecord.UserHausnummer with
          | Some x -> x
          | None -> 0
      Plz = 
        match dbRecord.UserPlz with
          | Some x -> x
          | None -> "Plz nicht gepflegt"
      Ort = 
        match dbRecord.UserOrt with
          | Some x -> x
          | None -> "Ort nicht gepflegt"
      Bundesland =
        match dbRecord.UserBundesland with
          | Some x -> x
          | None -> "Bundesland nicht gepflegt" 
      Inbetrieb = 
        match dbRecord.UserInBetrieb with
          | Some x -> x
          | None -> int16(0)
      Ibn = 
        match dbRecord.UserIbn with
          | Some x -> x
          | None ->  DateTime.MinValue
      Stilllegung = 
        match dbRecord.UserAbgang with
          | Some x -> x
          | None -> DateTime.MinValue
      Id = dbRecord.KstStelle  }

// SQLite

//let mapCustomers (dbRecord:SqlL.dataContext.``main.CustomersEntity``) : Domain.Customers =
//    { City =  match dbRecord.City with
//              | Some x -> x
//              | None -> "City not existing"
//      Address =     match dbRecord.Address with
//                    | Some x -> x
//                    | None -> "Address not existing"
//      Companyname = dbRecord.CompanyName   }

// MSSQL


let getMasterdata () = 
  query {   for energyplants in db.Dbo.EnmsSichtKstEnergiezentrale do
            select energyplants }
  |> Seq.map mapEnergyPlant
  |> Seq.toArray

// SQLite

//let getMasterdataCustomers () = 
//  query {   for customers in dbL.Main.Customers do
//            select customers }
//  |> Seq.map mapCustomers
//  |> Seq.toArray

//***************** Variante b) - convenience  method ***********************//

// MSSQL

let getEnergyplants () = 
  query {   for energyplants in db.Dbo.EnmsSichtKstEnergiezentrale do
            select energyplants }
  |> Seq.map (fun x -> x.MapTo<Domain.Energyplant>())
  |> Seq.toArray

// SQLite
  
//let getCustumers () = 
//  query {   for customers in dbL.Main.Customers do
//            select customers }
//  |> Seq.map (fun x -> x.MapTo<Domain.Customers>())
//  |> Seq.toArray


//***************** Add Standard Filters ***********************//
//      this is extremly helpful if you have tons of queries    //

let filterEZ mandantfrom mandantto = 
    <@ fun (energyplants : Db.Sql.dataContext.``dbo.EnmsSicht_KstEnergiezentraleEntity``) -> 
            energyplants.Aktiv = int16(-1) 
            && energyplants.Mandant >= int16 mandantfrom
            && energyplants.Mandant < int16 mandantto
            && energyplants.Bezeichnung <>% "%(CNG)%" 
            && (energyplants.UserFibuKostenstelle.Value <>% "T024%"  || energyplants.UserFibuKostenstelle.IsNone)
            && (energyplants.UserFibuKostenstelle.Value <>% "T025%" || energyplants.UserFibuKostenstelle.IsNone)
            && (energyplants.UserFibuKostenstelle.Value <> "T021001" || energyplants.UserFibuKostenstelle.IsNone) @>

//***************** Perform a easy join ***********************//

let countAllEnergyplantsWithGenerators () = 
  query {   for energyplants in db.Dbo.EnmsSichtKstEnergiezentrale do
            join generators in db.Dbo.EnmsSichtKstErzeuger on (energyplants.KstStelle =  generators.UserKstUebergeordnet.Value)
            where ((%filterEZ 1 100) energyplants)
            select energyplants }
  |> Seq.length

//***************** Using nested joins ***********************//

let getCountofEZ () =
    let query1 =
        query { for energyplants in db.Dbo.EnmsSichtKstEnergiezentrale do
                join generators in db.Dbo.EnmsSichtKstErzeuger on (energyplants.KstStelle =  generators.UserKstUebergeordnet.Value)
                select generators.UserKstUebergeordnet.Value }
    query { for energyplants in db.Dbo.EnmsSichtKstEnergiezentrale do
            where ((%filterEZ 1 100) energyplants
                    && query1.Contains(energyplants.KstStelle))
            select (energyplants.Bezeichnung) }
    |> Seq.toArray
    |> Seq.length

//***************** Performing a union all ***********************//

let getEnergyplantDataKstByMandantPreFilter mandantfrom mandantto saktolist vuperiodefrom vuperiodeto ()=
    let query1 =
         query { for energyplants in db.Dbo.EnmsSichtKstEnergiezentrale do
                 join generators in db.Dbo.EnmsSichtKstErzeuger on (energyplants.KstStelle = generators.UserKstUebergeordnet.Value)
                 join measures in db.Dbo.EnmsTabelle on (generators.KstStelle = measures.KstStelle)
                 where ( measures.Saktobez |=| saktolist 
                 && measures.VuPeriode >= vuperiodefrom 
                 && measures.VuPeriode < vuperiodeto 
                 && energyplants.Mandant >= int16 mandantfrom 
                 && energyplants.Mandant < int16 mandantto ) 
                 select (measures.Saktobez,energyplants.KstStelle, measures.SumMenge, measures.Meh,measures.VuPeriode)}
    let query2 =
         query { for energyplants in db.Dbo.EnmsSichtKstEnergiezentrale do
                 join measures in db.Dbo.EnmsTabelle on (energyplants.KstStelle = measures.KstStelle)
                 where ( measures.Saktobez |=| saktolist 
                 && measures.VuPeriode >= vuperiodefrom 
                 && measures.VuPeriode < vuperiodeto 
                 && energyplants.Mandant >= int16 mandantfrom 
                 && energyplants.Mandant < int16 mandantto )  
                 select (measures.Saktobez,energyplants.KstStelle, measures.SumMenge, measures.Meh,measures.VuPeriode)}
    let res = query1.Concat(query2)
    res
    |> Seq.toArray
    |> Array.groupBy (fun (x,y,z,a,b) -> x)