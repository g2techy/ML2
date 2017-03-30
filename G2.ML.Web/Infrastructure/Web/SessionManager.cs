using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace G2.ML.Web.Infrastructure.Web
{
    public class SessionManager
    {
        private const string m_SessionName_LoggedInUser = "LoggedInUser";

        public static LoggedInUser CurrentLoggedInUser
        {
            get
            {
                if (HttpContext.Current.Session[m_SessionName_LoggedInUser] != null)
                {
                    return (HttpContext.Current.Session[m_SessionName_LoggedInUser] as LoggedInUser);
                }
                return null;
            }
        }

        public static bool SetLoggedInUserSession(Models.UserLoginResultVM userVM)
        {
            bool _success = false;
            try
            {
                LoggedInUser _user = new LoggedInUser()
                {
                    UserID = userVM.UserID,
					ClientID = userVM.ClientID,
                    UserName = userVM.UserName,
                    DisplayName = string.Format(@"{0} {1}", userVM.FirstName, userVM.LastName)
                };

                HttpContext.Current.Session[m_SessionName_LoggedInUser] = _user;
            }
            catch
            {
                _success = false;
            }
            return _success;
        }

        public static void ClearSession()
        {
            HttpContext _httpContext = HttpContext.Current;
            if (_httpContext != null)
            {
                _httpContext.Session.Clear();
                _httpContext.Session.Abandon();
                _httpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                _httpContext.Response.Cache.SetExpires(DateTime.UtcNow);
                _httpContext.Response.Cache.SetNoStore();
            }
        }
    }

    public class LoggedInUser
    {
        public int UserID { get; set; }
		public int ClientID { get; set; }
		public string UserName { get; set; }
        public string DisplayName { get; set; }
    }
}