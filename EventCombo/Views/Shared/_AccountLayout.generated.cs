﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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

WriteAttribute("href", Tuple.Create(" href=\"", 523), Tuple.Create("\"", 553)
, Tuple.Create(Tuple.Create("", 530), Tuple.Create<System.Object, System.Int32>(Href("~/Content/bootstrap.css")
, 530), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n\r\n    <!-- Custom styles for this template -->\r\n   \r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 638), Tuple.Create("\"", 669)
, Tuple.Create(Tuple.Create("", 645), Tuple.Create<System.Object, System.Int32>(Href("~/Content/eventcombo.css")
, 645), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 701), Tuple.Create("\"", 743)
, Tuple.Create(Tuple.Create("", 708), Tuple.Create<System.Object, System.Int32>(Href("~/Content/bootstrap-multiselect.css")
, 708), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 775), Tuple.Create("\"", 805)
, Tuple.Create(Tuple.Create("", 782), Tuple.Create<System.Object, System.Int32>(Href("~/Content/ec-select.css")
, 782), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 837), Tuple.Create("\"", 868)
, Tuple.Create(Tuple.Create("", 844), Tuple.Create<System.Object, System.Int32>(Href("~/Content/summernote.css")
, 844), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n   \r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 905), Tuple.Create("\"", 936)
, Tuple.Create(Tuple.Create("", 912), Tuple.Create<System.Object, System.Int32>(Href("~/Content/datepicker.css")
, 912), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 968), Tuple.Create("\"", 1006)
, Tuple.Create(Tuple.Create("", 975), Tuple.Create<System.Object, System.Int32>(Href("~/Content/jquery.timepicker.css")
, 975), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(@" />
    <!--  <link href=""http://netdna.bootstrapcdn.com/font-awesome/4.1.0/css/font-awesome.min.css"" rel=""stylesheet""> -->
    <!--[if lt IE 9]>
      <script src=""https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js""></script>
      <script src=""https://oss.maxcdn.com/respond/1.4.2/respond.min.js""></script>
    <![endif]-->
    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1372), Tuple.Create("\"", 1401)
, Tuple.Create(Tuple.Create("", 1378), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/Validation.js")
, 1378), false)
);

WriteLiteral("></script>\r\n    ");

WriteLiteral("\r\n    <script");

WriteLiteral(" src=\"https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js\"");

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1580), Tuple.Create("\"", 1612)
, Tuple.Create(Tuple.Create("", 1586), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/bootstrap.min.js")
, 1586), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1636), Tuple.Create("\"", 1659)
, Tuple.Create(Tuple.Create("", 1642), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/main.js")
, 1642), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1683), Tuple.Create("\"", 1716)
, Tuple.Create(Tuple.Create("", 1689), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/summernote.min.js")
, 1689), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1740), Tuple.Create("\"", 1779)
, Tuple.Create(Tuple.Create("", 1746), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/bootstrap-datepicker.js")
, 1746), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1803), Tuple.Create("\"", 1839)
, Tuple.Create(Tuple.Create("", 1809), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/jquery.timepicker.js")
, 1809), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create("  src=\"", 1863), Tuple.Create("\"", 1897)
, Tuple.Create(Tuple.Create("", 1870), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/DateTimePicker.js")
, 1870), false)
);

WriteLiteral("></script>\r\n\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1923), Tuple.Create("\"", 1963)
, Tuple.Create(Tuple.Create("", 1929), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/bootstrap-multiselect.js")
, 1929), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1987), Tuple.Create("\"", 2015)
, Tuple.Create(Tuple.Create("", 1993), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/jquery-ui.js")
, 1993), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 2039), Tuple.Create("\"", 2067)
, Tuple.Create(Tuple.Create("", 2045), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/ec-select.js")
, 2045), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 2091), Tuple.Create("\"", 2123)
, Tuple.Create(Tuple.Create("", 2097), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/jquery.cookie.js")
, 2097), false)
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

WriteAttribute("src", Tuple.Create(" src=\"", 2366), Tuple.Create("\"", 2389)
, Tuple.Create(Tuple.Create("", 2372), Tuple.Create<System.Object, System.Int32>(Href("~/Images/logo.png")
, 2372), false)
);

