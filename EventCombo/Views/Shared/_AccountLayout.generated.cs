﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using EventCombo;
    
    #line 1 "..\..\Views\Shared\_AccountLayout.cshtml"
    using EventCombo.ViewModels;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/_AccountLayout.cshtml")]
    public partial class _Views_Shared__AccountLayout_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_Shared__AccountLayout_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<!DOCTYPE html>\r\n<html");

WriteLiteral(" lang=\"en\"");

WriteLiteral(">\r\n<head>\r\n    <meta");

WriteLiteral(" charset=\"utf-8\"");

WriteLiteral(">\r\n    <meta");

WriteLiteral(" http-equiv=\"X-UA-Compatible\"");

WriteLiteral(" content=\"IE=edge\"");

WriteLiteral(">\r\n    <meta");

WriteLiteral(" name=\"viewport\"");

WriteLiteral(" content=\"width=device-width, initial-scale=1\"");

WriteLiteral(">\r\n    <!-- The above 3 meta tags *must* come first in the head; any other head c" +
"ontent must come *after* these tags -->\r\n    <meta");

WriteLiteral(" name=\"description\"");

WriteLiteral(" content=\"\"");

WriteLiteral(">\r\n    <meta");

WriteLiteral(" name=\"author\"");

WriteLiteral(" content=\"\"");

WriteLiteral(">\r\n    <link");

WriteLiteral(" rel=\"icon\"");

WriteLiteral(" href=\"../../favicon.ico\"");

WriteLiteral(">\r\n    <title>eventcombo</title>\r\n    <!-- Bootstrap core CSS -->\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 553), Tuple.Create("\"", 583)
, Tuple.Create(Tuple.Create("", 560), Tuple.Create<System.Object, System.Int32>(Href("~/Content/bootstrap.css")
, 560), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n\r\n    <!-- Custom styles for this template -->\r\n   \r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 668), Tuple.Create("\"", 699)
, Tuple.Create(Tuple.Create("", 675), Tuple.Create<System.Object, System.Int32>(Href("~/Content/eventcombo.css")
, 675), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 731), Tuple.Create("\"", 773)
, Tuple.Create(Tuple.Create("", 738), Tuple.Create<System.Object, System.Int32>(Href("~/Content/bootstrap-multiselect.css")
, 738), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 805), Tuple.Create("\"", 835)
, Tuple.Create(Tuple.Create("", 812), Tuple.Create<System.Object, System.Int32>(Href("~/Content/ec-select.css")
, 812), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 867), Tuple.Create("\"", 898)
, Tuple.Create(Tuple.Create("", 874), Tuple.Create<System.Object, System.Int32>(Href("~/Content/summernote.css")
, 874), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n   \r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 935), Tuple.Create("\"", 966)
, Tuple.Create(Tuple.Create("", 942), Tuple.Create<System.Object, System.Int32>(Href("~/Content/datepicker.css")
, 942), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 998), Tuple.Create("\"", 1036)
, Tuple.Create(Tuple.Create("", 1005), Tuple.Create<System.Object, System.Int32>(Href("~/Content/jquery.timepicker.css")
, 1005), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(@" />
    <!--  <link href=""http://netdna.bootstrapcdn.com/font-awesome/4.1.0/css/font-awesome.min.css"" rel=""stylesheet""> -->
    <!--[if lt IE 9]>
      <script src=""https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js""></script>
      <script src=""https://oss.maxcdn.com/respond/1.4.2/respond.min.js""></script>
    <![endif]-->
    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1402), Tuple.Create("\"", 1431)
, Tuple.Create(Tuple.Create("", 1408), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/Validation.js")
, 1408), false)
);

WriteLiteral("></script>\r\n    ");

WriteLiteral("\r\n    <script");

WriteLiteral(" src=\"https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js\"");

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1610), Tuple.Create("\"", 1642)
, Tuple.Create(Tuple.Create("", 1616), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/bootstrap.min.js")
, 1616), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1666), Tuple.Create("\"", 1689)
, Tuple.Create(Tuple.Create("", 1672), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/main.js")
, 1672), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1713), Tuple.Create("\"", 1746)
, Tuple.Create(Tuple.Create("", 1719), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/summernote.min.js")
, 1719), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1770), Tuple.Create("\"", 1809)
, Tuple.Create(Tuple.Create("", 1776), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/bootstrap-datepicker.js")
, 1776), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1833), Tuple.Create("\"", 1869)
, Tuple.Create(Tuple.Create("", 1839), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/jquery.timepicker.js")
, 1839), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create("  src=\"", 1893), Tuple.Create("\"", 1927)
, Tuple.Create(Tuple.Create("", 1900), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/DateTimePicker.js")
, 1900), false)
);

