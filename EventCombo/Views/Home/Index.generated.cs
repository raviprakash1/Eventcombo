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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Home/Index.cshtml")]
    public partial class _Views_Home_Index_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_Home_Index_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\Home\Index.cshtml"
  
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

            
            #line 7 "..\..\Views\Home\Index.cshtml"
       
            
            #line default
            #line hidden
            
            #line 7 "..\..\Views\Home\Index.cshtml"
         Html.RenderPartial("~/Views/Home/HomeEventList.cshtml"); 
            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n\r\n    <div");

WriteLiteral(" class=\"col-sm-3 col-xs-12 ot_main_pad\"");

WriteLiteral(">\r\n");

            
            #line 11 "..\..\Views\Home\Index.cshtml"
      
            
            #line default
            #line hidden
            
            #line 11 "..\..\Views\Home\Index.cshtml"
         Html.RenderAction("ShowLastArticles", "Article"); 
            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n\r\n</div>\r\n\r\n\r\n\r\n<script>\r\n    $(document).prop(\'title\', \'Find and E" +
"njoy Cool Events, Create Your Own, Sell Tickets For Free\');\r\n    var getUrlParam" +
"eter = function getUrlParameter(sParam) {\r\n        var sPageURL = decodeURICompo" +
"nent(window.location.search.substring(1)),\r\n            sURLVariables = sPageURL" +
".split(\'&\'),\r\n            sParameterName,\r\n            i;\r\n\r\n        for (i = 0;" +
" i < sURLVariables.length; i++) {\r\n            sParameterName = sURLVariables[i]" +
".split(\'=\');\r\n\r\n            if (sParameterName[0] === sParam) {\r\n               " +
" return sParameterName[1] === undefined ? true : sParameterName[1];\r\n           " +
" }\r\n        }\r\n    };\r\n\r\n    $(document).ready(function () {\r\n        //url = ur" +
"l.replace(\"-parameter\", vEventId);\r\n      \r\n\r\n        var pg = getUrlParameter(\'" +
"page\');\r\n        if (!pg) {\r\n            if (navigator.geolocation) {\r\n         " +
"       navigator.geolocation.getCurrentPosition(function (p) {\r\n                " +
"    var LatLng = new google.maps.LatLng(p.coords.latitude, p.coords.longitude);\r" +
"\n                    var geocoder = new google.maps.Geocoder;\r\n                 " +
"   var infowindow = new google.maps.InfoWindow;\r\n\r\n                    var mapOp" +
"tions = {\r\n                        center: { lat: p.coords.latitude, lng: p.coor" +
"ds.longitude },\r\n                        zoom: 13,\r\n                        mapT" +
"ypeId: google.maps.MapTypeId.ROADMAP\r\n                    };\r\n                  " +
"  var vLat = p.coords.latitude;\r\n                    var vLong = p.coords.longit" +
"ude;\r\n\r\n                    var vUrl = \'");

            
            #line 55 "..\..\Views\Home\Index.cshtml"
                           Write(Html.Raw(Url.Action("Index", "Home", new {lat= "strLat", lng= "strLong", page = "1"})));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n                    vUrl = vUrl.replace(\"strLat\", vLat);\r\n                   " +
" vUrl = vUrl.replace(\"strLong\", vLong);\r\n                    window.location.rep" +
"lace(vUrl);\r\n\r\n                }, function (p) {\r\n                    var vUrl =" +
" \'");

            
            #line 61 "..\..\Views\Home\Index.cshtml"
                           Write(Html.Raw(Url.Action("Index", "Home", new {lat = "", lng = "", page = "1" })));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n                    window.location.replace(vUrl);\r\n                });\r\n\r\n  " +
"          } else {\r\n                var vUrl = \'");

            
            #line 66 "..\..\Views\Home\Index.cshtml"
                       Write(Html.Raw(Url.Action("Index", "Home", new {lat = "", lng = "", page = "1" })));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n                window.location.replace(vUrl);\r\n            }\r\n\r\n        }\r\n\r" +
"\n\r\n\r\n\r\n        ");

WriteLiteral("\r\n    });\r\n\r\n</script>\r\n");

        }
    }
}
#pragma warning restore 1591
