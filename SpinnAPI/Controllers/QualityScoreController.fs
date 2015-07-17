namespace SpinnAPI.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Net.Http
open System.Web.Http
open System.Web.Mvc
open System.Web.Mvc.Ajax

open SpinnAPI.Models
open SpinnAPI.DataRepository

/// Retrieves values.
type QualityScoreController(queries : DataQueries) =
    inherit Controller()
    new () = new QualityScoreController(DataQueries())

    member this.Index () = 
        let db = DataConnection.GetDataContext()

        let (?<-) (viewData:ViewDataDictionary) (name:string) (value:'T) =
            viewData.Add(name, box value)
    
        this.ViewData?PageTitle <- "Your QualityScore Report Settings"

        this.ViewData?EmailList <- queries.FindUsersReportEmails(1, db)
        this.ViewData?KeywordList <- queries.FindUsersReportBrands(1, db)
        this.ViewData?UserID <- 1

        this.View()

    member this.DeleteEmail(email, id) =
        let db = DataConnection.GetDataContext()

        0

        


