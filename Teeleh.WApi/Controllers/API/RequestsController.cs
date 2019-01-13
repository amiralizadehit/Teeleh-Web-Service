using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Teeleh.Models;

namespace Teeleh.WApi.Controllers.API
{
    public class RequestsController : ApiController
    {
        private AppDbContext db;
        private string localDomain;

        public RequestsController()
        {
            db = new AppDbContext();
            localDomain = "http://" + HttpContext.Current.Request.Url.Host;
        }

        /*[HttpGet]
        [Route("api/requests")]
        public IHttpActionResult GetRequests()
        {

        }*/
    }
}
