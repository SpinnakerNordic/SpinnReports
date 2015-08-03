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

    ///Returns setting view for a specific user. 
    member this.Settings(token) = 
        use db = DataConnection.GetDataContext()

        let user = queries.FindUserByToken(token, db)
        let QS = queries.FindQSByUserId(user.Id, db)

        this.ViewData.Add("Title", "Your QualityScore Report Settings")
        this.ViewData.Add("EmailList", queries.FindUsersQSEmails(QS.Id, db))
        this.ViewData.Add("KeywordList", queries.FindUsersQSBrands(QS.Id, db))
        this.ViewData.Add("UserID", user.Id)
        this.ViewData.Add("QSID", QS.Id)
        this.ViewData.Add("Token", token)

        this.View()

    member this.Report(token, date) =
        use db = DataConnection.GetDataContext()

        let user = queries.FindUserByToken(token, db)
        let QS = queries.FindQSByUserId(user.Id, db)

        let qsr = queries.MakeModel(QS.Id, date, db)

        let model = {
            SUMARIZE = "This is a summary"; 
            TITLE = "this be a title"; 
            NAMESTRING = "Oh no you didn't!"; 
            INFORMATIONLINE = "We can try it again"; 
            LOGOSOURCE = "http://googledrive.com/host/0B1HMPCqsxDmHflp1ODlWTzlIbzVqRlo5TjhZWXljQVR3Q24xMF9tRVZQR3VlaC1JMnhfVVk/logo.png";
            SCORE2 = 5.4; 
            SCORE3 = 5.7;
            CALLTOACTION = "CLICK ME!"; 
            BUTTONLINK = sprintf "/Settings?%s" token; 
            BUTTONTEXT = "I SAID CLICK ME!"; 
            TOP5 = "Top 5 losers"; 
            BOTTOM5 = "Bottom 5 winners";
            IMAGETEXT = "imagetext?"; 
            IMAGELINK = "http://www.example.com/"; 
            IMAGESOURCE = "http://cdn.desktopwallpapers4.me/wallpapers/comics/1920x1080/2/19476-superheroes-1920x1080-comic-wallpaper.jpg"; 
        }

        this.ViewData.Add("SCORE1", qsr.Score)
        this.ViewData.Add("TOP5Keys", qsr.Top5Keys)
        this.ViewData.Add("TOP5Scores", qsr.Top5Scores)
        this.ViewData.Add("BOTTOM5Keys", qsr.Bottom5Keys)
        this.ViewData.Add("BOTTOM5Scores", qsr.Bottom5Scores)

        this.ViewData.Add("Token", token)

        this.ViewData.Add("SUMARIZE", model.SUMARIZE )
        this.ViewData.Add("TITLE", model.TITLE)
        this.ViewData.Add("NAMESTRING", model.NAMESTRING)
        this.ViewData.Add("INFORMATIONLINE", model.INFORMATIONLINE)
        this.ViewData.Add("LOGOSOURCE", model.LOGOSOURCE)
        this.ViewData.Add("SCORE2", model.SCORE2)
        this.ViewData.Add("SCORE3", model.SCORE3)
        this.ViewData.Add("CALLTOACTION", model.CALLTOACTION)
        this.ViewData.Add("BUTTONLINK", model.BUTTONLINK)
        this.ViewData.Add("BUTTONTEXT", model.BUTTONTEXT)
        this.ViewData.Add("TOP5", model.TOP5)
        this.ViewData.Add("BOTTOM5", model.BOTTOM5)
        this.ViewData.Add("IMAGETEXT", model.IMAGETEXT)
        this.ViewData.Add("IMAGELINK", model.IMAGELINK)
        this.ViewData.Add("IMAGESOURCE", model.IMAGESOURCE)

        this.View()
        
    member this.PostEmail(email: string,  qsid : int, token : string) =

        if email.Contains('@') && email.Contains('.') && email.Length > 4 then
            use db = DataConnection.GetDataContext()
        
            let newmail = new DataConnection.ServiceTypes.QS_Emails(QS_Id = qsid, Email = email)

            db.QS_Emails.InsertOnSubmit(newmail)
            db.DataContext.SubmitChanges()

        let tokenRouter : TokenRouter= { Token = token; }
        this.RedirectToAction("Settings", tokenRouter);

    member this.DeleteEmail(email : string, qsid : int, token : string) =
        use db = DataConnection.GetDataContext()

        let newmail = queries.FindQSEmailByMailAndQSId(qsid, email, db)

        db.QS_Emails.DeleteOnSubmit(newmail)
        db.DataContext.SubmitChanges()

        let tokenRouter : TokenRouter= { Token = token }
        this.RedirectToAction("Settings", tokenRouter);

    member this.PostBrand(keyword: string,  qsid : int, token : string) =
        use db = DataConnection.GetDataContext()
        
        let newbrand = new DataConnection.ServiceTypes.QS_Brands(QS_Id = qsid, Keyword = keyword)

        db.QS_Brands.InsertOnSubmit(newbrand)
        db.DataContext.SubmitChanges()

        let tokenRouter : TokenRouter= { Token = token }
        this.RedirectToAction("Settings", tokenRouter);

    member this.DeleteBrand(keyword : string, qsid : int, token : string) =
        use db = DataConnection.GetDataContext()

        let newmail = queries.FindQSBrandByKeywordAndQSId(qsid, keyword, db)

        db.QS_Brands.DeleteOnSubmit(newmail)
        db.DataContext.SubmitChanges()

        let tokenRouter : TokenRouter= { Token = token }
        this.RedirectToAction("Settings", tokenRouter);

    member this.WordpressDashboard() =
        this.Redirect("http://app.spinntools.com/qsr-dashboard/")
