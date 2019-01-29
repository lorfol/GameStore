using System.Web.Mvc;

namespace GameStore.Web.Logging
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            GameStoreLogger.logger.Error(filterContext.Exception, filterContext.Exception.Message);
            base.OnException(filterContext);
        }
    }
}