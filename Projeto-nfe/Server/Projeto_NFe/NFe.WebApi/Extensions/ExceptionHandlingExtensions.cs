using NFe.WebApi.Exceptions;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace NFe.WebApi.Extensions
{
    public static class ExceptionHandlingExtensions
    {
        public static HttpResponseMessage HandleExecutedContextException(this HttpActionExecutedContext context)
        {
            return context.Request.CreateResponse(HttpStatusCode.InternalServerError, ExceptionPayload.New(context.Exception));
        }
    }
}