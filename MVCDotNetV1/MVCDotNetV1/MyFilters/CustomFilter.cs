using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDotNetV1.MyFilters
{
    public class CustomFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //HttpCookie ch = filterContext.HttpContext.Request.Cookies["c"];

            //if(ch == null)
            //filterContext.Result = new RedirectResult("/Account/Login");

            //session

         string sEmail =  filterContext.HttpContext.Session["Email"].ToString();
                if (sEmail == null)
                filterContext.Result = new RedirectResult("/Account/Login");

            string sPassword = filterContext.HttpContext.Session["Password"].ToString();
            if (sPassword == null)
                filterContext.Result = new RedirectResult("/Account/Login");


            // base.OnActionExecuting(filterContext);
        }

        //after
        //public override void OnResultExecuted(ResultExecutedContext filterContext)
        //{
        //    base.OnResultExecuted(filterContext);
        //}
    }
}