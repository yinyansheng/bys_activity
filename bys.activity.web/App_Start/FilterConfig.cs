﻿using bys.activity.web.Filter;
using System.Web;
using System.Web.Mvc;

namespace bys.activity.web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogFilter());
            filters.Add(new LoginFilter());
        }
    }
}