WriteLiteral("></script>\r\n\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1953), Tuple.Create("\"", 1993)
, Tuple.Create(Tuple.Create("", 1959), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/bootstrap-multiselect.js")
, 1959), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 2017), Tuple.Create("\"", 2045)
, Tuple.Create(Tuple.Create("", 2023), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/jquery-ui.js")
, 2023), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 2069), Tuple.Create("\"", 2097)
, Tuple.Create(Tuple.Create("", 2075), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/ec-select.js")
, 2075), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 2121), Tuple.Create("\"", 2153)
, Tuple.Create(Tuple.Create("", 2127), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/jquery.cookie.js")
, 2127), false)
);

WriteLiteral("></script>\r\n\r\n</head>\r\n<body>\r\n    ");

WriteLiteral("\r\n    <div");

WriteLiteral(" class=\"container-fluid headr_main fix_head\"");

WriteLiteral(" id=\"mainhead\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-sm-6\"");

WriteLiteral(">\r\n                <a");

WriteLiteral(" class=\"logo\"");

WriteLiteral(" href=\"/Home/Index\"");

WriteLiteral("><img");

WriteAttribute("src", Tuple.Create(" src=\"", 2396), Tuple.Create("\"", 2419)
, Tuple.Create(Tuple.Create("", 2402), Tuple.Create<System.Object, System.Int32>(Href("~/Images/logo.png")
, 2402), false)
);

