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
    
    #line 1 "..\..\Views\Home\ForgetPassword.cshtml"
    using EventCombo.Models;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Home/ForgetPassword.cshtml")]
    public partial class _Views_Home_ForgetPassword_cshtml : System.Web.Mvc.WebViewPage<ForgetPassword>
    {
        public _Views_Home_ForgetPassword_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral(" ");

            
            #line 3 "..\..\Views\Home\ForgetPassword.cshtml"
   
    ViewBag.Title = "ForgetPassword";
    

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n \r\n        <div");

WriteLiteral(" class=\"col-sm-12 forgot_top forgot_height\"");

WriteLiteral(" id=\"forgotcheck\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"forget_container\"");

WriteLiteral(">\r\n                <h3");

WriteLiteral(" class=\"acnt_title forg_acnt_title\"");

WriteLiteral(">Forgot your password?</h3>\r\n                <p");

WriteLiteral(" class=\"forget_title\"");

WriteLiteral(">Please provide your login email</p>\r\n                <div");

WriteLiteral(" class=\"forget_con_inner\"");

WriteLiteral(">\r\n");

            
            #line 15 "..\..\Views\Home\ForgetPassword.cshtml"
 using (Html.BeginForm("ForgetPassword", "Home", FormMethod.Post, new { role = "form", id = "formfrgt", @class = "form-horizontal acnt_form acnt_form_forget col-md-offset-1" }))
{
                   
                        if (ViewData["Message"] != null)
                        {

            
            #line default
            #line hidden
WriteLiteral("                            <div");

WriteLiteral(" class=\"er_suc_main\"");

WriteLiteral(" id=\"divaccfrgtsuc\"");

WriteLiteral(">\r\n                                <button");

WriteLiteral(" class=\"close\"");

WriteLiteral("  type=\"button\"");

WriteLiteral(" id=\"btndivaccfrgtsuc\"");

WriteLiteral(">&#215;</button>\r\n                                <div");

WriteLiteral(" class=\"er_suc_img\"");

WriteLiteral("></div>\r\n                                <div");

WriteLiteral(" class=\"er_suc\"");

WriteLiteral(" id=\"divsuccfrgt\"");

WriteLiteral(">");

            
            #line 23 "..\..\Views\Home\ForgetPassword.cshtml"
                                                                Write(ViewData["Message"]);

            
            #line default
            #line hidden
WriteLiteral(" </div>\r\n                            </div>\r\n");

            
            #line 25 "..\..\Views\Home\ForgetPassword.cshtml"

                        }

            
            #line default
            #line hidden
WriteLiteral("                        <div");

WriteLiteral(" class=\"err_main\"");

WriteLiteral(" id=\"diverrofrgt\"");

WriteLiteral(" style=\"display :none;\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"er_UI_main\"");

WriteLiteral(" >\r\n                                <button");

WriteLiteral(" class=\"close\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" id=\"btndiverrofrgt\"");

WriteLiteral(">&#215;</button>\r\n                                <div");

WriteLiteral(" class=\"er_UI_img\"");

WriteLiteral("></div>\r\n                                <div");

WriteLiteral(" class=\"er_suc\"");

WriteLiteral(" id=\"dvfrgtui\"");

WriteLiteral(">  </div>\r\n                            </div>\r\n                        </div>\r\n");

            
            #line 34 "..\..\Views\Home\ForgetPassword.cshtml"



            
            #line default
            #line hidden
WriteLiteral("                        <div");

WriteLiteral(" class=\"err_main\"");

WriteLiteral(" id=\"diverro1frgt\"");

WriteLiteral(" style=\"display :none;\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"er_UI_main\"");

WriteLiteral(">\r\n                                <button");

WriteLiteral(" class=\"close \"");

WriteLiteral("  type=\"button\"");

WriteLiteral(" id=\"btndiverro1frgt\"");

WriteLiteral(">&#215;</button>\r\n                                <div");

WriteLiteral(" class=\"er_sys_img\"");

WriteLiteral("></div>\r\n                                <div");

WriteLiteral(" class=\"er_suc\"");

WriteLiteral(" id=\"dvfrgtsys\"");

WriteLiteral(">  </div>\r\n                            </div>\r\n                        </div>\r\n");

WriteLiteral("                        <div");

WriteLiteral(" class=\"form-group \"");

WriteLiteral(">\r\n                            <label");

WriteLiteral(" class=\"col-md-4 control-label acnt_form_label\"");

WriteLiteral(">E-mail (Used as login)</label>\r\n                            <div");

WriteLiteral(" class=\"col-md-8\"");

WriteLiteral(">\r\n                                <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                                    <div");

WriteLiteral(" class=\"col-xs-12 add_form_bot\"");

WriteLiteral(">\r\n                                        <input");

WriteLiteral(" class=\" form-control\"");

WriteLiteral(" id=\"EmailPAssword\"");

WriteLiteral(" name=\"Email\"");

WriteLiteral(" size=\"32\"");

WriteLiteral(" value=\"\"");

WriteLiteral(" type=\"email\"");

WriteLiteral(" maxlength=\"256\"");

WriteLiteral(" autocomplete=\"off\"");

WriteLiteral(">\r\n                                        \r\n                                    " +
"</div>\r\n                                </div>\r\n                            </di" +
"v>\r\n                        </div>\r\n");

WriteLiteral("                        <input");

WriteLiteral(" type=\"button\"");

WriteLiteral(" value=\"SUBMIT\"");

WriteLiteral(" class=\"btn btn-info  acnt_btn\"");

WriteLiteral(" id=\"btnSavePassword\"");

WriteLiteral("  disabled=\"disabled\"");

WriteLiteral(" />\r\n");

            
            #line 55 "..\..\Views\Home\ForgetPassword.cshtml"
                       
               
}

            
            #line default
            #line hidden
WriteLiteral("                </div>\r\n            </div>\r\n        </div>\r\n\r\n\r\n<div");

WriteLiteral(" class=\"clearfix\"");

WriteLiteral("></div>\r\n\r\n\r\n\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    $(document).click(function () {\r\n\r\n      \r\n        $(\'#divaccfrgtsuc\').css" +
"(\'display\', \'none\');\r\n        $(\'#diverrofrgt\').css(\'display\', \'none\');\r\n       " +
" $(\'#diverro1frgt\').css(\'display\', \'none\');\r\n\r\n        $(\"input\").removeClass(\'e" +
"rr-bor\');\r\n    });\r\n\r\n    //$(\'#EmailPAssword\').bind(\"cut copy paste\", function " +
"(e) {\r\n    //    e.preventDefault();\r\n    //});\r\n    $(\"#EmailPAssword\").on(\"key" +
"press\", function (e) {\r\n\r\n        if (e.which == 32)\r\n            return false;\r" +
"\n    });\r\n    $(\'#btndivaccfrgtsuc\').click(function () {\r\n        $(\'#divaccfrgt" +
"suc\').css(\'display\', \'none\');\r\n        $(\'#diverrofrgt\').css(\'display\', \'none\');" +
"\r\n        $(\'#diverro1frgt\').css(\'display\', \'none\');\r\n        $(\"input\").removeC" +
"lass(\'err-bor\');\r\n    })\r\n    $(\'#btndiverro1frgt\').click(function () {\r\n       " +
" $(\'#divaccfrgtsuc\').css(\'display\', \'none\');\r\n        $(\'#diverrofrgt\').css(\'dis" +
"play\', \'none\');\r\n        $(\'#diverro1frgt\').css(\'display\', \'none\');\r\n        $(\"" +
"input\").removeClass(\'err-bor\');\r\n    })\r\n\r\n    $(\'#btndiverrofrgt\').click(functi" +
"on () {\r\n        $(\'#divaccfrgtsuc\').css(\'display\', \'none\');\r\n        $(\'#diverr" +
"ofrgt\').css(\'display\', \'none\');\r\n        $(\'#diverro1frgt\').css(\'display\', \'none" +
"\');\r\n        $(\"input\").removeClass(\'err-bor\');\r\n    })\r\n    $(document).ready(f" +
"unction () {\r\n\r\n        $(\'#EmailPAssword\').keyup(function () {\r\n\r\n            i" +
"f ($(this).val() != \'\') {\r\n                $(\'#btnSavePassword\').removeAttr(\"dis" +
"abled\");\r\n\r\n            } else {\r\n\r\n                $(\'#btnSavePassword\').attr(\"" +
"disabled\", true);\r\n            }\r\n\r\n        })\r\n\r\n    });\r\n                funct" +
"ion validationfrgt()\r\n                {\r\n                    var msglogin = \"\";\r" +
"\n                    var Emailforgot = $(\"#EmailPAssword\").val();\r\n\r\n           " +
"         if (!validateEmail(Emailforgot)) {\r\n                        $(\'#EmailPA" +
"ssword\').focus();\r\n                        $(\'#EmailPAssword\').addClass(\'err-bor" +
"\');\r\n\r\n                        msglogin += ajaxsetup(\'ForgotPassword\', \'FrgtpwdE" +
"mailValidationUI\');\r\n\r\n                    } else {\r\n\r\n                        m" +
"sglogin = \"\";\r\n                        $(\'#EmailPAssword\').focus();\r\n           " +
"             $(\'#EmailPAssword\').removeClass(\'err-bor\');\r\n                    }\r" +
"\n\r\n                    return msglogin;\r\n                }\r\n                func" +
"tion validateEmail(sEmail) {\r\n                    var filter = /^[\\w\\-\\.\\+]+\\");

WriteLiteral("@[a-zA-Z0-9\\.\\-]+\\.[a-zA-z0-9]{2,4}$/;\r\n\r\n                    if (filter.test(sEm" +
"ail)) {\r\n\r\n                        return true;\r\n                    }\r\n        " +
"            else {\r\n\r\n\r\n                        return false;\r\n                 " +
"   }\r\n                }\r\n\r\n                $(\"#btnSavePassword\").click(function " +
"() {\r\n                    var msgloginn = validationfrgt();\r\n\r\n                 " +
"   var flag1login = true;\r\n                    if (msgloginn == \"\") {\r\n         " +
"               $(\'#divsuccfrgt\').css(\'display\', \'none\');\r\n                      " +
"  $(\'#diverrofrgt\').css(\"display\", \"none\");\r\n                        flag1 = tru" +
"e;\r\n                    }\r\n                    else {\r\n                        $" +
"(\'#divsuccfrgt\').css(\'display\', \'none\');\r\n                        $(\'#diverro1fr" +
"gt\').css(\'display\', \'none\');\r\n                        $(\'#dvfrgtui\').html(msglog" +
"inn);\r\n                        $(\'#diverrofrgt\').css(\"display\", \"block\");\r\n\r\n   " +
"                     return false;\r\n                    }\r\n                    i" +
"f (flag1login) {\r\n                        var flaglogin = false;\r\n              " +
"          var Emaillogin = $(\"#EmailPAssword\").val();\r\n\r\n\r\n                     " +
"   $.ajax({\r\n                            url: \"/Home/IsValid\",\r\n                " +
"            data: \'Email=\' + Emaillogin,\r\n                            dataType: " +
"\"text\",\r\n                            type: \'Post\',\r\n                            " +
"success: function (data, textStatus, xhr) {\r\n                              if (d" +
"ata != \'\') {\r\n                                    if (data ==\"NotFound\")\r\n      " +
"                              {\r\n\r\n                                        $(\'#d" +
"ivaccfrgtsuc\').css(\'display\', \'none\');\r\n                                        " +
"$(\'#EmailPAssword\').focus();\r\n                                        $(\'#EmailP" +
"Assword\').addClass(\'err-bor\');\r\n                                        $(\'#dive" +
"rrofrgt\').css(\"display\", \"none\");\r\n                                        $(\'#d" +
"iverro1frgt\').css(\'display\', \'block\');\r\n                                        " +
"var msglogin = ajaxsetup(\'ForgotPassword\', \'FrgtpwdEmailValidationSy\');\r\n       " +
"                                 $(\'#dvfrgtsys\').html(msglogin);\r\n\r\n            " +
"                            return false;\r\n\r\n                                   " +
" }\r\n                                    else if (data == \"Found\")\r\n             " +
"                       {\r\n                                        $(\'#divaccfrgt" +
"suc\').css(\'display\', \'none\');\r\n                                        $(\'#diver" +
"ro1frgt\').css(\'display\', \'none\');\r\n                                        $(\'#E" +
"mailPAssword\').removeClass(\'err-bor\');\r\n                                        " +
"$(\'#formfrgt\').submit();\r\n\r\n                                    }\r\n\r\n\r\n         " +
"                       }\r\n                            },\r\n                      " +
"      error: function (xhr, textStatus, errorThrown) {\r\n                        " +
"        alert(\"Req \" + xhr + \" status \" + textStatus + \"  Error \" + errorThrown)" +
";\r\n                            }\r\n                        });\r\n\r\n\r\n\r\n           " +
"         }\r\n                });\r\n\r\n                function ajaxsetup(strname, s" +
"trFormTag) {\r\n                    var msgnew = \"\";\r\n\r\n                    var re" +
"quest = $.ajax({\r\n                        url: \'");

            
            #line 223 "..\..\Views\Home\ForgetPassword.cshtml"
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
