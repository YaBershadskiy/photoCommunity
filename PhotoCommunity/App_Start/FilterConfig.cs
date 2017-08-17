using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace PhotoCommunity
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }

    public class CultureAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //string cultureName = null;
            //// Получаем куки из контекста, которые могут содержать установленную культуру
            //HttpCookie cultureCookie = filterContext.HttpContext.Request.Cookies["lang"];
            //if (cultureCookie != null)
            //    cultureName = cultureCookie.Value;
            //else
            //    cultureName = "ru";

            //// Список культур
            //List<string> cultures = new List<string>() { "ru", "en"};
            //if (!cultures.Contains(cultureName))
            //{
            //    cultureName = "ru";
            //}
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //не реализован
        }
    }
}
