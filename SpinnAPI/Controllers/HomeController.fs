namespace SpinnAPI.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Web
open System.Web.Mvc
open System.Web.Mvc.Ajax

type HomeController() =
    inherit Controller()
    
    member this.Index () = 
        this.View()

    member this.Spinntools() =
        this.Redirect("http://app.spinntools.com/")

    member this.Tokens() =
        use db = SpinnAPI.DataRepository.DataConnection.GetDataContext()
        
        let users = SpinnAPI.DataRepository.DataQueries().FindAllUsers(db)
        db.Connection.Close()

        this.ViewData.Add("Users", users)

        this.View()