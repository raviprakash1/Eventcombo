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
    
    #line 1 "..\..\Views\Account\OrganizerProfile.cshtml"
    using EventCombo.Models;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Account/OrganizerProfile.cshtml")]
    public partial class _Views_Account_OrganizerProfile_cshtml : System.Web.Mvc.WebViewPage<OrganizerProfile>
    {
        public _Views_Account_OrganizerProfile_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\Account\OrganizerProfile.cshtml"
  
    ViewBag.Title = "OrganizerProfile";
    Layout = "~/Views/Shared/_AccountLayout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 164), Tuple.Create("\"", 205)
, Tuple.Create(Tuple.Create("", 170), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/jquery.maskedinput.min.js")
, 170), false)
);

WriteLiteral("></script>\r\n<link");

WriteAttribute("href", Tuple.Create(" href=\"", 223), Tuple.Create("\"", 262)
, Tuple.Create(Tuple.Create("", 230), Tuple.Create<System.Object, System.Int32>(Href("~/Content/Filer/jquery.filer.css")
, 230), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n\r\n<link");

WriteAttribute("href", Tuple.Create(" href=\"", 292), Tuple.Create("\"", 349)
, Tuple.Create(Tuple.Create("", 299), Tuple.Create<System.Object, System.Int32>(Href("~/Content/Filer/jquery.filer-dragdropbox-theme.css")
, 299), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 379), Tuple.Create("\"", 416)
, Tuple.Create(Tuple.Create("", 385), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/filer/jquery.filer.js")
, 385), false)
);

WriteLiteral(@"></script>

<style>
.jFiler-input-dragDrop{width:200px; height:auto; padding:0px; }
.jFiler-item-thumb-image{ }
.jFiler-item-thumb-image img{ width:100%; height:150px; }
.jFiler-item-list{ margin:0px;}
.jFiler-input-dragDrop .jFiler-input-icon{ margin-top:0px; height: 150px;}
.jFiler-item .jFiler-item-container{ margin:0px; padding:0px; }
.jFiler-item .jFiler-item-container .jFiler-item-assets{ margin-top:0px;}
.jFiler-item .jFiler-item-container .jFiler-item-assets .jFiler-jProgressBar{ width:226px;  margin-left:0px; }
.can_btn i{ font-size:25px;}
.can_btn{ padding:0px;}
.jFiler-input-text{ margin-bottom:5px;}
.jFiler-input-icon i{position: relative;
    top: 35px;}
.coun_lef{ color:#000; margin-top:5px;}

.note-editor .note-fontname{ display: none;}
.note-editor .note-fontsize{ display: none;}
.note-editor .note-color{ display: none;}
.note-editor .note-height{ display: none;}
.note-editor .note-table{ display: none;}
.note-editor .note-insert{ display: none;}
.note-editor .note-help{ display: none;}

   
</style>

        <div");

WriteLiteral(" class=\"col-sm-12\"");

WriteLiteral(">\r\n         \r\n            <p");

WriteLiteral(" class=\"p_info\"");

WriteLiteral(">Organizer Profile</p>\r\n        </div>\r\n<div");

WriteLiteral(" class=\"col-sm-10 mb10 col-xs-12\"");

WriteLiteral(" id=\"diverroacc\"");

WriteLiteral(" style=\"display :none;\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"er_UI_main\"");

WriteLiteral(" id=\"dveruimain\"");

WriteLiteral(">\r\n <div");

WriteLiteral(" class=\"er_UI_img\"");

WriteLiteral(" id=\"dveruiimg\"");

WriteLiteral("></div>\r\n        <div");

WriteLiteral(" class=\"er_suc\"");

WriteLiteral(" id=\"erraccmsg\"");

WriteLiteral(">  </div>\r\n        <button");

WriteLiteral(" class=\"close\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" id=\"btndiverroacc\"");

WriteLiteral(" tabindex=\"-1\"");

WriteLiteral(" onclick=\"$(\'#diverroacc\').hide(); $(\'input\').removeClass(\'err-bor\');\"");

WriteLiteral(">&#215;</button>\r\n     </div>\r\n\r\n</div>\r\n        <div");

WriteLiteral(" class=\"clearfix\"");

WriteLiteral("></div>\r\n        <div");

WriteLiteral(" class=\"col-sm-12 mb20 no_pad maindivnonvis\"");

WriteLiteral(" >\r\n\r\n            <div");

WriteLiteral(" class=\"col-sm-8 clearfix \"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"col-sm-8 ev_orgnz_lebel no_pad clearfix\"");

WriteLiteral(">\r\n                    Select Organizer\r\n                </div>\r\n                " +
"<div");

WriteLiteral(" class=\"col-sm-6 no_pad col-xs-12 dropheight mt5\"");

WriteLiteral(">\r\n                    <select");

WriteLiteral(" class=\"form-control evnt_inp_cont selectpicker\"");

WriteLiteral(" id=\"id_selectorg\"");

WriteLiteral(" >\r\n                        <option> Select </option>\r\n                        <o" +
"ption");

WriteLiteral(" value=\"A\"");

WriteLiteral("> Add Organizer </option>\r\n");

            
            #line 65 "..\..\Views\Account\OrganizerProfile.cshtml"
                       
            
            #line default
            #line hidden
            
            #line 65 "..\..\Views\Account\OrganizerProfile.cshtml"
                        foreach (var item in Model.Organizerdetail)
                       {

            
            #line default
            #line hidden
WriteLiteral("                        <option");

WriteAttribute("value", Tuple.Create(" value=", 2697), Tuple.Create("", 2721)
            
            #line 67 "..\..\Views\Account\OrganizerProfile.cshtml"
, Tuple.Create(Tuple.Create("", 2704), Tuple.Create<System.Object, System.Int32>(item.Orgnizer_Id
            
            #line default
            #line hidden
, 2704), false)
);

WriteLiteral(">");

            
            #line 67 "..\..\Views\Account\OrganizerProfile.cshtml"
                                                   Write(item.Orgnizer_Name);

            
            #line default
            #line hidden
WriteLiteral("</option>\r\n");

            
            #line 68 "..\..\Views\Account\OrganizerProfile.cshtml"
                       }

            
            #line default
            #line hidden
WriteLiteral("                       \r\n                     \r\n                    </select>\r\n  " +
"              </div>\r\n\r\n                <div");

WriteLiteral(" class=\"col-sm-3 col-xs-4 mt5 \"");

WriteLiteral(">\r\n                    <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-lg ev_org_add_btn xs_btn_orgz\"");

WriteLiteral(">Edit </button>\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"col-sm-3 col-xs-4 mt5 \"");

WriteLiteral(">\r\n                    <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-lg ev_org_add_btn xs_btn_orgz\"");

WriteLiteral(">Delete </button>\r\n                </div>\r\n\r\n\r\n\r\n            </div>\r\n        </di" +
"v>\r\n        <div");

WriteLiteral(" class=\"clearfix\"");

WriteLiteral("></div>\r\n        <div");

WriteLiteral(" id=\"divorgprofile\"");

WriteLiteral("></div>\r\n        <div");

WriteLiteral(" class=\"col-sm-3 orgdisnone\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" type=\"file\"");

WriteLiteral(" name=\"image\"");

WriteLiteral(" id=\"input2\"");

WriteLiteral(">\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"col-sm-7 orgdisnone\"");

WriteLiteral(">\r\n\r\n            <form");

WriteLiteral(" class=\"form-horizontal acnt_form\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                    <label");

WriteLiteral(" class=\"col-md-3 control-label acnt_form_label \"");

WriteLiteral(">Name</label>\r\n                    <div");

WriteLiteral(" class=\"col-md-9\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-xs-12\"");

WriteLiteral(">\r\n                                <input");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"txt_name\"");

WriteLiteral(" maxlength=\"256\"");

WriteLiteral(">\r\n                                <small");

WriteLiteral(" class=\"acnt_form_name_mini\"");

WriteLiteral(">First Name</small>\r\n                            </div>\r\n\r\n                      " +
"  </div>\r\n                    </div>\r\n                </div>\r\n                <d" +
"iv");

WriteLiteral(" class=\"clearfix\"");

WriteLiteral("></div>\r\n                <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                    <label");

WriteLiteral(" class=\"col-md-3 control-label acnt_form_label\"");

WriteLiteral(">Address</label>\r\n                    <div");

WriteLiteral(" class=\"col-md-9\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-xs-12 add_form_bot\"");

WriteLiteral(">\r\n                                <input");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"txt_streetadd1\"");

WriteLiteral(" maxlength=\"256\"");

WriteLiteral(">\r\n                                <small");

WriteLiteral(" class=\"acnt_form_name_mini\"");

WriteLiteral(">Street Address</small>\r\n                            </div>\r\n                    " +
"        <div");

WriteLiteral(" class=\"col-xs-12 add_form_bot\"");

WriteLiteral(">\r\n                                <input");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"txt_streetaddr2\"");

