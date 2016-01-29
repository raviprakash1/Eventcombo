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
    
    #line 4 "..\..\Views\EventList\EventList.cshtml"
    using EventCombo.Models;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Views\EventList\EventList.cshtml"
    using PagedList.Mvc;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/EventList/EventList.cshtml")]
    public partial class _Views_EventList_EventList_cshtml : System.Web.Mvc.WebViewPage<PagedList.IPagedList<EventCombo.Models.GetEventsListByStatus1_Result>>
    {
        public _Views_EventList_EventList_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

            
            #line 5 "..\..\Views\EventList\EventList.cshtml"
  
    ViewBag.Title = "EventList";
    Layout = "~/Views/Shared/_AccountLayout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n<div");

WriteLiteral(" class=\"col-sm-12\"");

WriteLiteral(">\r\n    <h3");

WriteLiteral(" class=\"acnt_title\"");

WriteLiteral(">My Events</h3>\r\n</div>\r\n<div");

WriteLiteral(" class=\"clearfix\"");

WriteLiteral("></div>\r\n<div");

WriteLiteral(" class=\"col-sm-12 mange-events\"");

WriteLiteral(">\r\n\r\n    <!-- -- Search box open -->\r\n");

            
            #line 16 "..\..\Views\EventList\EventList.cshtml"
    
            
            #line default
            #line hidden
            
            #line 16 "..\..\Views\EventList\EventList.cshtml"
     if (Convert.ToInt32(TempData["Allcount"]) != 0)
    {

            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"col-sm-6 no_pad\"");

WriteLiteral(">\r\n        <form>\r\n            <div");

WriteLiteral(" class=\"input-group custom \"");

WriteLiteral(">\r\n                ");

WriteLiteral("\r\n");

