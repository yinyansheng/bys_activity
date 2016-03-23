using bys.activity.web.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bys.activity.web.Filters
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var isAllowed = AuthorizeCore(filterContext.HttpContext);

            if (!isAllowed)
            {
                filterContext.RequestContext.HttpContext.Response.Write("Access Denied,Need Administrator Permission");
                filterContext.RequestContext.HttpContext.Response.End();
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("HttpContext");
            }
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }
            if (Configer.Administrators.Contains(httpContext.User.Identity.Name.Split('\\')[1]))
            {
                return true;
            }
            return false;
        }
    }
}