using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace YourProjectName
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CheckSessionOutAttribute : ActionFilterAttribute
    {
        public string idPermission { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session != null)
            {
                if (context.Session.IsNewSession)
                {
                    string sessionCookie = context.Request.Headers["Cookie"];

                    if ((sessionCookie != null) && (sessionCookie.IndexOf("ASP.NET_SessionId") >= 0))
                    {
                        FormsAuthentication.SignOut();
                        string redirectTo = "~/Home/Index";
                        if (!string.IsNullOrEmpty(context.Request.RawUrl))
                        {
                            redirectTo = string.Format("~/Home/Index?ReturnUrl={0}", HttpUtility.UrlEncode(context.Request.RawUrl));
                            filterContext.Result = new RedirectResult(redirectTo);
                            return;
                        }

                    }
                }

                if (HttpContext.Current.Session["Permissions"] == null)
                {
                    filterContext.Result = new RedirectResult("~/Home/Index");
                    return;
                }
                else
                {
                    if (!((List<string>)HttpContext.Current.Session["Permissions"]).Contains(idPermission))
                    {
                        filterContext.Result = new RedirectResult("~/Home/NoAccess");
                        return;
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}