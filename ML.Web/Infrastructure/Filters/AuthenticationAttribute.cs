using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DFM = G2.Frameworks.MVC;
namespace G2.ML.Web.Infrastructure.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthenticationAttribute : DFM.Filters.AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (base.AuthorizeCore(httpContext))
            {
                bool _isAuthenticated = (Web.SessionManager.CurrentLoggedInUser != null);
                return _isAuthenticated;
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new RedirectResult(Web.Common.LoginPageUrl, false);
            }
            else
            {
                throw new Frameworks.Core.BaseException("E100000", "Your session has timed out. Please sign in again.");
            }
        }
    }
}