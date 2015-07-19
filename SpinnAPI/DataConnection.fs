namespace SpinnAPI.DataRepository

open System
open System.Net
open System.Data
open System.Data.Linq
open System.Data.Linq.SqlClient
open Microsoft.FSharp.Data.TypeProviders
open Microsoft.FSharp.Linq

type DataConnection = SqlDataConnection<"Data Source=spinnreporting.cixmb8ypy409.eu-west-1.rds.amazonaws.com;Initial Catalog=Reporting;User ID=Spinnaker;Password=Copenhagen2014">

type DataQueries() =
    
    member x.FindUser(id, (db : DataConnection.ServiceTypes.SimpleDataContextTypes.Reporting)) = 
        query {
            for row in db.Users do
            where (row.Id = id)
            select row
        } |> Seq.exactlyOne

    member x.FindUserByToken(token, (db : DataConnection.ServiceTypes.SimpleDataContextTypes.Reporting)) = 
        query {
            for row in db.Users do
            where (row.Identifier = token)
            select row
        } |> Seq.exactlyOne

    member x.FindAllUsers(db : DataConnection.ServiceTypes.SimpleDataContextTypes.Reporting) = 
        query {
            for row in db.Users do
            select row
        } |> Seq.toList

    //QUALITYSCORE

    member x.FindQSByUserId(id, (db : DataConnection.ServiceTypes.SimpleDataContextTypes.Reporting)) = 
        query {
            for row in db.QualityScore do
            where (row.User_Id = id)
            select row
        } |> Seq.exactlyOne

    member x.FindUsersQSEmails(id, db : DataConnection.ServiceTypes.SimpleDataContextTypes.Reporting) = 
        query {
            for row in db.QS_Emails do
            where (row.QS_Id = id)
            select row.Email
        } |> Seq.toList

    member x.FindUsersQSBrands(id, db : DataConnection.ServiceTypes.SimpleDataContextTypes.Reporting) = 
        query {
            for row in db.QS_Brands do
            where (row.QS_Id = id)
            select row.Keyword
        } |> Seq.toList

    member x.FindQSEmailByMailAndQSId(id, email, db : DataConnection.ServiceTypes.SimpleDataContextTypes.Reporting) = 
        query {
            for row in db.QS_Emails do
            where (row.QS_Id = id && row.Email = email)
            select row
        } |> Seq.exactlyOne

    member x.FindQSBrandByKeywordAndQSId(id, keyword, db : DataConnection.ServiceTypes.SimpleDataContextTypes.Reporting) = 
        query {
            for row in db.QS_Brands do
            where (row.QS_Id = id && row.Keyword = keyword)
            select row
        } |> Seq.exactlyOne

