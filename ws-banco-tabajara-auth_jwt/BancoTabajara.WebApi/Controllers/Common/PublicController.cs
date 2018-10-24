using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BancoTabajara.WebApi.Controllers.Common
{
    [RoutePrefix("api/public")]
    public class PublicController : ApiController
    {
        [HttpGet]
        [Route("is-alive")]
        public IHttpActionResult IsAlive()
        {
            return Ok(true);
        }
    }
}
