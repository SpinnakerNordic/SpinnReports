namespace SpinnAPI.Models

open Newtonsoft.Json

[<CLIMutable>]
type User = {
    Name : string
    Email : string
}

type QualityScoreModel = {
    SUMARIZE : string
    TITLE : string
    NAMESTRING : string
    INFORMATIONLINE : string
    LOGOSOURCE : string
    SCORE1 : double
    SCORE2 : double
    SCORE3 : double
    CALLTOACTION : string
    BUTTONLINK : string
    BUTTONTEXT : string
    TOP5 : string
    BOTTOM5 : string
    TABLETOP5 : (string[] * string[])
    TABLEBOTTOM5 : (string[] * string[])
    IMAGETEXT : string
    IMAGELINK : string
    IMAGESOURCE : string
}
