using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace G2.Frameworks.MVC
{
    public static class Utilities
    {
        internal static bool IsQueryStringEncryptionEnabled
        {
            get
            {
                return Security.QueryStringEncryption.IsEnabled;
            }
        }

        internal static string EncryptUrlParams(object args)
        {
            var _list = new RouteValueDictionary(args);
            var _items = new List<string>();

            foreach (var _entry in _list)
            {
                _items.Add(_entry.Key + "=" + ((_entry.Value != null) ? _entry.Value.ToString() : ""));
            }

            string _encryptedUrl = Security.QueryStringEncryption.EncryptQuery(string.Join("&", _items));
            if (_encryptedUrl.StartsWith(Security.QueryStringEncryption.PARAMETER_NAME, StringComparison.OrdinalIgnoreCase))
            {
                _encryptedUrl = _encryptedUrl.Replace(Security.QueryStringEncryption.PARAMETER_NAME, string.Empty);
            }
            return _encryptedUrl;
        }

        internal static Logging.ILogManager LogManager
        {
            get
            {
                return Logging.DefaultLogManagerFactory.LogManager;
            }
        }

        internal static bool IsAjaxRequest
        {
            get
            {
                bool _isAjaxRequest = false;

                var _request = HttpContext.Current.Request;

                _isAjaxRequest = (_request["X-Requested-With"] == "XMLHttpRequest") ||
                                 ((_request.Headers != null) && (_request.Headers["X-Requested-With"] == "XMLHttpRequest"));

                return _isAjaxRequest;
            }
        }

        internal static string DefaultErrorNumber
        {
            get
            {
                return Core.BaseException.DefaultErrorNumber;
            }
        }

        internal static string NewErrorID
        {
            get
            {
                return Core.BaseException.NewErrorID;
            }
        }

        internal static string JsonContentType
        {
            get
            {
                return "application/json";
            }
        }
    }
}