WriteLiteral(" /></a>\r\n            </div>\r\n");

            
            #line 53 "..\..\Views\Shared\_AccountLayout.cshtml"
 if (HttpContext.Current.Session["AppId"] != null)
{

            
            #line default
            #line hidden
WriteLiteral("            <div");

WriteLiteral(" class=\"col-sm-5 head_left_nav\"");

WriteLiteral(">\r\n                <ul");

WriteLiteral(" class=\"navbar-right frnt-top-header  xs-signup-height\"");

WriteLiteral(">\r\n                    <li");

WriteLiteral(" class=\"msg_icn\"");

WriteLiteral("><span>12</span> </li>\r\n                    <li><a");

WriteLiteral(" href=\"#\"");

WriteLiteral(">Welcome ");

            
            #line 58 "..\..\Views\Shared\_AccountLayout.cshtml"
                                              Html.RenderAction("UserName", "Home");
            
            #line default
            #line hidden
WriteLiteral("!</a> </li>\r\n                    <li>\r\n");

WriteLiteral("                        ");

            
            #line 60 "..\..\Views\Shared\_AccountLayout.cshtml"
                   Write(Html.ActionLink("My Account", "MyAccount", "Account", routeValues: null, htmlAttributes: new { title = "Account" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </li>\r\n                    <li>\r\n");

            
            #line 63 "..\..\Views\Shared\_AccountLayout.cshtml"
                        
            
            #line default
            #line hidden
            
            #line 63 "..\..\Views\Shared\_AccountLayout.cshtml"
                         using (Html.BeginForm("LogOff", "Account", FormMethod.Post, new { id = "logoutForm" }))
                        {

            
            #line default
            #line hidden
WriteLiteral("                            <a");

WriteLiteral(" href=\"javascript:document.getElementById(\'logoutForm\').submit()\"");

WriteLiteral(">Log Out </a>\r\n");

            
            #line 66 "..\..\Views\Shared\_AccountLayout.cshtml"
                        }

            
            #line default
            #line hidden
WriteLiteral("                    </li>\r\n                </ul>\r\n            </div>\r\n");

            
            #line 70 "..\..\Views\Shared\_AccountLayout.cshtml"
                        }
                        else
                        {


            
            #line default
            #line hidden
WriteLiteral("                            <div");

WriteLiteral(" class=\"col-sm-5 head_left_nav clearfix\"");

WriteLiteral(">\r\n                                <ul");

WriteLiteral(" class=\"navbar-right frnt-top-header xs-signup-height\"");

WriteLiteral(">\r\n\r\n                                    <li><a");

WriteLiteral(" href=\"javascript:void(0)\"");

WriteLiteral(" data-toggle=\"modal\"");

WriteLiteral(" data-target=\"#myModal\"");

WriteLiteral(" id=\"loginPopup\"");

WriteLiteral(">LogIn | SignUp </a>  </li>\r\n\r\n                                </ul>\r\n\r\n         " +
"                   </div>\r\n");

            
            #line 82 "..\..\Views\Shared\_AccountLayout.cshtml"
                        }

            
            #line default
            #line hidden
WriteLiteral("\r\n            <div");

WriteLiteral(" class=\"col-sm-1 text-right\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"social_icn\"");

WriteLiteral(">\r\n                    <a");

WriteLiteral(" href=\"http://www.facebook.com/Eventcombo\"");

WriteLiteral(" target=\"_blank\"");

WriteLiteral("><img");

WriteAttribute("src", Tuple.Create(" src=\"", 4054), Tuple.Create("\"", 4075)
, Tuple.Create(Tuple.Create("", 4060), Tuple.Create<System.Object, System.Int32>(Href("~/Images/fb.png")
, 4060), false)
);

WriteLiteral(" /></a>\r\n                    <a");

WriteLiteral(" href=\"http://www.twitter.com/Eventcombo\"");

WriteLiteral(" target=\"_blank\"");

WriteLiteral("><img");

WriteAttribute("src", Tuple.Create(" src=\"", 4169), Tuple.Create("\"", 4190)
, Tuple.Create(Tuple.Create("", 4175), Tuple.Create<System.Object, System.Int32>(Href("~/Images/tw.png")
, 4175), false)
);

WriteLiteral(" /></a>\r\n                    ");

WriteLiteral("\r\n                    ");

WriteLiteral("\r\n                </div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"Fix_Head mt10\"");

WriteLiteral(" title=\"Turn on to fix header\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"switch\"");

WriteLiteral(">\r\n");

            
            #line 95 "..\..\Views\Shared\_AccountLayout.cshtml"
                  
            
            #line default
            #line hidden
            
            #line 95 "..\..\Views\Shared\_AccountLayout.cshtml"
                    

                      string header = CookieStore.GetCookie("ckHeader");


            
            #line default
            #line hidden
WriteLiteral("                    <input");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("value", Tuple.Create(" value=", 4705), Tuple.Create("", 4746)
            
            #line 99 "..\..\Views\Shared\_AccountLayout.cshtml"
, Tuple.Create(Tuple.Create("", 4712), Tuple.Create<System.Object, System.Int32>(CookieStore.GetCookie("ckHeader")
            
            #line default
            #line hidden
, 4712), false)
);

WriteLiteral(" id=\"hdheader\"");

WriteLiteral(" />\r\n");

            
            #line 100 "..\..\Views\Shared\_AccountLayout.cshtml"
                      if ((header != null) && (header != ""))
                      {
                          if (header == "on")
                          {

            
            #line default
            #line hidden
WriteLiteral("                            <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" class=\"switch-input\"");

WriteLiteral(" id=\"idchecked\"");

WriteLiteral(" checked");

WriteLiteral(" onchange=\"changeheader(this.id);\"");

WriteLiteral(">\r\n");

            
            #line 105 "..\..\Views\Shared\_AccountLayout.cshtml"
                          }
                          else
                          {

            
            #line default
            #line hidden
WriteLiteral("                            <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" class=\"switch-input\"");

WriteLiteral(" id=\"idchecked\"");

WriteLiteral(" onchange=\"changeheader(this.id);\"");

WriteLiteral(">\r\n");

            
            #line 109 "..\..\Views\Shared\_AccountLayout.cshtml"
                          }
                      }
                      else
                      {

            
            #line default
            #line hidden
WriteLiteral("                        <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" class=\"switch-input\"");

WriteLiteral(" id=\"idchecked\"");

WriteLiteral(" onchange=\"changeheader(this.id);\"");

WriteLiteral(">\r\n");

            
            #line 114 "..\..\Views\Shared\_AccountLayout.cshtml"
                      }

                
            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n                    <span");

WriteLiteral(" class=\"switch-label\"");

WriteLiteral(" data-on=\"On\"");

WriteLiteral(" data-off=\"Off\"");

WriteLiteral(" ></span>\r\n                    <span");

WriteLiteral(" class=\"switch-handle\"");

WriteLiteral("></span>\r\n                </label>\r\n            </div>\r\n        </div>\r\n        <" +
"div");

WriteLiteral(" class=\"col-sm-12 filter_sep cont_margin\"");

WriteLiteral(">\r\n            <img");

WriteAttribute("src", Tuple.Create(" src=\"", 5823), Tuple.Create("\"", 5906)
, Tuple.Create(Tuple.Create("", 5829), Tuple.Create<System.Object, System.Int32>(Href("~/Images/desi-")
, 5829), false)
            
            #line 124 "..\..\Views\Shared\_AccountLayout.cshtml"
, Tuple.Create(Tuple.Create("", 5843), Tuple.Create<System.Object, System.Int32>(HttpContext.Current.Session["logo"].ToString().Trim()
            
            #line default
            #line hidden
, 5843), false)
, Tuple.Create(Tuple.Create("", 5897), Tuple.Create("-logo.gif", 5897), true)
);

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"cus_left_kick\"");

WriteLiteral("><</div>\r\n          \r\n        </div>\r\n\r\n    </div>\r\n\r\n   <div");

WriteLiteral(" class=\"container-fluid cont_margin\"");

WriteLiteral(" id=\"bodysec\"");

WriteLiteral(">\r\n");

            
            #line 132 "..\..\Views\Shared\_AccountLayout.cshtml"
        
            
            #line default
            #line hidden
            
            #line 132 "..\..\Views\Shared\_AccountLayout.cshtml"
         if (HttpContext.Current.Session["Fromname"].ToString() == "events" || HttpContext.Current.Session["Fromname"].ToString() == "deals")
        {
            
            
            #line default
            #line hidden
            
            #line 134 "..\..\Views\Shared\_AccountLayout.cshtml"
       Write(RenderBody());

            
            #line default
            #line hidden
            
            #line 134 "..\..\Views\Shared\_AccountLayout.cshtml"
                         
         

        }
        else
        {



            
            #line default
            #line hidden
WriteLiteral("        <div");

WriteLiteral(" class=\"row xs-ip6\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-sm-2 nav_ipad_left\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"affix-top\"");

WriteLiteral(" id=\"navtop\"");

WriteLiteral(">\r\n                    <nav");

WriteLiteral(" class=\"navbar navbar-default nav_cus_main \"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"navbar-header\"");

WriteLiteral(">\r\n                            <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"navbar-toggle collapsed nav_btn\"");

WriteLiteral(" data-toggle=\"collapse\"");

WriteLiteral(" data-target=\"#navbar\"");

WriteLiteral(" aria-expanded=\"false\"");

WriteLiteral(" aria-controls=\"navbar\"");

WriteLiteral(">\r\n                                <span");

WriteLiteral(" class=\"sr-only\"");

WriteLiteral(">Toggle navigation</span>\r\n                                <span");

WriteLiteral(" class=\"icon-bar\"");

WriteLiteral("></span>\r\n                                <span");

WriteLiteral(" class=\"icon-bar\"");

WriteLiteral("></span>\r\n                                <span");

WriteLiteral(" class=\"icon-bar\"");

WriteLiteral("></span>\r\n                            </button>\r\n\r\n                        </div>" +
"\r\n                        <div");

WriteLiteral(" id=\"navbar\"");

WriteLiteral(" class=\"navbar-collapse collapse nav_cus\"");

WriteLiteral(">\r\n");

            
            #line 156 "..\..\Views\Shared\_AccountLayout.cshtml"
                            
            
            #line default
            #line hidden
            
            #line 156 "..\..\Views\Shared\_AccountLayout.cshtml"
                             if (IsSectionDefined("sidenav"))
                            {
                                
            
            #line default
            #line hidden
            
            #line 158 "..\..\Views\Shared\_AccountLayout.cshtml"
                           Write(RenderSection("sidenav", false));

            
            #line default
            #line hidden
            
            #line 158 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                                
                            }
                            else
                            {

            
            #line default
            #line hidden
WriteLiteral("                                <div");

WriteLiteral(" id=\"sidebar\"");

WriteLiteral(" class=\"nav nav-stacked\"");

WriteLiteral(">\r\n                                    <div");

WriteLiteral(" id=\"accordion\"");

WriteLiteral(" class=\"panel-group\"");

WriteLiteral(">\r\n\r\n                                        <div");

WriteLiteral(" class=\"panel panel-default menu_list\"");

WriteLiteral(">\r\n                                            <a");

WriteLiteral(" id=\"mgacnt\"");

WriteLiteral(" class=\"menu_title\"");

WriteLiteral(" data-toggle=\"collapse\"");

WriteLiteral(" data-parent=\"#accordion\"");

WriteLiteral(" href=\"#collapse1\"");

WriteLiteral(">\r\n                                              <div");

WriteLiteral(" id=\"dvacmang\"");

WriteAttribute("class", Tuple.Create(" class=\"", 7874), Tuple.Create("\"", 7947)
, Tuple.Create(Tuple.Create("", 7882), Tuple.Create("panel-heading", 7882), true)
            
            #line 167 "..\..\Views\Shared\_AccountLayout.cshtml"
      , Tuple.Create(Tuple.Create("", 7895), Tuple.Create<System.Object, System.Int32>(ViewBag.CurrentItem == "account" ? " active" : ""
            
            #line default
            #line hidden
, 7895), false)
);

WriteLiteral(">\r\n                                                Account <b");

WriteLiteral(" class=\"closed\"");

WriteLiteral("></b>\r\n                                              </div>\r\n                    " +
"                        </a>\r\n\r\n                                          <div");

WriteLiteral(" id=\"collapse1\"");

WriteAttribute("class", Tuple.Create(" class=\"", 8198), Tuple.Create("\"", 8277)
, Tuple.Create(Tuple.Create("", 8206), Tuple.Create("panel-collapse", 8206), true)
, Tuple.Create(Tuple.Create(" ", 8220), Tuple.Create("collapse", 8221), true)
            
            #line 172 "..\..\Views\Shared\_AccountLayout.cshtml"
             , Tuple.Create(Tuple.Create("", 8229), Tuple.Create<System.Object, System.Int32>(ViewBag.CurrentItem == "account" ? " in" : ""
            
            #line default
            #line hidden
, 8229), false)
);

WriteLiteral(">\r\n                                            <div");

WriteLiteral(" class=\"panel-body no-padding\"");

WriteLiteral(">\r\n                                              <p>");

            
            #line 174 "..\..\Views\Shared\_AccountLayout.cshtml"
                                            Write(Html.ActionLink("Personal Information", "MyAccount", "Account"));

            
            #line default
            #line hidden
WriteLiteral(" </p>\r\n                                              <p>");

            
            #line 175 "..\..\Views\Shared\_AccountLayout.cshtml"
                                            Write(Html.ActionLink("My Purchased Ticket", "PurchasedTicket", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                              <p>");

            
            #line 176 "..\..\Views\Shared\_AccountLayout.cshtml"
                                            Write(Html.ActionLink("My Purchased Deals", "PurchasedDeals", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                              <p>");

            
            #line 177 "..\..\Views\Shared\_AccountLayout.cshtml"
                                            Write(Html.ActionLink("Manage Organizer", "OrganizerProfile", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                            </div>\r\n                       " +
"                   </div>\r\n                                        </div>\r\n     " +
"                                   <div");

WriteLiteral(" class=\"panel panel-default menu_list\"");

WriteLiteral(">\r\n\r\n                                            <a");

WriteLiteral(" id=\"mgnav\"");

WriteLiteral(" class=\"menu_title\"");

WriteLiteral(" data-toggle=\"collapse\"");

WriteLiteral(" data-parent=\"#accordion\"");

WriteLiteral(" href=\"#collapse2\"");

WriteLiteral(">\r\n                                                <div");

WriteLiteral(" id=\"dvManage\"");

WriteLiteral(" class=\"panel-heading\"");

WriteLiteral(">\r\n                                                    Manage Events <b");

WriteLiteral(" class=\"closed\"");

WriteLiteral("></b>\r\n                                                </div>\r\n                  " +
"                          </a>\r\n\r\n                                            <d" +
"iv");

WriteLiteral(" id=\"collapse2\"");

WriteLiteral(" class=\"panel-collapse collapse\"");

WriteLiteral(">\r\n                                                <div");

WriteLiteral(" class=\"panel-body no-padding\"");

WriteLiteral(">\r\n                                                    <p>");

            
            #line 191 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Create Event", "CreateEvent", "CreateEvent"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 192 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("My Events", "EventList", "EventList"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    ");

WriteLiteral("\r\n                                                    ");

WriteLiteral("\r\n                                                    <p>");

            
            #line 195 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Events Help", "EventHelp", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n\r\n                                                    ");

WriteLiteral("\r\n                                                    ");

WriteLiteral("\r\n                                                    ");

WriteLiteral("\r\n                                                    ");

WriteLiteral("\r\n                                                </div>\r\n                       " +
"                     </div>\r\n                                        </div>\r\n   " +
"                                     <div");

WriteLiteral(" class=\"panel panel-default menu_list\"");

WriteLiteral(">\r\n\r\n                                            <a");

WriteLiteral(" id=\"dealnav\"");

WriteLiteral(" class=\"menu_title\"");

WriteLiteral(" data-toggle=\"collapse\"");

WriteLiteral(" data-parent=\"#accordion\"");

WriteLiteral(" href=\"#collapse3\"");

WriteLiteral(">\r\n                                                <div");

WriteLiteral(" id=\"dvmngdeal\"");

WriteLiteral(" class=\"panel-heading\"");

WriteLiteral(">\r\n                                                    Manage Deals <b");

WriteLiteral(" class=\"closed\"");

WriteLiteral("></b>\r\n                                                </div>\r\n                  " +
"                          </a>\r\n\r\n                                            <d" +
"iv");

WriteLiteral(" id=\"collapse3\"");

WriteLiteral(" class=\"panel-collapse collapse\"");

WriteLiteral(">\r\n                                                <div");

WriteLiteral(" class=\"panel-body no-padding\"");

WriteLiteral(">\r\n                                                    <p>Create Deal</p>\r\n      " +
"                                              <p>");

            
            #line 215 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Deals Dashboard", "DealsDashboard", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 216 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Current Deals", "CurrentDeals", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 217 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Past Deals", "PastDeals", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 218 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Feedback", "Feedback", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 219 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Overview", "Overview", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 220 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Impact Report", "ImpactReport", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 221 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Invoice History", "InvoiceHistory", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 222 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Customers", "Customers", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 223 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Deals Help", "DealsHelp", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 224 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Merchant Profile", "MerchantProfile", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                </div>\r\n                   " +
"                         </div>\r\n                                        </div>\r" +
"\n                                    </div>\r\n                                </d" +
"iv>\r\n");

            
            #line 230 "..\..\Views\Shared\_AccountLayout.cshtml"
                            }

            
            #line default
            #line hidden
WriteLiteral("                                  \r\n                                  \r\n         " +
"           </div>\r\n            </nav>\r\n                </div>\r\n            </div" +
">\r\n            <div");

WriteLiteral(" class=\"col-sm-10 nav_ipad_right\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"midd_cont\"");

WriteLiteral(" id=\"midsection\"");

WriteLiteral(">\r\n                    \r\n");

WriteLiteral("                    ");

            
            #line 240 "..\..\Views\Shared\_AccountLayout.cshtml"
               Write(RenderBody());

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n                    </div>\r\n                </div>\r\n            </div>\r\n");

            
            #line 245 "..\..\Views\Shared\_AccountLayout.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n    <div");

WriteLiteral(" id=\"dtBox\"");

WriteLiteral("></div>\r\n\r\n\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        $(window).load(function () {\r\n\r\n            var headervale = $(\"#hdhea" +
"der\").val();;\r\n            //alert(headervale);\r\n            if (headervale != u" +
"ndefined && headervale != null && headervale != \'\') {\r\n\r\n                if (hea" +
"dervale == \"on\") {\r\n                    $(\'#mainhead\').addClass(\'fix_head\');\r\n  " +
"                  $(\'#bodysec\').addClass(\'cont_margin\');\r\n                    $(" +
"\'#navtop\').addClass(\'affix-top\');\r\n                    $(\'#midsection\').addClass" +
"(\'midd_cont\');\r\n                    $(\'#eventtop\').addClass(\'event_top\');\r\n     " +
"               $(\'#eventtopscoll\').addClass(\'event_top\');\r\n\r\n                   " +
" $(\'#divsuccerr\').addClass(\'aler_container\');\r\n                    $(\'#eventtops" +
"coll\').addClass(\'viewevent_container\');\r\n                    //if ($(\'#idchecked" +
"\').attr(\'checked\')) {\r\n\r\n                    //} else {\r\n                    // " +
"   $(\"#idchecked\").attr(\'checked\', true);\r\n                    //}\r\n            " +
"        //$(\"#idchecked\").attr(\'checked\', true);\r\n                }\r\n           " +
"     else {\r\n                    $(\'#mainhead\').removeClass(\'fix_head\');\r\n      " +
"              $(\'#bodysec\').removeClass(\'cont_margin\');\r\n                    $(\'" +
"#navtop\').removeClass(\'affix-top\');\r\n                    $(\'#midsection\').remove" +
"Class(\'midd_cont\');\r\n                    $(\'#eventtopscoll\').removeClass(\'event_" +
"top\');\r\n                    $(\'#eventtop\').removeClass(\'event_top\');\r\n          " +
"          $(\'#divsuccerr\').removeClass(\'aler_container\');\r\n\r\n                   " +
" $(\'#eventtopscoll\').removeClass(\'viewevent_container\');\r\n                    //" +
"$(\"#idchecked\").attr(\'checked\', false);\r\n                    //if ($(\'#idchecked" +
"\').attr(\'checked\')) {\r\n\r\n                    //} else {\r\n                    // " +
"   $(\"#idchecked\").attr(\'checked\', false);\r\n                    //}\r\n\r\n         " +
"       }\r\n            } else {\r\n                $(\'#mainhead\').removeClass(\'fix_" +
"head\');\r\n                $(\'#bodysec\').removeClass(\'cont_margin\');\r\n            " +
"    $(\'#navtop\').removeClass(\'affix-top\');\r\n                $(\'#midsection\').rem" +
"oveClass(\'midd_cont\');\r\n                $(\'#eventtopscoll\').removeClass(\'event_t" +
"op\');\r\n                $(\'#eventtop\').removeClass(\'event_top\');\r\n               " +
" $(\'#divsuccerr\').removeClass(\'aler_container\');\r\n\r\n                $(\'#eventtop" +
"scoll\').removeClass(\'viewevent_container\');;\r\n                //if ($(\'#idchecke" +
"d\').attr(\'checked\')) {\r\n\r\n                //} else {\r\n                //    $(\"#" +
"idchecked\").attr(\'checked\', false);\r\n                //}\r\n                //$(\"#" +
"idchecked\").attr(\'checked\', false);\r\n            }\r\n\r\n\r\n\r\n\r\n        \r\n\r\n        " +
"});\r\n\r\n        function checkPermission(PermisionId) {\r\n            ");

WriteLiteral("\r\n\r\n\r\n\r\n        }\r\n\r\n\r\n\r\n\r\n    function changeheader(e)\r\n    {\r\n        if ($(\'#i" +
"dchecked\').is(\":checked\") == true) {\r\n            $.ajax({\r\n                url:" +
" \'");

            
            #line 352 "..\..\Views\Shared\_AccountLayout.cshtml"
                 Write(Url.Action("Setheader", "Home"));

            
            #line default
            #line hidden
WriteLiteral(@"',
                type: ""Post"",
                data: { header: ""on"" },
                success: function (data) {


                },
                error: function (data) {
                    //alert(""Sorry there is some problem."");
                    return false
                }
            });
       
            //$.cookie(""header"", ""on"", { expire: 1 });
            $('#mainhead').addClass('fix_head');
            $('#bodysec').addClass('cont_margin');
            $('#navtop').addClass('affix-top');
            $('#midsection').addClass('midd_cont');
            $('#eventtop').addClass('event_top');
            $('#eventtopscoll').addClass('event_top');
            
            $('#divsuccerr').addClass('aler_container');
            $('#eventtopscoll').addClass('viewevent_container');
        } else
        {
            $.ajax({
                url: '");

            
            #line 378 "..\..\Views\Shared\_AccountLayout.cshtml"
                 Write(Url.Action("Setheader", "Home"));

            
            #line default
            #line hidden
WriteLiteral(@"',
                type: ""Post"",
                data: { header: ""off"" },
                success: function (data) {


                },
                error: function (data) {
                    //alert(""Sorry there is some problem."");
                    return false
                }
            });
            //$.cookie(""header"", ""off"", { expire: 1 });
            $('#mainhead').removeClass('fix_head');
            $('#bodysec').removeClass('cont_margin');
            $('#navtop').removeClass('affix-top');
            $('#midsection').removeClass('midd_cont');
            $('#eventtopscoll').removeClass('event_top');
            $('#eventtop').removeClass('event_top');
            $('#divsuccerr').removeClass('aler_container');
            
            $('#eventtopscoll').removeClass('viewevent_container');
        }
    }
    </script>
</body>

</html>



");

        }
    }
}
#pragma warning restore 1591
