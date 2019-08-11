using System;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.Owin;
using Owin;
using Teeleh.Models;
using Teeleh.WApi.Services.Hangfire.Dashboard_Authorization;
using Teeleh.WApi.Services;


[assembly: OwinStartup(typeof(Teeleh.WApi.Startup))]

namespace Teeleh.WApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new AuthorizationFilter()}
            });
            Processes process = new Processes();
            RecurringJob.AddOrUpdate(() => process.SendNotification(), Cron.MinuteInterval(2));
            app.UseHangfireServer();
        }
    }
}
