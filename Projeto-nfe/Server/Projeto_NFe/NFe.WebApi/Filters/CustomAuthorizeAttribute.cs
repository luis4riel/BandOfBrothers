using System.Diagnostics.CodeAnalysis;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace NFe.WebApi.Filters
{
    [ExcludeFromCodeCoverage]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return base.IsAuthorized(actionContext);
        }
    }
}