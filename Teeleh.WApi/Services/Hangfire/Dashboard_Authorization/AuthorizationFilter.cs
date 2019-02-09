using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangfire.Dashboard;

namespace Teeleh.WApi.Services.Hangfire.Dashboard_Authorization
{
    public class AuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            HttpContext ctx = HttpContext.Current;

            if (HttpContext.Current.Session["SessionKey"] == null)
            {

                return false;
            }

            return true;
        }
    }
}