WriteLiteral(" maxlength=\"256\"");

WriteLiteral(">\r\n                                <small");

WriteLiteral(" class=\"acnt_form_name_mini\"");

WriteLiteral(">Street Address 2</small>\r\n                            </div>\r\n                  " +
"      </div>\r\n                        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-xs-6\"");

WriteLiteral(">\r\n                                <input");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"txt_city\"");

WriteLiteral(" maxlength=\"256\"");

WriteLiteral(">\r\n                                <small");

WriteLiteral(" class=\"acnt_form_name_mini\"");

WriteLiteral(">City</small>\r\n                            </div>\r\n                            <d" +
"iv");

WriteLiteral(" class=\"col-xs-6\"");

WriteLiteral(">\r\n                                <input");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"txt_state\"");

WriteLiteral(" maxlength=\"256\"");

WriteLiteral(">\r\n                                <small");

WriteLiteral(" class=\"acnt_form_name_mini\"");

WriteLiteral(">State</small>\r\n                            </div>\r\n\r\n                        </d" +
"iv>\r\n                        <div");

WriteLiteral(" class=\"row mt10\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-xs-6\"");

WriteLiteral(">\r\n                                <input");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"txt_zip\"");

WriteLiteral(" maxlength=\"5\"");

WriteLiteral(">\r\n                                <small");

