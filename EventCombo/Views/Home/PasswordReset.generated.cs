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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Home/PasswordReset.cshtml")]
    public partial class _Views_Home_PasswordReset_cshtml : System.Web.Mvc.WebViewPage<EventCombo.Models.ResetPasswordViewModel>
    {
        public _Views_Home_PasswordReset_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\Home\PasswordReset.cshtml"
  
    ViewBag.Title = "PasswordReset";
    Layout = "~/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<div");

WriteLiteral(" class=\"container-fluid cont_margin no-padding con_scr0l_home\"");

WriteLiteral(" id=\"scrollhome\"");

WriteLiteral(">\r\n   \r\n        <div");

WriteLiteral(" class=\"col-sm-12\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"forget_container\"");

WriteLiteral(">\r\n                <h3");

WriteLiteral(" class=\"acnt_title\"");

WriteLiteral(">Reset your password?</h3>\r\n               \r\n                <div");

WriteLiteral(" class=\"forget_con_inner\"");

WriteLiteral(">\r\n");

            
            #line 15 "..\..\Views\Home\PasswordReset.cshtml"
                    
            
            #line default
            #line hidden
            
            #line 15 "..\..\Views\Home\PasswordReset.cshtml"
                     using (Html.BeginForm("PasswordReset", "Home", FormMethod.Post, new { role = "form", id = "formrst", @class = "form-horizontal acnt_form col-md-offset-1" }))
                    {    
            
            #line default
            #line hidden
            
            #line 16 "..\..\Views\Home\PasswordReset.cshtml"
                    Write(Html.AntiForgeryToken());

            
            #line default
            #line hidden
            
            #line 16 "..\..\Views\Home\PasswordReset.cshtml"
                                                 

                        if (ViewData["Message"] != null)
                        {

            
            #line default
            #line hidden
WriteLiteral("                            <div");

WriteLiteral(" class=\"er_suc_main alert-success\"");

WriteLiteral(" id=\"dvrstsucc\"");

WriteLiteral(">\r\n                                <button");

WriteLiteral(" class=\"close closebtn\"");

WriteLiteral(" aria-hidden=\"true\"");

WriteLiteral(" data-dismiss=\"alert\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" id=\"btndvrstsucc\"");

WriteLiteral(">&#215;</button>\r\n                                <div");

WriteLiteral(" class=\"er_suc_img\"");

WriteLiteral("></div>\r\n                                <div");

WriteLiteral(" class=\"er_suc\"");

WriteLiteral(">");

            
            #line 23 "..\..\Views\Home\PasswordReset.cshtml"
                                               Write(ViewData["Message"]);

            
            #line default
            #line hidden
WriteLiteral(" </div>\r\n                            </div>\r\n");

            
            #line 25 "..\..\Views\Home\PasswordReset.cshtml"

                        }
                       

            
            #line default
            #line hidden
WriteLiteral("                            <div");

WriteLiteral(" class=\"er_UI_main alert-danger\"");

WriteLiteral(" id=\"diverrorst\"");

WriteLiteral(" style=\"display :none;width:100%\"");

WriteLiteral(">\r\n                                <button");

WriteLiteral(" class=\"close closebtn\"");

WriteLiteral(" aria-hidden=\"true\"");

WriteLiteral(" data-dismiss=\"alert\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" id=\"btndiverrorst\"");

WriteLiteral(">&#215;</button>\r\n                                <div");

WriteLiteral(" class=\"er_UI_img\"");

WriteLiteral("></div>\r\n                                <div");

WriteLiteral(" class=\"er_suc\"");

WriteLiteral(" id=\"dvrstsuc\"");

WriteLiteral(">  </div>\r\n                            </div>\r\n");

            
            #line 33 "..\..\Views\Home\PasswordReset.cshtml"
                       

                        if (ViewData["Error"]!=null)
                        {

                    

            
            #line default
            #line hidden
WriteLiteral("                            <div");

WriteLiteral(" class=\"er_UI_main alert-danger\"");

WriteLiteral(" id=\"diverro1rst\"");

WriteLiteral(" style=\"width:100%\"");

WriteLiteral(">\r\n                                <button");

WriteLiteral(" class=\"close closebtn\"");

WriteLiteral(" aria-hidden=\"true\"");

WriteLiteral(" data-dismiss=\"alert\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" id=\"btndiverro1rst\"");

WriteLiteral(">&#215;</button>\r\n                                <div");

WriteLiteral(" class=\"er_sys_img\"");

WriteLiteral("></div>\r\n                                <div");

WriteLiteral(" class=\"er_suc\"");

WriteLiteral(" id=\"dvrstmsg\"");

WriteLiteral("> ");

            
            #line 42 "..\..\Views\Home\PasswordReset.cshtml"
                                                              Write(ViewData["Error"]);

            
            #line default
            #line hidden
WriteLiteral(" </div>\r\n                            </div>\r\n");

            
            #line 44 "..\..\Views\Home\PasswordReset.cshtml"
                       
                        }

            
            #line default
            #line hidden
WriteLiteral("                        <div");

WriteLiteral(" class=\"clearfix\"");

WriteLiteral("></div>\r\n");

WriteLiteral("                        <div");

WriteLiteral(" class=\"form-group \"");

WriteLiteral(">\r\n                            <label");

WriteLiteral(" class=\"col-md-4 control-label acnt_form_label\"");

WriteLiteral(">Password</label>\r\n                            <div");

WriteLiteral(" class=\"col-md-8\"");

WriteLiteral(">\r\n                                <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                                    <div");

WriteLiteral(" class=\"col-xs-12 add_form_bot\"");

WriteLiteral(">\r\n");

WriteLiteral("                                        ");

            
            #line 52 "..\..\Views\Home\PasswordReset.cshtml"
                                   Write(Html.PasswordFor(m => m.Password, new { @class = "form-control" ,id="txtPaswordrst", @maxlength = 15 }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                                      \r\n\r\n                                    <" +
"/div>\r\n                                </div>\r\n                            </div" +
">\r\n                        </div>\r\n");

WriteLiteral("                        <div");

WriteLiteral(" class=\"form-group \"");

WriteLiteral(">\r\n                            <label");

WriteLiteral(" class=\"col-md-4 control-label acnt_form_label\"");

WriteLiteral(">Confirm Password</label>\r\n                            <div");

WriteLiteral(" class=\"col-md-8\"");

WriteLiteral(">\r\n                                <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                                    <div");

WriteLiteral(" class=\"col-xs-12 add_form_bot\"");

WriteLiteral(">\r\n");

WriteLiteral("                                        ");

            
            #line 64 "..\..\Views\Home\PasswordReset.cshtml"
                                   Write(Html.PasswordFor(m => m.ConfirmPassword, new { @class = "form-control", id = "txtPaswordrstconfirm", @maxlength = 15 }));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n                                    </div>\r\n                               " +
" </div>\r\n                            </div>\r\n                        </div>\r\n");

WriteLiteral("                        <input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"btn btn-info  acnt_btn\"");

WriteLiteral(" value=\"Reset\"");

WriteLiteral(" id=\"btnreset\"");

WriteLiteral(" disabled=\"disabled\"");

WriteLiteral("  />\r\n");

            
            #line 72 "..\..\Views\Home\PasswordReset.cshtml"


                    }

            
            #line default
            #line hidden
