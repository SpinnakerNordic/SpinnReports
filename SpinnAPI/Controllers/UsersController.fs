namespace SpinnAPI.Controllers
open System
open System.Collections.Generic
open System.Linq
open System.Net.Http
open System.Web.Http

open SpinnAPI.Models
open SpinnAPI.DataRepository

/// Retrieves values.
type UsersController(queries : DataQueries) =
    inherit ApiController()
    new () = new UsersController(DataQueries())
    
    /// POST /api/users/{UserModel}
    member x.Post([<FromBody>] value:User) = 
        
        use db = DataConnection.GetDataContext()

        let newtoken =
            let chars = "ABCDEFGHIJKLMNOPQRSTUVWUXYZ0123456789abcdefghijklmnopqrstuvwxyz+_)(*&^%$#@!~`|<>"
            let charsLen = chars.Length
            let random = System.Random()
            let randomChars = [|for i in 0..24 -> chars.[random.Next(charsLen)]|]
            new String(randomChars)

        let newUser = new DataConnection.ServiceTypes.User(Name = value.Name, Identifier = newtoken, Email = value.Email)
        
        db.User.InsertOnSubmit(newUser)
        db.DataContext.SubmitChanges() |> ignore
        
        newtoken

    member x.Delete(id:int) =
        let db = DataConnection.GetDataContext()

        let user = queries.FindUser(id, db)

        db.User.DeleteOnSubmit(user)
        db.DataContext.SubmitChanges() |> ignore

    member x.Get() =
        let db = DataConnection.GetDataContext()
        
        queries.FindAllUsers(db)