WriteLiteral(" class=\"acnt_form_name_mini\"");

WriteLiteral(">Postal / ZipCode</small>\r\n                            </div>\r\n                  " +
"          <div");

WriteLiteral(" class=\"col-xs-6\"");

WriteLiteral(">\r\n");

WriteLiteral("                                ");

            
            #line 136 "..\..\Views\Account\OrganizerProfile.cshtml"
                           Write(Html.DropDownList("Country", ViewBag.Countries as List<SelectListItem>, new { @class = "form-control selectpicker" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                            \r\n                                <small");

WriteLiteral(" class=\"acnt_form_name_mini\"");

WriteLiteral(">Country</small>\r\n                            </div>\r\n\r\n                        <" +
"/div>\r\n                    </div>\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"clearfix\"");

WriteLiteral("></div>\r\n                <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                    <label");

WriteLiteral(" class=\"col-md-3 control-label acnt_form_label\"");

WriteLiteral(">Email</label>\r\n                    <div");

WriteLiteral(" class=\"col-md-9\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-xs-12 add_form_bot\"");

WriteLiteral(">\r\n                                <input");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"txt_email\"");

WriteLiteral(" maxlength=\"100\"");

WriteLiteral(">\r\n                            </div>\r\n                        </div>\r\n          " +
"          </div>\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"clearfix\"");

WriteLiteral("></div>\r\n                <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                    <label");

WriteLiteral(" class=\"col-md-3 control-label acnt_form_label\"");

WriteLiteral(">Phone Number </label>\r\n                    <div");

WriteLiteral(" class=\"col-md-9\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-xs-12 add_form_bot\"");

WriteLiteral(">\r\n                                <input");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"txt_phnno\"");

WriteLiteral(">\r\n                            </div>\r\n                        </div>\r\n          " +
"          </div>\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"clearfix\"");

WriteLiteral("></div>\r\n                <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                    <label");

WriteLiteral(" class=\"col-md-3 control-label acnt_form_label\"");

WriteLiteral(">Website URL</label>\r\n                    <div");

WriteLiteral(" class=\"col-md-9\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-xs-12 add_form_bot\"");

WriteLiteral(">\r\n                                <input");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"txt_websiteurl\"");

WriteLiteral(" maxlength=\"256\"");

WriteLiteral(">\r\n                            </div>\r\n                        </div>\r\n          " +
"          </div>\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"clearfix\"");

WriteLiteral("></div>\r\n                <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                    <label");