WriteLiteral("                </div>\r\n            </div>\r\n        </div>\r\n\r\n</div>\r\n\r\n\r\n\r\n<scri" +
"pt");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    $(document).click(function () {\r\n        $(\'#dvrstsucc\').css(\'display\', \'n" +
"one\');\r\n        $(\'#diverrorst\').css(\'display\', \'none\');\r\n        $(\'#diverro1rs" +
"t\').css(\'display\', \'none\');\r\n       \r\n        $(\"input\").removeClass(\'err-bor\');" +
"\r\n    });\r\n    $(\'#txtPaswordrst\').bind(\"cut copy paste\", function (e) {\r\n      " +
"  e.preventDefault();\r\n    });\r\n    $(\"#txtPaswordrst\").on(\"keypress\", function " +
"(e) {\r\n\r\n        if (e.which == 32)\r\n            return false;\r\n    });\r\n    $(\'" +
"#txtPaswordrstconfirm\').bind(\"cut copy paste\", function (e) {\r\n        e.prevent" +
"Default();\r\n    });\r\n    $(\"#txtPaswordrstconfirm\").on(\"keypress\", function (e) " +
"{\r\n\r\n        if (e.which == 32)\r\n            return false;\r\n    });\r\n    $(\'#btn" +
"dvrstsucc\').click(function () {\r\n        $(\'#dvrstsucc\').css(\'display\', \'none\');" +
"\r\n        $(\'#diverrorst\').css(\'display\', \'none\');\r\n        $(\'#diverro1rst\').cs" +
"s(\'display\', \'none\');\r\n        $(\'#txtPaswordrst\').val(\"\");\r\n        $(\'#txtPasw" +
"ordrstconfirm\').val(\"\");\r\n        $(\"input\").removeClass(\'err-bor\');\r\n    })\r\n  " +
"  $(\'#btndiverrorst\').click(function () {\r\n        $(\'#dvrstsucc\').css(\'display\'" +
", \'none\');\r\n        $(\'#diverrorst\').css(\'display\', \'none\');\r\n        $(\'#diverr" +
"o1rst\').css(\'display\', \'none\');\r\n        $(\'#txtPaswordrst\').val(\"\");\r\n        $" +
"(\'#txtPaswordrstconfirm\').val(\"\");\r\n        $(\"input\").removeClass(\'err-bor\');\r\n" +
"    })\r\n\r\n    $(\'#btndiverro1rst\').click(function () {\r\n        $(\'#dvrstsucc\')." +
"css(\'display\', \'none\');\r\n        $(\'#diverrorst\').css(\'display\', \'none\');\r\n     " +
"   $(\'#diverro1rst\').css(\'display\', \'none\');\r\n        $(\'#txtPaswordrst\').val(\"\"" +
");\r\n        $(\'#txtPaswordrstconfirm\').val(\"\");\r\n        $(\"input\").removeClass(" +
"\'err-bor\');\r\n    })\r\n\r\n\r\n    $(document).ready(function () {\r\n\r\n        $(\'#txtP" +
"aswordrst\').keyup(function () {\r\n\r\n            if ($(this).val() != \'\' && $(\'#tx" +
"tPaswordrstconfirm\').val() != \'\') {\r\n                $(\'#btnreset\').removeAttr(\"" +
"disabled\");\r\n\r\n            } else {\r\n\r\n                $(\'#btnreset\').attr(\"disa" +
"bled\", true);\r\n            }\r\n\r\n        })\r\n        $(\'#txtPaswordrstconfirm\').k" +
"eyup(function () {\r\n            if ($(this).val() != \'\' && $(\'#txtPaswordrst\').v" +
"al() != \'\') {\r\n\r\n                $(\'#btnreset\').removeAttr(\"disabled\");\r\n\r\n     " +
"       } else {\r\n\r\n                $(\'#btnreset\').attr(\"disabled\", true);\r\n     " +
"       }\r\n\r\n        })\r\n\r\n    });\r\n    function validationResetPasword() {\r\n\r\n  " +
"      var msgrst = \"\";\r\n        var pwdcount = 0;\r\n        var Password = $(\"#tx" +
"tPaswordrst\").val();\r\n        var ConfirmPassword = $(\"#txtPaswordrstconfirm\").v" +
"al();\r\n        if (Password.length < 4) {\r\n            $(\'#txtPaswordrst\').focus" +
"();\r\n            $(\'#txtPaswordrst\').addClass(\'err-bor\');\r\n            pwdcount " +
"+= 1;\r\n              }\r\n     \r\n        if (ConfirmPassword.length < 4) {\r\n      " +
"      pwdcount += 1;\r\n                $(\'#txtPaswordrstconfirm\').focus();\r\n     " +
"           $(\'#txtPaswordrstconfirm\').addClass(\'err-bor\');\r\n               \r\n   " +
"         }\r\n\r\n        if (pwdcount > 0)\r\n        {\r\n            msgrst= ajaxsetu" +
"p(\'ResetPassword\',\'ResetPasswordPwdValidationUI\');\r\n\r\n        }\r\n              \r" +
"\n            return msgrst;\r\n           \r\n    }\r\n    $(\"#btnreset\").click(functi" +
"on () {\r\n      \r\n       \r\n        var msgloginn = validationResetPasword();\r\n   " +
"     console.log(msgloginn);\r\n       \r\n        //var flag1login = true;\r\n       " +
" if (msgloginn == \"\") {\r\n            $(\'#diverrorst\').css(\"display\", \"none\");\r\n " +
"           return true;\r\n          \r\n        }\r\n        else {\r\n            $(\'#" +
"diverro1rst\').css(\"display\", \"none\");\r\n            $(\'#dvrstsucc\').css(\"display\"" +
", \"none\");\r\n            $(\'#dvrstsuc\').html(msgloginn);\r\n            $(\'#diverro" +
"rst\').css(\"display\", \"block\");\r\n            //setTimeout(function () {\r\n        " +
"    //    $(\'#diverrorst\').fadeOut();\r\n            //    $(\'#txtPaswordrst\').val" +
"(\"\");\r\n            //    $(\'#txtPaswordrstconfirm\').val(\"\");\r\n            //    " +
"$(\'#txtPaswordrst\').css(\"border\", \"gray solid 1px\");\r\n            //    $(\'#txtP" +
"aswordrstconfirm\').css(\"border\", \"gray solid 1px\");\r\n            //}, 900)\r\n    " +
"        return false;\r\n        }\r\n    });\r\n\r\n    function ajaxsetup(strname, str" +
"FormTag) {\r\n        var msgnew = \"\";\r\n\r\n        var request = $.ajax({\r\n        " +
"    url: \"/ValidationMessage/Index\",\r\n            async: false,\r\n            dat" +
"a: { strFormName: strname, strFormTag: strFormTag },\r\n            type: \'Post\'\r\n" +
"        });\r\n        request.done(function (msg) {\r\n            msgnew += msg;\r\n" +
"        });\r\n\r\n        request.fail(function (jqXHR, textStatus) {\r\n            " +
"msgnew += \"Some Error occur!!\";\r\n        });\r\n        return msgnew;\r\n    }\r\n   " +
" </script>");

        }
    }
}
#pragma warning restore 1591