using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace G2.Frameworks.MVC.Extensions
{
    public static class HtmlHelperExtensions
    {

        public static MvcHtmlString ActionLinkEncrypted(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, 
                                                        object routeValues, object htmlAttributes)
        {
            if (!MVC.Utilities.IsQueryStringEncryptionEnabled)
            {
                return htmlHelper.ActionLink(linkText: linkText,
                                         actionName: actionName,
                                         controllerName: controllerName,
                                         routeValues: routeValues,
                                         htmlAttributes: htmlAttributes);
            }
            var _encryptedArgs = MVC.Utilities.EncryptUrlParams(routeValues);
            return htmlHelper.ActionLink(linkText: linkText,
                                         actionName: actionName,
                                         controllerName: controllerName,
                                         routeValues: new { qs = _encryptedArgs },
                                         htmlAttributes: htmlAttributes);
        }

    }
}
