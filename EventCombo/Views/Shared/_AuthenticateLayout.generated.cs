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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/_AuthenticateLayout.cshtml")]
    public partial class _Views_Shared__AuthenticateLayout_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_Shared__AuthenticateLayout_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<!DOCTYPE html>\r\n<html");

WriteLiteral(" lang=\"en\"");

WriteLiteral(">\r\n<head>\r\n\r\n    <meta");

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

WriteLiteral(" href=\"#\"");

WriteLiteral(">\r\n    <title>eventcombo</title>\r\n    <!-- Bootstrap core CSS -->\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 509), Tuple.Create("\"", 539)
, Tuple.Create(Tuple.Create("", 516), Tuple.Create<System.Object, System.Int32>(Href("~/Content/bootstrap.css")
, 516), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n    <!-- Custom styles for this template -->\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 617), Tuple.Create("\"", 648)
, Tuple.Create(Tuple.Create("", 624), Tuple.Create<System.Object, System.Int32>(Href("~/Content/eventcombo.css")
, 624), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(@" />
    <!--  <link href=""http://netdna.bootstrapcdn.com/font-awesome/4.1.0/css/font-awesome.min.css"" rel=""stylesheet""> -->
    <!--[if lt IE 9]>
      <script src=""https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js""></script>
      <script src=""https://oss.maxcdn.com/respond/1.4.2/respond.min.js""></script>
    <![endif]-->
    
    <script");

WriteLiteral(" src=\"https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js\"");

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1115), Tuple.Create("\"", 1147)
, Tuple.Create(Tuple.Create("", 1121), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/bootstrap.min.js")
, 1121), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1171), Tuple.Create("\"", 1194)
, Tuple.Create(Tuple.Create("", 1177), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/main.js")
, 1177), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1218), Tuple.Create("\"", 1251)
, Tuple.Create(Tuple.Create("", 1224), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/summernote.min.js")
, 1224), false)
);

WriteLiteral("></script>\r\n\r\n</head>\r\n\r\n<body>\r\n\r\n \r\n    <!-- Header -->\r\n    <div");

WriteLiteral(" class=\"home_fixed_header\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"container-fluid headr_main \"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"col-sm-6\"");

WriteLiteral(">\r\n                    <a");

WriteLiteral(" class=\"logo\"");

WriteLiteral(" href=\"/Home/Index\"");

WriteLiteral("><img");

WriteAttribute("src", Tuple.Create(" src=\"", 1529), Tuple.Create("\"", 1563)
, Tuple.Create(Tuple.Create(" ", 1535), Tuple.Create<System.Object, System.Int32>(Href(" ~/Images/EventCombo_Red.jpg")
, 1536), false)
);

WriteLiteral(" /></a>\r\n                </div>\r\n\r\n                <div");

WriteLiteral(" class=\"col-sm-5 head_left_nav\"");

WriteLiteral(">\r\n                    <ul");

WriteLiteral(" class=\"navbar-right\"");

WriteLiteral(">\r\n\r\n                        <li></li>\r\n                        <li>");

            
            #line 45 "..\..\Views\Shared\_AuthenticateLayout.cshtml"
                       Write(Html.Partial("_Logout"));

            
            #line default
            #line hidden
WriteLiteral("</li>\r\n\r\n\r\n                    </ul>\r\n\r\n                </div>\r\n               \r\n" +
"                <div");

WriteLiteral(" class=\"col-sm-1 text-right\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"social_icn\"");

WriteLiteral(" style=\"width:40px; margin-top:10px;\"");

WriteLiteral(">\r\n                        <a");

WriteLiteral(" href=\"http://www.facebook.com/Eventcombo\"");

WriteLiteral(" target=\"_blank\"");

WriteLiteral("><img");

WriteAttribute("src", Tuple.Create(" src=\"", 2093), Tuple.Create("\"", 2114)
, Tuple.Create(Tuple.Create("", 2099), Tuple.Create<System.Object, System.Int32>(Href("~/Images/fb.png")
, 2099), false)
);

WriteLiteral(" /></a>\r\n                        <a");

WriteLiteral(" href=\"http://www.twitter.com/Eventcombo\"");

WriteLiteral(" target=\"_blank\"");

WriteLiteral("><img");

WriteAttribute("src", Tuple.Create(" src=\"", 2212), Tuple.Create("\"", 2233)
, Tuple.Create(Tuple.Create("", 2218), Tuple.Create<System.Object, System.Int32>(Href("~/Images/tw.png")
, 2218), false)
);

WriteLiteral(" /></a>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n" +
"            <div");

WriteLiteral(" class=\"Fix_Head\"");

WriteLiteral(" title=\"Clik to fixed Header\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"switch\"");

