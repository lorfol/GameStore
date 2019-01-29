using GameStore.Web.Logging;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomHandleErrorAttribute());
            filters.Add(new LogHttpRequest());
        }
    }
}
