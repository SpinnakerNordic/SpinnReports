﻿namespace SpinnAPI.DataRepository

open System
open System.Net
open System.Data
open System.Data.Linq
open System.Data.Linq.SqlClient
open Microsoft.FSharp.Data.TypeProviders
open Microsoft.FSharp.Linq

type DataConnection = SqlDataConnection<"Data Source=spinnreporting.cixmb8ypy409.eu-west-1.rds.amazonaws.com;Initial Catalog=Spinntools;User ID=Spinnaker;Password=Copenhagen2014">

type DataQueries() =
    
    member x.FindUser(id, (db : DataConnection.ServiceTypes.SimpleDataContextTypes.Spinntools)) = 
        query {
            for row in db.User do
            where (row.Id = id)
            select row
        } |> Seq.exactlyOne

    member x.FindAllUsers(db : DataConnection.ServiceTypes.SimpleDataContextTypes.Spinntools) = 
        query {
            for row in db.User do
            select row
        } |> Seq.toList

    member x.FindUsersReportEmails(id, db : DataConnection.ServiceTypes.SimpleDataContextTypes.Spinntools) = 
        query {
            for row in db.ReportEmail do
            where (row.User_Id = id)
            select row.Email
        } |> Seq.toList

    member x.FindUsersReportBrands(id, db : DataConnection.ServiceTypes.SimpleDataContextTypes.Spinntools) = 
        query {
            for row in db.ReportBrandword do
            where (row.User_Id = id)
            select row.Keyword
        } |> Seq.toList