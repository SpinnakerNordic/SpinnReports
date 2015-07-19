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