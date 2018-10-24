using NFe.WebApi.Extensions;
using System.Web.Http.Filters;

namespace NFe.API.Filters
{
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            context.Response = context.HandleExecutedContextException();
        }
    }
}