namespace SpinnAPI.Mail

open System.Net
open System.Net.Mail

type Mail() =
    
    member this.SendMail(message, subject, tomail) =

        let smtp = new SmtpClient()
        smtp.EnableSsl <- true
        
        let msg = new MailMessage("test@mail.dk", tomail)
        msg.Body <- message
        msg.Subject <- subject
       
        smtp.Send(msg)
    
    member this.ConstructQualityScoreReport() =
        let TITLE = "TITLE"
        let BANNERSOURCE = "http://googledrive.com/host/0B1HMPCqsxDmHflp1ODlWTzlIbzVqRlo5TjhZWXljQVR3Q24xMF9tRVZQR3VlaC1JMnhfVVk/logo.png"
        let DESCRIPTIONS = "Here's the Quality Score compared   with  7  days and 30 days back"
        let OUTTROTEXT = "Happy with the development? Let's make  it  even better!"
        let ACCOUNTNAME = "ACCOUNTNAME"

        let SCORE1 = 6.50
        let SCORE2 = 5.50
        let CHANGE newValue oldValue = 
            let change = newValue/oldValue
            if change >=0.0 then "<span style='color: #38761d'>" + change.ToString() + "%</span>" else "<span style='color: #cc0000'> -" + change.ToString() + "%</span>";

        let CHANGE1 = CHANGE SCORE1 SCORE2
        let CHANGE2 = CHANGE SCORE2 SCORE1
                
        
        let s = """<html><body style="margin: 0;mso-line-height-rule: exactly;padding: 0;min-width: 100%;background-color: #fbfbfb">"""+
                """<style type="text/css">body,.wrapper,.emb-editor-canvas{background-color:#fbfbfb}.border{background-color:#e9e9e9}h1{color:#565656}.wrapper h1{}.wrapper h1{font-family:sans-serif}h1{}.one-col h1{line-height:44px}.two-col h1{line-height:32px}.three-col h1{line-height:26px}.wrapper .one-col-feature h1{line-height:58px}@media only screen and (max-width: 620px){h1{line-height:44px !important}}h2{color:#555}.wrapper h2{}.wrapper h2{font-family:sans-serif}h2{}.one-col h2{line-height:34px}.two-col h2{line-height:26px}.three-col h2{line-height:22px}.wrapper .one-col-feature h2{line-height:50px}@media only screen and (max-width: 620px){h2{line-height:34px !important}}h3{color:#555}.wrapper h3{}.wrapper h3{font-family:sans-serif}h3{}.one-col h3{line-height:26px}.two-col h3{line-height:22px}.three-col h3{line-height:20px}.wrapper .one-col-feature h3{line-height:40px}@media only screen and (max-width: 620px){h3{line-height:26px !important}}p,ol,ul{color:#565656}.wrapper p,.wrapper ol,.wrapper ul{}.wrapper p,.wrapper ol,.wrapper ul{font-family:Georgia,serif}p,ol,ul{}.one-col p,.one-col ol,.one-col ul{line-height:25px;Margin-bottom:25px}.two-col p,.two-col ol,.two-col ul{line-height:23px;Margin-bottom:23px}.three-col p,.three-col ol,.three-col ul{line-height:21px;Margin-bottom:21px}.wrapper .one-col-feature p,.wrapper .one-col-feature ol,.wrapper .one-col-feature ul{line-height:32px}.one-col-feature blockquote p,.one-col-feature blockquote ol,.one-col-feature blockquote ul{line-height:50px}@media only screen and (max-width: 620px){p,ol,ul{line-height:25px !important;Margin-bottom:25px !important}}.image{color:#565656}.image{font-family:Georgia,serif}.wrapper a{color:#41637e}.wrapper a:hover{color:#30495c !important}.wrapper .logo div{color:#41637e}.wrapper .logo div{font-family:sans-serif}@media only screen and (min-width: 0){.wrapper .logo div{font-family:Avenir,sans-serif !important}}.wrapper .logo div a{color:#41637e}.wrapper .logo div a:hover{color:#41637e !important}.wrapper .one-col-feature p a,.wrapper .one-col-feature ol a,.wrapper .one-col-feature ul a{border-bottom:1px solid #41637e}.wrapper .one-col-feature p a:hover,.wrapper .one-col-feature ol a:hover,.wrapper .one-col-feature ul a:hover{color:#30495c !important;border-bottom:1px solid #30495c !important}.btn a{}.wrapper .btn a{}.wrapper .btn a{font-family:Georgia,serif}.wrapper .btn a{background-color:#41637e;color:#fff !important;outline-color:#41637e;text-shadow:0 1px 0 #3b5971}.wrapper .btn a:hover{background-color:#3b5971 !important;color:#fff !important;outline-color:#3b5971 !important}.preheader .title,.preheader .webversion,.footer .padded{color:#999}.preheader.title,.preheader .webversion,.footer .padded{font-family:Georgia,serif}.preheader .title a,.preheader .webversion a,.footer .padded a{color:#999}.preheader .title a:hover,.preheader .webversion a:hover,.footer .padded a:hover{color:#737373 !important}.footer .social .divider{color:#e9e9e9}.footer .social .social-text,.footer .social a{color:#999}.wrapper .footer .social .social-text,.wrapper .footer .social a{}.wrapper .footer .social .social-text,.wrapper .footer .social a{font-family:Georgia,serif}.footer .social .social-text,.footer .social a{}.footer .social .social-text,.footer .social a{letter-spacing:0.05em}.footer .social .social-text:hover,.footer .social a:hover{color:#737373 !important}.image .border{background-color:#c8c8c8}.image-frame{background-color:#dadada}.image-background{background-color:#f7f7f7}</style>"""+
                """<center class="wrapper" style="display: table;table-layout: fixed;width: 100%;min-width: 750px;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;background-color: #fbfbfb">""" +
                //line
                """<table class="gmail" style="border-collapse: collapse;border-spacing: 0;width: 750px;min-width: 750px"><tbody><tr><td style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px">&nbsp;</td></tr></tbody></table> """ +

                """<table class="preheader centered" style="border-collapse: collapse;border-spacing: 0;Margin-left: auto;Margin-right: auto"><tbody><tr> """ +
                """<td style="padding: 0;vertical-align: top"><table style="border-collapse: collapse;border-spacing: 0;width: 602px"><tbody><tr> """ +
                """<td class="title" style="padding: 0;vertical-align: top;padding-top: 10px;padding-bottom: 12px;font-size: 12px;line-height: 21px;text-align: left;color: #999;font-family: Georgia,serif">"""+

                TITLE +

                """</td></tr></tbody></table></td></tr></tbody></table>""" +
                """<table class="header centered" style="border-collapse: collapse;border-spacing: 0;Margin-left: auto;Margin-right: auto;width: 602px"> """ +
                """<tbody><tr><td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&nbsp;</td></tr> """ +

                """<tr><td class="logo" style="padding: 32px 0;vertical-align: top;mso-line-height-rule: at-least"><div class="logo-center" style="font-size: 26px;font-weight: 700;letter-spacing: -0.02em;line-height: 32px;color: #41637e;font-family: sans-serif;text-align: center" align="center" id="emb-email-header"><img style="border: 0;-ms-interpolation-mode: bicubic;display: block;Margin-left: auto;Margin-right: auto;max-width: 300px" src=""" +

                BANNERSOURCE +

                """ alt="" width="300" height="65" /></div></td></tr></tbody></table> """ +
                """ <table class="border" style="border-collapse: collapse;border-spacing: 0;font-size: 1px;line-height: 1px;background-color: #e9e9e9;Margin-left: auto;Margin-right: auto"    width="602">""" +
                """ <tbody><tr><td style="padding: 0;vertical-align: top">&#8203;</td></tr></tbody></table> """ +
                """ <table class="centered" style="border-collapse: collapse;border-spacing: 0;Margin-left: auto;Margin-right: auto"> """ +
                """ <tbody><tr><td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td> """ +
                """ <td style="padding: 0;vertical-align: top"> """ +
                """ <table class="one-col" style="border-collapse: collapse;border-spacing: 0;Margin-left: auto;Margin-right: auto;width: 600px;background-color: #ffffff;font-size: 14px;table-layout: fixed">  """ +
                """ <tbody><tr><td class="column" style="padding: 0;vertical-align: top;text-align: left"> """ +
                """ <div><div class="column-top" style="font-size: 32px;line-height: 32px">&nbsp;</div></div> """ +
                """ <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%"> """ +
                """ <tbody><tr><td class="padded" style="padding: 0;vertical-align: top;padding-left: 32px;padding-right: 32px;word-break: break-word;word-wrap: break-word"> """ +
                """ """ +
                """ <h1 style="Margin-top: 0;color: #565656;font-weight: 700;font-size: 36px;Margin-bottom: 18px;font-family: sans-serif;line-height: 44px;text-align: center">""" +
                
                TITLE +

                """</h1><p style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 16px;line-height: 25px;Margin-bottom: 25px;text-align: center">""" +
                """Hi """ +
                
                ACCOUNTNAME + "!" +


                """</p><p style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 16px;line-height: 25px;Margin-bottom: 24px;text-align: center">"""+
                
                DESCRIPTIONS +
                
                """ </p> </td> </tr> </tbody></table><div class="column-bottom" style="font-size: 8px;line-height: 8px">&nbsp;</div> """ +
                """ </td> </tr> </tbody></table> </td> """ +
                """ <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td> """ +
                """ </tr> </tbody></table> <table class="border" style="border-collapse: collapse;border-spacing: 0;font-size: 1px;line-height: 1px;background-color: #e9e9e9;Margin-left: auto;Margin-right: auto"    width="602">   """ +
                """ <tbody><tr class="border" style="font-size: 1px;line-height: 1px;background-color: #e9e9e9;height: 1px"> """ +
                """ <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td> """ +
                """ <td style="padding: 0;vertical-align: top;line-height: 1px">&#8203;</td> """ +
                """ <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td> """ +
                """ </tr> </tbody></table><table class="centered" style="border-collapse: collapse;border-spacing: 0;Margin-left: auto;Margin-right: auto"> """ +
                """ <tbody><tr> <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td> """ +
                """ <td style="padding: 0;vertical-align: top"> """ +
                """ <table class="one-col-feature" style="border-collapse: collapse;border-spacing: 0;background-color: #ffffff;font-size: 14px;table-layout: fixed;Margin-left: auto;Margin-right:     auto">  """ +
                """ <tbody><tr><td class="column" style="padding: 0;vertical-align: top;text-align: center;width: 600px"> """ +
                """ <div><div class="column-top" style="font-size: 36px;line-height: 36px">&nbsp;</div></div> """ +
                """ <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%"> """ +
                """ <tbody><tr><td class="padded" style="padding: 0;vertical-align: top;padding-left: 32px;padding-right: 32px;word-break: break-word;word-wrap: break-word"> """ +
                """ <h1 style="Margin-top: 0;color: #565656;font-weight: normal;font-size: 52px;Margin-bottom: 22px;font-family: sans-serif;text-align: center;line-height: 58px">""" +
                
                SCORE1.ToString() +

                """</h1> </td> </tr></tbody></table> """ +
                """ <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%"> """ +
                """ <tbody><tr><td class="padded" style="padding: 0;vertical-align: top;padding-left: 32px;padding-right: 32px;word-break: break-word;word-wrap: break-word"> """ +
                """ <table class="divider" style="border-collapse: collapse;border-spacing: 0;width: 100%"><tbody><tr><td class="inner" style="padding: 0;vertical-align: top;padding-bottom: 32px"          align="center">  """ +
                """ <table style="border-collapse: collapse;border-spacing: 0;background-color: #e9e9e9;font-size: 2px;line-height: 2px;width: 60px"> """ +
                """ <tbody><tr><td style="padding: 0;vertical-align: top">&nbsp;</td></tr></tbody></table></td></tr></tbody></table></td></tr> </tbody></table> """ +
                """ <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%"> """ +
                """ <tbody><tr><td class="padded" style="padding: 0;vertical-align: top;padding-left: 32px;padding-right: 32px;word-break: break-word;word-wrap: break-word"> """ +
                """ """ +
                """ <p style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 21px;line-height: 32px;Margin-bottom: 32px;text-align: center"><strong style="font-weight:              bold">Week-"on-"Week</"strong><br /> """ +

                CHANGE1 + 

                """</p><p style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 21px;line-height: 32px;Margin-bottom: 32px;text-align: center"><strong style="font-weight:           bold">Month-"on-"Month</"strong><br /> """ +

                CHANGE2 +

                """</p></td></tr> </tbody></table><table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%"> """ +
                """ <tbody><tr> <td class="padded" style="padding: 0;vertical-align: top;padding-left: 32px;padding-right: 32px;word-break: break-word;word-wrap: break-word"> """ +
                """ <table class="divider" style="border-collapse: collapse;border-spacing: 0;width: 100%"><tbody><tr><td class="inner" style="padding: 0;vertical-align: top;padding-bottom: 32px"          align="center">  """ +
                """ <table style="border-collapse: collapse;border-spacing: 0;background-color: #e9e9e9;font-size: 2px;line-height: 2px;width: 60px"> """ +
                """ <tbody><tr><td style="padding: 0;vertical-align: top">&nbsp;</td></tr> """ +
                """ </tbody></table></td></tr></tbody></table></td></tr></tbody></table> """ +
                """ <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%"> """ +
                """ <tbody><tr><td class="padded" style="padding: 0;vertical-align: top;padding-left: 32px;padding-right: 32px;word-break: break-word;word-wrap: break-word"> """ +
                """ <p style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 21px;line-height: 32px;Margin-bottom: 32px;text-align: center">""" +
                
                OUTTROTEXT +
                
                """ </p></td> </tr></tbody></table><table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%"> """ +
                """ <tbody><tr><td class="padded" style="padding: 0;vertical-align: top;padding-left: 32px;padding-right: 32px;word-break: break-word;word-wrap: break-word"> """ +
                """ <div class="btn" style="Margin-bottom: 32px;padding: 2px;text-align: center"> """ +
                """ <![if !mso]><a style="border: 1px solid #ffffff;display: inline-block;font-size: 13px;font-weight: bold;line-height: 15px;outline-style: solid;outline-width: 2px;padding: 10px      30px;text-   align:" ""center;text-decoration: none !important;transition: all .2s;color: #fff !important;font-family: Georgia,serif;background-color: #41637e;outline-color: #41637e;text-shadow: 0 1px 0 #3b5971" href="http://app.spinntools.com/">Optimizing your Adwords Quality Score</a><![endif]> """ +
                """ <!--[if mso]><v:rect xmlns:v="urn:schemas-microsoft-com:vml" href="http://app.spinntools.com/" style="width:328px" fillcolor="#41637E" strokecolor="#41637E"        strokeweight="6px"><v:stroke linestyle="thinthin"></v:stroke><v:textbox style="mso-fit-shape-to-text:t" inset="0px,7px,0px,7px"><center style="font-          size:13px;line-"height:15px;color:#FFFFFF;font-"family:Georgia,serif;font-"weight:bold;mso-line-height-rule:exactly;mso-text-raise:0px">Optimizing your Adwords Quality Score</  center></v:textbox></v:rect><!"[endif]--></div> """ +
                """ """ +
                """ </td> """ +
                """ </tr> """ +
                """ </tbody></table> """ +
                """ """ +
                """ <div class="column-bottom" style="font-size: 4px;line-height: 4px">&nbsp;</div> """ +
                """ </td> """ +
                """ </tr> """ +
                """ </tbody></table> """ +
                """ </td> """ +
                """ <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td> """ +
                """ </tr> """ +
                """ </tbody></table> """ +
                """ """ +
                """ <table class="border" style="border-collapse: collapse;border-spacing: 0;font-size: 1px;line-height: 1px;background-color: #e9e9e9;Margin-left: auto;Margin-right: auto"    width="602">   """ +
                """ <tbody><tr class="border" style="font-size: 1px;line-height: 1px;background-color: #e9e9e9;height: 1px"> """ +
                """ <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td> """ +
                """ <td style="padding: 0;vertical-align: top;line-height: 1px">&#8203;</td> """ +
                """ <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td> """ +
                """ </tr> """ +
                """ </tbody></table> """ +
                """ """ +
                """ <table class="centered" style="border-collapse: collapse;border-spacing: 0;Margin-left: auto;Margin-right: auto"> """ +
                """ <tbody><tr> """ +
                """ <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td> """ +
                """ <td style="padding: 0;vertical-align: top"> """ +
                """ <table class="two-col" style="border-collapse: collapse;border-spacing: 0;Margin-left: auto;Margin-right: auto;width: 600px;background-color: #ffffff;font-size: 14px;table-layout:      fixed">  """ +
                """ <tbody><tr> """ +
                """ <td class="column first" style="padding: 0;vertical-align: top;text-align: left;width: 300px"> """ +
                """ <div><div class="column-top" style="font-size: 32px;line-height: 32px">&nbsp;</div></div> """ +
                """ <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%"> """ +
                """ <tbody><tr> """ +
                """ <td class="padded" style="padding: 0;vertical-align: top;padding-left: 32px;padding-right: 32px;word-break: break-word;word-wrap: break-word"> """ +
                """ """ +
                """ <h3 style="Margin-top: 0;color: #555;font-weight: normal;font-size: 16px;line-height: 22px;Margin-bottom: 14px;font-family: sans-serif;text-align: center"><strong style="font- weight: bold">Top" ""5 keywords</strong></h3><p style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 14px;line-height: 23px;Margin-bottom: 23px;text- align: center">""" +
                
                //TopFive() +

                """</p> """ +
                """ """ +
                """ </td> """ +
                """ </tr> """ +
                """ </tbody></table> """ +
                """ """ +
                """ <div class="column-bottom" style="font-size: 9px;line-height: 9px">&nbsp;</div> """ +
                """ </td> """ +
                """ <td class="column second" style="padding: 0;vertical-align: top;text-align: left;width: 300px"> """ +
                """ <div><div class="column-top" style="font-size: 32px;line-height: 32px">&nbsp;</div></div> """ +
                """ <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%"> """ +
                """ <tbody><tr> """ +
                """ <td class="padded" style="padding: 0;vertical-align: top;padding-left: 32px;padding-right: 32px;word-break: break-word;word-wrap: break-word"> """ +
                """ """ +
                """ <h3 style="Margin-top: 0;color: #555;font-weight: normal;font-size: 16px;line-height: 22px;Margin-bottom: 14px;font-family: sans-serif;text-align: center"><strong style="font- weight: bold">Bottom 5 keywords</strong></h3><p style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 14px;line-height: 23px;Margin-bottom: 23px;text-  align:     center">""" +
                
                //BottomFive() +

                """</p> """ +
                """ """ +
                """ </td> """ +
                """ </tr> """ +
                """ </tbody></table> """ +
                """ <div class="column-bottom" style="font-size: 9px;line-height: 9px">&nbsp;</div> """ +
                """ </td> """ +
                """ </tr> """ +
                """ </tbody></table> """ +
                """ </td> """ +
                """ <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td> """ +
                """ </tr> """ +
                """ </tbody></table> """ +
                """ <table class="border" style="border-collapse: collapse;border-spacing: 0;font-size: 1px;line-height: 1px;background-color: #e9e9e9;Margin-left: auto;Margin-right: auto"    width="602">   """ +
                """ <tbody><tr class="border" style="font-size: 1px;line-height: 1px;background-color: #e9e9e9;height: 1px"> """ +
                """ <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td> """ +
                """ <td style="padding: 0;vertical-align: top;line-height: 1px">&#8203;</td> """ +
                """ <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td> """ +
                """ </tr> """ +
                """ </tbody></table> """ +
                """ """ +
                """ <table class="centered" style="border-collapse: collapse;border-spacing: 0;Margin-left: auto;Margin-right: auto"> """ +
                """ <tbody><tr> """ +
                """ <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td> """ +
                """ <td style="padding: 0;vertical-align: top"> """ +
                """ <table class="one-col" style="border-collapse: collapse;border-spacing: 0;Margin-left: auto;Margin-right: auto;width: 600px;background-color: #ffffff;font-size: 14px;table-layout:      fixed">  """ +
                """ <tbody><tr> """ +
                """ <td class="column" style="padding: 0;vertical-align: top;text-align: left"> """ +
                """ <div><div class="column-top" style="font-size: 32px;line-height: 32px">&nbsp;</div></div> """ +
                """ <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%"> """ +
                """ <tbody><tr> """ +
                """ <td class="padded" style="padding: 0;vertical-align: top;padding-left: 32px;padding-right: 32px;word-break: break-word;word-wrap: break-word"> """ +
                """ """ +
                """ <p style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 16px;line-height: 25px;Margin-bottom: 25px;text-align: center">Current QS Graph</p> """ +
                """ """ +
                """ </td> """ +
                """ </tr> """ +
                """ </tbody></table> """ +
                """ """ +
                """ <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%"> """ +
                """ <tbody><tr> """ +
                """ <td class="padded" style="padding: 0;vertical-align: top;padding-left: 32px;padding-right: 32px;word-break: break-word;word-wrap: break-word"> """ +
                """ """ +
                """ <h1 style="Margin-top: 0;color: #565656;font-weight: 700;font-size: 36px;Margin-bottom: 24px;font-family: sans-serif;line-height: 44px;text-align: center">""" +

                """<img src='cid:graph' alt='' /> """ + //IMAGE HERE

                """ </td> """ +
                """ </tr> """ +
                """ </tbody></table> """ +
                """ """ +
                """ <div class="column-bottom" style="font-size: 8px;line-height: 8px">&nbsp;</div> """ +
                """ </td> """ +
                """ </tr> """ +
                """ </tbody></table> """ +
                """ </td> """ +
                """ <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td> """ +
                """ </tr> """ +
                """ </tbody></table> """ +
                """ """ +
                """ <table class="border" style="border-collapse: collapse;border-spacing: 0;font-size: 1px;line-height: 1px;background-color: #e9e9e9;Margin-left: auto;Margin-right: auto"    width="602">   """ +
                """ <tbody><tr><td style="padding: 0;vertical-align: top">&#8203;</td></tr> """ +
                """ </tbody></table> """ +
                """ """ +
                """ <div class="spacer" style="font-size: 1px;line-height: 32px;width: 100%">&nbsp;</div> """ +
                """ </td> """ +
                """ <table style="border-collapse: collapse;border-spacing: 0;width: 602px"> """ +
                """ <tbody><tr> """ +
                """ <td class="title" style="padding: 0;vertical-align: top;padding-top: 10px;padding-bottom: 12px;font-size: 12px;line-height: 21px;text-align: left;color: #999;font-family:          Georgia,serif">For more information about this product go to <a href="http://app.spinntools.com/">your spinntools account</a>. Have a nice day!</td> """ +
                """ <td class="title" style="padding: 0;vertical-align: top;padding-top: 10px;padding-bottom: 12px;font-size: 12px;line-height: 21px;text-align: left;color: #999;font-family:          Georgia,serif"><a href="http://app.spinntools.com/">Unsubscribe</a></td> """ +
                """ </tr> """ +
                """ </tbody></table> """ +
                """ </tr> """ +
                """ </tbody></table> """ +
                """ </td> """ +
                """ </tr> """ +
                """ <tr><td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&nbsp;</td></tr> """ +
                """ <tr> """ +
                """ <td style="padding: 0;vertical-align: top"> """ +
                """ <table style="border-collapse: collapse;border-spacing: 0"> """ +
                """ <tbody><tr> """ +
                """ <td class="address" style="padding: 0;vertical-align: top;width: 250px;padding-top: 32px;padding-bottom: 64px"> """ +
                """ <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%"> """ +
                """ <tbody><tr> """ +
                """ <td class="padded" style="padding: 0;vertical-align: top;padding-left: 0;padding-right: 10px;word-break: break-word;word-wrap: break-word;text-align: left;font-size: 12px;line-    height: 20px;color: #999;font-family: Georgia,serif"> """ +
                """ """ +
                """ </td> """ +
                """ </tr> """ +
                """ </tbody></table> """ +
                """ </td> """ +
                """ <td class="subscription" style="padding: 0;vertical-align: top;width: 350px;padding-top: 32px;padding-bottom: 64px"> """ +
                """ <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%"> """ +
                """ <tbody><tr> """ +
                """ <td class="padded" style="padding: 0;vertical-align: top;padding-left: 10px;padding-right: 0;word-break: break-word;word-wrap: break-word;font-size: 12px;line-height: 20px;color:        ""#999;font-"family: Georgia,serif;text-align: right"> """ +
                """ """ +
                """ <div> """ +
                """ <span class="block"> """ +
                """ """ +
                """ </span> """ +
                """ </div> """ +
                """ </td> """ +
                """ </tr> """ +
                """ </tbody></table> """ +
                """ </td> """ +
                """ </tr> """ +
                """ </tbody></table> """ +
                """ </td> """ +
                """ </tr> """ +
                """ </tbody></table> """ +
                """ </center> """ +
                """</body></html> """
            
        s