WriteLiteral(">\r\n                    <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" class=\"switch-input\"");

WriteLiteral(" checked>\r\n                    <span");

WriteLiteral(" class=\"switch-label\"");

WriteLiteral(" data-on=\"On\"");

WriteLiteral(" data-off=\"Off\"");

WriteLiteral("></span>\r\n                    <span");

WriteLiteral(" class=\"switch-handle\"");

WriteLiteral("></span>\r\n                </label>\r\n            </div>\r\n        </div>\r\n        <" +
"!-- Sub Header -->\r\n        <div");

WriteLiteral(" class=\"container-fluid cont_margin2\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"row cyan-line-new\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"col-sm-3\"");

WriteLiteral(">\r\n                    <form>\r\n                        <div");

WriteLiteral(" class=\"input-group stylish-input-group\"");

WriteLiteral(">\r\n                            <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control place_hold\"");

WriteLiteral(" onfocus=\"javascript:this.value=\'\'\"");

WriteLiteral(" placeholder=\"Search for Event\"");

WriteLiteral(">\r\n                            <span");

WriteLiteral(" class=\"input-group-addon\"");

WriteLiteral(">\r\n                                <button");

WriteLiteral(" type=\"submit\"");

WriteLiteral(">\r\n                                    <span");

WriteLiteral(" class=\"glyphicon glyphicon-search\"");

WriteLiteral("></span>\r\n                                </button>\r\n                            " +
"</span>\r\n                        </div>\r\n                    </form>\r\n          " +
"      </div>\r\n                <div");

WriteLiteral(" class=\"col-sm-2 disc_title_head\"");

WriteLiteral(">\r\n");

WriteLiteral("                    ");

            
            #line 83 "..\..\Views\Shared\_AuthenticateLayout.cshtml"
               Write(Html.ActionLink("Discover Events", "EventCreation", "Event"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"col-sm-2 disc_title_head\"");

WriteLiteral(">");

            
            #line 86 "..\..\Views\Shared\_AuthenticateLayout.cshtml"
                                                 Write(Html.ActionLink("Get the Buzz", "GetBuzz", "Home"));

            
            #line default
            #line hidden
WriteLiteral("  </div>\r\n                <div");

WriteLiteral(" class=\"col-sm-5 frnt_cret_evnt\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"psst\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 89 "..\..\Views\Shared\_AuthenticateLayout.cshtml"
                   Write(Html.ActionLink("Create Event", "CreateEvent", "EventManagement"));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </div>\r\n                  \r\n                    ");

WriteLiteral("\r\n                </div>\r\n\r\n            </div>\r\n        </div>\r\n        </div>\r\n " +
"       <!-- Body Section -->\r\n        <div>\r\n");

WriteLiteral("            ");

            
            #line 105 "..\..\Views\Shared\_AuthenticateLayout.cshtml"
       Write(RenderBody());

            
            #line default
            #line hidden
WriteLiteral("\r\n        </div>\r\n\r\n        <!--Footer Section-->\r\n    <footer");

WriteLiteral(" class=\"footer\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"container-fluid foot_bg\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-sm-12 no-padding\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"col-sm-12 foot_logo\"");

WriteLiteral(">\r\n                    <a");

WriteLiteral(" class=\"\"");

WriteLiteral(" href=\"#\"");

WriteLiteral("><img");

WriteAttribute("src", Tuple.Create(" src=\"", 4744), Tuple.Create("\"", 4773)
, Tuple.Create(Tuple.Create("", 4750), Tuple.Create<System.Object, System.Int32>(Href("~/Images/logo_small.png")
, 4750), false)
);

WriteLiteral(" /></a>\r\n                </div>\r\n\r\n                <div");

WriteLiteral(" class=\"row fot_cont\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n                        <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(">About Us</a>\r\n                        <br>\r\n                        <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(">Contact Us</a>\r\n                        <br>\r\n                        <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(">Terms Of Service</a>\r\n                    </div>\r\n                    <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n                        <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(">Ticket Purchase FAQ </a>\r\n                        <br>\r\n                        " +
"<a");

WriteLiteral(" href=\"#\"");

WriteLiteral(">Ticket Fulfillment Policy</a>\r\n                        <br>\r\n                   " +
"     <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(">Privacy Policy</a>\r\n                    </div>\r\n                    <div");

WriteLiteral(" class=\"col-sm-4\"");

WriteLiteral(">\r\n                        <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(">Facebook</a>\r\n                        <br>\r\n                        <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(">Twitter</a>\r\n                    </div>\r\n                </div>\r\n            </d" +
"iv>\r\n        </div>\r\n    </footer>\r\n</body>\r\n</html>\r\n");

        }
    }
}
#pragma warning restore 1591
