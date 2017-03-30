using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace G2.Frameworks.MVC.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string ActionEncrypted(this UrlHelper url, string action, string controller, object routeValues)
        {
            if (!MVC.Utilities.IsQueryStringEncryptionEnabled)
            {
                return url.Action(action, controller, routeValues);
            }
            var _encryptedArgs = MVC.Utilities.EncryptUrlParams(routeValues);
            return url.Action(action, controller, new { qs = _encryptedArgs });
        }
    }
}
