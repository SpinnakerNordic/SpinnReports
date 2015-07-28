namespace SpinnAPI.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Net.Http
open System.Web.Http
open System.IO
open System.Net
open System.Security.Cryptography
open System.Text

/// Retrieves values.
type TestController() =
    inherit ApiController()
    
    member this.Post() = 
        this.Ok
        
    member x.Get() = 
        200
    
    member this.Update() = 
        this.Ok
    
    member this.Delete() = 
        this.Ok

    