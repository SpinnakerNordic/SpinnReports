namespace SpinnAPI.DataRepository

open System
open System.Net
open System.Data
open System.Data.Linq
open System.Data.Linq.SqlClient
open Microsoft.FSharp.Data.TypeProviders
open Microsoft.FSharp.Linq

open SpinnAPI.Models

type DataConnection = SqlDataConnection<"Data Source=spinnreporting.cixmb8ypy409.eu-west-1.rds.amazonaws.com;Initial Catalog=Reporting;User ID=Spinnaker;Password=Copenhagen2014">

type DataQueries() =
    
    ///find the database user from a given id
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

    member x.MakeModel(id, date, db : DataConnection.ServiceTypes.SimpleDataContextTypes.Reporting) : QSR = 
        query {
            for row in db.QS_Scores do
            where (row.QS_Id = id && row.Date = date)
            select (if (row.HasKeysScores) then {Score = row.Score; 
                    Top5Keys = [|row.Top1_key; row.Top2_key; row.Top3_key; row.Top4_key; row.Top5_key;|]; 
                    Top5Scores = [|row.Top1_score; row.Top2_score; row.Top3_score; row.Top4_score; row.Top5_score;|] |> Seq.cast<decimal> |> Seq.toArray; 
                    Bottom5Keys = [|row.Bottom1_key; row.Bottom2_key; row.Bottom3_key; row.Bottom4_key; row.Bottom5_key;|]; 
                    Bottom5Scores = [|row.Bottom1_score; row.Bottom2_score; row.Bottom3_score; row.Bottom4_score; row.Bottom5_score;|] |> Seq.cast<decimal> |> Seq.toArray}
                else {Score = row.Score; 
                    Top5Keys = Array.empty ; 
                    Top5Scores = Array.empty; 
                    Bottom5Keys = Array.empty; 
                    Bottom5Scores = Array.empty})
        } |> Seq.exactlyOne

