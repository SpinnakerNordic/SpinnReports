namespace SpinnAPI.Mail

open System.Net
open System.Net.Mail
open System.Net.Http
open System.Web.Http

type Mail() =
    
    member this.SendMail(message, subject, tomail) =

        let smtp = new SmtpClient()
        smtp.EnableSsl <- true
        
        let msg = new MailMessage("test@mail.dk", tomail)
        msg.Body <- message
        msg.Subject <- subject
       
        smtp.Send(msg)

type ScriptController(mail) =
    inherit ApiController()
    new() = new ScriptController(Mail())

    member this.GetInfo() =
        0
    