WriteLiteral(" class=\"col-md-3 control-label acnt_form_label\"");

WriteLiteral(">Facebook URL </label>\r\n                    <div");

WriteLiteral(" class=\"col-md-9\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-xs-12 add_form_bot\"");

WriteLiteral(">\r\n                                <input");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"txt_fburl\"");

WriteLiteral(" maxlength=\"200\"");

WriteLiteral(">\r\n                            </div>\r\n                        </div>\r\n          " +
"          </div>\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"clearfix\"");

WriteLiteral("></div>\r\n                <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                    <label");

WriteLiteral(" class=\"col-md-3 control-label acnt_form_label\"");

WriteLiteral(">Twitter URL</label>\r\n                    <div");

WriteLiteral(" class=\"col-md-9\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-xs-12 add_form_bot\"");

WriteLiteral(">\r\n                                <input");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"txt_twitterurl\"");

WriteLiteral(" maxlength=\"200\"");

WriteLiteral(">\r\n                            </div>\r\n                        </div>\r\n          " +
"          </div>\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"clearfix\"");

WriteLiteral("></div>\r\n                <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                    <label");

WriteLiteral(" class=\"col-md-3 control-label acnt_form_label\"");

WriteLiteral(">LinkedIn URL</label>\r\n                    <div");

WriteLiteral(" class=\"col-md-9\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-xs-12 add_form_bot\"");

WriteLiteral(">\r\n                                <input");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"txt_linkedinurl\"");

WriteLiteral(" maxlength=\"200\"");

WriteLiteral(">\r\n                            </div>\r\n                        </div>\r\n          " +
"          </div>\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"clearfix\"");

WriteLiteral("></div>\r\n                <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                    <label");

WriteLiteral(" class=\"col-md-3 control-label acnt_form_label\"");

WriteLiteral(">Description </label>\r\n                    <div");

WriteLiteral(" class=\"col-md-9 orgaz_editor\"");

WriteLiteral(@">
                        <!-- <div class=""row"">
                          <div class=""col-xs-12 add_form_bot"">
                            <textarea class=""form-control""></textarea>
                          </div>
                        </div> -->
                        <div");

WriteLiteral(" class=\"summernote\"");

WriteLiteral(" id=\"txt_desc\"");

WriteLiteral("></div>\r\n                    </div>\r\n                </div>\r\n                <inp" +
"ut");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ImagePresent\"");

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"userimage\"");

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" value=\"0\"");

WriteLiteral(" id=\"imageeror\"");

WriteLiteral(" />\r\n                <input");

