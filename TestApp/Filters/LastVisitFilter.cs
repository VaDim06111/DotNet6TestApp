using Microsoft.AspNetCore.Mvc.Filters;

namespace TestApp.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public class LastVisitFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.HttpContext.Response.Cookies.Append("LastVisit", DateTime.Now.ToString("dd/MM/yyyy hh-mm-ss"));
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {         
        }
    }
}
