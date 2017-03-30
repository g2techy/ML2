using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace G2.Frameworks.MVC.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class VerifySSLRequiredAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        private string _loginUrl = string.Empty;
        public VerifySSLRequiredAttribute(string loginUrl)
        {
            if (string.IsNullOrEmpty(loginUrl))
            {
                throw new ArgumentNullException("loginUrl");
            }
            _loginUrl = loginUrl;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool _isSSLRequest = (httpContext.Request.IsLocal || httpContext.Request.IsSecureConnection);
            return _isSSLRequest;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string _loginUrlFinal = "https://" + HttpContext.Current.Request.Url.Host;
            if (HttpContext.Current.Request.Url.Port != 80)
            {
                _loginUrlFinal += ":" + HttpContext.Current.Request.Url.Port.ToString();
            }
            string _loginUrlLocal = _loginUrl;
            if (_loginUrlLocal.StartsWith("~/"))
            {
                _loginUrlLocal = _loginUrlLocal.Substring(1);
            }
            if (!_loginUrlLocal.StartsWith("/"))
            {
                _loginUrlLocal = "/" + _loginUrlLocal;
            }
            _loginUrlFinal += _loginUrlLocal;

            filterContext.Result = new RedirectResult(_loginUrlFinal, false);
        }
    }
}