WriteLiteral(" id=\"image_count\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" />\r\n                <button");

WriteLiteral(" class=\"btn btn-info col-md-offset-3 acnt_btn\"");

WriteLiteral(" title=\"\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" id=\"btnadd\"");

WriteLiteral(">Add</button>\r\n            </form>\r\n        </div>\r\n\r\n\r\n\r\n\r\n\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\r\n    $(document).ready(function () {\r\n        $(\'.summernote\').summernote({\r\n" +
"            height: 70,   //set editable area\'s height\r\n            disableDragA" +
"ndDrop: true\r\n        });\r\n\r\n        $(\"#txt_phnno\").mask(\"999-999-9999\");\r\n    " +
"    \r\n\r\n             $(\'#input2\').filer({\r\n                 limit: \'1\',\r\n       " +
"          maxSize: \'2\',\r\n                 extensions: [\'jpg\', \'jpeg\', \'png\'],\r\n " +
"                changeInput: \'<div class=\"jFiler-input-dragDrop\" id=\"drag-drop\">" +
"<div class=\"jFiler-input-inner\"><div class=\"jFiler-input-icon\"><i class=\"icon-jf" +
"i-cloud-up-o\"></i></div><div class=\"jFiler-input-text\"><h3>Drag  photo or normal" +
" upload</h3> </div></div></div>\',\r\n                 showThumbs: true,\r\n         " +
"        appendTo: \"#drag-drop\",\r\n                 theme: \"dragdropbox\",\r\n       " +
"          onChange: false,\r\n                 templates: {\r\n                     " +
"box: \'<ul class=\"jFiler-item-list profile-views clearfix\"></ul>\',\r\n             " +
"        item: \'<li class=\"jFiler-item\">\\\r\n                                    <d" +
"iv class=\"\">\\\r\n                                            {{fi-image}}\\\r\n      " +
"                                       <div class=\"jFiler-item-container\">\\\r\n   " +
"                             <div class=\"jFiler-item-inner\">\\\r\n                 " +
"                    <div class=\"jFiler-item-assets jFiler-row\">\\\r\n              " +
"                          <ul class=\"list-inline pull-left\">\\\r\n                 " +
"                           <li>{{fi-progressBar}}</li>\\\r\n                       " +
"                 </ul>\\\r\n                                    </div>\\\r\n          " +
"                          </div>\\\r\n                            </div>\\\r\n        " +
"                                    <span></span>\\\r\n                            " +
"                 <button class=\"btn  jFiler-item-trash-action can_btn\" type=\"but" +
"ton\">\\\r\n                                                <i class=\"glyphicon glyp" +
"hicon-remove-circle\"></i>\\\r\n                                             </butto" +
"n>\\\r\n                                       </div>\\\r\n                        </l" +
"i>\',\r\n                     itemAppend: \'<li class=\"jFiler-item\">\\\r\n             " +
"                       <div class=\"\">\\\r\n                                        " +
"    {{fi-image}}\\\r\n                                             <button class=\"b" +
"tn  jFiler-item-trash-action can_btn\" type=\"button\">\\\r\n                         " +
"                       <i class=\"glyphicon glyphicon-remove-circle\"></i>\\\r\n     " +
"                                        </button>\\\r\n                            " +
"           </div>\\\r\n                        </li>\',\r\n\r\n\r\n                     pr" +
"ogressBar: \'<div class=\"bar\"></div>\',\r\n                     itemAppendToEnd: tru" +
"e,\r\n                     removeConfirmation: false,\r\n                     _selec" +
"tors: {\r\n                         list: \'.jFiler-item-list\',\r\n                  " +
"       item: \'.jFiler-item\',\r\n                         progressBar: \'.bar\',\r\n   " +
"                      remove: \'.jFiler-item-trash-action\',\r\n                    " +
" }\r\n                 },\r\n                 uploadFile: {\r\n                     ur" +
"l: \"/Account/SaveOrganizerImage\",\r\n                     data: {},\r\n             " +
"        type: \'POST\',\r\n                     enctype: \'multipart/form-data\',\r\n   " +
"                  beforeSend: function () { },\r\n                     success: fu" +
"nction (data, el) {\r\n\r\n                         var parent = el.find(\".jFiler-jP" +
"rogressBar\").parent();\r\n                         el.find(\".jFiler-jProgressBar\")" +
".fadeOut(\"slow\", function () {\r\n                             $(\"<div class=\\\"jFi" +
"ler-item-others text-success\\\"><i class=\\\"icon-jfi-check-circle\\\"></i> Success</" +
"div>\").hide().appendTo(parent).fadeIn(\"slow\").remove();\r\n                       " +
"  });\r\n                         console.log(data);\r\n                         var" +
" image_count = $(\"#image_count\").val();\r\n                         var imagejson " +
"= jQuery.parseJSON(JSON.stringify(data));\r\n                         var imgdata " +
"= imagejson.image_name + \'¶\' + imagejson.image_type ;\r\n\r\n                       " +
"  $(\'#userimage\').val(imgdata);\r\n\r\n\r\n                         $(\'#ImagePresent\')" +
".val(\"Yes\");\r\n\r\n                         if ($.trim(image_count) == \'\') {\r\n     " +
"                        $(\"#image_count\").val(\'1\');\r\n                         } " +
"else {\r\n                             image_count = parseInt(image_count) + 1;\r\n " +
"                            $(\"#image_count\").val(image_count);\r\n               " +
"          }\r\n\r\n                     },\r\n                     error: function (el" +
") {\r\n\r\n                         //var parent = el.find(\".jFiler-jProgressBar\").p" +
"arent();\r\n                         //el.find(\".jFiler-jProgressBar\").fadeOut(\"sl" +
"ow\", function(){\r\n                         //    $(\"<div class=\\\"jFiler-item-oth" +
"ers text-error\\\"><i class=\\\"icon-jfi-minus-circle\\\"></i> Error</div>\").hide().ap" +
"pendTo(parent).fadeIn(\"slow\");\r\n                         //});\r\n                " +
"     },\r\n                     statusCode: {},\r\n                     onProgress: " +
"function () { },\r\n                 },\r\n                 dragDrop: {\r\n           " +
"          dragEnter: function () { },\r\n                     dragLeave: function " +
"() { },\r\n                     drop: function () { },\r\n                 },\r\n     " +
"            addMore: false,\r\n                 clipBoardPaste: true,\r\n           " +
"      excludeName: null,\r\n                 beforeShow: function () { $(\".jFiler-" +
"input-inner\").hide(); return true },\r\n                 onSelect: function () { $" +
"(\".jFiler-input-inner\").hide(); },\r\n                 afterShow: function () { $(" +
"\".jFiler-input-inner\").hide(); $(\".fi-progressBar\").hide(); },\r\n                " +
" onRemove: function (el) {\r\n                     var image_count = $(\"#image_cou" +
"nt\").val();\r\n                     image_count = parseInt(image_count) - 1;\r\n    " +
"                 var id_div = el[0].jfiler_id;\r\n                     var hidden_" +
"image_element = \'image_hidden_\' + id_div;\r\n\r\n                     $(\'#userimage\'" +
").val(\'\');\r\n                     $(\'#input2\').click(function (e) {\r\n            " +
"             e.preventDefault();\r\n\r\n                     })\r\n                   " +
"  $(\'#ImagePresent\').val(\"NO\");\r\n                 },\r\n                 onEmpty: " +
"function (el) {\r\n                     if ($(\"#imageeror\").val() == 0) {\r\n       " +
"                  if ($(\".jFiler-items\").length == 1) { setTimeout(function () {" +
" $(\".jFiler-input-inner\").css(\'display\', \'block\'); $(\'#input2\').unbind(\'click\');" +
" }, 200); };\r\n                     }\r\n                 },\r\n                 capt" +
"ions: {\r\n                     button: \"Choose Files\",\r\n                     feed" +
"back: \"Choose files To Upload\",\r\n                     feedback2: \"files were cho" +
"sen\",\r\n                     drop: \"Drop file here to Upload\",\r\n                 " +
"    removeConfirmation: \"Are you sure you want to remove this file?\",\r\n         " +
"            errors: {\r\n                         filesLimit: \'\',\r\n               " +
"          filesType: \'\',\r\n                         filesSize: \'\',\r\n             " +
"            filesSizeAll: \'\'\r\n                     }\r\n                 }\r\n\r\n    " +
"         });\r\n\r\n             $(\"#id_selectorg\").change(function () {\r\n          " +
"     var selectval= $(this).val();\r\n\r\n               if(selectval==\"A\")\r\n       " +
"        {\r\n                   $(\".orgdisnone\").show();\r\n                   $(\".m" +
"aindivnonvis\").hide();\r\n               } else {\r\n                   $(\".orgdisno" +
"ne\").hide();\r\n                   $(\".maindivnonvis\").show();\r\n               }\r\n" +
"\r\n             });\r\n             $(\'#txt_zip\').change(function () {\r\n           " +
"      var country = $(\"#Country option:selected\").text().trim();\r\n\r\n            " +
"     var city = \"\";\r\n                 var state = \"\";\r\n\r\n                 var pi" +
"ncode = $(this).val();\r\n                 $.ajax({\r\n                     url: \'ht" +
"tp://maps.googleapis.com/maps/api/geocode/json\',\r\n                     data: \'&a" +
"ddress=\' + pincode + \'&sensor=true\',\r\n                     dataType: \'json\',\r\n  " +
"                   cache: false,\r\n                     success: function (data) " +
"{\r\n\r\n\r\n                         if (data.status == \'OK\') {\r\n                    " +
"         $(\'#txt_zip\').removeClass(\'err-bor\');\r\n\r\n                             f" +
"or (var i = 0; i < data.results[0].address_components.length; i++) {\r\n          " +
"                       var obj = data.results[0].address_components[i];\r\n\r\n     " +
"                            var obj2 = obj[\'types\'];\r\n\r\n                        " +
"         for (var j = 0; j < obj2.length; j++) {\r\n                              " +
"       if (obj2[j] == \'locality\' || obj2[j] == \'postal_town\' || obj2[j] == \'subl" +
"ocality_level_1\' || obj2[j] == \'sublocality_level_2\' || obj2[j] == \'sublocality_" +
"level_3\' || obj2[j] == \'sublocality_level_4\' || obj2[j] == \'sublocality_level_5\'" +
" || obj2[j] == \'ward\' ) {\r\n                                         city = obj[\'" +
"long_name\'];\r\n\r\n\r\n                                     }\r\n                      " +
"               if (obj2[j] == \'administrative_area_level_1\') {\r\n                " +
"                         state = obj[\'long_name\'];\r\n\r\n                          " +
"           }\r\n                                    if (obj2[j] == \'country\') {\r\n " +
"                                        if (obj[\'long_name\'] == country) {\r\n    " +
"                                         $(\'#txt_city\').val(city);\r\n            " +
"                                 $(\'#txt_state\').val(state);\r\n\r\n\r\n              " +
"                           }\r\n\r\n                                     }\r\n\r\n      " +
"                           }\r\n\r\n\r\n\r\n                             }\r\n\r\n          " +
"               }else {\r\n                            \r\n                          " +
"   $(\'#erraccmsg\').html(ajaxsetup(\'OrganizerMaster\', \'OrgvalidZipUi\'));\r\n       " +
"                      $(\'#diverroacc\').show();\r\n                             $(\'" +
"html,body\').animate({ scrollTop: 0 });\r\n\r\n\r\n                             $(\'#txt" +
"_zip\').addClass(\'err-bor\');\r\n                             return false;\r\n       " +
"                  }\r\n                     }\r\n                 });\r\n             " +
"});\r\n             $(\"#btnadd\").click(function () {\r\n                \r\n          " +
"       var msg = validatemain();\r\n                 if (msg != \'\')\r\n             " +
"    {\r\n                     $(\"#dveruimain\").addClass(\"er_UI_main\");\r\n          " +
"           $(\"#dveruiimg\").addClass(\"er_UI_img\");\r\n                     $(\"#dver" +
"uimain\").removeClass(\"er_suc_main\");\r\n                     $(\"#dveruiimg\").remov" +
"eClass(\"er_suc_img\");\r\n                     $(\'#erraccmsg\').html(msg);\r\n        " +
"              $(\'#diverroacc\').show();\r\n                     $(\'html,body\').anim" +
"ate({ scrollTop: 0 });\r\n                    \r\n                     return false;" +
"\r\n                 }\r\n                 var code = $(\"#txt_desc\").code();\r\n      " +
"           var text = code.replace(/<p>/gi, \" \");\r\n                 var plainTex" +
"t = $(\"<div />\").html(text).text();\r\n                 var model = {\r\n           " +
"          \'Orgnizer_Name\': $(\"#txt_name\").val(),\r\n                     \'Organize" +
"r_Desc\': plainText,\r\n                     \'Organizer_FBLink\': $(\"#txt_fburl\").va" +
"l(),\r\n                     \'Organizer_Twitter\': $(\"#txt_twitterurl\").val(),\r\n   " +
"                  \'Organizer_Linkedin\': $(\"#txt_linkedinurl\").val(),\r\n          " +
"           \'UserId\': \'\',\r\n                     \'Organizer_Image\': $(\"#userimage\"" +
").val(),\r\n                     \'Organizer_Address1\': $(\"#txt_streetadd1\").val()," +
"\r\n                     \'Organizer_Address2\': $(\"#txt_streetaddr2\").val(),\r\n     " +
"                \'Organizer_City\': $(\"#txt_city\").val(),\r\n                     \'O" +
"rganizer_State\': $(\"#txt_state\").val(),\r\n                     \'Organizer_Country" +
"Id\': $(\"#Country\").val(),\r\n                     \'Organizer_Zipcode\': $(\"#txt_zip" +
"\").val(),\r\n                     \'Organizer_Email\': $(\"#txt_email\").val(),\r\n     " +
"                \'Organizer_Phoneno\': $(\"#txt_phnno\").val(),\r\n                   " +
"  \'Organizer_Websiteurl\': $(\"#txt_websiteurl\").val(),\r\n\r\n                 };\r\n  " +
"               $.ajax({\r\n                     url: \'");

            
            #line 492 "..\..\Views\Account\OrganizerProfile.cshtml"
                      Write(Url.Action("saveOrganizer", "Account"));

            
            #line default
            #line hidden
WriteLiteral(@"',
                     data: { model: model},
                     type: ""Post"",
                     success: function (data) {
                         if (typeof (data.Message) === ""undefined"") {
                             $(""#id_selectorg"").empty();
                             $(""#id_selectorg"").append(""<option value='0'>Select</option>"");
                             $(""#id_selectorg"").append(""<option value='A'>Add Organizer</option>"");
                             $.each(data, function (index, optiondata) {

                                 $(""#id_selectorg"").append(""<option value='"" + optiondata.Id + ""'>"" + optiondata.Name + ""</option>"");
                             });
                             $('.selectpicker').selectpicker('refresh');

                         } else {
                             if(data.Message==""O"")
                             {
                                 window.location.href = '");

            
            #line 509 "..\..\Views\Account\OrganizerProfile.cshtml"
                                                    Write(Url.Action("Index", "Home"));

            
            #line default
            #line hidden
WriteLiteral("\' ;\r\n\r\n                             }\r\n                           \r\n             " +
"               \r\n                         }\r\n                         $(\"#dverui" +
"main\").removeClass(\"er_UI_main\");\r\n                         $(\"#dveruiimg\").remo" +
"veClass(\"er_UI_img\");\r\n                         $(\"#dveruimain\").addClass(\"er_su" +
"c_main\");\r\n                         $(\"#dveruiimg\").addClass(\"er_suc_img\");\r\n   " +
"                      $(\'#erraccmsg\').html(ajaxsetup(\'OrganizerMaster\', \'Orgsave" +
"Succ\'));\r\n                         $(\'#diverroacc\').show();\r\n                   " +
"      $(\'html,body\').animate({ scrollTop: 0 });\r\n                         $(\".or" +
"gdisnone\").hide();\r\n                         $(\".maindivnonvis\").show();\r\n\r\n    " +
"                     \r\n                     }\r\n                 });\r\n\r\n\r\n\r\n     " +
"        });\r\n\r\n    });\r\n    function validatemain()\r\n    {\r\n        debugger;\r\n " +
"       var msg = \"\";\r\n        var organizername = $(\"#txt_name\").val();\r\n       " +
" var organizeremail = $(\'#txt_email\').val();\r\n        if(organizername==\"\")\r\n   " +
"     {\r\n            $(\"#txt_name\").addClass(\'err-bor\');\r\n            msg+= ajaxs" +
"etup(\'OrganizerMaster\',\'OrgNameUi\') +\"</br>\";\r\n\r\n        } else {\r\n            $" +
"(\"#txt_name\").removeClass(\'err-bor\');\r\n        }\r\n\r\n        if (organizeremail !" +
"= \"\") {\r\n            if (!validateEmail(organizeremail)) {\r\n               \r\n   " +
"             $(\'#txt_email\').addClass(\'err-bor\');\r\n                msg += ajaxse" +
"tup(\'OrganizerMaster\', \'OrgvalidEmailUi\') + \"</br>\";\r\n\r\n            } else {\r\n\r\n" +
"\r\n                $(\'#txt_email\').removeClass(\'err-bor\');\r\n            }\r\n      " +
"  } else {\r\n            $(\'#txt_email\').addClass(\'err-bor\');\r\n            msg +=" +
" ajaxsetup(\'OrganizerMaster\', \'OrgEmailUi\') + \"</br>\";;\r\n\r\n        }\r\n        re" +
"turn msg;\r\n    }\r\n    function ajaxsetup(strname, strFormTag) {\r\n        var msg" +
"new = \"\";\r\n\r\n        var request = $.ajax({\r\n            url: \'");

            
            #line 571 "..\..\Views\Account\OrganizerProfile.cshtml"
             Write(Url.Action("Index", "ValidationMessage"));

            
            #line default
            #line hidden
WriteLiteral(@"',
            async: false,
            data: { strFormName: strname, strFormTag: strFormTag },
            type: 'Post'
        });
        request.done(function (msg) {
            msgnew += msg;
        });

        request.fail(function (jqXHR, textStatus) {
            msgnew += ""Some Error occur!!"";
        });
        return msgnew;
    }
</script>");

        }
    }
}
#pragma warning restore 1591
