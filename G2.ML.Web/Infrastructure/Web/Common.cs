using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

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