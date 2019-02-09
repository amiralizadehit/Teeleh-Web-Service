using System;
using AutoMapper;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using Hangfire;
using System.Web.Routing;
using Teeleh.WApi.App_Start;

namespace Teeleh.WApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Mapper.Initialize(c=>c.AddProfile<MappingProfile>());
            AreaRegistration.RegisterAllAreas();
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Hangfire.GlobalConfiguration.Configuration
                .UseSqlServerStorage("HangfireConnection");
        }
    }
}
