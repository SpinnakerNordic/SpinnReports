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

type QualityScoreReport() =

    member this.Contruct(model : QualityScoreModel) =

        let change newValue oldValue = 
            let change = ((newValue/oldValue)-1.0)*100.0
            match change with
            |i when i >= 10.0 -> "<span style='color: #38761d'>" + (change).ToString().Substring(0, 5) + "%</span>" // [ 10.. N  ]  11.62
            |i when i >= 0.0 -> "<span style='color: #38761d'>" + (change).ToString().Substring(0, 4) + "%</span>"  // [ 0 .. 10 [  0.85
            |i when i < -10.0 -> "<span style='color: #cc0000'>" + (change).ToString().Substring(0, 6) + "%</span>" // [-N ..-10 ] -15.92
            |i -> "<span style='color: #cc0000'>" + (change).ToString().Substring(0, 5) + "%</span>"                // ] 0 ..-10 [ -5.33

        let table (keywords : string [], scores : string []) =
            """<table style="undefined;table-layout: fixed; width: 100%">
            <colgroup>
            <col style="width: 50%">
            <col style="width: 50%">
            </colgroup>
              <tr>
                <th style="background-color:#41637e; color:#ffffff; font-weight:bold; font-family: Georgia,serif; font-size:14px; padding: 5px 2px; text-align: center">Keyword</th>
                <th style="background-color:#41637e; color:#ffffff; font-weight:bold; font-family: Georgia,serif; font-size:14px; padding: 5px 2px; text-align: center">QualityScore</th>
              </tr>
              <tr>
                <td style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 14px;line-height: 23px;Margin-bottom: 23px;text-align: center">"""+ 
                keywords.[0] +
                """</td>
                <td style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 14px;line-height: 23px;Margin-bottom: 23px;text-align: center">"""+ 
                scores.[0] +
                """</td>
              </tr>
              <tr>
                <td style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 14px;line-height: 23px;Margin-bottom: 23px;text-align: center">"""+ 
                keywords.[1] +
                """</td>
                <td style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 14px;line-height: 23px;Margin-bottom: 23px;text-align: center">"""+ 
                scores.[1] +
                """</td>
              </tr>
              <tr>
                <td style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 14px;line-height: 23px;Margin-bottom: 23px;text-align: center">"""+ 
                keywords.[2] +
                """</td>
                <td style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 14px;line-height: 23px;Margin-bottom: 23px;text-align: center">"""+ 
                scores.[2] +
                """</td>
              </tr>
              <tr>
                <td style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 14px;line-height: 23px;Margin-bottom: 23px;text-align: center">"""+ 
                keywords.[3] +
                """</td>
                <td style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 14px;line-height: 23px;Margin-bottom: 23px;text-align: center">"""+ 
                scores.[3] +
                """</td>
              </tr>
              <tr>
                <td style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 14px;line-height: 23px;Margin-bottom: 23px;text-align: center">"""+ 
                keywords.[4] +
                """</td>
                <td style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 14px;line-height: 23px;Margin-bottom: 23px;text-align: center">"""+ 
                scores.[4] +
                """</td>
              </tr>
            </table>"""
        
        let WOW = change model.SCORE1 model.SCORE2
        let MOM = change model.SCORE1 model.SCORE3
        let TABLETOP5 = table model.TABLETOP5
        let TABLEBOTTOM5 = table model.TABLEBOTTOM5

        let head =
            """<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--_____       _                   __                _   __               ___
   / ___/____  (_)___  ____  ____ _/ /_____  _____   / | / /___  _________/ (_)____
   \__ \/ __ \/ / __ \/ __ \/ __ `/ //_/ _ \/ ___/  /  |/ / __ \/ ___/ __  / / ___/
  ___/ / /_/ / / / / / / / / /_/ / ,< /  __/ /     / /|  / /_/ / /  / /_/ / / /__
 /____/ .___/_/_/ /_/_/ /_/\__,_/_/|_|\___/_/     /_/ |_/\____/_/   \__,_/_/\___/
     /_/--> 
            <html xmlns="http://www.w3.org/1999/xhtml"><head>
            <title></title>
            <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
            <style type="text/css">
            body {
            margin: 0;
            mso-line-height-rule: exactly;
            padding: 0;
            min-width: 100%;
            }
            table {
            border-collapse: collapse;
            border-spacing: 0;
            }
            td {
            padding: 0;
            vertical-align: top;
            }
            .spacer,
            .border {
            font-size: 1px;
            line-height: 1px;
            }
            .spacer {
            width: 100%;
            }
            img {
            border: 0;
            -ms-interpolation-mode: bicubic;
            }
            .image {
            font-size: 12px;
            Margin-bottom: 24px;
            mso-line-height-rule: at-least;
            }
            .image img {
            di    splay: block;
            }
            .logo {
            ms    o-line-height-rule: at-least;
            }
            .logo img {
            di    splay: block;
            }
            strong {
            fo    nt-weight: bold;
            }
            h1,
            h2,
            h3,
            p,
            ol,
            ul,
            li {
            Ma    rgin-top: 0;
            }
            ol,
            ul,
            li {
              padding-left: 0;
            }
            blockquote {
              Margin-top: 0;
              Margin-right: 0;
              Margin-bottom: 0;
              padding-right: 0;
            }
            .column-top {
              font-size: 32px;
              line-height: 32px;
            }
            .column-bottom {
              font-size: 8px;
              line-height: 8px;
            }
            .column {
              text-align: left;
            }
            .contents {
            table-layout: fixed;
            width: 100%;
            }
            .padded {
            padding-left: 32px;
            padding-right: 32px;
            word-break: break-word;
            word-wrap: break-word;
            }
            .wrapper {
            display: table;
            table-layout: fixed;
            width: 100%;
            min-width: 620px;
            -webkit-text-size-adjust: 100%;
            -ms-text-size-adjust: 100%;
            }
            table.wrapper {
              table-layout: fixed;
            }
            .one-col,
            .two-col,
            .three-col {
              Margin-left: auto;
              Margin-right: auto;
              width: 600px;
            }
            .centered {
              Margin-left: auto;
              Margin-right: auto;
            }
            .two-col .image {
              Margin-bottom: 23px;
            }
            .two-col .column-bottom {
              font-size: 9px;
              line-height: 9px;
            }
            .two-col .column {
              width: 300px;
            }
            .three-col .image {
              Margin-bottom: 21px;
            }
            .three-col .column-bottom {
              font-size: 11px;
              line-height: 11px;
            }
            .three-col .column {
              width: 200px;
            }
            .three-col .first .padded {
              padding-left: 32px;
              padding-right: 16px;
            }
            .three-col .second .padded {
              padding-left: 24px;
              padding-right: 24px;
            }
            .three-col .third .padded {
              padding-left: 16px;
              padding-right: 32px;
            }
            @media only screen and (min-width: 0) {
            .wrapper {
            text-rendering: optimizeLegibility;
            }
            }
            @media only screen and (max-width: 620px) {
            [class=wrapper] {
            min-width: 318px !important;
            width: 100% !important;
            }
            [class=wrapper] .one-col,
            [class=wrapper] .two-col,
            [class=wrapper] .three-col {
            width: 318px !important;
            }
            [class=wrapper] .column,
            [class=wrapper] .gutter {
            display: block;
            float: left;
            width: 318px !important;
            }
            [class=wrapper] .padded {
            padding-left: 32px !important;
            padding-right: 32px !important;
            }
            [class=wrapper] .block {
            display: block !important;
            }
            [class=wrapper] .hide {
                display: none !important;
            }
            [class=wrapper] .image {
            margin-bottom: 24px !important;
            }
            [class=wrapper] .image img {
            height: auto !important;
            width: 100% !important;
            }
            }
            .wrapper h1 {
            font-weight: 700;
            }
            .wrapper h2 {
              font-style: italic;
              font-weight: normal;
            }
            .wrapper h3 {
            font-weight: normal;
            }
            .one-col blockquote,
            .two-col blockquote,
            .three-col blockquote {
            font-style: italic;
            }
            .one-col-feature h1 {
              font-weight: normal;
            }
            .one-col-feature h2 {
              font-style: normal;
              font-weight: bold;
            }
            .one-col-feature h3 {
              font-style: italic;
            }
            td.border {
              width: 1px;
            }
            tr.border {
              background-color: #e9e9e9;
              height: 1px;
            }
            tr.border td {
              line-height: 1px;
            }
            .one-col,
            .two-col,
            .three-col,
            .one-col-feature {
              background-color: #ffffff;
              font-size: 14px;
              table-layout: fixed;
            }
            .one-col,
            .two-col,
            .three-col,
            .one-col-feature,
            .preheader,
            .header,
            .footer {
              Margin-left: auto;
              Margin-right: auto;
            }
            .preheader table {
              width: 602px;
            }
            .preheader .title,
            .preheader .webversion {
              padding-top: 10px;
              padding-bottom: 12px;
              font-size: 12px;
              line-height: 21px;
            }
            .preheader .title {
              text-align: left;
            }
            .preheader .webversion {
              text-align: right;
              width: 300px;
            }
            .header {
              width: 602px;
            }
            .header .logo {
              padding: 32px 0;
            }
            .header .logo div {
              font-size: 26px;
              font-weight: 700;
              letter-spacing: -0.02em;
              line-height: 32px;
            }
            .header .logo div a {
              text-decoration: none;
            }
            .header .logo div.logo-center {
              text-align: center;
            }
            .header .logo div.logo-center img {
              Margin-left: auto;
              Margin-right: auto;
            }
            .gmail {
              width: 650px;
              min-width: 650px;
            }
            .gmail td {
              font-size: 1px;
              line-height: 1px;
            }
            .wrapper a {
              text-decoration: underline;
              transition: all .2s;
            }
            .wrapper h1 {
              font-size: 36px;
              Margin-bottom: 18px;
            }
            .wrapper h2 {
              font-size: 26px;
              line-height: 32px;
              Margin-bottom: 20px;
            }
            .wrapper h3 {
              font-size: 18px;
              line-height: 22px;
              Margin-bottom: 16px;
            }
            .wrapper h1 a,
            .wrapper h2 a,
            .wrapper h3 a {
              text-decoration: none;
            }
            .one-col blockquote,
            .two-col blockquote,
            .three-col blockquote {
              font-size: 14px;
              border-left: 2px solid #e9e9e9;
              Margin-left: 0;
              padding-left: 16px;
            }
            table.divider {
              width: 100%;
            }
            .divider .inner {
              padding-bottom: 24px;
            }
            .divider table {
              background-color: #e9e9e9;
              font-size: 2px;
              line-height: 2px;
              width: 60px;
            }
            .wrapper .gray {
              background-color: #f7f7f7;
            }
            .wrapper .gray blockquote {
              border-left-color: #dddddd;
            }
            .wrapper .gray .divider table {
              background-color: #dddddd;
            }
            .padded .image {
              font-size: 0;
            }
            .image-frame {
              padding: 8px;
            }
            .image-background {
              display: inline-block;
              font-size: 12px;
            }
            .btn {
              Margin-bottom: 24px;
              padding: 2px;
            }
            .btn a {
              border: 1px solid #ffffff;
              display: inline-block;
              font-size: 13px;
              font-weight: bold;
              line-height: 15px;
              outline-style: solid;
              outline-width: 2px;
              padding: 10px 30px;
              text-align: center;
              text-decoration: none !important;
            }
            .one-col .column table:nth-last-child(2) td h1:last-child,
            .one-col .column table:nth-last-child(2) td h2:last-child,
            .one-col .column table:nth-last-child(2) td h3:last-child,
            .one-col .column table:nth-last-child(2) td p:last-child,
            .one-col .column table:nth-last-child(2) td ol:last-child,
            .one-col .column table:nth-last-child(2) td ul:last-child {
              Margin-bottom: 24px;
            }
            .one-col p,
            .one-col ol,
            .one-col ul {
              font-size: 16px;
              line-height: 24px;
            }
            .one-col ol,
            .one-col ul {
              Margin-left: 18px;
            }
            .two-col .column table:nth-last-child(2) td h1:last-child,
            .two-col .column table:nth-last-child(2) td h2:last-child,
            .two-col .column table:nth-last-child(2) td h3:last-child,
            .two-col .column table:nth-last-child(2) td p:last-child,
            .two-col .column table:nth-last-child(2) td ol:last-child,
            .two-col .column table:nth-last-child(2) td ul:last-child {
              Margin-bottom: 23px;
            }
            .two-col .image-frame {
              padding: 6px;
            }
            .two-col h1 {
              font-size: 26px;
              line-height: 32px;
              Margin-bottom: 16px;
            }
            .two-col h2 {
              font-size: 20px;
              line-height: 26px;
              Margin-bottom: 18px;
            }
            .two-col h3 {
              font-size: 16px;
              line-height: 20px;
              Margin-bottom: 14px;
            }
            .two-col p,
            .two-col ol,
            .two-col ul {
              font-size: 14px;
              line-height: 23px;
            }
            .two-col ol,
            .two-col ul {
              Margin-left: 16px;
            }
            .two-col li {
              padding-left: 5px;
            }
            .two-col .divider .inner {
              padding-bottom: 23px;
            }
            .two-col .btn {
              Margin-bottom: 23px;
            }
            .two-col blockquote {
              padding-left: 16px;
            }
            .three-col .column table:nth-last-child(2) td h1:last-child,
            .three-col .column table:nth-last-child(2) td h2:last-child,
            .three-col .column table:nth-last-child(2) td h3:last-child,
            .three-col .column table:nth-last-child(2) td p:last-child,
            .three-col .column table:nth-last-child(2) td ol:last-child,
            .three-col .column table:nth-last-child(2) td ul:last-child {
              Margin-bottom: 21px;
            }
            .three-col .image-frame {
              padding: 4px;
            }
            .three-col h1 {
              font-size: 20px;
              line-height: 26px;
              Margin-bottom: 12px;
            }
            .three-col h2 {
              font-size: 16px;
              line-height: 22px;
              Margin-bottom: 14px;
            }
            .three-col h3 {
              font-size: 14px;
              line-height: 18px;
              Margin-bottom: 10px;
            }
            .three-col p,
            .three-col ol,
            .three-col ul {
              font-size: 12px;
              line-height: 21px;
            }
            .three-col ol,
            .three-col ul {
              Margin-left: 14px;
            }
            .three-col li {
              padding-left: 6px;
            }
            .three-col .divider .inner {
              padding-bottom: 21px;
            }
            .three-col .btn {
              Margin-bottom: 21px;
            }
            .three-col .btn a {
              font-size: 12px;
              line-height: 14px;
              padding: 8px 19px;
            }
            .three-col blockquote {
              padding-left: 16px;
            }
            .one-col-feature .column-top {
              font-size: 36px;
              line-height: 36px;
            }
            .one-col-feature .column-bottom {
              font-size: 4px;
              line-height: 4px;
            }
            .one-col-feature .column {
              text-align: center;
              width: 600px;
            }
            .one-col-feature .image {
              Margin-bottom: 32px;
            }
            .one-col-feature .column table:nth-last-child(2) td h1:last-child,
            .one-col-feature .column table:nth-last-child(2) td h2:last-child,
            .one-col-feature .column table:nth-last-child(2) td h3:last-child,
            .one-col-feature .column table:nth-last-child(2) td p:last-child,
            .one-col-feature .column table:nth-last-child(2) td ol:last-child,
            .one-col-feature .column table:nth-last-child(2) td ul:last-child {
              Margin-bottom: 32px;
            }
            .one-col-feature h1,
            .one-col-feature h2,
            .one-col-feature h3 {
              text-align: center;
            }
            .one-col-feature h1 {
              font-size: 52px;
              Margin-bottom: 22px;
            }
            .one-col-feature h2 {
              font-size: 42px;
              Margin-bottom: 20px;
            }
            .one-col-feature h3 {
              font-size: 32px;
              line-height: 42px;
              Margin-bottom: 20px;
            }
            .one-col-feature p,
            .one-col-feature ol,
            .one-col-feature ul {
              font-size: 21px;
              line-height: 32px;
              Margin-bottom: 32px;
            }
            .one-col-feature p a,
            .one-col-feature ol a,
            .one-col-feature ul a {
              text-decoration: none;
            }
            .one-col-feature p {
              text-align: center;
            }
            .one-col-feature ol,
            .one-col-feature ul {
              Margin-left: 40px;
              text-align: left;
            }
            .one-col-feature li {
              padding-left: 3px;
            }
            .one-col-feature .btn {
              Margin-bottom: 32px;
              text-align: center;
            }
            .one-col-feature .divider .inner {
              padding-bottom: 32px;
            }
            .one-col-feature blockquote {
              border-bottom: 2px solid #e9e9e9;
              border-left-color: #ffffff;
              border-left-width: 0;
              border-left-style: none;
              border-top: 2px solid #e9e9e9;
              Margin-bottom: 32px;
              Margin-left: 0;
              padding-bottom: 42px;
              padding-left: 0;
              padding-top: 42px;
              position: relative;
            }
            .one-col-feature blockquote:before,
            .one-col-feature blockquote:after {
              background: -moz-linear-gradient(left, #ffffff 25%, #e9e9e9 25%, #e9e9e9 75%, #ffffff 75%);
              background: -webkit-gradient(linear, left top, right top, color-stop(25%, #ffffff), color-stop(25%, #e9e9e9), color-stop(75%, #e9e9e9), color-stop(75%, #ffffff));
              background: -webkit-linear-gradient(left, #ffffff 25%, #e9e9e9 25%, #e9e9e9 75%, #ffffff 75%);
              background: -o-linear-gradient(left, #ffffff 25%, #e9e9e9 25%, #e9e9e9 75%, #ffffff 75%);
              background: -ms-linear-gradient(left, #ffffff 25%, #e9e9e9 25%, #e9e9e9 75%, #ffffff 75%);
              background: linear-gradient(to right, #ffffff 25%, #e9e9e9 25%, #e9e9e9 75%, #ffffff 75%);
              content: '';
              display: block;
              height: 2px;
              left: 0;
              outline: 1px solid #ffffff;
              position: absolute;
              right: 0;
            }
            .one-col-feature blockquote:before {
              top: -2px;
            }
            .one-col-feature blockquote:after {
              bottom: -2px;
            }
            .one-col-feature blockquote p,
            .one-col-feature blockquote ol,
            .one-col-feature blockquote ul {
              font-size: 42px;
              line-height: 48px;
              Margin-bottom: 48px;
            }
            .one-col-feature blockquote p:last-child,
            .one-col-feature blockquote ol:last-child,
            .one-col-feature blockquote ul:last-child {
              Margin-bottom: 0 !important;
            }
            .footer {
              width: 602px;
            }
            .footer .padded {
              font-size: 12px;
              line-height: 20px;
            }
            .social {
              padding-top: 32px;
              padding-bottom: 22px;
            }
            .social img {
              display: block;
            }
            .social .divider {
              font-family: sans-serif;
              font-size: 10px;
              line-height: 21px;
              text-align: center;
              padding-left: 14px;
              padding-right: 14px;
            }
            .social .social-text {
              height: 21px;
              vertical-align: middle !important;
              font-size: 10px;
              font-weight: bold;
              text-decoration: none;
              text-transform: uppercase;
            }
            .social .social-text a {
              text-decoration: none;
            }
            .address {
              width: 250px;
            }
            .address .padded {
              text-align: left;
              padding-left: 0;
              padding-right: 10px;
            }
            .subscription {
              width: 350px;
            }
            .subscription .padded {
              text-align: right;
              padding-right: 0;
              padding-left: 10px;
            }
            .address,
            .subscription {
              padding-top: 32px;
              padding-bottom: 64px;
            }
            .address a,
            .subscription a {
              font-weight: bold;
              text-decoration: none;
            }
            .address table,
            .subscription table {
              width: 100%;
            }
            @media only screen and (max-width: 651px) {
            .gmail {
                  display: none !important;
            }
                  }
                  @media only screen and (max-width: 620px) {
            [class=wrapper] .one-col .column:last-child table:nth-last-child(2) td h1:last-child,
            [class=wrapper] .two-col .column:last-child table:nth-last-child(2) td h1:last-child,
            [class=wrapper] .three-col .column:last-child table:nth-last-child(2) td h1:last-child,
            [class=wrapper] .one-col-feature .column:last-child table:nth-last-child(2) td h1:last-child,
            [class=wrapper] .one-col .column:last-child table:nth-last-child(2) td h2:last-child,
            [class=wrapper] .two-col .column:last-child table:nth-last-child(2) td h2:last-child,
            [class=wrapper] .three-col .column:last-child table:nth-last-child(2) td h2:last-child,
            [class=wrapper] .one-col-feature .column:last-child table:nth-last-child(2) td h2:last-child,
            [class=wrapper] .one-col .column:last-child table:nth-last-child(2) td h3:last-child,
            [class=wrapper] .two-col .column:last-child table:nth-last-child(2) td h3:last-child,
            [class=wrapper] .three-col .column:last-child table:nth-last-child(2) td h3:last-child,
            [class=wrapper] .one-col-feature .column:last-child table:nth-last-child(2) td h3:last-child,
            [class=wrapper] .one-col .column:last-child table:nth-last-child(2) td p:last-child,
            [class=wrapper] .two-col .column:last-child table:nth-last-child(2) td p:last-child,
            [class=wrapper] .three-col .column:last-child table:nth-last-child(2) td p:last-child,
            [class=wrapper] .one-col-feature .column:last-child table:nth-last-child(2) td p:last-child,
            [class=wrapper] .one-col .column:last-child table:nth-last-child(2) td ol:last-child,
            [class=wrapper] .two-col .column:last-child table:nth-last-child(2) td ol:last-child,
            [class=wrapper] .three-col .column:last-child table:nth-last-child(2) td ol:last-child,
            [class=wrapper] .one-col-feature .column:last-child table:nth-last-child(2) td ol:last-child,
            [class=wrapper] .one-col .column:last-child table:nth-last-child(2) td ul:last-child,
            [class=wrapper] .two-col .column:last-child table:nth-last-child(2) td ul:last-child,
            [class=wrapper] .three-col .column:last-child table:nth-last-child(2) td ul:last-child,
            [class=wrapper] .one-col-feature .column:last-child table:nth-last-child(2) td ul:last-child {
              Margin-bottom: 24px !important;
            }
            [class=wrapper] .address,
            [class=wrapper] .subscription {
              display: block;
              float: left;
              width: 318px !important;
              text-align: center !important;
            }
            [class=wrapper] .address {
              padding-bottom: 0 !important;
            }
            [class=wrapper] .subscription {
              padding-top: 0 !important;
            }
            [class=wrapper] h1 {
              font-size: 36px !important;
              line-height: 42px !important;
              Margin-bottom: 18px !important;
            }
            [class=wrapper] h2 {
              font-size: 26px !important;
              line-height: 32px !important;
              Margin-bottom: 20px !important;
            }
            [class=wrapper] h3 {
              font-size: 18px !important;
              line-height: 22px !important;
              Margin-bottom: 16px !important;
            }
            [class=wrapper] p,
            [class=wrapper] ol,
            [class=wrapper] ul {
              font-size: 16px !important;
              line-height: 24px !important;
              Margin-bottom: 24px !important;
            }
            [class=wrapper] ol,
            [class=wrapper] ul {
              Margin-left: 18px !important;
            }
            [class=wrapper] li {
              padding-left: 2px !important;
            }
            [class=wrapper] blockquote {
              padding-left: 16px !important;
            }
            [class=wrapper] .two-col .column:nth-child(n + 3) {
              border-top: 1px solid #e9e9e9;
            }
            [class=wrapper] .btn {
              margin-bottom: 24px !important;
            }
            [class=wrapper] .btn a {
              display: block !important;
              font-size: 13px !important;
              font-weight: bold !important;
              line-height: 15px !important;
              padding: 10px 30px !important;
            }
            [class=wrapper] .column-bottom {
              font-size: 8px !important;
              line-height: 8px !important;
            }
            [class=wrapper] .first .column-bottom,
            [class=wrapper] .three-col .second .column-bottom {
              display: none;
            }
            [class=wrapper] .second .column-top,
            [class=wrapper] .third .column-top {
              display: none;
            }
            [class=wrapper] .image-frame {
              padding: 4px !important;
            }
            [class=wrapper] .header .logo {
              padding-left: 10px !important;
              padding-right: 10px !important;
            }
            [class=wrapper] .header .logo div {
              font-size: 26px !important;
              line-height: 32px !important;
            }
            [class=wrapper] .header .logo div img {
              display: inline-block !important;
              max-width: 280px !important;
              height: auto !important;
            }
            [class=wrapper] table.border,
            [class=wrapper] .header,
            [class=wrapper] .webversion,
            [class=wrapper] .footer {
              width: 320px !important;
            }
            [class=wrapper] .preheader .webversion,
            [class=wrapper] .header .logo a {
              text-align: center !important;
            }
            [class=wrapper] .preheader table,
            [class=wrapper] .border td {
              width: 318px !important;
            }
            [class=wrapper] .border td.border {
              width: 1px !important;
            }
            [class=wrapper] .image .border td {
              width: auto !important;
            }
            [class=wrapper] .title {
              display: none;
            }
            [class=wrapper] .footer .padded {
              text-align: center !important;
            }
            [class=wrapper] .footer .subscription .padded {
              padding-top: 20px !important;
            }
            [class=wrapper] .footer .social-link {
              display: block !important;
            }
            [class=wrapper] .footer .social-link table {
              margin: 0 auto 10px !important;
            }
            [class=wrapper] .footer .divider {
              display: none !important;
            }
            [class=wrapper] .one-col-feature .btn {
              margin-bottom: 28px !important;
            }
            [class=wrapper] .one-col-feature .image {
              margin-bottom: 28px !important;
            }
            [class=wrapper] .one-col-feature .divider .inner {
              padding-bottom: 28px !important;
            }
            [class=wrapper] .one-col-feature h1 {
              font-size: 42px !important;
              line-height: 48px !important;
              margin-bottom: 20px !important;
            }
            [class=wrapper] .one-col-feature h2 {
              font-size: 32px !important;
              line-height: 36px !important;
              margin-bottom: 18px !important;
            }
            [class=wrapper] .one-col-feature h3 {
              font-size: 26px !important;
              line-height: 32px !important;
              margin-bottom: 20px !important;
            }
            [class=wrapper] .one-col-feature p,
            [class=wrapper] .one-col-feature ol,
            [class=wrapper] .one-col-feature ul {
              font-size: 20px !important;
              line-height: 28px !important;
              margin-bottom: 28px !important;
            }
            [class=wrapper] .one-col-feature blockquote {
              font-size: 18px !important;
              line-height: 26px !important;
              margin-bottom: 28px !important;
              padding-bottom: 26px !important;
              padding-left: 0 !important;
              padding-top: 26px !important;
            }
            [class=wrapper] .one-col-feature blockquote p,
            [class=wrapper] .one-col-feature blockquote ol,
            [class=wrapper] .one-col-feature blockquote ul {
              font-size: 26px !important;
              line-height: 32px !important;
            }
            [class=wrapper] .one-col-feature blockquote p:last-child,
            [class=wrapper] .one-col-feature blockquote ol:last-child,
            [class=wrapper] .one-col-feature blockquote ul:last-child {
              margin-bottom: 0 !important;
            }
            [class=wrapper] .one-col-feature .column table:last-of-type h1:last-child,
            [class=wrapper] .one-col-feature .column table:last-of-type h2:last-child,
            [class=wrapper] .one-col-feature .column table:last-of-type h3:last-child {
              margin-bottom: 28px !important;
            }
            }
            @media only screen and (max-width: 320px) {
              [class=wrapper] td.border {
            display: none;
              }
              [class=wrapper] table.border,
              [class=wrapper] .header,
              [class=wrapper] .webversion,
              [class=wrapper] .footer {
                width: 318px !important;
              }
            }
            </style>
              <meta name="robots" content="noindex,nofollow" />
            <meta property="og:title" content="My First Campaign" />
            </head>"""

        let body = 
            """"<body style="margin: 0;mso-line-height-rule: exactly;padding: 0;min-width: 100%;background-color: #fbfbfb"><style type="text/css">
            body,.wrapper,.emb-editor-canvas{background-color:#fbfbfb}.border{background-color:#e9e9e9}h1{color:#565656}.wrapper h1{}.wrapper h1{font-family:sans-serif}@media only         screen and (min-width: 0){.wrapper h1{font-family:Avenir,sans-serif !important}}h1{}.one-col h1{line-height:42px}.two-col h1{line-height:32px}.three-col h1{line-     height:26px}.wrapper .one-col-feature h1{line-height:58px}@media only screen and (max-width: 620px){h1{line-height:42px !important}}h2{color:#555}.wrapper h2   {}.wrapper h2 {font-family:Georgia,serif}h2{}.one-col h2{line-height:32px}.two-col h2{line-height:26px}.three-col h2{line-height:22px}.wrapper .one-col-feature h2 {line-height:52px}   @media only screen and (max-width: 620px){h2{line-height:32px !important}}h3{color:#555}.wrapper h3{}.wrapper h3{font-family:Georgia,serif}h3   {}.one-col h3{line-   height:26px}.two-col h3{line-height:22px}.three-col 
            h3{line-height:20px}.wrapper .one-col-feature h3{line-height:42px}@media only screen and (max-width: 620px){h3{line-height:26px !important}}p,ol,ul {color:#565656}.wrapper  p,.wrapper ol,.wrapper ul{}.wrapper p,.wrapper ol,.wrapper ul{font-family:Georgia,serif}p,ol,ul{}.one-col p,.one-col ol,.one-col ul{line-   height:25px;Margin-bottom:25px}.two-col p,.two-col ol,.two-col ul{line-height:23px;Margin-bottom:23px}.three-col p,.three-col ol,.three-col ul{line-  height:21px;Margin-bottom:21px}.wrapper .one-col-feature p,.wrapper .one-col-feature ol,.wrapper .one-col-feature ul{line-height:32px}.one-col-feature blockquote    p,.one-col-feature    blockquote ol,.one-col-feature blockquote ul{line-height:50px}@media only screen and (max-width: 620px){p,ol,ul{line-height:25px !   important;Margin-bottom:25px !   important}}.image{color:#565656}.image{font-family:Georgia,serif}.wrapper a{color:#41637e}.wrapper 
            a:hover{color:#30495c !important}.wrapper .logo div{color:#41637e}.wrapper .logo div{font-family:sans-serif}@media only screen and (min-width: 0){.wrapper .logo div    {font-  family:Avenir,sans-serif !important}}.wrapper .logo div a{color:#41637e}.wrapper .logo div a:hover{color:#41637e !important}.wrapper .one-col-feature p     a,.wrapper .one-  col-feature ol a,.wrapper .one-col-feature ul a{border-bottom:1px solid #41637e}.wrapper .one-col-feature p a:hover,.wrapper .one-col-feature ol  a:hover,.wrapper .one-   col-feature ul a:hover{color:#30495c !important;border-bottom:1px solid #30495c !important}.btn a{}.wrapper .btn a{}.wrapper .btn a{font-   family:Georgia,serif}.wrapper .btn a{background-color:#41637e;color:#fff !important;outline-color:#41637e;text-shadow:0 1px 0 #3b5971}.wrapper .btn a:hover  {background-   color:#3b5971 !important;color:#fff !important;outline-color:#3b5971 !important}.preheader 
            .title,.preheader .webversion,.footer .padded{color:#999}.preheader .title,.preheader .webversion,.footer .padded{font-family:Georgia,serif}.preheader .title       a,.preheader .webversion a,.footer .padded a{color:#999}.preheader .title a:hover,.preheader .webversion a:hover,.footer .padded a:hover{color:#737373 !    important}.footer .social .divider{color:#e9e9e9}.footer .social .social-text,.footer .social a{color:#999}.wrapper .footer .social .social-  text,.wrapper .footer .social     a{}.wrapper .footer .social .social-text,.wrapper .footer .social a{font-family:Georgia,serif}.footer .social .social-  text,.footer .social a{}.footer .social .social-  text,.footer .social a{letter-spacing:0.05em}.footer .social .social-text:hover,.footer .social a:hover {color:#737373 !important}.image .border{background-   color:#c8c8c8}.image-frame{background-color:#dadada}.image-background{background-color:#f7f7f7}
            </style>
                <center class="wrapper" style="display: table;table-layout: fixed;width: 100%;min-width: 620px;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;background-        color: #fbfbfb">
            	    <table class="gmail" style="border-collapse: collapse;border-spacing: 0;width: 650px;min-width: 650px"><tbody><tr><td style="padding: 0;vertical-align:     top;font-   size: 1px;line-height: 1px">&nbsp;</td></tr></tbody></table>
                  <table class="preheader centered" style="border-collapse: collapse;border-spacing: 0;Margin-left: auto;Margin-right: auto">
                    <tbody><tr>
                      <td style="padding: 0;vertical-align: top">
                        <table style="border-collapse: collapse;border-spacing: 0;width: 602px">
                          <tbody><tr>
                            <td class="title" style="padding: 0;vertical-align: top;padding-top: 10px;padding-bottom: 12px;font-size: 12px;line-height: 21px;text-align: left;color:        #999;font-family: Georgia,serif">"""+
        
            model.SUMARIZE + 
        
            """</td>
                          </tr>
                        </tbody></table>
                      </td>
                    </tr>
                  </tbody></table>
                  <table class="header centered" style="border-collapse: collapse;border-spacing: 0;Margin-left: auto;Margin-right: auto;width: 602px">
                    <tbody><tr><td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&nbsp;</td></tr>
                    <tr><td class="logo" style="padding: 32px 0;vertical-align: top;mso-line-height-rule: at-least"><div class="logo-center" style="font-size: 26px;font-weight:        700;letter-spacing: -0.02em;line-height: 32px;color: #41637e;font-family: sans-serif;text-align: center" align="center" id="emb-email-header"><img  style="border:  0;-ms-interpolation-mode: bicubic;display: block;Margin-left: auto;Margin-right: auto;max-width: 300px" src="""+
                
            model.LOGOSOURCE +
        
            """ alt=""    width="300" height="65" /></   div></td></tr>
                  </tbody></table>
                      <table class="border" style="border-collapse: collapse;border-spacing: 0;font-size: 1px;line-height: 1px;background-color: #e9e9e9;Margin-left: auto;Margin-right:     auto" width="602">
                        <tbody><tr><td style="padding: 0;vertical-align: top">&#8203;</td></tr>
                      </tbody></table>
                      <table class="centered" style="border-collapse: collapse;border-spacing: 0;Margin-left: auto;Margin-right: auto">
                        <tbody><tr>
                          <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td>
                          <td style="padding: 0;vertical-align: top">
                            <table class="one-col" style="border-collapse: collapse;border-spacing: 0;Margin-left: auto;Margin-right: auto;width: 600px;background-color:   #ffffff;font- size: 14px;table-layout: fixed">
                              <tbody><tr>
                                <td class="column" style="padding: 0;vertical-align: top;text-align: left">
                                  <div><div class="column-top" style="font-size: 32px;line-height: 32px">&nbsp;</div></div>
                                    <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%">
                                      <tbody><tr>
                                        <td class="padded" style="padding: 0;vertical-align: top;padding-left: 32px;padding-right: 32px;word-break: break-word;word-wrap: break-    word">
                                      
                        <h1 style="Margin-top: 0;color: #565656;font-weight: 700;font-size: 36px;Margin-bottom: 18px;font-family: sans-serif;line-height: 42px;text-align:  center">"""+
        
            model.TITLE +
        
            """</h1><p style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 16px;line-height: 25px;Margin-bottom: 25px;text-align: center">"""+
        
            model.NAMESTRING + 
        
            """</p><p style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 16px;line-height: 25px;Margin-bottom: 24px;text-align: center">"""+
        
            model.INFORMATIONLINE +
        
            """</p>
                                        </td>
                                      </tr>
                                    </tbody></table>
                              
                                  <div class="column-bottom" style="font-size: 8px;line-height: 8px">&nbsp;</div>
                                </td>
                              </tr>
                            </tbody></table>
                          </td>
                          <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td>
                        </tr>
                      </tbody></table>
                      <table class="border" style="border-collapse: collapse;border-spacing: 0;font-size: 1px;line-height: 1px;background-color: #e9e9e9;Margin-left: auto;Margin-right:     auto" width="602">
                        <tbody><tr class="border" style="font-size: 1px;line-height: 1px;background-color: #e9e9e9;height: 1px">
                          <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td>
                          <td style="padding: 0;vertical-align: top;line-height: 1px">&#8203;</td>
                          <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td>
                        </tr>
                      </tbody></table>
                
                      <table class="centered" style="border-collapse: collapse;border-spacing: 0;Margin-left: auto;Margin-right: auto">
                        <tbody><tr>
                          <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td>
                          <td style="padding: 0;vertical-align: top">
                            <table class="one-col-feature" style="border-collapse: collapse;border-spacing: 0;background-color: #ffffff;font-size: 14px;table-layout: fixed;Margin-left:     auto;Margin-right: auto">
                              <tbody><tr>
                                <td class="column" style="padding: 0;vertical-align: top;text-align: center;width: 600px">
                                  <div><div class="column-top" style="font-size: 36px;line-height: 36px">&nbsp;</div></div>
                                    <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%">
                                      <tbody><tr>
                                        <td class="padded" style="padding: 0;vertical-align: top;padding-left: 32px;padding-right: 32px;word-break: break-word;word-wrap: break-    word">
                                      
                        <h1 style="Margin-top: 0;color: #565656;font-weight: normal;font-size: 52px;Margin-bottom: 22px;font-family: sans-serif;text-align: center;line-height:         58px">"""+
        
            model.SCORE1.ToString() +
        
            """</h1>
                  
                                        </td>
                                      </tr>
                                    </tbody></table>
                              
                                    <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%">
                                      <tbody><tr>
                                        <td class="padded" style="padding: 0;vertical-align: top;padding-left: 32px;padding-right: 32px;word-break: break-word;word-wrap: break-    word">
                                      
                        <table class="divider" style="border-collapse: collapse;border-spacing: 0;width: 100%"><tbody><tr><td class="inner" style="padding: 0;vertical-align:       top;padding-bottom: 32px" align="center">
                          <table style="border-collapse: collapse;border-spacing: 0;background-color: #e9e9e9;font-size: 2px;line-height: 2px;width: 60px">
                            <tbody><tr><td style="padding: 0;vertical-align: top">&nbsp;</td></tr>
                          </tbody></table>
                        </td></tr></tbody></table>
                  
                                        </td>
                                      </tr>
                                    </tbody></table>
                              
                                    <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%">
                                      <tbody><tr>
                                        <td class="padded" style="padding: 0;vertical-align: top;padding-left: 32px;padding-right: 32px;word-break: break-word;word-wrap: break-    word">
                                      
                        <p style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 21px;line-height: 32px;Margin-bottom: 2px;text-align: center"><strong     style="font-    weight: bold">Week-on-Week</strong></p><p style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 21px;line-height:   32px;Margin-bottom:   32px;text-align: center">"""+
                
            WOW +
        
            """</p><p style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 21px;line-height: 32px;Margin-bottom: 2px;text-align: center"><strong          style="font-    weight: bold">Month-on-Month</strong></p><p style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 21px;line-height:         32px;Margin-bottom:    32px;text-align: center">"""+
        
            MOM +
        
            """</p>
                                        </td>
                                      </tr>
                                    </tbody></table>
                              
                                    <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%">
                                      <tbody><tr>
                                        <td class="padded" style="padding: 0;vertical-align: top;padding-left: 32px;padding-right: 32px;word-break: break-word;word-wrap: break-    word">
                                      
                        <table class="divider" style="border-collapse: collapse;border-spacing: 0;width: 100%"><tbody><tr><td class="inner" style="padding: 0;vertical-align:       top;padding-bottom: 32px" align="center">
                          <table style="border-collapse: collapse;border-spacing: 0;background-color: #e9e9e9;font-size: 2px;line-height: 2px;width: 60px">
                            <tbody><tr><td style="padding: 0;vertical-align: top">&nbsp;</td></tr>
                          </tbody></table>
                        </td></tr></tbody></table>
                                        </td>
                                      </tr>
                                    </tbody></table>
                              
                                    <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%">
                                      <tbody><tr>
                                        <td class="padded" style="padding: 0;vertical-align: top;padding-left: 32px;padding-right: 32px;word-break: break-word;word-wrap: break-    word">
                                      
                        <p style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 21px;line-height: 32px;Margin-bottom: 32px;text-align: center">"""+
                
            model.CALLTOACTION +
        
            """</p>
                                        </td>
                                      </tr>
                                    </tbody></table>
                              
                                    <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%">
                                      <tbody><tr>
                                        <td class="padded" style="padding: 0;vertical-align: top;padding-left: 32px;padding-right: 32px;word-break: break-word;word-wrap: break-    word">
                                      
                        <div class="btn" style="Margin-bottom: 32px;padding: 2px;text-align: center">
                          <![if !mso]><a style="border: 1px solid #ffffff;display: inline-block;font-size: 13px;font-weight: bold;line-height: 15px;outline-style: solid;outline-   width:     2px;padding: 10px 30px;text-align: center;text-decoration: none !important;transition: all .2s;color: #fff !important;font-family:  Georgia,serif;background-    color: #41637e;outline-color: #41637e;text-shadow: 0 1px 0 #3b5971" href=""" +
                      
            model.BUTTONLINK +
        
            ">"+
        
            model.BUTTONTEXT +
        
            """</a><![endif]>
                        </div>
                  
                                        </td>
                                      </tr>
                                    </tbody></table>
                              
                                  <div class="column-bottom" style="font-size: 4px;line-height: 4px">&nbsp;</div>
                                </td>
                              </tr>
                            </tbody></table>
                          </td>
                          <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td>
                        </tr>
                      </tbody></table>
                
                      <table class="border" style="border-collapse: collapse;border-spacing: 0;font-size: 1px;line-height: 1px;background-color: #e9e9e9;Margin-left: auto;Margin-right:     auto" width="602">
                        <tbody><tr class="border" style="font-size: 1px;line-height: 1px;background-color: #e9e9e9;height: 1px">
                          <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td>
                          <td style="padding: 0;vertical-align: top;line-height: 1px">&#8203;</td>
                          <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td>
                        </tr>
                      </tbody></table>
                
                      <table class="centered" style="border-collapse: collapse;border-spacing: 0;Margin-left: auto;Margin-right: auto">
                        <tbody><tr>
                          <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td>
                          <td style="padding: 0;vertical-align: top">
                            <table class="two-col" style="border-collapse: collapse;border-spacing: 0;Margin-left: auto;Margin-right: auto;width: 600px;background-color:   #ffffff;font- size: 14px;table-layout: fixed">
                              <tbody><tr>
                                <td class="column first" style="padding: 0;vertical-align: top;text-align: left;width: 300px">
                                  <div><div class="column-top" style="font-size: 32px;line-height: 32px">&nbsp;</div></div>
                                    <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%">
                                      <tbody><tr>
                                        <td class="padded" style="padding: 0;vertical-align: top;padding-left: 32px;padding-right: 32px;word-break: break-word;word-wrap: break-    word">
                                      
                        <p style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 14px;line-height: 23px;Margin-bottom: 23px;text-align: center">"""+
                
            model.TOP5 +
        
            """</p><p style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 14px;line-height: 23px;Margin-bottom: 23px;text-align: center">"""+
        
            TABLETOP5 + 
        
            """</p>
                                        </td>
                                      </tr>
                                    </tbody></table>
                              
                                  <div class="column-bottom" style="font-size: 9px;line-height: 9px">&nbsp;</div>
                                </td>
                                <td class="column second" style="padding: 0;vertical-align: top;text-align: left;width: 300px">
                                  <div><div class="column-top" style="font-size: 32px;line-height: 32px">&nbsp;</div></div>
                                    <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%">
                                      <tbody><tr>
                                        <td class="padded" style="padding: 0;vertical-align: top;padding-left: 32px;padding-right: 32px;word-break: break-word;word-wrap: break-    word">
                                      
                        <p style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 14px;line-height: 23px;Margin-bottom: 23px;text-align: center">"""+
                
            model.BOTTOM5 +
        
            """</p><p style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 14px;line-height: 23px;Margin-bottom: 23px;text-align: center">"""+
        
            TABLEBOTTOM5 +
        
            """</p>
                                        </td>
                                      </tr>
                                    </tbody></table>
                              
                                  <div class="column-bottom" style="font-size: 9px;line-height: 9px">&nbsp;</div>
                                </td>
                              </tr>
                            </tbody></table>
                          </td>
                          <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td>
                        </tr>
                      </tbody></table>
                
                      <table class="border" style="border-collapse: collapse;border-spacing: 0;font-size: 1px;line-height: 1px;background-color: #e9e9e9;Margin-left: auto;Margin-right:     auto" width="602">
                        <tbody><tr class="border" style="font-size: 1px;line-height: 1px;background-color: #e9e9e9;height: 1px">
                          <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td>
                          <td style="padding: 0;vertical-align: top;line-height: 1px">&#8203;</td>
                          <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td>
                        </tr>
                      </tbody></table>
                
                      <table class="centered" style="border-collapse: collapse;border-spacing: 0;Margin-left: auto;Margin-right: auto">
                        <tbody><tr>
                          <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td>
                          <td style="padding: 0;vertical-align: top">
                            <table class="one-col" style="border-collapse: collapse;border-spacing: 0;Margin-left: auto;Margin-right: auto;width: 600px;background-color:   #ffffff;font- size: 14px;table-layout: fixed">
                              <tbody><tr>
                                <td class="column" style="padding: 0;vertical-align: top;text-align: left">
                                  <div><div class="column-top" style="font-size: 32px;line-height: 32px">&nbsp;</div></div>
                                    <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%">
                                      <tbody><tr>
                                        <td class="padded" style="padding: 0;vertical-align: top;padding-left: 32px;padding-right: 32px;word-break: break-word;word-wrap: break-    word">
                                      
                        <p style="Margin-top: 0;color: #565656;font-family: Georgia,serif;font-size: 16px;line-height: 25px;Margin-bottom: 25px;text-align: center">"""+
                
            model.IMAGETEXT +
        
            """</p>
                                        </td>
                                      </tr>
                                    </tbody></table>
                              
                                    <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%">
                                      <tbody><tr>
                                        <td class="padded" style="padding: 0;vertical-align: top;padding-left: 32px;padding-right: 32px;word-break: break-word;word-wrap: break-    word">
                                      
                          <table class="image" style="border-collapse: collapse;border-spacing: 0;font-size: 0;Margin-bottom: 24px;mso-line-height-rule: at-least;color:    #565656;font-  family: Georgia,serif" align="center">
                            <tbody><tr>
                              <td class="image-frame" style="padding: 8px;vertical-align: top;background-color: #dadada">
                                <span class="image-background" style="display: inline-block;font-size: 12px;background-color: #f7f7f7"><a style="text-decoration:   underline;transition:     all .2s;color: #41637e" href="""+
                
            model.IMAGELINK +
        
            """><img style="border: 0;-ms-interpolation-mode: bicubic;display: block;max-width: 900px" src=""" +
        
            model.IMAGESOURCE +
        
            """ alt="" width="520" height="292" /></a></span>
                              </td>
                            </tr>
                            <tr><td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #c8c8c8;width: 1px">&nbsp;</td></tr>
                          </tbody></table>
                                        </td>
                                      </tr>
                                    </tbody></table>
                              
                                  <div class="column-bottom" style="font-size: 8px;line-height: 8px">&nbsp;</div>
                                </td>
                              </tr>
                            </tbody></table>
                          </td>
                          <td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&#8203;</td>
                        </tr>
                      </tbody></table>
                
                      <table class="border" style="border-collapse: collapse;border-spacing: 0;font-size: 1px;line-height: 1px;background-color: #e9e9e9;Margin-left: auto;Margin-right:     auto" width="602">
                        <tbody><tr><td style="padding: 0;vertical-align: top">&#8203;</td></tr>
                      </tbody></table>
                
                  <div class="spacer" style="font-size: 1px;line-height: 32px;width: 100%">&nbsp;</div>
                  <table class="footer centered" style="border-collapse: collapse;border-spacing: 0;Margin-left: auto;Margin-right: auto;width: 602px">
                    <tbody><tr>
                      <td class="social" style="padding: 0;vertical-align: top;padding-top: 32px;padding-bottom: 22px" align="center">
                    
                      </td>
                    </tr>
                    <tr><td class="border" style="padding: 0;vertical-align: top;font-size: 1px;line-height: 1px;background-color: #e9e9e9;width: 1px">&nbsp;</td></tr>
                    <tr>
                      <td style="padding: 0;vertical-align: top">
                        <table style="border-collapse: collapse;border-spacing: 0">
                          <tbody><tr>
                            <td class="address" style="padding: 0;vertical-align: top;width: 250px;padding-top: 32px;padding-bottom: 64px">
                              <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%">
                                <tbody><tr>
                                  <td class="padded" style="padding: 0;vertical-align: top;padding-left: 0;padding-right: 10px;word-break: break-word;word-wrap: break-word;text-   align:     left;font-size: 12px;line-height: 20px;color: #999;font-family: Georgia,serif">
                                
                                  </td>
                                </tr>
                              </tbody></table>
                            </td>
                            <td class="subscription" style="padding: 0;vertical-align: top;width: 350px;padding-top: 32px;padding-bottom: 64px">
                              <table class="contents" style="border-collapse: collapse;border-spacing: 0;table-layout: fixed;width: 100%">
                                <tbody><tr>
                                  <td class="padded" style="padding: 0;vertical-align: top;padding-left: 10px;padding-right: 0;word-break: break-word;word-wrap: break-word;font-   size:  12px;line-height: 20px;color: #999;font-family: Georgia,serif;text-align: right">
                                
                                    <div>
                                      <span class="block">
                                        <span>
                                          <span class="block"><a style="font-weight:bold;text-decoration:none;" href="PREFS">Prefereces</a></span>
                                          <span class="hide">&nbsp;&nbsp;|&nbsp;&nbsp;</span>
                                        </span>
                                      </span>
                                      <span class="block"><a style="font-weight:bold;text-decoration:none;" href="UNSUBS">Unsubscribe</a></span>
                                    </div>
                                  </td>
                                </tr>
                              </tbody></table>
                            </td>
                          </tr>
                        </tbody></table>
                      </td>
                    </tr>
                  </tbody></table>
                </center>
            </body></html>
            """
        
        head + body


