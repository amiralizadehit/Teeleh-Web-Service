using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Teeleh.Models.CustomValidation.Website
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            if (HttpContext.Current.Session["SessionKey"] == null)
            {
                filterContext.Result = new RedirectResult("~/Admin/Login");
                return;
            }
            base.OnActionExecuting(filterContext);

        }
    }
}