WriteLiteral(" /></a>\r\n            </div>\r\n");

            
            #line 52 "..\..\Views\Shared\_AccountLayout.cshtml"
 if (HttpContext.Current.Session["AppId"] != null)
{

            
            #line default
            #line hidden
WriteLiteral("            <div");

WriteLiteral(" class=\"col-sm-5 head_left_nav\"");

WriteLiteral(">\r\n                <ul");

WriteLiteral(" class=\"navbar-right frnt-top-header  xs-signup-height\"");

WriteLiteral(">\r\n\r\n\r\n\r\n\r\n                    <li");

WriteLiteral(" class=\"msg_icn\"");

WriteLiteral("><span>12</span> </li>\r\n\r\n                    <li><a");

WriteLiteral(" href=\"#\"");

WriteLiteral(">Welcome ");

            
            #line 62 "..\..\Views\Shared\_AccountLayout.cshtml"
                                              Html.RenderAction("UserName", "Home");
            
            #line default
            #line hidden
WriteLiteral("!</a> </li>\r\n                    <li>\r\n");

WriteLiteral("                        ");

            
            #line 64 "..\..\Views\Shared\_AccountLayout.cshtml"
                   Write(Html.ActionLink("My Account", "MyAccount", "Account", routeValues: null, htmlAttributes: new { title = "Account" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </li>\r\n                    <li>\r\n");

            
            #line 67 "..\..\Views\Shared\_AccountLayout.cshtml"
                        
            
            #line default
            #line hidden
            
            #line 67 "..\..\Views\Shared\_AccountLayout.cshtml"
                         using (Html.BeginForm("LogOff", "Account", FormMethod.Post, new { id = "logoutForm" }))
                        {

            
            #line default
            #line hidden
WriteLiteral("                            <a");

WriteLiteral(" href=\"javascript:document.getElementById(\'logoutForm\').submit()\"");

WriteLiteral(">Log Out </a>\r\n");

            
            #line 70 "..\..\Views\Shared\_AccountLayout.cshtml"
                        }

            
            #line default
            #line hidden
WriteLiteral("                    </li>\r\n\r\n\r\n\r\n\r\n\r\n\r\n                </ul>\r\n            </div>\r" +
"\n");

            
            #line 80 "..\..\Views\Shared\_AccountLayout.cshtml"
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

            
            #line 92 "..\..\Views\Shared\_AccountLayout.cshtml"
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

WriteAttribute("src", Tuple.Create(" src=\"", 4046), Tuple.Create("\"", 4067)
, Tuple.Create(Tuple.Create("", 4052), Tuple.Create<System.Object, System.Int32>(Href("~/Images/fb.png")
, 4052), false)
);

WriteLiteral(" /></a>\r\n                    <a");

WriteLiteral(" href=\"http://www.twitter.com/Eventcombo\"");

WriteLiteral(" target=\"_blank\"");

WriteLiteral("><img");

WriteAttribute("src", Tuple.Create(" src=\"", 4161), Tuple.Create("\"", 4182)
, Tuple.Create(Tuple.Create("", 4167), Tuple.Create<System.Object, System.Int32>(Href("~/Images/tw.png")
, 4167), false)
);

WriteLiteral(" /></a>\r\n                    ");

WriteLiteral("\r\n                    ");

WriteLiteral("\r\n                </div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"Fix_Head mt10\"");

WriteLiteral(" title=\"Turn on to fix header\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"switch\"");

WriteLiteral(">\r\n");

            
            #line 105 "..\..\Views\Shared\_AccountLayout.cshtml"
                    
            
            #line default
            #line hidden
            
            #line 105 "..\..\Views\Shared\_AccountLayout.cshtml"
                     if (Session["Header"] == null)
                    {

            
            #line default
            #line hidden
WriteLiteral("                        <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" class=\"switch-input\"");

WriteLiteral(" checked");

WriteLiteral(" id=\"idchecked\"");

WriteLiteral(" onchange=\"changeheader(this.id);\"");

WriteLiteral(">\r\n");

            
            #line 108 "..\..\Views\Shared\_AccountLayout.cshtml"

                    }
                    else
                    {
                        if (Session["Header"].ToString() == " " || Session["Header"].ToString() == "on")
                        {

            
            #line default
            #line hidden
WriteLiteral("                            <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" class=\"switch-input\"");

WriteLiteral(" checked");

WriteLiteral(" id=\"idchecked\"");

WriteLiteral(" onchange=\"changeheader(this.id);\"");

WriteLiteral(">\r\n");

            
            #line 115 "..\..\Views\Shared\_AccountLayout.cshtml"

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

            
            #line 120 "..\..\Views\Shared\_AccountLayout.cshtml"
                        }
                    }

            
            #line default
            #line hidden
WriteLiteral("\r\n                    <span");

WriteLiteral(" class=\"switch-label\"");

WriteLiteral(" data-on=\"On\"");

WriteLiteral(" data-off=\"Off\"");

WriteLiteral(" ></span>\r\n                    <span");

WriteLiteral(" class=\"switch-handle\"");

WriteLiteral("></span>\r\n                </label>\r\n            </div>\r\n        </div>\r\n        <" +
"div");

WriteLiteral(" class=\"col-sm-12 filter_sep cont_margin\"");

WriteLiteral(">\r\n            <img");

WriteAttribute("src", Tuple.Create(" src=\"", 5635), Tuple.Create("\"", 5718)
, Tuple.Create(Tuple.Create("", 5641), Tuple.Create<System.Object, System.Int32>(Href("~/Images/desi-")
, 5641), false)
            
            #line 129 "..\..\Views\Shared\_AccountLayout.cshtml"
, Tuple.Create(Tuple.Create("", 5655), Tuple.Create<System.Object, System.Int32>(HttpContext.Current.Session["logo"].ToString().Trim()
            
            #line default
            #line hidden
, 5655), false)
, Tuple.Create(Tuple.Create("", 5709), Tuple.Create("-logo.gif", 5709), true)
);

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"cus_left_kick\"");

WriteLiteral("><</div>\r\n          \r\n        </div>\r\n\r\n    </div>\r\n   <div");

WriteLiteral(" class=\"container-fluid cont_margin\"");

WriteLiteral(" id=\"bodysec\"");

WriteLiteral(">\r\n");

            
            #line 136 "..\..\Views\Shared\_AccountLayout.cshtml"
        
            
            #line default
            #line hidden
            
            #line 136 "..\..\Views\Shared\_AccountLayout.cshtml"
         if (HttpContext.Current.Session["Fromname"].ToString() == "events" || HttpContext.Current.Session["Fromname"].ToString() == "deals")
        {
            
            
            #line default
            #line hidden
            
            #line 138 "..\..\Views\Shared\_AccountLayout.cshtml"
       Write(RenderBody());

            
            #line default
            #line hidden
            
            #line 138 "..\..\Views\Shared\_AccountLayout.cshtml"
                         

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

            
            #line 159 "..\..\Views\Shared\_AccountLayout.cshtml"
                            
            
            #line default
            #line hidden
            
            #line 159 "..\..\Views\Shared\_AccountLayout.cshtml"
                             if (IsSectionDefined("sidenav"))
                            {
                                
            
            #line default
            #line hidden
            
            #line 161 "..\..\Views\Shared\_AccountLayout.cshtml"
                           Write(RenderSection("sidenav", false));

            
            #line default
            #line hidden
            
            #line 161 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                                
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

WriteLiteral(">\r\n                                                <div");

WriteLiteral(" id=\"dvacmang\"");

WriteLiteral(" class=\"panel-heading\"");

WriteLiteral(">\r\n                                                    Account <b");

WriteLiteral(" class=\"closed\"");

WriteLiteral("></b>\r\n                                                </div>\r\n                  " +
"                          </a>\r\n\r\n                                            <d" +
"iv");

WriteLiteral(" id=\"collapse1\"");

WriteLiteral(" class=\"panel-collapse collapse\"");

WriteLiteral(">\r\n                                                <div");

WriteLiteral(" class=\"panel-body no-padding\"");

WriteLiteral(">\r\n                                                    <p>");

            
            #line 177 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Personal Information", "MyAccount", "Account"));

            
            #line default
            #line hidden
WriteLiteral(" </p>\r\n                                                    <p>");

            
            #line 178 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("My Purchased Ticket", "PurchasedTicket", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 179 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("My Purchased Deals", "PurchasedDeals", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 180 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Payment Information", "PaymentInformation", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 181 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Manage Organizer", "OrganizerProfile", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                </div>\r\n                   " +
"                         </div>\r\n                                        </div>\r" +
"\n                                        <div");

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

            
            #line 195 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Create Event", "CreateEvent", "CreateEvent"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 196 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("My Events", "EventList", "EventList"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    ");

WriteLiteral("\r\n                                                    ");

WriteLiteral("\r\n                                                    <p>");

            
            #line 199 "..\..\Views\Shared\_AccountLayout.cshtml"
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

            
            #line 219 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Deals Dashboard", "DealsDashboard", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 220 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Current Deals", "CurrentDeals", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 221 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Past Deals", "PastDeals", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 222 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Feedback", "Feedback", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 223 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Overview", "Overview", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 224 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Impact Report", "ImpactReport", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 225 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Invoice History", "InvoiceHistory", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 226 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Customers", "Customers", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 227 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Deals Help", "DealsHelp", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                    <p>");

            
            #line 228 "..\..\Views\Shared\_AccountLayout.cshtml"
                                                  Write(Html.ActionLink("Merchant Profile", "MerchantProfile", "Account"));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                                </div>\r\n                   " +
"                         </div>\r\n                                        </div>\r" +
"\n                                    </div>\r\n                                </d" +
"iv>\r\n");

            
            #line 234 "..\..\Views\Shared\_AccountLayout.cshtml"
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

            
            #line 244 "..\..\Views\Shared\_AccountLayout.cshtml"
               Write(RenderBody());

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n                    </div>\r\n                </div>\r\n            </div>\r\n");

            
            #line 249 "..\..\Views\Shared\_AccountLayout.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n    <div");

WriteLiteral(" id=\"dtBox\"");

WriteLiteral("></div>\r\n\r\n\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        $(window).load(function () {\r\n            var sessionheader = \"");

            
            #line 257 "..\..\Views\Shared\_AccountLayout.cshtml"
                            Write(Session["Header"]);

            
            #line default
            #line hidden
WriteLiteral(@""";
        
            if (sessionheader == """" || sessionheader==""on"")
            {
                $('#mainhead').addClass('fix_head');
                $('#bodysec').addClass('cont_margin');
                $('#navtop').addClass('affix-top');
                $('#midsection').addClass('midd_cont');
                $('#eventtop').addClass('event_top');
                $('#eventtopscoll').addClass('event_top');

                $('#divsuccerr').addClass('aler_container');
                $('#eventtopscoll').addClass('viewevent_container');
           
          
            }
            if ( sessionheader == ""off"") {
                $('#mainhead').removeClass('fix_head');
                $('#bodysec').removeClass('cont_margin');
                $('#navtop').removeClass('affix-top');
                $('#midsection').removeClass('midd_cont');
                $('#eventtopscoll').removeClass('event_top');
                $('#eventtop').removeClass('event_top');
                $('#divsuccerr').removeClass('aler_container');

                $('#eventtopscoll').removeClass('viewevent_container');
            
            }

        });

        function checkPermission(PermisionId) {
            ");

WriteLiteral("\r\n\r\n\r\n\r\n        }\r\n\r\n\r\n\r\n\r\n    function changeheader(e)\r\n    {\r\n        if ($(\'#i" +
"dchecked\').is(\":checked\") == true) {\r\n            $.ajax({\r\n                url:" +
" \'");

            
            #line 322 "..\..\Views\Shared\_AccountLayout.cshtml"
                 Write(Url.Action("Setheader", "Home"));

            
            #line default
            #line hidden
WriteLiteral(@"',
                type: ""Post"",
                data: { header: ""on"" },
                success: function (data) {


                },
                error: function (data) {
                    alert(""Sorry there is some problem."");
                    return false
                }
            });

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

            
            #line 347 "..\..\Views\Shared\_AccountLayout.cshtml"
                 Write(Url.Action("Setheader", "Home"));

            
            #line default
            #line hidden
WriteLiteral(@"',
                type: ""Post"",
                data: { header: ""off"" },
                success: function (data) {


                },
                error: function (data) {
                    alert(""Sorry there is some problem."");
                    return false
                }
            });
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
