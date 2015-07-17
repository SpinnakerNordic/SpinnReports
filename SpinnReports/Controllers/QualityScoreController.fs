namespace SpinnReports.Controllers
open System
open System.Collections.Generic
open System.Linq
open System.Net.Http
open System.Web.Http

open SpinnReports.Models
open SpinnReports.DataRepository

/// Retrieves values.
type QualityScoreController(queries : DataQueries) =
    inherit ApiController()
    new () = new QualityScoreController(DataQueries())
    
    member x.Get() =
        let db = DataConnection.GetDataContext()
        
        let respone = new HttpResponseMessage()
        respone.Content = QSSettings.html
        