type TokenRouter = { token:string}

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

    member this.Report() =
        let model = {
            SUMARIZE = "This is a summary"; 
            TITLE = "this be a title"; 
            NAMESTRING = "Oh no you didn't!"; 
            INFORMATIONLINE = "We can try it again"; 
            LOGOSOURCE = "http://googledrive.com/host/0B1HMPCqsxDmHflp1ODlWTzlIbzVqRlo5TjhZWXljQVR3Q24xMF9tRVZQR3VlaC1JMnhfVVk/logo.png"; 
            SCORE1 = 5.55; 
            SCORE2 = 5.4; 
            SCORE3 = 5.7;
            CALLTOACTION = "CLICK ME!"; 
            BUTTONLINK = sprintf "/Settings?%s" "abcd"; 
            BUTTONTEXT = "I SAID CLICK ME!"; 
            TOP5 = "Top 5 losers"; 
            BOTTOM5 = "Bottom 5 winners"; 
            TABLETOP5 = ([|"keyword1"; "keyword2"; "keyword3"; "keyword4"; "keyword5"|],[|"score1"; "score2"; "score3"; "score4"; "score5"|]); 
            TABLEBOTTOM5 = ([|"keyword-1"; "keyword-2"; "keyword-3"; "keyword-4"; "keyword-5"|],[|"score-1"; "score-2"; "score-3"; "score-4"; "score-5"|]); 
            IMAGETEXT = "imagetext?"; 
            IMAGELINK = "http://www.example.com/"; 
            IMAGESOURCE = "http://cdn.desktopwallpapers4.me/wallpapers/comics/1920x1080/2/19476-superheroes-1920x1080-comic-wallpaper.jpg"; 
        }

        QualityScoreReport().Contruct(model)
        
    member this.PostEmail(email: string,  qsid : int, token : string) =
        use db = DataConnection.GetDataContext()
        
        let newmail = new DataConnection.ServiceTypes.QS_Emails(QS_Id = qsid, Email = email)

        db.QS_Emails.InsertOnSubmit(newmail)
        db.DataContext.SubmitChanges()

        let tokenRouter = { token = token }
        this.RedirectToAction("Settings", tokenRouter);

    member this.DeleteEmail(email : string, qsid : int, token : string) =
        use db = DataConnection.GetDataContext()

        let newmail = queries.FindQSEmailByMailAndQSId(qsid, email, db)

        db.QS_Emails.DeleteOnSubmit(newmail)
        db.DataContext.SubmitChanges()

        let tokenRouter = { token = token }
        this.RedirectToAction("Settings", tokenRouter);

    member this.PostBrand(keyword: string,  qsid : int, token : string) =
        use db = DataConnection.GetDataContext()
        
        let newbrand = new DataConnection.ServiceTypes.QS_Brands(QS_Id = qsid, Keyword = keyword)

        db.QS_Brands.InsertOnSubmit(newbrand)
        db.DataContext.SubmitChanges()

        let tokenRouter = { token = token }
        this.RedirectToAction("Settings", tokenRouter);

    member this.DeleteBrand(keyword : string, qsid : int, token : string) =
        use db = DataConnection.GetDataContext()

        let newmail = queries.FindQSBrandByKeywordAndQSId(qsid, keyword, db)

        db.QS_Brands.DeleteOnSubmit(newmail)
        db.DataContext.SubmitChanges()

        let tokenRouter = { token = token }
        this.RedirectToAction("Settings", tokenRouter);
