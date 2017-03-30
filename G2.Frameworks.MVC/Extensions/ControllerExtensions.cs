using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace G2.Frameworks.MVC.Extensions
{
    public static class ControllerExtensions
    {
        public static ActionResult RedirectToActionEncrypted(this Controller controllerBase, string action, object routeValues)
        {
            if (!MVC.Utilities.IsQueryStringEncryptionEnabled)
            {
                var _newRouteValues = new RouteValueDictionary(routeValues);
                _newRouteValues.Add("action", action);
                return new RedirectToRouteResult(_newRouteValues);
            }
            var _encryptedArgs = MVC.Utilities.EncryptUrlParams(routeValues);
            return new RedirectToRouteResult(new RouteValueDictionary(new
            {
                action = action,
                qs = _encryptedArgs
            }));
        }

        public static ActionResult RedirectToActionEncrypted(this Controller controllerBase, string action, string controller, object routeValues)
        {
            if (!MVC.Utilities.IsQueryStringEncryptionEnabled)
            {
                var _newRouteValues = new RouteValueDictionary();
                _newRouteValues.Add("action", action);
                _newRouteValues.Add("controller", controller);
                return new RedirectToRouteResult(_newRouteValues);
            }
            var _encryptedArgs = MVC.Utilities.EncryptUrlParams(routeValues);
            return new RedirectToRouteResult(new RouteValueDictionary(new
            {
                controller = controller,
                action = action,
                qs = _encryptedArgs
            }));
        }
    }
}
