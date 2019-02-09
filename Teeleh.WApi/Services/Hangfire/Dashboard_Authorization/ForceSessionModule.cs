using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Teeleh.WApi.Services.Hangfire.Dashboard_Authorization
{
    public class ForceSessionModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PostAuthorizeRequest += OnPostAuthorizeRequest;
        }

        public void Dispose() { }

        private void OnPostAuthorizeRequest(object sender, EventArgs eventArgs)
        {
            var context = ((HttpApplication)sender).Context;
            var request = context.Request;
            // for all requests, SetSessionStateBehavior to SessionStateBehavior.Required
            if ((request != null
                 && request.AppRelativeCurrentExecutionFilePath != null
                 && request.AppRelativeCurrentExecutionFilePath.StartsWith(
                     "~/hangfire",
                     StringComparison.InvariantCultureIgnoreCase)))
            {
                context.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }
    }
}