namespace SpinnReports.Models

open Newtonsoft.Json

[<CLIMutable>]
type Car = {
    Make : string
    Model : string
}

[<CLIMutable>]
type User = {
    Name : string
    Email : string
}
