﻿using System.Web;
using System.Web.Mvc;

namespace SmaxV2Attendance
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
