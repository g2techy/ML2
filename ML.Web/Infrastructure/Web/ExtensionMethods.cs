using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace G2.ML.Web.Infrastructure.Web
{
    public static class ExtensionMethods
    {
        public static bool HasFile(this HttpPostedFileBase file)
        {
            return file != null && file.ContentLength > 0;
        }

        public static string ToFriendlyUrl(this string url)
        {
            // make the url lowercase
            string _friendlyUrl = (url ?? "").ToLower();

            // replace & with and
            _friendlyUrl = Regex.Replace(_friendlyUrl, @"\&+", "and");

            // remove characters
            _friendlyUrl = _friendlyUrl.Replace("'", "");

            // remove invalid characters
            _friendlyUrl = Regex.Replace(_friendlyUrl, @"[^a-z0-9]", "-");

            // remove duplicates
            _friendlyUrl = Regex.Replace(_friendlyUrl, @"-+", "-");

            // trim leading & trailing characters
            _friendlyUrl = _friendlyUrl.Trim('-');

            return _friendlyUrl;
        }

        public static HelperResult RenderSection(this WebPageBase webPage, string name, Func<dynamic, HelperResult> defaultContents)
        {
            if (webPage.IsSectionDefined(name))
            {
                return webPage.RenderSection(name);
            }
            return defaultContents(null);
        }

        public static string AbsoluteRouteUrl(this UrlHelper urlHelper, string routeName, object routeValues = null)
        {
            string scheme = urlHelper.RequestContext.HttpContext.Request.Url.Scheme;
            return urlHelper.RouteUrl(routeName, routeValues, scheme);
        }

        public static ViewContext TopMostParent(this ViewContext context)
        {
            ViewContext result = context;
            while (result.ParentActionViewContext != null)
            {
                result = result.ParentActionViewContext;
            }
            return result;
        }
    }
}