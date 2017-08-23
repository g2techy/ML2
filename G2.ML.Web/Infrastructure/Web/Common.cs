using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace G2.ML.Web.Infrastructure.Web
{
    public static class Common
    {
        public const string LoginPageUrl = "~/Account/Login";
		public const string DashboardUrl = "~/Dashboard/Index";

		public static bool IsDebuggingEnabled
		{
			get
			{
				return HttpContext.Current.IsDebuggingEnabled;
			}
		}

		public static string AppBaseUrl
		{
			get
			{
				var _request = HttpContext.Current.Request;
				return string.Format("{0}://{1}", _request.Url.Scheme, _request.Url.Authority);
			}
		}
		public static void RedirectToLoginPage()
        {
            HttpContext.Current.Response.Redirect(LoginPageUrl, true);
        }

		public static void RedirectToDashboard()
		{
			HttpContext.Current.Response.Redirect(DashboardUrl, true);
		}

		public static string GetWebAppSettingParam(string paramName)
        {
            return ConfigurationManager.AppSettings[paramName];
        }
        
    }
    
}