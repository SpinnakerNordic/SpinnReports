namespace SpinnAPI.Models

open Newtonsoft.Json

[<CLIMutable>]
type User = {
    Name : string
    Email : string
}

type QSR = {
    Score : decimal
    Top5Keys : string[]
    Top5Scores : decimal[]
    Bottom5Keys : string[]
    Bottom5Scores : decimal[]
}

type QualityScoreModel = {
    SUMARIZE : string
    TITLE : string
    NAMESTRING : string
    INFORMATIONLINE : string
    LOGOSOURCE : string
    SCORE2 : double
    SCORE3 : double
    CALLTOACTION : string
    BUTTONLINK : string
    BUTTONTEXT : string
    TOP5 : string
    BOTTOM5 : string
    IMAGETEXT : string
    IMAGELINK : string
    IMAGESOURCE : string
}

type TokenRouter = { Token:string}