WriteLiteral("                ");

            
            #line 22 "..\..\Views\EventList\EventList.cshtml"
           Write(Html.TextBox("SearchStringEventTitle", "", new { Class = "form-control mng-evnt-search", placeholder = "Search for events and guests" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                ");

            
            #line 23 "..\..\Views\EventList\EventList.cshtml"
           Write(Html.Hidden("hdLastTab", ""));

            
            #line default
            #line hidden
WriteLiteral("\r\n                <div");

WriteLiteral(" class=\"input-group-addon\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"search-icon\"");

WriteLiteral(">\r\n                        <button");

WriteLiteral(" class=\"fa fa-search\"");

WriteLiteral(" type=\"submit\"");

WriteLiteral("></button>\r\n                    </div>\r\n                </div>\r\n            </div" +
">\r\n        </form>\r\n    </div>\r\n");

            
            #line 32 "..\..\Views\EventList\EventList.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n    <!-- Search box close -- -->\r\n    <!-- -- 3 tabs open -->\r\n    <div");

WriteLiteral(" class=\"col-sm-12 row\"");

WriteLiteral(">\r\n\r\n");

            
            #line 39 "..\..\Views\EventList\EventList.cshtml"
 if ((Convert.ToInt32(TempData["LiveEventscnt"]) > 0 && TempData["LiveEventscnt"] != null) || (Convert.ToInt32(TempData["SavedEventscnt"]) > 0 && TempData["SavedEventscnt"] != null) || (Convert.ToInt32(TempData["PastEventscnt"]) > 0 && TempData["PastEventscnt"] != null))
{

            
            #line default
            #line hidden
WriteLiteral("        <div");

WriteLiteral(" class=\"col-sm-12 no_pad mb10\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-sm-6 no_pad\"");

WriteLiteral("> <h3");

WriteLiteral(" class=\"acnt_title\"");

WriteLiteral(">Events</h3> </div>\r\n            <div");

WriteLiteral(" class=\"col-sm-6 no_pad evnt-list-pag text-right\"");

WriteLiteral(">\r\n");

            
            #line 44 "..\..\Views\EventList\EventList.cshtml"
                
            
            #line default
            #line hidden
            
            #line 44 "..\..\Views\EventList\EventList.cshtml"
                 if (ViewBag.LiveEvent.PageCount > 1)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <div");

WriteLiteral(" class=\"pager mar0\"");

WriteLiteral(" id=\"DivLivePager\"");

WriteLiteral(">\r\n                        ");

WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 48 "..\..\Views\EventList\EventList.cshtml"
                   Write(Html.PagedListPager((PagedList.IPagedList<GetEventsListByStatus1_Result>)ViewBag.LiveEvent, page_live => Url.Action("EventList", new
                   {
                       page_live,
                       hdLastTab = "L"
                   })));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </div>\r\n");

            
            #line 54 "..\..\Views\EventList\EventList.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("                ");

            
            #line 55 "..\..\Views\EventList\EventList.cshtml"
                 if (ViewBag.SavedEvent.PageCount > 1)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <div");

WriteLiteral(" class=\"pager mar0\"");

WriteLiteral(" id=\"DivSavedPager\"");

WriteLiteral(">\r\n\r\n                        ");

WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 60 "..\..\Views\EventList\EventList.cshtml"
                   Write(Html.PagedListPager((PagedList.IPagedList<GetEventsListByStatus1_Result>)ViewBag.SavedEvent, page_saved => Url.Action("EventList", new
                   {
                       page_saved,
                       hdLastTab = "S"
                   })));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </div>\r\n");

            
            #line 66 "..\..\Views\EventList\EventList.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("                ");

            
            #line 67 "..\..\Views\EventList\EventList.cshtml"
                 if (ViewBag.PastEvent.PageCount > 1)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <div");

WriteLiteral(" class=\"pager mar0\"");

WriteLiteral(" id=\"DivPastPager\"");

WriteLiteral(">\r\n\r\n                        ");

WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 72 "..\..\Views\EventList\EventList.cshtml"
                   Write(Html.PagedListPager((PagedList.IPagedList<GetEventsListByStatus1_Result>)ViewBag.PastEvent, page_past => Url.Action("EventList", new
                   {
                       page_past,
                       hdLastTab = "P"
                   })));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </div>\r\n");

            
            #line 78 "..\..\Views\EventList\EventList.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("            </div>\r\n        </div>\r\n");

            
            #line 81 "..\..\Views\EventList\EventList.cshtml"


            
            #line default
            #line hidden
WriteLiteral("        <div");

WriteLiteral(" class=\"col-sm-12  custom fs18 no_pad\"");

WriteLiteral(">\r\n            <ul");

WriteLiteral(" class=\"nav nav-tabs evnt-mng-tab\"");

WriteLiteral(" id=\"myTab\"");

WriteLiteral(">\r\n                <li");

WriteLiteral(" id=\"liLive\"");

WriteLiteral(">\r\n                    <a");

WriteLiteral(" href=\"#sectionA\"");

WriteLiteral(" onclick=\"ShowLivePager()\"");

WriteLiteral(" aria-controls=\"home\"");

WriteLiteral(" role=\"tab\"");

WriteLiteral(" data-toggle=\"tab\"");

WriteLiteral(">Live Events (");

            
            #line 85 "..\..\Views\EventList\EventList.cshtml"
                                                                                                                            Write(TempData["LiveEvents"]);

            
            #line default
            #line hidden
WriteLiteral(")</a>\r\n                </li>\r\n                <li");

WriteLiteral(" class=\"\"");

WriteLiteral(" id=\"liSaved\"");

WriteLiteral(">\r\n                    <a");

WriteLiteral(" href=\"#sectionB\"");

WriteLiteral(" onclick=\"ShowSavedPager()\"");

WriteLiteral(" aria-controls=\"home\"");

WriteLiteral(" role=\"tab\"");

WriteLiteral(" data-toggle=\"tab\"");

WriteLiteral(">Saved Events (");

            
            #line 88 "..\..\Views\EventList\EventList.cshtml"
                                                                                                                              Write(TempData["SavedEvents"]);

            
            #line default
            #line hidden
WriteLiteral(")</a>\r\n                </li>\r\n                <li");

WriteLiteral(" class=\"\"");

WriteLiteral(" id=\"liPast\"");

WriteLiteral(">\r\n                    <a");

WriteLiteral(" href=\"#sectionC\"");

WriteLiteral(" onclick=\"ShowPastPager()\"");

WriteLiteral(" aria-controls=\"home\"");

WriteLiteral(" role=\"tab\"");

WriteLiteral(" data-toggle=\"tab\"");

WriteLiteral(">Past Events (");

            
            #line 91 "..\..\Views\EventList\EventList.cshtml"
                                                                                                                            Write(TempData["PastEvents"]);

            
            #line default
            #line hidden
WriteLiteral(")</a>\r\n                </li>\r\n            </ul>\r\n        </div>\r\n");

WriteLiteral("        <div");

WriteLiteral(" class=\"tab-content mt10\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" id=\"sectionA\"");

WriteLiteral(" class=\"tab-pane\"");

WriteLiteral(">\r\n                <table");

WriteLiteral(" class=\"table fs20 mb0\"");

WriteLiteral(">\r\n                    <thead>\r\n                        <tr>\r\n                   " +
"         <th");

WriteLiteral(" class=\"evnt_mng_tit_200\"");

WriteLiteral(">Event Title</th>\r\n                            <th>Event Date /Time</th>\r\n       " +
"                     <th>Ticket Sold / Total No.</th>\r\n                         " +
"   <th");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">Action</th>\r\n                        </tr>\r\n                    </thead>\r\n      " +
"              <tbody>\r\n");

            
            #line 107 "..\..\Views\EventList\EventList.cshtml"
                        
            
            #line default
            #line hidden
            
            #line 107 "..\..\Views\EventList\EventList.cshtml"
                          
                            foreach (var item in (PagedList.IPagedList<GetEventsListByStatus1_Result>)ViewBag.LiveEvent)
                            {

            
            #line default
            #line hidden
WriteLiteral("                                <tr>\r\n                                    <td");

WriteLiteral(" class=\"evnt_mng_tit_200\"");

WriteLiteral(">");

            
            #line 111 "..\..\Views\EventList\EventList.cshtml"
                                                            Write(item.EventTitle);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                                    <td>");

            
            #line 112 "..\..\Views\EventList\EventList.cshtml"
                                   Write(item.EventDate);

            
            #line default
            #line hidden
WriteLiteral(" / ");

            
            #line 112 "..\..\Views\EventList\EventList.cshtml"
                                                     Write(item.EventTime);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                                    <td>");

            
            #line 113 "..\..\Views\EventList\EventList.cshtml"
                                   Write(item.TicketSold);

            
            #line default
            #line hidden
WriteLiteral("/");

            
            #line 113 "..\..\Views\EventList\EventList.cshtml"
                                                    Write(item.TotalTicket);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                                    <td");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">\r\n                                        <div");

WriteLiteral(" class=\"event-btn-action\"");

WriteLiteral(">\r\n                                            <p");

WriteLiteral(" class=\"btn-link manage\"");

WriteLiteral(">");

            
            #line 116 "..\..\Views\EventList\EventList.cshtml"
                                                                  Write(Html.ActionLink("Manage", "Index", "ManageEvent", new { Eventid = item.EventID, type = "N" }, null));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                            <p");

WriteLiteral(" class=\"btn-link manage\"");

WriteLiteral(">");

            
            #line 117 "..\..\Views\EventList\EventList.cshtml"
                                                                  Write(Html.ActionLink("Edit", "ModifyEvent", "EditEvent", new { Eventid = item.EventID }, null));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                            \r\n                             " +
"               <p");

WriteLiteral(" class=\"btn-link manage\"");

WriteLiteral(">");

            
            #line 119 "..\..\Views\EventList\EventList.cshtml"
                                                                  Write(Html.ActionLink("View", "ViewEvent", "ViewEvent", new { strEventDs = System.Text.RegularExpressions.Regex.Replace(item.EventTitle.Replace(" ", "-"),"[^a-zA-Z0-9_-]+",""), strEventId = item.EventID.ToString() }, null));

            
            #line default
            #line hidden
WriteLiteral(" </p>\r\n                                            ");

WriteLiteral("\r\n                                        </div>\r\n                               " +
"     </td>\r\n                                </tr>\r\n");

            
            #line 124 "..\..\Views\EventList\EventList.cshtml"
                            }
                        
            
            #line default
            #line hidden
WriteLiteral("\r\n                    </tbody>\r\n                </table>\r\n                <!-- ma" +
"in-sec -->\r\n");

            
            #line 129 "..\..\Views\EventList\EventList.cshtml"
                
            
            #line default
            #line hidden
            
            #line 129 "..\..\Views\EventList\EventList.cshtml"
                 if (Convert.ToInt32(ViewData["LiveEvntCnt"]) == 0 && ViewData["LiveEvntCnt"] != null)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <span>You don\'t have any live events.</span>\r\n");

            
            #line 132 "..\..\Views\EventList\EventList.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("\r\n            </div>\r\n\r\n            <div");

WriteLiteral(" id=\"sectionB\"");

WriteLiteral(" class=\"tab-pane\"");

WriteLiteral(">\r\n                <table");

WriteLiteral(" class=\"table fs20 mb0\"");

WriteLiteral(">\r\n                    <thead>\r\n                        <tr>\r\n                   " +
"         <th");

WriteLiteral(" class=\"evnt_mng_tit_200\"");

WriteLiteral(">Event Title</th>\r\n                            <th>Event Date /Time</th>\r\n       " +
"                     <th>Ticket Sold / Total No.</th>\r\n                         " +
"   <th");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">Action</th>\r\n                        </tr>\r\n                    </thead>\r\n      " +
"              <tbody>\r\n");

            
            #line 147 "..\..\Views\EventList\EventList.cshtml"
                        
            
            #line default
            #line hidden
            
            #line 147 "..\..\Views\EventList\EventList.cshtml"
                          
                            //foreach (var item in (List<GetEventsListByStatus1_Result>)ViewBag.SavedEvent)
                            foreach (var item in (PagedList.IPagedList<GetEventsListByStatus1_Result>)ViewBag.SavedEvent)
                            {

            
            #line default
            #line hidden
WriteLiteral("                                <tr>\r\n                                    <td");

WriteLiteral(" class=\"evnt_mng_tit_200\"");

WriteLiteral(">");

            
            #line 152 "..\..\Views\EventList\EventList.cshtml"
                                                            Write(item.EventTitle);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                                    <td>");

            
            #line 153 "..\..\Views\EventList\EventList.cshtml"
                                   Write(item.EventDate);

            
            #line default
            #line hidden
WriteLiteral(" / ");

            
            #line 153 "..\..\Views\EventList\EventList.cshtml"
                                                     Write(item.EventTime);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                                    <td>");

            
            #line 154 "..\..\Views\EventList\EventList.cshtml"
                                   Write(item.TicketSold);

            
            #line default
            #line hidden
WriteLiteral("/");

            
            #line 154 "..\..\Views\EventList\EventList.cshtml"
                                                    Write(item.TotalTicket);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                                    <td");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">\r\n                                        <div");

WriteLiteral(" class=\"event-btn-action\"");

WriteLiteral(">\r\n                                            <p");

WriteLiteral(" class=\"btn-link manage\"");

WriteLiteral(">");

            
            #line 157 "..\..\Views\EventList\EventList.cshtml"
                                                                  Write(Html.ActionLink("Manage", "Index", "ManageEvent", new { Eventid = item.EventID, type = "N" }, null));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n\r\n                                            <p");

WriteLiteral(" class=\"btn-link manage\"");

WriteLiteral(">");

            
            #line 159 "..\..\Views\EventList\EventList.cshtml"
                                                                  Write(Html.ActionLink("Edit", "ModifyEvent", "EditEvent", new { Eventid = item.EventID }, null));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                                            <p");

WriteLiteral(" class=\"btn-link manage\"");

WriteLiteral(">");

            
            #line 160 "..\..\Views\EventList\EventList.cshtml"
                                                                  Write(Html.ActionLink("View", "ViewEvent", "ViewEvent", new { strEventDs = System.Text.RegularExpressions.Regex.Replace(item.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = item.EventID.ToString() }, null));

            
            #line default
            #line hidden
WriteLiteral(" </p>\r\n\r\n                                            ");

WriteLiteral("\r\n                                        </div>\r\n                               " +
"     </td>\r\n                                </tr>\r\n");

            
            #line 166 "..\..\Views\EventList\EventList.cshtml"
                            }
                        
            
            #line default
            #line hidden
WriteLiteral("\r\n                    </tbody>\r\n                </table><!-- main-sec -->\r\n");

            
            #line 170 "..\..\Views\EventList\EventList.cshtml"
                
            
            #line default
            #line hidden
            
            #line 170 "..\..\Views\EventList\EventList.cshtml"
                 if (Convert.ToInt32(ViewData["SavedEvntCnt"]) == 0 && ViewData["SavedEvntCnt"] != null)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <span>You don\'t have any events in drafts.</span>\r\n");

            
            #line 173 "..\..\Views\EventList\EventList.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n            </div>\r\n\r\n            <div");

WriteLiteral(" id=\"sectionC\"");

WriteLiteral(" class=\"tab-pane\"");

WriteLiteral(">\r\n                <table");

WriteLiteral(" class=\"table fs20 mb0\"");

WriteLiteral(">\r\n                    <thead>\r\n                        <tr>\r\n                   " +
"         <th");

WriteLiteral(" class=\"evnt_mng_tit_200\"");

WriteLiteral(">Event Title</th>\r\n                            <th>Event Date /Time</th>\r\n       " +
"                     <th>Ticket Sold / Total No.</th>\r\n                         " +
"   <th");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">Action</th>\r\n                        </tr>\r\n                    </thead>\r\n      " +
"              <tbody>\r\n");

            
            #line 190 "..\..\Views\EventList\EventList.cshtml"
                        
            
            #line default
            #line hidden
            
            #line 190 "..\..\Views\EventList\EventList.cshtml"
                          
                            //foreach (var item in (List<GetEventsListByStatus1_Result>)ViewBag.PastEvent)
                            foreach (var item in (PagedList.IPagedList<GetEventsListByStatus1_Result>)ViewBag.PastEvent)
                            {

            
            #line default
            #line hidden
WriteLiteral("                                <tr>\r\n                                    <td");

WriteLiteral(" class=\"evnt_mng_tit_200\"");

WriteLiteral(">");

            
            #line 195 "..\..\Views\EventList\EventList.cshtml"
                                                            Write(item.EventTitle);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                                    <td>");

            
            #line 196 "..\..\Views\EventList\EventList.cshtml"
                                   Write(item.EventDate);

            
            #line default
            #line hidden
WriteLiteral("/ ");

            
            #line 196 "..\..\Views\EventList\EventList.cshtml"
                                                    Write(item.EventTime);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                                    <td>");

            
            #line 197 "..\..\Views\EventList\EventList.cshtml"
                                   Write(item.TicketSold);

            
            #line default
            #line hidden
WriteLiteral("/");

            
            #line 197 "..\..\Views\EventList\EventList.cshtml"
                                                    Write(item.TotalTicket);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                                    <td");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">\r\n                                        <div");

WriteLiteral(" class=\"event-btn-action\"");

WriteLiteral(">\r\n                                            <p");

WriteLiteral(" class=\"btn-link manage\"");

WriteLiteral(">");

            
            #line 200 "..\..\Views\EventList\EventList.cshtml"
                                                                  Write(Html.ActionLink("Manage", "Index", "ManageEvent", new { Eventid = item.EventID, type="N" }, null));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n");

            
            #line 201 "..\..\Views\EventList\EventList.cshtml"
                                          
            
            #line default
            #line hidden
            
            #line 201 "..\..\Views\EventList\EventList.cshtml"
                                           if (item.EventCancel != null)
                                          {
                                             if (item.EventCancel.ToString() == "Y")
                                             {

            
            #line default
            #line hidden
WriteLiteral("                                            <p");

WriteLiteral(" class=\"btn-link manage\"");

WriteLiteral("><text>Edit</text></p>\r\n");

WriteLiteral("                                                <p");

WriteLiteral(" class=\"btn-link manage\"");

WriteLiteral("><text>View</text> </p>\r\n");

            
            #line 207 "..\..\Views\EventList\EventList.cshtml"

                                             }
                                             else
                                             {

            
            #line default
            #line hidden
WriteLiteral("                                            <p");

WriteLiteral(" class=\"btn-link manage\"");

WriteLiteral(">");

            
            #line 211 "..\..\Views\EventList\EventList.cshtml"
                                                                  Write(Html.ActionLink("Edit", "ModifyEvent", "EditEvent", new { Eventid = item.EventID }, null));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n");

WriteLiteral("                                            <p");

WriteLiteral(" class=\"btn-link manage\"");

WriteLiteral(">");

            
            #line 212 "..\..\Views\EventList\EventList.cshtml"
                                                                  Write(Html.ActionLink("View", "ViewEvent", "ViewEvent", new { strEventDs = System.Text.RegularExpressions.Regex.Replace(item.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = item.EventID.ToString() }, null));

            
            #line default
            #line hidden
WriteLiteral(" </p>\r\n");

            
            #line 213 "..\..\Views\EventList\EventList.cshtml"

                                                    
            
            #line default
            #line hidden
            
            #line 214 "..\..\Views\EventList\EventList.cshtml"
                                                                                                                                                                                                                    


                                             }
                                          }
                                          else
                                          {

            
            #line default
            #line hidden
WriteLiteral("                                            <p");

WriteLiteral(" class=\"btn-link manage\"");

WriteLiteral(">");

            
            #line 221 "..\..\Views\EventList\EventList.cshtml"
                                                                  Write(Html.ActionLink("Edit", "ModifyEvent", "EditEvent", new { Eventid = item.EventID }, null));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n");

WriteLiteral("                                            <p");

WriteLiteral(" class=\"btn-link manage\"");

WriteLiteral(">");

            
            #line 222 "..\..\Views\EventList\EventList.cshtml"
                                                                  Write(Html.ActionLink("View", "ViewEvent", "ViewEvent", new { strEventDs = System.Text.RegularExpressions.Regex.Replace(item.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = item.EventID.ToString() }, null));

            
            #line default
            #line hidden
WriteLiteral(" </p>\r\n");

            
            #line 223 "..\..\Views\EventList\EventList.cshtml"

                                          }

            
            #line default
            #line hidden
WriteLiteral("                                        </div>\r\n                                 " +
"   </td>\r\n                                </tr>\r\n");

            
            #line 228 "..\..\Views\EventList\EventList.cshtml"
                            }
                        
            
            #line default
            #line hidden
WriteLiteral("\r\n                    </tbody>\r\n                </table><!-- main-sec -->\r\n");

            
            #line 232 "..\..\Views\EventList\EventList.cshtml"
                
            
            #line default
            #line hidden
            
            #line 232 "..\..\Views\EventList\EventList.cshtml"
                 if (Convert.ToInt32(ViewData["PastEvntCnt"]) == 0 && ViewData["PastEvntCnt"] != null)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <span>You don\'t have any past events.</span>\r\n");

            
            #line 235 "..\..\Views\EventList\EventList.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("\r\n            </div>\r\n\r\n            <!-- tab-content content-tab 2 -->\r\n\r\n       " +
" </div>\r\n");

            
            #line 242 "..\..\Views\EventList\EventList.cshtml"
 } 

            
            #line default
            #line hidden
WriteLiteral("     <!-- tab-content content-tab 1 -->\r\n");

            
            #line 244 "..\..\Views\EventList\EventList.cshtml"
  
            
            #line default
            #line hidden
            
            #line 244 "..\..\Views\EventList\EventList.cshtml"
   if (Convert.ToInt32(TempData["GuestListcnt"]) > 0 && TempData["GuestListcnt"] != null)
  {

            
            #line default
            #line hidden
WriteLiteral("        <div");

WriteLiteral(" class=\"col-sm-12 no_pad mt10\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-sm-12 no_pad mb5\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"col-sm-6 no_pad\"");

WriteLiteral("> <h3");

WriteLiteral(" class=\"acnt_title\"");

WriteLiteral(">Guests</h3> </div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"col-sm-12 row\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"table-responsive\"");

WriteLiteral(">\r\n                    <table");

WriteLiteral(" class=\"table fs20\"");

WriteLiteral(@">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Email</th>
                                <th>Event Name</th>
                                <th>Date-Time</th>
                                <th>Order Number</th>
                                <th>Ticket Type / Total</th>
                            </tr>
                        </thead>
                        <tbody>
");

            
            #line 264 "..\..\Views\EventList\EventList.cshtml"
                            
            
            #line default
            #line hidden
            
            #line 264 "..\..\Views\EventList\EventList.cshtml"
                              
                                foreach (var item in (List<GetEventsListByStatus1_Result>)ViewBag.GuestList)
                                {

            
            #line default
            #line hidden
WriteLiteral("                                    <tr>\r\n                                       " +
" <td>");

            
            #line 268 "..\..\Views\EventList\EventList.cshtml"
                                       Write(item.Name);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                                        <td>");

            
            #line 269 "..\..\Views\EventList\EventList.cshtml"
                                       Write(item.Email);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                                        <td>");

            
            #line 270 "..\..\Views\EventList\EventList.cshtml"
                                       Write(item.EventTitle);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                                        <td>");

            
            #line 271 "..\..\Views\EventList\EventList.cshtml"
                                       Write(item.Date_Time);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                                        <td>");

            
            #line 272 "..\..\Views\EventList\EventList.cshtml"
                                       Write(item.OrderId);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                                        <td>");

            
            #line 273 "..\..\Views\EventList\EventList.cshtml"
                                       Write(item.TicketType);

            
            #line default
            #line hidden
WriteLiteral("/");

            
            #line 273 "..\..\Views\EventList\EventList.cshtml"
                                                        Write(item.TicketPurchased);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n\r\n                                    </tr>\r\n");

            
            #line 276 "..\..\Views\EventList\EventList.cshtml"
                                }
                            
            
            #line default
            #line hidden
WriteLiteral("\r\n                        </tbody>\r\n                    </table>\r\n               " +
" </div>\r\n            </div>\r\n        </div>\r\n");

            
            #line 283 "..\..\Views\EventList\EventList.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("    </div><!-- event-tabs -->\r\n    <!-- 3 tabs close -- -->\r\n\r\n\r\n\r\n</div>\r\n\r\n<scr" +
"ipt");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    $(window).load(function () {\r\n      //  $(\"#DivSavedPager\").hide();\r\n     " +
" //      $(\"#DivPastPager\").hide();\r\n    });\r\n    function ShowLivePager() {\r\n  " +
"      $(\"#DivLivePager\").show();\r\n        $(\"#DivSavedPager\").hide();\r\n        $" +
"(\"#DivPastPager\").hide();\r\n    };\r\n    function ShowSavedPager() {\r\n        debu" +
"gger;\r\n        $(\"#DivSavedPager\").show();\r\n        $(\"#DivLivePager\").hide();\r\n" +
"        $(\"#DivPastPager\").hide();\r\n    };\r\n    function ShowPastPager() {\r\n    " +
"    $(\"#DivPastPager\").show();\r\n        $(\"#DivLivePager\").hide();\r\n        $(\"#" +
"DivSavedPager\").hide();\r\n    };\r\n    $(document).ready(function () {\r\n\r\n        " +
"//$(window).unload(function () {\r\n        //    $.removeCookie(\"LastTab\");\r\n    " +
"    //});\r\n\r\n\r\n        var vLastTab = $(\"#hdLastTab\").val();\r\n        if (vLastT" +
"ab == \"P\") {\r\n            $(\"#liPast\").addClass(\"active\");\r\n            $(\"#sect" +
"ionC\").addClass(\"active\");\r\n            $(\"#DivPastPager\").show();\r\n            " +
"$(\"#DivLivePager\").hide();\r\n            $(\"#DivSavedPager\").hide();\r\n        }\r\n" +
"        else if (vLastTab == \"S\") {\r\n            $(\"#liSaved\").addClass(\"active\"" +
");\r\n            $(\"#sectionB\").addClass(\"active\");\r\n            $(\"#DivSavedPage" +
"r\").show();\r\n            $(\"#DivLivePager\").hide();\r\n            $(\"#DivPastPage" +
"r\").hide();\r\n        }\r\n        else {\r\n            $(\"#liLive\").addClass(\"activ" +
"e\");\r\n            $(\"#sectionA\").addClass(\"active\");\r\n            $(\"#DivLivePag" +
"er\").show();\r\n            $(\"#DivSavedPager\").hide();\r\n            $(\"#DivPastPa" +
"ger\").hide();\r\n        }\r\n    });\r\n\r\n</script>\r\n");

        }
    }
}
#pragma warning restore 1591
