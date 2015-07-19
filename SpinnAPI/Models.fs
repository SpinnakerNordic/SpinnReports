namespace SpinnAPI.Models

open Newtonsoft.Json

[<CLIMutable>]
type User = {
    Name : string
    Email : string
}
