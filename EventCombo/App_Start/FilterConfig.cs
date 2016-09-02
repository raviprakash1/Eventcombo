﻿using EventCombo.ActionFilters;
using System.Web;
using System.Web.Mvc;

namespace EventCombo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomHandleErrorAttribute());
        }
    }
}
