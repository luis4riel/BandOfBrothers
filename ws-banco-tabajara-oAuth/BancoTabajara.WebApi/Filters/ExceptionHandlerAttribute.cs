using BancoTabajara.WebApi.Extensions;
using System.Web.Http.Filters;

namespace BancoTabajara.WebApi.Filters
{
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            context.Response = context.HandleExecutedContextException();
        }
    }
}