using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Mvc;
using Logging.Lib;

namespace PracticeTime.Web
{
    [ExcludeFromCodeCoverage]
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new PracticeTimeHandleError());
        }
    }

    public class PracticeTimeHandleError : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Logger.Instance.Writer.Write(filterContext.Exception.ToString()); 
            base.OnException(filterContext);
        }
    }
}
