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
    
    #line 1 "..\..\Views\Home\IndexOld.cshtml"
    using EventCombo.ViewModels;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Home/IndexOld.cshtml")]
    public partial class _Views_Home_IndexOld_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_Home_IndexOld_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Views\Home\IndexOld.cshtml"
  
    ViewBag.Title = "EventCombo - Find and Enjoy Cool Events, Create Your Own, Sell Tickets For Free";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<div");

WriteLiteral(" class=\"container-fluid cont_margin no-padding con_scr0l_home\"");

WriteLiteral(" id=\"scrollhome\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" id=\"dvEventListing\"");

WriteLiteral(" class=\"col-sm-9 col-xs-12 ot_main_pad mb10\"");

WriteLiteral(">\r\n");

            
            #line 8 "..\..\Views\Home\IndexOld.cshtml"
       
            
            #line default
            #line hidden
            
            #line 8 "..\..\Views\Home\IndexOld.cshtml"
         Html.RenderPartial("~/Views/Home/HomeEventList.cshtml"); 
            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n\r\n    <div");

WriteLiteral(" class=\"col-sm-3 col-xs-12 ot_main_pad\"");

WriteLiteral(">\r\n");

            
            #line 12 "..\..\Views\Home\IndexOld.cshtml"
      
            
            #line default
            #line hidden
            
            #line 12 "..\..\Views\Home\IndexOld.cshtml"
         Html.RenderAction("ShowLastArticles", "Article"); 
            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n\r\n</div>\r\n\r\n\r\n<div");

WriteLiteral(" class=\"modal fade\"");

WriteLiteral(" id=\"UserOrgnizer\"");

WriteLiteral(" tabindex=\"-1\"");

WriteLiteral(" role=\"dialog\"");

WriteLiteral(" aria-labelledby=\"myModalLabel\"");

WriteLiteral(" aria-hidden=\"true\"");

WriteLiteral(" data-backdrop=\"static\"");

WriteLiteral(" data-keyboard=\"false\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"modal-dialog modal-sm confirm-msg ok-msg custom\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"modal-content\"");

WriteLiteral(">\r\n            <form");

WriteLiteral(" id=\"contactForm\"");

WriteLiteral(" class=\"fullw1 add\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"modal-body text-center pv50 txt-msg\"");

WriteLiteral(">\r\n                    <h4");

WriteLiteral(" class=\"msg\"");

WriteLiteral(" id=\"hId\"");

WriteLiteral(">You are not authorized to create an event. Please contact admin@eventcombo.com</" +
"h4>\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"modal-footer msg-action-btn ok\"");

WriteLiteral(">\r\n                    <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-primary yes ok\"");

WriteLiteral(" id=\"btMessOk\"");

WriteLiteral(" data-dismiss=\"modal\"");

WriteLiteral(">OK</button>\r\n                </div>\r\n            </form>\r\n        </div>\r\n    </" +
"div><!-- confirm-msg -->\r\n</div>\r\n\r\n<div>\r\n    <a");

WriteLiteral(" class=\"td-btn\"");

WriteLiteral(" id=\"btOkMes\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" data-backdrop=\"static\"");

WriteLiteral(" data-keyboard=\"false\"");

WriteLiteral(" href=\"#UserOrgnizer\"");

WriteLiteral(" data-toggle=\"modal\"");

WriteLiteral(" style=\"display:none\"");

WriteLiteral(@">
    </a>
</div>


<script>
    $(document).prop('title', 'Find and Enjoy Cool Events, Create Your Own, Sell Tickets For Free');
    var getUrlParameter = function getUrlParameter(sParam) {
        var sPageURL = decodeURIComponent(window.location.search.substring(1)),
            sURLVariables = sPageURL.split('&'),
            sParameterName,
            i;

        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split('=');

            if (sParameterName[0] === sParam) {
                return sParameterName[1] === undefined ? true : sParameterName[1];
            }
        }
    };

    $(document).ready(function () {
        //url = url.replace(""-parameter"", vEventId);
        
        if ('");

            
            #line 59 "..\..\Views\Home\IndexOld.cshtml"
        Write(ViewBag.UserOrg);

            
            #line default
            #line hidden
WriteLiteral("\' == \"Y\")\r\n        {\r\n            $(\"#btOkMes\").click();\r\n        }\r\n\r\n      \r\n  " +
"      \r\n        var pg = getUrlParameter(\'page\');\r\n        if (!pg) {\r\n         " +
"   var Lat = ");

            
            #line 68 "..\..\Views\Home\IndexOld.cshtml"
                 Write(CookieStore.GetCookie("Lat"));

            
            #line default
            #line hidden
WriteLiteral(";\r\n            var Lng = ");

            
            #line 69 "..\..\Views\Home\IndexOld.cshtml"
                 Write(CookieStore.GetCookie("Long"));

            
            #line default
            #line hidden
WriteLiteral(@";
            Lat  = parseFloat(Lat);
            Lng = parseFloat(Lng);
            if (isNaN(Lat) == true) Lat = 0;
            if (isNaN(Lng) == true) Lng = 0;
         //   alert(Lat);
            if (Lat > 0)
            {
                var vUrl = '");

            
            #line 77 "..\..\Views\Home\IndexOld.cshtml"
                       Write(Html.Raw(Url.Action("Index", "Home", new {lat = CookieStore.GetCookie("Lat"), lng = CookieStore.GetCookie("Long"), page = "1"})));

            
            #line default
            #line hidden
WriteLiteral(@"';
                window.location.replace(vUrl);
            }
            else
            {
                if (navigator.geolocation) {
                    navigator.geolocation.getCurrentPosition(function (p) {
                        var LatLng = new google.maps.LatLng(p.coords.latitude,p.coords.longitude);
                        var geocoder = new google.maps.Geocoder;
                        var infowindow = new google.maps.InfoWindow;

                        var mapOptions = {
                            center: { lat: p.coords.latitude, lng: p.coords.longitude },
                            zoom: 13,
                            mapTypeId: google.maps.MapTypeId.ROADMAP
                        };
                        var vLat = p.coords.latitude;
                        var vLong = p.coords.longitude;

                        //$.cookie(""Lat"", vLat, { expire: 365 });
                        //$.cookie(""Long"", vLong, { expire: 365 });

                        var vUrl = '");

            
            #line 99 "..\..\Views\Home\IndexOld.cshtml"
                               Write(Html.Raw(Url.Action("Index", "Home", new {lat= "strLat", lng= "strLong", page = "1"})));

            
            #line default
            #line hidden
WriteLiteral(@"';
                        vUrl = vUrl.replace(""strLat"", vLat);
                        vUrl = vUrl.replace(""strLong"", vLong);
                        window.location.replace(vUrl);

                    }, function (p) {
                        var vUrl = '");

            
            #line 105 "..\..\Views\Home\IndexOld.cshtml"
                               Write(Html.Raw(Url.Action("Index", "Home", new {lat = "", lng = "", page = "1" })));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n                        window.location.replace(vUrl);\r\n                    }" +
");\r\n\r\n                } else {\r\n                    var vUrl = \'");

            
            #line 110 "..\..\Views\Home\IndexOld.cshtml"
                           Write(Html.Raw(Url.Action("Index", "Home", new {lat = "", lng = "", page = "1" })));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n                    window.location.replace(vUrl);\r\n                }\r\n      " +
"      }\r\n\r\n        }\r\n\r\n        \r\n\r\n\r\n\r\n        ");

WriteLiteral("\r\n    });\r\n\r\n</script>\r\n");

        }
    }
}
#pragma warning restore 1591